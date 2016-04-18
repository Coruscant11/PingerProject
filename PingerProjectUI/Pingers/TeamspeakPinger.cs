using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerProject
{
    class TeamspeakPinger : Pinger
    {
        public int ClientsConnected { get; private set; }
        public double AverragePing { get; private set; }
        public double PacketLoss { get; private set; }
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

        /// <summary>
        /// Ping le serveur et fournit le nombre de clients connectés
        /// </summary>
        /// <returns>Retourne l'état du serveur</returns>
        public bool PingAndGetClientsAndAverragePing()
        {
            try
            {
                Console.WriteLine("Connexion au serveur " + this.ServerType + "...");
                MinimalisticTelnet.TelnetConnection connection = new MinimalisticTelnet.TelnetConnection(this.ServerIP, 10011);

                string response = connection.Read();

                if (response.Length > 0)
                {
                    /* Utilisation de l'id 1 */
                    connection.WriteLine("use 1");
                    connection.Read();

                    /* Récupération des informations */
                    connection.WriteLine("serverinfo");
                    System.Threading.Thread.Sleep(150);

                    string serverinfo = connection.Read();
                    string serverinfoCopy = serverinfo;
                    string serverinfoCopy2 = serverinfo;

                    /* Recherche de la valeur dans le string */
                    string searchForThis = "virtualserver_clientsonline=";
                    int firstCharacter = serverinfo.IndexOf(searchForThis);
                    string s = serverinfo.Substring(firstCharacter + searchForThis.Length, 2);

                    /* Recherche de la valeur dans le string */
                    string searchForThis2 = "virtualserver_total_ping=";
                    int firstCharacter2 = serverinfoCopy.IndexOf(searchForThis2);
                    string s2 = serverinfoCopy.Substring(firstCharacter2 + searchForThis2.Length, 7);

                    /* Recherche de la valeur dans le string */
                    string searchForThis3 = "virtualserver_total_packetloss_total=";
                    int firstCharacter3 = serverinfoCopy2.IndexOf(searchForThis3);
                    string s3 = serverinfoCopy2.Substring(firstCharacter3 + searchForThis3.Length, 6);

                    connection.WriteLine("logout"); // Déconnexion

                    this.ClientsConnected = int.Parse(s); // Conversion de la chaîne de caractère en int
                    Console.WriteLine(s2);
                    this.AverragePing = double.Parse(s2.Replace('.', ','));
                    this.PacketLoss = System.Math.Round(double.Parse(s3.Replace('.', ',')), 2);

                    /* Destruction des variables */
                    connection = null;
                    serverinfo = null;
                    s = null;
                    firstCharacter = 0;
                    searchForThis = null;

                    return true;
                }
                else return false;

            }
            catch(Exception e) {
                Console.WriteLine(e);
                return false; }
        }
    }
}