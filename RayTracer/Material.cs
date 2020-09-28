using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Material
    {
        //Instance Variables
        Color color = Color.White;
        float ambient = 0.1f;
        float diffuse = 0.9f;
        float specular = 0.9f;
        float shininess = 200.0f;

        // Get/Set methods
        public Color mColor
        {
            get { return color; }
            set { color = value; }
        }

        public float Ambient
        {
            get { return ambient; }
            set
            {
                if (value < 0.0)
                {
                    value = 0.0f;
                    ambient = value;
                }
                else
                {
                    ambient = value;
                }
            }
        }

        public float Diffuse
        {
            get { return diffuse; }
            set
            {
                if (value < 0.0)
                {
                    value = 0.0f;
                    diffuse = value;
                }
                else
                {
                    diffuse = value;
                }
            }
        }

        public float Specular
        {
            get { return specular; }
            set
            {
                if (value < 0.0)
                {
                    value = 0.0f;
                    specular = value;
                }
                else
                {
                    specular = value;
                }
            }
        }

        public float Shininess
        {
            get { return shininess; }
            set
            {
                if (value < 10.0)
                {
                    value = 0.0f;
                    shininess = value;
                }
                else
                {
                    shininess = value;
                }
            }
        }

        // Constructors
        /// <summary>
        /// Default Material settings constructor
        /// </summary>
        public Material()
        {
            color = Color.White;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
        }

        /// <summary>
        /// Consumer Material settings constructor
        /// </summary>
        /// <param name="color"></param>
        /// <param name="ambient"></param>
        /// <param name="diffuse"></param>
        /// <param name="specular"></param>
        /// <param name="shininess"></param>
        public Material(Color color,
                        float ambient = 0.1f, 
                        float diffuse = 0.9f, 
                        float specular = 0.9f, 
                        float shininess = 200.0f)
        {
            this.color = color;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
        }

        // Class overloads
        public override string ToString()
        {
            return "Color -> " + color + "\n" +
                   "Ambient -> " + ambient + "\n" +
                   "Diffuse -> " + diffuse + "\n" +
                   "Specular -> " + specular + "\n" +
                   "Shininess -> " + shininess;
        }

        public override bool Equals(object obj)
        {
            return obj is Material material &&
                   EqualityComparer<Color>.Default.Equals(color, material.color) &&
                   ambient == material.ambient &&
                   diffuse == material.diffuse &&
                   specular == material.specular &&
                   shininess == material.shininess &&
                   EqualityComparer<Color>.Default.Equals(mColor, material.mColor) &&
                   Ambient == material.Ambient &&
                   Diffuse == material.Diffuse &&
                   Specular == material.Specular &&
                   Shininess == material.Shininess;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(color);
            hash.Add(ambient);
            hash.Add(diffuse);
            hash.Add(specular);
            hash.Add(shininess);
            hash.Add(mColor);
            hash.Add(Ambient);
            hash.Add(Diffuse);
            hash.Add(Specular);
            hash.Add(Shininess);
            return hash.ToHashCode();
        }

        public static bool operator ==(Material m1, Material m2)
        {
            if (m1.color == m2.color &&
                m1.ambient == m2.ambient &&
                m1.diffuse == m2.diffuse &&
                m1.specular == m2.specular &&
                m1.shininess == m2.shininess)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
        
        public static bool operator !=(Material m1, Material m2)
        {
            if (m1.color == m2.color &&
                m1.ambient == m2.ambient &&
                m1.diffuse == m2.diffuse &&
                m1.specular == m2.specular &&
                m1.shininess == m2.shininess)
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
        /// Uses Phong light model to calculate surface color.
        /// </summary>
        /// <param name="material"></param>
        /// <param name="light"></param>
        /// <param name="point"></param>
        /// <param name="eyeV"></param>
        /// <param name="normalV"></param>
        public Color Lighting(Material material, Light light, Point point,
                              Vector3 eyeV, Vector3 normalV, bool inShadow) // CONSIDER removing material from parameter list. Already getting data from self
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

            // If in shadow just return the ambient color skip diffuse and specular calculations.
            if (inShadow)
                return ambient;


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
