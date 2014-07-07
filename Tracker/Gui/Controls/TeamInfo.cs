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
                Moment latestMoment = (td as TeamData).LatestMoment;
                if (latestMoment != null)
                {
                    this.labelPositionAt.Text = latestMoment.at.ToString();
                    this.labelPosition.Text = latestMoment.ToString();
                    this.labelSpeed.Text = latestMoment.spd.ToString("F2") + " kn / " + latestMoment.heading.ToString("F0") + " deg";
                    this.labelDistanceToGo.Text = latestMoment.dtf.ToString() + "nm";
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
