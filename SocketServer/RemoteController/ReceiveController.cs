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
            JsonInfo info;
            int num = 0;
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
                num = ReceiveDao.UpdateOrSaveSession(info);
                SaveData(info);
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}
