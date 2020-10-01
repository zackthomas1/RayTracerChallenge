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
            c1 = Color.White;
            c2 = Color.Black;
        }
        public GradientPattern(Color c1, Color c2) : base()
        {
            this.c1 = c1;
            this.c2 = c2;
        }

        // Methods
        public override Pattern CreatePattern(Color c1, Color c2)
        {
            GradientPattern result = new GradientPattern(c1, c2);
            return result;
        }

        public override Color PatternAt(Point point)
        {
            GradientPattern gradient = this;
            Color distance = gradient.c2 - gradient.c1;
            float fraction = point.x - (float)Math.Floor(point.x);

            return gradient.c1 + distance * fraction;
        }






    }
}
