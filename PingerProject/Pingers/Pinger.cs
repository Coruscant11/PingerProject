using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerProject
{
    abstract class Pinger
    {
        protected string ServerIP, ServerType;
        protected uint Port;

        public Pinger(string server, uint port, string servertype)
        {
            this.ServerIP = server;
            this.ServerType = servertype;
            this.Port = port;
        }

        public abstract bool Ping();
    }
}
