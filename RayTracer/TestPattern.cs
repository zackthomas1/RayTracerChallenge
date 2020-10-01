using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class TestPattern : Pattern
    {
        // Constructors
        public TestPattern() : base()
        {

        }

        //Methods 

        //public override Pattern CreatePattern(Color c1, Color c2)
        //{
        //    TestPattern pattern = new TestPattern();
        //    return pattern;
        //}

        public override Color PatternAt(Point point)
        {
            Point tp = this.Transform.Invert() * point;

            return new Color(tp.x, tp.y, tp.z);
        }
    }

}
