using System.ComponentModel;

namespace Snow.Calendar.Common.Model
{
    /// <summary>
    /// 日类型
    /// </summary>
    public enum DayType
    {
        /// <summary>
        /// 工作日
        /// </summary>
        [Description("工作日")]
        Workday = 1,

        /// <summary>
        /// 周末
        /// </summary>
        [Description("周末")]
        Weekend,

        /// <summary>
        /// 节日
        /// </summary>
        [Description("节日")]
        Holiday,

        /// <summary>
        /// 节日休息
        /// </summary>
        [Description("节日休息")]
        HolidayRest,

        /// <summary>
        /// 补班
        /// </summary>
        [Description("补班")]
        CompensationWork
    }
}