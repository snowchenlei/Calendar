using Snow.Calendar.Common.Model;

namespace Snow.Calendar.Common
{
    /// <summary>
    /// 节气类
    /// </summary>
    public class SolarTerm(Resource resource)
    {
        private const double D = 0.2422;
        private readonly SolarModel[] _solarModels = resource.SolarTerms;

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
                if (new int[] { 0, 1, 2, 3 }.Contains(Array.IndexOf(_solarModels, solar)))
                {
                    // 注意：凡闰年3月1日前闰年数要减一，
                    // 即：L=[(Y-1)/4],因为小寒、大寒、立春、雨水这两个节气都小于3月1日,
                    // 所以 y = y-1
                    y = y - 1;// 步骤2
                }
            }

            double centuryValue = solar.ThrottleValues[centuryIndex];
            int dateNum = (int)(y * D + centuryValue) - (int)(y / 4);// 步骤3，使用公式[Y*D+C]-L计算
            if (solar.Offsets.TryGetValue(year, out int offset))
            {
                dateNum += offset;
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
            if (_solarModels.Length == 0)
            {
                return String.Empty;
            }
            Dictionary<DateTime, string> solarTerms = GetSolarTerms(date.Year);
            KeyValuePair<DateTime, string>[] prevTwentyFours = solarTerms.Where(c => c.Key.DayOfYear <= date.DayOfYear).ToArray();
            KeyValuePair<DateTime, string>[] nextTwentyFours = solarTerms.Where(c => c.Key.DayOfYear > date.DayOfYear).ToArray();

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