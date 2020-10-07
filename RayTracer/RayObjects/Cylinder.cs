using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.RayObjects
{
    public class Cylinder : RayObject
    {

        // Instance Variables
        float radius;
        bool closed;
        float maxHeight;
        float minHeight;

        // Get/Set methods
        public float Radius
        {
            get { return radius; }
        }

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
        public Cylinder(float radius = 1.0f, bool endCaps = false,
                        float maxHeight = float.PositiveInfinity, float minHeight = float.NegativeInfinity) : base()
        {
            this.radius = radius;
            this.closed = endCaps;
            this.maxHeight = maxHeight;
            this.minHeight = minHeight; 
        }

        public Cylinder(Material material, float radius = 1.0f, bool endCaps = false,
                        float maxHeight = float.PositiveInfinity, float minHeight = float.NegativeInfinity) : base()
        {
            this.radius = radius;
            this.closed = endCaps;
            this.maxHeight = maxHeight;
            this.minHeight = minHeight;

            this.material = material;
        }

        public override List<Intersection> LocalIntersects(Ray objSpaceRay)
        {

            objSpaceRay.direction.Normalize();

            //Console.WriteLine("LocalIntersects called.");
            List<Intersection> intersections = new List<Intersection>();

            bool rayParallelToYAxis = false;
            float a = (objSpaceRay.direction.x * objSpaceRay.direction.x) + (objSpaceRay.direction.z * objSpaceRay.direction.z);
            if (Utilities.FloatEquality(a,0))  // Ray is parallel to the y-axis
            {
                //Console.WriteLine("Ray parallel to y-axis: " + a);
                //return intersections; // return empty list
                rayParallelToYAxis = true;
            }

            if (!rayParallelToYAxis)
            {
                float b = 2 * ((objSpaceRay.origin.x * objSpaceRay.direction.x)) +
                          2 * ((objSpaceRay.origin.z * objSpaceRay.direction.z));

                float c = (objSpaceRay.origin.x * objSpaceRay.origin.x) + (objSpaceRay.origin.z * objSpaceRay.origin.z) - 1;

                float discriminant = (b * b) - 4 * a * c;
                if (discriminant < 0)   // Ray doesn not intersect cyclinder
                {
                    //Console.WriteLine("Discriminant less than zero: " + discriminant);
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
                    //Console.WriteLine("t00 and t01 switched");
                }

                // Checks that the intersection points are between the min and max height
                float y00 = objSpaceRay.origin.y + t00 * objSpaceRay.direction.y;
                //Console.WriteLine("y00: " + y00);
                if (minHeight < Math.Round(y00,4) && Math.Round(y00, 4) < maxHeight)
                {
                    Intersection i00 = new Intersection(t00, this);
                    intersections.Add(i00);
                    //Console.WriteLine("i00 added to intersections" + i00);
                }

                float y01 = objSpaceRay.origin.y + t01 * objSpaceRay.direction.y;
                //Console.WriteLine("y01: " + y01);
                if (minHeight < Math.Round(y01, 4) && Math.Round(y01, 4) < maxHeight)
                {                    
                    Intersection i01 = new Intersection(t01, this);
                    intersections.Add(i01);
                    //Console.WriteLine("i01 added to intersections" + i01);
                }
            }

            // Looks for intersections with the end caps of a closed cylinder.
            //Console.WriteLine("end cap closed: " + closed);
            //Console.WriteLine("Before Caps: " + intersections.Count);
            IntersectCaps(objSpaceRay, intersections);
            //Console.WriteLine("After Caps: " + intersections.Count + "\n");

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

            Console.WriteLine("CheckCap: " + ((float)Math.Pow(x, 2) + (float)Math.Pow(z, 2) <= 1) + " ---> " + ((float)Math.Pow(x, 2) + (float)Math.Pow(z, 2)));
            return Math.Round(Math.Pow(x, 2) + Math.Pow(z, 2), 4) <= 1;
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
            if (CheckCap(objSpaceRay, tMin))
            {
                xs.Add(new Intersection(tMin, this));
            }

            // Checks for an intersection with the upper end cap 
            // by intersecting the ray with the plane at y = cly.maxHeight
            float tMax = (maxHeight - objSpaceRay.origin.y) / objSpaceRay.direction.y;
            if (CheckCap(objSpaceRay, tMax))
            {
                xs.Add(new Intersection(tMax, this));
            }
        }

        public override Vector3 LocalNormal(Point objectPoint)
        {
            return new Vector3(objectPoint.x, 0, objectPoint.z); 
        }

        public void EndCapNormals()
        {

        }
    }
}
