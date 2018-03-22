using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteServer
{
    class MyRequestInfo : IRequestInfo
    {
        public string Key { get; set; }

        public string JsonInfo { get; set; }

        /*
        // Other properties
        */
    }
}
