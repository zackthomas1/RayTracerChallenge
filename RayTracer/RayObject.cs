using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Windows.Markup;

namespace RayTracer
{
    public abstract class RayObject
    {
        // Instance Variables
        protected static int currentID = 0;
        protected int id;
        Matrix4 transformMatrix = new Matrix4();
        Point position;
        public Material material;

        // Get/Set methods
        public int ID
        {
            get { return id; }
            private set { id = value; }
        }
        public Matrix4 TransformMatrix
        {
            get { return transformMatrix; }
            set { transformMatrix = value; }
        }
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        // Constructors
        public RayObject()
        {
            id = currentID++;
            material = new Material();
        }

        //public RayObject(Material material)
        //{
        //    id = currentID++;
        //    this.material = material;
        //}

        // Class overloads
        public override string ToString()
        {
            return "RayObject_" + id.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is RayObject @object &&
                   id == @object.id &&
                   EqualityComparer<Matrix4>.Default.Equals(transformMatrix, @object.transformMatrix) &&
                   EqualityComparer<Point>.Default.Equals(position, @object.position) &&
                   EqualityComparer<Material>.Default.Equals(material, @object.material) &&
                   ID == @object.ID &&
                   EqualityComparer<Matrix4>.Default.Equals(TransformMatrix, @object.TransformMatrix) &&
                   EqualityComparer<Point>.Default.Equals(Position, @object.Position);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, transformMatrix, position, material, ID, TransformMatrix, Position);
        }

        public static bool operator ==(RayObject obj1, RayObject obj2)
        {
            if(obj1.Position == obj2.Position && obj1.material == obj2.material)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(RayObject obj1, RayObject obj2)
        {
            if (obj1.Position == obj2.Position && obj1.material == obj2.material)
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
        /// Determines if a given ray intersects a given RayObject
        /// Parent class method. Each child class will implement its own method
        /// for determining intersection.
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public abstract List<Intersection> Intersect(Ray ray);

        /// <summary>
        /// Given a position in world-coordinates finds the normal 
        /// of rayObject to that given point. 
        /// </summary>
        /// <param name="worldPoint"></param>
        /// <returns></returns>
        public abstract Vector3 GetNormal(Point worldPoint);

        /// <summary>
        /// Takes the input ray applies RayObjects tranformMatrix. 
        /// Taking transformations on the object and putting them on the ray.
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        virtual protected Ray RayToObjectSpace(Ray ray)
        {
            return ray * transformMatrix.Invert();
        }
     
    }
}
