using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Snow.Calendar.Web.Model;

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
        public Dictionary<int, Dictionary<int, int[]>> Holidays => Get<Dictionary<int, Dictionary<int, int[]>>>("Holidays", "Config/holiday.json");

        /// <summary>
        /// 工作日(补班)
        /// </summary>
        public Dictionary<int, Dictionary<int, int[]>> Workdays => Get<Dictionary<int, Dictionary<int, int[]>>>("Workdays", "Config/workday.json");

        /// <summary>
        /// 按公历计算的节日
        /// </summary>
        public SolarHoliday[] SolarHoliday => Get<SolarHoliday[]>("SolarHoliday", "Config/solarHoliday.json");

        /// <summary>
        /// 按农历计算的节日
        /// </summary>
        public LunarHoliday[] LunarHoliday => Get<LunarHoliday[]>("LunarHoliday", "Config/lunarHoliday.json");

        /// <summary>
        /// 按某月第几个星期几
        /// </summary>
        public WeekHoliday[] WeekHoliday => Get<WeekHoliday[]>("WeekHoliday", "Config/weekHoliday.json");

        /// <summary>
        /// 节气
        /// </summary>
        public SolarModel[] SolarTerms => Get<SolarModel[]>("SolarTerms", "Config/solarTerm.json");

        /// <summary>
        /// 获取Json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private T Get<T>(string key, string path)
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
                        return JsonConvert.DeserializeObject<T>(output);
                    }
                }
            });
        }

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