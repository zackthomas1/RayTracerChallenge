using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Point : Tuple
    {

        Point() : base(0.0f, 0.0f, 0.0f, 1.0f)
        {

        }

        public Point(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 1.0f) : base(x, y, z, w)
        {

        }


        public static Point operator +(Point p1, Vector3 v2)
        {
            Point p3 = new Point();

            p3.x = p1.x + v2.x;
            p3.y = p1.y + v2.y;
            p3.z = p1.z + v2.z;

            return p3;
        }

        public static Point operator -(Point p1, Vector3 v2)
        {
            Point p3 = new Point();

            p3.x = p1.x - v2.x;
            p3.y = p1.y - v2.y;
            p3.z = p1.z - v2.z;

            return p3;
        }

        public static Vector3 operator -(Point p1, Point p2)
        {
            Vector3 v3 = new Vector3();

            v3.x = p1.x - p2.x;
            v3.y = p1.y - p2.y;
            v3.z = p1.z - p2.z;

            return v3;
        }

        public static Point operator -(Point p1)
        {
            Point p2 = new Point();

            p2.x = 0 - p1.x;
            p2.y = 0 - p1.y;
            p2.z = 0 - p1.z;

            return p2;
        }

        public static Point operator *(Point p1, float scalar)
        {
            Point p2 = new Point();

            p2.x = p1.x * scalar;
            p2.y = p1.y * scalar;
            p2.z = p1.z * scalar;

            return p2;
        }

        public static Point operator /(Point p1, float scalar)
        {
            Point p2 = new Point();

            p2.x = p1.x / scalar;
            p2.y = p1.y / scalar;
            p2.z = p1.z / scalar;

            return p2;
        }
    }
}
