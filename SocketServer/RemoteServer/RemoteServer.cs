using RemoteController;
using RemoteModel;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;

namespace RemoteServer
{
    public class SocketServer
    {
        //***********************************************************************//
        //****** 说明，引用这个程序集所在的程序集里面需要引用supersocket，不然报错.
        //***********************************************************************//
        MyAppServer appServer = null;
        static Dictionary<string, MySession> dic = new Dictionary<string, MySession>();
        public bool start()
        {
            //Console.ReadKey();
            var config = new SuperSocket.SocketBase.Config.ServerConfig()
            {
                Name = "SSServer",
                ServerTypeName = "SServer",
                ClearIdleSession = true, //60秒执行一次清理90秒没数据传送的连接
                ClearIdleSessionInterval = 60,
                IdleSessionTimeOut = 90,
                MaxRequestLength = 20480, //最大包长度
                ReceiveBufferSize = 10000,
                SendBufferSize = 10000,
                Ip = "Any",
                Port = 12315,
                MaxConnectionNumber = 10,
                TextEncoding = "GBK"
            };

            appServer = new MyAppServer();
            appServer.NewSessionConnected += appServer_NewSessionConnected;
            appServer.NewRequestReceived += appServer_NewRequestReceived;
            appServer.SessionClosed += appServer_SessionClosed;

            //Setup the appServer
            if (!appServer.Setup(config)) //Setup with listening port
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return false;
            }

            Console.WriteLine();

            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        void appServer_SessionClosed(MySession session, CloseReason value)
        {
            Console.WriteLine("断开了" + session.SessionID + " " + value.ToString());
            dic.Remove(session.SessionID);
            ReceiveController.SessionClose(session.SessionID);

            testIds.Remove(session.SessionID);
        }

        void appServer_NewSessionConnected(MySession session)
        {
            Console.WriteLine("连上了" + session.SessionID);
            dic.Add(session.SessionID, session);
            ReceiveController.SessionConnect(session.SessionID, session.StartTime);

            testIds.Add(session.SessionID);
        }

        private void appServer_NewRequestReceived(MySession session, MyRequestInfo requestInfo)
        {
            Console.WriteLine("收到了" + session.SessionID);
            session.Send(reply2Client);
            //if (!session.Updated)
            //{
            ReceiveController.UpdateOrSaveSession(session.SessionID, session.StartTime, requestInfo.JsonInfo);
            //    session.Updated = true;
            //}
        }

        //一些固定消息的定义
        string reply2Client = "{\"status\":0,\"message\":\"ok\",\"data\":null}";

        public void stop()
        {
            if (null != appServer)
            {
                appServer.Stop();
            }
        }

        /// <summary>
        /// 往仪器端发送消息
        /// </summary>
        /// <param name="sessionId">socket连接的ID</param>
        /// <param name="msg">消息内容</param>
        /// <returns>操作结果</returns>
        public static ResultInfo sendMsg(string sessionId, string msg)
        {
            ResultInfo result = new ResultInfo();
            MySession session;

            bool bl = dic.TryGetValue(sessionId, out session);
            if (bl)
            {
                bl = session.TrySend(msg);
                if (!bl)
                {
                    result.code = 100;
                    result.msg = "发送消息失败.";
                }
            }
            else
            {
                result.code = 200;
                result.msg = "该设备不在线.";
            }

            return result;
        }

        /// <summary>
        /// for test
        /// </summary>
        public static List<string> testIds = new List<string>();

    }
}
