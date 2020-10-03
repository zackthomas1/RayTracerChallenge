using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer
{
    public class Computation
    {
        //Instance Variables
        public float t { get; set; } // Stores the t-value for where the object intersection occured along the ray
        public RayObject rayObject { get; set; }
        public Point point { get; set; } // Intersection point between ray and object
        public Vector3 eyeV { get; set; } // 
        public Vector3 normalV { get; set; } // 
        public bool inside { get; set; } // If ray originates from inside of object 
        public Point overPoint { get; set; }
        public Vector3 reflectV { get; set; }
        public float n1 { get; set; } = 0.0f;
        public float n2 { get; set; } = 0.0f;
        public Point underPoint { get; set; }


        // Get/Set methods

        // Constructors
        public Computation()
        {

        }

        //public Computation(float t, RayObject rayObject, 
        //                    Point point, Vector3 eyeV, 
        //                    Vector3 normalV, bool inside, 
        //                    Vector3 reflectV, float n1, 
        //                    float n2)
        //{
        //    this.t = t;
        //    this.rayObject = rayObject;
        //    this.point = point;
        //    this.eyeV = eyeV;
        //    this.normalV = normalV;
        //    this.inside = inside;

        //    overPoint = point + normalV * Utilities.shadowPointEpsilon;

        //    this.reflectV = reflectV;

        //    this.n1 = n1;
        //    this.n2 = n2;

        //}

        public Computation(Intersection i, Ray r, List<Intersection> xs = null)
        {

            this.t = i.t;
            this.rayObject = i.rayObject;
            this.point = r.GetPointPosition(i.t);
            this.eyeV = -r.direction;
            this.normalV = rayObject.GetNormal(point);

            // Checks if eye vector and normal are pointing in the same direction 
            // if dot product is less than zero that point in the same direction. 
            if (Vector3.Dot(eyeV, normalV) < 0)
            {
                inside = true;
                normalV = -normalV;
            }
            else
                inside = false;


            // If a List<Intersection> parameter is not passed into the method 
            // create one using i as the only intersection in the list
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
                        this.n1 = 1.0f;
                    }
                    else
                    {
                        this.n1 = containers.Last<RayObject>().material.RefractIndex;
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
                        this.n2 = 1.0f;
                    }
                    else
                    {
                        this.n2 = containers[containers.Count - 1].material.RefractIndex;
                    }
                    break;
                }
            }
           
            this.reflectV = Vector3.Reflection(r.direction, this.normalV);
            this.overPoint = point + normalV * Utilities.overPointEpsilon;
            this.underPoint = point - normalV * Utilities.underPointEpsilon; 
        }

        // override methods 
        public override string ToString()
        {
            return "t-value ->" + t + "\n" +
            "RayObject ->" + rayObject + "\n" +
            "Point ->" + point + "\n" +
            "Eye Vector ->" + eyeV + "\n" +
            "Normal Vector->" + normalV + "\n" +
            "Inside Object ->" + inside + "\n" +
            "Over Point ->" + overPoint + "\n" +
            "Reflection Vector ->" + reflectV + "\n" +
            "n1 ->" + n1 + "\n" +
            "n2 ->" + n2 + "\n";
        }

        public override bool Equals(object obj)
        {
            return obj is Computation computation &&
                   t == computation.t &&
                   EqualityComparer<RayObject>.Default.Equals(rayObject, computation.rayObject) &&
                   EqualityComparer<Point>.Default.Equals(point, computation.point) &&
                   EqualityComparer<Vector3>.Default.Equals(eyeV, computation.eyeV) &&
                   EqualityComparer<Vector3>.Default.Equals(normalV, computation.normalV) &&
                   inside == computation.inside &&
                   EqualityComparer<Point>.Default.Equals(overPoint, computation.overPoint) &&
                   EqualityComparer<Vector3>.Default.Equals(reflectV, computation.reflectV) &&
                   n1 == computation.n1 &&
                   n2 == computation.n2;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(t);
            hash.Add(rayObject);
            hash.Add(point);
            hash.Add(eyeV);
            hash.Add(normalV);
            hash.Add(inside);
            hash.Add(overPoint);
            hash.Add(reflectV);
            hash.Add(n1);
            hash.Add(n2);
            return hash.ToHashCode();
        }

        public static bool operator ==(Computation comp01, Computation comp02)
        {
            if ( comp01.t == comp02.t &&
                 comp01.rayObject == comp02.rayObject &&
                 comp01.point == comp02.point &&
                 comp01.eyeV == comp02.eyeV &&
                 comp01.normalV == comp02.normalV &&
                 comp01.inside == comp02.inside &&
                 comp01.overPoint == comp02.overPoint &&
                 comp01.reflectV == comp02.reflectV &&
                 comp01.n1 == comp02.n1 &&
                 comp01.n2 == comp02.n2
                 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Computation comp01, Computation comp02)
        {
            if (comp01.t == comp02.t &&
                 comp01.rayObject == comp02.rayObject &&
                 comp01.point == comp02.point &&
                 comp01.eyeV == comp02.eyeV &&
                 comp01.normalV == comp02.normalV &&
                 comp01.inside == comp02.inside &&
                 comp01.overPoint == comp02.overPoint &&
                 comp01.reflectV == comp02.reflectV &&
                 comp01.n1 == comp02.n1 &&
                 comp01.n2 == comp02.n2
                 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }



    }
}
