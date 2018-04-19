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
            string msg = "{ \"category\" : { \"blood\" : { \"fault\" : [ { \"code\" : \"16789557\", \"time\" : \"2018-04-18 16:57:59\" }, { \"code\" : \"16789558\", \"time\" : \"2018-04-18 17:57:59\" }], \"update_time\" : \"2018-04-18 16:57:59\" } }, \"encoding\" : \"utf-8\", \"sn\" : \"201804174567\" }";
            string testId = SocketServer.SocketServer.testIds.Count > 0 ? SocketServer.SocketServer.testIds[0] : "当前没有连接";

            ServerService service = new ServerService();
            ResultInfo ri = service.updateBloodParas(testId, msg);
            Console.WriteLine(ri.msg);
        }

    }
}
