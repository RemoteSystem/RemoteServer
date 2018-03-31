using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class ProtocolSession : AppSession<ProtocolSession, ProtocolRequestInfo>
    {
        protected override void HandleException(Exception e)
        {
        }
    }
}
