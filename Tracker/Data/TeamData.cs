using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowbrickV6.Entities;

namespace Tracker.Data
{
    internal class TeamData : Team
    {
        internal Moment LatestMoment 
        {
            get 
            {
                if (this.moments != null && this.moments.Count > 0)
                    return this.moments.OrderByDescending(m => m.at).First();
                else
                    return null;
            }
        }
    }
}
