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
            string msg = "{\"addr\":\"长沙市\",\"category\":{\"bio\":{\"fault\":[{\"code\":\"C10001\",\"time\":\"2019-01-18 15:08\"}],\"item\":[{\"blank_time\":12,\"calibration_method\":\"wer李\",\"corrected_intercept\":12.3,\"corrected_slope\":12.5,\"first_reagent_volume\":1.2,\"k_factor_value\":3.4,\"main_wavelength\":145,\"measuring_method\":\"未知\",\"num\":\"67\",\"reaction_direction\":\"1-2\",\"reaction_time\":12,\"sample_volume\":45.5,\"second_reagent_volume\":2.3,\"sub_wavelength\":245}],\"statistics\":{\"item\":[{\"R1\":5.6,\"R2\":76,\"num\":\"tyu\",\"smpl\":12}],\"sample\":89}}},\"dump\":{\"data\":\"aW50IG1haW4oKQp7CiAgICB0cnkKICAgIHsKICAgICAgICBzdGQ6OnN0cmluZyBzZXJJcCA9ICIxMjcuMC4wLjEiOwogICAgICAgIHN0ZDo6c3RyaW5nIHNlclBvcnQgPSAiODA4MSI7CiAgICAgICAgQ2VudGVyU2VydmVyIHNlcnZlciggc2VySXAsIHNlclBvcnQgKTsKICAgICAgICBzdGQ6OmNvdXQgPDwgInNlcnZlciBzdGFydCIgPDwgc3RkOjplbmRsOwogICAgICAgIHNlcnZlci5SdW4oKTsKICAgICAgICBzdGQ6OmNvdXQgPDwgInNlcnZlciBlbmQiIDw8IHN0ZDo6ZW5kbDsKICAgIH0KICAgIGNhdGNoICggc3RkOjpleGNlcHRpb24mIGUgKQogICAgewogICAgICAgIHN0ZDo6Y2VyciA8PCAiRXhjZXB0aW9uOiAiIDw8IGUud2hhdCgpIDw8ICJcbiI7CiAgICB9CiAgICAKICAgIHJldHVybiAwOwp9Cg==\",\"encoding\":\"base64\",\"filename\":\"log20190121.tar.gz\"},\"encoding\":\"utf-8\",\"hospital\":\"第四人民医院\",\"machine_type\":0,\"model\":\"ZS400\",\"region\":\"湖南省\",\"request\":\"246807df-7e49-443e-937e-342d89f6c85c\",\"sim\":\"123578910\",\"sn\":\"ZS12341234567321\",\"update_time\":\"2019-03-21 13:26:34\"}";
            string testId = SocketServer.SocketServer.testIds.Count > 0 ? SocketServer.SocketServer.testIds[0] : "当前没有连接";

            ServerService service = new ServerService();
            ResultInfo ri = service.updateBloodParas(testId, msg);
            Console.WriteLine(ri.msg);
        }

    }
}
