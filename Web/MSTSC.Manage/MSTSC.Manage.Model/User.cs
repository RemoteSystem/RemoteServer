using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class User
    {
        public string id { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public string sex { get; set; }

        public string age { get; set; }

        public string isAdmin { get; set; }

        public string isDel { get; set; }
    }
}
