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
                Console.WriteLine();
                continue;
            }

            server.stop();
            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        private static void testSend()
        {
            JsonInfo info = new JsonInfo();
            info.category = new Category();
            info.category.BLOOD = new BLOOD();
            info.category.BLOOD.OEM = "10";

            string testId = SocketServer.SocketServer.testIds.Count > 0 ? SocketServer.SocketServer.testIds[0] : "当前没有连接";

            ServerService service = new ServerService();
            ResultInfo ri = service.updateBloodParas(testId, info);
            Console.WriteLine(ri.msg);
        }

    }
}
