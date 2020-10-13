using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace UnitTestRayTracer
{
    public class Chapter02_DrawingOnCanvas
    {
        [Fact]
        public void ColorClass()
        {
            Color c1 = new Color(-0.5f, 0.4f, 1.7f);

            Assert.Equal(-0.5f, c1.red);
            Assert.Equal(0.4f, c1.green);
            Assert.Equal(1.7f, c1.blue);
        }

        [Fact]
        public void ColorToString()
        {
            Color c1 = new Color(-0.5f, 0.4f, 1.7f);

            Assert.Equal($"({c1.red},{c1.green},{c1.blue})", c1.ToString());
        }

        [Fact]
        public void AddColor()
        {
            Color c1 = new Color(-0.5f, 0.4f, 1.7f);
            Color c3 = new Color(0.8f, 1.4f, 2.1f);

            Color additionColor = c1 + c3;
            Color answer = new Color(0.3f, 1.8f, 3.8f);

            Assert.Equal(answer, additionColor);
            Assert.True(answer == additionColor);
        }

        [Fact]
        public void SubstractColor()
        {
            Color c1 = new Color(-0.5f, 0.4f, 1.7f);
            Color c3 = new Color(0.8f, 1.4f, 2.2f);

            Color substractionColor = c1 - c3;
            Color answer = new Color(-1.3f, -1.0f, -0.5f);

            Assert.Equal(answer, substractionColor);
            Assert.True(answer == substractionColor);
        }

        [Fact]
        public void MultipleScalarColor()
        {
            Color c1 = new Color(-0.5f, 0.4f, 1.7f);
            float scalar = 2;

            Color scalarMultipleColor = c1 * scalar;
            Color scalarMultipleColor2 = scalar * c1;
            Color answer = new Color(-1f, 0.8f, 3.4f);

            Assert.Equal(answer, scalarMultipleColor);
            Assert.True(answer == scalarMultipleColor);
            Assert.True(scalarMultipleColor2 == scalarMultipleColor);
        }

        [Fact]
        public void MultiplyColors()
        {
            Color c1 = new Color(-0.5f, 0.4f, 1.7f);
            Color c3 = new Color(0.8f, 1.4f, 3f);

            Color multiplyColor = c1 * c3;
            Color answer = new Color(-0.4f, 0.56f, 5.1f);

            //Assert.Equal(answer, substractionColor);
            Assert.True(answer == multiplyColor);
        }

        [Fact]
        public void EqualityOperatorColor()
        {
            Color c1 = new Color(-0.5f, 0.4f, 1.7f);
            Color c2 = new Color(-0.5f, 0.4f, 1.7f);
            Color c3 = new Color(0.8f, 1.4f, 2.1f);

            Assert.True(c1 == c2);
            Assert.False(c1 == c3);
        }

        [Fact]
        public void InequalityOperatorColor()
        {
            Color c1 = new Color(-0.5f, 0.4f, 1.7f);
            Color c2 = new Color(-0.5f, 0.4f, 1.7f);
            Color c3 = new Color(0.8f, 1.4f, 2.1f);

            Assert.False(c1 != c2);
            Assert.True(c1 != c3);
        }

        [Fact]
        public void SetColor()
        {
            Color c1 = new Color();

            c1 = Color.SetColor(0.5f, 0.8f, 0.35f);

            Color answer = new Color(0.5f, 0.8f, 0.35f);

            Assert.Equal(answer, c1);
            Assert.True(answer == c1);
        }

        [Fact]
        public void SetBlack()
        {
            Color c1 = new Color(0.5f, 0.8f, 0.35f);

            c1 = Color.Black;

            Color answer = new Color(0f, 0f, 0f);

            Assert.Equal(answer, c1);
            Assert.True(answer == c1);
        }

        [Fact]
        public void SetWhite()
        {
            Color c1 = new Color(0.5f, 0.8f, 0.35f);

            c1 = Color.White;

            Color answer = new Color(1f, 1f, 1f);

            Assert.Equal(answer, c1);
            Assert.True(answer == c1);
        }

        [Fact]
        public void SetRed()
        {
            Color c1 = new Color(0.5f, 0.8f, 0.35f);

            c1 = Color.Red;

            Color answer = new Color(1f, 0f, 0f);

            Assert.Equal(answer, c1);
            Assert.True(answer == c1);
        }

        [Fact]
        public void SetGreen()
        {
            Color c1 = new Color(0.5f, 0.8f, 0.35f);

            c1 = Color.Green;

            Color answer = new Color(0f, 1f, 0f);

            Assert.Equal(answer, c1);
            Assert.True(answer == c1);
        }

        [Fact]
        public void SetBlue()
        {
            Color c1 = new Color(0.5f, 0.8f, 0.35f);

            c1 = Color.Blue;

            Color answer = new Color(0f, 0f, 1f);

            Assert.Equal(answer, c1);
            Assert.True(answer == c1);
        }

        [Fact]
        public void GetCanvasWidthHeight()
        {
            Canvas canvas = new Canvas(1920, 1080);

            Assert.Equal(1920, canvas.width);
            Assert.Equal(1080, canvas.height);

            Assert.Equal(1920, canvas.GetWidth());
            Assert.Equal(1080, canvas.GetHeight());
        }

        [Fact]
        public void SetCanvasWidthHeight()
        {
            Canvas canvas = new Canvas();

            canvas.width = 1920;
            canvas.height = 1080;

            Assert.Equal(1920, canvas.GetWidth());
            Assert.Equal(1080, canvas.GetHeight());

            canvas.SetWidth(4000);
            canvas.SetHeight(3000);

            Assert.Equal(4000, canvas.GetWidth());
            Assert.Equal(3000, canvas.GetHeight());
        }

        [Fact]
        public void CreateCanvas()
        {
            Canvas canvas = new Canvas(1920, 1080);

            Color answer = new Color(0f, 0f, 0f);

            bool pass = true;

            for (int x = 0; x < canvas.width; x++)
            {
                for (int y = 0; y < canvas.height; y++)
                {
                    if (canvas.imagePlane[x, y] != answer)
                        pass = false;
                    Assert.Equal(0, canvas.imagePlane[x, y].red);
                    Assert.Equal(0, canvas.imagePlane[x, y].green);
                    Assert.Equal(0, canvas.imagePlane[x, y].blue);
                }
            }
            Assert.True(pass);
        }

        [Fact]
        public void FillCanvas()
        {
            Canvas canvas = new Canvas(1920, 1080);

            canvas.FillCanvas(Color.Green);

            Color answer = new Color(0f, 1f, 0f);

            bool pass = true;

            for (int x = 0; x < canvas.width; x++)
            {
                for (int y = 0; y < canvas.height; y++)
                {
                    if (canvas.imagePlane[x, y] != answer)
                        pass = false;
                    Assert.Equal(0, canvas.imagePlane[x, y].red);
                    Assert.Equal(1, canvas.imagePlane[x, y].green);
                    Assert.Equal(0, canvas.imagePlane[x, y].blue);
                }
            }
            Assert.True(pass); 
        }

        [Fact]
        public void SetPixelColor()
        {
            Canvas canvas = new Canvas(1920, 1080);

            canvas.SetPixelColor(50,200, Color.Red);
            canvas.imagePlane[500, 654] = Color.Blue;

            Assert.Equal(Color.Red, canvas.imagePlane[50, 200]);
            Assert.Equal(Color.Blue, canvas.GetPixelColor(500, 654));
        }

        [Fact]
        public void GetPixelColor()
        {
            Canvas canvas = new Canvas(1920,1080);

            canvas.FillCanvas(Color.Green);
            canvas.SetPixelColor(50, 200, Color.Red);
            canvas.imagePlane[500, 654] = Color.Blue;

            Assert.Equal(Color.Green, canvas.imagePlane[51, 200]);
            Assert.Equal(Color.Red, canvas.imagePlane[50, 200]);
            Assert.Equal(Color.Blue, canvas.GetPixelColor(500, 654));
        }

        [Fact]
        public void PPMHeader()
        {
            Canvas canvas = new Canvas(15, 10);
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders__";
            string fileName = "PPMHeader_Test_01.ppm";

            string fileDirectoryComplete = filePath + "\\" + fileName;

            canvas.FillCanvas(Color.Blue);
            canvas.SetPixelColor(9, 4, Color.Red);
            canvas.imagePlane[3, 2] = Color.Green;

            Save.PPM(fileDirectoryComplete, canvas);


            string[] fileContentsByLine = File.ReadAllLines(fileDirectoryComplete);

            Assert.Equal("P3", fileContentsByLine[0]);
            Assert.Equal($"{canvas.width} {canvas.height}", fileContentsByLine[1]);
            Assert.Equal("255", fileContentsByLine[2]);
        }

        [Fact]
        public void PPMBody()
        {
            Canvas canvas = new Canvas(5, 5);
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders__";
            string fileName = "PPMBody_Test_01.ppm";

            string fileDirectoryComplete = filePath + "\\" + fileName;

            canvas.FillCanvas(Color.Blue);

            Save.PPM(fileDirectoryComplete, canvas);

            string[] fileContentsByLine = File.ReadAllLines(fileDirectoryComplete);

            Assert.Equal("0 0 255 0 0 255 0 0 255 0 0 255 0 0 255 ", fileContentsByLine[4]);
        }

        [Fact]
        public void PPMBodyLineMax()
        {

        }

        [Fact]
        public void PPMFooter()
        {
            Canvas canvas = new Canvas(15, 10);
            string filePath = "C:\\Dev\\C#\\PracticePrograms\\RayTracerChallenge\\__renders__";
            string fileName = "PPMFooter_Test_01.ppm";

            string fileDirectoryComplete = filePath + "\\" + fileName;

            canvas.FillCanvas(Color.Blue);
            canvas.SetPixelColor(9, 4, Color.Red);
            canvas.imagePlane[3, 2] = Color.Green;

            Save.PPM(fileDirectoryComplete, canvas);

            string[] fileContentsByLine = File.ReadAllLines(fileDirectoryComplete);

            Assert.Equal("", fileContentsByLine[fileContentsByLine.Length - 1]);
        }

        [Fact]
        public void Clamp()
        {
            Color c1 = new Color(11.0f, -5.23f, .5f);

            Assert.Equal(1.0f, Save.Clamp(c1.red));
            Assert.Equal(0.0f, Save.Clamp(c1.green));
            Assert.Equal(0.5f, Save.Clamp(c1.blue));
        }

    }
}
