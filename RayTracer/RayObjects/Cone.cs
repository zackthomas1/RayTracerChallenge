using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Cone : RayObject
    {
        // Instance Variables
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
        public Cone(bool closed = false,
                        float maxHeight = float.PositiveInfinity, float minHeight = float.NegativeInfinity) : base()
        {
            this.closed = closed;
            this.maxHeight = maxHeight;
            this.minHeight = minHeight;
        }

        public Cone(Material material, bool closed = false,
                        float maxHeight = float.PositiveInfinity, float minHeight = float.NegativeInfinity) : base()
        {
            this.closed = closed;
            this.maxHeight = maxHeight;
            this.minHeight = minHeight;

            this.material = material;
        }

        public override List<Intersection> LocalIntersects(Ray objSpaceRay)
        {
            List<Intersection> intersections = new List<Intersection>();


            float a = (objSpaceRay.direction.x * objSpaceRay.direction.x) -
                      (objSpaceRay.direction.y * objSpaceRay.direction.y) +
                      (objSpaceRay.direction.z * objSpaceRay.direction.z);

            float b = (2.0f * objSpaceRay.origin.x * objSpaceRay.direction.x) -
                      (2.0f * objSpaceRay.origin.y * objSpaceRay.direction.y) +
                      (2.0f * objSpaceRay.origin.z * objSpaceRay.direction.z);

            float c = (objSpaceRay.origin.x * objSpaceRay.origin.x) -
                      (objSpaceRay.origin.y * objSpaceRay.origin.y) +
                      (objSpaceRay.origin.z * objSpaceRay.origin.z);

            if (Utilities.FloatEquality(a,0)) // Ray misses if a and b are 0
            {

                if (Utilities.FloatEquality(b, 0)) // Ray misses if a and b are 0
                {
                    return intersections;
                }
                else // Ray parallel to a side
                {
                    float t = -c / (2 * b);
                    intersections.Add(new Intersection(t, this));
                }
            } 
 

            float discriminant = (b * b) - 4 * a * c;

            if (Utilities.FloatEquality(discriminant, 0))
                discriminant = 0;
            if (discriminant < 0)   // Ray does not intersect cyclinder
                return intersections; // return empty list

            float t00 = (-b - (float)Math.Sqrt(discriminant)) / (2 * a);
            float t01 = (-b + (float)Math.Sqrt(discriminant)) / (2 * a);

            //
            if (t00 > t01)
            {
                float temp_t00 = t01;
                float temp_t01 = t00;

                t00 = temp_t00;
                t01 = temp_t01;
            }

            // Checks that the intersection points are between the min and max height
            float y00 = objSpaceRay.origin.y + t00 * objSpaceRay.direction.y;
            if (minHeight < Math.Round(y00, 4) && Math.Round(y00, 4) < maxHeight)
            {
                intersections.Add(new Intersection(t00, this));
            }

            float y01 = objSpaceRay.origin.y + t01 * objSpaceRay.direction.y;
            if (minHeight < Math.Round(y01, 4) && Math.Round(y01, 4) < maxHeight)
            {
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
        private bool CheckCap(Ray objSpaceRay, float t, float radius)
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
            if (CheckCap(objSpaceRay, tMin, Math.Abs(minHeight)))
            {
                xs.Add(new Intersection(tMin, this));
            }

            // Checks for an intersection with the upper end cap 
            // by intersecting the ray with the plane at y = cly.maxHeight
            float tMax = (maxHeight - objSpaceRay.origin.y) / objSpaceRay.direction.y;
            if (CheckCap(objSpaceRay, tMax, Math.Abs(maxHeight)))
            {
                xs.Add(new Intersection(tMax, this));
            }
        }

        public override Vector3 LocalNormal(Point objectPoint)
        {
            // Compute the square of the distance from the y-axis
            float dist = objectPoint.x * objectPoint.x + objectPoint.z * objectPoint.z + Utilities.OVER_POINT_EPSILON;

            //Console.WriteLine("distance: " + dist);

            float topCapRadius = Math.Abs(MaxHeight);
            float bottomCapRadius = Math.Abs(minHeight);

            if (dist < topCapRadius && objectPoint.y >= maxHeight - Utilities.EPSILON) // max end cap
            {
                return new Vector3(0, 1, 0);
            }
            else if (dist < bottomCapRadius && objectPoint.y <= minHeight + Utilities.EPSILON) // min end cap
            {
                return new Vector3(0, -1, 0);
            }
            else
            {
                double y = Math.Sqrt(Math.Pow(objectPoint.x, 2) + Math.Pow(objectPoint.z, 2));
                if (objectPoint.y > 0)
                {
                    y = y * -1.0f;
                }
                return new Vector3(objectPoint.x, (float)y, objectPoint.z);
            }
        }
    }
}
