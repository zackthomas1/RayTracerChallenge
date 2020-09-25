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
            return "Intersect Object: " + rayObject.ID + " t: " + t.ToString();       
        }

        /// <summary>
        /// Sorts a list of Intersect class variables into non-decending order(increasing).
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <returns></returns>
        public static List<Intersection> Sort(List<Intersection> intersections)
        {
            
            for(int currentIndex = 1; currentIndex < intersections.Count; currentIndex++)
            {
                Intersection key = intersections[currentIndex];
                int previousIndex = currentIndex - 1;

                while (previousIndex >= 0 && intersections[previousIndex].t > key.t)
                {
                    intersections[previousIndex + 1] = intersections[previousIndex];
                    previousIndex = previousIndex - 1;
                }
                intersections[previousIndex + 1] = key;
            }

            return intersections;
        }

        /// <summary>
        /// Determines which Ray-RayObject intersection is the hit intersection.(aka. Which has the lowest 't' value above 0.)
        /// Uses Sort method from Intersection class to arrange list in non-decending order
        /// </summary>
        /// <param name="intersections"></param>
        /// <returns></returns>
        public static Intersection Hit(List<Intersection> intersections)
        {
            List<Intersection> sortedIntersections = Intersection.Sort(intersections);

            for (int index = 0; index < sortedIntersections.Count; index++)
            {
                if (sortedIntersections[index].t > 0)
                {
                    return sortedIntersections[index]; 
                }
            }

            return null;
        }


    }
}
