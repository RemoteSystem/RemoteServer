using RemoteModel;
using ServerController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer.SocketServer server = new SocketServer.SocketServer();
            bool bl = server.start();

            if (!bl)
            {
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");
            char c;
            while ((c = Console.ReadKey().KeyChar) != 'q')
            {
                if (c == 's')
                {
                    testSend();
                }
                if (c == 'a')
                {
                    senginfo();
                }
                Console.WriteLine();
                continue;
            }

            server.stop();
            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        private static void testSend()
        {
            BloodSettings info = new BloodSettings();
            info.dump = new Dump();
            info.SN = "z320190120";
            info.sim = "15928882400";
            info.inf_query = "BI";
            info.model_setup = "Z3";
            info.reagent_setup = "OPEN";
            info.oem_change = 1;
            info.agent_change = 1;
            info.lang_change = "chinese";
            info.sn_setup = "Z3201801001";
            info.remote_shut = "close";
            info.dump.encoding = "base64";
            info.dump.filename = "update.tar.gz";
            info.dump.data = "";

            string testId = SocketServer.SocketServer.testIds.Count > 0 ? SocketServer.SocketServer.testIds[0] : "当前没有连接";

            ServerService service = new ServerService();
            ResultInfo ri = service.updateBloodParas(testId, info);
            Console.WriteLine(ri.msg);
        }

        private static void senginfo()
        {
            string msg = "{\"encoding\":\"utf-8\",\"request\":\"uuid\",\"sn\":\"仪器序列号\",\"sim\":\"SIM卡编号\",\"model\":\"仪器型号\",\"region\":\"装机区域\",\"addr\":\"医院地址\",\"hospital\":\"医院名字\",\"dump \":{\"encoding\":\"base64\",\"filename\":\"Log20180121.tar.gz\",\"data\":\"wsdsads\"},\"update_time\":\"2019-02-22 15:23:12\",\"machine_type\":1,\"category\":{\"poct\":{\"item\":[{\"num\":\"123\",\"card_name\":\"测试项目\",\"incubate_time\":\"10\",\"analyte_1\":\"分析项目1名称\",\"analyte_2\":\"分析项目2名称\",\"analyte_3\":\"分析项目3名称\",\"signals\":\"5\",\"card_lot\":\"测试卡批次\",\"expiry\":\"测试卡有效期\",\"analyte_1_params\":\"分析项目1参数\",\"analyte_2_params\":\"分析项目2参数\",\"analyte_3_params\":\"分析项目3参数\"}],\"statistics\":{\"sample\":\"20\",\"item\":[{\"num\":\"项目编号\",\"smpl\":\"100\",\"card_consume\":\"120\"}]},\"fault\":[{\"code\":\"A1001\",\"time\":\"2019-06-27 19:32:45.123\"}]}}}";
            string testId = SocketServer.SocketServer.testIds.Count > 0 ? SocketServer.SocketServer.testIds[0] : "当前没有连接";

            ServerService service = new ServerService();
            ResultInfo ri = service.updateBloodParas(testId, msg);
            Console.WriteLine(ri.msg);
        }

    }
}
