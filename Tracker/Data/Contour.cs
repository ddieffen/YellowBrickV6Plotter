using System.Collections.Generic;

namespace Tracker.Data
{
    public class Contour
    {
        public List<ContourPoint> points = new List<ContourPoint>();
    }

    public struct ContourPoint
    {
        public double latN;
        public double lonE;

        public ContourPoint(double lat, double lon)
        {
            this.latN = lat;
            this.lonE = lon;
        }
    }
}
