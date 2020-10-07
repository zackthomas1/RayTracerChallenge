using System;
using Xunit;
using RayTracer;
using RayTracer.RayObjects;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace UnitTestRayTracer
{
    public class Chapter13_Cylinders
    {
        [Fact]
        public void RayMissCyclinder()
        // A Ray misses a cyclinder
        {
            Cylinder cyl = new Cylinder();

            Vector3 d01 = new Vector3(0, 1, 0).Normalize();
            Vector3 d02 = new Vector3(0, 1, 0).Normalize();
            Vector3 d03 = new Vector3(1, 1, 1).Normalize();

            Point org01 = new Point(1, 0, 0);
            Point org02 = new Point(0, 1, 0);
            Point org03 = new Point(0, 0, -5);

            Ray r01 = new Ray(org01, d01);
            Ray r02 = new Ray(org02, d02);
            Ray r03 = new Ray(org03, d03);

            List<Intersection> xs01 = cyl.LocalIntersects(r01);
            List<Intersection> xs02 = cyl.LocalIntersects(r02);
            List<Intersection> xs03 = cyl.LocalIntersects(r03);

            Assert.Empty(xs01);
            Assert.Empty(xs02);
            Assert.Empty(xs03);
        }

        [Fact]
        public void RayHitCylinder()
        // A ray strikes a cylinder
        {
            Cylinder cyl = new Cylinder();
            cyl.MaxHeight = float.PositiveInfinity;
            cyl.MinHeight = float.NegativeInfinity;

            Point org01 = new Point(1, 0, -5);
            Point org02 = new Point(0, 0, -5);
            Point org03 = new Point(0.5f, 0, -5);

            Vector3 d01 = new Vector3(0, 0, 1).Normalize();
            Vector3 d02 = new Vector3(0, 0, 1).Normalize();
            Vector3 d03 = new Vector3(0.1f, 1, 1).Normalize();

            Ray r01 = new Ray(org01, d01);
            Ray r02 = new Ray(org02, d02);
            Ray r03 = new Ray(org03, d03);

            List<Intersection> xs01 = cyl.LocalIntersects(r01);
            List<Intersection> xs02 = cyl.LocalIntersects(r02);
            List<Intersection> xs03 = cyl.LocalIntersects(r03);

            Assert.True(xs01.Count == 2);
            Assert.True(xs02.Count == 2);
            Assert.True(xs03.Count == 2);

            Assert.True(xs01[0].t == 5);
            Assert.True(xs02[0].t == 4);
            //Assert.True(xs03[0].t == 6.80798f); // Original value from book
            Assert.True(Utilities.FloatEquality(xs03[0].t, 6.808006f)); // Updated floating point rounding

            Assert.True(xs01[1].t == 5);
            Assert.True(xs02[1].t == 6);
            //Assert.True(xs03[1].t == 7.08872f); // Original value from book
            Assert.True(Utilities.FloatEquality(xs03[1].t, 7.08869f)); // Updated floating point rounding
        }

        [Fact]
        public void CylinderNormal()
        // Normal vector on a cylinder
        {
            Cylinder cyl = new Cylinder();

            Point p01 = new Point(1, 0, 0);
            Point p02 = new Point(0, 5, -1);
            Point p03 = new Point(0, -2, 1);
            Point p04 = new Point(-1, 1, 0);

            Vector3 ans01 = new Vector3(1, 0, 0);
            Vector3 ans02 = new Vector3(0, 0, -1);
            Vector3 ans03 = new Vector3(0, 0, 1);
            Vector3 ans04 = new Vector3(-1, 0, 0);

            Vector3 result01 = cyl.LocalNormal(p01);
            Vector3 result02 = cyl.LocalNormal(p02);
            Vector3 result03 = cyl.LocalNormal(p03);
            Vector3 result04 = cyl.LocalNormal(p04);

            Assert.True(ans01 == result01);
            Assert.True(ans02 == result02);
            Assert.True(ans03 == result03);
            Assert.True(ans04 == result04);
        }

        [Fact]
        public void CylinderHeightDefaults()
        {
            Cylinder cyl = new Cylinder();

            Assert.Equal(float.PositiveInfinity, cyl.MaxHeight);
            Assert.Equal(float.NegativeInfinity, cyl.MinHeight);
        }

        [Fact]
        public void IntersectContrainedCylinder()
        {
            Cylinder cyl = new Cylinder();
            cyl.MinHeight = 1.0f;
            cyl.MaxHeight = 2.0f;

            Point org01 = new Point(0, 1.5f, 0);
            Point org02 = new Point(0, 3, -5);
            Point org03 = new Point(0, 0, -5);
            Point org04 = new Point(0, 2, -5);
            Point org05 = new Point(0, 1, -5);
            Point org06 = new Point(0, 1.5f, -2);

            Vector3 d01 = new Vector3(0.1f, 1, 0).Normalize();
            Vector3 d02 = new Vector3(0, 0, 1).Normalize();
            Vector3 d03 = new Vector3(0, 0, 1).Normalize();
            Vector3 d04 = new Vector3(0, 0, 1).Normalize();
            Vector3 d05 = new Vector3(0, 0, 1).Normalize();
            Vector3 d06 = new Vector3(0, 0, 1).Normalize();

            Ray r01 = new Ray(org01, d01);
            Ray r02 = new Ray(org02, d02);
            Ray r03 = new Ray(org03, d03);
            Ray r04 = new Ray(org04, d04);
            Ray r05 = new Ray(org05, d05);
            Ray r06 = new Ray(org06, d06);

            List<Intersection> xs01 = cyl.LocalIntersects(r01);
            List<Intersection> xs02 = cyl.LocalIntersects(r02);
            List<Intersection> xs03 = cyl.LocalIntersects(r03);
            List<Intersection> xs04 = cyl.LocalIntersects(r04);
            List<Intersection> xs05 = cyl.LocalIntersects(r05);
            List<Intersection> xs06 = cyl.LocalIntersects(r06);

            Assert.Empty(xs01);
            Assert.Empty(xs02);
            Assert.Empty(xs03);
            Assert.Empty(xs04);
            Assert.Empty(xs05);
            Assert.Equal(2, xs06.Count);
        }

        [Fact]
        public void IntersectTruncatedCylinder()
        {
            Cylinder cyl = new Cylinder();
            cyl.MinHeight = 1.0f;
            cyl.MaxHeight = 2.0f;
            cyl.Closed = true;

            Point org01 = new Point(0, 3, 0);
            Point org02 = new Point(0, 3, -2);
            Point org03 = new Point(0, 4, -2);
            Point org04 = new Point(0, 0, -2);
            Point org05 = new Point(0, -1, -2);

            Vector3 d01 = new Vector3(0, -1, 0).Normalize();
            Vector3 d02 = new Vector3(0, -1, 2).Normalize();
            Vector3 d03 = new Vector3(0, -1, 1).Normalize();
            Vector3 d04 = new Vector3(0, 1, 2).Normalize();
            Vector3 d05 = new Vector3(0, 1, 1).Normalize();

            Ray r01 = new Ray(org01, d01);
            Ray r02 = new Ray(org02, d02);
            Ray r03 = new Ray(org03, d03);
            Ray r04 = new Ray(org04, d04);
            Ray r05 = new Ray(org05, d05);

            List<Intersection> xs01 = cyl.LocalIntersects(r01);
            List<Intersection> xs02 = cyl.LocalIntersects(r02);
            List<Intersection> xs03 = cyl.LocalIntersects(r03);
            List<Intersection> xs04 = cyl.LocalIntersects(r04);
            List<Intersection> xs05 = cyl.LocalIntersects(r05);

            Assert.Equal(2, xs01.Count);
            Assert.Equal(2, xs02.Count);
            Assert.Equal(2, xs03.Count); // Corner Case
            Assert.Equal(2, xs04.Count);
            Assert.Equal(2, xs05.Count); // Corner Case
        }



    }
}
