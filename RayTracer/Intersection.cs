using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Intersection
    {
        // Instance Variables
        public float t; // Stores the t-value for where the object intersection occured along the ray
        public RayObject rayObject;

        // Get/Set methods

        // Constructors
        public Intersection(float t, RayObject rayObject)
        {
            this.t = t;
            this.rayObject = rayObject; 
        }

        // Class overloads
        public override string ToString()
        {
            return "Intersect Object: " + rayObject.ID + " t: " + t.ToString();       
        }

        // Methods
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

            return null; // If no intersection with a t-value greater than zero is found returns null
        }

        /// <summary>
        /// Given an Intersection and a Ray 
        /// evaluates for eye vector, normal vector, insection point, 
        /// and if ray is inside of object.
        /// This is the same as calling the Computation class constructor with Intersection and Ray parameter
        /// </summary>
        /// <param name="i"></param>
        /// <param name="r"></param>
        public static Computation PrepareComputations(Intersection i,Ray r)
        {
            Computation comp = new Computation();
            comp.t = i.t;
            comp.rayObject = i.rayObject;
            comp.point = r.GetPointPosition(i.t);
            comp.eyeV = -r.direction;
            comp.normalV = comp.rayObject.GetNormal(comp.point);

            if (Vector3.Dot(comp.eyeV, comp.normalV) < 0)
            {
                comp.inside = true;
                comp.normalV = -comp.normalV;
            }
            else
                comp.inside = false;

            comp.overPoint = comp.point + comp.normalV * Utilities.shadowPointEpsilon;

            comp.reflectV = Vector3.Reflection(-r.direction, comp.normalV);

            return comp;
        }


    }
}
