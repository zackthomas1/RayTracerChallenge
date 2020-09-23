using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Matrix3
    {
        int size = 3;
        float[,] matrix;

        public Matrix3(float m00 = 1.0f, float m01 = 0.0f, float m02 = 0.0f,
                       float m10 = 0.0f, float m11 = 1.0f, float m12 = 0.0f,
                       float m20 = 0.0f, float m21 = 0.0f, float m22 = 1.0f)
        {
            matrix = new float[3, 3];

            matrix[0, 0] = m00; matrix[0, 1] = m01; matrix[0, 2] = m02;
            matrix[1, 0] = m10; matrix[1, 1] = m11; matrix[1, 2] = m12;
            matrix[2, 0] = m20; matrix[2, 1] = m21; matrix[2, 2] = m22;
        }

        public float this[int r, int c]
        {
            get { return matrix[r, c]; }
            set { matrix[r, c] = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix3 matrix &&
                   size == matrix.size &&
                   EqualityComparer<float[,]>.Default.Equals(this.matrix, matrix.matrix);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(size, matrix);
        }

        public override string ToString()
        {
            string returnString = "";

            for (int rowIndex = 0; rowIndex < this.size; rowIndex++)
            {
                returnString += "|";
                for (int columnIndex = 0; columnIndex < this.size; columnIndex++)
                {
                    returnString += this[rowIndex, columnIndex] + " ";
                    returnString += "|";
                }
                returnString += "\n";
            }

            return returnString;
        }

        public static bool operator ==(Matrix3 m1, Matrix3 m2)
        {
            bool matrixEquality = true;

            for (int rowIndex = 0; rowIndex < m1.size; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < m1.size; columnIndex++)
                {
                    if (!Utilities.FloatEquality(m1[rowIndex, columnIndex], m2[rowIndex, columnIndex]))
                    {
                        matrixEquality = false;
                    }
                }
            }

            return matrixEquality;
        }

        public static bool operator !=(Matrix3 m1, Matrix3 m2)
        {
            bool matrixEquality = false;

            for (int rowIndex = 0; rowIndex < m1.size; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < m1.size; columnIndex++)
                {
                    if (!Utilities.FloatEquality(m1[rowIndex, columnIndex], m2[rowIndex, columnIndex]))
                    {
                        matrixEquality = true;
                    }
                }
            }

            return matrixEquality;
        }

        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            Matrix3 returnMatrix = new Matrix3();

            for (int row = 0; row < m1.size; row++)
            {
                for (int column = 0; column < m1.size; column++)
                {
                    returnMatrix[row, column] = m1[row, 0] * m2[0, column] +
                                                m1[row, 1] * m2[1, column] +
                                                m1[row, 2] * m2[2, column];
                }
            }

            return returnMatrix;
        }

        public Matrix3 Transpose()
        {
            Matrix3 returnMatrix = new Matrix3();

            for (int row = 0; row < this.size; row++)
            {
                for (int column = 0; column < this.size; column++)
                {
                    returnMatrix[row, column] = this[column, row];
                }
            }

            return returnMatrix;
        }

        public Matrix2 SubMatrix(int rowDel, int columnDel)
        {
            Matrix2 subMatrix = new Matrix2();

            int subMatrixRow = 0;

            for (int row = 0; row < this.size; row++)
            {
                if (row != rowDel)
                {
                    int subMatrixColumn = 0;
                    for (int column = 0; column < this.size; column++)
                    {
                        if (column != columnDel)
                        {
                            subMatrix[subMatrixRow, subMatrixColumn] = this[row, column];
                            subMatrixColumn++;
                        }
                    }
                    subMatrixRow++;
                } 
            }
            return subMatrix;
        }

        /// <summary>
        /// The determinate of a 2x2 submatrix of given 3x3 matrix and position
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public float Minor(int row, int column)
        {
            Matrix2 subMatrix = this.SubMatrix(row, column);

            float minor = subMatrix.Determinate();
            return minor;
        }

        /// <summary>
        /// The minor multiplied by the sign matrix ((row + column) odd postive, even negative)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public float Cofactor(int row, int column)
        {
            Matrix3 signMatrix = new Matrix3(1.0f, -1.0f, 1.0f,
                                            -1.0f, 1.0f, -1.0f,
                                             1.0f, -1.0f, 1.0f);

            float cofactor = this.Minor(row, column) * signMatrix[row, column];
            return cofactor;
        }

        public float Determinate()
        {
            float firstColCofactor = this.Cofactor(0, 0); 
            float secondColCofactor = this.Cofactor(0, 1);
            float thirdColCofactor = this.Cofactor(0, 2);

            float determinate = (this[0, 0] * firstColCofactor) + (this[0, 1] * secondColCofactor) + (this[0, 2] * thirdColCofactor);
            return determinate;
        }

    }
}
