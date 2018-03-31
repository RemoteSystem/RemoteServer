using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class ProtocolServer : AppServer<ProtocolSession, ProtocolRequestInfo>
    {
        public ProtocolServer()
            : base(new DefaultReceiveFilterFactory<ProtocolReceiveFilter, ProtocolRequestInfo>()) //使用默认的接受过滤器工厂 (DefaultReceiveFilterFactory)
        {
        }
    }
}
