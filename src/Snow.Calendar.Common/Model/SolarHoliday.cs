namespace Snow.Calendar.Common.Model
{
    /// <summary>
    /// 按公历计算的节日
    /// </summary>
    public class SolarHoliday
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Recess { get; set; } //假期长度
        public string HolidayName { get; set; } = default!;
        public string HolidayAll { get; set; } = default!;
    }
}