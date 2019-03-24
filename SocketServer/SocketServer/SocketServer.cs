using RemoteController;
using RemoteModel;
using SocketServer;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class SocketServer
    {
        //***********************************************************************//
        //****** 说明，引用这个程序集所在的程序集里面需要引用supersocket，不然报错.
        //***********************************************************************//
        ProtocolServer appServer = null;
        static Dictionary<string, ProtocolSession> dic = new Dictionary<string, ProtocolSession>();
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
                MaxRequestLength = 1024 * 2, //最大包长度
                Ip = "Any",
                Port = 12315,
                MaxConnectionNumber = 300,
                TextEncoding = "GBK"
            };

            appServer = new ProtocolServer();
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

        void appServer_SessionClosed(ProtocolSession session, CloseReason value)
        {
            Console.WriteLine("断开了 " + session.SessionID + " " + value.ToString());
            dic.Remove(session.SessionID);
            ReceiveController.SessionClose(session.SessionID);

            testIds.Remove(session.SessionID);
        }

        void appServer_NewSessionConnected(ProtocolSession session)
        {
            Console.WriteLine("连上了 " + session.SessionID);
            dic.Add(session.SessionID, session);
            ReceiveController.SessionConnect(session.SessionID, session.StartTime);

            testIds.Add(session.SessionID);
        }

        private void appServer_NewRequestReceived(ProtocolSession session, ProtocolRequestInfo requestInfo)
        {
            if (requestInfo == null)
            {
                Console.WriteLine("非正常包 " + session.SessionID);
                return;
            }
            //心跳
            if (requestInfo.Length == 0)
            {
                Console.WriteLine("心跳 " + session.SessionID);
                return;
            }
            else if (requestInfo.Length < 0)
            {
                Console.WriteLine("异常数据,算作心跳 " + session.SessionID);
                return;
            }

            Console.WriteLine("收到了 " + session.SessionID);
            sendReplyMsg(session, reply2Client);

            //报错数据
            ReceiveController.UpdateOrSaveSession(session.SessionID, session.StartTime, requestInfo.Data);
        }

        //一些固定消息的定义
        string reply2Client = "{\"status\":0,\"message\":\"ok\",\"data\":null}";

        public void stop()
        {
            if (null != appServer)
            {
                appServer.Stop();
            }
            ReceiveController.ServerStop();
        }

        /// <summary>
        /// 往仪器端回复消息
        /// </summary>
        /// <param name="session">socket连接</param>
        /// <param name="msg">消息内容</param>
        public static ResultInfo sendReplyMsg(ProtocolSession session, string msg)
        {
            ResultInfo result = new ResultInfo();

            byte[] dataByte = Encoding.UTF8.GetBytes(msg);
            byte[] startByte = HexStrTobyte("aa 55");
            byte[] controlByte = HexStrTobyte("02");
            byte[] lenByte = BitConverter.GetBytes(dataByte.Length);
            if (BitConverter.IsLittleEndian)//判断计算机结构的endian设置(高低位转换)
                Array.Reverse(lenByte);
            byte[] verfiByte = HexStrTobyte("1c 1d");
            int len = 7 + dataByte.Length + 2;
            byte[] b = new byte[len];
            Buffer.BlockCopy(startByte, 0, b, 0, 2);
            Buffer.BlockCopy(controlByte, 0, b, 2, 1);
            Buffer.BlockCopy(lenByte, 0, b, 3, 4);
            Buffer.BlockCopy(dataByte, 0, b, 7, dataByte.Length);
            Buffer.BlockCopy(verfiByte, 0, b, 7 + dataByte.Length, 2);

            bool bl = session.TrySend(b, 0, b.Length);
            if (!bl)
            {
                result.code = 100;
                result.msg = "发送消息失败.";
            }


            return result;
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
            ProtocolSession session;

            bool bl = dic.TryGetValue(sessionId, out session);
            if (bl)
            {
                sendReplyMsg(session, msg);
                //bl = session.TrySend(msg);
                //if (!bl)
                //{
                //    result.code = 100;
                //    result.msg = "发送消息失败.";
                //}
            }
            else
            {
                result.code = 200;
                result.msg = "该设备不在线.";
            }

            return result;
        }

        /// <summary>
        /// 16进制字符串转字节数组   格式为 string sendMessage = "00 01 00 00 00 06 FF 05 00 64 00 00";
        /// </summary>
        /// <param name="hexString">16进制字符串转</param>
        /// <returns>字节数组</returns>
        private static byte[] HexStrTobyte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }

        /// <summary>
        /// for test
        /// </summary>
        public static List<string> testIds = new List<string>();

    }
}
