using System;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using RayTracer.RayObjects;

namespace ChapterChallenges
{

    class Program
    {

        public static void Chapter05()
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
            sphere.Transform = Matrix4.ShearMatrix(1, 0, 0, 0, 0, 0) * Matrix4.ScaleMatrix(.5f, 1, 1);


            for (int y = 0; y < canvasPixels; y++)
            {
                float world_Y = canvasMidPoint - pixelSize * y;
                for (int x = 0; x < canvasPixels; x++)
                {
                    float world_X = -canvasMidPoint + pixelSize * x;

                    Point position = new Point(world_X, world_Y, wall_Z);
                    Ray r = new Ray(rayOrigin, (position - rayOrigin).Normalize());
                    r = r.ApplyObjectTransform(sphere);

                    List<Intersection> xs = sphere.GetIntersects(r);


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

                    List<Intersection> xs = sphere.GetIntersects(r);

                    if (Intersection.Hit(xs) != null)
                    {
                        Intersection hit = Intersection.Hit(xs);
                        Console.WriteLine("Hit: " + hit.ToString());

                        Point point = r.GetPointPosition(hit.t);
                        Vector3 normal = sphere.GetNormal(point);
                        Vector3 eyeV = -r.direction;

                        Color phongColor = sphere.material.Lighting(hit.rayObject.material, hit.rayObject, light, point, eyeV, normal, new Tuple<bool, RayObject>(false, null));
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
            floor.Transform = Matrix4.ScaleMatrix(10, 0.01f, 10);
            floor.material = new Material();
            floor.material.mColor = new Color(1, 0.9f, 0.9f);
            floor.material.Specular = 0;

            Sphere leftWall = new Sphere();
            leftWall.Transform = Matrix4.TranslateMatrix(0, 0, 5) * Matrix4.RotateMatrix_Y(-(float)Math.PI / 4) *
                                        Matrix4.RotateMatrix_X((float)Math.PI/2) * Matrix4.ScaleMatrix(10, 0.01f, 10);
            leftWall.material = floor.material;

            Sphere rightWall = new Sphere();
            rightWall.Transform = Matrix4.TranslateMatrix(0, 0, 5) * Matrix4.RotateMatrix_Y((float)Math.PI / 4) *
                                        Matrix4.RotateMatrix_X((float)Math.PI/2) * Matrix4.ScaleMatrix(10, 0.01f, 10);
            rightWall.material = floor.material;

            Sphere middle = new Sphere();
            middle.Transform = Matrix4.TranslateMatrix(-0.5f, 1, 0.5f);
            middle.material = new Material();
            middle.material.mColor = new Color(0.1f, 1, 0.5f);
            middle.material.Diffuse = 0.7f;
            middle.material.Specular = 0.3f;

            Sphere right = new Sphere();
            right.Transform = Matrix4.TranslateMatrix(1.5f, 1.0f, -0.5f) * Matrix4.ScaleMatrix(0.75f, 0.25f, 0.75f);
            right.material = new Material();
            right.material.mColor = new Color(0.5f, 1, 0.1f);
            right.material.Diffuse = 0.4f;
            right.material.Specular = 0.6f;

            Sphere left = new Sphere();
            left.Transform = Matrix4.TranslateMatrix(-1.5f, 0.33f, -0.75f) * Matrix4.ScaleMatrix(0.33f, 0.33f, 0.33f);
            left.material = new Material();
            left.material.mColor = new Color(1.0f, 0.8f, 0.1f);
            left.material.Diffuse = 0.7f;
            left.material.Specular = 0.1f;

            // Create Scene
            Scene scene = new Scene();
            Light l1 = new Light(new Color(0.5f, 0.5f, 0.5f), new Point(-10, 10, -10));
            Light l2 = new Light(new Color(0.65f, 0.65f, 0.65f), new Point(10, 10, -10));
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
            string fileName = "Chapter07Challenge_05";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, image);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();
        }

        // Patterns
        public static void Chapter10()
        {

            Plane floor = new Plane();
            floor.material = new Material();
            floor.material.mColor = new Color(1, 0.9f, 0.9f);
            floor.material.Pattern = new CheckerPattern(SolidPattern.White, SolidPattern.White *.25f);
            floor.material.Specular = 0;

            Plane backWall = new Plane();
            backWall.Transform = Matrix4.TranslateMatrix(0, 0, 10) * Matrix4.RotateMatrix_X(Math.PI / 2);
            StripedPattern s01 = new StripedPattern(SolidPattern.White, SolidPattern.Black);
            StripedPattern s02 = new StripedPattern(SolidPattern.Red, SolidPattern.Green);
            s02.Transform = Matrix4.RotateMatrix_Y((float)Math.PI / 4);
            backWall.material.Pattern = new BlendPattern(s01, s02);
            backWall.material.Specular = 0;

            Plane wallRight = new Plane();
            wallRight.Transform = Matrix4.TranslateMatrix(4, 0, 0) * Matrix4.RotateMatrix(0, 0, Math.PI / 2);
            wallRight.material = new Material();
            wallRight.material.mColor = Color.Blue;
            floor.material.Specular = 0;

            Plane wallLeft = new Plane();
            wallLeft.Transform = Matrix4.TranslateMatrix(-4, 0, 0) * Matrix4.RotateMatrix(0, 0, Math.PI / 2);
            wallLeft.material = new Material();
            wallLeft.material.mColor = Color.Red;
            wallLeft.material.Pattern = new CheckerPattern(new StripedPattern(SolidPattern.Yellow, SolidPattern.Green), new RingPattern(SolidPattern.White, SolidPattern.Black));
            floor.material.Specular = 0;

            Sphere middle = new Sphere();
            middle.Transform = Matrix4.TranslateMatrix(-1.5f, 1, 2.0f);
            middle.material = new Material();
            middle.material.mColor = new Color(0.1f, 1, 0.5f);
            middle.material.Pattern = new TestPattern();
            middle.material.Pattern.Transform = Matrix4.ScaleMatrix(.25f, .25f, .25f);
            middle.material.Diffuse = 0.7f;
            middle.material.Specular = 0.3f;

            Sphere middle02 = new Sphere();
            middle02.Transform = Matrix4.TranslateMatrix(1.5f, 1, 2.0f);
            middle02.material = new Material();
            middle02.material.mColor = new Color(0.1f, 1, 0.5f);
            middle02.material.Pattern = new RadialGradientPattern(SolidPattern.Purple, SolidPattern.Orange);
            middle02.material.Pattern.Transform = Matrix4.RotateMatrix_Z((float)Math.PI/2)  * Matrix4.ScaleMatrix(0.25f, 0.25f, 0.25f);
            middle02.material.Diffuse = 0.7f;
            middle02.material.Specular = 0.3f;
            
            Sphere middle03 = new Sphere();
            middle03.Transform = Matrix4.TranslateMatrix(0, .5f, -1.5f) * Matrix4.ScaleMatrix(.5f, .5f, .5f);
            middle03.material = new Material();
            middle03.material.mColor = new Color(0.1f, 1, 0.5f);
            middle03.material.Pattern = new CheckerPattern(SolidPattern.Yellow, SolidPattern.Green);
            middle03.material.Pattern.Transform = Matrix4.ScaleMatrix(0.5f, 0.5f, 0.5f);
            middle03.material.Diffuse = 0.7f;
            middle03.material.Specular = 0.3f;

            Sphere right = new Sphere();
            right.Transform = Matrix4.TranslateMatrix(2.0f, 1.0f, -0.5f) * Matrix4.ScaleMatrix(0.85f, 0.25f, 0.85f);
            right.material = new Material();
            right.material.mColor = new Color(0.5f, 1, 0.1f);
            right.material.Pattern = new StripedPattern(SolidPattern.White, SolidPattern.White * .25f);
            right.material.Pattern.Transform = Matrix4.ScaleMatrix(.15f, 1, 1);
            right.material.Diffuse = 0.4f;
            right.material.Specular = 0.6f;

            Sphere left = new Sphere();
            left.Transform = Matrix4.TranslateMatrix(-2.5f, 0.33f, -0.75f) * Matrix4.ScaleMatrix(0.33f, 0.33f, 0.33f);
            left.material = new Material();
            left.material.mColor = new Color(1.0f, 0.8f, 0.1f);
            left.material.Pattern = new GradientPattern(SolidPattern.Yellow, SolidPattern.Blue);
            left.material.Diffuse = 0.7f;
            left.material.Specular = 0.1f;

            // Create Scene
            Scene scene = new Scene();
            Light l1 = new Light(Color.White * .8f, new Point(-3, 10, -10));
            Light l2 = new Light(new Color(0.5f, 0.5f, 0.65f), new Point(3, 10, -5));
            List<Light> lights = new List<Light>() { l1, l2 };
            scene.Lights = lights;


            List<RayObject> sceneObjects = new List<RayObject>() { floor, backWall, wallRight, wallLeft, middle, middle02, middle03, right, left };
            scene.Objects = sceneObjects;

            // Create Camera
            Camera cam = new Camera(1920 / 4, 1080 / 4, (float)Math.PI / 4);
            cam.Transform = cam.ViewTransform(new Point(0, 2f, -8),
                                              new Point(0, 1f, 0),
                                              new Vector3(0, 1, 0));
            Canvas image = cam.Render(scene); // Outputs image



            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "Chapter10Challenge_07";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, image);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();
        }

