using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PingerProject
{
    class StatusWriter
    {
        private string FilePath;
        private StreamWriter tw;

        public StatusWriter(string path)
        {
            this.FilePath = path;
        }

        public void WriteStatus(bool vanilla, bool feedthebeast, bool teamspeak, bool ftp, bool csgo, string map)
        {
            tw = new StreamWriter(File.OpenWrite(this.FilePath));

            tw.WriteLine(vanilla);
            tw.WriteLine(feedthebeast);
            tw.WriteLine(teamspeak);
            tw.WriteLine(ftp);
            tw.WriteLine(csgo);
            tw.WriteLine(map);

            tw.Flush();
            tw.Close();
        }
    }
}
