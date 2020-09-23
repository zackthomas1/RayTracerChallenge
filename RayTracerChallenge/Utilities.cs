using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Utilities
    {

        public static float Epsilon = 0.0001f; 

        public static bool FloatEquality(double a, double b)
        {
            if (Math.Abs(a - b) < Epsilon)
                return true;
            else
                return false;
        }

    }
}
