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

        public static void Chapter06()
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
            sphere.Position = new Point(0.5f, -0.25f, 0);

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

                        Point point = r.GetPointPosition(hit.t);
                        Vector3 normal = sphere.GetNormal(point);
                        Vector3 eyeV = -r.direction;

                        Color phongColor = sphere.material.Lighting(hit.rayObject.material, light, point, eyeV, normal, false);
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

        public static void Chapter07()
        {
            // Define scene objects
            Sphere floor = new Sphere();
            floor.TransformMatrix = Matrix4.ScaleMatrix(10, 0.01f, 10);
            floor.material = new Material();
            floor.material.mColor = new Color(1, 0.9f, 0.9f);
            floor.material.Specular = 0;

            Sphere leftWall = new Sphere();
            leftWall.TransformMatrix = Matrix4.TranslateMatrix(0, 0, 5) * Matrix4.RotateMatrix_Y(-(float)Math.PI / 4) *
                                        Matrix4.RotateMatrix_X((float)Math.PI/2) * Matrix4.ScaleMatrix(10, 0.01f, 10);
            leftWall.material = floor.material;

            Sphere rightWall = new Sphere();
            rightWall.TransformMatrix = Matrix4.TranslateMatrix(0, 0, 5) * Matrix4.RotateMatrix_Y((float)Math.PI / 4) *
                                        Matrix4.RotateMatrix_X((float)Math.PI/2) * Matrix4.ScaleMatrix(10, 0.01f, 10);
            rightWall.material = floor.material;

            Sphere middle = new Sphere();
            middle.TransformMatrix = Matrix4.TranslateMatrix(-0.5f, 1, 0.5f);
            middle.material = new Material();
            middle.material.mColor = new Color(0.1f, 1, 0.5f);
            middle.material.Diffuse = 0.7f;
            middle.material.Specular = 0.3f;

            Sphere right = new Sphere();
            right.TransformMatrix = Matrix4.TranslateMatrix(1.5f, 0.5f, -0.5f) * Matrix4.ScaleMatrix(0.5f, 0.5f, 0.5f);
            right.material = new Material();
            right.material.mColor = new Color(0.5f, 1, 0.1f);
            right.material.Diffuse = 0.4f;
            right.material.Specular = 0.6f;

            Sphere left = new Sphere();
            left.TransformMatrix = Matrix4.TranslateMatrix(-1.5f, 0.33f, -0.75f) * Matrix4.ScaleMatrix(0.33f, 0.33f, 0.33f);
            left.material = new Material();
            left.material.mColor = new Color(1.0f, 0.8f, 0.1f);
            left.material.Diffuse = 0.7f;
            left.material.Specular = 0.1f;

            // Create Scene
            Scene scene = new Scene();
            Light l1 = new Light(new Color(0.5f, 0.5f, 0.5f), new Point(-10, 10, -10));
            Light l2 = new Light(new Color(0.5f, 0.5f, 0.5f), new Point(10, 10, -10));
            List<Light> lights = new List<Light>() { l1, l2 };
            scene.Lights = lights; 

            List<RayObject> sceneObjects = new List<RayObject>() { floor, leftWall, rightWall, middle, right, left };
            scene.Objects = sceneObjects;

            // Create Camera
            Camera cam = new Camera(1920/2, 1080/2, (float)Math.PI / 3);
            cam.Transform = cam.ViewTransform(new Point(0, 1.5f, -5),
                                              new Point(0, 1, 0),
                                              new Vector3(0, 1, 0));
            Canvas image = cam.Render(scene); // Outputs image



            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "Chapter07Challenge_04";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, image);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();
        }


        static void Main(string[] args)
        {
            Chapter07();
        }
    }
}
