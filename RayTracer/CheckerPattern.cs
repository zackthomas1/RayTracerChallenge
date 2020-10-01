using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class CheckerPattern : Pattern
    {
        public CheckerPattern() : base()
        {

        }

        public CheckerPattern(Color c1, Color c2) : base()
        {
            this.c1 = c1;
            this.c2 = c2;
        }
        public override Pattern CreatePattern(Color c1, Color c2)
        {
            RingPattern pattern = new RingPattern(c1, c2);
            return pattern;
        }

        public override Color PatternAt(Point point)
        {
            if ((Math.Floor(point.x) + Math.Floor(point.y) + Math.Floor(point.z)) % 2 == 0)
                return c1;
            else
                return c2;
        }
    }
}
