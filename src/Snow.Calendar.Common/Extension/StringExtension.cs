namespace Snow.Calendar.Common.Extension
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 字符串强转数组
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="str">调用源</param>
        /// <param name="converter">转换逻辑</param>
        /// <returns>目标类型数组</returns>
        public static T[] ConvertTo<T>(this string str, Converter<string, T> converter)
        {
            try
            {
                string[] strMonths = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                return Array.ConvertAll(strMonths, converter);
            }
            catch (FormatException e)
            {
                throw new UserFriendlyException("字符串转换数组失败");
            }
        }
    }
}