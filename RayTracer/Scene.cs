using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace RayTracer
{
    public class Scene
    {
        List<Light> lights = new List<Light>();
        List<RayObject> objects = new List<RayObject>();

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

        public Scene()
        {
            DefaultScene();
        }

        /// <summary>
        /// Sets up a defual matching book describtion (pg. 92)
        /// </summary>
        private void DefaultScene()
        {
            Light lightDefault = new Light(Color.White, new Point(-10, 10, -10));
            lights.Add(lightDefault);

            Material m1 = new Material(new Color(0.8f, 1.0f, 0.6f), diffuse: 0.7f, specular: 0.2f);
            Sphere unitSphere = new Sphere(m1, radius: 1.0f);
            Sphere halfUnitSphere = new Sphere(radius: 0.5f);

            objects.Add(unitSphere);
            objects.Add(halfUnitSphere);
        }



    }
}
