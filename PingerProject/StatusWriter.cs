using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PingerProject
{
    /// <summary>
    ///  Description complète de la classe. Généralement on donne la fonction de cette classe ainsi que ces particularités
    /// </summary>
    class StatusWriter
    {
        private string FilePath;
        private StreamWriter tw;

        /// <summary>
        ///  Constructeur récupérant l'url du fichier
        /// </summary>
        /// <param name="path">Chemin du fichier</param>
        public StatusWriter(string path)
        {
            this.FilePath = path;
        }

        /// <summary>
        ///  Ecris les valeurs envoyées en paramètres
        /// </summary>
        /// <param name="vanilla">Etat du serveur Vanilla</param>
        /// <param name="feedthebeast">Etat du serveur FTB</param>
        /// <param name="teamspeak">Etat du serveur TS</param>
        /// <param name="ftp">Etat du serveur FTP</param>
        /// <param name="csgo">Etat du serveur CSGO</param>
        /// <param name="map">Map disponible sur le serveur CSGO</param>
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
