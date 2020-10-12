using System;
using System.IO;
using System.Collections.Generic;
using RayTracer.RayObjects;

namespace RayTracer
{
    class Program
    {

        static void Main(string[] args)
        {

            //----------------------------------------------------------------------------

            //Scene scene = new Scene();

            //RayObject a = scene.Objects[0];
            //a.material.Ambient = 1.0f;
            //a.material.Pattern = new TestPattern();

            //RayObject b = scene.Objects[1];
            //b.material.Transparency = 1.0f;
            //b.material.RefractIndex = 1.5f;

            //Ray r = new Ray(new Point(0, 0, 0.1f), new Vector3(0, 1, 0));

            //Intersection i01 = new Intersection(-0.9899f, a);
            //Intersection i02 = new Intersection(-0.4899f, b);
            //Intersection i03 = new Intersection(0.4899f, b);
            //Intersection i04 = new Intersection(0.9899f, a);

            //List<Intersection> xs = new List<Intersection>() { i01, i02, i03, i04 };

            //Computation comps = new Computation(xs[2], r, xs);

            //Color result = scene.RefractedColor(comps, 5);
            //Color answer = new Color(0, 0.99888f, 0.04725f);

            //Console.WriteLine(result.ToString());

            //----------------------------------------------------------------------------

            //Scene scene = new Scene();

            //Plane floor = new Plane();
            //floor.Transform = Matrix4.TranslateMatrix(0, -1, 0);
            //floor.material.Transparency = 0.5f;
            //floor.material.RefractIndex = 1.5f;

            //Sphere s = new Sphere();
            //s.material.mColor = new Color(1, 0, 0);
            //s.material.Ambient = 0.5f;
            //s.Transform = Matrix4.TranslateMatrix(0, -3.5f, -0.5f);

            //scene.AddObject(floor);
            //scene.AddObject(s);

            //Ray r = new Ray(new Point(0, 0, -3), new Vector3(0, (float)Math.Sqrt(2) / -2, (float)Math.Sqrt(2) / 2));

            //Intersection i = new Intersection((float)Math.Sqrt(2), floor);
            //List<Intersection> xs = new List<Intersection>() { i };

            //Computation comps = new Computation(xs[0], r, xs);

            //Color c = scene.ShadeHit(comps, 5);

            //Console.WriteLine(c.ToString());

            //----------------------------------------------------------------------------

            //Scene s = new Scene();
            //s.EmptyObjects();

            //Sphere glassSphere = new Sphere();
            //glassSphere.material.Transparency = 1.0f;
            //glassSphere.material.RefractIndex = 1.5f;
            //s.AddObject(glassSphere);

            //Ray r = new Ray(new Point(0, 0.99f, -2), new Vector3(0, 0, 1));

            //Intersection i01 = new Intersection(1.8589f, glassSphere);
            //List<Intersection> xs = new List<Intersection>() { i01 };

            //Computation comps = new Computation(xs[0], r, xs);

            //float reflectance = comps.Schlick();

            //Console.WriteLine(reflectance);

            //----------------------------------------------------------------------------

            //Scene s = new Scene();

            //Ray r = new Ray(new Point(0, 0, -3), new Vector3(0, -(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2));

            //Plane p = new Plane();
            //p.Transform = Matrix4.TranslateMatrix(0, -1, 0);
            //p.material.Reflective = 0.5f;
            //p.material.Transparency = 0.5f;
            //p.material.RefractIndex = 1.5f;

            //s.AddObject(p);

            //Sphere ball = new Sphere();
            //ball.Transform = Matrix4.TranslateMatrix(0, -3.5f, -0.5f);
            //ball.material.mColor = Color.Red;
            //ball.material.Ambient = 0.5f;

            //s.AddObject(ball);

            //Intersection i01 = new Intersection((float)Math.Sqrt(2), p);
            //List<Intersection> xs = new List<Intersection>() { i01 };

            //Computation comps = new Computation(xs[0], r, xs);

            //Color result = s.ShadeHit(comps, 5);
            //Color answer = new Color(0.93391f, 0.69643f, 0.69243f);

            //Console.WriteLine(result.ToString());

            //Cube cube = new Cube();

            //----------------------------------------------------------------------------

            //System.Tuple<Point, Vector3>[] RayTupleList = new Tuple<Point, Vector3>[]
            //{
            //    System.Tuple.Create(new Point(5, 0.5f, 0), new Vector3(-1, 0, 0)), // +x
            //    System.Tuple.Create(new Point(-5, 0.5f, 0), new Vector3(1, 0, 0)), // -x
            //    System.Tuple.Create(new Point(0.5f, 5f, 0), new Vector3(0, -1, 0)), // +y
            //    System.Tuple.Create(new Point(0.5f, -5f, 0), new Vector3(0, 1, 0)), // -y
            //    System.Tuple.Create(new Point(0.5f, 0, 5), new Vector3(0, 0, -1)), // +z
            //    System.Tuple.Create(new Point(0.5f, 0, -5), new Vector3(0, 0, 1)),  // -z
            //    System.Tuple.Create(new Point(0, 0.5f, 0), new Vector3(0, 0, 1))  // inside
            //};

            //System.Tuple<float, float>[] answers = new Tuple<float, float>[]
            //{
            //    System.Tuple.Create(4f, 6f), // +x
            //    System.Tuple.Create(4f, 6f), // -x
            //    System.Tuple.Create(4f, 6f), // +y
            //    System.Tuple.Create(4f, 6f), // -y
            //    System.Tuple.Create(4f, 6f), // +z
            //    System.Tuple.Create(4f, 6f), // -z
            //    System.Tuple.Create(-1f,1f)  // inside
            //};

            //Ray r00 = new Ray(RayTupleList[0].Item1, RayTupleList[0].Item2);
            //Ray r01 = new Ray(RayTupleList[1].Item1, RayTupleList[1].Item2);
            //Ray r02 = new Ray(RayTupleList[2].Item1, RayTupleList[2].Item2);
            //Ray r03 = new Ray(RayTupleList[3].Item1, RayTupleList[3].Item2);
            //Ray r04 = new Ray(RayTupleList[4].Item1, RayTupleList[4].Item2);
            //Ray r05 = new Ray(RayTupleList[5].Item1, RayTupleList[5].Item2);
            //Ray r06 = new Ray(RayTupleList[6].Item1, RayTupleList[6].Item2);

            //List<Intersection> xs00 = cube.LocalIntersects(r00);
            //List<Intersection> xs01 = cube.LocalIntersects(r01);
            //List<Intersection> xs02 = cube.LocalIntersects(r02);
            //List<Intersection> xs03 = cube.LocalIntersects(r03);
            //List<Intersection> xs04 = cube.LocalIntersects(r04);
            //List<Intersection> xs05 = cube.LocalIntersects(r05);
            //List<Intersection> xs06 = cube.LocalIntersects(r06);

            //Console.WriteLine(xs00[0].t);
            //Console.WriteLine(xs00[1].t);

            //----------------------------------------------------------------------------

            //Cylinder cyl = new Cylinder();

            //Vector3 d01 = new Vector3(0, 1, 0).Normalize();
            //Vector3 d02 = new Vector3(0, 1, 0).Normalize();
            //Vector3 d03 = new Vector3(1, 1, 1).Normalize();

            //Point org01 = new Point(1, 0, 0);
            //Point org02 = new Point(0, 1, 0);
            //Point org03 = new Point(0, 0, -5);

            //Ray r01 = new Ray(org01, d01);
            //Ray r02 = new Ray(org02, d02);
            //Ray r03 = new Ray(org03, d03);

            //List<Intersection> xs01 = cyl.LocalIntersects(r01);
            //List<Intersection> xs02 = cyl.LocalIntersects(r02);
            //List<Intersection> xs03 = cyl.LocalIntersects(r03);

            //Console.WriteLine(xs01.Count.ToString());
            //Console.WriteLine(xs02.Count.ToString());
            //Console.WriteLine(xs03.Count.ToString());

            //----------------------------------------------------------------------------

            //Cylinder cyl = new Cylinder();
            //cyl.MaxHeight = float.PositiveInfinity;
            //cyl.MinHeight = float.NegativeInfinity;

            //Point org01 = new Point(1f, 0, -5);
            //Point org02 = new Point(0, 0, -5);
            //Point org03 = new Point(0.5f, 0, -5);

            //Vector3 d01 = new Vector3(0, 0, 1).Normalize();
            //Vector3 d02 = new Vector3(0, 0, 1).Normalize();
            //Vector3 d03 = new Vector3(0.1f, 1, 1).Normalize();

            //Ray r01 = new Ray(org01, d01);
            //Ray r02 = new Ray(org02, d02);
            //Ray r03 = new Ray(org03, d03);

            //List<Intersection> xs01 = cyl.LocalIntersects(r01);
            //List<Intersection> xs02 = cyl.LocalIntersects(r02);
            //List<Intersection> xs03 = cyl.LocalIntersects(r03);

            //Console.WriteLine();

            //Console.WriteLine("List Length: " + xs01.Count);
            //Console.WriteLine("List Length: " + xs02.Count);
            //Console.WriteLine("List Length: " + xs03.Count);

            //Console.WriteLine();

            //Console.WriteLine("t01: " + xs01[0].t);
            //Console.WriteLine("t01: " + xs02[0].t);
            //Console.WriteLine("t01: " + xs03[0].t);

            //Console.WriteLine();

            //Console.WriteLine("t02: " + xs01[1].t);
            //Console.WriteLine("t02: " + xs02[1].t);
            //Console.WriteLine("t02: " + xs03[1].t);

            //----------------------------------------------------------------------------

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

            //List<Intersection> xs01 = cyl.LocalIntersects(r01);
            //List<Intersection> xs02 = cyl.LocalIntersects(r02);
            List<Intersection> xs03 = cyl.LocalIntersects(r03);
            //List<Intersection> xs04 = cyl.LocalIntersects(r04);
            List<Intersection> xs05 = cyl.LocalIntersects(r05);

            //Console.WriteLine("List Length: " + xs01.Count);
            //Console.WriteLine("List Length: " + xs02.Count);
            Console.WriteLine("List Length: " + xs03.Count);
            //Console.WriteLine("List Length: " + xs04.Count);
            Console.WriteLine("List Length: " + xs05.Count);

            //----------------------------------------------------------------------------

            //Cylinder cyl = new Cylinder();
            //cyl.MinHeight = 1.0f;
            //cyl.MaxHeight = 2.0f;
            //cyl.Closed = true;

            //Point org01 = new Point(0, 1.5f, 0);
            //Point org02 = new Point(0, 3, -5);
            //Point org03 = new Point(0, 0, -5);
            //Point org04 = new Point(0, 2, -5);
            //Point org05 = new Point(0, 1, -5);
            //Point org06 = new Point(0, 1.5f, -2);

            //Vector3 d01 = new Vector3(0.1f, 1, 0).Normalize();
            //Vector3 d02 = new Vector3(0, 0, 1).Normalize();
            //Vector3 d03 = new Vector3(0, 0, 1).Normalize();
            //Vector3 d04 = new Vector3(0, 0, 1).Normalize();
            //Vector3 d05 = new Vector3(0, 0, 1).Normalize();
            //Vector3 d06 = new Vector3(0, 0, 1).Normalize();

            //Ray r01 = new Ray(org01, d01);
            //Ray r02 = new Ray(org02, d02);
            //Ray r03 = new Ray(org03, d03);
            //Ray r04 = new Ray(org04, d04);
            //Ray r05 = new Ray(org05, d05);
            //Ray r06 = new Ray(org06, d06);

            //List<Intersection> xs01 = cyl.LocalIntersects(r01);
            //List<Intersection> xs02 = cyl.LocalIntersects(r02);
            //List<Intersection> xs03 = cyl.LocalIntersects(r03);
            //List<Intersection> xs04 = cyl.LocalIntersects(r04);
            //List<Intersection> xs05 = cyl.LocalIntersects(r05);
            //List<Intersection> xs06 = cyl.LocalIntersects(r06);

            //Console.WriteLine(xs01.Count);
            //Console.WriteLine(xs02.Count);
            //Console.WriteLine(xs03.Count);
            //Console.WriteLine(xs04.Count);
            //Console.WriteLine(xs05.Count);
            //Console.WriteLine(xs06.Count);

            //----------------------------------------------------------------------------

            //Cone cone = new Cone();

            //Point org01 = new Point(0, 0, -5);
            //Point org02 = new Point(0, 0, -5);
            //Point org03 = new Point(1, 1, -5);

            //Vector3 d01 = new Vector3(0, 0, 1).Normalize();
            //Vector3 d02 = new Vector3(1, 1, 1).Normalize();
            //Console.WriteLine(d02.ToString());
            //Vector3 d03 = new Vector3(-0.5f, -1, 1).Normalize();

            //Ray r01 = new Ray(org01, d01);
            //Ray r02 = new Ray(org02, d02);
            //Ray r03 = new Ray(org03, d03);

            //List<Intersection> xs01 = cone.LocalIntersects(r01);
            //List<Intersection> xs02 = cone.LocalIntersects(r02);
            //List<Intersection> xs03 = cone.LocalIntersects(r03);

            //Console.WriteLine(xs01.Count);
            //Console.WriteLine(xs02.Count);
            //Console.WriteLine(xs03.Count);

            //Console.WriteLine();

            //Console.WriteLine(xs01[0].t); // 5
            //Console.WriteLine(xs02[0].t); // 8.66025f
            //Console.WriteLine(xs03[0].t); // 4.55006f

            //Console.WriteLine();

            //Console.WriteLine(xs01[1].t); // 5
            //Console.WriteLine(xs02[1].t); // 8.66025f
            //Console.WriteLine(xs03[1].t); // 49.44994f

            //----------------------------------------------------------------------------

            //Cone cone = new Cone();
            //Vector3 d01 = new Vector3(0, 1, 1).Normalize();
            //Ray r01 = new Ray(new Point(0, 0, -1), d01);

            //List<Intersection> xs = cone.LocalIntersects(r01);

            //Console.WriteLine(xs.Count);
            //Console.WriteLine(xs[0].t); // 0.35355f

            //----------------------------------------------------------------------------

            //Cone cone = new Cone();

            //cone.MinHeight = -0.5f;
            //cone.MaxHeight = 0.5f;
            //cone.Closed = true;

            //Point org01 = new Point(0, 0, -5);
            //Point org02 = new Point(0, 0, -0.25f);
            //Point org03 = new Point(0, 0, -0.25f);

            //Vector3 d01 = new Vector3(0, 1, 0).Normalize();
            //Vector3 d02 = new Vector3(0, 1, 1).Normalize();
            //Vector3 d03 = new Vector3(0, 1, 0).Normalize();

            //Ray r01 = new Ray(org01, d01);
            //Ray r02 = new Ray(org02, d02);
            //Ray r03 = new Ray(org03, d03);

            //List<Intersection> xs01 = cone.LocalIntersects(r01);
            //List<Intersection> xs02 = cone.LocalIntersects(r02);
            //List<Intersection> xs03 = cone.LocalIntersects(r03);

            //Console.WriteLine(xs01.Count); // 0
            //Console.WriteLine(xs02.Count); // 2
            //Console.WriteLine(xs03.Count); // 4

            //----------------------------------------------------------------------------

            //Cone cone = new Cone();

            //Point p01 = new Point(0, 0, 0);
            //Point p02 = new Point(1, 1, 1);
            //Point p03 = new Point(-1, -1, 0);

            //Vector3 n01 = cone.LocalNormal(p01);
            //Vector3 n02 = cone.LocalNormal(p02);
            //Vector3 n03 = cone.LocalNormal(p03);

            //Vector3 ans01 = new Vector3(0, 0, 0);
            //Vector3 ans02 = new Vector3(1, (float)Math.Sqrt(2), 1);
            //Vector3 ans03 = new Vector3(-1, -1, 0);

            //Console.WriteLine(n01.ToString());
            //Console.WriteLine(n02.ToString());
            //Console.WriteLine(n03.ToString());
        }
    }
}
