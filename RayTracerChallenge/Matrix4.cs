using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Matrix4
    {
        int size = 4;
        float[,] matrix; 

        public Matrix4(float m00 = 1.0f, float m01 = 0.0f, float m02 = 0.0f, float m03 = 0.0f,
                       float m10 = 0.0f, float m11 = 1.0f, float m12 = 0.0f, float m13 = 0.0f,
                       float m20 = 0.0f, float m21 = 0.0f, float m22 = 1.0f, float m23 = 0.0f,
                       float m30 = 0.0f, float m31 = 0.0f, float m32 = 0.0f, float m33 = 1.0f)
        {
            matrix = new float[4, 4];

            matrix[0, 0] = m00; matrix[0, 1] = m01; matrix[0, 2] = m02; matrix[0, 3] = m03;
            matrix[1, 0] = m10; matrix[1, 1] = m11; matrix[1, 2] = m12; matrix[1, 3] = m13;
            matrix[2, 0] = m20; matrix[2, 1] = m21; matrix[2, 2] = m22; matrix[2, 3] = m23;
            matrix[3, 0] = m30; matrix[3, 1] = m31; matrix[3, 2] = m32; matrix[3, 3] = m33;
        }

        public float this[int r, int c]
        {
            get { return matrix[r, c]; }
            set { matrix[r, c] = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix4 matrix &&
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

        public static bool operator ==(Matrix4 m1, Matrix4 m2)
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

        public static bool operator !=(Matrix4 m1, Matrix4 m2)
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

        public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
        {
            Matrix4 returnMatrix = new Matrix4();

            for (int row = 0; row < m1.size; row++)
            {
                for (int column = 0; column < m1.size; column++)
                {
                    returnMatrix[row, column] = m1[row, 0] * m2[0, column] +
                                                m1[row, 1] * m2[1, column] +
                                                m1[row, 2] * m2[2, column] +
                                                m1[row, 3] * m2[3, column] ;
                }
            }

            return returnMatrix;
        }

        public static Tuple operator *(Matrix4 m1, Tuple t2)
        {
            Tuple returnTuple = new Tuple();

            returnTuple.x = m1[0, 0] * t2.x + m1[0, 1] * t2.y + m1[0, 2] * t2.z + m1[0, 3] * t2.w;
            returnTuple.y = m1[1, 0] * t2.x + m1[1, 1] * t2.y + m1[1, 2] * t2.z + m1[1, 3] * t2.w;
            returnTuple.z = m1[2, 0] * t2.x + m1[2, 1] * t2.y + m1[2, 2] * t2.z + m1[2, 3] * t2.w;
            returnTuple.w = m1[3, 0] * t2.x + m1[3, 1] * t2.y + m1[3, 2] * t2.z + m1[3, 3] * t2.w;

            return returnTuple;
        }

        public Matrix4 Transpose()
        {
            Matrix4 returnMatrix = new Matrix4();

            for (int row = 0; row < this.size; row++)
            {
                for (int column = 0; column < this.size; column++)
                {
                    returnMatrix[row, column] = this[column, row];
                }
            }

            return returnMatrix;
        }

        public Matrix3 SubMatrix(int rowDel, int columnDel)
        {
            Matrix3 subMatrix = new Matrix3();

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

        public float Minor(int row, int column)
        {
            Matrix3 subMatrix = this.SubMatrix(row, column);

            float minor = subMatrix.Determinate();
            return minor;
        }

        public float Cofactor(int row, int column)
        {
            Matrix4 signMatrix = new Matrix4(1.0f, -1.0f, 1.0f, -1.0f,
                                             -1.0f, 1.0f, -1.0f, 1.0f,
                                             1.0f, -1.0f, 1.0f, -1.0f,
                                             -1.0f, 1.0f, -1.0f, 1.0f);

            float cofactor = this.Minor(row, column) * signMatrix[row, column];
            return cofactor;
        }

        public float Determinate()
        {
            float firstColCofactor = this.Cofactor(0, 0);
            float secondColCofactor = this.Cofactor(0, 1);
            float thirdColCofactor = this.Cofactor(0, 2);
            float forthColCofactor = this.Cofactor(0, 3);

            float determinate = (this[0, 0] * firstColCofactor) + (this[0, 1] * secondColCofactor) + (this[0, 2] * thirdColCofactor) + (this[0, 3] * forthColCofactor);
            
            return determinate;
        }

        public Matrix4 Invert()
        {
            Matrix4 returnMatrix = new Matrix4();
            Matrix4 inputMatrix = this;

            float determinate = inputMatrix.Determinate();
            if (determinate == 0)
                throw new MatrixInvertibleException();

            for (int row = 0; row < inputMatrix.size; row++)
            {
                for (int column = 0; column < inputMatrix.size; column++)
                {
                    returnMatrix[column, row] = inputMatrix.Cofactor(row, column) / determinate;
                }
            }

            return returnMatrix;
        }

 

    }
}
