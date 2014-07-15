using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Tracker.Data;
using System.IO;
using System.Net;
using System.Net.Cache;
using Tracker.Gui.Controls;

namespace Tracker
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class MainForm : Form
    {
        #region attributes
        /// <summary>
        /// 
        /// </summary>
        internal string WorkingDirectory;
        /// <summary>
        /// 
        /// </summary>
        internal BackgroundWorker UpdaterWorker = new BackgroundWorker();
        /// <summary>
        /// 
        /// </summary>
        internal BackgroundWorker SavingWorker = new BackgroundWorker();
        /// <summary>
        /// 
        /// </summary>
        int updateResult = 0;

        #endregion

        #region constructors
        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this.Text = "Yellowbrick Data Reader " + Application.ProductVersion.ToString();

            this.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.WorkingDirectory += "\\tracker-yellowbrick\\" + Tracker.Properties.Settings.Default.RaceKey;
            DirectoryInfo info = new DirectoryInfo(this.WorkingDirectory);
            if (info.Exists == false)
                Directory.CreateDirectory(this.WorkingDirectory);

            Presenter.UpdatingEvent += new Presenter.Updating(SetStatusThreadSafe);
            this.UpdaterWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(UpdaterWorer_RunWorkerCompleted);
            this.UpdaterWorker.DoWork += new DoWorkEventHandler(UpdaterWorker_DoWork);
            this.SavingWorker.DoWork += new DoWorkEventHandler(SavingWorker_DoWork);
            this.analytics1.MyBoatChangedEvent += new Analytics.MyBoatSelectionChanged(analytics1_MyBoatChangedEvent);
            this.chartPositions1.MySelectionChangedEvent += new ChartPositions.MyBoatSelectionChanged(chartPositions1_MySelectionChangedEvent);
            this.emailPasting1.ChangeEventHandlerEvent += new EmailPasting.ChangedEventHandler(emailPasting1_ChangeEventHandlerEvent);

            int result = Presenter.LoadData(this.WorkingDirectory, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\tracker-yellowbrick\\");

            if (result == -1)
                this.UpdaterWorker.RunWorkerAsync(true);
            else if (result == 0)
            {
                this.chartPositions1.UpdateDisplay();
                this.analytics1.UpdateDisplay();
                this.SetStatusThreadSafe("Ready");
            }
        }


        #endregion

        #region async workers
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SavingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Presenter.SaveData(this.WorkingDirectory + @"\data.xml");
            Tracker.Properties.Settings.Default.Save();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UpdaterWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool useAllPositions = false;
            if (e.Argument != null && e.Argument is bool)
                useAllPositions = (bool)e.Argument;

            int result = 0;
            if (Holder.race == null)
                result += Presenter.updateRace(Tracker.Properties.Settings.Default.ServerName,
                Tracker.Properties.Settings.Default.RaceKey);
            
            if(useAllPositions)
                result += Presenter.updateAllPositions(Tracker.Properties.Settings.Default.ServerName,
                Tracker.Properties.Settings.Default.RaceKey);
            else
                result += Presenter.updateLatestPositions(Tracker.Properties.Settings.Default.ServerName,
                Tracker.Properties.Settings.Default.RaceKey);

            this.updateResult = result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UpdaterWorer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (this.updateResult == 0)
            {
                this.SetLastUpdateTextThreadSafe("Last Updated on " + DateTime.Now.ToString());
                this.SetStatusThreadSafe("Ready");
                this.chartPositions1.UpdateDisplay();
                this.analytics1.UpdateDisplay();

                this.SavingWorker.RunWorkerAsync();
            }
            else
            {
                if (Holder.race == null && Holder.race.course == null && Holder.race.teams == null)
                    this.SetStatusThreadSafe("Error : No data can be downloaded, check your connexion and the root server adress");
                else
                    this.SetStatusThreadSafe("Error : Faild to update, see messages log for errors");
            }
        }
        #endregion

        #region event methods
        /// <summary>
        /// 
        /// </summary>
        void chartPositions1_MySelectionChangedEvent()
        {
            this.analytics1.UpdateDisplay();
        }
        /// <summary>
        /// 
        /// </summary>
        void analytics1_MyBoatChangedEvent()
        {
            this.chartPositions1.UpdateDisplay();
        }

        void  emailPasting1_ChangeEventHandlerEvent()
        {
            this.chartPositions1.UpdateDisplay();
            this.analytics1.UpdateDisplay();
            this.SavingWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private delegate void UpdateStripText(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SetStatusThreadSafe(string message)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateStripText(this.SetStatusThreadSafe), message);
            else
            {
                if (message.Contains("Error"))
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                else
                    this.toolStripStatusLabel1.ForeColor = Color.Black;
                this.toolStripStatusLabel1.Text = message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private delegate void LastUpdateDownText(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SetLastUpdateTextThreadSafe(string message)
        {
            if (this.InvokeRequired)
                this.Invoke(new LastUpdateDownText(this.SetLastUpdateTextThreadSafe), message);
            else
            {
                this.toolStripStatusLabel3.Text = message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form paramsForm = new Form();
            Parameters ctrlP = new Parameters();
            paramsForm.Size = new Size(ctrlP.MinimumSize.Width + 20, ctrlP.MinimumSize.Height + 40);
            ctrlP.Dock = DockStyle.Fill;
            paramsForm.Controls.Add(ctrlP);
            paramsForm.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Presenter.SaveData(this.WorkingDirectory + @"/data.xml");
            Tracker.Properties.Settings.Default.Save();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.SavingWorker.RunWorkerAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void messagesLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form messagesForm = new Form();
            MessagesViewer mViewer = new MessagesViewer();
            mViewer.Init(Presenter.messages);
            messagesForm.Size = new Size(mViewer.MinimumSize.Width + 20, mViewer.MinimumSize.Height + 40);
            mViewer.Dock = DockStyle.Fill;
            messagesForm.Controls.Add(mViewer);
            messagesForm.AutoSize = true;
            messagesForm.Show();
        }
        #endregion

        private void updateRaceInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(0 == Presenter.updateRace(Tracker.Properties.Settings.Default.ServerName
                ,Tracker.Properties.Settings.Default.RaceKey))
            {
                this.chartPositions1.UpdateDisplay();
                this.analytics1.UpdateDisplay();
                this.SavingWorker.RunWorkerAsync();
                MessageBox.Show("Race Updated!");
            }
            else
            {
                MessageBox.Show("Something went wrong when updating the race");
            }
        }

        private void getAllPositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 == Presenter.updateAllPositions(Tracker.Properties.Settings.Default.ServerName,
                Tracker.Properties.Settings.Default.RaceKey))
            {
                this.chartPositions1.UpdateDisplay();
                this.analytics1.UpdateDisplay();
                this.SavingWorker.RunWorkerAsync();
                MessageBox.Show("All Positions Updated!");
            }
            else
            {
                MessageBox.Show("Something went wrong when updating All Positions");
            }
        }

        private void getLatestPositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 == Presenter.updateLatestPositions(Tracker.Properties.Settings.Default.ServerName,
                Tracker.Properties.Settings.Default.RaceKey))
            {
                this.chartPositions1.UpdateDisplay();
                this.analytics1.UpdateDisplay();
                this.SavingWorker.RunWorkerAsync();
                MessageBox.Show(" Latest Positions Updated!");
            }
            else
            {
                MessageBox.Show("Something went wrong when updating Latest Positions");
            }
        }
    }
}
