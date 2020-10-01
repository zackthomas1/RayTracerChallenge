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
            c1 = Color.White;
            c2 = Color.Black;
        }
        public StripedPattern(Color c1, Color c2) : base()
        {
            this.c1 = c1;
            this.c2 = c2;
        }

        // Methods
        public override Pattern CreatePattern(Color c1, Color c2)
        {
            StripedPattern result = new StripedPattern(c1, c2);
            return result;
        }
    
        public override Color PatternAt(Point point)
        {
            if (Math.Floor(point.x) % 2 == 0)
                return c1;
            else
                return c2;
        }

    }
}
