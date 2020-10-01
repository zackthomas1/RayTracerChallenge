using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class RadialGradient : Pattern
    {

        // Constructors
        public RadialGradient() : base()
        {

        }

        public RadialGradient(Pattern p1, Pattern p2) : base()
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        // Methods 
        public override Color PatternAt(Point point)
        {

            Point tp = this.Transform.Invert() * point;

            double distance = Math.Sqrt(tp.x * tp.x + tp.z * tp.z);
            double fraction = distance - Math.Floor(distance);
           
            return p1.PatternAt(tp) +
                    (p2.PatternAt(tp) - p1.PatternAt(tp)) * (float)fraction;
        }

    }
}
