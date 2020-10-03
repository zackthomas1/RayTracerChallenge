using System;
using System.Collections.Generic;
using System.Linq;
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
        public static Computation PrepareComputations(Intersection i, Ray r, List<Intersection> xs = null)
        {
            Computation comp = new Computation();
            comp.t = i.t;
            comp.rayObject = i.rayObject;
            comp.point = r.GetPointPosition(i.t);
            comp.eyeV = -r.direction;
            comp.normalV = comp.rayObject.GetNormal(comp.point);

            // Checks if eye vector and normal are pointing in the same direction 
            // if dot product is less than zero that point in the same direction
            if (Vector3.Dot(comp.eyeV, comp.normalV) < 0)
            {
                comp.inside = true;
                comp.normalV = -comp.normalV;
            }
            else
                comp.inside = false;


            if (xs == null)
                xs = new List<Intersection>() { i };

            // Transparency Intersections algorithm
            List<RayObject> containers = new List<RayObject>();
            foreach (Intersection intersect in xs)
            {
                // n1
                if (i == intersect)
                {
                    if (containers.Count == 0)
                    {
                        comp.n1 = 1.0f;
                    }
                    else
                    {
                        comp.n1 = containers.Last<RayObject>().material.RefractIndex;
                    }
                }

                if (containers.Contains(intersect.rayObject))
                {
                    containers.Remove(intersect.rayObject);
                    //Console.WriteLine("Object Removed: " + intersect.rayObject.ToString());
                }
                else
                {
                    containers.Add(intersect.rayObject);
                    //Console.WriteLine("Object Added: " + intersect.rayObject.ToString());
                }
                //Console.WriteLine("List Lenght: " + containers.Count);

                // n2
                if (i == intersect)
                {
                    if (containers.Count == 0)
                    {
                        comp.n2 = 1.0f;
                    }
                    else
                    {
                        comp.n2 = containers[containers.Count - 1].material.RefractIndex;
                    }
                    break;
                }
                
            }

            comp.reflectV = Vector3.Reflection(r.direction, comp.normalV);
            comp.overPoint = comp.point + comp.normalV * Utilities.overPointEpsilon;
            comp.underPoint = comp.point - comp.normalV * Utilities.underPointEpsilon;

            return comp;
        }



    }
}
