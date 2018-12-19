using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 节气类
    /// </summary>
    public class SolarTerm
    {
        private readonly SolarModel[] _solarModels;
        private readonly Resource _resource;

        public SolarTerm(Resource resource)
        {
            _resource = resource;
            _solarModels = resource.SolarTerms;
        }

        #region 准备数据

        /// <summary>
        /// 24节气
        /// </summary>
        private static readonly string[] SolarTerms = {
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分",
            "清明", "谷雨", "立夏", "小满", "芒种", "夏至",
            "小暑", "大暑", "立秋", "处暑", "白露", "秋分",
            "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
        };

        /// <summary>
        /// 节气所在月
        /// </summary>
        /// <remarks>
        /// 与SolarTerm索引对应
        /// </remarks>
        private static readonly int[] SolarMonth = {
            1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12
        };

        private const double D = 0.2422;

        /// <summary>
        /// 正向偏移值(+1)
        /// </summary>
        private readonly Dictionary<int, int[]> INCREASE_OFFSETMAP = new Dictionary<int, int[]>()
        {
            [0] = new int[] { 1982 },
            [1] = new int[] { 2082 },
            [5] = new int[] { 2084 },
            [9] = new int[] { 2008 },
            [10] = new int[] { 1902 },
            [11] = new int[] { 1928 },
            [12] = new int[] { 1925, 2016 },
            [13] = new int[] { 1922 },
            [14] = new int[] { 2002 },
            [16] = new int[] { 1927 },
            [17] = new int[] { 1942 },
            [19] = new int[] { 2089 },
            [20] = new int[] { 2089 },
            [21] = new int[] { 1978 },
            [22] = new int[] { 1954 },
        };

        /// <summary>
        /// 负向偏移值(-1)
        /// </summary>
        private readonly Dictionary<int, int[]> DECREASE_OFFSETMAP = new Dictionary<int, int[]>()
        {
            [0] = new int[] { 2019 },
            [3] = new int[] { 2026 },
            [23] = new int[] { 1918, 2021 },
        };

        /// <summary>
        /// 节气值
        /// </summary>
        /// <remarks>
        /// 定义一个二维数组，第一维数组存储的是20世纪的节气C值，第二维数组存储的是21世纪的节气C值,0到23个，
        /// 与SolarTerm索引对应
        /// </remarks>
        private double[,] CENTURY_ARRAY = {
            { 6.11, 20.84, 4.6295, 19.4599, 6.3826, 21.4155, 5.59, 20.888, 6.318, 21.86, 6.5, 22.2, 7.928, 23.65, 8.35, 23.95, 8.44,
                23.822, 9.098, 24.218, 8.218, 23.08, 7.9, 22.6 },
            { 5.4055, 20.12, 3.87, 18.73, 5.63, 20.646, 4.81, 20.1, 5.52, 21.04, 5.678, 21.37, 7.108, 22.83, 7.5, 23.13, 7.646,
                23.042, 8.318, 23.438, 7.438, 22.36, 7.18, 21.94 } };

        private static int[] sTermInfo = new int[]
        {
            0, 21208, 42467, 63836, 85337, 107014, 128867, 150921,
            173149, 195551, 218072, 240693, 263343, 285989, 308563,
            331033, 353350, 375494, 397447, 419210, 440795, 462224,
            483532, 504758
        };

        #endregion 准备数据

        public Dictionary<DateTime, string> _chineseTwentyFours;

        #region 24节气

        /// <summary>
        /// 获取24节气
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public Dictionary<DateTime, string> GetSolarTerms(int year)
        {
            Dictionary<DateTime, string> dts = new Dictionary<DateTime, string>();
            for (int i = 0; i <= _solarModels.Length - 1; i++)
            {
                var s = GetDate(year, _solarModels[i]);
                dts.Add(s.Key, s.Value);
            }

            return dts;
        }

        /// <summary>
        /// 获取节气日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="solar">节气</param>
        /// <returns>日期,节气名</returns>
        private KeyValuePair<DateTime, string> GetDate(int year, SolarModel solar)
        {
            return new
                KeyValuePair<DateTime, string>(new DateTime(year, solar.Month, GetChineseTwentyFourDay(year, solar)), solar.Name);
        }

        /// <summary>
        /// 获取节气所在日
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="solar">节气</param>
        /// <returns>日期</returns>
        private int GetChineseTwentyFourDay(int year, SolarModel solar)
        {
            int centuryIndex = year <= 2000 ? 0 : 1;
            int y = year % 100;// 步骤1:取年分的后两位数
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
            {   // 闰年
                if (new int[] { 0, 1, 2, 3 }.Contains(_solarModels.IndexOf(solar)))
                {
                    // 注意：凡闰年3月1日前闰年数要减一，
                    // 即：L=[(Y-1)/4],因为小寒、大寒、立春、雨水这两个节气都小于3月1日,
                    // 所以 y = y-1
                    y = y - 1;// 步骤2
                }
            }

            double centuryValue = solar.ThrottleValues[centuryIndex];
            int dateNum = (int)(y * D + centuryValue) - (int)(y / 4);// 步骤3，使用公式[Y*D+C]-L计算
            if (solar.Offsets.ContainsKey(year))
            {
                dateNum += solar.Offsets[year];
            }
            return dateNum;
        }

        /// <summary>
        /// 获取节气
        /// </summary>
        public string GetSolarTerm(DateTime date)
        {
            Dictionary<DateTime, string> solarTerms = GetSolarTerms(date.Year);
            string solarTerm = String.Empty;
            if (solarTerms.Any(c => c.Key.DayOfYear == date.DayOfYear))
            {
                solarTerm = solarTerms.FirstOrDefault(c => c.Key.DayOfYear == date.DayOfYear).Value;
            }
            return solarTerm;
        }

        /// <summary>
        /// 24节气描述
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>描述</returns>
        public string GetSolarTermDays(DateTime date)
        {
            Dictionary<DateTime, string> solarTerms = GetSolarTerms(date.Year);
            var prevTwentyFours = solarTerms.Where(c => c.Key.DayOfYear <= date.DayOfYear);
            var nextTwentyFours = solarTerms.Where(c => c.Key.DayOfYear > date.DayOfYear);

            KeyValuePair<DateTime, string> prevTwentyFour = prevTwentyFours.Any()
                ? prevTwentyFours.OrderByDescending(c => c.Key).First()
                : GetDate(date.Year - 1, _solarModels.Last());
            KeyValuePair<DateTime, string> nextTwentyFour = nextTwentyFours.Any()
                ? nextTwentyFours.OrderBy(c => c.Key).First()
                : GetDate(date.Year + 1, _solarModels.First());

            return
                $"{prevTwentyFour.Value} 第{date.Subtract(Convert.ToDateTime(prevTwentyFour.Key.ToShortDateString())).Days + 1}天(距\"{nextTwentyFour.Value}\"还有{nextTwentyFour.Key.Subtract(date).Days}天)";
        }

        #endregion 24节气
    }
}