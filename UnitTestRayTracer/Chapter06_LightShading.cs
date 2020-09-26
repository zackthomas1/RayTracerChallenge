using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace UnitTestRayTracer
{
    public class Chapter06_LightShading
    {

        [Fact]
        public void NormalSphereXAxis()
        {
            Sphere s = new Sphere();

            Vector3 normal = s.GetNormal(new Point(1, 0, 0));
            Vector3 answer = new Vector3(1, 0, 0);

            Assert.True(answer == normal);
        }

        [Fact]
        public void NormalSphereYAxis()
        {
            Sphere s = new Sphere();

            Vector3 normal = s.GetNormal(new Point(0, 1, 0));
            Vector3 answer = new Vector3(0, 1, 0);

            Assert.True(answer == normal);
        }

        [Fact]
        public void NormalSphereZAxis()
        {
            Sphere s = new Sphere();

            Vector3 normal = s.GetNormal(new Point(0, 0, 1));
            Vector3 answer = new Vector3(0, 0, 1);

            Assert.True(answer == normal);
        }

        [Fact]
        public void NormalSphereNonAxial()
        {
            Sphere s = new Sphere();
            float sqrtThreeDivdeThree = (float)(Math.Sqrt(3) / 3);

            Vector3 normal = s.GetNormal(new Point(sqrtThreeDivdeThree, sqrtThreeDivdeThree, sqrtThreeDivdeThree));
            Vector3 answer = new Vector3(sqrtThreeDivdeThree, sqrtThreeDivdeThree, sqrtThreeDivdeThree);

            Assert.True(answer == normal);
        }

        [Fact]
        public void NormalTranlatedSphere()
        {
            Sphere s = new Sphere();
            s.TransformMatrix = s.TransformMatrix.Translate(0, 1, 0);

            Vector3 normal = s.GetNormal(new Point(0, 1.70711f, -0.70711f));
            Vector3 answer = new Vector3(0, 0.70711f, -0.70711f);

            Assert.True(answer == normal);

        }

        [Fact]
        public void NormalTransformedSphere()
        {
            Sphere s = new Sphere();
            s.TransformMatrix = s.TransformMatrix.Scale(1, 0.5f, 1).Rotate_Z(Math.PI / 5);

            Vector3 normal = s.GetNormal(new Point(0, (float)Math.Sqrt(2) / 2, (float)-(Math.Sqrt(2) / 2)));
            Vector3 answer = new Vector3(0, 0.97014f, -0.24254f);

            Assert.True(answer == normal);
        }

        [Fact]
        public void ReflectVector45()
        {
            Vector3 v1 = new Vector3(1, -1, 0);
            Vector3 normal = new Vector3(0, 1, 0);

            Vector3 reflection = Vector3.Reflection(v1, normal);
            Vector3 answer = new Vector3(1, 1, 0);

            Assert.True(answer == reflection);
        }

        [Fact]
        public void ReflectVectorSlatedSurface()
        {
            Vector3 v1 = new Vector3(0, -1, 0);
            Vector3 normal = new Vector3((float)(Math.Sqrt(2) / 2), (float)(Math.Sqrt(2) / 2), 0);

            Vector3 reflection = Vector3.Reflection(v1, normal);
            Vector3 answer = new Vector3(1, 0, 0);

            Assert.True(answer == reflection);
        }

        [Fact]
        public void DefaultMaterial()
        {
            Material m = new Material();
            Assert.True(Color.White == m.mColor);
            Assert.Equal(0.1f, m.Ambient);
            Assert.Equal(0.9f, m.Diffuse);
            Assert.Equal(0.9f, m.Specular);
            Assert.Equal(200.0f, m.Shininess);
        }

        [Fact]
        public void SphereWithDefaultMaterial()
        {
            Sphere s = new Sphere();
            Material m = s.material;

            Material answer = new Material();
            Material wrong = new Material(Color.Red);

            Assert.True(answer == m);
            Assert.False(wrong == m);
        }

        [Fact]
        public void SphereWithCostumeMaterial()
        {
            Sphere s = new Sphere(new Material(Color.Red));
            Material m = s.material;
            m.Ambient = 1.0f;

            Material answer = new Material(color: Color.Red, ambient: 1.0f);
            Material wrong = new Material();

            Assert.True(answer == m);
            Assert.False(wrong == m);
        }

        [Fact]
        public void LightingEyeBetweenLightandSurface()
        {
            Sphere sphere = new Sphere(); 

            Material m = new Material();
            Point position = new Point(0, 0, 0);

            Vector3 eyeV = new Vector3(0, 0, -1);
            Vector3 normalV = new Vector3(0, 0, -1);
            Light light = new Light(Color.White, new Point(0, 0, -10));

            Color result = sphere.material.Lighting(m, light, position, eyeV, normalV);
            Color answer = new Color(1.9f, 1.9f, 1.9f);

            Assert.Equal(answer, result); 
        }

        [Fact]
        public void LightingEyeOffset45()
        {
            Sphere sphere = new Sphere();

            Material m = new Material();
            Point position = new Point(0, 0, 0);

            Vector3 eyeV = new Vector3(0, (float)(Math.Sqrt(2)/2), (float)(Math.Sqrt(2) / 2));
            Vector3 normalV = new Vector3(0, 0, -1);
            Light light = new Light(Color.White, new Point(0, 0, -10));

            Color result = sphere.material.Lighting(m, light, position, eyeV, normalV);
            Color answer = new Color(1.0f, 1.0f, 1.0f);

            Assert.Equal(answer, result);
        }

        [Fact]
        public void LightingEyeOppSurface()
        {
            Sphere sphere = new Sphere();

            Material m = new Material();
            Point position = new Point(0, 0, 0);

            Vector3 eyeV = new Vector3(0, 0, -1);
            Vector3 normalV = new Vector3(0, 0, -1);
            Light light = new Light(Color.White, new Point(0, 10, -10));

            Color result = sphere.material.Lighting(m, light, position, eyeV, normalV);
            Color answer = new Color(0.7364f, 0.7364f, 0.7364f);

            Assert.True(answer == result);
        }

        [Fact]
        public void LightingEyeReflectionPath()
        {
            Sphere sphere = new Sphere();

            Material m = new Material();
            Point position = new Point(0, 0, 0);

            Vector3 eyeV = new Vector3(0, -(float)(Math.Sqrt(2) / 2), -(float)(Math.Sqrt(2) / 2));
            Vector3 normalV = new Vector3(0, 0, -1);
            Light light = new Light(Color.White, new Point(0, 10, -10));

            Color result = sphere.material.Lighting(m, light, position, eyeV, normalV);
            Color answer = new Color(1.6364f, 1.6364f, 1.6364f);

            Assert.True(answer == result);
        }

        [Fact]
        public void LightingLightBehindSurface()
        {
            Sphere sphere = new Sphere();

            Material m = new Material();
            Point position = new Point(0, 0, 0);

            Vector3 eyeV = new Vector3(0, 0, -1);
            Vector3 normalV = new Vector3(0, 0, -1);
            Light light = new Light(Color.White, new Point(0, 0, 10));

            Color result = sphere.material.Lighting(m, light, position, eyeV, normalV);
            Color answer = new Color(0.1f, 0.1f, 0.1f);

            Assert.True(answer == result);
        }

    }
}
