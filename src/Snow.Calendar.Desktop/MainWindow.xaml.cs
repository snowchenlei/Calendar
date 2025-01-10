using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snow.Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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

            // 显示当前日期的日历
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
            string[] daysOfWeek = ["日", "一", "二", "三", "四", "五", "六"];
            foreach (var day in daysOfWeek)
            {
                CalendarGrid.Children.Add(new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(220, 220, 220)),
                    BorderThickness = new Thickness(1),
                    Background = new SolidColorBrush(Color.FromRgb(240, 240, 240)),
                    Child = new TextBlock
                    {
                        Text = day,
                        FontWeight = FontWeights.Bold,
                        FontSize = 16,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });
            }

            // 填充空白天数
            for (int i = 0; i < firstDayOfWeek; i++)
            {
                CalendarGrid.Children.Add(new Border());
            }

            // 填充日期
            var chineseCalendar = new ChineseLunisolarCalendar();
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDay = new DateTime(year, month, day);
                bool isWeekend = currentDay.DayOfWeek == DayOfWeek.Saturday || currentDay.DayOfWeek == DayOfWeek.Sunday;
                bool isToday = currentDay.Date == DateTime.Today;
                string lunarDay = GetLunarDate(chineseCalendar, currentDay, out bool isFirstDayOfMonth);
                Border cell = new()
                {
                    BorderBrush = isToday ? new SolidColorBrush(Colors.Orange) : new SolidColorBrush(Colors.Gray),
                    BorderThickness = isToday ? new Thickness(2) : new Thickness(1),
                    Margin = new Thickness(5),
                    Background = isWeekend
                        ? new SolidColorBrush(Color.FromRgb(255, 230, 230))
                        : new SolidColorBrush(Colors.White),
                    CornerRadius = new CornerRadius(4),
                    Child = new StackPanel()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Children =
                        {
                            new TextBlock
                            {
                                Text = day.ToString(),
                                FontSize = 16,
                                FontWeight = isToday ? FontWeights.Bold : FontWeights.Normal,
                                Foreground = isToday
                                    ? new SolidColorBrush(Colors.OrangeRed)
                                    : (isWeekend ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Black)),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center
                            },
                            new TextBlock
                            {
                                Text = lunarDay,
                                FontSize = 12,
                                FontWeight = isFirstDayOfMonth ? FontWeights.Bold : FontWeights.Normal,
                                Foreground = new SolidColorBrush(Color.FromRgb(120, 120, 120)),
                                HorizontalAlignment = HorizontalAlignment.Center
                            }
                        }
                    }
                };
                cell.MouseLeftButtonDown += (sender, e) => ShowDateDetails(currentDay, lunarDay);
                CalendarGrid.Children.Add(cell);
            }
        }

        private void ShowDateDetails(DateTime date, string lunarDay)
        {
            string details = $"公历: {date:yyyy年MM月dd日}\n" +
                             $"星期: {date:dddd}\n" +
                             $"农历: {lunarDay}";

            DetailsTextBlock.Text = details;
            DetailsPanel.Visibility = Visibility.Visible;
        }

        private void HideDetailsPanel_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Collapsed;
        }

        private string GetLunarDate(ChineseLunisolarCalendar chineseCalendar, DateTime date, out bool isFirstDayOfMonth)
        {
            int lunarYear = chineseCalendar.GetYear(date);
            int lunarMonth = chineseCalendar.GetMonth(date);
            int lunarDay = chineseCalendar.GetDayOfMonth(date);

            // 农历月份和日期
            string[] lunarMonths = ["正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "冬月", "腊月"];
            string[] lunarDays =
            [
                "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十",
                "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十",
                "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十"
            ];

            // 判断是否为农历月初
            isFirstDayOfMonth = lunarDay == 1;

            // 返回格式化农历日期
            try
            {
                return isFirstDayOfMonth ? lunarMonths[lunarMonth - 1] : lunarDays[lunarDay - 1];
            }
            catch
            {
                return lunarMonths[0];
            }
        }
    }
}