using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snow.Calendar.Web.Model
{
    /// <summary>
    /// 星座行星信息
    /// </summary>
    public class ConstellationInfo
    {
        /// <summary>
        /// 阳历星座
        /// </summary>
        public string SolarConstellation { get; set; }

        /// <summary>
        /// 阳历诞生石
        /// </summary>
        public string SolarBirthStone { get; set; }

        /// <summary>
        /// 星宫
        /// </summary>
        public string SolarPalace { get; set; }

        /// <summary>
        /// 行星
        /// </summary>
        public string SolarPlanet { get; set; }
    }
}