using System;
using System.Linq;
using YellowbrickV8.Entities;

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
                m.dtfMeters = Int32.MaxValue;
                return m;
            }
        }
    }
}