        // reflections/refractions
        public static void Chapter11()
        {
            // Create objects
            Plane floor = new Plane();
            floor.material = new Material();
            floor.material.Pattern = new CheckerPattern(SolidPattern.White, SolidPattern.White * .15f);
            //floor.material.Reflective = 0.2f;
            floor.material.Specular = 0.1f;

            Plane backWall = new Plane();
            backWall.Transform = Matrix4.TranslateMatrix(0, 0, 10) * Matrix4.RotateMatrix_X(Math.PI / 2);
            backWall.material = new Material();
            backWall.material.Pattern = new CheckerPattern(SolidPattern.White, SolidPattern.White * .15f);
            //backWall.material.Reflective = 0.6f;
            backWall.material.Specular = 0.1f;

            Sphere s01 = new Sphere();
            s01.Transform = Matrix4.TranslateMatrix(1.5f, 1, 0);
            s01.material.mColor = Color.Red;
            s01.material.Transparency = 0.0f;
            s01.material.Reflective = 0.6f;
            //s01.material.Specular = 0.0f;

            Sphere s02 = new Sphere();
            s02.Transform = Matrix4.TranslateMatrix(-1.5f, 1, -0.75f) * Matrix4.ScaleMatrix(0.5f, 0.5f, 0.5f);
            s02.material.mColor = Color.White * .15f;
            s02.material.Specular = 1f;
            s02.material.Shininess = 300f;
            s02.material.Reflective = 1f;
            s02.material.Transparency = .95f;
            s02.material.RefractIndex = RefractiveIndex.Air;

            Sphere s03 = new Sphere();
            s03.Transform = Matrix4.TranslateMatrix(-1.5f, 1, -0.75f) * Matrix4.ScaleMatrix(1f, 1f, 1f);
            s03.material.mColor = Color.White * .15f;
            s03.material.Specular = 1f;
            s03.material.Shininess = 300f;
            s03.material.Reflective = 1f;
            s03.material.Transparency = .95f;
            s03.material.RefractIndex = RefractiveIndex.Glass;

            // Create scene
            Scene scene = new Scene();
            scene.Lights[0].Insensity = scene.Lights[0].Insensity * 0.5f;
            Light l02 = new Light(((Color.White * .9f) + (Color.Orange * .1f) *.5f), new Point(10, 10, -10));
            scene.AddLight(l02);

            scene.Objects = new List<RayObject>() { floor, backWall, s01, s02, s03 };

            // Create Camera
            Camera cam = new Camera(1920 / 4, 1080 / 4, (float)Math.PI / 4);
            cam.Transform = cam.ViewTransform(new Point(0, 2f, -8),
                                              new Point(0, 1f, 0),
                                              new Vector3(0, 1, 0));
            Canvas image = cam.Render(scene); // Outputs image

            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "Chapter11_08";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, image);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();

        }

