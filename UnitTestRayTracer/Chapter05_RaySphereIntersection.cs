using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace UnitTestRayTracer
{
    public class Chapter05_RaySphereIntersection
    {


        [Fact]
        public void RayConstructor()
        {
            Point origin = new Point(1, 2, 3);
            Vector3 direction = new Vector3(4, 5, 6);

            Ray ray1 = new Ray(origin, direction);

            Assert.Equal(origin, ray1.origin);
            Assert.Equal(direction, ray1.direction);
        }

        [Fact]
        public void PositionRay()
        {
            Ray ray01 = new Ray(new Point(2, 3, 4), new Vector3(1, 0, 0));

            Assert.Equal(ray01.Position(0), new Point(2,3,4));
            Assert.Equal(ray01.Position(1), new Point(3, 3, 4));
            Assert.Equal(ray01.Position(-1), new Point(1, 3, 4));
            Assert.Equal(ray01.Position(2.5f), new Point(4.5f, 3, 4));
        } 

        [Fact]
        public void RayIntersectSphereTwoPoints()
        {
            Ray ray01 = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            Sphere sphere = new Sphere();

            List<Intersection> intersecionPoints = sphere.Intersect(ray01);

            Assert.Equal(2, intersecionPoints.Count);
            Assert.Equal(4.0, intersecionPoints[0].t);
            Assert.Equal(6.0, intersecionPoints[1].t);

        }

        [Fact]
        public void RayIntersectSphereTangent()
        {
            Ray ray01 = new Ray(new Point(0, 1, -5), new Vector3(0, 0, 1));
            Sphere sphere = new Sphere();

            List<Intersection> intersecionPoints = sphere.Intersect(ray01);

            Assert.Equal(2, intersecionPoints.Count);
            Assert.Equal(5.0, intersecionPoints[0].t);
            Assert.Equal(5.0, intersecionPoints[1].t);
        }

        [Fact]
        public void RayIntersectMissSphere()
        {
            Ray ray01 = new Ray(new Point(0, 2, -5), new Vector3(0, 0, 1));
            Sphere sphere = new Sphere();

            List<Intersection> intersecionPoints = sphere.Intersect(ray01);

            Assert.Empty(intersecionPoints);
        }

        [Fact]
        public void RayInsideSphere()
        {
            Ray ray01 = new Ray(new Point(0, 0, 0), new Vector3(0, 0, 1));
            Sphere sphere = new Sphere();

            List<Intersection> intersecionPoints = sphere.Intersect(ray01);

            Assert.Equal(2, intersecionPoints.Count);
            Assert.Equal(-1.0, intersecionPoints[0].t);
            Assert.Equal(1.0, intersecionPoints[1].t); 
        }

        [Fact]
        public void SphereBehindRay()
        {
            Ray ray01 = new Ray(new Point(0, 0, 5), new Vector3(0, 0, 1));
            Sphere sphere = new Sphere();

            List<Intersection> intersecionPoints = sphere.Intersect(ray01);

            Assert.Equal(2, intersecionPoints.Count);
            Assert.Equal(-6.0, intersecionPoints[0].t);
            Assert.Equal(-4.0, intersecionPoints[1].t) ;
        }

        [Fact]
        public void IntersectionClass()
        {
            Sphere sphere = new Sphere();
            Intersection i = new Intersection(3.5f, sphere);

            Assert.Equal(3.5f, i.t);
            Assert.Equal(sphere,i.rayObject);
        }

        [Fact]
        public void SortIntersections()
        {
            Sphere sphere = new Sphere();
            Intersection i1 = new Intersection(5, sphere);
            Intersection i2 = new Intersection(7, sphere);
            Intersection i3 = new Intersection(-3, sphere);
            Intersection i4 = new Intersection(2, sphere);

            List<Intersection> intersectionsList = new List<Intersection>() {i1, i2, i3, i4 };

            List<Intersection> xs = Intersection.Sort(intersectionsList);

            Assert.Equal(4, xs.Count);
            Assert.True(xs[0] == i3);
            Assert.True(xs[1] == i4);
            Assert.True(xs[2] == i1);
            Assert.True(xs[3] == i2);
        }

        [Fact]
        public void HitPositiveIntersections()
        {
            Sphere s = new Sphere();
            Intersection i1 = new Intersection(1, s);
            Intersection i2 = new Intersection(2, s);

            List<Intersection> intersectionsList = new List<Intersection>() { i1, i2 };
            List<Intersection> xs = Intersection.Sort(intersectionsList);

            Intersection i = Intersection.Hit(xs);

            Assert.Equal(i1, i);
        }

        [Fact]
        public void HitNegativeIntersection()
        {
            Sphere s = new Sphere();
            Intersection i1 = new Intersection(-1, s);
            Intersection i2 = new Intersection(2, s);

            List<Intersection> xs = new List<Intersection>() { i1, i2 };
            xs = Intersection.Sort(xs);

            Intersection i = Intersection.Hit(xs);

            Assert.Equal(i2, i);
        }

        [Fact]
        public void HitALLNegativeIntersections()
        {
            Sphere s = new Sphere();
            Intersection i1 = new Intersection(-1, s);
            Intersection i2 = new Intersection(-2, s);

            List<Intersection> xs = new List<Intersection>() { i1, i2 };
            xs = Intersection.Sort(xs);

            Intersection i = Intersection.Hit(xs);

            Assert.Null(i);
        }

        [Fact]
        public void TranslateRay()
        {
            Ray r1 = new Ray(new Point(1, 2, 3), new Vector3(0, 1, 0));
            Matrix4 translate = Matrix4.TranslateMatrix(3, 4, 5);
            Ray r2 = r1.transform(translate);

            Assert.Equal(new Point(4,6,8),r2.origin);
            Assert.Equal(r1.direction, r2.direction);
        }

        [Fact]
        public void ScaleRay()
        {
            Ray r1 = new Ray(new Point(1, 2, 3), new Vector3(0, 1, 0));
            Matrix4 translate = Matrix4.ScaleMatrix(2, 3, 4);
            Ray r2 = r1.transform(translate);

            Assert.Equal(new Point(2, 6, 12), r2.origin);
            Assert.Equal(new Vector3(0, 3, 0), r2.direction);
        }

        [Fact]
        public void SphereDefualtTransform()
        {
            Sphere s = new Sphere();
            Matrix4 identity = new Matrix4();
            Assert.True(identity == s.transformMatrix); 
        }

        [Fact]
        public void ChangingSphereTransform()
        {
            Sphere s = new Sphere();
            Matrix4 translate = Matrix4.TranslateMatrix(2, 3, 4); ;
            s.SetTranform(translate);
            Assert.True(translate == s.transformMatrix);
        }

        [Fact]
        public void ChangingSphereScale()
        {
            Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1)); 
            Sphere s = new Sphere();
            s.TransformMatrix = Matrix4.ScaleMatrix(2, 2, 2);

            Ray scaleRay = ray.ApplyObjectTransform(s);

            List<Intersection> xs = s.Intersect(scaleRay);

            Assert.Equal(2, xs.Count);
            Assert.Equal(3.0f, xs[0].t);
            Assert.Equal(7.0f, xs[1].t);
        }

        [Fact]
        public void InsectTranslatedSphere()
        {
            Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            Sphere s = new Sphere();
            s.TransformMatrix = Matrix4.TranslateMatrix(5, 0, 0);

            Ray transformRay = ray.ApplyObjectTransform(s);

            List<Intersection> xs = s.Intersect(transformRay);
            
            Assert.Empty(xs);

        }
    }
}
