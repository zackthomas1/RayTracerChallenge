using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace UnitTestRayTracer
{
    public class Chapter09_Planes
    {
        [Fact]
        public void DefaultTransformation()
        {
            Sphere s = new Sphere();
            Assert.IsAssignableFrom<RayObject>(s);
        }

        [Fact]
        public void PlaneNormalConstant()
        {
            Plane plane = new Plane();
            Vector3 nV1 = plane.GetNormal(new Point(0, 0, 0));
            Vector3 nV2 = plane.GetNormal(new Point(10, 0, -10));
            Vector3 nV3 = plane.GetNormal(new Point(-5, 0, 150));

            Vector3 answer = new Vector3(0, 1, 0);

            Assert.True(answer == nV1);
            Assert.True(answer == nV2);
            Assert.True(answer == nV3);
        }

        [Fact]
        public void RotatedPlanNormal()
        {
            Plane p = new Plane();

            p.Transform = p.Transform.Rotate_X(Math.PI / 2);
            Vector3 normalVector = p.GetNormal(new Point(5, 5, 5));

            Vector3 nV1 = p.GetNormal(new Point(0, 0, 0));
            Vector3 nV2 = p.GetNormal(new Point(10, 0, -10));
            Vector3 nV3 = p.GetNormal(new Point(-5, 0, 150));

            Vector3 answer = new Vector3(0, 0, 1);

            Assert.True(answer == nV1);
            Assert.True(answer == nV2);
            Assert.True(answer == nV3);
        }

        [Fact]
        public void RayPlaneParallelNonIntersecting()
        {
            Plane p = new Plane();
            Ray r = new Ray(new Point(0, 10, 0), new Vector3(0, 0, 1));

            List<Intersection> xs = p.Intersect(r);

            Assert.Null(xs); 
        }

        [Fact]
        public void RayPlaneCoplanar()
        {
            Plane p = new Plane();
            Ray r = new Ray(new Point(0, 0, 0), new Vector3(0, 0, 1));

            List<Intersection> xs = p.Intersect(r);

            Assert.Null(xs);
        }

        [Fact]
        public void RayIntersectsPlaneAbove()
        {
            Plane p = new Plane();
            Ray r = new Ray(new Point(0, 1, 0), new Vector3(0, -1, 0));

            List<Intersection> xs = p.Intersect(r);

            Assert.Single(xs);
            Assert.True(1 == xs[0].t);
            Assert.True(p == xs[0].rayObject);
        }

        [Fact]
        public void RayIntersectsPlaneBelow()
        {
            Plane p = new Plane();
            Ray r = new Ray(new Point(0, -1, 0), new Vector3(0, 1, 0));

            List<Intersection> xs = p.Intersect(r);

            Assert.Single(xs);
            Assert.True(1 == xs[0].t);
            Assert.True(p == xs[0].rayObject);
        }





    }
}
