using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Vector3 : Tuple
    {

        // Constructors
        Vector3() : base(0.0f, 0.0f, 0.0f, 0.0f)
        {

        }

        public Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 0.0f) : base(x, y, z, w)
        {

        }

        // Class overloads
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = new Vector3();

            v3.x = v1.x + v2.x;
            v3.y = v1.y + v2.y;
            v3.z = v1.z + v2.z;

            return v3;
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = new Vector3();

            v3.x = v1.x - v2.x;
            v3.y = v1.y - v2.y;
            v3.z = v1.z - v2.z;

            return v3;
        }

        public static Vector3 operator -(Vector3 v1)
        {
            Vector3 v2 = new Vector3();

            v2.x = 0 - v1.x;
            v2.y = 0 - v1.y;
            v2.z = 0 - v1.z;

            return v2;
        }

        public static Vector3 operator *(Vector3 v1, float scalar)
        {
            Vector3 v2 = new Vector3();

            v2.x = v1.x * scalar;
            v2.y = v1.y * scalar;
            v2.z = v1.z * scalar;

            return v2;
        }

        public static Vector3 operator *(Matrix4 m1, Vector3 v1)
        {
            Vector3 result = new Vector3();

            result.x = m1[0, 0] * v1.x + m1[0, 1] * v1.y + m1[0, 2] * v1.z + m1[0, 3] * v1.w;
            result.y = m1[1, 0] * v1.x + m1[1, 1] * v1.y + m1[1, 2] * v1.z + m1[1, 3] * v1.w;
            result.z = m1[2, 0] * v1.x + m1[2, 1] * v1.y + m1[2, 2] * v1.z + m1[2, 3] * v1.w;
            result.w = m1[3, 0] * v1.x + m1[3, 1] * v1.y + m1[3, 2] * v1.z + m1[3, 3] * v1.w;

            return result;
        }

        public static Vector3 operator *(Vector3 v1, Matrix4 m1)
        {
            Vector3 result = new Vector3();

            result.x = m1[0, 0] * v1.x + m1[0, 1] * v1.y + m1[0, 2] * v1.z + m1[0, 3] * v1.w;
            result.y = m1[1, 0] * v1.x + m1[1, 1] * v1.y + m1[1, 2] * v1.z + m1[1, 3] * v1.w;
            result.z = m1[2, 0] * v1.x + m1[2, 1] * v1.y + m1[2, 2] * v1.z + m1[2, 3] * v1.w;
            result.w = m1[3, 0] * v1.x + m1[3, 1] * v1.y + m1[3, 2] * v1.z + m1[3, 3] * v1.w;

            return result;
        }

        public static Vector3 operator /(Vector3 v1, float scalar)
        {
            Vector3 v2 = new Vector3();

            v2.x = v1.x / scalar;
            v2.y = v1.y / scalar;
            v2.z = v1.z / scalar;

            return v2;
        }

        // Methods
        /// <summary>
        /// Calculates magnitude of a vector
        /// (ie. the length of the vector)
        /// </summary>
        /// <returns></returns>
        public float Magnitude()
        {
            double magnitude = Math.Sqrt((Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2) + Math.Pow(this.w, 2)));

            // If the magnitude is within a certain tolerence to 1 then it is equal to a unit vector's magnitude
            if(Utilities.FloatEquality(1, magnitude))
                magnitude = 1;

            return (float)magnitude;

        }

        /// <summary>
        /// Normalizes input vector. (modifies input)
        /// </summary>
        /// <returns></returns>
        public Vector3 Normalize()
        {
            float vectorMagnitude = this.Magnitude(); 

            Vector3 returnVector = new Vector3();

            this.x = this.x / vectorMagnitude;
            this.y = this.y / vectorMagnitude;
            this.z = this.z / vectorMagnitude;
            this.w = this.w / vectorMagnitude;

            return this;
        }

        /// <summary>
        /// Returns a NEW normalized vector
        /// </summary>
        /// <returns></returns>
        public Vector3 Normalized()
        {
            float vectorMagnitude = this.Magnitude();

            Vector3 returnVector = new Vector3();

            returnVector.x = this.x / vectorMagnitude;
            returnVector.y = this.y / vectorMagnitude;
            returnVector.z = this.z / vectorMagnitude;
            returnVector.w = this.w / vectorMagnitude;

            return returnVector;
        }

        /// <summary>
        /// Returns the cross product of two vectors
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 Cross(Vector3 v2)
        {
            Vector3 returnVector = new Vector3();

            returnVector.x = this.y * v2.z - this.z * v2.y;
            returnVector.y = this.z * v2.x - this.x * v2.z;
            returnVector.z = this.x * v2.y - this.y * v2.x;

            return returnVector;
        }

        /// <summary>
        /// Returns the cross product of two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            Vector3 returnVector = new Vector3();

            returnVector.x = v1.y * v2.z - v1.z * v2.y;
            returnVector.y = v1.z * v2.x - v1.x * v2.z;
            returnVector.z = v1.x * v2.y - v1.y * v2.x;

            return returnVector;
        }

        /// <summary>
        /// Caculates the reflection vector. 
        /// Given incoming vector and vector representing surface normal.
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        public static Vector3 Reflection(Vector3 incoming, Vector3 normal)
        {
            Vector3 result = incoming - normal * 2 * Dot(incoming, normal);
            return result;
        }

        // Methods
        /// <summary>
        /// Returns the dot product of two vector as a float.
        /// </summary>
        /// <param name="t2"></param>
        /// <returns></returns>
        public float Dot(Tuple t2)
        {
            float product = (this.x * t2.x) + (this.y * t2.y) + (this.z * t2.z) + (this.w * t2.w);
            return product;
        }

        /// <summary>
        /// Returns the dot product of two vector as a float.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static float Dot(Tuple t1, Tuple t2)
        {
            float product = (t1.x * t2.x) + (t1.y * t2.y) + (t1.z * t2.z) + (t1.w * t2.w);
            return product;
        }

    }
}
