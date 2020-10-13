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
                tMin = tMinNumerator * float.PositiveInfinity;
                tMax = tMaxNumerator * float.PositiveInfinity; ;
            }

            if (tMin > tMax)
            {
                float tempTMin = tMax;
                float tempTMax = tMin;

                tMin = tempTMin;
                tMax = tempTMax;
            }

            float[] result = new float[] { tMin, tMax };
            return result;
        }

        public override List<Intersection> LocalIntersects(Ray objSpaceRay)
        {
            List<Intersection> intersections = new List<Intersection>();

            float x_tMin = CheckAxis(objSpaceRay.origin.x, objSpaceRay.direction.x)[0];
            float x_tMax = CheckAxis(objSpaceRay.origin.x, objSpaceRay.direction.x)[1];

            float y_tMin = CheckAxis(objSpaceRay.origin.y, objSpaceRay.direction.y)[0];
            float y_tMax = CheckAxis(objSpaceRay.origin.y, objSpaceRay.direction.y)[1];

            float z_tMin = CheckAxis(objSpaceRay.origin.z, objSpaceRay.direction.z)[0];
            float z_tMax = CheckAxis(objSpaceRay.origin.z, objSpaceRay.direction.z)[1];

            float tMin = Math.Max(x_tMin, Math.Max(y_tMin, z_tMin));
            float tMax = Math.Min(x_tMax, Math.Min(y_tMax, z_tMax));

            if (tMin > tMax)
            {
                return intersections;
            }

            Intersection i01 = new Intersection(tMin, this);
            Intersection i02 = new Intersection(tMax, this);
            intersections = new List<Intersection>() { i01, i02 };

            return intersections;

        }

        public override Vector3 LocalNormal(Point objectPoint)
        {
            double maxC = Math.Max(Math.Abs(objectPoint.x), Math.Max(Math.Abs(objectPoint.y), Math.Abs(objectPoint.z))); 

            if (maxC == Math.Abs(objectPoint.x))
                return new Vector3(objectPoint.x, 0, 0);
            else if (maxC == Math.Abs(objectPoint.y))
                return new Vector3(0, objectPoint.y, 0);
            else
                return new Vector3(0, 0, objectPoint.z);

        }

    }
}
