using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Color
    {
        // Instance Variables
        public float red;
        public float green;
        public float blue;

        // Get/Set methods

        // readonly variables for standard default color values
        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color White = new Color(1, 1, 1);
        public static readonly Color Red = new Color(1, 0, 0);
        public static readonly Color Green = new Color(0, 1, 0);
        public static readonly Color Blue = new Color(0, 0, 1);

        // Constructors
        public Color(float red = 0.0f, float green = 0.0f, float blue = 0.0f)
        {
            this.red = red;
            this.green = green; 
            this.blue = blue;
        }

        // Class overloads
        public override string ToString()
        {
            return $"({red},{green},{blue})";
        }

        public override bool Equals(object obj)
        {
            return obj is Color color &&
                   red == color.red &&
                   green == color.green &&
                   blue == color.blue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(red, green, blue);
        }

        public static Color operator +(Color c1, Color c2)
        {
            Color returnColor = new Color();

            returnColor.red = c1.red + c2.red;
            returnColor.green = c1.green + c2.green;
            returnColor.blue = c1.blue + c2.blue;

            return returnColor;
        }

        public static Color operator -(Color c1, Color c2)
        {
            Color returnColor = new Color();

            returnColor.red = c1.red - c2.red;
            returnColor.green = c1.green - c2.green;
            returnColor.blue = c1.blue - c2.blue;

            return returnColor;
        }

        public static Color operator *(Color c1, float scalar)
        {
            Color returnColor = new Color();

            returnColor.red = c1.red * scalar;
            returnColor.green = c1.green * scalar;
            returnColor.blue = c1.blue * scalar;

            return returnColor;
        }

        public static Color operator *(float scalar, Color c1)
        {
            Color returnColor = new Color();

            returnColor.red = c1.red * scalar;
            returnColor.green = c1.green * scalar;
            returnColor.blue = c1.blue * scalar;

            return returnColor;
        }

        public static Color operator *(Color c1, Color c2)
        {
            Color returnColor = new Color();

            returnColor.red = c1.red * c2.red;
            returnColor.green = c1.green * c2.green;
            returnColor.blue = c1.blue * c2.blue;

            return returnColor;
        }

        public static bool operator ==(Color c1, Color c2)
        {
            if (Utilities.FloatEquality(c1.red, c2.red) &&
                Utilities.FloatEquality(c1.green, c2.green) &&
                Utilities.FloatEquality(c1.blue, c2.blue))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool operator !=(Color c1, Color c2)
        {
            if (Utilities.FloatEquality(c1.red, c2.red) &&
                Utilities.FloatEquality(c1.green, c2.green) &&
                Utilities.FloatEquality(c1.blue, c2.blue))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Methods
        /// <summary>
        /// Sets color values to input parameters (red, green, blue)
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public static Color SetColor(float red, float green, float blue)
        {
            Color returnColor = new Color();

            returnColor.red = red;
            returnColor.green = green;
            returnColor.blue = blue;

            return returnColor;
        }

    }
}
