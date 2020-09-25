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
        protected static int currentID = 0;
        protected int id;
        public Matrix4 transformMatrix = new Matrix4();
        public Material material;

        /// <summary>
        /// Get/set RayObject ID.
        /// </summary>
        public int ID
        {
            get { return id; }
            private set { id = value; }
        }

        /// <summary>
        /// Get/Set RayObject's transformMatrix
        /// </summary>
        public Matrix4 TransformMatrix
        {
            get { return transformMatrix; }
            set { transformMatrix = value; }
        }



        public RayObject()
        {
            id = currentID++;
            material = new Material();
        }

        public RayObject(Material material)
        {
            id = currentID++;
            this.material = material;
        }

        public override string ToString()
        {
            return "RayObject_" + id.ToString();
        }

        public Matrix4 GetTransform(Matrix4 matrix)
        {
            return transformMatrix;
        }

        public void SetTranform(Matrix4 matrix)
        {
            this.transformMatrix = matrix;
        }

        public abstract List<Intersection> Intersect(Ray ray);

  
    }
}
