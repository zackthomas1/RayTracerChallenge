using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
namespace UnitTestRayTracer
{
    public class Chapter11_ReflectionsRefractions
    {

        [Fact]
        public void ReflectivityDefault()
        {
            Material mat = new Material();

            Assert.True(Utilities.FloatEquality(mat.Reflective, 0));
        }

        [Fact]
        public void ComputeReflctionVector()
        {
            Plane p = new Plane();
            Ray r = new Ray(new Point(0, 1, -1), new Vector3(0, -(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2));
            Intersection i = new Intersection((float)Math.Sqrt(2), p);

            Computation comp = new Computation(i, r); 
            Vector3 answer = new Vector3(0, (float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2);

            Assert.True(comp.reflectV == answer); 
        }

        [Fact]
        public void ReflectColorNonReflectiveMaterial()
        {
            Scene scene = new Scene();
            Ray r = new Ray(new Point(0, 0, 0), new Vector3(0, 0, 1));
            RayObject s = scene.Objects[1];
            s.material.Ambient = 1.0f;
            Intersection i = new Intersection(1, s);
            Computation comps = new Computation(i, r);
            
            Color result = scene.ReflectedColor(comps);

            Assert.True(result == Color.Black);
        }

        [Fact]
        public void ReflectedColorReflectiveMaterial()
        {
            Scene scene = new Scene();

            Plane p = new Plane();
            p.material.Reflective = 0.5f;
            p.Transform = Matrix4.TranslateMatrix(0, -1, 0);
            scene.AddObject(p);

            Ray r = new Ray(new Point(0, 0, -3), new Vector3(0, -(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2));
            Intersection i = new Intersection((float)Math.Sqrt(2), p);
            Computation comps = new Computation(i, r);
            
            Color result = scene.ReflectedColor(comps);
            Color answer = new Color(0.19073f, 0.23841f, 0.14304f); // Results are slightly off from the values in the book(pg 144)

            Assert.True(result == answer);
        }

        [Fact]
        public void ShadeHitReflectiveMaterial()
        {
            Scene scene = new Scene();
            Plane p = new Plane();
            p.material.Reflective = 0.5f;
            p.Transform = Matrix4.TranslateMatrix(0, -1, 0);
            scene.AddObject(p);
            Ray r = new Ray(new Point(0,0,-3), new Vector3(0, -(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2));
            Intersection i = new Intersection((float)Math.Sqrt(2), p);

            Computation comps = new Computation(i, r);
            
            Color result = scene.ShadeHit(comps);
            Color answer = new Color(0.8770806f, 0.92476356f, 0.8293976f); // Results are slightly off from the values in the book(pg 145)

            Assert.True(result == answer); 
        }

        [Fact]
        public void ColorAtMutuallyReflectiveSurfaces()
        {
            Scene scene = new Scene();
            
            scene.Lights = new List<Light>();
            scene.Objects = new List<RayObject>();

            scene.AddLight(new Light(Color.White, new Point(0, 0, 0)));
            
            Plane lower = new Plane();
            lower.material.Reflective = 1.0f;
            lower.Transform = Matrix4.TranslateMatrix(0, -1, 0);

            Plane upper = new Plane();
            upper.material.Reflective = 1.0f;
            upper.Transform = Matrix4.TranslateMatrix(0, 1, 0);

            scene.AddObject(lower);
            scene.AddObject(upper);

            Ray r = new Ray(new Point(0, 0, 0), new Vector3(0, 1, 0));
            Color result = scene.ColorAt(r);

            Assert.True(result == new Color(11.4f, 11.4f, 11.4f)); 

        }

        [Fact]
        public void ReflectedColorMaxRecusiveDepth()
        {
            Scene scene = new Scene();

            Plane p = new Plane();
            p.material.Reflective = 0.5f;
            p.Transform = Matrix4.TranslateMatrix(0, -1, 0);
            scene.AddObject(p); 

            Ray r = new Ray(new Point(0,0,-3), new Vector3(0, -(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2));
            Intersection i = new Intersection((float)Math.Sqrt(2), p);

            Computation comps = new Computation(i, r);

            Color result = scene.ReflectedColor(comps, 0);

            Assert.True(result == Color.Black); 
        }
    }
}
