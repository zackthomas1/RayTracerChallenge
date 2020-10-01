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
            this.Position = new Point(0, 0, 0);
        }

        public Plane(Material material) : base()
        {
            this.Position = new Point(0, 0, 0);
            this.material = material;
        }

        // Methods
        public override List<Intersection> Intersect(Ray ray)
        {
            List<Intersection> result = new List<Intersection>();

            // Takes the input ray and applies all object transfomations.
            Ray transRay = RayToObjectSpace(ray);
            //Ray transRay = ray.ApplyObjectTransform(this); // alt method

            // if the ray direction vector has NO slope in the y
            // it will either never intersect the plane or intersect it infinite times.
            // In either case return nothing(null)
            if (Math.Abs(transRay.direction.y) < Utilities.Epsilon)
                return null;


            Intersection i1 = new Intersection(0,this);
            // only works to describe an intersection when the plane sits on the XZ plane position (0,0,0)
            i1.t = -transRay.origin.y / transRay.direction.y;

            result.Add(i1);
            return result;
        }

        public override Vector3 GetNormal(Point worldPoint)
        {
            Vector3 objectNormal = new Vector3(0, 1, 0);
            Vector3 worldNormal = this.Transform.Invert().Transpose() * objectNormal;
            worldNormal.w = 0;

            worldNormal.Normalize();

            return worldNormal;
        }

    }
}
