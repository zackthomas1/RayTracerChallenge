using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public abstract class Pattern
    {
        // Instance Variables
        public Pattern p1 { get; set; }
        public Pattern p2 { get; set; }
        Matrix4 transform = new Matrix4();

        // Get/Set methods
        public Matrix4 Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        // Constructors
        public Pattern()
        {
            this.p1 = SolidPattern.White;
            this.p2 = SolidPattern.Black;
        }

        //public Pattern(Color a, Color b)
        //{
        //    this.a = a;
        //    this.b = b;
        //}

        // Methods
        /// <summary>
        /// Construct a new Pattern
        /// aka stripe_pattern(a,b) method from book
        /// </summary>
        //public abstract Pattern CreatePattern(Color a, Color b);

        /// <summary>
        /// Determines color result for a given point in a pattern.
        /// aka stripe_at(Point) from book
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public abstract Color PatternAt(Point point);

        /// <summary>
        /// Uses PatternAt() method to return Color value.
        /// Converting world space to object to apply pattern to each RayObject independently.
        /// aka stripe_at_object(Rayobject,Point) from book
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pointWorld"></param>
        /// <returns></returns>
        public Color PatternAtObject(RayObject RayObject, Point pointWorld)
        {

            // Converts world space to pattern space through objectspace
            Point objectPoint = RayObject.Transform.Invert() * pointWorld;
            //Point patternPoint = this.Transform.Invert() * objectPoint;

            return this.PatternAt(objectPoint);

        }
    }
}
