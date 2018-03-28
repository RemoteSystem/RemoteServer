using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteServer
{
    class MySession : AppSession<MySession, MyRequestInfo>
    {
        public bool Updated = false;
    }
}
