using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class BlendPattern : Pattern
    {
        public float t;

        // Constructors
        public BlendPattern() : base()
        {
            this.t = 0.5f;
        }

        public BlendPattern(Pattern p1, Pattern p2, float t = 0.5f) : base()
        {
            this.p1 = p1;
            this.p2 = p2;
            this.t = t;
        }

        // Methods 
        public override Color PatternAt(Point point)
        {
            Point tp = this.Transform.Invert() * point;

            return p1.PatternAt(tp) * (1.0f - t) + p2.PatternAt(tp) * t;
        }


    }
}
