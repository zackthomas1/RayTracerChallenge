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

        /// <summary>
        /// Uses Phong light model to caculate specular lighting on object
        /// </summary>
        /// <param name="material"></param>
        /// <param name="light"></param>
        /// <param name="point"></param>
        /// <param name="eyeV"></param>
        /// <param name="normalV"></param>
        public Color Lighting(Material material, Light light, Point point,
                            Vector3 eyeV, Vector3 normalV)
        {
            Color ambient = Color.White; 
            Color diffuse = Color.White; 
            Color specular = Color.White;

            // Combine the surface color with the light's color/intensity
            Color effect_color = material.mColor * light.Insensity;

            // find the directionto the light source 
            Vector3 lightV = (light.Position - point).Normalized();

            // Compute the ambient contribution 
            ambient = effect_color * material.Ambient;

            // lighDotNormal represents the cosine of the angle between the
            // light vector and the normal vector. A negative number means the 
            // light is on the other side of the surface. 
            float lighDotNormal = Vector3.Dot(lightV, normalV);

            if (lighDotNormal < 0)
            {
                diffuse = Color.Black;
                specular = Color.Black;
            }
            else
            {
                // Compute the diffuse contribution
                diffuse = effect_color * material.Diffuse * lighDotNormal;

                // reflectDotEye represents the cosine of the angle between the 
                // reflection vector and the eye vector. A negative number means the
                // light reflects away from the eye. 
                Vector3 reflectV = Vector3.Reflection(-lightV, normalV);
                float reflectDotEye = Tuple.Dot(reflectV, eyeV);

                if (reflectDotEye <= 0)
                {
                    specular = Color.Black;
                }
                else
                {
                    // compute the specular contribution 
                    float factor = (float)Math.Pow(reflectDotEye, material.Shininess);
                    specular = light.Insensity * material.Specular * factor;
                }
            }

            /*
            Console.WriteLine("Ambient: " + ambient.ToString());
            Console.WriteLine("Diffuse: " + diffuse.ToString());
            Console.WriteLine("Specular: " + specular.ToString());
            */

            // Add the three contributions together to get the final shading 
            return ambient + diffuse + specular;

        }
    }
}
