using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using Tracker.Data;
using YellowbrickV8.Entities;
using YellowbrickV8;
using System.Drawing.Drawing2D;

namespace Tracker.Gui.Controls
{
    public partial class Scatter : UserControl
    {
        public Scatter()
        {
            InitializeComponent();
           
        }

        internal void UpdateDisplay()
        {
            List<int> teams = new List<int>();
            if (this.radioButton1.Checked)
            {//all boats
                teams = CreateTeamList(true);
            }
            else if (this.radioButtonMysectionOnly.Checked)
            {//my section only
                teams = CreateTeamList(false);
            }

            this.UpdateChart(teams);
        }

        public List<int> CreateTeamList(bool allBoats)
        {
            List<int> checkedSections = new List<int>();
            if (!allBoats)
            {
                foreach (string str in Holder.sectionsSelected.Split(','))
                {
                    if (str != "")
                        checkedSections.Add(Convert.ToInt32(str));
                }
            }

            List<int> teams = new List<int>();
            if (Holder.race != null && Holder.race.teams != null)
            {        
                foreach (Team td in Holder.race.teams)
                {
                    if (checkedSections != null && checkedSections.Count > 0)
                    {
                        foreach (int secid in checkedSections)
                        {
                            if (td.tags.Contains(secid))
                            {
                                teams.Add(td.id);
                                break;
                            }
                        }
                    }
                    else
                        teams.Add(td.id);
                }
            }

            return teams;
        }

        public void UpdateChart(List<int> teamsToPlot)
        {
            GraphPane myPane = this.zedGraphControl1.GraphPane;
            myPane.Title.Text = "Boats Normalized Speed";
            myPane.XAxis.Title.Text = "Longitude";
            myPane.YAxis.Title.Text = "Latitude";
            myPane.Legend.IsVisible = false;
            myPane.CurveList.Clear();

            this.traceBoatPositions(myPane, teamsToPlot);
            //if (Tracker.Properties.Settings.Default.ShowRumLine)
            //    this.traceCourse(myPane);
            //if (Tracker.Properties.Settings.Default.ShowPOIS)
            //    this.tracePOIS(myPane);
            //if (Tracker.Properties.Settings.Default.ShowContour)
            //    this.TraceContours(myPane);

            // Fill the background of the chart rect and pane
            myPane.Chart.Fill = new Fill(Color.White, Color.White, 45.0f);
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.Fill = new Fill(Color.White, Color.LightYellow, 45.0f);

            this.zedGraphControl1.AxisChange();
            this.SyncAxis();
            this.zedGraphControl1.Refresh();
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

        public void traceBoatPositions(GraphPane myPane, List<int> teamsToPlot)
        {
            if (Holder.race != null && Holder.race.teams != null)
            {
                foreach (Team team in Holder.race.teams)
                {
                    PointPairList ppl = new PointPairList();

                        Moment latestPos = team.LatestMoment();
                        double relSpeed = latestPos.spdKn / team.maxSpeedKn;
                        Color col;
                        if (team.id == Tracker.Properties.Settings.Default.MyTeam)
                            col = Color.FromArgb(255, 0, 255);
                        else
                            col = ColFromRelativeSpeed(relSpeed);

                    ppl.Add(latestPos.lon, latestPos.lat,
                        "TEAM: " + team.name + "\r\n" +
                        "SPD: " + latestPos.spdKn.ToString("F1") + " kn\r\n" +
                        "HDG: " + latestPos.heading.ToString("F0") + "\r\n" +
                        "DTF: " + UnitTools.M2Nm(latestPos.dtfMeters).ToString("F1") + " nm");

                    LineItem myCurve = myPane.AddCurve(team.name, ppl, col, SymbolType.Circle);
                    myCurve.Symbol.Fill = new Fill(col);
                    myCurve.Line.IsVisible = false;
                    myCurve.Symbol.IsVisible = true;
                }
            }
        }

        /// <summary>
        /// Creates a color from a 0-1 value, blue for 0, red for 1
        /// </summary>
        /// <param name="relSpeed"></param>
        /// <returns></returns>
        private Color ColFromRelativeSpeed(double relSpeed)
        {
            if (double.IsNaN(relSpeed) || double.IsInfinity(relSpeed))
                return Color.Black;
            relSpeed = Math.Abs(1 - relSpeed);
            double div = (Math.Abs(relSpeed % 1) * 4);
            if (relSpeed == 1)
                div = 4;
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return Color.FromArgb(255, 255, 0, descending);
            }
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

        void zedGraphControl1_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            this.SyncAxis();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush br = new LinearGradientBrush(this.panel1.ClientRectangle, Color.Black, Color.Black, -90 , false);
            ColorBlend cb = new ColorBlend();
            cb.Positions = new[] { 0, 1 / 5f, 2 / 5f, 3 / 5f, 4 / 5f, 1 };
            cb.Colors = new[] { ColFromRelativeSpeed(0), ColFromRelativeSpeed(1 / 5f), ColFromRelativeSpeed(2 / 5f), ColFromRelativeSpeed(3 / 5f), ColFromRelativeSpeed(4 / 5f), ColFromRelativeSpeed(1) };
            br.InterpolationColors = cb;
            br.RotateTransform(0);
            e.Graphics.FillRectangle(br, this.panel1.ClientRectangle);
        }
    }
}
