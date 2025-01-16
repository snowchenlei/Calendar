using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Snow.Calendar.Common;
using Snow.Calendar.Common.Model;

namespace Snow.Calendar.Web;

public class DynamicResource(
    IFileProvider fileProvider,
    IMemoryCache cache) : Resource
{
    /// <summary>
    /// 节假日(休息)
    /// </summary>
    public override Dictionary<int, Dictionary<int, int[]>> Holidays => Get<Dictionary<int, Dictionary<int, int[]>>>("Holidays", "Config/holiday.json") ?? [];

    /// <summary>
    /// 工作日(补班)
    /// </summary>
    public override Dictionary<int, Dictionary<int, int[]>> Workdays => Get<Dictionary<int, Dictionary<int, int[]>>>("Workdays", "Config/workday.json") ?? [];

    /// <summary>
    /// 按公历计算的节日
    /// </summary>
    public override SolarHoliday[] SolarHoliday => Get<SolarHoliday[]>("SolarHoliday", "Config/solarHoliday.json") ?? [];

    /// <summary>
    /// 按农历计算的节日
    /// </summary>
    public override LunarHoliday[] LunarHoliday => Get<LunarHoliday[]>("LunarHoliday", "Config/lunarHoliday.json") ?? [];

    /// <summary>
    /// 按某月第几个星期几
    /// </summary>
    public override WeekHoliday[] WeekHoliday => Get<WeekHoliday[]>("WeekHoliday", "Config/weekHoliday.json") ?? [];

    /// <summary>
    /// 节气
    /// </summary>
    public override SolarModel[] SolarTerms => Get<SolarModel[]>("SolarTerms", "Config/solarTerm.json") ?? [];

    /// <summary>
    /// 获取Json数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private T? Get<T>(string key, string path)
    {
        return cache.GetOrCreate(key, entry =>
        {
            entry.AddExpirationToken(fileProvider.Watch(path));

            IFileInfo file = fileProvider.GetFileInfo(path);
            using var stream = file.CreateReadStream();
            using var reader = new StreamReader(stream);
            string output = reader.ReadToEnd();
            return JsonSerializer.Deserialize<T>(output);
        });
    }
}