using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingerProjectUI
{
    public partial class MainForm : Form
    {

        public string StatusTripText {
            get { return this.statusStripLabel.Text; }
            set { this.statusStripLabel.Text = value; }
        }

        private MainProcess Process;

        public MainForm()
        {
            InitializeComponent();
            this.ConfigurePicturesBox();

            Process = new MainProcess(this);
            StartProcess();
        }

        private void StartProcess()
        {
            Process.Start();
        }

        private void ConfigurePicturesBox()
        {
            this.TeamspeakStatePic.SizeMode = PictureBoxSizeMode.Zoom;
            this.VanillaStatePic.SizeMode = PictureBoxSizeMode.Zoom;
            this.FeedTheBeastStatePic.SizeMode = PictureBoxSizeMode.Zoom;
            this.FTPStatePic.SizeMode = PictureBoxSizeMode.Zoom;
            this.CounterStatePic.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Stop();
            this.Close();
        }

        private void ouvrirLeFichierDeConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void SetTeamSpeakState(bool state, int cliensConnected, double averragePing, double packetlosss)
        {
            if(state)
            {
                TeamspeakStateLabel.BeginInvoke(new Action(() =>
                {
                    TeamspeakStateLabel.Text = "Online";
                    TeamspeakStateLabel.ForeColor = Color.LimeGreen;
                }));
                ClientsTSLabel.BeginInvoke(new Action(() =>
                {
                    ClientsTSLabel.Text = cliensConnected.ToString();
                    ClientsTSLabel.ForeColor = Color.LimeGreen;
                }));
                TeamspeakStatePic.BeginInvoke(new Action(() =>
                {
                    TeamspeakStatePic.Image = Properties.Resources.Online;
                    TeamspeakStatePic.BackColor = System.Drawing.SystemColors.Control;
                }));
                PingMoyenLabel.BeginInvoke(new Action(() =>
                {
                    PingMoyenLabel.Text = ((int) averragePing).ToString();
                    PingMoyenLabel.ForeColor = Color.LimeGreen;
                }));
                PacketLossLabel.BeginInvoke(new Action(() =>
                {
                    PacketLossLabel.Text = packetlosss.ToString() + " %";
                    PacketLossLabel.ForeColor = Color.LimeGreen;
                }));
            }
            else
            {
                TeamspeakStateLabel.BeginInvoke(new Action(() =>
                {
                    TeamspeakStateLabel.Text = "Offline";
                    TeamspeakStateLabel.ForeColor = Color.Red;
                }));
                ClientsTSLabel.BeginInvoke(new Action(() =>
                {
                    ClientsTSLabel.Text = cliensConnected.ToString();
                    ClientsTSLabel.ForeColor = Color.Red;
                }));
                TeamspeakStatePic.BeginInvoke(new Action(() =>
                {
                    TeamspeakStatePic.Image = Properties.Resources.Offline;
                    TeamspeakStatePic.BackColor = Color.Red;
                }));
                PingMoyenLabel.BeginInvoke(new Action(() =>
                {
                    PingMoyenLabel.Text = "0";
                    PingMoyenLabel.ForeColor = Color.Red;
                }));
                PacketLossLabel.BeginInvoke(new Action(() =>
                {
                    PacketLossLabel.Text = "0,00 %";
                    PacketLossLabel.ForeColor = Color.Red;
                }));
            }
        }

        public void SetVanillaState(bool state)
        {
            if(state)
            {
                VanillaStatePic.BeginInvoke(new Action(() =>
                {
                    VanillaStatePic.Image = Properties.Resources.Online;
                    VanillaStatePic.BackColor = System.Drawing.SystemColors.Control;
                }));
            }
            else
            {
                VanillaStatePic.BeginInvoke(new Action(() =>
                {
                    VanillaStatePic.Image = Properties.Resources.Offline;
                    VanillaStatePic.BackColor = Color.Red;
                }));
            }
        }

        public void SetFTBState(bool state)
        {
            if (state)
            {
                FeedTheBeastStatePic.BeginInvoke(new Action(() =>
                {
                    FeedTheBeastStatePic.Image = Properties.Resources.Online;
                    FeedTheBeastStatePic.BackColor = System.Drawing.SystemColors.Control;
                }));
            }
            else
            {
                FeedTheBeastStatePic.BeginInvoke(new Action(() =>
                {
                    FeedTheBeastStatePic.Image = Properties.Resources.Offline;
                    FeedTheBeastStatePic.BackColor = Color.Red;
                }));
            }
        }

        public void SetFTPState(bool state)
        {
            if(state)
            {
                FTPStateLabel.BeginInvoke(new Action(() =>
                {
                    FTPStateLabel.Text = "Online";
                    FTPStateLabel.ForeColor = Color.LimeGreen;
                }));
                FTPStatePic.BeginInvoke(new Action(() =>
                {
                    FTPStatePic.Image = Properties.Resources.Online;
                    FTPStatePic.BackColor = System.Drawing.SystemColors.Control;
                }));
            }
            else
            {
                FTPStateLabel.BeginInvoke(new Action(() =>
                {
                    FTPStateLabel.Text = "Offline";
                    FTPStateLabel.ForeColor = Color.Red;
                }));
                FTPStatePic.BeginInvoke(new Action(() =>
                {
                    FTPStatePic.Image = Properties.Resources.Offline;
                    FTPStatePic.BackColor = Color.Red;
                }));
            }
        }

        public void SetCounterStrikeState(bool state, string map, int players, int slots)
        {
            if(state)
            {
                CounterStatePic.BeginInvoke(new Action(() =>
                {
                    CounterStatePic.Image = Properties.Resources.Online;
                    CounterStatePic.BackColor = System.Drawing.SystemColors.Control;
                }));
                CounterStateLabel.BeginInvoke(new Action(() =>
                {
                    CounterStateLabel.Text = "Online";
                    CounterStateLabel.ForeColor = Color.LimeGreen;
                }));
                MapCSLabel.BeginInvoke(new Action(() =>
                {
                    MapCSLabel.Text = map;
                    MapCSLabel.ForeColor = Color.LimeGreen;
                }));
                PlayerCSLabel.BeginInvoke(new Action(() =>
                {
                    PlayerCSLabel.Text = players.ToString();
                    PlayerCSLabel.ForeColor = Color.LimeGreen;
                }));
                PlayersMaxLabel.BeginInvoke(new Action(() =>
                {
                    PlayersMaxLabel.Text = slots.ToString();
                    PlayersMaxLabel.ForeColor = Color.LimeGreen;
                }));
            }
            else
            {
                CounterStatePic.BeginInvoke(new Action(() =>
                {
                    CounterStatePic.Image = Properties.Resources.Offline;
                    CounterStatePic.BackColor = Color.Red;
                }));
                CounterStateLabel.BeginInvoke(new Action(() =>
                {
                    CounterStateLabel.Text = "Offline";
                    CounterStateLabel.ForeColor = Color.Red;
                }));
                MapCSLabel.BeginInvoke(new Action(() =>
                {
                    MapCSLabel.Text = "Aucune";
                    MapCSLabel.ForeColor = Color.Red;
                }));
                PlayerCSLabel.BeginInvoke(new Action(() =>
                {
                    PlayerCSLabel.Text = players.ToString();
                    PlayerCSLabel.ForeColor = Color.Red;
                }));
                PlayersMaxLabel.BeginInvoke(new Action(() =>
                {
                    PlayersMaxLabel.Text = slots.ToString();
                    PlayersMaxLabel.ForeColor = Color.Red;
                }));
            }
        }

        private void FormClosingEvent(object sender, FormClosingEventArgs e)
        {
            Process.Stop();
        }
    }
}
