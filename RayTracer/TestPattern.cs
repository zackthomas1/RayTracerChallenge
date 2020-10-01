using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class TestPattern : Pattern
    {
        public TestPattern() : base()
        {

        }
        public override Pattern CreatePattern(Color c1, Color c2)
        {
            TestPattern pattern = new TestPattern();
            return pattern;
        }

        public override Color PatternAt(Point point)
        {
            return new Color(point.x, point.y, point.z);
        }
    }

}
