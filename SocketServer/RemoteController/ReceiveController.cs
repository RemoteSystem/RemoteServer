using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RemoteModel;
using System;
using System.Collections.Generic;
using RemoteDao;

namespace RemoteController
{
    public class ReceiveController
    {
        public static void SessionConnect(string id, DateTime dt)
        {
            ReceiveDao.SessionConnect(id, dt);
        }

        public static int UpdateOrSaveSession(string id, DateTime dt, string strJson)
        {
            ClientInfo info;
            int clientId = 0;
            try
            {
                info = JsonConvert.DeserializeObject<ClientInfo>(strJson);
            }
            catch (Exception e)
            {
                info = null;
                Console.WriteLine("Json 转 ClientInfo 出错：" + e.Message);
            }

            if (info != null)
            {
                info.sessionid = id;
                info.starttime = dt;
                clientId = ReceiveDao.UpdateOrSaveSession(info);
            }
            else
            {
                Console.WriteLine("ClientInfo 为 null.");
            }

            return clientId;
        }

        public static void SessionClose(string id)
        {
            ReceiveDao.SessionClose(id);
        }

    }
}
