using System;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace Chapter05Challenge
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
            Color color = Color.Red;
           
            // Create Sphere in scene
            Sphere sphere = new Sphere();

            // Apply Transforms 
            //sphere.transformMatrix = Matrix4.TranslateMatrix(0,-0.75f,0) * Matrix4.ScaleMatrix(1, 0.5f, 1);
            //sphere.transformMatrix = Matrix4.ScaleMatrix(0.5f, 1, 1) * Matrix4.RotateMatrix_Y(Math.PI / 6);
            sphere.transformMatrix = Matrix4.ShearMatrix(1, 0, 0, 0, 0, 0) * Matrix4.ScaleMatrix(.5f, 1, 1); 


            for (int y = 0; y < canvasPixels; y++)
            {
                float world_Y = canvasMidPoint - pixelSize * y;
                for (int x = 0; x < canvasPixels; x++)
                {
                    float world_X = -canvasMidPoint + pixelSize * x;

                    Point position = new Point(world_X, world_Y, wall_Z);
                    Ray r = new Ray(rayOrigin, (position - rayOrigin).Normalize());
                    r = r.ApplyObjectTransform(sphere);

                    List<Intersection> xs = sphere.Intersect(r);


                    //Console.WriteLine(Intersection.Hit(xs).t.ToString());
                    if (Intersection.Hit(xs) != null)
                    {
                        if (Math.Abs(xs[0].t - xs[1].t) < .5f)
                        {
                            canvas.SetPixelColor(x, y, Color.Blue);
                        }
                        else
                        {
                            canvas.SetPixelColor(x, y, color);
                        }
                    }

                }
            }


            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "Chapter05Challenge_06";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, canvas);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();

        }
    }
}
