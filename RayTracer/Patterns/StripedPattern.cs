using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class StripedPattern : Pattern
    {
        // Instance Variables

        // Get/Set methods

        // Constructors
        public StripedPattern() : base()
        {
            this.p1 = new SolidPattern(Color.White);
            this.p2 = new SolidPattern(Color.Black);
        }
        public StripedPattern(Pattern p1, Pattern p2) : base()
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        // Methods

        //public override Pattern CreatePattern(Color c1, Color c2)
        //{
        //    StripedPattern result = new StripedPattern(c1, c2);
        //    return result;
        //}
    
        public override Color PatternAt(Point point)
        {

            Point tp = this.Transform.Invert() * point;

            if (Utilities.FloatEquality(Math.Floor(tp.x) % 2, 0))
                return p1.PatternAt(tp);
            else
                return p2.PatternAt(tp);
        }

    }
}
