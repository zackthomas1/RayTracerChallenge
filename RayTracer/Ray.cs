using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Ray
    {
        // Instance Variables
        public Point origin;
        public Vector3 direction;

        // Get/Set methods

        // Constructors
        public Ray(Point origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        // Class overloads
        public override string ToString()
        {
            return "Origin:" + origin.ToString() + " -> " + "Direction:" + direction.ToString(); 
        }

        public static Ray operator *(Ray r1, Matrix4 matrix)
        {
            Ray temp = new Ray(new Point(), new Vector3());

            temp.origin = r1.origin * matrix;
            temp.direction = r1.direction * matrix;

            return temp;
        }

        // Methods
        /// <summary>
        /// Gets the point position of a ray at a given t value.
        /// (aka. iterations of direction)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point GetPointPosition(float t)
        {
            return origin + (direction * t); 
        }

        /// <summary>
        /// Returns a new Ray that is the result of input Ray multipled by a transformation
        /// matrix This will change the position Point but not the direction Vector
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public Ray Transform(Matrix4 matrix)
        {
            Ray temp = this;
            temp = temp * matrix;

            return temp;
        }

        // Considering deleting this method and using only RayToObjectSpace method in RayObject class
        /// <summary>
        /// Takes a RayObject's tranformMatrix inverts it and 
        /// returns a new ray with an update origin position Point
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Ray ApplyObjectTransform(RayObject obj)
        {
            Ray temp = new Ray(new Point(), new Vector3());
            temp =  this * obj.Transform.Invert();

            return temp; 
        }
    }
}
