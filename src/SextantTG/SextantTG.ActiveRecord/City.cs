﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SextantTG.ActiveRecord
{
    /// <summary>
    /// 城市
    /// </summary>
    public class City : BaseAR
    {
        /// <summary>
        /// 城市ID
        /// </summary>
        public string CityId { get; set; }
        
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        
        /// <summary>
        /// 所属省份ID
        /// </summary>
        public string ProvinceId { get; set; }
    }
}
