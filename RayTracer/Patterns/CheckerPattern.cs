using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class CheckerPattern : Pattern
    {

        // Constructors
        public CheckerPattern() : base()
        {

        }

        public CheckerPattern(Pattern p1, Pattern p2) : base()
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        // Methods 

        //public override Pattern CreatePattern(Color c1, Color c2)
        //{
        //    RingPattern pattern = new RingPattern(c1, c2);
        //    return pattern;
        //}

        public override Color PatternAt(Point point)
        {
            Point tp = this.Transform.Invert() * point;

            if (Utilities.FloatEquality((Math.Floor(tp.x) + Math.Floor(tp.y) + Math.Floor(tp.z)) % 2, 0))
                return p1.PatternAt(tp);
            else
                return p2.PatternAt(tp);
        }
    }
}
