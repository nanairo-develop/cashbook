namespace cashbook
{
    public partial class UserDateCombo : UserControl
    {
        public int IntYear
        {
            get => Year.Text == string.Empty || Year.Text == "0" ? DateTime.Now.Year : int.Parse(Year.Text);
            set => Year.Text = value.ToString();
        }
        public int IntMonth
        {
            get => Month.Text == string.Empty || Month.Text == "0" ? DateTime.Now.Month : int.Parse(Month.Text);
            set => Month.Text = value.ToString();
        }
        public int IntDay
        {
            get => Day.Text == string.Empty || Day.Text == "0" ? DateTime.Now.Day : int.Parse(Day.Text);
            set => Day.Text = value.ToString();
        }
        public string TextYear
        {
            get => Year.Text;
            set => Year.Text = value;
        } 
        public string TextMonth
        {
            get => Month.Text;
            set => Month.Text = value;
        }
        public string TextDay
        {
            get => Day.Text;
            set => Day.Text = value;
        }
        public DateTime DateTimeFrom
        {
            get; set;
        }

        public DateTime Value
        {
            get
            {
                if (!int.TryParse(TextYear, out int year))
                {
                    year = DateTimeFrom.Year;
                }
                if (!int.TryParse(TextMonth, out int month))
                {
                    month = DateTimeFrom.Month;
                }
                if (!int.TryParse(TextDay, out int day))
                {
                    day = DateTimeFrom.Day;
                }
                return new(year, month, day);
            }
            set
            {
                IntYear = value.Year;
                IntMonth = value.Month;
                IntDay = value.Day;
                DatePicker.Value = new(IntYear, IntMonth, IntDay);
            }
        }

        #region コンストラクタ
        public UserDateCombo()
        {
            InitializeComponent();
        }
        public UserDateCombo(int year, int month, int day)
        {
            InitializeComponent();
            IntYear = year;
            IntMonth = month;
            IntDay = day;
        }
        public UserDateCombo(DateTime dateTime)
        {
            InitializeComponent();
            Value = dateTime;
        }
        #endregion コンストラクタ

        #region イベント
        private void UserDateCombo_Load(object sender, EventArgs e)
        {
            SetDateTimeCombo(Value);
        }
        private void Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            YearMonth_Changed();
        }

        private void Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            YearMonth_Changed();
        }
        private void Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePicker.Value = new(IntYear, IntMonth, IntDay);
        }

        #endregion イベント

        #region メソッド
        private void SetDateTimeCombo(DateTime dateTime)
        {
            SetYearCombo(dateTime);
            SetMonthCombo(dateTime);
            SetDayCombo(dateTime);
        }

        private void SetYearCombo(DateTime dateTime)
        {
            int minYear = DatePicker.MinDate.Year;
            int maxYear = DatePicker.MaxDate.Year;
            for (int i = minYear; i <= maxYear; i++)
            {
                _ = Year.Items.Add(i);
            }
            IntYear = dateTime.Year;
        }
        private void SetMonthCombo(DateTime dateTime)
        {
            for (int i = 1; i <= 12; i++)
            {
                _ = Month.Items.Add(i);
            }
            IntMonth = dateTime.Month;
        }
        private void SetDayCombo(DateTime dateTime)
        {
            Day.Items.Clear();
            TimeSpan subst = new(1, 0, 0, 0);
            int dayCount = (new DateTime(IntYear, IntMonth + 1, 1) - subst).Day;
            for (int i = 1; i <= dayCount; i++)
            {
                _ = Day.Items.Add(i);
            }
            IntDay = dateTime.Day;
        }

        private void YearMonth_Changed()
        {
            SetDayCombo(new DateTime(IntYear, IntMonth, 1));
            DatePicker.Value = new(IntYear, IntMonth, 1);
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
        public void SetDispValue(DateTime dateTime)
        {
            Year.Text = dateTime.Year.ToString();
            Month.Text = dateTime.Month.ToString();
            Day.Text = dateTime.Day.ToString();
        }
        #endregion メソッド

    }
}
