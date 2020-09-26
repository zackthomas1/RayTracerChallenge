using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Canvas
    {
        // Instance Variables
        public int width { get; set; }
        public int height { get; set; }
        public Color[,] canvas { get; set; }

        // Get/Set methods

        // Constructors
        public Canvas(int width = 640 ,int height = 480)
        {
            this.width = width;
            this.height = height;
            CreateCanvas(Color.Black);
        }
        public Canvas(Color color, int width = 640, int height = 480)
        {
            this.width = width;
            this.height = height;
            CreateCanvas(color);
        }

        // Class overloads

        // Methods
        /// <summary>
        /// Colors the Canvas a single color
        /// </summary>
        /// <param name="color"></param>
        public void CreateCanvas(Color color)
        {
            canvas = new Color[width, height]; 
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    canvas[x, y] = color;
                }
            }
        }

        public int GetWidth()
        {
            return width; 
        }

        public int GetHeight()
        {
            return height; 
        }

        public void SetWidth(int width)
        {
            this.width = width;
        }

        public void SetHeight(int height)
        {
            this.height = height;
        }

        /// <summary>
        /// Takes a Color values and sets every pixel on the canvas to that value
        /// </summary>
        /// <param name="c1"></param>
        public void FillCanvas(Color c1)
        {
            for (int x = 0; x < canvas.GetLength(0); x++)
            {
                for (int y = 0; y < canvas.GetLength(1); y++)
                {
                    canvas[x, y] = c1;
                }
            }
        }

        /// <summary>
        /// Takes (x,y) coordinates representing pixel position on canvas and a color 
        /// then sets that pixel value to the color
        /// </summary>
        /// <param name="pixelCordinate"></param>
        /// <param name="c1"></param>
        public void SetPixelColor(int x, int y, Color c1)
        {
            canvas[x, y] = c1; 
        }

        /// <summary>
        /// (x,y) coordinates representing pixel position on canvas
        /// then returns that pixels color values
        /// </summary>
        /// <param name="pixelCordinate"></param>
        /// <returns></returns>
        public Color GetPixelColor(int x, int y)
        {
            return canvas[x, y];
        }

    }
}
