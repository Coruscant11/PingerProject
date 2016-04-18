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

        /// <summary>
        ///  Constructeur récupérant les valeurs du serveur et se connectant a celui-ci
        /// </summary>
        /// <param name="serveur">Adresse IP du serveur</param>
        /// <param name="port">Port du serveur</param>
        /// <param name="servertype">Nom du serveur</param>
        public CounterStrikePinger(string server, uint port, string servertype) : base(server, port, servertype)
        {
            this.CounterStrikeServer = new SourceServer(new IPEndPoint(IPAddress.Parse(this.ServerIP), (int)this.Port), String.Empty); // Connection au serveur CSGO
        }

        /// <summary>
        /// Ping le serveur
        /// </summary>
        /// <returns>Retourne l'état du serveur</returns>
        public override bool Ping()
        {
            Console.WriteLine("Connexion au serveur " + this.ServerType + "...");
            try
            {
                return this.CounterStrikeServer.Ping();
            }
            catch { return false; }
        }

        /// <summary>
        /// Ping le serveur
        /// </summary>
        /// <returns>Retourne l'état et la map du serveur</returns>
        public CSStatus PingWithMap()
        {
            Console.WriteLine("Connexion au serveur " + this.ServerType + "...");
            CSStatus status = new CSStatus();
            try
            {
                status.Status = this.CounterStrikeServer.Ping(); // Ping le serveur

                if(status.Status)
                {
                    status.Map = this.GetMap();
                    status.PlayersConnected = this.GetPlayers();
                    status.PlayersMax = this.GetPlayersMax();
                    return status;
                }
                else
                {
                    CSStatus wrong = new CSStatus();
                    wrong.Status = false;
                    wrong.Map = "none";
                    wrong.PlayersConnected = 0;
                    wrong.PlayersMax = 0;
                    return wrong;
                }
            }
            catch {
                /* Si le serveur ne fonctionne pas, crée un CSStatus avec les valeurs concordantes */
                CSStatus wrong = new CSStatus();
                wrong.Status = false;
                wrong.Map = "none";
                wrong.PlayersConnected = 0;
                wrong.PlayersMax = 0;
                return wrong;
            }
        }

        /// <summary>
        /// Nombre de joueurs connectés
        /// </summary>
        /// <returns>Retourne le nombre de joueurs connectés</returns>
        public int GetPlayers()
        {
            var info = this.CounterStrikeServer.GetInfo(); // Récupère les infos du serveur
            return info.NumPlayers;
        }


        /// <summary>
        /// Map disponible
        /// </summary>
        /// <returns>Retourne le nom de la map disponible</returns>
        public string GetMap()
        {
            var info = this.CounterStrikeServer.GetInfo(); // Récupère les infos du serveur
            return info.Map;
        }

        /// <summary>
        /// Nombre de slots
        /// </summary>
        /// <returns>Retourne le nombre maximal de joueurs</returns>
        public int GetPlayersMax()
        {
            var info = this.CounterStrikeServer.GetInfo();
            return info.MaxPlayers;
        }
    }
}
