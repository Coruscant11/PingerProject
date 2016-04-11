using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerProject
{
    class TeamspeakPinger : Pinger
    {
        /// <summary>
        ///  Constructeur récupérant les valeurs du serveur
        /// </summary>
        /// <param name="serveur">Adresse IP du serveur</param>
        /// <param name="port">Port du serveur</param>
        /// <param name="servertype">Nom du serveur</param>
        public TeamspeakPinger(string server, uint port, string servertype) : base(server, port, servertype)
        {

        }
         
        /// <summary>
        /// Ping le serveur
        /// </summary>
        /// <returns>Retourne l'état du serveur</returns>
        public override bool Ping()
        {
            try
            {
                Console.WriteLine("Connexion au serveur " + this.ServerType + "...");
                MinimalisticTelnet.TelnetConnection connection = new MinimalisticTelnet.TelnetConnection(this.ServerIP, 10011);

                string response = connection.Read();
                connection = null;

                return response.Length > 0 ? true : false;

            }
            catch { return false; }
        }
    }
}