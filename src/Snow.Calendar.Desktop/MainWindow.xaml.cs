using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Snow.Calendar.Common;
using Snow.Calendar.Common.Model;
using Snow.Calendar.Common.Service;

namespace Snow.Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double MinWidthToShowDetails = 800;
        private readonly IDateHelper _dateHelper;
        private readonly ICalendarDateHelper _calendarDateHelper;
        private readonly Resource _resource;
        private Border? selectedCell = null;
        private Border? todayCell = null;

        public MainWindow(IDateHelper dateHelper, ICalendarDateHelper calendarDateHelper, Resource resource)
        {
            _dateHelper = dateHelper;
            _calendarDateHelper = calendarDateHelper;
            _resource = resource;

            InitializeComponent();
            InitializeYearAndMonth();
        }

        private void InitializeYearAndMonth()
        {
            // 初始化年份和月份选择框
            for (int year = 1900; year <= 2100; year++)
            {
                YearComboBox.Items.Add(year);
            }
            YearComboBox.SelectedItem = DateTime.Now.Year;

            for (int month = 1; month <= 12; month++)
            {
                MonthComboBox.Items.Add(month);
            }
            MonthComboBox.SelectedItem = DateTime.Now.Month;

            GenerateCalendar(DateTime.Now.Year, DateTime.Now.Month);
        }

        private void YearOrMonthChanged(object sender, SelectionChangedEventArgs e)
        {
            if (YearComboBox.SelectedItem == null || MonthComboBox.SelectedItem == null)
                return;

            int year = (int)YearComboBox.SelectedItem;
            int month = (int)MonthComboBox.SelectedItem;

            GenerateCalendar(year, month);
        }

        private void GenerateCalendar(int year, int month)
        {
            CalendarGrid.Children.Clear();
            // 获取当月的第一天和天数
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            // 计算总单元格数和行数
            int totalCells = firstDayOfWeek + daysInMonth;
            int rows = (int)Math.Ceiling(totalCells / 7.0);

            // 设置 UniformGrid 行和列
            CalendarGrid.Rows = rows + 1; // 多一行用于显示星期标题
            CalendarGrid.Columns = 7;
            // 添加星期标题
            Dictionary<DayOfWeek, string> daysOfWeek = new Dictionary<DayOfWeek, string>()
            {
                [DayOfWeek.Sunday] = "日",
                [DayOfWeek.Monday] = "一",
                [DayOfWeek.Tuesday] = "二",
                [DayOfWeek.Wednesday] = "三",
                [DayOfWeek.Thursday] = "四",
                [DayOfWeek.Friday] = "五",
                [DayOfWeek.Saturday] = "六",
            };
            foreach (KeyValuePair<DayOfWeek, string> day in daysOfWeek)
            {
                TextBlock text = new TextBlock
                {
                    Text = day.Value,
                    FontWeight = FontWeights.Bold,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                if (day.Key is DayOfWeek.Saturday or DayOfWeek.Sunday)
                {
                    text.Foreground = new SolidColorBrush(Colors.Red);
                }
                CalendarGrid.Children.Add(new Border
                {
                    Child = text
                });
            }

            IEnumerable<DateTime> days = _dateHelper.GetDatesByMonth(year, month);
            CalendarDate[] dates = _calendarDateHelper.GetCalendarDates(days).ToArray();
            for (int i = 0, max = Array.IndexOf(_resource.OneWeek.Keys.ToArray(), dates.First().CalendarDay.DayOfWeek); i < max; i++)
            {
                CalendarGrid.Children.Add(new Border());
            }
            foreach (CalendarDate date in dates)
            {
                CanlendarDayInfo calendarDay = GetCalendarDay(date);
                TextBlock content = new TextBlock
                {
                    Text = date.CalendarDay.CurrentDay.ToString(),
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                DateTime currentDay = new DateTime(year, month, calendarDay.CurrentDay);
                bool isWeekend = currentDay.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
                bool isToday = currentDay.Date == DateTime.Today;
                string lunarDay = calendarDay.LunarDayText;

                if (isWeekend)
                {
                    content.Foreground = new SolidColorBrush(Colors.Red);
                }
                if (isToday)
                {
                    content.Foreground = new SolidColorBrush(Colors.Green);
                }
                Border cell = new()
                {
                    Background = Brushes.Transparent,
                    CornerRadius = new CornerRadius(4),
                    Child = new StackPanel()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Children =
                        {
                            content,
                            new TextBlock
                            {
                                Text = lunarDay,
                                FontSize = 12,
                                FontWeight = date.CalendarDay.LunarDay == 1 ? FontWeights.Bold : FontWeights.Normal,
                                Foreground = new SolidColorBrush(Color.FromRgb(120, 120, 120)),
                                HorizontalAlignment = HorizontalAlignment.Center
                            }
                        }
                    }
                };
                if (isToday)
                {
                    cell.BorderBrush = new SolidColorBrush(Colors.Orange);
                    cell.BorderThickness = new Thickness(1);
                    todayCell = cell;
                    ShowDateDetails(currentDay, lunarDay);
                }
                cell.MouseLeftButtonDown += (sender, e) =>
                {
                    if (selectedCell is not null)
                    {
                        selectedCell.BorderBrush = new SolidColorBrush(Colors.White);
                        selectedCell.BorderThickness = new Thickness(0);
                    }

                    if (todayCell is not null)
                    {
                        todayCell.BorderBrush = new SolidColorBrush(Colors.Orange);
                        todayCell.BorderThickness = new Thickness(1);
                    }

                    Border currentCell = (Border)sender;
                    selectedCell = currentCell;
                    currentCell.BorderBrush = new SolidColorBrush(Colors.Green);
                    currentCell.BorderThickness = new Thickness(1);
                    ShowDateDetails(currentDay, lunarDay);
                };
                CalendarGrid.Children.Add(cell);
            }
        }

        /// <summary>
        /// 获取万年历信息
        /// </summary>
        /// <param name="calendarDate"></param>
        /// <returns></returns>
        private CanlendarDayInfo GetCalendarDay(CalendarDate calendarDate)
        {
            return new CanlendarDayInfo
            {
                CurrentDay = calendarDate.CalendarDay.CurrentDay,
                LunarDayText = calendarDate.CalendarDay.LunarDayText,
                DayType = calendarDate.CalendarDay.DayType,
            };
        }

        private void ShowDateDetails(DateTime date, string lunarDay)
        {
            string details = $"公历: {date:yyyy年MM月dd日}\n" +
                             $"星期: {date:dddd}\n" +
                             $"农历: {lunarDay}";

            DetailsTextBlock.Text = details;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 获取当前窗口宽度
            double currentWidth = e.NewSize.Width;

            // 动态设置右侧列宽
            DetailsColumn.Width = currentWidth < MinWidthToShowDetails ? new GridLength(0) : new GridLength(1, GridUnitType.Star);
        }

        private void Today_OnClick(object sender, RoutedEventArgs e)
        {
            YearComboBox.SelectedItem = DateTime.Now.Year;
            MonthComboBox.SelectedItem = DateTime.Now.Month;
            GenerateCalendar(DateTime.Now.Year, DateTime.Now.Month);
        }
    }
}