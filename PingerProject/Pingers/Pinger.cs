using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerProject
{
    /// <summary>
    ///  Classe abstraite définissant les classes qui vont pinguer les différents serveurs
    /// </summary>
    abstract class Pinger
    {
        /* Adresse IP du serveur, nom du serveur et le Port */
        protected string ServerIP, ServerType;
        protected uint Port;

        /// <summary>
        ///  Constructeur récupérant les valeurs du serveur
        /// </summary>
        /// <param name="serveur">Adresse IP du serveur</param>
        /// <param name="port">Port du serveur</param>
        /// <param name="servertype">Nom du serveur</param>
        public Pinger(string server, uint port, string servertype)
        {
            this.ServerIP = server;
            this.ServerType = servertype;
            this.Port = port;
        }

        /// <summary>
        ///  Fonction a redéfinir qui ping le serveur
        /// </summary>
        /// <returns>Retourne l'état du serveur</returns>
        public abstract bool Ping();
    }
}
