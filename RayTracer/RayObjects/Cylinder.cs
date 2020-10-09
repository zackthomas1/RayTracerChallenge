using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.RayObjects
{
    public class Cylinder : RayObject
    {

        // Instance Variables
        float radius = 1.0f;
        bool closed;
        float maxHeight;
        float minHeight;

        // Get/Set methods
        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        public float MaxHeight
        {
            get { return maxHeight; }
            set { maxHeight = value; }
        }

        public float MinHeight
        {
            get { return minHeight; }
            set { minHeight = value; }
        }

        // Constructors
        public Cylinder(bool closed = false,
                        float maxHeight = float.PositiveInfinity, float minHeight = float.NegativeInfinity) : base()
        {
            this.closed = closed;
            this.maxHeight = maxHeight;
            this.minHeight = minHeight; 
        }

        public Cylinder(Material material, bool closed = false,
                        float maxHeight = float.PositiveInfinity, float minHeight = float.NegativeInfinity) : base()
        {
            this.closed = closed;
            this.maxHeight = maxHeight;
            this.minHeight = minHeight;

            this.material = material;
        }

        public override List<Intersection> LocalIntersects(Ray objSpaceRay)
        {
            //objSpaceRay.direction.Normalize();

            List<Intersection> intersections = new List<Intersection>();


            float a = (objSpaceRay.direction.x * objSpaceRay.direction.x) + (objSpaceRay.direction.z * objSpaceRay.direction.z);
            if (Utilities.FloatEquality(a,0))  // Ray is parallel to the y-axis
            {
                IntersectCaps(objSpaceRay, intersections); 
                return intersections; // return empty list
            }

            float b = 2 * ((objSpaceRay.origin.x * objSpaceRay.direction.x)) +
                        2 * ((objSpaceRay.origin.z * objSpaceRay.direction.z));

            float c = (objSpaceRay.origin.x * objSpaceRay.origin.x) + (objSpaceRay.origin.z * objSpaceRay.origin.z) - 1;

            float discriminant = (b * b) - 4 * a * c;
            if (discriminant < 0)   // Ray doesn not intersect cyclinder
            {
                return intersections; // return empty list
            }

            float t00 = (-b - (float)Math.Sqrt(discriminant)) / (2 * a);
            float t01 = (-b + (float)Math.Sqrt(discriminant)) / (2 * a);

            if (t00 > t01)
            {
                float temp_t00 = t01;
                float temp_t01 = t00;

                t00 = temp_t00;
                t01 = temp_t01;
            }

            //WriteLine();
            // Checks that the intersection points are between the min and max height
            float y00 = objSpaceRay.origin.y + t00 * objSpaceRay.direction.y;
            //Console.WriteLine("y00: " + y00);
            if (minHeight < Math.Round(y00,4) && Math.Round(y00, 4) < maxHeight)
            {
                //Console.WriteLine("\tt00: " + t00);
                intersections.Add(new Intersection(t00, this));
            }

            float y01 = objSpaceRay.origin.y + t01 * objSpaceRay.direction.y;
            //Console.WriteLine("y01: " + y01);
            if (minHeight < Math.Round(y01, 4) && Math.Round(y01, 4) < maxHeight)
            {
                //Console.WriteLine("\tt01: " + t01);
                intersections.Add(new Intersection(t01, this));
            }
            
            IntersectCaps(objSpaceRay, intersections);
            return intersections;
        }

        /// <summary>
        /// Helper method for IntersectCaps. Checks  to see if the intersection at 't' is within a radius 
        /// of 1 (the raudis of the your cylinders) from the y axis
        /// </summary>
        /// <param name="objSpaceRay"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool CheckCap(Ray objSpaceRay, float t)
        {
            float x = objSpaceRay.origin.x + t * objSpaceRay.direction.x;
            float z = objSpaceRay.origin.z + t * objSpaceRay.direction.z;

            return Math.Round(Math.Pow(x, 2) + Math.Pow(z, 2), 4) <= radius;
        }

        public void IntersectCaps(Ray objSpaceRay, List<Intersection> xs)
        {
            // Caps only matter if the cylinder is closed, 
            // and might possibly be intersected by the ray
            if (closed == false || Math.Abs(objSpaceRay.direction.y) < Utilities.EPSILON)
            {
                return;
            }

            // Checks for an intersection with the lower end cap 
            // by intersecting the ray with the plane at y = cyl.minHeight
            float tMin = (minHeight - objSpaceRay.origin.y) / objSpaceRay.direction.y;
            if (CheckCap(objSpaceRay, minHeight))
            {
                xs.Add(new Intersection(tMin, this));
            }

            // Checks for an intersection with the upper end cap 
            // by intersecting the ray with the plane at y = cly.maxHeight
            float tMax = (maxHeight - objSpaceRay.origin.y) / objSpaceRay.direction.y;
            if (CheckCap(objSpaceRay, maxHeight))
            {
                xs.Add(new Intersection(tMax, this));
            }
        }

        public override Vector3 LocalNormal(Point objectPoint)
        {
            // Compute the square of the distance from the y-axis
            float dist = objectPoint.x * objectPoint.x + objectPoint.z * objectPoint.z + Utilities.OVER_POINT_EPSILON;

            if (dist < radius && objectPoint.y >= maxHeight - Utilities.EPSILON)
            {
                return new Vector3(0, 1, 0);
            }
            else if (dist < radius && objectPoint.y <= minHeight + Utilities.EPSILON)
            {
                return new Vector3(0, -1, 0);
            }
            else
            {
                return new Vector3(objectPoint.x, 0, objectPoint.z);
            }
        }
        

    }
}
