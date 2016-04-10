using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace PingerProject
{
    class FTPPinger : Pinger
    {
        public FTPPinger(string server, uint port, string servertype) : base(server, port, servertype)
        {

        }

        public override bool Ping()
        {
            Console.WriteLine("Connexion au serveur " + this.ServerType + "...");
            FtpWebRequest ftp = (FtpWebRequest) WebRequest.Create(this.ServerIP);
            ftp.Credentials = new NetworkCredential("coruscant", "licorne");
            
            try
            {
                ftp.Method = WebRequestMethods.Ftp.GetFileSize;
                try
                {
                    FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                    return true;
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
