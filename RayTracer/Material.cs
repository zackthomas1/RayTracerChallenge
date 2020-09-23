using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Material
    {

        Color color = Color.White();
        float ambient = 0.1f;
        float diffuse = 0.9f;
        float specular = 0.9f;
        float shininess = 200.0f;

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



        /// <summary>
        /// Default Material settings constructor
        /// </summary>
        public Material()
        {
            color = RayTracer.Color.White();
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

        public override string ToString()
        {
            return "Color -> " + color + "\n" +
                   "Ambient -> " + ambient + "\n" +
                   "Diffuse -> " + diffuse + "\n" +
                   "Specular -> " + specular + "\n" +
                   "Shininess -> " + shininess;
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


    }
}
