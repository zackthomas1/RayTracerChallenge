using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class GradientPattern : Pattern
    {

        // Constructor
        public GradientPattern() : base()
        {

        }
        public GradientPattern(Pattern p1, Pattern p2) : base()
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        // Methods

        //public override Pattern CreatePattern(Color c1, Color c2)
        //{
        //    GradientPattern result = new GradientPattern(c1, c2);
        //    return result;
        //}

        public override Color PatternAt(Point point)
        {

            Point tp = this.Transform.Invert() * point;

            GradientPattern gradient = this;
            Color distance = gradient.p2.PatternAt(tp) - gradient.p1.PatternAt(tp);
            float fraction = tp.x - (float)Math.Floor(tp.x);

            return gradient.p1.PatternAt(tp) + distance * fraction;
        }






    }
}
