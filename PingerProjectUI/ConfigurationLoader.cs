using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerProjectUI
{
    class ConfigurationLoader
    {

        public static bool GetIfCheckFtpByProcess()
        {
            return bool.Parse(ConfigurationManager.AppSettings["CheckFTPbyProcess"]);
        }

        public static string GetServerAdress()
        {
            return ConfigurationManager.AppSettings["ServerIpDns"]; 
        }

        public static string GetServerIpV4()
        {
            return ConfigurationManager.AppSettings["ServerIpV4"];
        }

        public static string GetStatusFilePath()
        {
            return ConfigurationManager.AppSettings["StatusFilePath"];
        }

        public static int GetRewriteHTMLTimer()
        {
            return int.Parse(ConfigurationManager.AppSettings["RewriteHTMLTimer"]);
        }

        public static int GetTeamspeakTimer()
        {
            return int.Parse(ConfigurationManager.AppSettings["TeamspeakTimer"]);
        }

        public static int GetVanillaTimer()
        {
            return int.Parse(ConfigurationManager.AppSettings["VanillaTimer"]);
        }

        public static int GetFeedTheBeastTimer()
        {
            return int.Parse(ConfigurationManager.AppSettings["FeedTheBeastTimer"]);
        }

        public static int GetFTPTimer()
        {
            return int.Parse(ConfigurationManager.AppSettings["FTPTimer"]);
        }

        public static int GetCounterStrikeTimer()
        {
            return int.Parse(ConfigurationManager.AppSettings["CounterStrikeTimer"]);
        }

        public static int GetReceiveTimeoutTimer()
        {
            return int.Parse(ConfigurationManager.AppSettings["ReceiveTimeoutTCPTimer"]);
        }

        public static bool GetIfNeedToAlert()
        {
            return bool.Parse(ConfigurationManager.AppSettings["AlertByMailWhenTSDown"]);
        }

        public static bool GetIfNeedToGenerateStatsTS()
        {
            return bool.Parse(ConfigurationManager.AppSettings["GenerateTeamspeakStatsFile"]);
        }
    }
}
