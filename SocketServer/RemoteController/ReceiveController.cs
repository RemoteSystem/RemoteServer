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
            BloodInfo bloodInfo = null;
            BioInfo bioInfo = null;

            strJson = strJson.Trim().TrimStart('&').TrimEnd('#');
            try
            {
                if (strJson.IndexOf("\"bio\":{") >= 0)
                {//生化仪
                    bioInfo = JsonConvert.DeserializeObject<BioInfo>(strJson);
                }
                else
                {//血液分析仪
                    bloodInfo = JsonConvert.DeserializeObject<BloodInfo>(strJson.Replace("+", "_"));
                }
            }
            catch (Exception e)
            {
                bloodInfo = null;
                Console.WriteLine("Json 转 ClientInfo 出错：" + e.Message);
            }

            if (bloodInfo != null)
            {
                bloodInfo.sessionid = id;
                bloodInfo.starttime = dt;

                num = ReceiveDao.UpdateOrSaveSession(bloodInfo);
                SaveBloodData(bloodInfo);
            }
            else if (bioInfo != null)
            {
                bioInfo.sessionid = id;
                bioInfo.starttime = dt;
                num = ReceiveDao.UpdateOrSaveSessionForBio(bioInfo);
                SaveBioData(bioInfo);
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

        private static void SaveBloodData(BloodInfo info)
        {
            if (info.category != null && info.category.BLOOD != null)
            {
                try
                {
                    BloodDao.UpdateOrSaveRuntime(info);
                    BloodDao.UpdateOrSaveCount(info);
                    BloodDao.InsertCountDetail(info);
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

        private static void SaveBioData(BioInfo info)
        {
            try
            {
                if (info.dump != null)
                {
                    BioDao.InsertDump(info);
                }
                if (info.category != null && info.category.bio != null)
                {
                    BioDao.UpdateOrSaveBioItem(info);
                    BioDao.UpdateOrSaveStatistics(info);
                    BioDao.UpdateOrSaveStatisticsItem(info);
                    BioDao.SaveFault(info);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
