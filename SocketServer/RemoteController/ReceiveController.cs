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
            int num = 0;
            JsonInfo info;

            strJson = strJson.Trim().TrimStart('&').TrimEnd('#');
            try
            {
                info = JsonConvert.DeserializeObject<JsonInfo>(strJson.Replace("+", "_"));
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
                if (info.category != null && info.category.BLOOD != null)
                {
                    num = ReceiveDao.UpdateOrSaveSession(info);
                    SaveData(info);
                }
            }
            else
            {
                Console.WriteLine("ClientInfo 为 null.");
            }

            return num;
        }

        public static void SessionClose(string id)
        {
            ReceiveDao.SessionClose(id);
        }

        public static void ServerStop()
        {
            ServerDao.ServerStop();
        }

        private static void SaveData(JsonInfo info)
        {
            if (info.category.BLOOD != null)
            {
                try
                {
                    BloodDao.UpdateOrSaveRuntime(info);
                    BloodDao.UpdateOrSaveCount(info);
                    BloodDao.UpdateOrSaveReagent(info);
                    BloodDao.UpdateOrSaveModule(info);
                    BloodDao.SaveFault(info);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}
