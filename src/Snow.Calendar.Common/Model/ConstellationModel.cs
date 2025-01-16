namespace Snow.Calendar.Common.Model
{
    /// <summary>
    /// 星宿实体
    /// </summary>
    public class ConstellationModel
    {
        /// <summary>
        /// 星宿名称
        /// </summary>
        public string ConstellationName { get; set; }

        /// <summary>
        /// 星宿描述
        /// </summary>
        public string ConstellationValue { get; set; }

        /// <summary>
        /// 转换字符串哦
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ConstellationName}({ConstellationValue})";
        }
    }
}