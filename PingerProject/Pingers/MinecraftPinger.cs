using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace PingerProject
{
    class MinecraftPinger : Pinger
    {

        public MinecraftPinger(string server, uint port, string servertype) : base(server, port, servertype)
        {

        }

        public override bool Ping()
        {
            var client = new TcpClient();       
            var task = client.ConnectAsync(this.ServerIP, (int) this.Port);
            Console.WriteLine("Connexion au serveur " + this.ServerType + "...");

            while(!task.IsCompleted)
                Port = 27;

            if (client.Connected)
            {
                client.Close();
                return true;
            }
            else
            {
                client.Close();
                return false;
            }
        }
    }
}
