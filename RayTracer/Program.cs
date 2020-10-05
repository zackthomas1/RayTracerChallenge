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

            Scene s = new Scene();

            Ray r = new Ray(new Point(0, 0, -3), new Vector3(0, -(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2));

            Plane p = new Plane();
            p.Transform = Matrix4.TranslateMatrix(0, -1, 0);
            p.material.Reflective = 0.5f;
            p.material.Transparency = 0.5f;
            p.material.RefractIndex = 1.5f;

            s.AddObject(p);

            Sphere ball = new Sphere();
            ball.Transform = Matrix4.TranslateMatrix(0, -3.5f, -0.5f);
            ball.material.mColor = Color.Red;
            ball.material.Ambient = 0.5f;

            s.AddObject(ball);

            Intersection i01 = new Intersection((float)Math.Sqrt(2), p);
            List<Intersection> xs = new List<Intersection>() { i01 };

            Computation comps = new Computation(xs[0], r, xs);

            Color result = s.ShadeHit(comps, 5);
            Color answer = new Color(0.93391f, 0.69643f, 0.69243f);

            Console.WriteLine(result.ToString());
        }
    }
}
