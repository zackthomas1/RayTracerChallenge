using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class RingPattern : Pattern
    {
        // Constructors

        public RingPattern() : base()
        {

        }

        public RingPattern(Pattern p1, Pattern p2) : base()
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

            if (Utilities.FloatEquality( Math.Floor(Math.Sqrt((tp.x * tp.x) + (tp.z * tp.z)) % 2), 0))
                return p1.PatternAt(tp);
            else
                return p2.PatternAt(tp);
        }

    }
}
