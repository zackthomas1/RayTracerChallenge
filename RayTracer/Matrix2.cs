using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Matrix2
    {
        // Instance Variables
        int size = 2;
        float[,] matrix;

        // Get/Set methods
        public float this[int r, int c]
        {
            get { return matrix[r, c]; }
            set { matrix[r, c] = value; }
        }

        // Constructors
        public Matrix2(float m00 = 1.0f, float m01 = 0.0f,
                       float m10 = 0.0f, float m11 = 1.0f)
        {
            matrix = new float[2, 2];

            matrix[0, 0] = m00; matrix[0, 1] = m01;
            matrix[1, 0] = m10; matrix[1, 1] = m11;

        }

        // Class overloads
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

        public override bool Equals(object obj)
        {
            return obj is Matrix2 matrix &&
                   size == matrix.size &&
                   EqualityComparer<float[,]>.Default.Equals(this.matrix, matrix.matrix);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(size, matrix);
        }

        public static bool operator ==(Matrix2 m1, Matrix2 m2)
        {
            bool matrixEquality = true; 

            for (int rowIndex = 0; rowIndex < m1.size; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < m1.size; columnIndex++)
                {
                    if (!Utilities.FloatEquality(m1[rowIndex,columnIndex], m2[rowIndex, columnIndex]))
                    {
                        matrixEquality = false; 
                    }
                }
            }

            return matrixEquality;
        }

        public static bool operator !=(Matrix2 m1, Matrix2 m2)
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

        public static Matrix2 operator *(Matrix2 m1, Matrix2 m2)
        {
            Matrix2 returnMatrix = new Matrix2();

            //returnMatrix[0, 0] = m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0];
            //returnMatrix[1, 0] = m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0];
            //returnMatrix[0, 1] = m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1];
            //returnMatrix[1, 1] = m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1];

            for (int row = 0;  row < m1.size; row++)
            {
                for (int column = 0; column < m1.size; column++)
                {
                    returnMatrix[row, column] = m1[row, 0] * m2[0, column] +
                                                m1[row, 1] * m2[1, column]; 
                }
            }

            return returnMatrix;
        }

        // Methods
        /// <summary>
        /// Returns a Matrix2 with rows and column position reversed from input Matrix2
        /// </summary>
        /// <returns></returns>
        public Matrix2 Transpose()
        {
            Matrix2 returnMatrix = new Matrix2(); 

            for (int row = 0; row < this.size; row++)
            {
                for (int column = 0; column < this.size; column++)
                {
                    returnMatrix[row, column] = this[column, row];
                }
            }

            return returnMatrix;

        }

        public float Determinate()
        {
            float determinate = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            return determinate;
        }



    }
}
