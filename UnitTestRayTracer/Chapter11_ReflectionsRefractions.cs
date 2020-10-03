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

        [Fact]
        public void TransparencyRefractiveIndexAdded()
        {
            Material m = new Material();
            m.RefractIndex = 1.0f;

            Assert.True(m.Transparency == 0.0f);
            Assert.True(m.RefractIndex == 1.0f);
        }

        [Fact]
        public void GlassSphere()
        {
            Sphere glassSphere = new Sphere();
            glassSphere.material.Transparency = 1.0f;
            glassSphere.material.RefractIndex = 1.5f;

            Assert.True(glassSphere.material.Transparency == 1.0f);
            Assert.True(glassSphere.material.RefractIndex == 1.5f);
        }

        [Fact]
        public void Determine_n1_n2()
        {
            Sphere glassSphere01 = new Sphere();
            glassSphere01.material.Transparency = 1.0f;
            glassSphere01.material.RefractIndex = 1.5f;
            glassSphere01.Transform = Matrix4.ScaleMatrix(2, 2, 2);

            Sphere glassSphere02 = new Sphere();
            glassSphere02.material.Transparency = 1.0f;
            glassSphere02.material.RefractIndex = 2.0f;
            glassSphere02.Transform = Matrix4.TranslateMatrix(0, 0, -0.25f);

            Sphere glassSphere03 = new Sphere();
            glassSphere03.material.Transparency = 1.0f;
            glassSphere03.material.RefractIndex = 2.5f;
            glassSphere03.Transform = Matrix4.TranslateMatrix(0, 0, 0.25f);

            Ray r = new Ray(new Point(0, 0, -4), new Vector3(0, 0, 1));

            Intersection i01 = new Intersection(2, glassSphere01);
            Intersection i02 = new Intersection(2.75f, glassSphere02);
            Intersection i03 = new Intersection(3.25f, glassSphere03);
            Intersection i04 = new Intersection(4.75f, glassSphere02);
            Intersection i05 = new Intersection(5.25f, glassSphere03);
            Intersection i06 = new Intersection(6, glassSphere01);
            
            List<Intersection> xs = new List<Intersection>() { i01, i02, i03, i04, i05, i06 };

            Computation comps01 = new Computation(xs[0], r, xs);
            Computation comps02 = new Computation(xs[1], r, xs);
            Computation comps03 = new Computation(xs[2], r, xs);
            Computation comps04 = new Computation(xs[3], r, xs);
            Computation comps05 = new Computation(xs[4], r, xs);
            Computation comps06 = new Computation(xs[5], r, xs);

            Assert.Equal(1.0f, comps01.n1);
            Assert.Equal(1.5f, comps02.n1);
            Assert.Equal(2.0f, comps03.n1);
            Assert.Equal(2.5f, comps04.n1);
            Assert.Equal(2.5f, comps05.n1);
            Assert.Equal(1.5f, comps06.n1);

            Assert.Equal(1.5f, comps01.n2);
            Assert.Equal(2.0f, comps02.n2);
            Assert.Equal(2.5f, comps03.n2);
            Assert.Equal(2.5f, comps04.n2);
            Assert.Equal(1.5f, comps05.n2);
            Assert.Equal(1.0f, comps06.n2);
        }

        [Fact]
        public void UnderPoint()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));

            Sphere glassSphere01 = new Sphere();
            glassSphere01.material.Transparency = 1.0f;
            glassSphere01.material.RefractIndex = 1.5f;
            glassSphere01.Transform = Matrix4.TranslateMatrix(0, 0, 1);

            Intersection i = new Intersection(5, glassSphere01);

            Computation comps = new Computation(i, r);

            Assert.True(comps.underPoint.z > Utilities.underPointEpsilon / 2);
            Assert.True(comps.point.z < comps.underPoint.z);
        }

        [Fact]
        public void OpaqueBlack()
        {
            Scene scene = new Scene();
            RayObject s = scene.Objects[0];
            s.material.Transparency = 0.0f;
            Ray r = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));

            Intersection i01 = new Intersection(4, s);
            Intersection i02 = new Intersection(6, s);

            List<Intersection> xs = new List<Intersection>() { i01, i02 };

            Computation comps = new Computation(xs[0], r, xs);

            Color result = scene.RefractedColor(comps, 5);

            Assert.True(result == Color.Black); 
        }

        [Fact]
        public void MaximumRecursive()
        {
            Scene scene = new Scene();
            RayObject s = scene.Objects[0];
            s.material.Transparency = 1.0f;
            s.material.RefractIndex = 1.5f;

            Ray r = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));

            Intersection i01 = new Intersection(4, s);
            Intersection i02 = new Intersection(6, s);

            List<Intersection> xs = new List<Intersection>() { i01, i02 };

            Computation comps = new Computation(xs[0], r, xs);

            Color result = scene.RefractedColor(comps, 0);

            Assert.True(result == Color.Black);
        }

        [Fact]
        public void RefractedColor()
        {
            Scene scene = new Scene();

            RayObject a = scene.Objects[0];
            a.material.Ambient = 1.0f;
            a.material.Pattern = new TestPattern();

            RayObject b = scene.Objects[1];
            b.material.Transparency = 1.0f;
            b.material.RefractIndex = 1.5f;

            Ray r = new Ray(new Point(0, 0, 0.1f), new Vector3(0, 1, 0));

            Intersection i01 = new Intersection(-0.9899f, a);
            Intersection i02 = new Intersection(-0.4899f, b);
            Intersection i03 = new Intersection(0.4899f, b);
            Intersection i04 = new Intersection(0.9899f, a);

            List<Intersection> xs = new List<Intersection>() { i01, i02, i03, i04 };

            Computation comps = new Computation(xs[2], r, xs);

            Color result = scene.RefractedColor(comps, 5);
            Color answer = new Color(0, 0.9963838f, 0.047175623f); // Answer doesn't match book exactly having small floating poitn difference

            Assert.True(answer == result);
        }

        [Fact]
        public void ShadeHitTransparentMaterial()
        {
            Scene scene = new Scene();

            Plane floor = new Plane();
            floor.Transform = Matrix4.TranslateMatrix(0, -1, 0);
            floor.material.Transparency = 0.5f;
            floor.material.RefractIndex = 1.5f;

            scene.AddObject(floor);

            Sphere s = new Sphere();
            s.Transform = Matrix4.TranslateMatrix(0, -3.5f, -0.5f);
            s.material.mColor = new Color(1, 0, 0);
            s.material.Ambient = 0.5f;

            scene.AddObject(s);

            Ray r = new Ray(new Point(0, 0, -3), new Vector3(0, (float)Math.Sqrt(2) / -2, (float)Math.Sqrt(2) / 2));

            List<Intersection> xs = new List<Intersection>() { new Intersection((float)Math.Sqrt(2), floor) };

            Computation comps = new Computation(xs[0], r, xs);

            Color c = scene.ShadeHit(comps, 5);
            Color answer = new Color(0.9363487f, 0.6863487f, 0.6863487f); // Answer doesn't match book exactly having small floating poitn difference

            Assert.True(answer == c);
        }



    }
}
