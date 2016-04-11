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
            tw = new StreamWriter(File.Open(this.FilePath, FileMode.OpenOrCreate)); // Ouverture du fichier en écriture
            
            
            /* Génération du fichier HTML */
            tw.WriteLine
                ("<pcolor:#000000; font-weight:bold;style=\"font-family:Lucida Console;\"><span style=\"color:#A8A8A8\">Minecraft Vanilla :&nbsp;&nbsp;"
                    + this.BoolToState(vanilla) 
                + "<br>" 
                    + "<span style=\"color:#A8A8A8\">Feed The Beast :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp"
                        + this.BoolToState(feedthebeast) 
                + "<br>" 
                    + "<span style=\"color:#A8A8A8\">Teamspeak : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                        + this.BoolToState(teamspeak) 
                + "<br>" 
                    + "<span style=\"color:#A8A8A8\">FTP : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                        + this.BoolToState(ftp) 
                + "<br>" 
                    + "<span style=\"color:#A8A8A8\">Counter-Strike : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                        + this.BoolToState(csgo) 
                + "</span><br><span style=\"color:#A8A8A8\">Map CSGO : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    + this.MapToState(map) 
                        + "</span></p>");

            /* Fermeture du flux */
            tw.Flush();
            tw.Close();
        }

        /// <summary>
        /// Convertis l'état du serveur en valeur a afficher dans le fichier html (avec le span color)
        /// </summary>
        /// <param name="value">Etat du serveur</param>
        /// <returns>Retourne le string a afficher dans le html</returns>
        private string BoolToState(bool value)
        {
            return value ? "<span style=\"color:#32CD32\">Online" : "<span style=\"color:#FF0000\">Offline";
        }

        /// <summary>
        /// Convertis le nom d'une map en valeur a afficher dans le fichier html (avec le span color)
        /// </summary>
        /// <param name="map">Nom de la map</param>
        /// <returns>Retourne le string a afficher dans le html</returns>
        private string MapToState(string map)
        {
            return map.ToLower().Equals("none") ? "<span style =\"color:#FF0000\">Aucune" : "<span style =\"color:#32CD32\">" + map;
        }
    }
}
