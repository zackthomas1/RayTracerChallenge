using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;


namespace UnitTestRayTracer
{
    public class Chapter08_Shadows
    {

        [Fact]
        public void LightingShadowSurface()
        {
            Vector3 eyeV = new Vector3(0, 0, -1);
            Vector3 normalV = new Vector3(0, 0, -1);
            Light light = new Light(Color.White, new Point(0, 0, -10));
            bool inShadow = true;
            Material m = new Material();
            Point position = new Point(0, 0, 0);

            Color result = m.Lighting(m, light, position, eyeV, normalV, inShadow);
            Color answer = new Color(.1f, 0.1f, 0.1f);

            Assert.True(answer == result);
        }

        [Fact]
        public void PointLightCollinearNoObjectBetween()
        {
            Scene scene = new Scene();
            Point p = new Point(0, 10, 0);

            bool result = scene.IsShadowed(p, scene.Lights[0]);

            Assert.False(result);
        }

        [Fact]
        public void ObjectBetweenPointandLight()
        {
            Scene scene = new Scene();
            Point p = new Point(10, -10, 10);
            
            bool result = scene.IsShadowed(p, scene.Lights[0]);

            Assert.True(result);
        }
       
        [Fact]
        public void ObjectBehindLight()
        {
            Scene scene = new Scene();
            Point p = new Point(-20, 20, -20);

            bool result = scene.IsShadowed(p, scene.Lights[0]);

            Assert.False(result);
        }

        [Fact]
        public void ObjectBehindPoint()
        {
            Scene scene = new Scene();
            Point p = new Point(-2, 2, -2);

            bool result = scene.IsShadowed(p, scene.Lights[0]);

            Assert.False(result);
        }

        [Fact]
        public void IntersectionInShadow()
        {
            Scene scene = new Scene();

            Light l1 = new Light(Color.White, new Point(0, 0, -10));
            List<Light> lights = new List<Light>() { l1 };
            scene.Lights = lights;

            Sphere s1 = new Sphere();
            Sphere s2 = new Sphere();
            s2.TransformMatrix = Matrix4.TranslateMatrix(0, 0, 10);
            List<RayObject> objects = new List<RayObject>() { s1, s2 };
            scene.AddObject(s1);
            scene.AddObject(s2);

            Ray r = new Ray(new Point(0, 0, 5), new Vector3(0, 0, 1));
            Intersection i = new Intersection(4, s2);
            Computation comp = new Computation(i, r);

            Color result = scene.ShadeHit(comp);
            Color answer = new Color(0.1f, 0.1f, 0.1f);

            Assert.True(answer == result);
        }

        [Fact]
        public void HitOffsetPoint()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            Sphere s = new Sphere();
            s.TransformMatrix = Matrix4.TranslateMatrix(0, 0, 1);
            Intersection i = new Intersection(5, s);
            Computation comps = new Computation(i, r);

            Assert.True(comps.overPoint.z < -Utilities.Epsilon / 2);
            Assert.True(comps.point.z > comps.overPoint.z);

        }

    }
}
