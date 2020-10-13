using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace RayTracer
{
    public class Tuple
    {
        // Instance Variables
        public float x;
        public float y;
        public float z;
        public float w;

        // Constructors
        public Tuple(float x = 0.0f,
                     float y = 0.0f,
                     float z = 0.0f,
                     float w = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        // Class overloads
        public override string ToString()
        {
            return $"({this.x},{this.y},{this.z},{this.w})";
        }

        public override bool Equals(object obj)
        {
            return obj is Tuple tuple &&
                   x == tuple.x &&
                   y == tuple.y &&
                   z == tuple.z &&
                   w == tuple.w;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z, w);
        }

        public static Tuple operator +(Tuple t1, Tuple t2)
        {
            Tuple t3 = new Tuple();

            t3.x = t1.x + t2.x;
            t3.y = t1.y + t2.y;
            t3.z = t1.z + t2.z;
            t3.w = t1.w + t2.w;

            return t3;
        }

        public static Tuple operator -(Tuple t1, Tuple t2)
        {
            Tuple t3 = new Tuple();

            t3.x = t1.x - t2.x;
            t3.y = t1.y - t2.y;
            t3.z = t1.z - t2.z;
            t3.w = t1.w - t2.w;

            return t3;
        }

        public static Tuple operator -(Tuple t1)
        {
            Tuple t2 = new Tuple();

            t2.x = 0 - t1.x;
            t2.y = 0 - t1.y;
            t2.z = 0 - t1.z;

            return t2;
        }
        public static Tuple operator *(Tuple t1, float scalar)
        {
            Tuple t2 = new Tuple();

            t2.x = t1.x * scalar;
            t2.y = t1.y * scalar;
            t2.z = t1.z * scalar;
            t2.w = t1.w * scalar;

            return t2;
        }

        public static Tuple operator *(Matrix4 m1, Tuple t2)
        {
            Tuple returnTuple = new Tuple();

            returnTuple.x = m1[0, 0] * t2.x + m1[0, 1] * t2.y + m1[0, 2] * t2.z + m1[0, 3] * t2.w;
            returnTuple.y = m1[1, 0] * t2.x + m1[1, 1] * t2.y + m1[1, 2] * t2.z + m1[1, 3] * t2.w;
            returnTuple.z = m1[2, 0] * t2.x + m1[2, 1] * t2.y + m1[2, 2] * t2.z + m1[2, 3] * t2.w;
            returnTuple.w = m1[3, 0] * t2.x + m1[3, 1] * t2.y + m1[3, 2] * t2.z + m1[3, 3] * t2.w;

            return returnTuple;
        }

        public static Tuple operator /(Tuple t1, float scalar)
        {
            Tuple t2 = new Tuple();

            t2.x = t1.x / scalar;
            t2.y = t1.y / scalar;
            t2.z = t1.z / scalar;
            t2.w = t1.w / scalar;

            return t2;
        }

        public static bool operator ==(Tuple t1, Tuple t2)
        {
            if (Utilities.FloatEquality(t1.x, t2.x) &&
                Utilities.FloatEquality(t1.y, t2.y) &&
                Utilities.FloatEquality(t1.z, t2.z) &&
                Utilities.FloatEquality(t1.w, t2.w))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Tuple t1, Tuple t2)
        {
            if (Utilities.FloatEquality(t1.x, t2.x) &&
                Utilities.FloatEquality(t1.y, t2.y) &&
                Utilities.FloatEquality(t1.z, t2.z) &&
                Utilities.FloatEquality(t1.w, t2.w))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
