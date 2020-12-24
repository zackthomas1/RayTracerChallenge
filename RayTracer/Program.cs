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

            Group g = new Group();
            Sphere s = new Sphere();
            g.AddChild(s);

            Console.WriteLine(g.childern.Count);
        }
    }
}
