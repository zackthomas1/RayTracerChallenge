using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{

    public class RefractiveIndex
    {
        public const float Vacuum = 1.0f;
        public const float Air = 1.00029f;
        public const float Water = 1.333f;
        public const float Glass = 1.52f;
        public const float Diamond = 2.417f;
    }


    public class Material
    {
        //Instance Variables
        Color color = Color.White;
        float ambient = 0.1f;
        float diffuse = 0.9f;
        float specular = 0.9f;
        float shininess = 200.0f;
        Pattern pattern = null;
        float reflective = 0.0f;
        float transparency = 0.0f;
        float refractiveIndex = RefractiveIndex.Vacuum;

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

        public Pattern Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        public float Reflective
        {
            get { return reflective; }
            set { reflective = value; }
        }

        public float Transparency
        {
            get { return transparency; }
            set
            {
                if (value < 0.0f)
                {
                    value = 0.0f;
                }
                if (value > 1.0f)
                {
                    value = 1.0f;
                }

                transparency = value;
            }
        }

        public float RefractIndex
        {
            get { return refractiveIndex; }
            set { refractiveIndex = value; }
        }

        // Constructors
        /// <summary>
        /// Default Material settings constructor
        /// </summary>
        public Material()
        {
            mColor = Color.White;
            Pattern = null;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
            Reflective = reflective;
            Transparency = 0.0f;
            RefractIndex = RefractiveIndex.Air;
        }

        /// <summary>
        ///  Define material color parameter constructor
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
                        float shininess = 200.0f,
                        float reflective = 0.0f,
                        Pattern pattern = null,
                        float transparency = 0.0f,
                        float refactiveIndex = RefractiveIndex.Air)
        {
            mColor = color;
            Pattern = pattern;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
            Reflective = reflective;
            Transparency = transparency;
            RefractIndex = refactiveIndex;
        }

        public Material(Pattern pattern,
                        float ambient = 0.1f,
                        float diffuse = 0.9f,
                        float specular = 0.9f,
                        float shininess = 200.0f,
                        float reflective = 0.0f,
                        float transparency = 0.0f,
                        float refactiveIndex = RefractiveIndex.Air)
        {
            Pattern = pattern;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
            Reflective = reflective;
            Transparency = transparency;
            RefractIndex = refactiveIndex;
        }

        // Class overloads
        public override string ToString()
        {
            return "Color -> " + color + "\n" +
                   "Pattern -> " + pattern + "\n" +
                   "Ambient -> " + ambient + "\n" +
                   "Diffuse -> " + diffuse + "\n" +
                   "Specular -> " + specular + "\n" +
                   "Shininess -> " + shininess + "\n" +
                   "Reflective -> " + reflective + "\n" +
                   "Transparency -> " + transparency + "\n" +
                   "RefractiveIndex -> " + refractiveIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is Material material &&
                   EqualityComparer<Color>.Default.Equals(color, material.color) &&
                   ambient == material.ambient &&
                   diffuse == material.diffuse &&
                   specular == material.specular &&
                   shininess == material.shininess &&
                   EqualityComparer<Pattern>.Default.Equals(pattern, material.pattern) &&
                   reflective == material.reflective &&
                   transparency == material.transparency &&
                   refractiveIndex == material.refractiveIndex &&
                   EqualityComparer<Color>.Default.Equals(mColor, material.mColor) &&
                   Ambient == material.Ambient &&
                   Diffuse == material.Diffuse &&
                   Specular == material.Specular &&
                   Shininess == material.Shininess &&
                   EqualityComparer<Pattern>.Default.Equals(Pattern, material.Pattern) &&
                   Reflective == material.Reflective &&
                   Transparency == material.Transparency &&
                   RefractIndex == material.RefractIndex;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(color);
            hash.Add(ambient);
            hash.Add(diffuse);
            hash.Add(specular);
            hash.Add(shininess);
            hash.Add(pattern);
            hash.Add(reflective);
            hash.Add(transparency);
            hash.Add(refractiveIndex);
            hash.Add(mColor);
            hash.Add(Ambient);
            hash.Add(Diffuse);
            hash.Add(Specular);
            hash.Add(Shininess);
            hash.Add(Pattern);
            hash.Add(Reflective);
            hash.Add(Transparency);
            hash.Add(RefractIndex);
            return hash.ToHashCode();
        }

        public static bool operator ==(Material m1, Material m2)
        {
            if (m1.color == m2.color &&
                m1.pattern == m2.pattern &&
                m1.ambient == m2.ambient &&
                m1.diffuse == m2.diffuse &&
                m1.specular == m2.specular &&
                m1.shininess == m2.shininess &&
                m1.reflective == m2.reflective &&
                m1.transparency == m2.transparency &&
                m1.refractiveIndex == m2.refractiveIndex)
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
                m1.pattern == m2.pattern &&
                m1.ambient == m2.ambient &&
                m1.diffuse == m2.diffuse &&
                m1.specular == m2.specular &&
                m1.shininess == m2.shininess &&
                m1.reflective == m2.reflective &&
                m1.transparency == m2.transparency &&
                m1.refractiveIndex == m2.refractiveIndex)

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
        public Color Lighting(Material material, RayObject rayObject, Light light, Point point,
                              Vector3 eyeV, Vector3 normalV, Tuple<bool, RayObject> inShadow) // CONSIDER removing material from parameter list. Already getting data from self. Also CONSIDER moving RayObject call.
        {
            Color ambient = Color.White;
            Color diffuse = Color.White;
            Color specular = Color.White;

            Color effect_color;
            if (pattern != null)
            {
                // Combines surface pattern color with light's color/intensity if a pattern exist
                effect_color = pattern.PatternAtObject(rayObject, point) * light.Insensity;
            }
            else
            {
                // Combines surface color with light's color/intensity
                effect_color = material.mColor * light.Insensity;
            }


            // find the direction to the light source 
            Vector3 lightV = (light.Position - point).Normalized();

            // Compute the ambient contribution 
            ambient = effect_color * material.Ambient;

            // If in shadow just return the ambient color AND
            // if object that the ray hits on the way to the light source is completely non-transparent 
            // skip diffuse and specular calculations return ambient color.
            if (inShadow.Item1 && (Utilities.FloatEquality(inShadow.Item2.material.transparency, 0.0f)))
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

                // if reflection is away from eye
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

            // Checks to see if the point is in shadow AND If the object has transparency
            if (inShadow.Item1 && !(Utilities.FloatEquality(inShadow.Item2.material.transparency, 0.0f)))
            {
                return ambient + ((diffuse + specular) * (inShadow.Item2.material.transparency * .95f));
            }
     
            // Add the three contributions together to get the final shading 
            return ambient + diffuse + specular;
        }

        /// <summary>
        /// Old Lighting method. Uses bool type for determining if in shadow.
        /// Doesn't adjust shadow darkness based on transparency of object blocking light
        /// Legacy method. Will Remove eventually. 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="rayObject"></param>
        /// <param name="light"></param>
        /// <param name="point"></param>
        /// <param name="eyeV"></param>
        /// <param name="normalV"></param>
        /// <param name="inShadow"></param>
        /// <returns></returns>
        public Color Lighting(Material material, RayObject rayObject, Light light, Point point,
                             Vector3 eyeV, Vector3 normalV, bool inShadow) // LEGACY // CONSIDER removing material from parameter list. Already getting data from self. Also CONSIDER moving RayObject call.
        {
            Color ambient = Color.White;
            Color diffuse = Color.White;
            Color specular = Color.White;

            Color effect_color;
            if (pattern != null)
            {
                // Combines surface pattern color with light's color/intensity if a pattern exist
                effect_color = pattern.PatternAtObject(rayObject, point) * light.Insensity;
            }
            else
            {
                // Combines surface color with light's color/intensity
                effect_color = material.mColor * light.Insensity;
            }


            // find the direction to the light source 
            Vector3 lightV = (light.Position - point).Normalized();

            // Compute the ambient contribution 
            ambient = effect_color * material.Ambient;

            // If in shadow just return the ambient color AND
            // if object that the ray hits on the way to the light source is completely non-transparent 
            // skip diffuse and specular calculations return ambient color.
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

                // if reflection is away from eye
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
