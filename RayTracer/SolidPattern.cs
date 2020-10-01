using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class SolidPattern : Pattern
    {

        public Color color { get; set; }

        // readonly variables for standard default color values
        public static readonly SolidPattern Black = new SolidPattern(Color.Black);
        public static readonly SolidPattern White = new SolidPattern(Color.White);
        public static readonly SolidPattern Red = new SolidPattern(Color.Red);
        public static readonly SolidPattern Orange = new SolidPattern(Color.Orange);
        public static readonly SolidPattern Yellow = new SolidPattern(Color.Yellow);
        public static readonly SolidPattern Green = new SolidPattern(Color.Green);
        public static readonly SolidPattern Blue = new SolidPattern(Color.Blue);
        public static readonly SolidPattern Purple = new SolidPattern(Color.Purple);

        // Constructors 

        public SolidPattern() : base()
        {
            color = Color.White; 
        }

        public SolidPattern(Color color) : base()
        {
            this.color = color; 
        }

        //Operator overrides
        public static SolidPattern operator *(SolidPattern p1, float scalar) // May want to move to Pattern parent class
        {
            Color colorResult = p1.color * scalar;
            return new SolidPattern(colorResult);
        }


        // Methods 

        public override Color PatternAt(Point point) 
        {
            return this.color; 
        }


    }
}
