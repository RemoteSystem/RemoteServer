﻿using RemoteModel;
using RemoteServer;
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
            SocketServer server = new SocketServer();
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
            BloodMsg blood = new BloodMsg();
            blood.oem_change = 10;

            string testId = SocketServer.testId;

            ServerService service = new ServerService();
            ResultInfo ri = service.updateBloodParas(testId, blood);
            Console.WriteLine(ri.msg);
        }

    }
}
