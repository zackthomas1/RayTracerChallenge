using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace UnitTestRayTracer
{
    public class Chapter07_MakingAScene
    {

        [Fact]
        public void SetUpSceneClass()
        {
            Scene sceneDefault = new Scene();

            Material m1 = new Material(new Color(0.8f, 1.0f, 0.6f), diffuse: 0.7f, specular: 0.2f);
            Sphere s1 = new Sphere(m1);
            Sphere s2 = new Sphere(radius: 0.5f);

            Assert.Single(sceneDefault.Lights);
            Assert.Equal(2, sceneDefault.Objects.Count);

            Assert.True(s1 == sceneDefault.Objects[0]);
            Assert.True(s2 == sceneDefault.Objects[1]);
        }

        [Fact]
        public void IntersectWorldwithRay()
        {
            Scene scene = new Scene();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));

            List<Intersection> xs = scene.Intersections(ray);

            Assert.Equal(4, xs.Count);
            Assert.Equal(4, xs[0].t);
            Assert.Equal(4.5f, xs[1].t);
            Assert.Equal(5.5f, xs[2].t);
            Assert.Equal(6, xs[3].t);
        }

        [Fact]
        public void PrecomputeIntersectionStates()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            Sphere s = new Sphere();
            Intersection i = new Intersection(4, s);

            Computation comp = Intersection.PrepareComputations(i, r);

            Assert.Equal(i.t, comp.t);
            Assert.Equal(i.rayObject, comp.rayObject);
            Assert.Equal(new Point(0, 0, -1), comp.point);
            Assert.Equal(new Vector3(0, 0, -1), comp.eyeV);
            Assert.Equal(new Vector3(0, 0, -1), comp.normalV);
        }

        [Fact]
        public void HitOutside()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            Sphere s = new Sphere();
            Intersection i = new Intersection(4, s);
            Computation comp = new Computation(i, r);

            Assert.False(comp.inside);
        }

        [Fact]
        public void HitIntside()
        {
            Ray r = new Ray(new Point(0, 0, 0), new Vector3(0, 0, 1));
            Sphere s = new Sphere();
            Intersection i = new Intersection(1, s);
            Computation comp = new Computation(i, r);

            Assert.True(comp.inside);
            Assert.Equal(new Point(0, 0, 1), comp.point);
            Assert.Equal(new Vector3(0, 0, -1), comp.eyeV);
            Assert.Equal(new Vector3(0, 0, -1), comp.normalV);
        }

        [Fact]
        public void ShadeIntersection()
        {
            Scene scene = new Scene();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            RayObject shape = scene.Objects[0];
            Intersection i = new Intersection(4, shape);

            Computation comp = new Computation(i, ray);

            Color color = scene.ShadeHit(comp);
            Color answer = new Color(0.38066f, 0.47583f, 0.2855f);

            Assert.True(answer == color);
        }

        [Fact]
        public void ShadeIntersectionInside()
        {
            Scene scene = new Scene();
            scene.Lights[0] = new Light(new Color(1, 1, 1), new Point(0, 0.25f, 0));
            Ray ray = new Ray(new Point(0, 0, 0), new Vector3(0, 0, 1));
            RayObject shape = scene.Objects[1];
            Intersection i = new Intersection(0.5f, shape);

            Computation comp = new Computation(i, ray);

            Color color = scene.ShadeHit(comp);
            Color answer = new Color(0.90498f, 0.90498f, 0.90498f);

            Assert.True(answer == color);
        }

    }
}
