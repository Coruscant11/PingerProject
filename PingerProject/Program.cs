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
        public static string ServerAdress;
        public static int Timer;

        static void Main(string[] args)
        {
            string[] configs = System.IO.File.ReadAllLines("config.txt");
            ServerAdress = configs[0];
            Timer = int.Parse(configs[1]);

            Console.Title = "PingerProject - by Coruscant11 - [OSBLC]";
            Console.WriteLine("Bienvenue dans le PingerProject de l'OSBLC.\nVeuillez appuyer sur entrée pour démarrer le programmeavec pour serveur \"" + ServerAdress + "\".\n");
            Console.Read();
            Console.WriteLine("Lancement du programme avec pour timer " + Timer + " secondes...\n");

            StatusWriter sw = new StatusWriter("status.txt");

            while (true)
            {
                bool vanilla = ProcessMinecraftVanilla();
                bool ftb = ProcessMinecraftFTB();
                bool ts = ProcessTeamspeak();
                bool ftp = ProcessFtp();
                CSStatus csgo = ProcessCounterStrike();

                sw.WriteStatus(vanilla, ftb, ts, ftp, csgo.Status, csgo.Map);
                System.Threading.Thread.Sleep(Timer * 1000);
            }
        }

        static bool ProcessMinecraftVanilla()
        {
            Console.WriteLine("------------------------------");

            MinecraftPinger vanilla = new MinecraftPinger(ServerAdress, 25566, "Vanilla");

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

        static bool ProcessMinecraftFTB()
        {
            Console.WriteLine("------------------------------");

            MinecraftPinger ftb = new MinecraftPinger(ServerAdress, 25565, "Feed The Beast");

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

        static bool ProcessTeamspeak()
        {
            Console.WriteLine("------------------------------");

            TeamspeakPinger ts = new TeamspeakPinger(ServerAdress, 10011, "Teamspeak");

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

        static bool ProcessFtp()
        {
            Console.WriteLine("------------------------------");

            FTPPinger ftp = new FTPPinger("ftp://" + ServerAdress + "/zizi.txt", 21, "FTP");

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

        static CSStatus ProcessCounterStrike()
        {
            Console.WriteLine("------------------------------");

            CounterStrikePinger csgo = new CounterStrikePinger(ServerAdress, 27015, "Counter-Strike Global Offensive");
            CSStatus status = csgo.PingWithMap();

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
