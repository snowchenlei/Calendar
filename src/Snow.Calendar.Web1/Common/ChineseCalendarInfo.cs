using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 中国日历信息实体类
    /// 参考文档http://www.360doc.com/content/14/0904/19/5827448_407075666.shtml
    /// </summary>
    public sealed class ChineseCalendarInfo
    {
        private DateTime m_SolarDate;
        private int m_LunarYear, m_LunarMonth;
        private string m_LunarYearSexagenary = null, m_LunarYearAnimal = null;
        private string m_LunarYearText = null, m_LunarMonthText = null, m_LunarDayText = null;
        private string m_SolarWeekText = null, m_SolarConstellation = null, m_SolarBirthStone = null;
        private static DateTime GanZhiStartDay = new DateTime(1899, 12, 22); //起始日
        private static DateTime ChineseConstellationReferDay = new DateTime(2007, 9, 11);//28星宿参考值,本日为角

        private static ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();
        private static GregorianCalendar gc = new GregorianCalendar();

        /// <summary>
        /// 公历年
        /// </summary>
        private int _year;

        /// <summary>
        /// 公历月
        /// </summary>
        private int _month;

        /// <summary>
        /// 公历日
        /// </summary>
        private int _day;

        #region 基础数据

        private static string nStr2 = "初十廿卅";
        private const string ChineseNumber = "零一二三四五六七八九";

        /// <summary>
        /// 纳音五行
        /// </summary>
        private Dictionary<string, string> NaYinFiveElements = new Dictionary<string, string>()
        {
            ["甲子"] = "海中金",
            ["乙丑"] = "海中金",
            ["丙寅"] = "炉中火",
            ["丁卯"] = "炉中火",
            ["戊辰"] = "大林木",
            ["己巳"] = "大林木",
            ["庚午"] = "路旁土",
            ["辛未"] = "路旁土",
            ["壬申"] = "剑锋金",
            ["癸酉"] = "剑锋金",
            ["甲戌"] = "山头火",
            ["乙亥"] = "山头火",
            ["丙子"] = "涧下水",
            ["丁丑"] = "涧下水",
            ["戊寅"] = "城头土",
            ["己卯"] = "城头土",
            ["庚辰"] = "白腊金",
            ["辛巳"] = "白腊金",
            ["壬午"] = "杨柳木",
            ["癸未"] = "杨柳木",
            ["甲申"] = "泉中水",
            ["乙酉"] = "泉中水",
            ["丙戌"] = "屋上土",
            ["丁亥"] = "屋上土",
            ["戊子"] = "劈雳火",
            ["己丑"] = "劈雳火",
            ["庚寅"] = "松柏木",
            ["辛卯"] = "松柏木",
            ["壬辰"] = "长流水",
            ["癸巳"] = "长流水",
            ["甲午"] = "沙中金",
            ["乙未"] = "沙中金",
            ["丙申"] = "山下火",
            ["丁酉"] = "山下火",
            ["戊戌"] = "平地木",
            ["己亥"] = "平地木",
            ["庚子"] = "壁上土",
            ["辛丑"] = "壁上土",
            ["壬寅"] = "金箔金",
            ["癸卯"] = "金箔金",
            ["甲辰"] = "佛灯火",
            ["乙巳"] = "佛灯火",
            ["丙午"] = "天河水",
            ["丁未"] = "天河水",
            ["戊申"] = "大驿土",
            ["己酉"] = "大驿土",
            ["庚戌"] = "插环金",
            ["辛亥"] = "插环金",
            ["壬子"] = "桑枝木",
            ["癸丑"] = "桑枝木",
            ["甲寅"] = "大溪水",
            ["乙卯"] = "大溪水",
            ["丙辰"] = "沙中土",
            ["丁巳"] = "沙中土",
            ["戊午"] = "天上火",
            ["己未"] = "天上火",
            ["庚申"] = "石榴木",
            ["辛酉"] = "石榴木",
            ["壬戌"] = "大海水",
            ["癸亥"] = "大海水",
        };

        /// <summary>
        /// 天干
        /// </summary>
        private const string CelestialStem = "甲乙丙丁戊己庚辛壬癸";

        /// <summary>
        /// 地支
        /// </summary>
        private const string TerrestrialBranch = "子丑寅卯辰巳午未申酉戌亥";

        private static readonly string[] ChineseWeekName = new string[]
            { "星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        private static readonly string[] ChineseDayName = new string[] {
            "初一","初二","初三","初四","初五","初六","初七","初八","初九","初十",
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十",
            "廿一","廿二","廿三","廿四","廿五","廿六","廿七","廿八","廿九","三十"};

        private static readonly string[] ChineseMonthName = new string[]
            { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "腊" };

        #endregion 基础数据

        #region 构造函数

        private Resource _resource;

        public ChineseCalendarInfo(Resource resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// 从指定的阳历日期创建中国日历信息实体类
        /// </summary>
        /// <param name="date">指定的阳历日期</param>
        public ChineseCalendarInfo(DateTime date,
            Resource resource) : this(resource)
        {
            Init(date);
        }

        private void LoadFromSolarDate()
        {
            m_LunarYearSexagenary = null;
            m_LunarYearAnimal = null;
            m_LunarYearText = null;
            m_LunarMonthText = null;
            m_LunarDayText = null;
            m_SolarWeekText = null;
            m_SolarConstellation = null;
            m_SolarBirthStone = null;

            m_LunarYear = calendar.GetYear(m_SolarDate);
            m_LunarMonth = LunarMonth = calendar.GetMonth(m_SolarDate);
            int leapMonth = calendar.GetLeapMonth(LunarYear);

            if (leapMonth == m_LunarMonth)
            {
                IsLeapLunarMonth = true;
                m_LunarMonth = LunarMonth = m_LunarMonth - 1;
            }
            else if (leapMonth > 0 && leapMonth < m_LunarMonth)
            {
                m_LunarMonth = LunarMonth = m_LunarMonth - 1;
            }
            CalcConstellation(m_SolarDate);
        }

        #endregion 构造函数

        #region 私有方法

        /// <summary>
        /// 比较当天是不是指定的第周几
        /// </summary>
        private bool CompareWeekDayHoliday(DateTime date, int month, int week, int day)
        {
            bool ret = false;

            if (date.Month == month) //月份相同
            {
                if (ConvertDayOfWeek(date.DayOfWeek) == day) //星期几相同
                {
                    //本月第一天
                    DateTime firstDay = date.AddDays(1 - date.Day);
                    //本月第一天是周几
                    int weekday = ConvertDayOfWeek(firstDay.DayOfWeek);
                    //本月第一周有几天
                    int firstWeekEndDay = 7 - ConvertDayOfWeek(firstDay.DayOfWeek) + 1;

                    if (weekday > day)
                    {
                        if ((week - 1) * 7 + day + firstWeekEndDay == date.Day)
                        {
                            ret = true;
                        }
                    }
                    else
                    {
                        if (day + firstWeekEndDay + (week - 2) * 7 == date.Day)
                        {
                            ret = true;
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 将星期几转成数字表示
        /// </summary>
        private int ConvertDayOfWeek(DayOfWeek dayOfWeek)
        {
            return (int)dayOfWeek + 1;
        }

        /// <summary>
        /// 获取本月的第几周
        /// </summary>
        /// <param name="daytime"></param>
        /// <returns></returns>
        private static int getWeekNumInMonth(DateTime daytime)
        {
            int dayInMonth = daytime.Day;
            //本月第一天
            DateTime firstDay = daytime.AddDays(1 - daytime.Day);
            //本月第一天是周几
            int weekday = (int)firstDay.DayOfWeek == 0 ? 7 : (int)firstDay.DayOfWeek;
            //本月第一周有几天
            int firstWeekEndDay = 7 - (weekday - 1);
            //当前日期和第一周之差
            int diffday = dayInMonth - firstWeekEndDay;
            diffday = diffday > 0 ? diffday : 1;
            //当前是第几周,如果整除7就减一天
            int WeekNumInMonth = ((diffday % 7) == 0
                                     ? (diffday / 7 - 1)
                                     : (diffday / 7)) + 1 + (dayInMonth > firstWeekEndDay ? 1 : 0);
            return WeekNumInMonth;
        }

        private void Init(DateTime date)
        {
            _year = date.Year;
            _month = date.Month;
            _day = date.Day;

            _solarDate = date;
            m_SolarDate = date;
            LoadFromSolarDate();
        }

        #endregion 私有方法

        #region 日历属性

        private DateTime _solarDate;

        /// <summary>
        /// 阳历日期
        /// </summary>
        public DateTime SolarDate
        {
            get => _solarDate;
            set
            {
                if (_solarDate.Equals(value))
                {
                    return;
                }

                Init(value);
            }
        }

        /// <summary>
        /// 星期几
        /// </summary>
        public DayOfWeek SolarWeek => SolarDate.DayOfWeek;

        /// <summary>
        /// 星期几
        /// </summary>
        public string SolarWeekText => ChineseWeekName[(int)SolarWeek];

        /// <summary>
        /// 阴历年份
        /// </summary>
        public int LunarYear => calendar.GetYear(SolarDate);

        /// <summary>
        /// 农历月大月小
        /// </summary>
        public bool IsBiglunarMonth
        {
            get
            {
                int i = calendar.GetDaysInMonth(LunarYear, LunarMonth);
                return i == 30;
            }
        }

        /// <summary>
        /// 阴历月份
        /// </summary>
        public int LunarMonth { get; private set; }

        /// <summary>
        /// 阴历月中日期
        /// </summary>
        public int LunarDay => calendar.GetDayOfMonth(SolarDate);

        /// <summary>
        /// 是否阳历闰年
        /// </summary>
        public bool IsLeapYear => DateTime.IsLeapYear(_year);

        /// <summary>
        /// 是否阴历闰月
        /// </summary>
        public bool IsLeapLunarMonth { get; private set; }

        /// <summary>
        /// 是否阴历闰年
        /// </summary>
        public bool IsLeapLunarYear => calendar.IsLeapYear(_year);

        /// <summary>
        /// 农历年中第几周
        /// </summary>
        public int LunarWeekOfYear => calendar.GetWeekOfYear(SolarDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        /// <summary>
        /// 公历年中第几周
        /// </summary>
        public int WeekOfYear => gc.GetWeekOfYear(SolarDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

        /// <summary>
        /// 当年农历闰月月份
        /// </summary>
        public int LunarYearLeapMonth => calendar.GetLeapMonth(LunarYear);

        /// <summary>
        /// 当年闰月月份
        /// </summary>
        public int YearLeapMonth => gc.GetLeapMonth(_year);

        /// <summary>
        /// 阴历年干支
        /// </summary>
        public string LunarYearSexagenary
        {
            get
            {
                if (string.IsNullOrEmpty(m_LunarYearSexagenary))
                {
                    int y = calendar.GetSexagenaryYear(this.SolarDate) - 1;
                    m_LunarYearSexagenary = CelestialStem.Substring(y % 10, 1) + TerrestrialBranch.Substring(y % 12, 1);
                }
                return m_LunarYearSexagenary;
            }
        }

        /// <summary>
        /// 阴历月干支
        /// </summary>
        public string LunarMonthSexagenary
        {
            get
            {
                //月支是固定的
                int zhiIndex = m_LunarMonth > 10 ? m_LunarMonth - 10 : m_LunarMonth + 2;
                string zhi = TerrestrialBranch[zhiIndex - 1].ToString();

                int ganIndex = 1;
                int y = calendar.GetSexagenaryYear(this.SolarDate) - 1;
                switch (y % 10)
                {
                    case 0: //甲
                        ganIndex = 3;
                        break;

                    case 1: //乙
                        ganIndex = 5;
                        break;

                    case 2: //丙
                        ganIndex = 7;
                        break;

                    case 3: //丁
                        ganIndex = 9;
                        break;

                    case 4: //戊
                        ganIndex = 1;
                        break;

                    case 5: //己
                        ganIndex = 3;
                        break;

                    case 6: //庚
                        ganIndex = 5;
                        break;

                    case 7: //辛
                        ganIndex = 7;
                        break;

                    case 8: //壬
                        ganIndex = 9;
                        break;

                    case 9: //癸
                        ganIndex = 1;
                        break;
                }
                string gan = CelestialStem[(ganIndex + this.m_LunarMonth - 2) % 10].ToString();
                //gan = 年幹x2 + 月數 = 月幹(超過10要減10，只取個位數);

                return gan + zhi;
            }
        }

        /// <summary>
        /// 阴历日干支
        /// </summary>
        public string LunarDaySexagenary
        {
            get
            {
                int i, offset;
                TimeSpan ts = this.SolarDate - GanZhiStartDay;
                offset = ts.Days;
                i = offset % 60;
                return CelestialStem[i % 10] + TerrestrialBranch[i % 12].ToString();
            }
        }

        /// <summary>
        /// 小时干支
        /// </summary>
        public string LunarHourSexagenary
        {
            get
            {
                int i, offset;
                TimeSpan ts = this.SolarDate - GanZhiStartDay;
                offset = ts.Days;
                i = offset % 60;
                return CelestialStem[(2 * (i % 10) + LunarHourBranch) % 10].ToString() + LunarHourText;
            }
        }

        /// <summary>
        /// 年纳音五行
        /// </summary>
        public string LunarYearNaYinFiveElements => NaYinFiveElements[LunarYearSexagenary];

        /// <summary>
        /// 月纳音五行
        /// </summary>
        public string LunarMonthNaYinFiveElements => NaYinFiveElements[LunarMonthSexagenary];

        /// <summary>
        /// 日纳音五行
        /// </summary>
        public string LunarDayNaYinFiveElements => NaYinFiveElements[LunarDaySexagenary];

        /// <summary>
        /// 时纳音五行
        /// </summary>
        public string LunarHourNaYinFiveElements => NaYinFiveElements[LunarHourSexagenary];

        /// <summary>
        /// 周几
        /// </summary>
        public DayOfWeek DayOfWeek => SolarDate.DayOfWeek;

        /// <summary>
        /// 一年的第几天
        /// </summary>
        public int DayOfYear => SolarDate.DayOfYear;

        /// <summary>
        /// 时辰
        /// </summary>
        public int LunarHourBranch
        {
            get
            {
                int hour = Convert.ToInt32(SolarDate.ToString("HH"));
                int i = (hour + 1) / 2;
                i = i == 12 ? 0 : i;
                return i;
            }
        }

        public string LunarHourText => TerrestrialBranch[LunarHourBranch].ToString();

        #endregion 日历属性

        /// <summary>
        /// 根据指定阳历日期计算星座＆诞生石
        /// </summary>
        /// <param name="date">指定阳历日期</param>
        /// <param name="constellation">星座</param>
        /// <param name="birthstone">诞生石</param>
        public void CalcConstellation(DateTime date)
        {
            int i = Convert.ToInt32(date.ToString("MMdd"));
            int j;
            if (i >= 321 && i <= 419)
                j = 0;
            else if (i >= 420 && i <= 520)
                j = 1;
            else if (i >= 521 && i <= 621)
                j = 2;
            else if (i >= 622 && i <= 722)
                j = 3;
            else if (i >= 723 && i <= 822)
                j = 4;
            else if (i >= 823 && i <= 922)
                j = 5;
            else if (i >= 923 && i <= 1023)
                j = 6;
            else if (i >= 1024 && i <= 1121)
                j = 7;
            else if (i >= 1122 && i <= 1221)
                j = 8;
            else if (i >= 1222 || i <= 119)
                j = 9;
            else if (i >= 120 && i <= 218)
                j = 10;
            else if (i >= 219 && i <= 320)
                j = 11;
            else
            {
                return;
            }
            //SolarConstellation = Constellations[j];
            //SolarBirthStone = BirthStones[j];
            //SolarPalace = Palaces[j % 3];
            //SolarPlanet = Planets[j];

            #region 星座划分

            //白羊座：   3月21日------4月19日     诞生石：   钻石
            //金牛座：   4月20日------5月20日   诞生石：   蓝宝石
            //双子座：   5月21日------6月21日     诞生石：   玛瑙
            //巨蟹座：   6月22日------7月22日   诞生石：   珍珠
            //狮子座：   7月23日------8月22日   诞生石：   红宝石
            //处女座：   8月23日------9月22日   诞生石：   红条纹玛瑙
            //天秤座：   9月23日------10月23日     诞生石：   蓝宝石
            //天蝎座：   10月24日-----11月21日     诞生石：   猫眼石
            //射手座：   11月22日-----12月21日   诞生石：   黄宝石
            //摩羯座：   12月22日-----1月19日   诞生石：   土耳其玉
            //水瓶座：   1月20日-----2月18日   诞生石：   紫水晶
            //双鱼座：   2月19日------3月20日   诞生石：   月长石，血石

            #endregion 星座划分
        }

        #region 阴历信息

        /// <summary>
        /// 阴历年文本
        /// </summary>
        public string LunarYearText
        {
            get
            {
                string tempStr = String.Empty;
                string num = this.LunarYear.ToString();
                for (int i = 0; i < 4; i++)
                {
                    tempStr += ConvertNumToChineseNum(num[i]);
                }
                return tempStr;
            }
        }

        /// <summary>
        /// 阴历月文本
        /// </summary>
        public string LunarMonthText => (this.IsLeapLunarMonth ? "闰" : "") + ChineseMonthName[LunarMonth - 1];

        /// <summary>
        /// 阴历月中日期文本
        /// </summary>
        public string LunarDayText
        {
            get
            {
                if (string.IsNullOrEmpty(m_LunarDayText))
                    m_LunarDayText = ChineseDayName[this.LunarDay - 1];
                return m_LunarDayText;
            }
        }

        /// <summary>
        /// 阴历文本
        /// </summary>
        public string LunarText => LunarYearText + "年" + LunarMonthText + "月" + LunarDayText;

        #endregion 阴历信息

        #region 阴历转阳历

        /// <summary>
        /// 获取指定年份春节当日（正月初一）的阳历日期
        /// </summary>
        /// <param name="year">指定的年份</param>
        private static DateTime GetLunarNewYearDate(int year)
        {
            DateTime dt = new DateTime(year, 1, 1);
            int cnYear = calendar.GetYear(dt);
            int cnMonth = calendar.GetMonth(dt);

            int num1 = 0;
            int num2 = calendar.IsLeapYear(cnYear) ? 13 : 12;

            while (num2 >= cnMonth)
            {
                num1 += calendar.GetDaysInMonth(cnYear, num2--);
            }

            num1 = num1 - calendar.GetDayOfMonth(dt) + 1;
            return dt.AddDays(num1);
        }

        /// <summary>
        /// 阴历转阳历
        /// </summary>
        /// <param name="year">阴历年</param>
        /// <param name="month">阴历月</param>
        /// <param name="day">阴历日</param>
        /// <param name="IsLeapMonth">是否闰月</param>
        public static DateTime GetDateFromLunarDate(int year, int month, int day, bool IsLeapMonth)
        {
            int num1 = 0, num2 = 0;
            int leapMonth = calendar.GetLeapMonth(year);

            if (((leapMonth == month + 1) && IsLeapMonth) || (leapMonth > 0 && leapMonth <= month))
                num2 = month;
            else
                num2 = month - 1;

            while (num2 > 0)
            {
                num1 += calendar.GetDaysInMonth(year, num2--);
            }

            DateTime dt = GetLunarNewYearDate(year);
            return dt.AddDays(num1 + day - 1);
        }

        /// <summary>
        /// 阴历转阳历
        /// </summary>
        /// <param name="date">阴历日期</param>
        /// <param name="IsLeapMonth">是否闰月</param>
        public static DateTime GetDateFromLunarDate(DateTime date, bool IsLeapMonth)
        {
            return GetDateFromLunarDate(date.Year, date.Month, date.Day, IsLeapMonth);
        }

        #endregion 阴历转阳历

        #region 从阴历创建日历

        /// <summary>
        /// 从阴历创建日历实体
        /// </summary>
        /// <param name="year">阴历年</param>
        /// <param name="month">阴历月</param>
        /// <param name="day">阴历日</param>
        /// <param name="IsLeapMonth">是否闰月</param>
        public static ChineseCalendarInfo FromLunarDate(int year, int month, int day, bool IsLeapMonth)
        {
            DateTime dt = GetDateFromLunarDate(year, month, day, IsLeapMonth);
            return new ChineseCalendarInfo(dt, null);
        }

        /// <summary>
        /// 从阴历创建日历实体
        /// </summary>
        /// <param name="date">阴历日期</param>
        /// <param name="IsLeapMonth">是否闰月</param>
        public static ChineseCalendarInfo FromLunarDate(DateTime date, bool IsLeapMonth)
        {
            return FromLunarDate(date.Year, date.Month, date.Day, IsLeapMonth);
        }

        /// <summary>
        /// 从阴历创建日历实体
        /// </summary>
        /// <param name="date">表示阴历日期的8位数字，例如：20070209</param>
        /// <param name="IsLeapMonth">是否闰月</param>
        public static ChineseCalendarInfo FromLunarDate(string date, bool IsLeapMonth)
        {
            Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{7}(\d)$");
            Match mc = rg.Match(date);
            if (!mc.Success)
            {
                throw new Exception("日期字符串输入有误！");
            }
            DateTime dt = DateTime.Parse(string.Format("{0}-{1}-{2}", date.Substring(0, 4), date.Substring(4, 2), date.Substring(6, 2)));
            return FromLunarDate(dt, IsLeapMonth);
        }

        #endregion 从阴历创建日历

        /// <summary>
        /// 将0-9转成汉字形式
        /// </summary>
        private string ConvertNumToChineseNum(char n)
        {
            if ((n < '0') || (n > '9')) return "";
            switch (n)
            {
                case '0':
                    return ChineseNumber[0].ToString();

                case '1':
                    return ChineseNumber[1].ToString();

                case '2':
                    return ChineseNumber[2].ToString();

                case '3':
                    return ChineseNumber[3].ToString();

                case '4':
                    return ChineseNumber[4].ToString();

                case '5':
                    return ChineseNumber[5].ToString();

                case '6':
                    return ChineseNumber[6].ToString();

                case '7':
                    return ChineseNumber[7].ToString();

                case '8':
                    return ChineseNumber[8].ToString();

                case '9':
                    return ChineseNumber[9].ToString();

                default:
                    return "";
            }
        }
    }
}