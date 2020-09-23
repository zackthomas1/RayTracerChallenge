using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Save
    {
        /// <summary>
        /// Generates a ppm image file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="canvas"></param>
        public static void PPM(string filePath, Canvas canvas)
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                int maxValue = 255;

                // header that tells image viewer program how to read the file
                // P3
                // width height 
                // maxColorValue
                string header = "P3\n" +
                                canvas.width + " " + canvas.height + "\n" +
                                maxValue.ToString() + "\n";
                writer.WriteLine(header);

                // The body of the ppm file. This contains color data for image
                for (int y = 0; y < canvas.height; y++)
                {

                    string colorGroup = "";

                    for (int x = 0; x < canvas.width; x++)
                    {
                        // Converts each red, green, blue value to a string representation of a valid integer between 0 and 255
                        string red = ((int)(Clamp(canvas.GetPixelColor(x, y).red) * maxValue)).ToString();
                        string green = ((int)(Clamp(canvas.GetPixelColor(x, y).green) * maxValue)).ToString();
                        string blue = ((int)(Clamp(canvas.GetPixelColor(x, y).blue) * maxValue)).ToString();

                        colorGroup = red + " " + green + " " + blue + " ";
                        writer.Write(colorGroup);
                    }
                    writer.WriteLine();
                }

                // Includes a new line at the end of the file becuase some image viewers require it
                string footer = "\n";
                writer.WriteLine(footer);

                writer.Close();
            }

        }

        /// <summary>
        /// Clamps floating point color values to valid range of 0.0f to 1.0f
        /// </summary>
        public static float Clamp(float pixelValue)
        {
            if (pixelValue > 1.0f)
            {
                return pixelValue = 1.0f;
            }
            else if (pixelValue < 0.0f)
            {
                return pixelValue = 0.0f;
            }
            else
            {
                return pixelValue;
            }
        }

    }
}
