using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace UnitTestRayTracer
{
    public class Chapter12_Cube
    {
        [Fact]
        public void RayIntersectCube()
        // Test that the Ray intersection with cube is correct in each possible case
        {
            Cube cube = new Cube();

            System.Tuple<Point, Vector3>[] RayTupleList = new Tuple<Point, Vector3>[]
            {
                //                  Origin              Direction     
                System.Tuple.Create(new Point(5, 0.5f, 0), new Vector3(-1, 0, 0)), // +x
                System.Tuple.Create(new Point(-5, 0.5f, 0), new Vector3(1, 0, 0)), // -x
                System.Tuple.Create(new Point(0.5f, 5f, 0), new Vector3(0, -1, 0)), // +y
                System.Tuple.Create(new Point(0.5f, -5f, 0), new Vector3(0, 1, 0)), // -y
                System.Tuple.Create(new Point(0.5f, 0, 5), new Vector3(0, 0, -1)), // +z
                System.Tuple.Create(new Point(0.5f, 0, -5), new Vector3(0, 0, 1)),  // -z
                System.Tuple.Create(new Point(0, 0.5f, 0), new Vector3(0, 0, 1))  // inside
            };

            Ray r00 = new Ray(RayTupleList[0].Item1, RayTupleList[0].Item2);
            Ray r01 = new Ray(RayTupleList[1].Item1, RayTupleList[1].Item2);
            Ray r02 = new Ray(RayTupleList[2].Item1, RayTupleList[2].Item2);
            Ray r03 = new Ray(RayTupleList[3].Item1, RayTupleList[3].Item2);
            Ray r04 = new Ray(RayTupleList[4].Item1, RayTupleList[4].Item2);
            Ray r05 = new Ray(RayTupleList[5].Item1, RayTupleList[5].Item2);
            Ray r06 = new Ray(RayTupleList[6].Item1, RayTupleList[6].Item2);

            List<Intersection> xs00 = cube.LocalIntersects(r00);
            List<Intersection> xs01 = cube.LocalIntersects(r01);
            List<Intersection> xs02 = cube.LocalIntersects(r02);
            List<Intersection> xs03 = cube.LocalIntersects(r03);
            List<Intersection> xs04 = cube.LocalIntersects(r04);
            List<Intersection> xs05 = cube.LocalIntersects(r05);
            List<Intersection> xs06 = cube.LocalIntersects(r06);

            Assert.Equal(4, xs00[0].t); // +x
            Assert.Equal(6, xs00[1].t);

            Assert.Equal(4, xs01[0].t); // -x
            Assert.Equal(6, xs01[1].t);

            Assert.Equal(4, xs02[0].t); // +y
            Assert.Equal(6, xs02[1].t);

            Assert.Equal(4, xs03[0].t); // -y
            Assert.Equal(6, xs03[1].t);

            Assert.Equal(4, xs04[0].t); // +z
            Assert.Equal(6, xs04[1].t);

            Assert.Equal(4, xs05[0].t); // -z
            Assert.Equal(6, xs05[1].t);

            Assert.Equal(-1, xs06[0].t); // inside
            Assert.Equal(1, xs06[1].t);
        }

        [Fact]
        public void RayMissesCube()
        // Test cases where ray misses the cube
        {
            Cube cube = new Cube();

            System.Tuple<Point, Vector3>[] RayTupleList = new Tuple<Point, Vector3>[]
            {
                //                  Origin              Direction                               
                System.Tuple.Create(new Point(-2, 0, 0), new Vector3(0.2673f, 0.5345f, 0.8018f)), 
                System.Tuple.Create(new Point(0, -2, 0), new Vector3(0.8018f, 0.2673f, 0.5345f)), 
                System.Tuple.Create(new Point(0, 0, -2), new Vector3(0.5345f, 0.8018f, 0.2673f)), 
                System.Tuple.Create(new Point(2, -0, 2), new Vector3(0, 0, -1)), 
                System.Tuple.Create(new Point(0, 2, 2), new Vector3(0, -1, 0)), 
                System.Tuple.Create(new Point(2, 2, 0), new Vector3(-1, 0, 0)),  
            };

            Ray r00 = new Ray(RayTupleList[0].Item1, RayTupleList[0].Item2);
            Ray r01 = new Ray(RayTupleList[1].Item1, RayTupleList[1].Item2);
            Ray r02 = new Ray(RayTupleList[2].Item1, RayTupleList[2].Item2);
            Ray r03 = new Ray(RayTupleList[3].Item1, RayTupleList[3].Item2);
            Ray r04 = new Ray(RayTupleList[4].Item1, RayTupleList[4].Item2);
            Ray r05 = new Ray(RayTupleList[5].Item1, RayTupleList[5].Item2);

            List<Intersection> xs00 = cube.LocalIntersects(r00);
            List<Intersection> xs01 = cube.LocalIntersects(r01);
            List<Intersection> xs02 = cube.LocalIntersects(r02);
            List<Intersection> xs03 = cube.LocalIntersects(r03);
            List<Intersection> xs04 = cube.LocalIntersects(r04);
            List<Intersection> xs05 = cube.LocalIntersects(r05);

            Assert.Empty(xs00);
            Assert.Empty(xs01);
            Assert.Empty(xs02);
            Assert.Empty(xs03);
            Assert.Empty(xs04);
            Assert.Empty(xs05);
        }

        [Fact]
        public void CubeNormals()
        {
            Cube c = new Cube();

            Point[] points = new Point[] { new Point(1, 0.5f, -0.8f),
                                           new Point(-1, -0.2f, 0.9f),
                                           new Point(-0.4f, 1, -0.1f),
                                           new Point(0.3f, -1, -0.7f),
                                           new Point(-0.6f, 0.3f, 1f),
                                           new Point(0.4f, 0.4f, -1),
                                           new Point(1, 1, 1),
                                           new Point(-1, -1, -1)};

            Vector3[] directions = new Vector3[] { new Vector3(1,0,0),
                                                new Vector3(-1,0,0),
                                                new Vector3(0,1,0),
                                                new Vector3(0,-1,0),
                                                new Vector3(0,0,1),
                                                new Vector3(0,0,-1),
                                                new Vector3(1,0,0),
                                                new Vector3(-1,0,0)};

            Vector3 n00 = c.LocalNormal(points[0]);
            Vector3 n01 = c.LocalNormal(points[1]);
            Vector3 n02 = c.LocalNormal(points[2]);
            Vector3 n03 = c.LocalNormal(points[3]);
            Vector3 n04 = c.LocalNormal(points[4]);
            Vector3 n05 = c.LocalNormal(points[5]);
            Vector3 n06 = c.LocalNormal(points[6]);
            Vector3 n07 = c.LocalNormal(points[7]);

            Assert.True(n00 == directions[0]);
            Assert.True(n01 == directions[1]);
            Assert.True(n02 == directions[2]);
            Assert.True(n03 == directions[3]);
            Assert.True(n04 == directions[4]);
            Assert.True(n05 == directions[5]);
            Assert.True(n06 == directions[6]);
            Assert.True(n07 == directions[7]);



        }

    }
}
