using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteServer
{
    class MyAppServer : AppServer<MySession, MyRequestInfo>
    {
        public MyAppServer()
            : base(new DefaultReceiveFilterFactory<MyReceiveFilter, MyRequestInfo>())
        {

        }

        //protected override void OnNewSessionConnected(MySession session)
        //{
        //    Console.WriteLine("连上了哦");
        //}

        //protected override void OnSessionClosed(MySession session, CloseReason reason)
        //{
        //    Console.WriteLine("关闭了哦");
        //}

    }
}
