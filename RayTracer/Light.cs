using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Light
    {
        protected static int currentID = 0;
        protected int id;

        Color intensity;
        Point position;

        public Color Insensity
        {
            get { return intensity; }
            set { intensity = value; }
        }

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Get/set RayObject ID.
        /// </summary>
        public int ID
        {
            get { return id; }
            private set { id = value; }
        }



        public Light()
        {
            id = currentID++; 

            // sets the default to white light at the origin
            intensity = new Color(1, 1, 1); 
            position = new Point(0, 0, 0); 
        }

        public Light(Color intensity, Point position)
        {
            id = currentID++;

            this.intensity = intensity;
            this.position = position; 
        }

        public override string ToString()
        {
            return "Light " + id.ToString() + ": "+ "\n" +
                    "    " + "Insensity -> " + Insensity.ToString() + "\n" +
                    "    " + "Position -> " + Position.ToString() + "\n";
        }


    }
}
