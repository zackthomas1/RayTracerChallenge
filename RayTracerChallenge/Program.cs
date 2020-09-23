using System;
using System.IO;

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

            Matrix4 m2 = new Matrix4(8, -5, 9, 2,
                                     7, 5, 6, 1,
                                    -6, 0, 9, 6,
                                    -3, 0, -9, -4);

            Matrix4 result02 = m2.Invert();
            Matrix4 answer02 = new Matrix4(-0.15385f, -0.15385f, -0.28205f, -0.53846f,
                                           -0.07682f, 0.12308f, 0.02564f, 0.03077f,
                                            0.35897f, 0.35897f, 0.43590f, 0.92308f,
                                           -0.69231f, -0.69231f, -0.76923f, -1.92308f);

            Console.WriteLine(m2.Determinate());
            Console.WriteLine("result:\n" + result02.ToString());
            Console.WriteLine("answer:\n" + answer02.ToString());
        }
    }
}
