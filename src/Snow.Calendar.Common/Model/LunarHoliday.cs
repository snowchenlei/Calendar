namespace Snow.Calendar.Common.Model
{
    /// <summary>
    /// 按农历计算的节日
    /// </summary>
    public class LunarHoliday
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Recess { get; set; }
        public string HolidayName { get; set; }
        public string HolidayAll { get; set; }
    }
}