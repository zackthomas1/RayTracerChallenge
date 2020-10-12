using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace RayTracer
{
    public class Scene
    {
        // Instance Variables
        List<Light> lights = new List<Light>();
        List<RayObject> objects = new List<RayObject>();

        private const int reflectionBounces = 5;

        // Get/Set methods
        public List<Light> Lights
        {
            get { return lights; }
            set { lights = value;  }
        }

        public List<RayObject> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        // Constructors
        public Scene()
        {
            DefaultScene();
        }

        //
        public override string ToString()
        {
            String result = "";

            foreach(Light light in lights)
            {
                result += light.ToString() + " ";
            }
            result += "\nObjects: \n";
            foreach (RayObject obj in objects)
            {
                result += "    " + obj.ToString() + "\n"; 
            }

            return result;
            
        }

        // Methods
        /// <summary>
        /// Sets up a defual matching book describtion (pg. 92)
        /// </summary>
        private void DefaultScene()
        {
            Light lightDefault = new Light(Color.White, new Point(-10, 10, -10));
            lights.Add(lightDefault);

            Material m1 = new Material(new Color(0.8f, 1.0f, 0.6f), diffuse: 0.7f, specular: 0.2f);
            Sphere unitSphere = new Sphere(m1, radius: 1.0f);
            Sphere halfUnitSphere = new Sphere(radius: 1.0f);
            halfUnitSphere.Transform = Matrix4.ScaleMatrix(0.5f, 0.5f, 0.5f);
           
            objects.Add(unitSphere);
            objects.Add(halfUnitSphere);
        }

        /// <summary>
        /// Finds intersections with a ray and every object in a scene.
        /// aka intersect_world
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public List<Intersection> Intersections(Ray ray)
        {

            List<Intersection> result = new List<Intersection>();

            foreach (RayObject obj in objects)
            {
                List<Intersection> objIntersects = obj.GetIntersects(ray);
                if (objIntersects != null)
                    result.AddRange(objIntersects);
            }

            return result;
        }
    
        /// <summary>
        /// Add a Rayobject to scene
        /// </summary>
        /// <param name="rayObject"></param>
        public void AddObject(RayObject rayObject)
        {
            this.objects.Add(rayObject);
        }

        /// <summary>
        /// Sets objects varible to an empty List<RayObject>()
        /// </summary>
        public void EmptyObjects()
        {
            this.objects = new List<RayObject>() { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="light"></param>
        public void AddLight(Light light)
        {
            this.lights.Add(light);
        }

        /// <summary>
        /// Sets lights varible to an empty List<Light>()
        /// </summary>
        public void EmptyLights()
        {
            this.lights = new List<Light>() { };
        }

        /// <summary>
        /// Finds color value at given intersection encapsulated in Computation class parameter
        /// </summary>
        /// <param name="comps"></param>
        /// <returns></returns>
        public Color ShadeHit(Computation comps, int remaining = reflectionBounces)
        {
            //Material mat = comp.rayObject.material;
            Scene scene = this;

            Color surfaceColor = Color.Black;

            // for scenes with multiple lights 
            foreach (Light light in lights)
            {
                Tuple<bool, RayObject> inShadow = scene.IsShadowedObjectInfo(comps.overPoint, light);
                surfaceColor += comps.rayObject.material.Lighting(comps.rayObject.material, comps.rayObject, 
                                                                light, comps.overPoint, comps.eyeV, 
                                                                comps.normalV, inShadow);

            }

            Color reflectedColor = this.ReflectedColor(comps, remaining);
            Color refractedColor = this.RefractedColor(comps, remaining);

            Material material = comps.rayObject.material; 

            // Fresnel 
            if ( material.Reflective > 0 && material.Transparency > 0)
            {
                float reflectance = comps.Schlick();
                return surfaceColor + reflectedColor * reflectance + refractedColor * (1 - reflectance);  
            }

            return surfaceColor + reflectedColor + refractedColor ;
        }

        /// <summary>
        /// Finds color value given a Ray and Scene
        /// Use this method for outside calls to find color value.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="ray"></param>
        /// <returns></returns>
        public Color ColorAt(Ray ray, int remaining = reflectionBounces)
        {
            Color resultColor = Color.Black;
            Scene scene = this;

            List<Intersection> intersections = scene.Intersections(ray);
            
            Intersection hit = Intersection.Hit(intersections);
            if (hit == null)
                return resultColor;

            Computation comp = new Computation(hit, ray);
            resultColor = ShadeHit(comp, remaining);

            return resultColor;
        }

        //Consider moving to light class
        /// <summary>
        /// Determines if a point is in shadow
        /// Returns true-false and object in ray-path between intersection and light 
        /// aka isShadowed()
        /// </summary>
        /// <param name="light"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public Tuple<bool,RayObject> IsShadowedObjectInfo(Point point, Light light)
        {
            // this needs to work for every light in the scene
            Scene scene = this;

            Vector3 pointToLight = light.Position - point;
            float distance = pointToLight.Magnitude();
            Vector3 direction = pointToLight.Normalized();

            Ray ray = new Ray(point, direction);

            List<Intersection> intersects = scene.Intersections(ray);
            Intersection hit = Intersection.Hit(intersects);

            //Do we have a hit and is it within the distance to the light?
            if (hit != null && hit.t < distance)
                return new Tuple<bool, RayObject>(true, hit.rayObject);
            else
                return new Tuple<bool, RayObject>(false, null);
        }

        /// <summary>
        /// Determines if a point is in shadow
        /// Legacy. Will Remove. Doesn't pass object blocking light. 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="light"></param>
        /// <returns></returns>
        public bool IsShadowed(Point point, Light light) // LEGACY
        {
            // this needs to work for every light in the scene
            Scene scene = this;

            Vector3 pointToLight = light.Position - point;
            float distance = pointToLight.Magnitude();
            Vector3 direction = pointToLight.Normalized();

            Ray ray = new Ray(point, direction);

            List<Intersection> intersects = scene.Intersections(ray);
            Intersection hit = Intersection.Hit(intersects);

            //Do we have a hit and is it within the distance to the light?
            if (hit != null && hit.t < distance)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Determines reflected color
        /// </summary>
        /// <param name="comps"></param>
        /// <param name="remaining"></param>
        /// <returns></returns>
        public Color ReflectedColor(Computation comps, int remaining = reflectionBounces)
        {
            if (Utilities.FloatEquality(comps.rayObject.material.Reflective, 0))
                return Color.Black;
            if ( remaining < 1) // Breaks recussion 
                return Color.Black;

            Ray reflectiveRay = new Ray(comps.overPoint, comps.reflectV);
            Color reflectedColor = ColorAt(reflectiveRay, remaining - 1);

            return reflectedColor * comps.rayObject.material.Reflective;
        }

        /// <summary>
        /// Determines  refracted color
        /// </summary>
        /// <param name="comps"></param>
        /// <param name="remaining"></param>
        /// <returns></returns>
        public Color RefractedColor(Computation comps, int remaining = reflectionBounces)
        {
            if(Utilities.FloatEquality(comps.rayObject.material.Transparency, 0) || remaining < 1)
                return Color.Black;

            // Checking for Total Internal Refractions
            // Find the ratio of the first index of refraction to the second
            float nRatio = comps.n1 / comps.n2;
            float cosI = Vector3.Dot(comps.eyeV, comps.normalV); // cos_i is the same as the dot product of the two vectors
            float sin2T = (nRatio * nRatio) * (1 - (cosI * cosI)); // Find sin(theta_t)^2 via trigonometric identity
           
            if (sin2T > 1) // if sin(theta_t) is create that 1 than there are total interal refractions
                return Color.Black; 
     
            float cosT = (float)Math.Sqrt(1.0 - sin2T); // Find cos(theta_t) via trigonometric identity
            // Compute the direction of the refacted ray 
            Vector3 direction = comps.normalV * (nRatio * cosI - cosT) - comps.eyeV * nRatio;
            Ray refractedRay = new Ray(comps.underPoint, direction); // Create refracted ray

            //Find the color of the refracted ray, making sure to multiply by 
            //the transparency value to account for any opacity
            Color refractedColor = this.ColorAt(refractedRay, remaining - 1) * comps.rayObject.material.Transparency;

            return refractedColor;
        }



    }
}
