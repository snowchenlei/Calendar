namespace Snow.Calendar.Common.Model
{
    /// <summary>
    /// 节气模型
    /// </summary>
    public class SolarModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所在月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 节气值数组
        /// </summary>
        /// <remarks>
        /// 20世纪、21世纪...
        /// </remarks>
        public double[] ThrottleValues { get; set; }

        /// <summary>
        /// 偏移量
        /// </summary>
        public Dictionary<int, int> Offsets { get; set; }
    }
}