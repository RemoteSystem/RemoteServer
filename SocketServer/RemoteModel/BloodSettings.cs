using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteModel
{
    public class BloodSettings
    {
        /// <summary>
        /// z320190120
        /// </summary>
        public string SN { get; set; }
        /// <summary>
        /// 15928882400
        /// </summary>
        public string sim { get; set; }
        /// <summary>
        ///  BI 
        /// </summary>
        public string inf_query { get; set; }
        /// <summary>
        ///  Z3 
        /// </summary>
        public string model_setup { get; set; }
        /// <summary>
        /// OPEN
        /// </summary>
        public string reagent_setup { get; set; }
        /// <summary>
        /// Oem_change
        /// </summary>
        public int oem_change { get; set; }
        /// <summary>
        /// Agent_change
        /// </summary>
        public int agent_change { get; set; }
        /// <summary>
        /// chinese
        /// </summary>
        public string lang_change { get; set; }
        /// <summary>
        /// Z3201801001
        /// </summary>
        public string sn_setup { get; set; }
        /// <summary>
        /// close
        /// </summary>
        public string remote_shut { get; set; }
        /// <summary>
        /// Dump
        /// </summary>
        public Dump dump { get; set; }
    }

}
