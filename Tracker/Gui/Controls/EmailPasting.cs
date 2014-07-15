using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tracker.Data;
using YellowbrickV6.Entities;
using YellowbrickV6;

namespace Tracker.Gui.Controls
{
    public partial class EmailPasting : UserControl
    {
        #region delegates
        public delegate void ChangedEventHandler();
        public event ChangedEventHandler ChangeEventHandlerEvent;
        #endregion

        #region constructors
        public EmailPasting()
        {
            InitializeComponent();
        }
        #endregion

        #region event methods
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string result = "Uknown";
            if (this.tryFormatV1(this.textBox1.Text))
                result = "V1 Format VALID";

            this.labelIsValid.Text = result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Team> teams = new List<Team>();
            if (this.tryFormatV1(this.textBox1.Text))
                teams = this.updateFromV1(this.textBox1.Text);

            this.FindTeamIDs(Holder.race.teams, teams);
            YBTracker.UpdateTeamsMoments(Holder.race.teams, teams);
            foreach (Team team in Holder.race.teams)
                YBTracker.UpdateMomentsSpeedHeading(team);

            this.labelIsValid.Text = "Data Parsed and Updated";

            if (ChangeEventHandlerEvent != null)
                this.ChangeEventHandlerEvent();
        }
        #endregion

        #region private methods
        /// <summary>
        /// Find Team IDs from their Names
        /// </summary>
        /// <param name="list"></param>
        /// <param name="teams"></param>
        private void FindTeamIDs(List<Team> list, List<Team> teams)
        {
            foreach (Team team in teams)
            {
                if (list.Any(t => t.name.Contains(team.name)))
                {
                    Team teamData = list.Single(t => t.name.Contains(team.name));
                    team.id = teamData.id;
                }
            }
        }

        /// <summary>
        /// Creates a list of Team instances and Moment instances from a copied and pasted email
        /// </summary>
        /// <param name="p">String containing the content of the email</param>
        /// <returns>List of Teams extracted from the email</returns>
        private List<Team> updateFromV1(string p)
        {
            List<Team> teams = new List<Team>();
            try
            {
                foreach (string myString in p.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] rowSplit = myString.Split(new string[] { "\t" }, StringSplitOptions.None);
                    {
                        if (rowSplit.Length == 10)
                        {
                            string position = rowSplit[0];
                            string name = rowSplit[1].TrimEnd().TrimStart('>');
                            string speed = rowSplit[2].Trim(" kn".ToCharArray());
                            string dtf = rowSplit[3].Trim(" nm".ToCharArray());
                            string reltiveDTF = rowSplit[4].Trim(" nm".ToCharArray());
                            string relativeAngle = rowSplit[5];
                            string relativeDst = rowSplit[6].Trim(" nm".ToCharArray());
                            string lat = rowSplit[7];
                            string lon = rowSplit[8];
                            string dateAt = rowSplit[9];

                            int tempPos;
                            double tempSpd;
                            double tempDtf;
                            double tempRDtf;
                            double tempRAng;
                            double tempRDst;
                            double tempLat;
                            double tempLon;
                            DateTime tempDate;

                            if (Int32.TryParse(position, out tempPos)
                                && Double.TryParse(speed, out tempSpd)
                                && Double.TryParse(dtf, out tempDtf)
                                && Double.TryParse(reltiveDTF, out tempRDtf)
                                && Double.TryParse(relativeAngle, out tempRAng)
                                && Double.TryParse(relativeDst, out tempRDst)
                                && Double.TryParse(lat, out tempLat)
                                && Double.TryParse(lon, out tempLon)
                                && DateTime.TryParse(dateAt, out tempDate)) 
                            {
                                Team team = new Team();
                                team.name = name;

                                Moment m = new Moment();
                                m.at = (uint)TimeTools.DateTimeToUnixTime(tempDate.ToUniversalTime());
                                m.lat = tempLat;
                                m.lon = tempLon;
                                team.moments.Add(m);

                                teams.Add(team);
                            }
                        }
                    }
                }
            }
            catch { }
            return teams;
        }

        /// <summary>
        /// Test if the string can be parsed with the parser V1
        /// </summary>
        /// <param name="p">String containing the content of the email</param>
        /// <returns></returns>
        private bool tryFormatV1(string p)
        {
               int detectedTab10 = 0;
            int rowsWithDataOK = 0;

            try
            {
                foreach (string myString in p.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] rowSplit = myString.Split(new string[] { "\t" }, StringSplitOptions.None);
                    {
                        if (rowSplit.Length == 10)
                        {
                            detectedTab10++;

                            string position = rowSplit[0];
                            string name = rowSplit[1];
                            string speed = rowSplit[2].Trim(" kn".ToCharArray());
                            string dtf = rowSplit[3].Trim(" nm".ToCharArray());
                            string reltiveDTF = rowSplit[4].Trim(" nm".ToCharArray());
                            string relativeAngle = rowSplit[5];
                            string relativeDst = rowSplit[6].Trim(" nm".ToCharArray());
                            string lat = rowSplit[7];
                            string lon = rowSplit[8];
                            string dateAt = rowSplit[9];

                            int tempI;
                            double tempD;
                            DateTime tempDate;

                            if (Int32.TryParse(position, out tempI)
                                && Double.TryParse(speed, out tempD)
                                && Double.TryParse(dtf, out tempD)
                                && Double.TryParse(reltiveDTF, out tempD)
                                && Double.TryParse(relativeAngle, out tempD)
                                && Double.TryParse(relativeDst, out tempD)
                                && Double.TryParse(lat, out tempD)
                                && Double.TryParse(lon, out tempD)
                                && DateTime.TryParse(dateAt, out tempDate))
                                rowsWithDataOK++;
                        }
                    }
                }
            }
            catch 
            { 
            
            }

            return rowsWithDataOK + 1 == detectedTab10;
        }
        #endregion
    }
}
