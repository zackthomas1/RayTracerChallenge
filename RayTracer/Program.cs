using System;
using System.IO;
using System.Collections.Generic;

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

            Cube cube = new Cube();

            System.Tuple<Point, Vector3>[] RayTupleList = new Tuple<Point, Vector3>[]
            {
                System.Tuple.Create(new Point(5, 0.5f, 0), new Vector3(-1, 0, 0)), // +x
                System.Tuple.Create(new Point(-5, 0.5f, 0), new Vector3(1, 0, 0)), // -x
                System.Tuple.Create(new Point(0.5f, 5f, 0), new Vector3(0, -1, 0)), // +y
                System.Tuple.Create(new Point(0.5f, -5f, 0), new Vector3(0, 1, 0)), // -y
                System.Tuple.Create(new Point(0.5f, 0, 5), new Vector3(0, 0, -1)), // +z
                System.Tuple.Create(new Point(0.5f, 0, -5), new Vector3(0, 0, 1)),  // -z
                System.Tuple.Create(new Point(0, 0.5f, 0), new Vector3(0, 0, 1))  // inside
            };

            System.Tuple<float, float>[] answers = new Tuple<float, float>[]
            {
                System.Tuple.Create(4f, 6f), // +x
                System.Tuple.Create(4f, 6f), // -x
                System.Tuple.Create(4f, 6f), // +y
                System.Tuple.Create(4f, 6f), // -y
                System.Tuple.Create(4f, 6f), // +z
                System.Tuple.Create(4f, 6f), // -z
                System.Tuple.Create(-1f,1f)  // inside
            };

            Ray r00 = new Ray(RayTupleList[0].Item1, RayTupleList[0].Item2);
            Ray r01 = new Ray(RayTupleList[1].Item1, RayTupleList[1].Item2);
            Ray r02 = new Ray(RayTupleList[2].Item1, RayTupleList[2].Item2);
            Ray r03 = new Ray(RayTupleList[3].Item1, RayTupleList[3].Item2);
            Ray r04 = new Ray(RayTupleList[4].Item1, RayTupleList[4].Item2);
            Ray r05 = new Ray(RayTupleList[5].Item1, RayTupleList[5].Item2);
            Ray r06 = new Ray(RayTupleList[6].Item1, RayTupleList[6].Item2);

            List<Intersection> xs00 = cube.Intersect(r00);
            List<Intersection> xs01 = cube.Intersect(r01);
            List<Intersection> xs02 = cube.Intersect(r02);
            List<Intersection> xs03 = cube.Intersect(r03);
            List<Intersection> xs04 = cube.Intersect(r04);
            List<Intersection> xs05 = cube.Intersect(r05);
            List<Intersection> xs06 = cube.Intersect(r06);

            Console.WriteLine(xs00[0].t);
            Console.WriteLine(xs00[1].t);




        }
    }
}
