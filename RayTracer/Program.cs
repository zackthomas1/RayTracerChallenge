using System;
using System.IO;
using System.Collections.Generic;

namespace RayTracer
{
    class Program
    {

        static void Main(string[] args)
        {

            // Testing program
            //Point p1 = new Point(3f, 2f, 1f);
            //Vector3 v2 = new Vector3(5f, 6f, 7f);

            //Point additionPoint = p1 + v2;

            //Console.WriteLine(additionPoint.ToString());

            //-----------------------------------------------------------------------------

            //Vector3 v3 = new Vector3(3, 2, 1);
            //Vector3 v4 = new Vector3(5, 6, 7);

            //Vector3 additionVector = v3 + v4;

            //Console.WriteLine(additionVector.ToString());

            //float[] testArray = new float[] { 3.0f, 4.0f, 8.3f };

            //Color c1 = new Color(-0.5f, 0.4f, 1.7f);
            //Console.WriteLine(c1.ToString());
            //Console.WriteLine(c1.red);

            //Console.ReadKey();

            //-----------------------------------------------------------------------------

            //Canvas canvas = new Canvas(15, 10);
            //string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\Render";
            //string fileName = "HelloWorld8.ppm";

            //string fileDirectoryComplete = filePath + "\\" + fileName;

            //canvas.FillCanvas(Color.Blue());
            //canvas.SetPixelColor(9, 4, Color.Red());
            //canvas.canvas[3, 2] = Color.Green();

            //Console.WriteLine("Writting File");
            //Save.PPM(fileDirectoryComplete, canvas);

            //Console.WriteLine("Done");

            //-----------------------------------------------------------------------------

            //Matrix3 m1 = new Matrix3(1, 2, 5,
            //                         3, 7, 6,
            //                         4, 5, 8);
            //Matrix3 m2 = new Matrix3(2, 4, 7,
            //                         1, 3, 8,
            //                         7, 5, 4);

            //Matrix3 multiplicationResult01 = m1 * m2;
            //Matrix3 answer01 = new Matrix3(39, 35, 43,
            //                               55, 63, 101,
            //                               69, 71, 100);


            //Console.WriteLine(multiplicationResult01.ToString());
            //Console.WriteLine(answer01.ToString());

            //-----------------------------------------------------------------------------

            //Matrix2 m1 = new Matrix2(1, 7,
            //                        2, -4);

            //Matrix2 result = m1.Transpose();
            //Matrix2 answer = new Matrix2(1, 2,
            //                             7, -4);

            //Console.WriteLine(m1.ToString());
            //Console.WriteLine(m1.Transpose().ToString());

            //-----------------------------------------------------------------------------

            //Matrix3 m1 = new Matrix3(2, 5, 6,
            //                         8, 10, -2,
            //                         -3, 5, 4);

            //Matrix2 subMatrix = m1.SubMatrix(0, 2);
            //Matrix2 answer = new Matrix2(8, 10,
            //                            -3, 5);
            //Console.WriteLine(subMatrix.ToString());

            //-----------------------------------------------------------------------------

            //Matrix4 m1 = new Matrix4(-5, 2, 6, -8,
            //                          1, -5, 1, 8,
            //                          7, 7, -6, -7,
            //                          1, -3, 7, 4);

            //Matrix4 result = m1.Invert();
            //Matrix4 answer = new Matrix4(0.21805f, 0.45113f, 0.24060f, -0.04511f,
            //                            -0.80827f, -1.45677f, -0.44361f, 0.52068f,
            //                            -0.07895f, -0.22368f, -0.05263f, 0.19737f,
            //                            -0.52256f, -0.81391f, -0.30075f, 0.30639f);

            //Console.WriteLine(m1.Determinate());
            //Console.WriteLine("result:\n" + result.ToString());
            //Console.WriteLine("answer:\n" + answer.ToString());

            //-----------------------------------------------------------------------------

            //Matrix4 m2 = new Matrix4(8, -5, 9, 2,
            //                         7, 5, 6, 1,
            //                        -6, 0, 9, 6,
            //                        -3, 0, -9, -4);

            //Matrix4 result02 = m2.Invert();
            //Matrix4 answer02 = new Matrix4(-0.15385f, -0.15385f, -0.28205f, -0.53846f,
            //                               -0.07682f, 0.12308f, 0.02564f, 0.03077f,
            //                                0.35897f, 0.35897f, 0.43590f, 0.92308f,
            //                               -0.69231f, -0.69231f, -0.76923f, -1.92308f);

            //Console.WriteLine(m2.Determinate());
            //Console.WriteLine("result:\n" + result02.ToString());
            //Console.WriteLine("answer:\n" + answer02.ToString());

            //-----------------------------------------------------------------------------

            //Tuple v1 = new Tuple(2, 5, 6); 
            //Matrix4 m1 = new Matrix4(-3, 5f, 1f, 4,
            //                        -2, 4, 5, 9,
            //                         7, 9, 8, 3,
            //                         4, 6, 7, 2);
            //Tuple result = (m1 * v1);

            //Console.WriteLine(result.ToString());

            //-----------------------------------------------------------------------------

            //Point p1 = new Point(-3, 4, 5);
            //Matrix4 m2 = new Matrix4();

            //m2 = m2.Translation(5, -3, 2);

            //Point result = m2 * p1;
            //Point invert = m2.Invert() * p1; 
            //Point answer = new Point(2, 1, 7);

            //Console.WriteLine("Point: " + p1.ToString());
            //Console.WriteLine("Result: " + result.ToString());
            //Console.WriteLine("Answer: " + answer.ToString());
            //Console.WriteLine("Invert: " + invert.ToString());

            //-----------------------------------------------------------------------------

            //Vector3 v1 = new Vector3(-3, 4, 5);
            //Matrix4 m2 = new Matrix4();

            //m2 = m2.Translate(5, -3, 2);
            //m2 = m2.Invert();

            //Vector3 result = v1 * m2;

            //Console.WriteLine("Vector: " + v1.ToString());
            //Console.WriteLine("Result: " + result.ToString());

            //-----------------------------------------------------------------------------
            //Point p1 = new Point(0, 0, 1);
            //Matrix4 rotation01 = new Matrix4();
            //Matrix4 rotation02 = new Matrix4();

            //rotation01 = rotation01.Rotate_Y(Math.PI / 2);

            //Point result01 = p1 * rotation01;
            //Point answer01 = new Point(1, 0, 0);

            //Console.WriteLine("Result: " + result01.ToString());
            //Console.WriteLine("Answer: " + answer01.ToString());
            //Console.WriteLine(result01 == answer01);
            //Console.WriteLine(6.123234E-17 < Utilities.Epsilon);

            //-----------------------------------------------------------------------------

            //Point p1 = new Point(1, 0, 1);
            //Matrix4 identity = new Matrix4();

            //Matrix4 rotateX = Matrix4.RotateMatrix_X(Math.PI / 2);
            //Matrix4 scale = Matrix4.ScaleMatrix(5, 5, 5);
            //Matrix4 translate = Matrix4.TranslateMatrix(10, 5, 7);

            //Point p2 = p1 * (translate * scale * rotateX);
            //Point p3 = p1 * identity.Translate(10, 5, 7).Scale(5, 5, 5).Rotate_X(Math.PI / 2);
            //Point p4 = p1 * identity.Rotate_X(Math.PI / 2).Scale(5, 5, 5).Translate(10, 5, 7);

            //Console.WriteLine(p2.ToString());
            //Console.WriteLine(p3.ToString());
            //Console.WriteLine(p4.ToString());

            //-----------------------------------------------------------------------------

            //Matrix4 rotation01 = new Matrix4();
            //Matrix4 rotation02 = new Matrix4();

            //rotation01 = rotation01.RotateDegree(0, 0, 90);
            //rotation02 = rotation02.RotateDegree_Z(90);

            //Console.WriteLine(rotation01.ToString());
            //Console.WriteLine(rotation02.ToString());

            //-----------------------------------------------------------------------------

            //Ray ray01 = new Ray(new Point(2, 3, 4), new Vector3(1, 0, 0));
            //Console.WriteLine(ray01.ToString());

            //Sphere sphere = new Sphere();
            //Sphere sphere01 = new Sphere();
            //Sphere sphere02 = new Sphere();

            //Console.WriteLine(sphere.ToString() + ": " + sphere.position);

            //Console.WriteLine(sphere01.ToString());
            //Console.WriteLine(sphere02.ToString());

            //-----------------------------------------------------------------------------

            //Ray ray01 = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            //Sphere sphere = new Sphere();

            //List<Intersection> intersecionPoints = sphere.Intersect(ray01);

            //Console.WriteLine(intersecionPoints[0].rayObject + ": " + intersecionPoints[0].t);
            //Console.WriteLine(intersecionPoints[1].rayObject + ": " + intersecionPoints[1].t);

            //-----------------------------------------------------------------------------

            //Ray r1 = new Ray(new Point(1, 2, 3), new Vector3(0, 1, 0));
            //Matrix4 translate = Matrix4.TranslateMatrix(3, 4, 5);
            //Ray r2 = r1.transform(translate);

            //Console.WriteLine(r2.origin.ToString());
            //Console.WriteLine(r2.direction.ToString());

            //-----------------------------------------------------------------------------

            //Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            //Sphere s = new Sphere();
            //s.SetTranform(Matrix4.ScaleMatrix(2, 2, 2));

            //Console.WriteLine(s.transformMatrix.ToString());

            //Console.WriteLine(s.position.ToString());

            //List<Intersection> xs = s.Intersect(ray);

            //Console.WriteLine((s.position * s.transformMatrix).ToString());

            //-----------------------------------------------------------------------------

            //Material m = new Material();

            //Console.WriteLine(m.ToString());

            //-----------------------------------------------------------------------------

            //Sphere s1 = new Sphere();
            //s1.material.mColor = Color.Blue(); 
            //Console.WriteLine(s1.material.ToString());

            //Console.WriteLine();

            //Material m2 = new Material(Color.Red());
            //Sphere s2 = new Sphere(m2);
            //Console.WriteLine(s2.material.ToString());

            //----------------------------------------------------------------------------

            //Light light01 = new Light();
            //Console.WriteLine(light01.ToString());

            //Console.WriteLine();

            //Light light02 = new Light();
            //Console.WriteLine(light02.ToString());

            //----------------------------------------------------------------------------
            Sphere sphere = new Sphere();

            Material m = new Material();
            Point position = new Point(0, 0, 0);

            Vector3 normalV = new Vector3(0, 0, -1);
            Vector3 eyeV = new Vector3(0, 0, -1);
            Light light = new Light(Color.White(), new Point(0, 0, -10));

            Console.WriteLine("Light: " + light.ToString());

            Color result = sphere.Lighting(m, light, position, eyeV, normalV);
            Color answer = new Color(1.9f, 1.9f, 1.9f);

            Console.WriteLine();

            Console.WriteLine("Result: " + result.ToString());
            Console.WriteLine("Answer: " + answer.ToString());



        }
    }
}
