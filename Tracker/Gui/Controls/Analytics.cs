using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tracker.Data;
using YellowbrickV8.Entities;
using YellowbrickV8;

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
                foreach (Team td in Holder.race.teams.OrderBy(item => item.name))
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
                IEnumerable<Team> teams = (from t in Holder.race.teams
                                           where t as Team != null && checkedTeams.Contains(t.id)
                                           select t as Team).ToList();
                foreach (Team td in teams.OrderBy(item => item.LatestMoment().dtfMeters))
                {
                    scores.Add(new ScoringItem(positionCount, td.id));
                    positionCount++;
                }
            }
            this.dataGridView1.DataSource = scores.ToArray();
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.MyTeam = (this.comboBox1.SelectedItem as Team).id;
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
            get { return UnitTools.M2Nm(Holder.race.teams.Single(t => t.id == this.teamId).LatestMoment().dtfMeters).ToString("F2"); }
        }

        public String ToGoDifference
        {
            get 
            {
                if (Holder.race.teams.Any(t => t.id == Tracker.Properties.Settings.Default.MyTeam))
                {
                    Moment mine = Holder.race.teams.Single(t => t.id == Tracker.Properties.Settings.Default.MyTeam).LatestMoment();
                    Moment theirs = Holder.race.teams.Single(t => t.id == this.teamId).LatestMoment();

                    return UnitTools.M2Nm(mine.dtfMeters - theirs.dtfMeters).ToString("F2");
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
                    Moment mine = Holder.race.teams.Single(t => t.id == Tracker.Properties.Settings.Default.MyTeam).LatestMoment();
                    Moment theirs = Holder.race.teams.Single(t => t.id == this.teamId).LatestMoment();

                    return YellowbrickV8.CoordinateTools.HaversineDistanceNauticalMiles(mine.lat, mine.lon, theirs.lat, theirs.lon).ToString("F2");
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
                    Moment mine = Holder.race.teams.Single(t => t.id == Tracker.Properties.Settings.Default.MyTeam).LatestMoment();
                    Moment theirs = Holder.race.teams.Single(t => t.id == this.teamId).LatestMoment();

                    return YellowbrickV8.CoordinateTools.HaversineHeadingDegrees(mine.lat, mine.lon, theirs.lat, theirs.lon).ToString("F0");
                }
                return "NaN";
            }
        }
    }
}
