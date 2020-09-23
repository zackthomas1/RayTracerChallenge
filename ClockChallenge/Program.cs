using System;
using RayTracer; 


namespace ClockChallenge
{
    class Program
    {
        static void DrawSquare(int x, int y, Canvas canvas)
        {
            for (int row = x - 20; row < x + 20; row++)
            {
                for (int column = y - 20; column < y + 20; column++)
                {
                    canvas.SetPixelColor(row, column, Color.Blue()); 
                }
            }

        }
        static void Main(string[] args)
        {
            Canvas canvas = new Canvas(900,900);
            int centerWidth = canvas.width / 2;
            int centerHeight = canvas.height / 2;

            float positionMult = canvas.width * (3.0f / 8.0f);
            Console.WriteLine(positionMult);

            Point TwelvePosition = new Point(0, 1, 0); // Plan to rotate around the x axis 


            DrawSquare(centerWidth,centerHeight, canvas); // Draw center

            for (int iter = 0;  iter < 12; iter++)
            {
                Point positionUpdate = TwelvePosition * Matrix4.RotateMatrix_X(iter * Math.PI / 6);
                Console.WriteLine(positionUpdate.ToString());
                DrawSquare(centerWidth + (int)(positionUpdate.z * positionMult), centerHeight - (int)(positionUpdate.y * positionMult), canvas); // Draw twelve position
            }

            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "ClockChallenge";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, canvas);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();
        }
    }
}
