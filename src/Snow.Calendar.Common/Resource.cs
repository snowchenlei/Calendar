using Snow.Calendar.Common.Model;

namespace Snow.Calendar.Common;

public abstract class Resource
{
    /// <summary>
    /// 节假日(休息)
    /// </summary>
    public abstract Dictionary<int, Dictionary<int, int[]>> Holidays { get; }

    /// <summary>
    /// 工作日(补班)
    /// </summary>
    public abstract Dictionary<int, Dictionary<int, int[]>> Workdays { get; }

    /// <summary>
    /// 按公历计算的节日
    /// </summary>
    public abstract SolarHoliday[] SolarHoliday { get; }

    /// <summary>
    /// 按农历计算的节日
    /// </summary>
    public abstract LunarHoliday[] LunarHoliday { get; }

    /// <summary>
    /// 按某月第几个星期几
    /// </summary>
    public abstract WeekHoliday[] WeekHoliday { get; }

    /// <summary>
    /// 节气
    /// </summary>
    public abstract SolarModel[] SolarTerms { get; }

    public readonly Dictionary<DayOfWeek, string> OneWeek = new()
    {
        [DayOfWeek.Sunday] = "星期日",
        [DayOfWeek.Monday] = "星期一",
        [DayOfWeek.Tuesday] = "星期二",
        [DayOfWeek.Wednesday] = "星期三",
        [DayOfWeek.Thursday] = "星期四",
        [DayOfWeek.Friday] = "星期五",
        [DayOfWeek.Saturday] = "星期六",
    };

    /// <summary>
    /// 双休日
    /// </summary>
    public readonly DayOfWeek[] RestDay =
    [
        DayOfWeek.Saturday,
        DayOfWeek.Sunday
    ];
}