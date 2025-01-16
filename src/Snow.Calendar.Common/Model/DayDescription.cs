namespace Snow.Calendar.Common.Model
{
    public class CanlendarDayOutput
    {
        public int Year { get; set; }
        public string YearDescription { get; set; }
        public IEnumerable<CalendarMonth> Months { get; set; }
    }

    public class CanlendarDay
    {
        public int Day { get; set; }
        public string Date { get; set; }
        public DayType DayType { get; set; }
    }
}