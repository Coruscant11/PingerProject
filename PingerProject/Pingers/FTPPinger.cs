using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Limilabs.FTP.Client;

namespace PingerProject
{
    class FTPPinger : Pinger
    {
        /// <summary>
        ///  Constructeur récupérant les valeurs du serveur
        /// </summary>
        /// <param name="serveur">Adresse IP du serveur</param>
        /// <param name="port">Port du serveur</param>
        /// <param name="servertype">Nom du serveur</param>
        public FTPPinger(string server, uint port, string servertype) : base(server, port, servertype)
        {

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
                using (Ftp ftp = new Ftp()) // Initialisation de l'objet du FTP
                {
                    ftp.Connect("server.osblc.fr");  // Connection au FTP sans login

                    if(ftp.Connected)
                    {
                        ftp.Close();
                        return true;
                    }
                    else
                    {
                        ftp.Close();
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
