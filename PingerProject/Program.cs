using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace PingerProject
{
    class Program
    {
        public static string ServerAdress, StatusFilePath; // Adresse IP du serveur, Chemin du fichier d'écriture
        public static int Timer; // Temps pour sleep le Thread

        static void Main(string[] args)
        {
            /* Récupération des valeurs de configurations dans le config.txt */
            string[] configs = System.IO.File.ReadAllLines("config.txt");
            ServerAdress = configs[0];
            Timer = int.Parse(configs[1]);
            StatusFilePath = configs[2]; 

            /* Messages de bienvenus avec la console */
            Console.Title = "PingerProject - by Coruscant11 - [OSBLC]";
            Console.WriteLine("Bienvenue dans le PingerProject de l'OSBLC.\nVeuillez appuyer sur entrée pour démarrer le programme avec pour serveur \"" + ServerAdress + "\".\n");
            Console.Read();
            Console.WriteLine("Lancement du programme avec pour timer " + Timer + " secondes...\n");

            /* Initialisation du writers des données dans le fichiers status.txt */
            StatusWriter sw = new StatusWriter(StatusFilePath);

            /* Boucle infini */
            while (true)
            {
                /* Récupération des différentes valeurs booléennes des pings */
                bool vanilla = ProcessMinecraftVanilla();
                bool ftb = ProcessMinecraftFTB();
                bool ts = ProcessTeamspeak();
                bool ftp = ProcessFtp();
                CSStatus csgo = ProcessCounterStrike();

                sw.WriteStatus(vanilla, ftb, ts, ftp, csgo.Status, csgo.Map); // Ecriture des valeurs dans le fichier 

                System.Threading.Thread.Sleep(Timer * 1000); // On sleep le thread avec le temps donné
            }
        }

        /// <summary>
        ///  Ping le serveur Minecraft Vanilla
        /// </summary>
        /// /// <returns>Retourne une valeure booléenne si le serveur est en ligne ou non.</returns>
        static bool ProcessMinecraftVanilla()
        {
            Console.WriteLine("------------------------------");

            MinecraftPinger vanilla = new MinecraftPinger(ServerAdress, 25566, "Vanilla"); // Création de l'objet pinguant la classe

            if (vanilla.Ping())
            {
                Console.WriteLine("Le serveur Vanilla est disponible.");
                Console.WriteLine("------------------------------\n");
                return true;
            }
            else
            {
                Console.WriteLine("Le serveur Vanilla est indisponible.");
                Console.WriteLine("------------------------------\n");
                return false;
            }
        }

        /// <summary>
        ///  Ping le serveur Minecraft Feed the Beast
        /// </summary>
        /// /// <returns>Retourne une valeure booléenne si le serveur est en ligne ou non.</returns>
        static bool ProcessMinecraftFTB()
        {
            Console.WriteLine("------------------------------");

            MinecraftPinger ftb = new MinecraftPinger(ServerAdress, 25565, "Feed The Beast"); // Création de l'objet pinguant la classe

            if (ftb.Ping())
            {
                Console.WriteLine("Le serveur Feed The Beast est disponible.");
                Console.WriteLine("------------------------------\n");
                return true;
            }
            else
            {
                Console.WriteLine("Le serveur Feed The Beast est indisponible.");
                Console.WriteLine("------------------------------\n");
                return false;
            }
        }

        /// <summary>
        ///  Ping le serveur Teamspeak
        /// </summary>
        /// /// <returns>Retourne une valeure booléenne si le serveur est en ligne ou non.</returns>
        static bool ProcessTeamspeak()
        {
            Console.WriteLine("------------------------------");

            TeamspeakPinger ts = new TeamspeakPinger(ServerAdress, 10011, "Teamspeak"); // Création de l'objet pinguant la classe

            if (ts.Ping())
            {
                Console.WriteLine("Le serveur Teamspeak est disponible.");
                Console.WriteLine("------------------------------\n");
                return true;
            }
            else
            {
                Console.WriteLine("Le serveur Teamspeak est indisponible.");
                Console.WriteLine("------------------------------\n");
                return false;
            }
        }

        /// <summary>
        ///  Ping le serveur FTP
        /// </summary>
        /// /// <returns>Retourne une valeure booléenne si le serveur est en ligne ou non.</returns>
        static bool ProcessFtp()
        {
            Console.WriteLine("------------------------------");

            FTPPinger ftp = new FTPPinger("ftp://" + ServerAdress, 21, "FTP"); // Création de l'objet pinguant la classe

            if (ftp.Ping())
            {
                Console.WriteLine("Le serveur FTP est disponible.");
                Console.WriteLine("------------------------------\n");
                return true;
            }
            else
            {
                Console.WriteLine("Le serveur FTP est indisponible.");
                Console.WriteLine("------------------------------\n");
                return false;
            }
        }

        /// <summary>
        ///  Ping le serveur Counter-Strike Global-Offensive
        /// </summary>
        /// /// <returns>Retourne un objet contenant une valeure booléenne si le serveur est en ligne ou non et la map actuellement en ligne.</returns>
        static CSStatus ProcessCounterStrike()
        {
            Console.WriteLine("------------------------------");

            CounterStrikePinger csgo = new CounterStrikePinger(ServerAdress, 27015, "Counter-Strike Global Offensive"); // Création de l'objet pinguant la classe
            CSStatus status = csgo.PingWithMap(); // Ping le serveur et récupère le CSStatus

            if (status.Status)
            {
                Console.WriteLine("Le serveur Counter-Strike Global Offensive est disponible.");
                Console.WriteLine("------------------------------\n");
                return status;
            }
            else
            {
                Console.WriteLine("Le serveur Counter-Strike Global Offensive est indisponible.");
                Console.WriteLine("------------------------------\n");
                return status;
            }
        }
    }
}
