using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace UnitTestRayTracer
{
    public class Chapter07_MakingAScene
    {

        [Fact]
        public void SetUpSceneClass()
        {
            Scene sceneDefault = new Scene();

            Material m1 = new Material(new Color(0.8f, 1.0f, 0.6f), diffuse: 0.7f, specular: 0.2f);
            Sphere s1 = new Sphere(m1);
            Sphere s2 = new Sphere(radius: 0.5f);

            Assert.Single(sceneDefault.Lights);
            Assert.Equal(2, sceneDefault.Objects.Count);

            Assert.True(s1 == sceneDefault.Objects[0]);
            Assert.True(s2 == sceneDefault.Objects[1]);
        }

        [Fact]
        public void IntersectWorldwithRay()
        {
            Scene scene = new Scene();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));

            List<Intersection> xs = Intersection.Sort(scene.Intersections(ray));

            Assert.Equal(4, xs.Count);
            Assert.Equal(4, xs[0].t);
            Assert.Equal(4.5f, xs[1].t);
            Assert.Equal(5.5f, xs[2].t);
            Assert.Equal(6, xs[3].t);
        }

        [Fact]
        public void PrecomputeIntersectionStates()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            Sphere s = new Sphere();
            Intersection i = new Intersection(4, s);

            Computation comp = Intersection.PrepareComputations(i, r);

            Assert.Equal(i.t, comp.t);
            Assert.Equal(i.rayObject, comp.rayObject);
            Assert.Equal(new Point(0, 0, -1), comp.point);
            Assert.Equal(new Vector3(0, 0, -1), comp.eyeV);
            Assert.Equal(new Vector3(0, 0, -1), comp.normalV);
        }

        [Fact]
        public void HitOutside()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            Sphere s = new Sphere();
            Intersection i = new Intersection(4, s);
            Computation comp = new Computation(i, r);

            Assert.False(comp.inside);
        }

        [Fact]
        public void HitIntside()
        {
            Ray r = new Ray(new Point(0, 0, 0), new Vector3(0, 0, 1));
            Sphere s = new Sphere();
            Intersection i = new Intersection(1, s);
            Computation comp = new Computation(i, r);

            Assert.True(comp.inside);
            Assert.Equal(new Point(0, 0, 1), comp.point);
            Assert.Equal(new Vector3(0, 0, -1), comp.eyeV);
            Assert.Equal(new Vector3(0, 0, -1), comp.normalV);
        }

        [Fact]
        public void ShadeIntersection()
        {
            Scene scene = new Scene();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));
            RayObject shape = scene.Objects[0];
            Intersection i = new Intersection(4, shape);

            Computation comp = new Computation(i, ray);

            Color color = scene.ShadeHit(comp);
            Color answer = new Color(0.38066f, 0.47583f, 0.2855f);

            Assert.True(answer == color);
        }

        [Fact]
        public void ShadeIntersectionInside()
        {
            Scene scene = new Scene();
            scene.Lights[0] = new Light(new Color(1, 1, 1), new Point(0, 0.25f, 0));
            Ray ray = new Ray(new Point(0, 0, 0), new Vector3(0, 0, 1));
            RayObject shape = scene.Objects[1];
            Intersection i = new Intersection(0.5f, shape);

            Computation comp = new Computation(i, ray);

            Color color = scene.ShadeHit(comp);
            Color answer = new Color(0.90498f, 0.90498f, 0.90498f);

            Assert.True(answer == color);
        }

        [Fact]
        public void RayMissColor()
        {
            Scene scene = new Scene();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 1, 0));

            Color color = scene.ColorAt(ray);
            Color answer = new Color(0, 0, 0);

            Assert.True(answer == color);
        }

        [Fact]
        public void RayHitColor()
        {
            Scene scene = new Scene();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector3(0, 0, 1));

            Color color = scene.ColorAt(ray);
            Color answer = new Color(0.38066f, 0.47583f, 0.2855f);

            Assert.True(answer == color);
        }

        [Fact]
        public void BehindRayColor()
        {
            Scene scene = new Scene();

            Material mat = new Material(Color.White, ambient: 1.0f);

            RayObject outer = scene.Objects[0];
            RayObject inner = scene.Objects[1];
            outer.material = mat;
            inner.material = mat;

            Ray r = new Ray(new Point(0, 0, 0.75f), new Vector3(0, 0, -1f));

            Color color = scene.ColorAt(r);

            Assert.True(inner.material.mColor == color);
        }

        [Fact]
        public void DefaultOrientation()
        {
            Point from = new Point(0, 0, 0);
            Point to = new Point(0, 0, -1);
            Vector3 up = new Vector3(0, 1, 0);
            Camera cam = new Camera();
            Matrix4 t = cam.ViewTransform(from, to, up);
            Matrix4 identity = new Matrix4();

            Assert.True(identity == t);
        }

        [Fact]
        public void PositiveDirection()
        {
            Point from = new Point(0, 0, 0);
            Point to = new Point(0, 0, 1);
            Vector3 up = new Vector3(0, 1, 0);

            Camera cam = new Camera();

            Matrix4 trans = cam.ViewTransform(from, to, up);
            Matrix4 answer = Matrix4.ScaleMatrix(-1, 1, -1);

            Assert.True(answer == trans);
        }

        [Fact]
        public void ViewTransformMovesWorld()
        {
            Point from = new Point(0, 0, 8);
            Point to = new Point(0, 0, 0);
            Vector3 up = new Vector3(0, 1, 0);

            Camera cam = new Camera();

            Matrix4 trans = cam.ViewTransform(from, to, up);
            Matrix4 answer = Matrix4.TranslateMatrix(0, 0, -8);

            Assert.True(answer == trans);
        }

        // Not sure about this test got close but not exact
        [Fact]
        public void ArbitraryViewTransform()
        {
            Point from = new Point(1, 3, 2);
            Point to = new Point(4, -2, 8);
            Vector3 up = new Vector3(1, 1, 0);

            Camera cam = new Camera();

            Matrix4 trans = cam.ViewTransform(from, to, up);
            Matrix4 answer = new Matrix4(-0.50709f, 0.50709f, 0.67612f, -2.36643f,
                                         0.76772f, 0.60609f, 0.12122f, -2.82843f,
                                         -0.35857f, 0.59761f, -0.71714f, 0.00000f,
                                         0.00000f, 0.00000f, 0.00000f, 1.00000f);

            Assert.True(answer == trans);
        }

        [Fact]
        public void ConstructCamera()
        {
            int hsize = 160;
            int vsize = 120;
            float fieldOfView = (float)(Math.PI / 2);

            Camera camera = new Camera(hsize, vsize, fieldOfView);

            Assert.Equal(160, camera.Hsize);
            Assert.Equal(120, camera.Vsize);
            Assert.Equal((float)Math.PI / 2, camera.FieldOfView);
            Assert.True(new Matrix4() == camera.Transform);

        }

        [Fact]
        public void PixelSizeHorizontalCanvas()
        {
            Camera cam = new Camera(200, 125, (float)Math.PI / 2);
            Assert.Equal(0.01f, cam.PSize);
        }

        [Fact]
        public void PixelSizeVerticalCanvas()
        {
            Camera cam = new Camera(125, 200, (float)Math.PI / 2);
            Assert.Equal(0.01f, cam.PSize);
        }

        [Fact]
        public void RayCenterCanvas()
        {
            Camera cam = new Camera(201, 101, (float)Math.PI / 2);
            Ray r = cam.RayForPixel(100, 50);
            
            Assert.Equal(new Point(0, 0, 0), r.origin);
            Assert.True(new Vector3(0, 0, -1) == r.direction); 
        }

        [Fact]
        public void RayCornerCanvas()
        {
            Camera cam = new Camera(201, 101, (float)Math.PI / 2);
            Ray r = cam.RayForPixel(0, 0);

            Assert.Equal(new Point(0, 0, 0), r.origin);
            Assert.True(new Vector3(0.66519f, 0.33259f, -0.66851f) == r.direction);
        }

        [Fact]
        public void RayCamTransform()
        {
            Camera cam = new Camera(201, 101, (float)Math.PI / 2);
            cam.Transform = Matrix4.RotateMatrix_Y((float)Math.PI / 4) * Matrix4.TranslateMatrix(0, -2, 5); 

            Ray r = cam.RayForPixel(100, 50);

            Assert.True(new Point(0, 2, -5) == r.origin);
            Assert.True(new Vector3((float)(Math.Sqrt(2)/2), 0, -(float)(Math.Sqrt(2) / 2)) == r.direction);
        }

        [Fact]
        public void RenderWorldCamera()
        {
            Scene scene = new Scene();
            Camera cam = new Camera(11, 11, (float)Math.PI / 2);
            Point from = new Point(0, 0, -5);
            Point to = new Point(0, 0, 0);
            Vector3 up = new Vector3(0, 1, 0);
            cam.Transform = cam.ViewTransform(from, to, up);
            Canvas image = cam.Render(scene);

            Assert.True(image.GetPixelColor(5, 5) == new Color(0.38066f, 0.47583f, 0.2855f)); 
        }




    }
}
