using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftReportSample
{
    /// <summary>
    /// レポートに出力するサンプルデータを管理するクラスを定義します。
    /// </summary>
    public class ReportSample
    {
        // ------------------------------------------------------------------------------------------------------------
        #region コンストラクタ

        /// <summary>
        /// MicrosoftReportSample.ReportSample クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ReportSample()
        {
            var list = this.FList;
            list.Add(new Person("健太", "前田",  new DateTime(1988, 4, 11)));
            list.Add(new Person("優也", "福井", new DateTime(1988, 2, 8)));
            list.Add(new Person("大地", "大瀬良", new DateTime(1991, 6, 17)));
            list.Add(new Person("博樹", "黒田", new DateTime(1975, 2, 10)));
            list.Add(new Person("猛", "今村", new DateTime(1991, 4, 17)));
            list.Add(new Person("哲也", "小窪", new DateTime(1985, 4, 12)));
            list.Add(new Person("涼介", "菊池", new DateTime(1990, 3, 11)));
        }

        #endregion
        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報のリストを管理します。
        /// </summary>
        private List<Person> FList = new List<Person>();

        /// <summary>
        /// 個人情報の一覧を取得します。
        /// </summary>
        public ICollection<Person> List
        {
            get
            {
                return this.FList;
            }
        }
    }

    /// <summary>
    /// レポートに出力する個人情報を管理するクラスを定義します。
    /// </summary>
    public class Person
    {
        /// <summary>
        /// 名前を設定／取得します。
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 苗字を設定／取得します。
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// 年齢を設定／取得します。
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 誕生日を設定／取得します。
        /// </summary>
        public DateTime BirthDay { get; set; }

        // ------------------------------------------------------------------------------------------------------------
        #region コンストラクタ

        /// <summary>
        /// MicrosoftReportSample.ReportSample クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Person(string firstName, string familyName, DateTime birthday)
        {
            this.FirstName = firstName;
            this.FamilyName = familyName;
            var time = DateTime.Now - birthday;
            var date = new DateTime(time.Ticks);
            this.Age = date.Year;
            this.BirthDay = birthday;
        }

        #endregion
        // ------------------------------------------------------------------------------------------------------------

    }
}
