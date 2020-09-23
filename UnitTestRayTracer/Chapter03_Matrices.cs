using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace UnitTestRayTracer
{
    public class Chapter03_Matrices
    {

        [Fact]
        public void ConstructMatrix2()
        {
            Matrix2 matrix = new Matrix2(-3, 5,
                                          1, -2);

            Assert.Equal(-3f, matrix[0, 0]);
            Assert.Equal(5f, matrix[0, 1]);
            Assert.Equal(1f, matrix[1, 0]);
            Assert.Equal(-2f, matrix[1, 1]);
        }


        [Fact]
        public void ConstructMatrix3()
        {
            Matrix3 matrix = new Matrix3(-3, 5, 0,
                                         1, -2, -7,
                                         0, 1, 1);

            Assert.Equal(-3f, matrix[0, 0]);
            Assert.Equal(5f, matrix[0, 1]);
            Assert.Equal(0f, matrix[0, 2]);

            Assert.Equal(1f, matrix[1, 0]);
            Assert.Equal(-2f, matrix[1, 1]);
            Assert.Equal(-7f, matrix[1, 2]);

            Assert.Equal(0f, matrix[2, 0]);
            Assert.Equal(1f, matrix[2, 1]);
            Assert.Equal(1f, matrix[2, 2]);
        }

        [Fact]
        public void ConstructMatrix4()
        {
            Matrix4 matrix = new Matrix4(1, 2, 3, 4,
                                         5.5f, 6.5f, 7.5f, 8.5f,
                                         9, 10, 11, 12,
                                         13.5f, 14.5f, 15.5f, 16.5f);

            Assert.Equal(1, matrix[0, 0]);
            Assert.Equal(2, matrix[0, 1]);
            Assert.Equal(3, matrix[0, 2]);
            Assert.Equal(4, matrix[0, 3]);

            Assert.Equal(5.5, matrix[1, 0]);
            Assert.Equal(6.5, matrix[1, 1]);
            Assert.Equal(7.5, matrix[1, 2]);
            Assert.Equal(8.5, matrix[1, 3]);

            Assert.Equal(9, matrix[2, 0]);
            Assert.Equal(10, matrix[2, 1]);
            Assert.Equal(11, matrix[2, 2]);
            Assert.Equal(12, matrix[2, 3]);

            Assert.Equal(13.5, matrix[3, 0]);
            Assert.Equal(14.5, matrix[3, 1]);
            Assert.Equal(15.5, matrix[3, 2]);
            Assert.Equal(16.5, matrix[3, 3]);
        }

        [Fact]
        public void ToStringMatrix2()
        {
            Matrix2 matrix = new Matrix2(-3, 5f, 1f, -2);

            string answer = "|-3 |5 |\n" +
                            "|1 |-2 |\n";

            Assert.Equal(answer, matrix.ToString());
        }

        [Fact]
        public void EqualityMatrix2()
        {
            Matrix2 m1 = new Matrix2(-3, 5f, 1f, -2);
            Matrix2 m2 = new Matrix2(-3, 5f, 1f, -2);

            //Assert.Equal(m1, m2);
            Assert.True(m1 == m2);
        }

        [Fact]
        public void InequalityMatrix2()
        {
            Matrix2 m1 = new Matrix2(-3, 5f, 1f, -2);
            Matrix2 m2 = new Matrix2(-3, 5f, 1f, -2);
            Matrix2 m3 = new Matrix2(-3, 5.5f, 1f, -2);

            //Assert.Equal(m1, m2);
            Assert.False(m1 != m2);
            Assert.True(m1 != m3);
        }

        [Fact]
        public void ToStringMatrix3()
        {
            Matrix3 m1 = new Matrix3(-3, 5f, 1f,
                                    -2, 4, 5,
                                     7, 9, 8);

            string answer = "|-3 |5 |1 |\n" +
                            "|-2 |4 |5 |\n" +
                            "|7 |9 |8 |\n";

            Assert.Equal(answer, m1.ToString());
        }

        [Fact]
        public void EqualityMatrix3()
        {
            Matrix3 m1 = new Matrix3(-3, 5f, 1f,
                                     -2, 4, 5,
                                      7, 9, 8);
            Matrix3 m2 = new Matrix3(-3, 5f, 1f,
                                     -2, 4, 5,
                                      7, 9, 8);

            //Assert.Equal(m1, m2);
            Assert.True(m1 == m2);
        }

        [Fact]
        public void InequalityMatrix3()
        {
            Matrix3 m1 = new Matrix3(-3, 5, 1,
                                     -2, 4, 5,
                                      7, 9, 8);
            Matrix3 m2 = new Matrix3(-3, 5, 1,
                                     -2, 4, 5,
                                      7, 9, 8);
            Matrix3 m3 = new Matrix3(-3, 5.5f, 1,
                                     -2, 4.4f, 7.4f,
                                      7, 9, 8);

            //Assert.Equal(m1, m2);
            Assert.False(m1 != m2);
            Assert.True(m1 != m3);
        }

        [Fact]
        public void ToStringMatrix4()
        {
            Matrix4 m1 = new Matrix4(-3, 5f, 1f, 4,
                                    -2, 4, 5, 9,
                                     7, 9, 8, 3,
                                     4, 6, 7, 2);

            string answer = "|-3 |5 |1 |4 |\n" +
                            "|-2 |4 |5 |9 |\n" +
                            "|7 |9 |8 |3 |\n" +
                            "|4 |6 |7 |2 |\n";

            Assert.Equal(answer, m1.ToString());
        }

        [Fact]
        public void EqualityMatrix4()
        {
            Matrix4 m1 = new Matrix4(-3, 5f, 1f, 4,
                                    -2, 4, 5, 9,
                                     7, 9, 8, 3,
                                     4, 6, 7, 2);
            Matrix4 m2 = new Matrix4(-3, 5f, 1f, 4,
                                    -2, 4, 5, 9,
                                     7, 9, 8, 3,
                                     4, 6, 7, 2);

            //Assert.Equal(m1, m2);
            Assert.True(m1 == m2);
        }

        [Fact]
        public void InequalityMatrix4()
        {
            Matrix4 m1 = new Matrix4(-3, 5f, 1f, 4,
                                    -2, 4, 5, 9,
                                     7, 9, 8, 3,
                                     4, 6, 7, 2);
            Matrix4 m2 = new Matrix4(-3, 5f, 1f, 4,
                                    -2, 4, 5, 9,
                                     7, 9, 8, 3,
                                     4, 6, 7, 2);
            Matrix4 m3 = new Matrix4(3, 5f, 1f, 4,
                                    -2, 4, 5, 9,
                                     7, 9, 5, -3,
                                     4, -6, 7, 2);

            //Assert.Equal(m1, m2);
            Assert.False(m1 != m2);
            Assert.True(m1 != m3);
        }

        [Fact]
        public void MultipicationMatrix2()
        {
            Matrix2 m1 = new Matrix2(2, 1,
                                     5, 2);
            Matrix2 m2 = new Matrix2(1, 4,
                                     3, 6);

            Matrix2 multiplicationResult01 = m1 * m2;
            Matrix2 answer01 = new Matrix2(5, 14,
                                         11, 32);

            Assert.True(answer01 == multiplicationResult01);

            Matrix2 m3 = new Matrix2(1, 7,
                                     2, 4);
            Matrix2 m4 = new Matrix2(3, 3,
                                     5, 2);

            Matrix2 multiplicationResult02 = m3 * m4;
            Matrix2 answer02 = new Matrix2(38, 17,
                                           26, 14);

            Assert.True(answer02 == multiplicationResult02);
        }

        [Fact]
        public void MultipicationMatrix3()
        {
            Matrix3 m1 = new Matrix3(1, 2, 5,
                                     3, 7, 6,
                                     4, 5, 8);
            Matrix3 m2 = new Matrix3(2, 4, 7,
                                     1, 3, 8,
                                     7, 5, 4);

            Matrix3 multiplicationResult01 = m1 * m2;
            Matrix3 answer01 = new Matrix3(39, 35, 43,
                                           55, 63, 101,
                                           69, 71, 100);

            Assert.True(answer01 == multiplicationResult01);

            Matrix3 m3 = new Matrix3(2, 5, 6,
                                     8, 10, -2,
                                     -3, 5, 4);
            Matrix3 m4 = new Matrix3(-9, 12, -4,
                                     6, 2, 7,
                                     8, -5, 3);

            Matrix3 multiplicationResult02 = m3 * m4;
            Matrix3 answer02 = new Matrix3(60, 4, 45,
                                           -28, 126, 32,
                                           89, -46, 59);

            Assert.True(answer02 == multiplicationResult02);
        }

        [Fact]
        public void MultipicationMatrix4()
        {
            Matrix4 m1 = new Matrix4(1, 2, 5, 4,
                                     3, 7, 6, -3,
                                     4, 5, 8, -2,
                                     -2, 4, -8, 7);
            Matrix4 m2 = new Matrix4(2, 4, 7, 8,
                                     1, 3, 8, -3,
                                     7, 5, 4, -1,
                                     3, -5, 7, -2);

            Matrix4 multiplicationResult01 = m1 * m2;
            Matrix4 answer01 = new Matrix4(51, 15, 71, -11,
                                           46, 78, 80, 3,
                                           63, 81, 86, 13,
                                           -35, -71, 35, -34);

            Assert.True(answer01 == multiplicationResult01);
        }

        [Fact]
        public void MultipicationMatrix4Tuple()
        {
            Matrix4 m1 = new Matrix4(1, 2, 3, 4,
                                     2, 4, 4, 2,
                                     8, 6, 4, 1,
                                     0, 0, 0, 1);
            RayTracer.Tuple t2 = new RayTracer.Tuple(1, 2, 3, 1);

            RayTracer.Tuple result = m1 * t2;

            RayTracer.Tuple answer = new RayTracer.Tuple(18, 24, 33, 1);

            Assert.True(answer == result);
        }

        [Fact]
        public void IdentityMultMatrix2()
        {
            Matrix2 m1 = new Matrix2(1, 7,
                                     2, -4);
            Matrix2 identityM = new Matrix2();

            Matrix2 result = m1 * identityM;
            Matrix2 answer = new Matrix2(1, 7,
                                         2, -4);

            Assert.True(answer == result);
        }

        [Fact]
        public void IdentityMultMatrix3()
        {
            Matrix3 m1 = new Matrix3(2, 5, 6,
                                     8, 10, -2,
                                     -3, 5, 4);
            Matrix3 identityM = new Matrix3();

            Matrix3 result = m1 * identityM;
            Matrix3 answer = new Matrix3(2, 5, 6,
                                         8, 10, -2,
                                         -3, 5, 4);
            Assert.True(answer == result);
        }

        [Fact]
        public void IdentityMultMatrix4()
        {
            Matrix4 m1 = new Matrix4(1, 2, 5, 4,
                                     3, 7, 6, -3,
                                     4, 5, 8, -2,
                                     -2, 4, -8, 7);
            Matrix4 identityM = new Matrix4();

            Matrix4 result = m1 * identityM;
            Matrix4 answer = new Matrix4(1, 2, 5, 4,
                                     3, 7, 6, -3,
                                     4, 5, 8, -2,
                                     -2, 4, -8, 7);

            Assert.True(answer == result);
        }

        [Fact]
        public void TransposeMatrix2()
        {
            Matrix2 m1 = new Matrix2(1, 7,
                                    2, -4);

            Matrix2 result = m1.Transpose();
            Matrix2 answer = new Matrix2(1, 2,
                                         7, -4);

            Assert.True(result == answer);

            Matrix2 identity = new Matrix2();
            Assert.True(identity == identity.Transpose());
        }

        [Fact]
        public void TransposeMatrix3()
        {
            Matrix3 m1 = new Matrix3(2, 5, 6,
                                     8, 10, -2,
                                     -3, 5, 4);

            Matrix3 result = m1.Transpose();
            Matrix3 answer = new Matrix3(2, 8, -3,
                                         5, 10, 5,
                                         6, -2, 4);

            Assert.True(result == answer);

            Matrix3 identity = new Matrix3();
            Assert.True(identity == identity.Transpose());
        }

        [Fact]
        public void TransposeMatrix4()
        {
            Matrix4 m1 = new Matrix4(1, 2, 5, 4,
                                     3, 7, 6, -3,
                                     4, 5, 8, -2,
                                     -2, 4, -8, 7);

            Matrix4 result = m1.Transpose();
            Matrix4 answer = new Matrix4(1, 3, 4, -2,
                                         2, 7, 5, 4,
                                         5, 6, 8, -8,
                                         4, -3, -2, 7);

            Assert.True(result == answer);

            Matrix4 identity = new Matrix4();
            Assert.True(identity == identity.Transpose());
        }

        [Fact]
        public void DeterminateMatrix2()
        {
            Matrix2 matrix = new Matrix2(1, 5,
                                        -3, 2);

            float result = matrix.Determinate();
            float answer = 17;

            Assert.True(answer == result);
        }
        [Fact]
        public void DeterminateMatrix3()
        {
            Matrix3 m1 = new Matrix3(3, 5, 0,
                                     2, -1, -7,
                                     6, -1, 5);

            float result = m1.Determinate();
            float answer = -296;

            Assert.Equal(answer, result);

            Matrix3 m2 = new Matrix3(1, 2, 6,
                                     -5, 8, -4,
                                     2, 6, 4);

            float result02 = m2.Determinate();
            float answer02 = -196;

            Assert.Equal(answer02, result02);
        }

        [Fact]
        public void DeterminateMatrix4()
        {
            Matrix4 m1 = new Matrix4(-2, -8, 3, 5,
                                     -3, 1, 7, 3,
                                      1, 2, -9, 6, 
                                      -6, 7, 7, -9);

            float result = m1.Determinate();
            float answer = -4071;

            Assert.Equal(answer, result);
        }

            [Fact]
        public void SubMatrixMatrix3()
        {
            Matrix3 m1 = new Matrix3(2, 5, 6,
                                     8, 10, -2,
                                     -3, 5, 4);

            Matrix2 subMatrix = m1.SubMatrix(0, 2);
            Matrix2 answer = new Matrix2(8, 10,
                                        -3, 5);

            Assert.True(subMatrix == answer);


            Matrix3 m2 = new Matrix3(3, 5, 0,
                                     2, -1, -7,
                                     6, -1, 5);

            Matrix2 subMatrix2 = m2.SubMatrix(1, 0);
            Matrix2 answe2 = new Matrix2(5, 0,
                                        -1, 5);

            Assert.True(subMatrix2 == answe2);

        }

        [Fact]
        public void SubMatrixMatrix4()
        {
            Matrix4 m1 = new Matrix4(-6, 1, 1, 6,
                                     -8, 5, 8, 6,
                                     -1, 0, 8, 2,
                                     -7, 1, -1, 1);

            Matrix3 subMatrix = m1.SubMatrix(2, 1);
            Matrix3 answer = new Matrix3(-6, 1, 6,
                                         -8, 8, 6,
                                         -7, -1, 1);

            Assert.True(subMatrix == answer);
        }

        [Fact]
        public void MinorMatrix3()
        {
            Matrix3 m1 = new Matrix3(3, 5, 0,
                                     2, -1, -7,
                                     6, -1, 5);

            float result = m1.Minor(1, 0);
            float answer = 25;

            Assert.Equal(answer, result);
        }

        [Fact]
        public void MinorMatrix4()
        {
            Matrix4 m1 = new Matrix4(-2, -8, 3, 5,
                                     -3, 1, 7, 3,
                                      1, 2, -9, 6,
                                     -6, 7, 7, -9);
            Matrix3 minor00 = new Matrix3(1, 7, 3,
                                          2, -9, 6,
                                          7, 7, -9);

            float result = m1.Minor(0, 0);
            float answer = minor00.Determinate();

            Assert.Equal(answer, result);
        }

        [Fact]
        public void CofactorMatrix3()
        {
            Matrix3 m1 = new Matrix3(3, 5, 0,
                                     2, -1, -7,
                                     6, -1, 5);
            float result = m1.Cofactor(0, 0);
            float answer = -12;

            Assert.Equal(answer, result);

            float result02 = m1.Cofactor(1, 0);
            float answer02 = -25;

            Assert.Equal(answer02, result02); 
        }

        [Fact]
        public void CofactorMatrix4()
        {
            Matrix4 m1 = new Matrix4(-2, -8, 3, 5,
                                     -3, 1, 7, 3,
                                      1, 2, -9, 6,
                                     -6, 7, 7, -9);

            float result01 = m1.Cofactor(0, 0);
            float result02 = m1.Cofactor(0, 1);
            float result03 = m1.Cofactor(0, 2);
            float result04 = m1.Cofactor(0, 3);

            float answer01 = 690;
            float answer02 = 447;
            float answer03 = 210;
            float answer04 = 51;

            Assert.Equal(answer01, result01);
            Assert.Equal(answer02, result02);
            Assert.Equal(answer03, result03);
            Assert.Equal(answer04, result04);
        }

        [Fact]
        public void InverseMatrix4()
        {
            Matrix4 m1 = new Matrix4(-5, 2, 6, -8,
                                      1, -5, 1, 8,
                                      7, 7, -6, -7,
                                      1, -3, 7, 4);

            Matrix4 result = m1.Invert();
            Matrix4 answer = new Matrix4(0.21805f, 0.45113f, 0.24060f, -0.04511f,
                                        -0.80827f, -1.45677f, -0.44361f, 0.52068f,
                                        -0.07895f, -0.22368f, -0.05263f, 0.19737f,
                                        -0.52256f, -0.81391f, -0.30075f, 0.30639f);

            Assert.Equal(532, m1.Determinate());
            Assert.Equal(-160, m1.Cofactor(2, 3));
            Assert.Equal(105, m1.Cofactor(3, 2));

            Assert.True(answer == result);

            Matrix4 m2 = new Matrix4(8, -5, 9, 2,
                                     7, 5, 6, 1,
                                    -6, 0, 9, 6,
                                    -3, 0, -9, -4);

            Matrix4 result02 = m2.Invert();
            Matrix4 answer02 = new Matrix4(-0.15385f, -0.15385f, -0.28205f, -0.53846f,
                                           -0.07692f, 0.12308f, 0.02564f, 0.03077f,
                                            0.35897f, 0.35897f, 0.43590f, 0.92308f,
                                           -0.69231f, -0.69231f, -0.76923f, -1.92308f);

            Assert.True(answer02 == result02);
        }

        [Fact]
        public void MultiplyProductByInverseMatrix4()
        {
            Matrix4 m1 = new Matrix4(-5, 2, -6, -8,
                                      1, 4, 1, 8,
                                     -7, 8, -6, -7,
                                      1, -3, 7, 4);

            Matrix4 m2 = new Matrix4(-6, 1, 1, 6,
                                     -8, 5, 8, 6,
                                     -1, 0, 8, 2,
                                     -7, 1, -1, 1);

            Matrix4 product = m1 * m2;

            Assert.True(product * m2.Invert() == m1);
        }

        [Fact]
        public void InverseIdentityMatrix4()
        {
            Matrix4 m1 = new Matrix4(-5, 2, -6, -8,
                                      1, 4, 1, 8,
                                      -7, 8, -6, -7,
                                      1, -3, 7, 4);


            Matrix4 inverse = m1.Invert();
            Matrix4 identity = new Matrix4();

            Assert.True(identity == m1 * inverse);
        }


    }
}
