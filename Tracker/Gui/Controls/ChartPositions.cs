using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tracker.Data;
using ZedGraph;
using YellowbrickV8.Entities;
using YellowbrickV8;

namespace Tracker.Gui.Controls
{
    public partial class ChartPositions : UserControl
    {
        #region delegates
        public delegate void MyBoatSelectionChanged();
        public event MyBoatSelectionChanged MySelectionChangedEvent;
        #endregion

        #region constructors
        public ChartPositions()
        {
            InitializeComponent();

            GraphPane myPane = this.zedGraphControl1.GraphPane;

            // Set the titles
            myPane.Title.IsVisible = false;// = "Teams positions";
            myPane.XAxis.Title.IsVisible = false; //.Text = "Longitude";
            myPane.YAxis.Title.IsVisible = false; //.Text = "Latitude";
            myPane.Legend.IsVisible = false;

            this.zedGraphControl1.IsShowPointValues = true;


            this.zedGraphControl1.ZoomEvent += new ZedGraphControl.ZoomEventHandler(zedGraphControl1_ZoomEvent);
        }

        void zedGraphControl1_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            this.SyncAxis();
            
        }

        public void SyncAxis()
        {
            GraphPane pane = this.zedGraphControl1.GraphPane;
            double centerY = (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) / 2 + pane.YAxis.Scale.Min;
            double centerX = (pane.XAxis.Scale.Max - pane.XAxis.Scale.Min) / 2 + pane.XAxis.Scale.Min;

            double yPixPerUnit = pane.Chart.Rect.Height / (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min);
            double newUnitSpanForX = pane.Chart.Rect.Width / yPixPerUnit;
            pane.XAxis.Scale.Min = centerX - newUnitSpanForX / 2;
            pane.XAxis.Scale.Max = centerX + newUnitSpanForX / 2;
            pane.XAxis.Scale.MinAuto = false;
            pane.XAxis.Scale.MaxAuto = false;

            this.zedGraphControl1.AxisChange();
            this.zedGraphControl1.Refresh();
        }

        private void CenterOnPosition(double lat, double lon)
        {
            this.zedGraphControl1.ZoomEvent -= new ZedGraphControl.ZoomEventHandler(zedGraphControl1_ZoomEvent);
            GraphPane pane = this.zedGraphControl1.GraphPane;
            double centerY = lat;
            double centerX = lon;
            double yPixPerUnit = pane.Chart.Rect.Height / (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min);
            double newUnitSpanForX = pane.Chart.Rect.Width / yPixPerUnit;
            double newUnitsSpanForY = pane.Chart.Rect.Height / yPixPerUnit;
            pane.XAxis.Scale.Min = centerX - newUnitSpanForX / 2;
            pane.XAxis.Scale.Max = centerX + newUnitSpanForX / 2;
            pane.XAxis.Scale.MinAuto = false;
            pane.XAxis.Scale.MaxAuto = false;
            pane.YAxis.Scale.Min = centerY - newUnitsSpanForY / 2;
            pane.YAxis.Scale.Max = centerY + newUnitsSpanForY / 2;
            this.zedGraphControl1.AxisChange();
            this.zedGraphControl1.Refresh();
            this.zedGraphControl1.ZoomEvent += new ZedGraphControl.ZoomEventHandler(zedGraphControl1_ZoomEvent);
        }

        #endregion

        #region initialisation
        public void UpdateDisplay()
        {
            try
            {
                this.checkedListBoxSections.Items.Clear();
                this.checkedListBoxTeams.Items.Clear();

                List<int> checkedSections = new List<int>();
                foreach (string str in Holder.sectionsSelected.Split(','))
                {
                    if (str != "")
                        checkedSections.Add(Convert.ToInt32(str));
                }
                if (Holder.race != null)
                {
                    foreach (Tag sec in Holder.race.tags)
                        this.checkedListBoxSections.Items.Add(sec, checkedSections.Contains(sec.id));
                }
                List<int> checkedTeams = new List<int>();
                foreach (string str in Holder.teamsSelected.Split(','))
                {
                    if (str != "")
                        checkedTeams.Add(Convert.ToInt32(str));
                }

                this.updateTeamList(checkedSections, checkedTeams);

                this.checkBoxCenterSelected.Checked = Tracker.Properties.Settings.Default.CenterSelectedTeam;
                this.checkBoxTeamsTrace.Checked = Tracker.Properties.Settings.Default.ShowTeamTrace;
                this.checkBoxRumLine.Checked = Tracker.Properties.Settings.Default.ShowRumLine;
                this.checkBoxPOIS.Checked = Tracker.Properties.Settings.Default.ShowPOIS;
                this.checkBoxContour.Checked = Tracker.Properties.Settings.Default.ShowContour;

                this.checkBoxCenterSelected.CheckedChanged += new System.EventHandler(this.checkBoxCenter_CheckedChanged);
                this.checkBoxTeamsTrace.CheckedChanged += new System.EventHandler(this.checkBoxTrace_CheckedChanged);
                this.checkBoxRumLine.CheckedChanged += new System.EventHandler(this.checkBoxRumLine_CheckedChanged);
                this.checkBoxPOIS.CheckedChanged += new System.EventHandler(this.checkBoxPOIS_CheckedChanged);
                this.checkBoxContour.CheckedChanged += new System.EventHandler(this.checkBoxContour_CheckedChanged);

                this.UpdateChart();
            }
            catch (Exception e)
            {
                Presenter.messages.Add(DateTime.Now, "Update Display crashed : " + e.Message);
            }
        }

