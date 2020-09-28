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
            halfUnitSphere.TransformMatrix = Matrix4.ScaleMatrix(0.5f, 0.5f, 0.5f);
            
            //Console.WriteLine(halfUnitSphere.TransformMatrix.ToString());
           
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
                List<Intersection> objIntersects = obj.Intersect(ray);
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
        /// 
        /// </summary>
        /// <param name="light"></param>
        public void AddLight(Light light)
        {
            this.lights.Add(light);
        }

        /// <summary>
        /// Finds color value at given intersection encapsulated in Computation class parameter
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public Color ShadeHit(Computation comp)
        {
            //Material mat = comp.rayObject.material;

            Color totalColor = new Color(0,0,0);

            foreach(Light light in lights)
            {
                Color hitColor = comp.rayObject.material.Lighting(comp.rayObject.material, light, 
                                                                  comp.point, comp.eyeV, 
                                                                  comp.normalV);
                totalColor += hitColor;
            }
            
            return totalColor;
        }

        /// <summary>
        /// Finds color value given a Ray and Scene
        /// Use this method for outside calls to find color value.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="ray"></param>
        /// <returns></returns>
        public Color ColorAt(Ray ray)
        {
            Color resultColor = Color.Black;
            Scene scene = this;

            List<Intersection> intersections = scene.Intersections(ray);
            
            Intersection hit = Intersection.Hit(intersections);
            if (hit == null)
            {
                return resultColor;
            }

            Computation comp = new Computation(hit, ray);
            resultColor = ShadeHit(comp);

            return resultColor;
        }
    
    }
}
