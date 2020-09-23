using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Utilities
    {

        public static float Epsilon = 0.0001f;

        public const double PI = Math.PI;

        public static bool FloatEquality(double a, double b)
        {
            if (Math.Abs(a - b) < Epsilon)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Converts rotation in degrees to radians
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static double DegreeToRadian(double degree)
        {
            double radian = (degree / 180) * Math.PI;
            return radian; 
        }

    }
}
