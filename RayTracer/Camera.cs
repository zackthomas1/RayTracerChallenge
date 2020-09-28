using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Camera
    {
        //Instance Variables
        int hsize; // Image horizontal size
        int vsize; // Image vertical size
        float fieldOfView;
        Matrix4 transform = new Matrix4();
        float pSize; // pixelSize
        float halfWidth ; // 
        float halfHeight; // 

        // Get/Set methods
        public int Hsize
        {
            get { return hsize; }
            set { hsize = value; }
        }
        public int Vsize
        {
            get { return vsize; }
            set { vsize = value; }
        }
        public float FieldOfView
        {
            get { return fieldOfView; }
            set { fieldOfView = value; }
        }

        public Matrix4 Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        public float PSize
        {
            get { return pSize; }
            set { pSize = value; }
        }


        // Constructors
        public Camera()
        {
            hsize = 640;
            vsize = 480;
            fieldOfView = 35.0f;
            pSize = PixelSize(hsize, vsize);
        }
        public Camera(int hsize, int vsize, float fieldOfView)
        {
            this.hsize = hsize;
            this.vsize = vsize;
            this.fieldOfView = fieldOfView;
            pSize = PixelSize(hsize, vsize);
        }
        public Camera(Matrix4 transform, int hsize = 640, int vsize = 480, float fieldOfView = 35.0f)
        {
            this.transform = transform;

            this.hsize = hsize;
            this.vsize = vsize;
            this.fieldOfView = fieldOfView;
            pSize = PixelSize(hsize, vsize);
        }


        // Methods
        /// <summary>
        /// Returns Matrix4 that reprents the transformation of a camera through space.
        /// This method appears to move the camera realitive the the world, 
        /// but actually transforms world realitive to the camera
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="up"></param>
        /// <returns></returns>
        public Matrix4 ViewTransform(Point from, Point to, Vector3 up)
        {
            Vector3 foward = (to - from).Normalize();
            
            Vector3 upNormalized = up.Normalized();

            Vector3 left = Vector3.Cross(foward, upNormalized);

            Vector3 trueUp = Vector3.Cross(left, foward);

            //Console.WriteLine("left:" + left.ToString());
            //Console.WriteLine("true up:" + trueUp.ToString());
            //Console.WriteLine("foward:" + foward.ToString());

            Matrix4 orientationTransformation = new Matrix4(left.x, left.y, left.z, 0,
                                                            trueUp.x, trueUp.y, trueUp.z, 0,
                                                           -foward.x, -foward.y, -foward.z, 0,
                                                            0, 0, 0, 1);
            Matrix4 orentation = orientationTransformation * Matrix4.TranslateMatrix(-from.x, -from.y, -from.z);
            return orentation;
        }

        /// <summary>
        /// Finds the pixel size the image plane(canvas)
        /// </summary>
        /// <param name="hsize"></param>
        /// <param name="vsize"></param>
        /// <returns></returns>
        public float PixelSize(int hsize, int vsize)
        {
            float halfView = (float)Math.Tan(fieldOfView / 2);
            float aspect = (float)hsize / (float)vsize;

            float halfHeight;
            float halfWidth;

            if(aspect >= 1) // if hsize is greater than vsize
            {
                halfWidth = halfView;
                halfHeight = halfView / aspect;
            }
            else // if vsize is greater than hsize
            {
                halfWidth = (float)halfView * (float)aspect;
                halfHeight = halfView;
            }

            this.halfHeight = halfHeight;
            this.halfWidth = halfWidth;
            float pixelSize = (halfWidth * 2) / hsize;
            return pixelSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        public Ray RayForPixel(int pX, int pY)
        {
            // The offset from the edge of the canvas to the pixel's center 
            float xOffset = (pX + 0.5f) * pSize;
            float yOffset = (pY + 0.5f) * pSize;

            // The untransformed coordinates of the pixel in world space. 
            // (remember that the camera looks toward -z, so +x is to the *left*
            float sceneX = halfWidth - xOffset;
            float sceneY = halfHeight - yOffset;

            // Using the camera matrix, transform the canvas point and the origin, 
            // and then compute the ray's direction vector. 
            // (remember that the canvas is at  z = -1) 
            Point pixel = transform.Invert() * new Point(sceneX, sceneY, -1);
            Point origin = transform.Invert() * new Point(0, 0, 0);
            Vector3 direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        public Canvas Render(Scene scene)
        {
            Canvas image = new Canvas(hsize, vsize);
            Camera cam = this;

            for (int y = 0; y < vsize; y++)
            {
                for (int x = 0; x < hsize; x++)
                {
                    Ray ray = cam.RayForPixel(x, y);
                    Color color = scene.ColorAt(ray);

                    Console.WriteLine("Color: " + color.ToString());
                    image.SetPixelColor(x, y, color);
                }
            }
            return image;
        }




    }
}