        public void UpdateChart()
        {
            List<int> teamsToPlot = new List<int>();
            foreach (object obj in this.checkedListBoxTeams.CheckedItems)
                teamsToPlot.Add((obj as Team).id);
            this.UpdateChart(teamsToPlot);

            this.CenterOnMyBoat();
           
        }

        public void CenterOnMyBoat()
        {
            if (Holder.race.teams != null && Tracker.Properties.Settings.Default.CenterSelectedTeam)
            {
                if (Holder.race.teams.Any(t => t.id == Tracker.Properties.Settings.Default.MyTeam))
                {
                    Moment pos = Holder.race.teams.Single(t => t.id == Tracker.Properties.Settings.Default.MyTeam).LatestMoment();
                    this.CenterOnPosition(pos.lat, pos.lon);
                }
            }
        }

        public void UpdateChart(List<int> teamsToPlot)
        {
            GraphPane myPane = this.zedGraphControl1.GraphPane;

            myPane.CurveList.Clear();
            this.zedGraphControl1.AxisChange();

            this.traceBoatPositions(myPane, teamsToPlot);
            if(Tracker.Properties.Settings.Default.ShowRumLine)
                this.traceCourse(myPane);
            if (Tracker.Properties.Settings.Default.ShowPOIS)
                this.tracePOIS(myPane);
            if (Tracker.Properties.Settings.Default.ShowContour)
                this.TraceContours(myPane);

            // Fill the background of the chart rect and pane
            myPane.Chart.Fill = new Fill(Color.White, Color.White, 45.0f);
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.Fill = new Fill(Color.White, Color.LightYellow, 45.0f);

            this.zedGraphControl1.AxisChange();
            this.SyncAxis();
            this.zedGraphControl1.Refresh();
        }

        #endregion
        
