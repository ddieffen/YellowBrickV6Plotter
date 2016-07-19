using System;
using System.Drawing;
using System.Windows.Forms;
using Tracker.Data;
using YellowbrickV8.Entities;
using YellowbrickV8;

namespace Tracker.Gui.Controls
{
    public partial class TeamInfo : UserControl
    {
        public TeamInfo()
        {
            InitializeComponent();
        }

        public void SetTeam(Team td)
        {
            try
            {
                this.labelName.Text = "";
                this.labelType.Text = "";
                this.labelSail.Text = "";
                this.labelPositionAt.Text = "";
                this.labelPosition.Text = "";
                this.labelSpeed.Text = "";
                this.labelDistanceToGo.Text = "";

                this.labelName.Text = td.name;
                this.labelName.ForeColor = Tools.InvertMeAColour(ColorTranslator.FromHtml("#" + td.colour));
                this.labelType.Text = td.model;
                this.labelSail.Text = td.sail;
                Moment latestMoment = (td as Team).LatestMoment();
                if (latestMoment != null)
                {
                    this.labelPositionAt.Text = TimeTools.UnixTimeStampToDateTime(latestMoment.at).ToLocalTime().ToString();
                    this.labelPosition.Text = "Lat: " + latestMoment.lat.ToString("F6") + "   Lon: " + latestMoment.lon.ToString("F6");
                    this.labelSpeed.Text = latestMoment.spdKn.ToString("F2") + " kn / " + latestMoment.heading.ToString("F0") + " deg";
                    this.labelDistanceToGo.Text = UnitTools.M2Nm(latestMoment.dtfMeters).ToString("F1") + "nm";
                    this.labelStatus.Text = td.status;
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
