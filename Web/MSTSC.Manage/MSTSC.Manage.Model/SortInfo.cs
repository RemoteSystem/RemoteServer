using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class SortInfo
    {
        public string SortName;
        public string SortOrder;
        public bool IsDesc;

        public SortInfo(string name, string order)
        {
            this.SortName = name;
            this.SortOrder = order;
            this.IsDesc = false;
        }
    }
}
