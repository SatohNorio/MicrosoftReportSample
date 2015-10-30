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
    /// PreviewWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PreviewWindow : Window
    {
        // ------------------------------------------------------------------------------------------------------------
        #region コンストラクタ

        /// <summary>
        /// MicrosoftReportSample.PreviewWindow クラスの新しいインスタンスを初期化します。
        /// </summary>
        public PreviewWindow()
        {
            InitializeComponent();
            this.FPersonBindingSource = new BindingSource();
        }

        /// <summary>
        /// BindingSourceを使用して MicrosoftReportSample.PreviewWindow クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="source"></param>
        public PreviewWindow(BindingSource source)
        {
            InitializeComponent();
            this.FPersonBindingSource = source;
        }

        #endregion
        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// レポート用のBindingSourceを管理します。
        /// </summary>
        private BindingSource FPersonBindingSource;

        /// <summary>
        /// ReportViewerのロードイベントを処理します。
        /// </summary>
        /// <param name="sender">イベントを送信したオブジェクトを指定します。</param>
        /// <param name="e">イベント引数を指定します。</param>
        private void FReportViewerLoad(object sender, EventArgs e)
        {
            if (!this.FReportViewerLoaded)
            {
                var reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "DataSet";
                reportDataSource1.Value = this.FPersonBindingSource;

                this.FReportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.FReportViewer.LocalReport.ReportEmbeddedResource = "MicrosoftReportSample.Report.rdlc";
                this.FReportViewer.RefreshReport();
                this.FReportViewerLoaded = true;
            }

        }

        /// <summary>
        /// ReportViewerの初期化完了状態を管理します。
        /// </summary>
        private bool FReportViewerLoaded = false;

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.FReportViewer.PrintDialog();
        }
    }
}
