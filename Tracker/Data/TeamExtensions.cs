using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowbrickV6.Entities;

namespace Tracker.Data
{
    internal static class TeamExtension
    {
        static internal Moment LatestMoment(this Team team)
        {
            if (team.moments != null && team.moments.Count > 0)
                return team.moments.OrderByDescending(m => m.at).First();
            else
            {
                Moment m = new Moment();
                m.dtf = Int32.MaxValue;
                return m;
            }
        }
    }
}
