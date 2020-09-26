using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer
{
    public class Sphere : RayObject
    {
        //Instance Variables
        float radius;

        // Get/Set methods
        public float Radius
        {
            get { return radius; }
        }

        // Constructors
        public Sphere(float radius = 1.0f) : base()
        {
            this.Position = new Point(0,0,0);
            this.radius = radius;
        }

        public Sphere(Material material, float radius = 1.0f) : base()
        {
            this.Position = new Point(0, 0, 0);
            this.radius = radius;
            this.material = material;
        }

        // Class overloads
        public override string ToString()
        {
            return "Sphere_" + id.ToString();
        }
        public override bool Equals(object obj)
        {
            return obj is Sphere sphere &&
                   base.Equals(obj) &&
                   id == sphere.id &&
                   EqualityComparer<Material>.Default.Equals(material, sphere.material) &&
                   ID == sphere.ID &&
                   EqualityComparer<Matrix4>.Default.Equals(TransformMatrix, sphere.TransformMatrix) &&
                   EqualityComparer<Point>.Default.Equals(Position, sphere.Position) &&
                   radius == sphere.radius &&
                   Radius == sphere.Radius;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), id, material, ID, TransformMatrix, Position, radius, Radius);
        }

        public static bool operator ==(Sphere s1, Sphere s2)
        {
            if (s1.Position == s2.Position && s1.radius == s2.radius && s1.material == s2.material)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Sphere s1, Sphere s2)
        {
            if (s1.Position == s2.Position && s1.radius == s2.radius && s1.material == s2.material)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Methods
        /// <summary>
        /// Caclulates the intersect of a Ray and a Sphere. 
        /// Returns a list of those intersections. 
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public override List<Intersection> Intersect(Ray ray)
        {

            List<Intersection> intersectionPoints = new List<Intersection>();

            // The vector from the sphere's center, to the ray origin.
            Vector3 sphereToRay = ray.origin - this.Position; // Sphere centered at the origin (0, 0, 0).

            double a = Tuple.Dot(ray.direction, ray.direction);
            double b = 2 * Tuple.Dot(ray.direction, sphereToRay);
            double c = Tuple.Dot(sphereToRay, sphereToRay) - 1.0f;

            double discriminant = (b * b) - 4 * a * c;     

            if (discriminant < 0)
            {
                return intersectionPoints;
            }

            double t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            double t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            intersectionPoints.Add(new Intersection((float)t1, this));
            intersectionPoints.Add(new Intersection((float)t2, this));

            return intersectionPoints;
        }

        /// <summary>
        /// Given a position in world-coordinates finds the normal 
        /// of sphere to that given point. 
        /// </summary>
        /// <param name="worldPoint"></param>
        /// <returns></returns>
        public Vector3 GetNormal(Point worldPoint)
        {
            Point objectPoint = this.TransformMatrix.Invert() * worldPoint;
            Vector3 objectNormal = objectPoint - new Point(0, 0, 0);
            Vector3 worldNormal = this.TransformMatrix.Invert().Transpose() * objectNormal;
            worldNormal.w = 0;

            worldNormal.Normalize();

            return worldNormal;
        }

    }
}
