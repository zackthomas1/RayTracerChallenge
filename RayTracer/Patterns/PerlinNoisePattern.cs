using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class PerlinNoisePattern : Pattern
    {
        // Constructors
        public PerlinNoisePattern() : base()
        {
            
        }

        public PerlinNoisePattern(Pattern p1, Pattern p2) : base()
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        // Methods 




        public override Color PatternAt(Point point)
        {
            return new Color();
        }
    }
}
