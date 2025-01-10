using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Snow.Calendar.Web.Model;
using System.Text.Json;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 资源类
    /// </summary>
    public class Resource
    {
        private readonly IFileProvider _fileProvider;
        private readonly IMemoryCache _cache;

        public Resource(IFileProvider fileProvider,
            IMemoryCache cache)
        {
            _fileProvider = fileProvider;
            _cache = cache;
        }

        /// <summary>
        /// 节假日(休息)
        /// </summary>
        public Dictionary<int, Dictionary<int, int[]>> Holidays => Get<Dictionary<int, Dictionary<int, int[]>>>("Holidays", "Config/holiday.json") ?? [];

        /// <summary>
        /// 工作日(补班)
        /// </summary>
        public Dictionary<int, Dictionary<int, int[]>> Workdays => Get<Dictionary<int, Dictionary<int, int[]>>>("Workdays", "Config/workday.json") ?? [];

        /// <summary>
        /// 按公历计算的节日
        /// </summary>
        public SolarHoliday[] SolarHoliday => Get<SolarHoliday[]>("SolarHoliday", "Config/solarHoliday.json") ?? [];

        /// <summary>
        /// 按农历计算的节日
        /// </summary>
        public LunarHoliday[] LunarHoliday => Get<LunarHoliday[]>("LunarHoliday", "Config/lunarHoliday.json") ?? [];

        /// <summary>
        /// 按某月第几个星期几
        /// </summary>
        public WeekHoliday[] WeekHoliday => Get<WeekHoliday[]>("WeekHoliday", "Config/weekHoliday.json") ?? [];

        /// <summary>
        /// 节气
        /// </summary>
        public SolarModel[] SolarTerms => Get<SolarModel[]>("SolarTerms", "Config/solarTerm.json") ?? [];

        /// <summary>
        /// 获取Json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private T? Get<T>(string key, string path)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                entry.AddExpirationToken(_fileProvider.Watch(path));

                IFileInfo file = _fileProvider.GetFileInfo(path);
                using (var stream = file.CreateReadStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string output = reader.ReadToEnd();
                        return JsonSerializer.Deserialize<T>(output);
                    }
                }
            });
        }

        public readonly Dictionary<DayOfWeek, string> OneWeek = new Dictionary<DayOfWeek, string>()
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
        {
            DayOfWeek.Saturday,
            DayOfWeek.Sunday
        };
    }
}