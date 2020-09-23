using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class InvalidMateralPropertiesException : Exception
    {

        public InvalidMateralPropertiesException()
        {
            Console.WriteLine("ambient, diffuse, specular must be float values greater than 0" +
                "and shininess must be float value greater than 10."); 
        }

    }
}
