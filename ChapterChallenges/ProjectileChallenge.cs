using System;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using RayTracer.RayObjects;

namespace ChapterChallenges
{
    public static class ProjectileChallenge
    {
        static Point projectilePosition = new Point(3, 3, 0);
        static Vector3 projectileVelocity = new Vector3(1f, 1.8f, 0f);

        static Vector3 gravityVector = new Vector3(0, -.3f, 0);
        static Vector3 windVector = new Vector3(-.01f, 0f, 0);

        /// <summary>
        /// Draws a cross on the canvas at a given position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="canvas"></param>
        /// <param name="color"></param>
        static void drawCross(int x, int y, Canvas canvas, Color color)
        {
            //int squareWidth = 5;

            //canvas.SetPixelColor(x + 2, y, color);
            canvas.SetPixelColor(x + 1, y, color);
            canvas.SetPixelColor(x, y, color);
            canvas.SetPixelColor(x - 1, y, color);
            //canvas.SetPixelColor(x - 2, y, color);

            //canvas.SetPixelColor(x, y + 2, color);
            canvas.SetPixelColor(x, y + 1, color);
            canvas.SetPixelColor(x, y, color);
            canvas.SetPixelColor(x, y - 1, color);
            //canvas.SetPixelColor(x, y - 2, color);
        }


        /// <summary>
        /// Updates the projectile position and velocity
        /// </summary>
        /// <returns></returns>
        static Point UpdateTick()
        {
            projectileVelocity = projectileVelocity.Normalized() * 11.25f;

            projectilePosition = projectilePosition + projectileVelocity;
            projectileVelocity = projectileVelocity + gravityVector + windVector;

            Console.WriteLine($"Updated Position Vector: {projectilePosition.ToString()}");
            Console.WriteLine($"Updated Velocity Vector: {projectileVelocity.ToString()}");

            return projectilePosition;
        }

        static void Projectile()
        {
            Canvas canvas = new Canvas(900, 550);


            while (projectilePosition.y > 0 && projectilePosition.x < canvas.width)
            {
                Console.WriteLine($"x:{(int)projectilePosition.x} y:{(int)projectilePosition.y}");

                // Checks if projectile is within a valid range to be drawn
                if ((int)projectilePosition.y - 2 > 0 && (int)projectilePosition.y + 2 < canvas.height && (int)projectilePosition.x - 2 > 0 && (int)projectilePosition.x + 2 < canvas.width)
                {
                    drawCross((int)projectilePosition.x, canvas.height - (int)projectilePosition.y, canvas, Color.Red);
                    //canvas.SetPixelColor((int)projectilePosition.x, canvas.height - (int)projectilePosition.y, Color.Red());
                    Console.WriteLine("Cross position Drawn");
                }
                Console.WriteLine();
                projectilePosition = UpdateTick(); // update projectile velocity and position
            }

            // End State Message
            if (projectilePosition.y < 0)
                Console.WriteLine("\nY-position less than zero.");
            else if (projectilePosition.x > canvas.width)
                Console.WriteLine("\nX-position out of bounds.");

            // Save Canvas to ppm
            Console.WriteLine("\nSaving PPM file");
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders";
            string fileName = "projectileChallenge02";
            string fileDirectoryComplete = filePath + "\\" + fileName + ".ppm";
            Save.PPM(fileDirectoryComplete, canvas);

            Console.WriteLine("Done: Program complete.");
            Console.ReadKey();
        }

    }
}
