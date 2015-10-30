using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace MicrosoftReportSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // ------------------------------------------------------------------------------------------------------------
        #region コンストラクタ

        /// <summary>
        /// MicrosoftReportSample.MainWindow クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            var rs = new ReportSample();
            this.FPersonBindingSource.DataSource = rs.List;
        }

        #endregion
        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// レポート用のBindingSourceを管理します。
        /// </summary>
        private BindingSource FPersonBindingSource = new BindingSource();

        // ------------------------------------------------------------------------------------------------------------
        #region MSDNから引用したサンプルプログラム

        private int m_currentPageIndex;

        private IList<Stream> m_streams;

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        // Export the given report as an EMF (Enhanced Metafile) file.
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                return;
            var ptdlg = new System.Windows.Controls.PrintDialog();
            if (ptdlg.ShowDialog() == true)
            {
                //const string printerName = "Microsoft Office Document Image Writer";
                string printerName = ptdlg.PrintQueue.FullName;

                PrintDocument printDoc = new PrintDocument();
                printDoc.PrinterSettings.PrinterName = printerName;
                if (!printDoc.PrinterSettings.IsValid)
                {
                    string msg = String.Format(
                       "Can't find printer \"{0}\".", printerName);
                    throw new InvalidOperationException(msg);
                }
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                printDoc.Print();
            }
        }

        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        private void Run()
        {
            LocalReport report = new LocalReport();
            report.DataSources.Add(new ReportDataSource("DataSet", this.FPersonBindingSource));
            report.ReportEmbeddedResource = "MicrosoftReportSample.Report.rdlc";
            Export(report);
            m_currentPageIndex = 0;
            Print();
        }

        #endregion
        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 印刷ボタンのクリックイベントを処理します。
        /// </summary>
        /// <param name="sender">イベントを送信したオブジェクトを指定します。</param>
        /// <param name="e">イベント引数を指定します。</param>
        private void FPrintButtonClick(object sender, RoutedEventArgs e)
        {
            this.Run();
        }

        /// <summary>
        /// 印刷プレビューボタンのクリックイベントを処理します。
        /// </summary>
        /// <param name="sender">イベントを送信したオブジェクトを指定します。</param>
        /// <param name="e">イベント引数を指定します。</param>
        private void FPreviewButton_Click(object sender, RoutedEventArgs e)
        {
            new PreviewWindow(this.FPersonBindingSource).Show();
        }
    }
}
