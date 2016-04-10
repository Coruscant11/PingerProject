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

        /// <summary>
        ///  Constructeur récupérant les valeurs du serveur
        /// </summary>
        /// <param name="serveur">Adresse IP du serveur</param>
        /// <param name="port">Port du serveur</param>
        /// <param name="servertype">Nom du serveur</param>
        public MinecraftPinger(string server, uint port, string servertype) : base(server, port, servertype)
        {

        }

        /// <summary>
        /// Ping le serveur
        /// </summary>
        /// <returns>Retourne l'état du serveur</returns>
        public override bool Ping()
        {
            var client = new TcpClient(); // Initialisation du client TCP  
            var task = client.ConnectAsync(this.ServerIP, (int) this.Port); // Connection au serveur
            Console.WriteLine("Connexion au serveur " + this.ServerType + "...");

            /* Attend la connectin */
            while (!task.IsCompleted)
                continue;

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
