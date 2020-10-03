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
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            StripedPattern pattern = new StripedPattern(white, black);

            Assert.True(white == pattern.p1);
            Assert.True(black == pattern.p2);
        }

        [Fact]
        public void StripePatternConstantY()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            StripedPattern pattern = new StripedPattern(white, black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0, 1, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0, 2, 0)));

        }

        [Fact]
        public void StripePatternConstantZ()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            StripedPattern pattern = new StripedPattern(white, black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 1)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 2)));
        }

        [Fact]
        public void StripePatternAlternatesX()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            StripedPattern pattern = new StripedPattern(white, black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0.9f, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(1, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(-0.1f, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(-1, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(-1.1f, 0, 0)));
        }

        [Fact]
        public void LightingWithPattern()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            Sphere s = new Sphere();

            StripedPattern pattern = new StripedPattern(white, black);
            Material material = new Material(pattern: pattern, ambient: 1.0f, diffuse: 0.0f, specular: 0.0f);

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
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            Sphere s = new Sphere();
            s.Transform = Matrix4.ScaleMatrix(2, 2, 2);

            StripedPattern pattern = new StripedPattern(white, black);

            Color result = pattern.PatternAtObject(s, new Point(1.5f, 0, 0));

            Assert.True(Color.White == result);

        }

        [Fact]
        public void StripesPatternTransform()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            Sphere s = new Sphere();

            StripedPattern pattern = new StripedPattern(white, black);
            pattern.Transform = Matrix4.ScaleMatrix(2, 2, 2);

            Color result = pattern.PatternAtObject(s, new Point(1.5f, 0, 0));

            Assert.True(Color.White == result);
        }

        [Fact]
        public void StripesObjectPatternTransform()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            Sphere s = new Sphere();
            s.Transform = Matrix4.ScaleMatrix(2, 2, 2);

            StripedPattern pattern = new StripedPattern(white, black);
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
            GradientPattern pattern = new GradientPattern(SolidPattern.White, SolidPattern.Black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(new Color(0.75f, 0.75f, 0.75f) == pattern.PatternAt(new Point(0.25f, 0, 0)));
            Assert.True(new Color(0.5f, 0.5f, 0.5f) == pattern.PatternAt(new Point(0.5f, 0, 0)));
            Assert.True(new Color(0.25f, 0.25f, 0.25f) == pattern.PatternAt(new Point(0.75f, 0, 0)));
        }

        [Fact] 
        public void RingPattern()
        {
            RingPattern pattern = new RingPattern(SolidPattern.White, SolidPattern.Black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(1, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(0, 0, 1)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(0.708f, 0.708f, 0.708f)));

        }

        [Fact]
        public void CheckersRepeatX()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            CheckerPattern pattern = new CheckerPattern(white, black); 

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(.99f, 0, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(1.01f, 0, 0)));
        }

        [Fact]
        public void CheckersRepeatY()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            CheckerPattern pattern = new CheckerPattern(white, black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0, .99f, 0)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(0, 1.01f, 0)));
        }

        [Fact]
        public void CheckersRepeatZ()
        {
            Pattern black = new SolidPattern(Color.Black);
            Pattern white = new SolidPattern(Color.White);

            CheckerPattern pattern = new CheckerPattern(white, black);

            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, 0)));
            Assert.True(Color.White == pattern.PatternAt(new Point(0, 0, .99f)));
            Assert.True(Color.Black == pattern.PatternAt(new Point(0, 0, 1.01f)));
        }
    }
}
