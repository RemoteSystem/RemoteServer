using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class KeyValueModel
    {
        public KeyValueModel(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        /// <summary>
        /// key
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// value
        /// </summary>
        public string value { get; set; }

    }
}
