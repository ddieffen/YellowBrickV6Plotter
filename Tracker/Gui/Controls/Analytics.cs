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

namespace Tracker.Gui.Controls
{
    public partial class Analytics : UserControl
    {
        #region delegates
        public delegate void MyBoatSelectionChanged();
        public event MyBoatSelectionChanged MyBoatChangedEvent;
        #endregion

        #region constructors
        public Analytics()
        {
            InitializeComponent();
        }
        #endregion

        #region methods
        private void InitializeCombo()
        {
            if(Holder.race != null && Holder.race.teams != null)
            {
                foreach (TeamData td in Holder.race.teams.OrderBy(item => item.name))
                {
                    this.comboBox1.Items.Add(td);
                }

                this.comboBox1.SelectedIndexChanged -= new EventHandler(comboBox1_SelectedIndexChanged);

                if(Tracker.Properties.Settings.Default.MyTeam != -1 && Holder.race.teams.Any(t => t.id == Tracker.Properties.Settings.Default.MyTeam))
                    this.comboBox1.SelectedItem = Holder.race.teams.Single(t => t.id == Tracker.Properties.Settings.Default.MyTeam);

                this.comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            }
        }

        private void InitializeScores()
        {
            List<ScoringItem> scores = new List<ScoringItem>();

            List<int> checkedTeams = new List<int>();
            foreach (string str in Holder.teamsSelected.Split(','))
            {
                if (str != "")
                    checkedTeams.Add(Convert.ToInt32(str));
            }

            
            int positionCount = 1;
            if(Holder.race != null && Holder.race.teams != null)
            {
                IEnumerable<TeamData> teams = (from t in Holder.race.teams
                                           where t as TeamData != null && checkedTeams.Contains(t.id)
                                           select t as TeamData).ToList();
                foreach (TeamData td in teams.OrderBy(item => item.LatestMoment.dtf))
                {
                    scores.Add(new ScoringItem(positionCount, td.id));
                    positionCount++;
                }
            }
            this.dataGridView1.DataSource = scores.ToArray();
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.MyTeam = (this.comboBox1.SelectedItem as TeamData).id;
            this.UpdateDisplay();
            if (this.MyBoatChangedEvent != null)
                this.MyBoatChangedEvent();
            Tracker.Properties.Settings.Default.Save();
        }
        internal void UpdateDisplay()
        {
            this.InitializeCombo();
            this.InitializeScores();
        }
        #endregion

       
    }


    public class ScoringItem
    {
        private int position = -1;
        private int teamId = -1;

        public ScoringItem(int position, int teamId)
        {
            this.position = position;
            this.teamId = teamId;
        }

        public int Position 
        {
            get { return this.position; }
        }

        public String BoatName
        {
            get { return Holder.race.teams.Single(t => t.id == this.teamId).name; }
        }

        public String DistanceToGo
        {
            get { return (Holder.race.teams.Single(t => t.id == this.teamId) as TeamData).LatestMoment.dtf.ToString("F2"); }
        }

        public String ToGoDifference
        {
            get 
            {
                if (Holder.race.teams.Any(t => t.id == Tracker.Properties.Settings.Default.MyTeam))
                {
                    Moment mine = (Holder.race.teams.Single(t => t.id == Tracker.Properties.Settings.Default.MyTeam) as TeamData).LatestMoment;
                    Moment theirs = (Holder.race.teams.Single(t => t.id == this.teamId) as TeamData).LatestMoment;

                    return (mine.dtf - theirs.dtf).ToString("F2");
                }
                return "NaN";
            }
        }

        public string Distance
        {
            get
            {
                if (Holder.race.teams.Any(t => t.id == Tracker.Properties.Settings.Default.MyTeam))
                {
                    Moment mine = (Holder.race.teams.Single(t => t.id == Tracker.Properties.Settings.Default.MyTeam) as TeamData).LatestMoment;
                    Moment theirs = (Holder.race.teams.Single(t => t.id == this.teamId) as TeamData).LatestMoment;

                    return YellowbrickV6.CoordinateTools.HaversineDistanceNauticalMiles(mine.lat, mine.lon, theirs.lat, theirs.lon).ToString("F2");
                }
                return "NaN";
            }
        }

        public string BearingT
        {
            get
            {
                if (Holder.race.teams.Any(t => t.id == Tracker.Properties.Settings.Default.MyTeam))
                {
                    Moment mine = (Holder.race.teams.Single(t => t.id == Tracker.Properties.Settings.Default.MyTeam) as TeamData).LatestMoment;
                    Moment theirs = (Holder.race.teams.Single(t => t.id == this.teamId) as TeamData).LatestMoment;

                    return YellowbrickV6.CoordinateTools.HaversineHeadingDegrees(mine.lat, mine.lon, theirs.lat, theirs.lon).ToString("F0");
                }
                return "NaN";
            }
        }
    }
}
