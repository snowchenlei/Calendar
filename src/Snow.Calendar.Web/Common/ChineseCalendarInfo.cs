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
    /// �й�������Ϣʵ����
    /// �ο��ĵ�http://www.360doc.com/content/14/0904/19/5827448_407075666.shtml
    /// </summary>
    public sealed class ChineseCalendarInfo
    {
        private DateTime m_SolarDate;
        private int m_LunarYear, m_LunarMonth;
        private string m_LunarYearSexagenary = null, m_LunarYearAnimal = null;
        private string m_LunarYearText = null, m_LunarMonthText = null, m_LunarDayText = null;
        private string m_SolarWeekText = null, m_SolarConstellation = null, m_SolarBirthStone = null;
        private static DateTime GanZhiStartDay = new DateTime(1899, 12, 22); //��ʼ��
        private static DateTime ChineseConstellationReferDay = new DateTime(2007, 9, 11);//28���޲ο�ֵ,����Ϊ��

        private static ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();
        private static GregorianCalendar gc = new GregorianCalendar();

        /// <summary>
        /// ������
        /// </summary>
        private int _year;

        /// <summary>
        /// ������
        /// </summary>
        private int _month;

        /// <summary>
        /// ������
        /// </summary>
        private int _day;

        #region ��������

        private static string nStr2 = "��ʮإئ";
        private const string ChineseNumber = "��һ�����������߰˾�";

        /// <summary>
        /// ��������
        /// </summary>
        private Dictionary<string, string> NaYinFiveElements = new Dictionary<string, string>()
        {
            ["����"] = "���н�",
            ["�ҳ�"] = "���н�",
            ["����"] = "¯�л�",
            ["��î"] = "¯�л�",
            ["�쳽"] = "����ľ",
            ["����"] = "����ľ",
            ["����"] = "·����",
            ["��δ"] = "·����",
            ["����"] = "�����",
            ["����"] = "�����",
            ["����"] = "ɽͷ��",
            ["�Һ�"] = "ɽͷ��",
            ["����"] = "����ˮ",
            ["����"] = "����ˮ",
            ["����"] = "��ͷ��",
            ["��î"] = "��ͷ��",
            ["����"] = "������",
            ["����"] = "������",
            ["����"] = "����ľ",
            ["��δ"] = "����ľ",
            ["����"] = "Ȫ��ˮ",
            ["����"] = "Ȫ��ˮ",
            ["����"] = "������",
            ["����"] = "������",
            ["����"] = "������",
            ["����"] = "������",
            ["����"] = "�ɰ�ľ",
            ["��î"] = "�ɰ�ľ",
            ["�ɳ�"] = "����ˮ",
            ["����"] = "����ˮ",
            ["����"] = "ɳ�н�",
            ["��δ"] = "ɳ�н�",
            ["����"] = "ɽ�»�",
            ["����"] = "ɽ�»�",
            ["����"] = "ƽ��ľ",
            ["����"] = "ƽ��ľ",
            ["����"] = "������",
            ["����"] = "������",
            ["����"] = "�𲭽�",
            ["��î"] = "�𲭽�",
            ["�׳�"] = "��ƻ�",
            ["����"] = "��ƻ�",
            ["����"] = "���ˮ",
            ["��δ"] = "���ˮ",
            ["����"] = "������",
            ["����"] = "������",
            ["����"] = "�廷��",
            ["����"] = "�廷��",
            ["����"] = "ɣ֦ľ",
            ["���"] = "ɣ֦ľ",
            ["����"] = "��Ϫˮ",
            ["��î"] = "��Ϫˮ",
            ["����"] = "ɳ����",
            ["����"] = "ɳ����",
            ["����"] = "���ϻ�",
            ["��δ"] = "���ϻ�",
            ["����"] = "ʯ��ľ",
            ["����"] = "ʯ��ľ",
            ["����"] = "��ˮ",
            ["�ﺥ"] = "��ˮ",
        };

        /// <summary>
        /// ���
        /// </summary>
        private const string CelestialStem = "���ұ����켺�����ɹ�";

        /// <summary>
        /// ��֧
        /// </summary>
        private const string TerrestrialBranch = "�ӳ���î������δ�����纥";

        private const string s = "ˮ��ľľ������������ˮ";

        /// <summary>
        /// ����
        /// </summary>
        private const string Animals = "��ţ������������Ｆ����";

        private static readonly string[] ChineseWeekName = new string[]
            { "������", "����һ", "���ڶ�", "������", "������", "������", "������" };

        private static readonly string[] ChineseDayName = new string[] {
            "��һ","����","����","����","����","����","����","����","����","��ʮ",
            "ʮһ","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","��ʮ",
            "إһ","إ��","إ��","إ��","إ��","إ��","إ��","إ��","إ��","��ʮ"};

        private static readonly string[] ChineseMonthName = new string[]
            { "��", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", "ʮһ", "��" };

        /// <summary>
        /// 24����
        /// </summary>
        private static readonly string[] SolarTerm = {
            "С��", "��", "����", "��ˮ", "����", "����",
            "����", "����", "����", "С��", "â��", "����",
            "С��", "����", "����", "����", "��¶", "���",
            "��¶", "˪��", "����", "Сѩ", "��ѩ", "����"
        };

        /// <summary>
        /// ����������
        /// </summary>
        /// <remarks>
        /// ��SolarTerm������Ӧ
        /// </remarks>
        private static readonly int[] SolarMonth = {
            1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12
        };

        private const double D = 0.2422;

        /// <summary>
        /// ����ƫ��ֵ(+1)
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
        /// ����ƫ��ֵ(-1)
        /// </summary>
        private readonly Dictionary<int, int[]> DECREASE_OFFSETMAP = new Dictionary<int, int[]>()
        {
            [0] = new int[] { 2019 },
            [3] = new int[] { 2026 },
            [23] = new int[] { 1918, 2021 },
        };

        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <remarks>
        /// ����һ����ά���飬��һά����洢����20���͵Ľ���Cֵ���ڶ�ά����洢����21���͵Ľ���Cֵ,0��23����
        /// ��SolarTerm������Ӧ
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

        private static readonly string[] Constellations = new string[]
        {
            "������", "��ţ��", "˫����", "��з��", "ʨ����", "��Ů��",
            "�����", "��Ы��", "������", "Ħ����", "ˮƿ��", "˫����"
        };

        private static readonly string[] Palaces = new string[]
        {
            "ʼ��", "����", "����"
        };

        private static readonly string[] Planets = new string[]
        {
            "����", "����", "ˮ��", "����", "̫��", "ˮ��",
            "����", "����", "ľ��", "����", "������&����", "ľ��&������"
        };

        private static readonly string[] BirthStones = new string[]
        {
            "��ʯ", "����ʯ", "���", "����", "�챦ʯ", "���������",
            "����ʯ", "è��ʯ", "�Ʊ�ʯ", "��������", "��ˮ��", "�³�ʯ��Ѫʯ"
        };

        #endregion ��������

        #region ���캯��

        private Resource _resource;

        public ChineseCalendarInfo(Resource resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// ��ָ�����������ڴ����й�������Ϣʵ����
        /// </summary>
        /// <param name="date">ָ������������</param>
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

        #endregion ���캯��

        #region ˽�з���

        /// <summary>
        /// �Ƚϵ����ǲ���ָ���ĵ��ܼ�
        /// </summary>
        private bool CompareWeekDayHoliday(DateTime date, int month, int week, int day)
        {
            bool ret = false;

            if (date.Month == month) //�·���ͬ
            {
                if (ConvertDayOfWeek(date.DayOfWeek) == day) //���ڼ���ͬ
                {
                    //���µ�һ��
                    DateTime firstDay = date.AddDays(1 - date.Day);
                    //���µ�һ�����ܼ�
                    int weekday = ConvertDayOfWeek(firstDay.DayOfWeek);
                    //���µ�һ���м���
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
        /// �����ڼ�ת�����ֱ�ʾ
        /// </summary>
        private int ConvertDayOfWeek(DayOfWeek dayOfWeek)
        {
            return (int)dayOfWeek + 1;
        }

        /// <summary>
        /// ��ȡ���µĵڼ���
        /// </summary>
        /// <param name="daytime"></param>
        /// <returns></returns>
        private static int getWeekNumInMonth(DateTime daytime)
        {
            int dayInMonth = daytime.Day;
            //���µ�һ��
            DateTime firstDay = daytime.AddDays(1 - daytime.Day);
            //���µ�һ�����ܼ�
            int weekday = (int)firstDay.DayOfWeek == 0 ? 7 : (int)firstDay.DayOfWeek;
            //���µ�һ���м���
            int firstWeekEndDay = 7 - (weekday - 1);
            //��ǰ���ں͵�һ��֮��
            int diffday = dayInMonth - firstWeekEndDay;
            diffday = diffday > 0 ? diffday : 1;
            //��ǰ�ǵڼ���,�������7�ͼ�һ��
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

        #endregion ˽�з���

        #region ��������

        private DateTime _solarDate;

        /// <summary>
        /// ��������
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
        /// ���ڼ�
        /// </summary>
        public DayOfWeek SolarWeek => SolarDate.DayOfWeek;

        /// <summary>
        /// ���ڼ�
        /// </summary>
        public string SolarWeekText => ChineseWeekName[(int)SolarWeek];

        /// <summary>
        /// ��������
        /// </summary>
        public string SolarConstellation { get; private set; }

        /// <summary>
        /// ��������ʯ
        /// </summary>
        public string SolarBirthStone { get; private set; }

        /// <summary>
        /// �ǹ�
        /// </summary>
        public string SolarPalace { get; private set; }

        /// <summary>
        /// ����
        /// </summary>
        public string SolarPlanet { get; private set; }

        /// <summary>
        /// �������
        /// </summary>
        public int LunarYear => calendar.GetYear(SolarDate);

        /// <summary>
        /// ũ���´���С
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
        /// �����·�
        /// </summary>
        public int LunarMonth { get; private set; }

        /// <summary>
        /// ������������
        /// </summary>
        public int LunarDay => calendar.GetDayOfMonth(SolarDate);

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public bool IsLeapYear => DateTime.IsLeapYear(_year);

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public bool IsLeapLunarMonth { get; private set; }

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public bool IsLeapLunarYear => calendar.IsLeapYear(_year);

        /// <summary>
        /// ũ�����еڼ���
        /// </summary>
        public int LunarWeekOfYear => calendar.GetWeekOfYear(SolarDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        /// <summary>
        /// �������еڼ���
        /// </summary>
        public int WeekOfYear => gc.GetWeekOfYear(SolarDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

        /// <summary>
        /// ����ũ�������·�
        /// </summary>
        public int LunarYearLeapMonth => calendar.GetLeapMonth(LunarYear);

        /// <summary>
        /// ���������·�
        /// </summary>
        public int YearLeapMonth => gc.GetLeapMonth(_year);

        /// <summary>
        /// �������֧
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
        /// �����¸�֧
        /// </summary>
        public string LunarMonthSexagenary
        {
            get
            {
                //��֧�ǹ̶���
                int zhiIndex = m_LunarMonth > 10 ? m_LunarMonth - 10 : m_LunarMonth + 2;
                string zhi = TerrestrialBranch[zhiIndex - 1].ToString();

                int ganIndex = 1;
                int y = calendar.GetSexagenaryYear(this.SolarDate) - 1;
                switch (y % 10)
                {
                    case 0: //��
                        ganIndex = 3;
                        break;

                    case 1: //��
                        ganIndex = 5;
                        break;

                    case 2: //��
                        ganIndex = 7;
                        break;

                    case 3: //��
                        ganIndex = 9;
                        break;

                    case 4: //��
                        ganIndex = 1;
                        break;

                    case 5: //��
                        ganIndex = 3;
                        break;

                    case 6: //��
                        ganIndex = 5;
                        break;

                    case 7: //��
                        ganIndex = 7;
                        break;

                    case 8: //��
                        ganIndex = 9;
                        break;

                    case 9: //��
                        ganIndex = 1;
                        break;
                }
                string gan = CelestialStem[(ganIndex + this.m_LunarMonth - 2) % 10].ToString();
                //gan = ���x2 + �� = ��(���^10Ҫ�p10��ֻȡ��λ��);

                return gan + zhi;
            }
        }

        /// <summary>
        /// �����ո�֧
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
        /// Сʱ��֧
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
        /// ����������
        /// </summary>
        public string LunarYearNaYinFiveElements => NaYinFiveElements[LunarYearSexagenary];

        /// <summary>
        /// ����������
        /// </summary>
        public string LunarMonthNaYinFiveElements => NaYinFiveElements[LunarMonthSexagenary];

        /// <summary>
        /// ����������
        /// </summary>
        public string LunarDayNaYinFiveElements => NaYinFiveElements[LunarDaySexagenary];

        /// <summary>
        /// ʱ��������
        /// </summary>
        public string LunarHourNaYinFiveElements => NaYinFiveElements[LunarHourSexagenary];

        /// <summary>
        /// �ܼ�
        /// </summary>
        public DayOfWeek DayOfWeek => SolarDate.DayOfWeek;

        /// <summary>
        /// һ��ĵڼ���
        /// </summary>
        public int DayOfYear => SolarDate.DayOfYear;

        /// <summary>
        /// ʱ��
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

        /// <summary>
        /// ��������Ф
        /// </summary>
        public string LunarYearAnimal
        {
            get
            {
                if (string.IsNullOrEmpty(m_LunarYearAnimal))
                {
                    int y = calendar.GetSexagenaryYear(this.SolarDate);
                    m_LunarYearAnimal = Animals.Substring((y - 1) % 12, 1);
                }
                return m_LunarYearAnimal;
            }
        }

        /// <summary>
        /// �������ռ���Ľ���
        /// </summary>
        public KeyValuePair<string, string> SolarHoliday
        {
            get
            {
                //string tempStr = String.Empty;
                //TODO:Where
                KeyValuePair<string, string> tempStr = _resource.SolarHoliday
                    .Where(s => s.Month == SolarDate.Month && s.Day == SolarDate.Day)
                    .Select(s => new KeyValuePair<string, string>(s.HolidayName, s.HolidayAll))
                    .FirstOrDefault();
                //SolarHoliday[] ss = _resource.SolarHoliday;//.Where(a => a.)
                //foreach (SolarHoliday sh in _resource.SolarHoliday)
                //{
                //    if ((sh.Month == SolarDate.Month) && (sh.Day == SolarDate.Day))
                //    {
                //        tempStr = sh.HolidayName;
                //        break;
                //    }
                //}
                //foreach (SolarHolidayStruct sh in sHolidayInfo)
                //{
                //    if ((sh.Month == SolarDate.Month) && (sh.Day == SolarDate.Day))
                //    {
                //        tempStr = sh.HolidayName;
                //        break;
                //    }
                //}
                return tempStr;
            }
        }

        /// <summary>
        /// �����й�ũ������
        /// </summary>
        public KeyValuePair<string, string> LunarHoliday
        {
            get
            {
                KeyValuePair<string, string> tempStr = new KeyValuePair<string, string>();// = String.Empty;
                if (IsLeapLunarMonth == false) //���²��������
                {
                    if (_resource.LunarHoliday.Any(l => l.Month == LunarMonth && l.Day == LunarDay))
                    {
                        tempStr = _resource.LunarHoliday.Where(l => l.Month == LunarMonth && l.Day == LunarDay)
                            .Select(l => new KeyValuePair<string, string>(l.HolidayName, l.HolidayAll))
                            .FirstOrDefault();
                    }
                    else
                    {
                        //�Գ�Ϧ�����ر���
                        if (this.LunarMonth == 12)
                        {
                            int i = calendar.GetDaysInMonth(LunarYear, LunarMonth);//���㵱��ũ��12�µ�������
                            if (this.LunarDay == i) //���Ϊ���һ��
                            {
                                tempStr = new KeyValuePair<string, string>("��Ϧ", "��Ϧ");
                            }
                        }
                    }
                    //foreach (LunarHoliday lh in _resource.LunarHoliday)
                    //{
                    //    if ((lh.Month == this.LunarMonth) && (lh.Day == this.LunarDay))
                    //    {
                    //        tempStr = lh.HolidayName;
                    //        break;
                    //    }
                    //}
                }
                return tempStr;
            }
        }

        /// <summary>
        /// ��ĳ�µڼ��ܵڼ��ռ���Ľ���
        /// </summary>
        public string WeekDayHoliday
        {
            get
            {
                string tempStr = String.Empty;
                foreach (WeekHoliday wh in _resource.WeekHoliday)
                {
                    if (CompareWeekDayHoliday(SolarDate, wh.Month, wh.WeekAtMonth, wh.WeekDay))
                    {
                        tempStr = wh.HolidayName;
                        break;
                    }
                }
                //foreach (WeekHolidayStruct wh in wHolidayInfo)
                //{
                //    if (CompareWeekDayHoliday(SolarDate, wh.Month, wh.WeekAtMonth, wh.WeekDay))
                //    {
                //        tempStr = wh.HolidayName;
                //        break;
                //    }
                //}
                return tempStr;
            }
        }

        /// <summary>
        /// �����������ʮ�Ľ���,��ʮ�Ľ����ǰ�����ת������ģ����������������
        /// </summary>
        /// <remarks>
        /// �����Ķ��������֡��Ŵ��������õĳ�Ϊ"����"������ʱ���һ��ȷ�Ϊ24�ݣ�
        /// ÿһ����ƽ����15�����࣬�����ֳ�"ƽ��"���ִ�ũ�����õĳ�Ϊ"����"����
        /// �������ڹ���ϵ�λ��Ϊ��׼��һ��360�㣬������֮�����15�㡣���ڶ���ʱ��
        /// ��λ�ڽ��յ㸽�����˶��ٶȽϿ죬���̫���ڻƵ����ƶ�15���ʱ�䲻��15�졣
        /// ����ǰ�����������෴��̫���ڻƵ����ƶ�������һ��������16��֮�ࡣ����
        /// ����ʱ���Ա�֤���������ֱ�Ȼ����ҹƽ�ֵ������졣
        /// </remarks>
        public Dictionary<DateTime, string> ChineseTwentyFours
        {
            get { return GetChineseTwentyFours(SolarDate.Year); }
        }

        #region 24����

        /// <summary>
        /// ��ȡ24����
        /// </summary>
        /// <param name="year">���</param>
        /// <returns></returns>
        public Dictionary<DateTime, string> GetChineseTwentyFours(int year)
        {
            Dictionary<DateTime, string> dts = new Dictionary<DateTime, string>();
            for (int i = 0; i <= 23; i++)
            {
                dts.Append(GetDate(year, i));
            }

            return dts;
        }

        public KeyValuePair<DateTime, string> GetDate(int year, int index)
        {
            return new
                KeyValuePair<DateTime, string>(new DateTime(year, SolarMonth[index], GetDay(year, index)), SolarTerm[index]);
        }

        public int GetDay(int year, int index)
        {
            int centuryIndex = year <= 2000 ? 0 : 1;
            int y = year % 100;// ����1:ȡ��ֵĺ���λ��
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
            {// ����
                if (new int[] { 0, 1, 2, 3 }.Contains(index))
                {
                    // ע�⣺������3��1��ǰ������Ҫ��һ������L=[(Y-1)/4],��ΪС�����󺮡���������ˮ������������С��3��1��,���� y = y-1
                    y = y - 1;// ����2
                }
            }
            double centuryValue = CENTURY_ARRAY[centuryIndex, index];
            int dateNum = (int)(y * D + centuryValue) - (int)(y / 4);// ����3��ʹ�ù�ʽ[Y*D+C]-L����
            dateNum += specialYearOffset(year, index);// ����4�������������ֵĽ���ƫ����
            return dateNum;
        }

        public int specialYearOffset(int year, int index)
        {
            int offset = 0;
            offset += getOffset(DECREASE_OFFSETMAP, year, index, -1);
            offset += getOffset(INCREASE_OFFSETMAP, year, index, 1);

            return offset;
        }

        public static int getOffset(Dictionary<int, int[]> map, int year, int index, int offset)
        {
            int off = 0;
            int[] years = map.ContainsKey(index) ? map[index] : new int[] { };
            if (years.Contains(year))
            {
                off = offset;
            }
            return off;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        public string ChineseTwentyFourDay
        {
            get
            {
                string tempStr = String.Empty;
                if (ChineseTwentyFours.Any(c => c.Key.DayOfYear == SolarDate.DayOfYear))
                {
                    tempStr = ChineseTwentyFours.FirstOrDefault(c => c.Key.DayOfYear == SolarDate.DayOfYear).Value;
                }

                return tempStr;
            }
        }

        /// <summary>
        /// 24��������
        /// </summary>
        public string ChineseTwentyFourDayText
        {
            get
            {
                var prevTwentyFours = ChineseTwentyFours.Where(c => c.Key.DayOfYear <= SolarDate.DayOfYear);
                var nextTwentyFours = ChineseTwentyFours.Where(c => c.Key.DayOfYear > SolarDate.DayOfYear);

                KeyValuePair<DateTime, string> prevTwentyFour = prevTwentyFours.Any()
                    ? prevTwentyFours.OrderByDescending(c => c.Key).First()
                    : GetDate(SolarDate.Year - 1, SolarTerm.Length - 1);// GetChineseTwentyFours(SolarDate.Year - 1).OrderByDescending(c => c.Key).First();
                KeyValuePair<DateTime, string> nextTwentyFour = nextTwentyFours.Any()
                    ? nextTwentyFours.OrderBy(c => c.Key).First()
                    : GetDate(SolarDate.Year + 1, 0);//GetChineseTwentyFours(SolarDate.Year + 1).OrderBy(c => c.Key).First();

                return
                    $"{prevTwentyFour.Value} ��{SolarDate.Subtract(Convert.ToDateTime(prevTwentyFour.Key.ToShortDateString())).Days + 1}��(��\"{nextTwentyFour.Value}\"����{nextTwentyFour.Key.Subtract(SolarDate).Days}��)";
            }
        }

        #endregion 24����

        /// <summary>
        /// �������ı�
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
        /// �������ı�
        /// </summary>
        public string LunarMonthText => (this.IsLeapLunarMonth ? "��" : "") + ChineseMonthName[LunarMonth - 1];

        /// <summary>
        /// �������������ı�
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
        /// �����ı�
        /// </summary>
        public string LunarText => LunarYearText + "��" + LunarMonthText + "��" + LunarDayText;

        #endregion ��������

        /// <summary>
        /// ����ָ���������ڼ�������������ʯ
        /// </summary>
        /// <param name="date">ָ����������</param>
        /// <param name="constellation">����</param>
        /// <param name="birthstone">����ʯ</param>
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
            SolarConstellation = Constellations[j];
            SolarBirthStone = BirthStones[j];
            SolarPalace = Palaces[j % 3];
            SolarPlanet = Planets[j];

            #region ��������

            //��������   3��21��------4��19��     ����ʯ��   ��ʯ
            //��ţ����   4��20��------5��20��   ����ʯ��   ����ʯ
            //˫������   5��21��------6��21��     ����ʯ��   ���
            //��з����   6��22��------7��22��   ����ʯ��   ����
            //ʨ������   7��23��------8��22��   ����ʯ��   �챦ʯ
            //��Ů����   8��23��------9��22��   ����ʯ��   ���������
            //�������   9��23��------10��23��     ����ʯ��   ����ʯ
            //��Ы����   10��24��-----11��21��     ����ʯ��   è��ʯ
            //��������   11��22��-----12��21��   ����ʯ��   �Ʊ�ʯ
            //Ħ������   12��22��-----1��19��   ����ʯ��   ��������
            //ˮƿ����   1��20��-----2��18��   ����ʯ��   ��ˮ��
            //˫������   2��19��------3��20��   ����ʯ��   �³�ʯ��Ѫʯ

            #endregion ��������
        }

        #region ����ת����

        /// <summary>
        /// ��ȡָ����ݴ��ڵ��գ����³�һ������������
        /// </summary>
        /// <param name="year">ָ�������</param>
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
        /// ����ת����
        /// </summary>
        /// <param name="year">������</param>
        /// <param name="month">������</param>
        /// <param name="day">������</param>
        /// <param name="IsLeapMonth">�Ƿ�����</param>
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
        /// ����ת����
        /// </summary>
        /// <param name="date">��������</param>
        /// <param name="IsLeapMonth">�Ƿ�����</param>
        public static DateTime GetDateFromLunarDate(DateTime date, bool IsLeapMonth)
        {
            return GetDateFromLunarDate(date.Year, date.Month, date.Day, IsLeapMonth);
        }

        #endregion ����ת����

        #region ��������������

        /// <summary>
        /// ��������������ʵ��
        /// </summary>
        /// <param name="year">������</param>
        /// <param name="month">������</param>
        /// <param name="day">������</param>
        /// <param name="IsLeapMonth">�Ƿ�����</param>
        public static ChineseCalendarInfo FromLunarDate(int year, int month, int day, bool IsLeapMonth)
        {
            DateTime dt = GetDateFromLunarDate(year, month, day, IsLeapMonth);
            return new ChineseCalendarInfo(dt, null);
        }

        /// <summary>
        /// ��������������ʵ��
        /// </summary>
        /// <param name="date">��������</param>
        /// <param name="IsLeapMonth">�Ƿ�����</param>
        public static ChineseCalendarInfo FromLunarDate(DateTime date, bool IsLeapMonth)
        {
            return FromLunarDate(date.Year, date.Month, date.Day, IsLeapMonth);
        }

        /// <summary>
        /// ��������������ʵ��
        /// </summary>
        /// <param name="date">��ʾ�������ڵ�8λ���֣����磺20070209</param>
        /// <param name="IsLeapMonth">�Ƿ�����</param>
        public static ChineseCalendarInfo FromLunarDate(string date, bool IsLeapMonth)
        {
            Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{7}(\d)$");
            Match mc = rg.Match(date);
            if (!mc.Success)
            {
                throw new Exception("�����ַ�����������");
            }
            DateTime dt = DateTime.Parse(string.Format("{0}-{1}-{2}", date.Substring(0, 4), date.Substring(4, 2), date.Substring(6, 2)));
            return FromLunarDate(dt, IsLeapMonth);
        }

        #endregion ��������������

        /// <summary>
        /// ��0-9ת�ɺ�����ʽ
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