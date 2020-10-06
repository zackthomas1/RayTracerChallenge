using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Cube : RayObject
    {
        // Instance Variables

        // Get/Set methods

        // Constructors
        public Cube()
        {

        }

        // Class overloads

        // Methods

        public override List<Intersection> Intersect(Ray ray)
        {
            Ray transRay = RayToObjectSpace(ray);
            List<Intersection> xs;

            float x_tMin = CheckAxis(transRay.origin.x, transRay.direction.x)[0];
            float x_tMax = CheckAxis(transRay.origin.x, transRay.direction.x)[1];

            float y_tMin = CheckAxis(transRay.origin.y, transRay.direction.y)[0];
            float y_tMax = CheckAxis(transRay.origin.y, transRay.direction.y)[1];

            float z_tMin = CheckAxis(transRay.origin.z, transRay.direction.z)[0];
            float z_tMax = CheckAxis(transRay.origin.z, transRay.direction.z)[1];

            float tMin = Math.Max(x_tMin, Math.Max(y_tMin, z_tMin));
            float tMax = Math.Min(x_tMax, Math.Min(y_tMax, z_tMax));

            if (tMin > tMax)
            {
                xs = new List<Intersection>() { };
                return xs;
            }

            Intersection i01 = new Intersection(tMin, this);
            Intersection i02 = new Intersection(tMax, this);
            xs = new List<Intersection>() { i01, i02 };

            return xs;

        }

        public override Vector3 CalculateLocalNormal(Point objectPoint)
        {
            double maxC = Math.Max(Math.Abs(objectPoint.x), Math.Max(Math.Abs(objectPoint.y), Math.Abs(objectPoint.z))); 

            if (maxC == Math.Abs(objectPoint.x))
                return new Vector3(objectPoint.x, 0, 0);
            else if (maxC == Math.Abs(objectPoint.y))
                return new Vector3(0, objectPoint.y, 0);
            else
                return new Vector3(0, 0, objectPoint.z);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private float[] CheckAxis(float origin, float direction)
        {
            float tMinNumerator = (-1 - origin);
            float tMaxNumerator = (1 - origin);

            float tMin;
            float tMax;

            if ((float)Math.Abs(direction) >= Utilities.EPSILON)
            {
                tMin = tMinNumerator / direction;
                tMax = tMaxNumerator / direction;
            }
            else
            {
                tMin = tMinNumerator * Utilities.INFINITY;
                tMax = tMaxNumerator * Utilities.INFINITY;
            }

            if(tMin > tMax)
            {
                float tempTMin = tMax;
                float tempTMax = tMin;

                tMin = tempTMin;
                tMax = tempTMax;        
            }

            float[] result = new float[] { tMin, tMax };
            return result;
        }
    }
}
