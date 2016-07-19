using System.Collections.Generic;
using YellowbrickV8.Entities;

namespace Tracker.Data
{
    public static class Holder
    {
        public static double latestPositionsUpdate = -1;
        public static double allPositionsUpdate = -1;

        public static bool dataIsBeeingUpdated = false;
        public static string sectionsSelected = "";
        public static string teamsSelected = "";

        public static Race race;
        public static List<Contour> contours;
    }
}
