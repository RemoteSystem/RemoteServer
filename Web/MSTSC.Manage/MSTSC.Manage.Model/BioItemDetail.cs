using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class BioItemDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public int? blank_time_begin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? blank_time_end { get; set; }
        /// <summary>
        /// wer李
        /// </summary>
        public string calibration_method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string corrected_intercept { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string corrected_slope { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string first_reagent_volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string k_factor_value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? main_wavelength { get; set; }
        /// <summary>
        /// 未知
        /// </summary>
        public string measuring_method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reaction_direction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? reaction_time_begin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? reaction_time_end { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sample_volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string second_reagent_volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? sub_wavelength { get; set; }
    }
}
