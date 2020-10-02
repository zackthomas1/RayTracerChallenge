using System;
using System.Collections.Generic;
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

        // Get/Set methods

        // Constructors
        public Computation()
        {

        }

        public Computation(float t, RayObject rayObject, 
                            Point point, Vector3 eyeV, 
                            Vector3 normalV, bool inside, Vector3 reflectV)
        {
            this.t = t;
            this.rayObject = rayObject;
            this.point = point;
            this.eyeV = eyeV;
            this.normalV = normalV;
            this.inside = inside;

            overPoint = point + normalV * Utilities.shadowPointEpsilon;

            this.reflectV = reflectV;

        }

        public Computation(Intersection i, Ray r)
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

            this.overPoint = point + normalV * Utilities.shadowPointEpsilon;

            this.reflectV = Vector3.Reflection(r.direction, this.normalV);
        }

    }
}
