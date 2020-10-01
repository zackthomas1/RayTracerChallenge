using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace UnitTestRayTracer
{
    public class Chapter10_Patterns
    {

        [Fact]
        public void CreateStripePattern()
        {
            Color black = Color.Black;
            Color white = Color.White;

            StripedPattern pattern = new StripedPattern(white, black);

            Assert.True(white == pattern.c1);
            Assert.True(black == pattern.c2);
        }

        [Fact]
        public void StripePatternConstantY()
        {
            Color black = Color.Black;
            Color white = Color.White;

            StripedPattern pattern = new StripedPattern(white, black);

            Assert.True(white == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(white == pattern.PatternAt(new Point(0, 1, 0)));
            Assert.True(white == pattern.PatternAt(new Point(0, 2, 0)));

        }

        [Fact]
        public void StripePatternConstantZ()
        {
            Color black = Color.Black;
            Color white = Color.White;

            StripedPattern pattern = new StripedPattern(white, black);

            Assert.True(white == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(white == pattern.PatternAt(new Point(0, 0, 1)));
            Assert.True(white == pattern.PatternAt(new Point(0, 0, 2)));
        }

        [Fact]
        public void StripePatternAlternatesX()
        {
            Color black = Color.Black;
            Color white = Color.White;

            StripedPattern pattern = new StripedPattern(white, black);

            Assert.True(white == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(white == pattern.PatternAt(new Point(0.9f, 0, 0)));
            Assert.True(black == pattern.PatternAt(new Point(1, 0, 0)));
            Assert.True(black == pattern.PatternAt(new Point(-0.1f, 0, 0)));
            Assert.True(black == pattern.PatternAt(new Point(-1, 0, 0)));
            Assert.True(white == pattern.PatternAt(new Point(-1.1f, 0, 0)));
        }

        [Fact]
        public void LightingWithPattern()
        {
            Sphere s = new Sphere();

            StripedPattern pattern = new StripedPattern(Color.White, Color.Black);
            Material material = new Material(pattern, ambient: 1.0f, diffuse: 0.0f, specular: 0.0f);

            Vector3 eyeV = new Vector3(0, 0, -1);
            Vector3 normalV = new Vector3(0, 0, -1);

            Light light = new Light(Color.White, new Point(0, 0, -10));

            Color c1 = material.Lighting(material, s, light, new Point(0.9f, 0, 0), eyeV, normalV, false);
            Color c2 = material.Lighting(material, s, light, new Point(1.9f, 0, 0), eyeV, normalV, false);

            Assert.True(new Color(1, 1, 1) == c1);
            Assert.True(new Color(0, 0, 0) == c2);
        }

        [Fact]
        public void StripesObjectTransform()
        {
            Sphere s = new Sphere();
            s.Transform = Matrix4.ScaleMatrix(2, 2, 2);

            StripedPattern pattern = new StripedPattern(Color.White, Color.Black);

            Color result = pattern.PatternAtObject(s, new Point(1.5f, 0, 0));

            Assert.True(Color.White == result);

        }

        [Fact]
        public void StripesPatternTransform()
        {
            Sphere s = new Sphere();

            StripedPattern pattern = new StripedPattern(Color.White, Color.Black);
            pattern.Transform = Matrix4.ScaleMatrix(2, 2, 2);

            Color result = pattern.PatternAtObject(s, new Point(1.5f, 0, 0));

            Assert.True(Color.White == result);
        }

        [Fact]
        public void StripesObjectPatternTransform()
        {
            Sphere s = new Sphere();
            s.Transform = Matrix4.ScaleMatrix(2, 2, 2);

            StripedPattern pattern = new StripedPattern(Color.White, Color.Black);
            pattern.Transform = Matrix4.TranslateMatrix(0.5f, 0, 0);

            Color result = pattern.PatternAtObject(s, new Point(2.5f, 0, 0));

            Assert.True(Color.White == result);
        }

        [Fact]
        public void DefaultPatternTransform()
        {
            StripedPattern pattern = new StripedPattern();
            Assert.True(pattern.Transform == new Matrix4());
        }

        [Fact]
        public void AssigneTransformation()
        {
            StripedPattern pattern = new StripedPattern();
            pattern.Transform = Matrix4.TranslateMatrix(1, 2, 3);

            Assert.True(pattern.Transform == Matrix4.TranslateMatrix(1, 2, 3));
        }

        [Fact]
        public void PatternObjectTransform()
        {
            Sphere s = new Sphere();
            s.Transform = Matrix4.ScaleMatrix(2, 2, 2);
            TestPattern pattern = new TestPattern();


            Color c = pattern.PatternAtObject(s, new Point(2, 3, 4));
            Color answer = new Color(1, 1.5f, 2);

            Assert.True(c == answer);

        }

        [Fact]
        public void PatternPatternTransform()
        {
            Sphere s = new Sphere();
            TestPattern pattern = new TestPattern();
            pattern.Transform = Matrix4.ScaleMatrix(2, 2, 2);


            Color c = pattern.PatternAtObject(s, new Point(2, 3, 4));
            Color answer = new Color(1, 1.5f, 2);

            Assert.True(c == answer);
        }

        [Fact]
        public void PatternObjectPatternTransform()
        {
            Sphere s = new Sphere();
            s.Transform = Matrix4.ScaleMatrix(2, 2, 2);
            TestPattern pattern = new TestPattern();
            pattern.Transform = Matrix4.TranslateMatrix(0.5f, 1, 1.5f);

            Color c = pattern.PatternAtObject(s, new Point(2.5f, 3, 3.5f));
            Color answer = new Color(0.75f, 0.5f, 0.25f);

            Assert.True(c == answer);
        }

        [Fact]
        public void Gradient()
        {
            GradientPattern pattern = new GradientPattern(Color.White, Color.Black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(new Color(0.75f, 0.75f, 0.75f) == pattern.PatternAt(new Point(0.25f, 0, 0)));
            Assert.True(new Color(0.5f, 0.5f, 0.5f) == pattern.PatternAt(new Point(0.5f, 0, 0)));
            Assert.True(new Color(0.25f, 0.25f, 0.25f) == pattern.PatternAt(new Point(0.75f, 0, 0)));
        }

        [Fact] 
        public void RingPattern()
        {
            RingPattern pattern = new RingPattern(Color.White, Color.Black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(1, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(0, 0, 1)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(0.708f, 0.708f, 0.708f)));

        }

        [Fact]
        public void CheckersRepeatX()
        {
            CheckerPattern pattern = new CheckerPattern(Color.White, Color.Black); 

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(.99f, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(1.01f, 0, 0)));
        }

        [Fact]
        public void CheckersRepeatY()
        {
            CheckerPattern pattern = new CheckerPattern(Color.White, Color.Black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0, .99f, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(0, 1.01f, 0)));
        }

        [Fact]
        public void CheckersRepeatZ()
        {
            CheckerPattern pattern = new CheckerPattern(Color.White, Color.Black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, .99f)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(0, 0, 1.01f)));
        }
    }
}
