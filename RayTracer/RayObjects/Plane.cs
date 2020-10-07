using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Plane : RayObject
    {

        // Instance Variables

        // Get/Set methods

        // Constructors
        public Plane() : base()
        {

        }

        public Plane(Material material) : base()
        {
            this.material = material;
        }

        // Methods
        public override List<Intersection> LocalIntersects(Ray objSpaceRay)
        {
            List<Intersection> intersections = new List<Intersection>();

            // if the ray direction vector has NO slope in the y
            // it will either never intersect the plane or intersect it infinite times.
            // In either case return nothing(null)
            if (Math.Abs(objSpaceRay.direction.y) < Utilities.EPSILON)
                return null;


            Intersection i1 = new Intersection(0,this);
            // only works to describe an intersection when the plane sits on the XZ plane position (0,0,0)
            i1.t = -objSpaceRay.origin.y / objSpaceRay.direction.y;

            intersections.Add(i1);
            return intersections;
        }

        public override Vector3 LocalNormal(Point objectPoint)
        {
            Vector3 objectNormal = new Vector3(0, 1, 0);
            objectNormal.w = 0;
            objectNormal.Normalize();

            return objectNormal;
        }

    }
}