        #region subInit
        public void updateTeamList(List<int> checkedSections, List<int> checkedTeams)
        {
            if (Holder.race != null && Holder.race.teams != null)
            {
                this.checkedListBoxSections.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
                this.checkedListBoxTeams.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox2_ItemCheck);

                this.checkedListBoxTeams.Items.Clear();
                foreach (Team td in Holder.race.teams.OrderBy(item => item.name))
                {
                    if (checkedSections != null && checkedSections.Count > 0)
                    {
                        foreach (int secid in checkedSections)
                        {
                            if (td.tags.Contains(secid))
                            {
                                this.checkedListBoxTeams.Items.Add(td, checkedTeams.Contains(td.id));
                                break;
                            }
                        }
                    }
                    else
                        this.checkedListBoxTeams.Items.Add(td, checkedTeams.Contains(td.id));
                }


                this.checkedListBoxSections.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
                this.checkedListBoxTeams.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox2_ItemCheck);

            }
        }

        public void traceBoatPositions(GraphPane myPane, List<int> teamsToPlot)
        {
            if (Holder.race != null && Holder.race.teams != null)
            {
                foreach (int teamID in teamsToPlot)
                {
                    PointPairList ppl = new PointPairList();
                    if (Holder.race.teams.Any(t => t.id == teamID))
                    {
                        Team team = Holder.race.teams.Single(t => t.id == teamID) as Team;

                        if (Tracker.Properties.Settings.Default.ShowTeamTrace)
                        {
                            foreach (Moment tp in team.moments.OrderBy(item => item.at))
                                ppl.Add(tp.lon, tp.lat, 
                                "TEAM: " + team.name + "\r\n" +
                                "SPD: " + tp.spdKn.ToString("F1") + " kn\r\n" +
                                "HDG: " + tp.heading.ToString("F0") + "\r\n" +
                                "DTF: " + UnitTools.M2Nm(tp.dtfMeters).ToString("F1") + " nm");
                        }
                        else
                        {
                            Moment latestPos = team.LatestMoment();
                            ppl.Add(latestPos.lon, latestPos.lat,
                                "TEAM: " + team.name + "\r\n" +
                                "SPD: " + latestPos.spdKn.ToString("F1") + " kn\r\n" +
                                "HDG: " + latestPos.heading.ToString("F0") + "\r\n" + 
                                "DTF: " + UnitTools.M2Nm(latestPos.dtfMeters).ToString("F1") + " nm");
                        }

                        LineItem myCurve = myPane.AddCurve(team.name, ppl, Tools.InvertMeAColour(ColorTranslator.FromHtml("#" + team.colour)), SymbolType.Circle);
                        myCurve.Symbol.IsVisible = true;
                    }
                }
            }
        }

        private void tracePOIS(GraphPane myPane)
        {
            if (Holder.race != null && Holder.race.course != null)
            {
                PointPairList ppl = new PointPairList();

                foreach (Node pois in Holder.race.course.nodes)
                {
                    ppl.Add(pois.lon, pois.lat, 0, pois.name);
                }
                LineItem myCurve = myPane.AddCurve("Points of Interrest", ppl, Color.Black, SymbolType.Square);
                myCurve.Symbol.IsVisible = true;
                myCurve.Line.IsVisible = false;
            }
        }

        private void TraceContours(GraphPane myPane)
        {
            if (Holder.contours != null)
            {
                foreach (Contour cnt in Holder.contours)
                {
                    PointPairList ppl = new PointPairList();

                    foreach (ContourPoint cp in cnt.points)
                    {
                        ppl.Add(cp.lonE, cp.latN, 0, "");
                    }
                    LineItem myCurve = myPane.AddCurve("Contour", ppl, Color.Black, SymbolType.None);
                    myCurve.Symbol.IsVisible = false;
                    myCurve.Line.IsVisible = true;
                }
            }
        }

        public void traceCourse(GraphPane myPane)
        {
            if (Holder.race != null && Holder.race.course != null)
            {
                PointPairList ppl = new PointPairList();
                foreach (Node wp in Holder.race.course.nodes)
                {
                    ppl.Add(wp.lon, wp.lat, 0, wp.name);
                }
                LineItem myCurve = myPane.AddCurve("Rum Line", ppl, Color.Black);
                myCurve.Symbol.IsVisible = false;
            }
        }
        
        #endregion

        #region eventMethods
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<int> checkedSections = new List<int>();
            foreach (object obj in this.checkedListBoxSections.CheckedItems)
            {
                checkedSections.Add((obj as Tag).id);
            }
            if (e.CurrentValue == CheckState.Unchecked)
                checkedSections.Add((this.checkedListBoxSections.Items[e.Index] as Tag).id);
            else
                checkedSections.Remove((this.checkedListBoxSections.Items[e.Index] as Tag).id);

            string checkedSectionsText = "";
            foreach (int id in checkedSections)
                checkedSectionsText += id.ToString() + ",";
            Holder.sectionsSelected = checkedSectionsText;

            List<int> checkedTeams = new List<int>();
            foreach (string str in Holder.teamsSelected.Split(','))
            {
                if (str != "")
                    checkedTeams.Add(Convert.ToInt32(str));
            }
            this.updateTeamList(checkedSections, checkedTeams);
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<int> checkedTeams = new List<int>();
            foreach (object obj in this.checkedListBoxTeams.CheckedItems)
            {
                checkedTeams.Add((obj as Team).id);
            }
            if (e.CurrentValue == CheckState.Unchecked)
                checkedTeams.Add((this.checkedListBoxTeams.Items[e.Index] as Team).id);
            else
                checkedTeams.Remove((this.checkedListBoxTeams.Items[e.Index] as Team).id);

            string checkedSectionsText = "";
            foreach (int id in checkedTeams)
                checkedSectionsText += id.ToString() + ",";
            Holder.teamsSelected = checkedSectionsText;

            if (checkedTeams.Count > 0)
                this.UpdateChart(checkedTeams);
            else
                this.UpdateChart(new List<int>());

            if (this.MySelectionChangedEvent != null)
                this.MySelectionChangedEvent();
           
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.teamInfo1.SetTeam((this.checkedListBoxTeams.SelectedItem as Team));
        }
        #endregion eventMethods

        #region chartOptions
        private void checkBoxCenter_CheckedChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.CenterSelectedTeam = this.checkBoxCenterSelected.Checked;
            this.UpdateChart();
        }

        private void checkBoxTrace_CheckedChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.ShowTeamTrace = this.checkBoxTeamsTrace.Checked;
            this.UpdateChart();
        }

        private void checkBoxRumLine_CheckedChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.ShowRumLine = this.checkBoxRumLine.Checked;
            this.UpdateChart();
        }

        private void checkBoxPOIS_CheckedChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.ShowPOIS = this.checkBoxPOIS.Checked;
            this.UpdateChart();
        }
        #endregion

        private void checkBoxContour_CheckedChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.ShowContour = this.checkBoxContour.Checked;
            this.UpdateChart();
        }

        private void checkBoxCenterSelected_CheckedChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.CenterSelectedTeam = this.checkBoxCenterSelected.Checked;
            this.CenterOnMyBoat();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ngdc.noaa.gov/mgg/coast/");
        }

       
    }
}
