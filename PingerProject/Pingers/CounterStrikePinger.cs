using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using AServerQuery;

namespace PingerProject
{
    class CounterStrikePinger : Pinger
    {
        private SourceServer CounterStrikeServer;
        
        public CounterStrikePinger(string server, uint port, string servertype) : base(server, port, servertype)
        {
            this.CounterStrikeServer = new SourceServer(new IPEndPoint(IPAddress.Parse(this.ServerIP), (int)this.Port), String.Empty);
        }

        public override bool Ping()
        {
            Console.WriteLine("Connexion au serveur " + this.ServerType + "...");
            return this.CounterStrikeServer.Ping();
        }

        public CSStatus PingWithMap()
        {
            Console.WriteLine("Connexion au serveur " + this.ServerType + "...");
            CSStatus status = new CSStatus();
            status.Status = this.CounterStrikeServer.Ping();

            if (status.Status)
                status.Map = this.GetMap();
            else
                status.Map = "none";

            return status;
        }

        public int GetPlayers()
        {
            var info = this.CounterStrikeServer.GetInfo();
            return info.NumPlayers;
        }

        public string GetMap()
        {
            var info = this.CounterStrikeServer.GetInfo();
            return info.Map;
        }
    }
}
