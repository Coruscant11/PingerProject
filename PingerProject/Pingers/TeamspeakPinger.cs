using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerProject
{
    class TeamspeakPinger : Pinger
    {
        public TeamspeakPinger(string server, uint port, string servertype) : base(server, port, servertype)
        {

        }

        public override bool Ping()
        {
            Console.WriteLine("Connexion au serveur " + this.ServerType + "...");
            MinimalisticTelnet.TelnetConnection connection = new MinimalisticTelnet.TelnetConnection(this.ServerIP, 10011);

            string response = connection.Read(); 
            connection = null;

            if (response.Length > 0)
                return true;
            else
                return false;
        }
    }
}