using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Intersection
    {
        public float t;
        public RayObject rayObject; 

        public Intersection(float t, RayObject rayObject)
        {
            this.t = t;
            this.rayObject = rayObject; 
        }

        public override string ToString()
        {
            return "Intersect Object: " + rayObject.ID + " t: " + this.t.ToString();       
        }

        /// <summary>
        /// Use to manually create a list of intersections.
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <returns></returns>
        public static List<Intersection> Intersections(Intersection i1, Intersection i2)
        {
            return new List<Intersection>() {i1, i2}; 
        }

        /// <summary>
        /// Determines which Ray-RayObject intersection is the Hit intersection. 
        /// it. Which has the lowest 't' value above 0.
        /// </summary>
        /// <param name="intersections"></param>
        /// <returns></returns>
        public static Intersection Hit(List<Intersection> intersections)
        {
            float lowT = 0;
            Intersection hit = null;

            foreach (Intersection intersect in intersections)
            {
                if (lowT == 0 && intersect.t > 0)
                {
                    hit = intersect;
                    lowT = intersect.t;
                }else if(intersect.t < lowT && intersect.t > 0)
                {
                    hit = intersect;
                    lowT = intersect.t;
                }
            }
            return hit;
        }


    }
}
