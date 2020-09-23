using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Vector3 : Tuple
    {

        Vector3() : base(0.0f, 0.0f, 0.0f, 0.0f)
        {

        }

        public Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 0.0f) : base(x, y, z, w)
        {

        }

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

        public static Vector3 operator /(Vector3 v1, float scalar)
        {
            Vector3 v2 = new Vector3();

            v2.x = v1.x / scalar;
            v2.y = v1.y / scalar;
            v2.z = v1.z / scalar;

            return v2;
        }

        /// <summary>
        /// Gets the Magnitude of a vector
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
        /// Returns a modified input Vector that is normalized
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
        /// Returns a new normalized vector
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

    }
}
