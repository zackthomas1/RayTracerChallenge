using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Utilities
    {
        // Instance Variables
        public static readonly float EPSILON = 0.0001f;
        public const double PI = Math.PI;
        public static readonly float OVER_POINT_EPSILON = 0.0025f;
        public static readonly float UNDER_POINT_EPSILON = 0.0001f;
        // Methods
        /// <summary>
        /// Determines if the value of two floats are with in a given tolerence 
        /// to be considered equal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool FloatEquality(double a, double b)
        {
            if (Math.Abs(a - b) < EPSILON)
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
