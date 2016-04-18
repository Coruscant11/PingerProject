using PingerProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PingerProjectUI
{
    class MainProcess
    {
        private static MainForm Form;

        private static string ServerAdress, FTPAdress, StatusFilePath;
        private static int Timer;
        private static int TeamspeakTimer, VanillaTimer, FTBTimer, FTPTimer, CounterStrikeTimer;

        private bool CanRun;
        private Thread MainProcessThread, TeamspeakThread, VanillaThread, FTBThread, FTPThread, CounterThread;

        private static bool TeamspeakState, VanillaState, FTBState, FTPState, CounterStrikeState;
        private static int TeamspeakClientsConnected;
        private static double TeamspeakAverragePing, TeamspeakPacketLoss;
        private static int CounterStrikePlayersConnected;
        private static int CounterStrikePlayersMax;
        private static string CounterStrikeMap;

        /// <summary>
        /// Constructeur chargeant les données depuis le fichier config et initialisant les threads
        /// </summary>
        /// <param name="mainForm">Fenêtre Principale</param>
        public MainProcess(MainForm mainForm)
        {
            /* Chargement des données depuis le fichier de configuration */
            ServerAdress = ConfigurationLoader.GetServerAdress();
            FTPAdress = ConfigurationLoader.GetServerIpV4();
            StatusFilePath = ConfigurationLoader.GetStatusFilePath();

            Timer = ConfigurationLoader.GetRewriteHTMLTimer();
            TeamspeakTimer = ConfigurationLoader.GetTeamspeakTimer();
            VanillaTimer = ConfigurationLoader.GetVanillaTimer();
            FTBTimer = ConfigurationLoader.GetFeedTheBeastTimer();
            FTPTimer = ConfigurationLoader.GetFTPTimer();
            CounterStrikeTimer = ConfigurationLoader.GetCounterStrikeTimer();

            /* Récupération de la fenêtre principale */
            Form = mainForm;

            /* Initialisation des Threads */
            this.MainProcessThread = new Thread(new ThreadStart(this.LaunchProcess));
            this.TeamspeakThread = new Thread(new ThreadStart(this.TeamSpeakProcess));
            this.VanillaThread = new Thread(new ThreadStart(this.VanillaProcess));
            this.FTBThread = new Thread(new ThreadStart(this.FTBProcess));
            this.FTPThread = new Thread(new ThreadStart(this.FTPProcess));
            this.CounterThread = new Thread(new ThreadStart(this.CounterProcess));
        }

        /// <summary>
        /// Lance le processus principale gérant les sous processus
        /// </summary>
        public void Start()
        {
            this.CanRun = true;
            this.MainProcessThread.Start();
        }

        /// <summary>
        /// Arrête tous les processus
        /// </summary>
        public void Stop()
        {
            this.CanRun = false;
        }

        /// <summary>
        /// Thread principal
        /// </summary>
        private void LaunchProcess()
        {
            StatusWriter writer = new StatusWriter(StatusFilePath); // Chargement du fichiers HTML des status

            /* Lancement des threads */
            TeamspeakThread.Start();
            VanillaThread.Start();
            FTBThread.Start();
            FTPThread.Start();
            CounterThread.Start();

            /* Boucle infini */
            while (this.CanRun)
            {
                System.Threading.Thread.Sleep(Timer * 1000); // On sleep le thread avec le temps donné
                Debug.WriteLine("Ecriture dans le html");

                bool vanilla = VanillaState, ftb = FTBState, ts = TeamspeakState, ftp = FTPState, csgo = CounterStrikeState;
                string map = CounterStrikeMap;

                writer.WriteStatus(vanilla, ftb, ts, ftp, csgo, map);
            }
        }

        private void TeamSpeakProcess()
        {
            TeamspeakPinger teamspeak = new TeamspeakPinger(ServerAdress, 10011, "Teamspeak");
            do
            {
                TeamspeakState = teamspeak.PingAndGetClientsAndAverragePing();
                TeamspeakClientsConnected = teamspeak.ClientsConnected;
                TeamspeakAverragePing = teamspeak.AverragePing;
                TeamspeakPacketLoss = teamspeak.PacketLoss;

                if(CanRun)
                    Form.SetTeamSpeakState(TeamspeakState, TeamspeakClientsConnected, TeamspeakAverragePing, TeamspeakPacketLoss);

                Debug.WriteLine("TS FINIS : " + TeamspeakState + " clients : " + TeamspeakClientsConnected);
                Thread.Sleep(TeamspeakTimer * 1000);
            } while (CanRun);
        }

        private void VanillaProcess()
        {
            MinecraftPinger vanilla = new MinecraftPinger(ServerAdress, 25566, "Minecraft Vanilla");
            do
            {
                VanillaState = vanilla.Ping();

                if(CanRun)
                    Form.SetVanillaState(VanillaState);

                Debug.WriteLine("VANILLA FINIS : " + VanillaState);
                Thread.Sleep(VanillaTimer * 1000);
            } while (CanRun);
        }

        private void FTBProcess()
        {
            MinecraftPinger feedthebeast = new MinecraftPinger(ServerAdress, 25565, "Minecraft Feed The Beast");
            do
            {
                FTBState = feedthebeast.Ping();

                if(CanRun)
                    Form.SetFTBState(FTBState);

                Debug.WriteLine("FTB FINIS : " + FTBState);
                Thread.Sleep(FTBTimer * 1000);
            } while (CanRun);
        }
         
        private void FTPProcess()
        {
            FTPPinger ftp = new FTPPinger(ServerAdress, 21, "FTP");
            do
            {
                FTPState = ftp.CheckProcess();

                if(CanRun)
                    Form.SetFTPState(FTPState);

                Debug.WriteLine("FTP FINIS : " + FTPState);
                Thread.Sleep(FTPTimer * 1000);
            } while (CanRun);
        }

        private void CounterProcess()
        {
            CSStatus status;
            CounterStrikePinger counterstrike = new CounterStrikePinger(FTPAdress, 27015, "Counter-Strike Global-Offensive");

            do
            {
                status = counterstrike.PingWithMap();

                CounterStrikeState = status.Status;
                CounterStrikeMap = status.Map;
                CounterStrikePlayersConnected = status.PlayersConnected;
                CounterStrikePlayersMax = status.PlayersMax;

                if(CanRun)
                    Form.SetCounterStrikeState(status.Status, status.Map, status.PlayersConnected, status.PlayersMax);

                Debug.WriteLine("CS FINIS : " + status.Status + " map : " + status.Map + " max : " + status.PlayersMax);
                Thread.Sleep(CounterStrikeTimer * 1000);
            } while (CanRun);
        }
    }
}
