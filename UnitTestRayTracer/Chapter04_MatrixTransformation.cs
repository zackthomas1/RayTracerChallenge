using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace UnitTestRayTracer
{
    public class Chapter04_MatrixTransformation
    {

        [Fact]
        public void TranslationPoint()
        {
            Point p1 = new Point(-3, 4, 5);
            Matrix4 m2 = new Matrix4();

            m2.Translate(5, -3, 2);

            Point result = p1 * m2;
            Point answer = new Point(2, 1, 7);

            Assert.True(answer == result);
        }

        [Fact]
        public void TranslationInversePoint()
        {
            Point p1 = new Point(-3, 4, 5);
            Matrix4 m2 = new Matrix4();

            m2.Translate(5, -3, 2);
            m2 = m2.Invert();

            Point result = m2 * p1;
            Point answer = new Point(-8, 7, 3);

            Assert.True(answer == result);
        }

        [Fact]
        public void TranslationNoEffectVector()
        {
            Vector3 v1 = new Vector3(-3, 4, 5);
            Matrix4 m2 = new Matrix4();

            m2.Translate(5, -3, 2);
            m2 = m2.Invert();

            Vector3 result = v1 * m2;

            Assert.True(v1 == result);

        }

        [Fact]
        public void ScalePoint()
        {
            Point p1 = new Point(-4, 6, 8);
            Matrix4 m2 = new Matrix4();

            m2.Scale(2, 3, 4);

            Point result = m2 * p1;
            Point answer = new Point(-8, 18, 32);


            Assert.True(answer == result);
        }

        [Fact]
        public void ScaleVector()
        {
            Vector3 v1 = new Vector3(-4, 6, 8);
            Matrix4 m2 = new Matrix4();

            m2.Scale(2, 3, 4);

            Vector3 result = m2 * v1;
            Vector3 answer = new Vector3(-8, 18, 32);

            Assert.True(answer == result);
        }

        [Fact]
        public void RotateXPoint()
        {
            Point p1 = new Point(0, 1, 0);
            Matrix4 rotation01 = new Matrix4();
            Matrix4 rotation02 = new Matrix4();

            rotation01.Rotate_X(Math.PI / 2);
            rotation02.RotateDegree_X(45);

            Point result01 = rotation01 * p1;
            Point answer01 = new Point(0, 0, 1);

            //Assert.Equal(answer01, result01);
            Assert.True(answer01 == result01);

            Point result02 = rotation02 * p1;
            Point answer02 = new Point(0, (float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2);


            //Assert.Equal(answer02, result02);
            Assert.True(answer02 == result02);

        }

        [Fact]
        public void RotateXVector()
        {
            Vector3 v1 = new Vector3(0, 1, 0);
            Matrix4 rotation01 = new Matrix4();
            Matrix4 rotation02 = new Matrix4();

            rotation01.Rotate_X(Math.PI / 2);
            rotation02.RotateDegree_X(45);

            Vector3 result01 = rotation01 * v1;
            Vector3 answer01 = new Vector3(0, 0, 1);

            //Assert.Equal(answer01, result01);
            Assert.True(answer01 == result01);

            Vector3 result02 = rotation02 * v1;
            Vector3 answer02 = new Vector3(0, (float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2);


            //Assert.Equal(answer02, result02);
            Assert.True(answer02 == result02);
        }

        [Fact]
        public void RotateYPoint()
        {
            Point p1 = new Point(0, 0, 1);
            Matrix4 rotation01 = new Matrix4();
            Matrix4 rotation02 = new Matrix4();

            rotation01.Rotate_Y(Math.PI / 2);
            rotation02.RotateDegree_Y(45);

            Point result01 = p1 * rotation01;
            Point answer01 = new Point(1, 0, 0);

            //Assert.Equal(answer01, result01);
            Assert.True(answer01 == result01);


            Point result02 = rotation02 * p1;
            Point answer02 = new Point((float)Math.Sqrt(2) / 2, 0, (float)Math.Sqrt(2) / 2);

            Assert.Equal(answer02, result02);
            Assert.True(answer02 == result02);
        }

        [Fact]
        public void RotateYVector()
        {
            Vector3 p1 = new Vector3(0, 0, 1);
            Matrix4 rotation01 = new Matrix4();
            Matrix4 rotation02 = new Matrix4();

            rotation01.Rotate_Y(Math.PI / 2);
            rotation02.RotateDegree_Y(45);

            Vector3 result01 = p1 * rotation01;
            Vector3 answer01 = new Vector3(1, 0, 0);

            //Assert.Equal(answer01, result01);
            Assert.True(answer01 == result01);


            Vector3 result02 = rotation02 * p1;
            Vector3 answer02 = new Vector3((float)Math.Sqrt(2) / 2, 0, (float)Math.Sqrt(2) / 2);

            Assert.Equal(answer02, result02);
            Assert.True(answer02 == result02);
        }

        [Fact]
        public void RotateZPoint()
        {
            Point p1 = new Point(0, 1, 0);
            Matrix4 rotation01 = new Matrix4();
            Matrix4 rotation02 = new Matrix4();

             rotation01.Rotate_Z(Math.PI / 2);
             rotation02.RotateDegree_Z(45);

            Point result01 = p1 * rotation01;
            Point answer01 = new Point(-1, 0, 0);

            Point result02 = p1 * rotation02;
            Point answer02 = new Point(-(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2, 0);

            //Assert.Equal(answer01, result01);
            Assert.True(answer01 == result01);

            //Assert.Equal(answer02, result02);
            Assert.True(answer02 == result02);
        }

        [Fact]
        public void RotateZVector()
        {
            Vector3 p1 = new Vector3(0, 1, 0);
            Matrix4 rotation01 = new Matrix4();
            Matrix4 rotation02 = new Matrix4();

            rotation01.Rotate_Z(Math.PI / 2);
            rotation02.RotateDegree_Z(45);

            Vector3 result01 = p1 * rotation01;
            Vector3 answer01 = new Vector3(-1, 0, 0);

            Vector3 result02 = p1 * rotation02;
            Vector3 answer02 = new Vector3(-(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2, 0);

            //Assert.Equal(answer01, result01);
            Assert.True(answer01 == result01);

            //Assert.Equal(answer02, result02);
            Assert.True(answer02 == result02);
        }

        [Fact]
        public void RotationPoint()
        {
            Point p1 = new Point(0, 1, 0);
            Matrix4 rotation01 = new Matrix4();

            rotation01.RotateDegree(0,0,90);

            Point result = p1 * rotation01;
            Point answer = new Point(-1, 0, 0);

            //Assert.Equal(answer, result);
            Assert.True(answer == result);
        }

        [Fact]
        public void ShearPoint()
        {
            Point p1 = new Point(2, 3, 4);
            Matrix4 m2 = new Matrix4();

            m2.Shear(0, 1, 0, 0, 0, 0);

            Point result = p1 * m2;
            Point answer = new Point(6, 3, 4);

            Assert.True(answer == result);
        }

        [Fact]
        public void ShearVector()
        {
            Vector3 v1 = new Vector3(2, 3, 4);
            Matrix4 m2 = new Matrix4();

            m2.Shear(0, 1, 0, 0, 0, 0);

            Vector3 result = v1 * m2;
            Vector3 answer = new Vector3(6, 3, 4);

            Assert.True(answer == result);
        }

        [Fact]
        public void ChainTransformation()
        {
            Point p1 = new Point(1, 0, 1);
            Matrix4 identity = new Matrix4();

            Matrix4 rotateX = Matrix4.RotateMatrix_X(Math.PI / 2);
            Matrix4 scale = Matrix4.ScaleMatrix(5, 5, 5);
            Matrix4 translate = Matrix4.TranslateMatrix(10, 5, 7);

            Point p2 = p1 * (translate * scale * rotateX);
            Point p3 = p1 * identity.Translate(10, 5, 7).Scale(5, 5, 5).Rotate_X(Math.PI / 2);
            Point p4 = p1 * identity.Rotate_X(Math.PI / 2).Scale(5, 5, 5).Translate(10, 5, 7);

            Assert.Equal(p2, p3);
            Assert.True(p2 == p3);
        }

    }
}