        // Cube
        public static void Chapter12()
        {
            // Create objects
            Plane floor = new Plane();
            floor.material = new Material();
            floor.material.Pattern = new CheckerPattern(SolidPattern.White, SolidPattern.White * .15f);
            //floor.material.Reflective = 0.2f;
            floor.material.Specular = 0.1f;

            Plane backWall = new Plane();
            backWall.Transform = Matrix4.TranslateMatrix(0, 0, 10) * Matrix4.RotateMatrix_X(Math.PI / 2);
            backWall.material = new Material();
            backWall.material.Pattern = new CheckerPattern(SolidPattern.White, SolidPattern.White * .15f);
            //backWall.material.Reflective = 0.6f;
            backWall.material.Specular = 0.1f;

            Cube cube01 = new Cube();
            cube01.Transform = Matrix4.TranslateMatrix(1.5f, 1, 0) * Matrix4.RotateMatrix_Y(Math.PI / 4);
            cube01.material.mColor = Color.Red;
            cube01.material.Transparency = 0.0f;
            cube01.material.Reflective = 0.4f;
            //s01.material.Specular = 0.0f;

            Sphere s02 = new Sphere();
            s02.Transform = Matrix4.TranslateMatrix(-1.5f, 1, -0.75f) * Matrix4.ScaleMatrix(0.5f, 0.5f, 0.5f);
            s02.material.mColor = Color.White * .15f;
            s02.material.Specular = 1f;
            s02.material.Shininess = 300f;
            s02.material.Reflective = 1f;
            s02.material.Transparency = .95f;
            s02.material.RefractIndex = RefractiveIndex.Air;

            Sphere s03 = new Sphere();
            s03.Transform = Matrix4.TranslateMatrix(-1.5f, 1, -0.75f) * Matrix4.ScaleMatrix(1f, 1f, 1f);
            s03.material.mColor = Color.White * .15f;
            s03.material.Specular = 1f;
            s03.material.Shininess = 300f;
            s03.material.Reflective = 1f;
            s03.material.Transparency = .95f;
            s03.material.RefractIndex = RefractiveIndex.Glass;

            // Create scene
            Scene scene = new Scene();
            scene.Objects = new List<RayObject>() { floor, backWall, cube01, s02, s03 };

            // Create Camera
            Camera cam = new Camera(1920 / 4, 1080 / 4, (float)Math.PI / 4);
            cam.Transform = cam.ViewTransform(new Point(0, 2f, -8),
                                              new Point(0, 1f, 0),
                                              new Vector3(0, 1, 0));
            Canvas image = cam.Render(scene); // Outputs image

            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "Chapter12_02";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, image);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();

        }

        // Cylinder
        public static void Chapter13()
        {
            // Create scene
            Scene scene = new Scene();

            // Create Lights 
            scene.Lights[0].Insensity = scene.Lights[0].Insensity * 0.5f;
            Light l02 = new Light(((Color.White * .9f) + (Color.Orange * .1f) * 0.5f), new Point(10, 10, -10));
            scene.AddLight(l02);

            // Create objects
            Plane floor = new Plane();
            floor.material = new Material();
            floor.material.Pattern = new CheckerPattern(SolidPattern.White, SolidPattern.White * .15f);
            //floor.material.Reflective = 0.2f;
            floor.material.Specular = 0.1f;

            Plane backWall = new Plane();
            backWall.Transform = Matrix4.TranslateMatrix(0, 0, 10) * Matrix4.RotateMatrix_X(Math.PI / 2);
            backWall.material = new Material();
            backWall.material.Pattern = new CheckerPattern(SolidPattern.White, SolidPattern.White * .15f);
            //backWall.material.Reflective = 0.6f;
            backWall.material.Specular = 0.1f;

            Cube cube01 = new Cube();
            cube01.Transform = Matrix4.TranslateMatrix(4f, 1, 0);
            cube01.material.mColor = Color.Red;
            cube01.material.Transparency = 0.0f;
            cube01.material.Reflective = 0.4f;
            //s01.material.Specular = 0.0f;

            Cone cone01 = new Cone();
            cone01.Transform = Matrix4.TranslateMatrix(-4f, 2.0f, -0.5f) * Matrix4.RotateMatrix_X(-(Math.PI/2) * .25f)* Matrix4.ScaleMatrix(1.5f, 1.5f, 1.5f);
            cone01.material.mColor = Color.Blue;
            cone01.MaxHeight = 0.0f;
            cone01.MinHeight = -1.0f;
            cone01.material.Transparency = 0.0f;
            cone01.material.Reflective = 0.05f;
            cone01.material.Specular = 0.8f;

            Sphere s03 = new Sphere();
            s03.Transform = Matrix4.TranslateMatrix(0.5f, 2, -3f) * Matrix4.ScaleMatrix(1f, 1f, 1f);
            s03.material.mColor = Color.White * .15f;
            s03.material.Specular = 1f;
            s03.material.Shininess = 300f;
            s03.material.Reflective = 1f;
            s03.material.Transparency = .95f;
            s03.material.RefractIndex = RefractiveIndex.Glass;

            Cylinder cyl01 = new Cylinder();
            cyl01.MaxHeight = 3f;
            cyl01.MinHeight = 0f;
            cyl01.Closed = true;
            cyl01.Transform = Matrix4.TranslateMatrix(-2f, 0f, 2);
            Console.WriteLine(cyl01.Transform.ToString());
            Console.WriteLine(cyl01.Transform.Determinate());
            cyl01.material.mColor = Color.Orange;
            cyl01.material.Specular = .2f;
            cyl01.material.Shininess = 300f;
            cyl01.material.Reflective = .95f;
            //cyl01.material.Transparency = .95f;
            //cyl01.material.RefractIndex = RefractiveIndex.Glass;

            scene.Objects = new List<RayObject>() { floor, backWall, cube01, cone01, s03, cyl01 };

            // Create Camera
            Camera cam = new Camera(1920 / 6, 1080 / 6, (float)Math.PI / 4);
            cam.Transform = cam.ViewTransform(new Point(-.5f, 2f, -15.5f),
                                              new Point(-.5f, 1f, 0),
                                              new Vector3(0, 1, 0));
            Canvas image = cam.Render(scene); // Outputs image

            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "Chapter13_08";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, image);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();
        }


        static void Main(string[] args)
        {
            Chapter13();
        }
    }
}
