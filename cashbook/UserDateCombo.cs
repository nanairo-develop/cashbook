namespace cashbook
{
    public partial class UserDateCombo : UserControl
    {
        public int IntYear
        {
            get => Year.Text == string.Empty ? DateTime.Now.Year : (int)Year.Value;
            set => Year.Value = value;
        }
        public int IntMonth
        {
            get => Month.Text == string.Empty ? DateTime.Now.Month : (int)Month.Value;
            set => Month.Value = value;
        }
        public int IntDay
        {
            get => Day.Text == string.Empty ? DateTime.Now.Day : (int)Day.Value;
            set => Day.Value = value;
        }
        public string TextYear
        {
            set => Year.Text = value;
        }
        public string TextMonth
        {
            set => Month.Text = value;
        }
        public string TextDay
        {
            set => Day.Text = value;
        }
        public DateTime DateTimeFrom
        {
            get; set;
        }

        public DateTime Value
        {
            get => DatePicker.Value;
            set => DatePicker.Value = Value;
        }
        #region コンストラクタ
        public UserDateCombo()
        {
            InitializeComponent();
        }
        #endregion コンストラクタ

        #region イベント
        private void UserDateCombo_Load(object sender, EventArgs e)
        {
            SetDateNumericUpdown(Value);
        }
        private void Year_ValueChanged(object sender, EventArgs e)
        {
            // 2月の場合は年が変更された場合に末日の調整をする
            if (IntMonth == 2)
            {
                YearMonth_Changed();
            }
            else
            {
                DatePicker.Value = new(IntYear, IntMonth, IntDay);
            }
        }

        private void Month_ValueChanged(object sender, EventArgs e)
        {
            YearMonth_Changed();
        }

        private void Day_ValueChanged(object sender, EventArgs e)
        {
            DatePicker.Value = new(IntYear, IntMonth, IntDay);
        }

        #endregion イベント

        #region メソッド
        private void SetDateNumericUpdown(DateTime dateTime)
        {
            SetYearNumericUpdown(dateTime);
            SetDayNumericUpdown(dateTime.Day);
        }

        private void SetYearNumericUpdown(DateTime dateTime)
        {
            Year.Minimum = DatePicker.MinDate.Year;
            Year.Maximum = DatePicker.MaxDate.Year;
            IntYear = dateTime.Year;
        }
        private void SetDayNumericUpdown(int intDay)
        {
            TimeSpan subst = new(1, 0, 0, 0);
            if (IntMonth < 12)
            {
                Day.Maximum = (new DateTime(IntYear, IntMonth + 1, 1) - subst).Day;
            }
            else
            {
                Day.Maximum = 31;
            }
            // 年、月が変わることで末日が変わる場合、
            // 日付として妥当な末日に置き換える
            if (intDay <= Day.Maximum)
            {
                IntDay = intDay;
            }
            else
            {
                IntDay = (int)Day.Maximum;
            }
        }

        private void YearMonth_Changed()
        {
            SetDayNumericUpdown(IntDay);
            DatePicker.Value = new(IntYear, IntMonth, IntDay);
        }

        public void SetValue(DateTime dateTime)
        {
            Value = dateTime;
            TextYear = dateTime.Year.ToString();
            TextMonth = dateTime.Month.ToString();
            TextDay = dateTime.Day.ToString();
        }

        public bool IsDispValue()
        {
            bool ret = true;
            if (Year.Text == string.Empty || Month.Text == string.Empty || Day.Text == string.Empty)
            {
                ret = false;
            }

            return ret;
        }
        public bool IsYearDisp()
        {
            return Year.Text == string.Empty;
        }
        #endregion メソッド
    }
}
