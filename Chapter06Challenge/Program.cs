using System;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;
namespace Chapter06Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define projection plane
            Point rayOrigin = new Point(0, 0, -5);
            float wall_Z = 10.0f;
            float wallSize = 7.0f;

            // Create Canvas
            int canvasPixels = 900;
            Canvas canvas = new Canvas(canvasPixels, canvasPixels);
            float pixelSize = wallSize / canvasPixels;
            float canvasMidPoint = wallSize / 2;

            // Create Sphere in scene
            Material m1 = new Material(Color.SetColor(1, 0.2f, 1), specular: 50);
            Sphere sphere = new Sphere(m1);
            sphere.position = new Point(0.5f, -0.25f, 0);

            // Add light source
            Point lightPosition = new Point(30, 10, -10);
            Color lightColor = Color.White;
            Light light = new Light(lightColor, lightPosition);

            // Apply Transforms 
            //sphere.transformMatrix = Matrix4.TranslateMatrix(0,-0.75f,0) * Matrix4.ScaleMatrix(1, 0.5f, 1);

            for (int y = 0; y < canvasPixels; y++)
            {
                float world_Y = canvasMidPoint - pixelSize * y;
                for (int x = 0; x < canvasPixels; x++)
                {
                    float world_X = -canvasMidPoint + pixelSize * x;

                    Point position = new Point(world_X, world_Y, wall_Z);
                    Ray r = new Ray(rayOrigin, (position - rayOrigin).Normalize());

                    List<Intersection> xs = sphere.Intersect(r);

                    if (Intersection.Hit(xs) != null)
                    {
                        Intersection hit = Intersection.Hit(xs);
                        Console.WriteLine("Hit: " + hit.ToString());

                        Point point = r.Position(hit.t);
                        Vector3 normal = sphere.GetNormal(point);
                        Vector3 eyeV = -r.direction;

                        Color phongColor = sphere.material.Lighting(hit.rayObject.material, light, point, eyeV, normal);
                        if (Math.Abs(xs[0].t - xs[1].t) < .5f)
                        {
                            phongColor = phongColor * new Color(0.1f, 2.5f, 0.1f);
                        }

                        Console.WriteLine("Color: " + phongColor.ToString());
                        canvas.SetPixelColor(x, y, phongColor);
                    }
                }
            }


            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "Chapter06Challenge_05";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, canvas);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();

        }
    }
}
