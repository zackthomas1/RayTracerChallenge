using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Matrix4
    {
        // Instance Variables
        int size = 4;
        float[,] matrix;

        // Get/Set methods
        public float this[int r, int c]
        {
            get { return matrix[r, c]; }
            set { matrix[r, c] = value; }
        }

        // Constructors
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
            return obj is Matrix4 matrix &&
                   size == matrix.size &&
                   EqualityComparer<float[,]>.Default.Equals(this.matrix, matrix.matrix);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(size, matrix);
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

        // Methods
        /// <summary>
        /// Switches the rows and column values
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Give a row and column position returns a submatrix of type Matrix3
        /// </summary>
        /// <param name="rowDel"></param>
        /// <param name="columnDel"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Calculates the minor which is the determinate of Matrix3 submatrix
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public float Minor(int row, int column)
        {
            Matrix3 subMatrix = this.SubMatrix(row, column);

            float minor = subMatrix.Determinate();
            return minor;
        }

        /// <summary>
        /// Cofactor is minor mutipled by 1.0(odd) or -1.0(even)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public float Cofactor(int row, int column)
        {
            Matrix4 signMatrix = new Matrix4(1.0f, -1.0f, 1.0f, -1.0f,
                                             -1.0f, 1.0f, -1.0f, 1.0f,
                                             1.0f, -1.0f, 1.0f, -1.0f,
                                             -1.0f, 1.0f, -1.0f, 1.0f);

            float cofactor = this.Minor(row, column) * signMatrix[row, column];
            return cofactor;
        }

        /// <summary>
        /// Calculates the determinate of a Matrix4 returning a float value
        /// </summary>
        /// <returns></returns>
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

        // Translate Methods
        //------------------------------------------------------------------
        public static Matrix4 TranslateMatrix(float x, float y, float z)
        {
            Matrix4 translationMatrix = new Matrix4();

            translationMatrix[0, 3] = x;
            translationMatrix[1, 3] = y;
            translationMatrix[2, 3] = z;

            return translationMatrix;
        }
       
        public Matrix4 Translate(float x, float y, float z)
        {
            Matrix4 temp = TranslateMatrix(x, y, z);
            this.matrix = (this * temp).matrix;
            return this;
        }

        // Scale Methods
        //------------------------------------------------------------------
        public static Matrix4 ScaleMatrix(float x, float y, float z)
        {
            Matrix4 translationMatrix = new Matrix4();

            translationMatrix[0, 0] = x;
            translationMatrix[1, 1] = y;
            translationMatrix[2, 2] = z;

            return translationMatrix;
        }

        public Matrix4 Scale(float x, float y, float z)
        {
            Matrix4 temp = ScaleMatrix(x, y, z);
            this.matrix = (this * temp).matrix;
            return this;
        }

        // Rotate Methods
        //------------------------------------------------------------------
            // Rotate X Methods
            //---------------------------------------------------------------
        public static Matrix4 RotateMatrix_X(double radian) 
        {
            Matrix4 rotationMatrix = new Matrix4();

            rotationMatrix[1, 1] = (float)Math.Cos(radian);
            rotationMatrix[1, 2] = (float)(Math.Sin(radian) * -1.0);
            rotationMatrix[2, 1] = (float)Math.Sin(radian);
            rotationMatrix[2, 2] = (float)(Math.Cos(radian) * -1.0);

            return rotationMatrix;
        }

        public Matrix4 Rotate_X(double radian)
        {
            Matrix4 temp = RotateMatrix_X(radian);
            this.matrix = (this * temp).matrix;
            return this;
        }
        
        public Matrix4 RotateDegree_X(double degree)
        {
            double radian = Utilities.DegreeToRadian(degree);

            Matrix4 temp = RotateMatrix_X(radian);
            this.matrix = (this * temp).matrix;
            return this;
        }

            // Rotate Y Methods
            //---------------------------------------------------------------
        public static Matrix4 RotateMatrix_Y(double radian)
        {
            Matrix4 rotationMatrix = new Matrix4();

            rotationMatrix[0, 0] = (float)Math.Cos(radian);
            rotationMatrix[0, 2] = (float)Math.Sin(radian);
            rotationMatrix[2, 0] = (float)(Math.Sin(radian) * -1.0);
            rotationMatrix[2, 2] = (float)Math.Cos(radian);

            return rotationMatrix;
        }

        public Matrix4 Rotate_Y(double radian)
        {
            Matrix4 temp = RotateMatrix_Y(radian);
            this.matrix = (this * temp).matrix;
            return this;
        }

        public Matrix4 RotateDegree_Y(double degree)
        {
            double radian = Utilities.DegreeToRadian(degree);

            Matrix4 temp = RotateMatrix_Y(radian);
            this.matrix = (this * temp).matrix;
            return this;
        }

            // Rotate Z Methods
            //---------------------------------------------------------------
        public static Matrix4 RotateMatrix_Z(double radian)
        {
            Matrix4 rotationMatrix = new Matrix4();

            rotationMatrix[0, 0] = (float)Math.Cos(radian);
            rotationMatrix[0, 1] = (float)(Math.Sin(radian) * -1.0);
            rotationMatrix[1, 0] = (float)Math.Sin(radian);
            rotationMatrix[1, 1] = (float)Math.Cos(radian);

            return rotationMatrix;
        }

        public Matrix4 Rotate_Z(double radian)
        {
            Matrix4 temp = RotateMatrix_Z(radian);
            this.matrix = (this * temp).matrix;
            return this;
        }

        public Matrix4 RotateDegree_Z(double degree)
        {
            double radian = Utilities.DegreeToRadian(degree);

            Matrix4 temp = RotateMatrix_Z(radian);
            this.matrix = (this * temp).matrix;
            return this;
        }

            // Rotate Total Methods
            //---------------------------------------------------------------
        public static Matrix4 RotateMatrix(double xRadian, double yRadian, double zRadian)
        {

            Matrix4 xRotation = Matrix4.RotateMatrix_X(xRadian);
            Matrix4 yRotation = Matrix4.RotateMatrix_Y(yRadian);
            Matrix4 zRotation = Matrix4.RotateMatrix_Z(zRadian);

            Matrix4 rotationMatrix = xRotation * yRotation * zRotation;

            return rotationMatrix;
        }

        public Matrix4 Rotate(double xRadian, double yRadian, double zRadian)
        {
            Matrix4 temp = RotateMatrix(xRadian, yRadian, zRadian);
            this.matrix = (this * temp).matrix;
            return this;
        }

        public Matrix4 RotateDegree(double xDegree, double yDegree, double zDegree)
        {

            Matrix4 temp = RotateMatrix(Utilities.DegreeToRadian(xDegree), Utilities.DegreeToRadian(yDegree), Utilities.DegreeToRadian(zDegree));
            this.matrix = (this * temp).matrix;
            return this;
        }

        // Shear Methods
        //------------------------------------------------------------------
        public static Matrix4 ShearMatrix(float Xy, float Xz, float Yx, float Yz, float Zx, float Zy)
        {
            Matrix4 shearMatrix = new Matrix4();

            shearMatrix[0,1] = Xy;
            shearMatrix[0,2] = Xz;
            shearMatrix[1,0] = Yx;
            shearMatrix[1,2] = Yz;
            shearMatrix[2,0] = Zx;
            shearMatrix[2,1] = Zy;

            return shearMatrix;
        }

        public Matrix4 Shear(float Xy, float Xz, float Yx, float Yz, float Zx, float Zy)
        {
            Matrix4 temp = ShearMatrix(Xy, Xz, Yx, Yz, Zx, Zy);
            this.matrix = (this * temp).matrix;
            return this;
        }

    }
}
