
using System;

namespace XModel.DMaths
{
    //! Describes a three column, three row matrix. This sort of
    //! object is used in various vectorial or matrix computations.
    public class gp_Mat
    {
        //! creates  a matrix with null coefficients.
        public gp_Mat()
        {
            matrix = new double[3][] { new double[3] { 0.0, 0.0, 0.0 }, new double[3] { 0.0, 0.0, 0.0 }, new double[3] { 0.0, 0.0, 0.0 } };
        }

        public gp_Mat(double a11, double a12, double a13, double a21, double a22, double a23, double a31, double a32, double a33)
        {
            matrix = new double[3][] { new double[3] { a11, a12, a13 }, new double[3] { a21, a22, a23 }, new double[3] { a31, a32, a33 } };
        }

        //! Creates a matrix.
        //! Col1, Col2, Col3 are the 3 columns of the matrix.
        public gp_Mat(gp_XYZ Col1, gp_XYZ Col2, gp_XYZ Col3)
        {
            matrix[0][0] = Col1.X; matrix[1][0] = Col1.Y; matrix[2][0] = Col1.Z;
            matrix[0][1] = Col2.X; matrix[1][1] = Col2.Y; matrix[2][1] = Col2.Z;
            matrix[0][2] = Col3.X; matrix[1][2] = Col3.Y; matrix[2][2] = Col3.Z;
        }

        //! Assigns the three coordinates of Value to the column of index
        //! Col of this matrix.
        //! Raises OutOfRange if Col < 1 or Col > 3.
        public void SetCol(int Col, gp_XYZ Value)
        {
            if (Col < 1 || Col > 3)
                throw new System.Exception("");
            if (Col == 1)
            {
                matrix[0][0] = Value.X; matrix[1][0] = Value.Y; matrix[2][0] = Value.Z;
            }
            else if (Col == 2)
            {
                matrix[0][1] = Value.X; matrix[1][1] = Value.Y; matrix[2][1] = Value.Z;
            }
            else
            {
                matrix[0][2] = Value.X; matrix[1][2] = Value.Y; matrix[2][2] = Value.Z;
            }
        }

        //! Assigns the number triples Col1, Col2, Col3 to the three
        //! columns of this matrix.
        public void SetCols(gp_XYZ Col1, gp_XYZ Col2, gp_XYZ Col3)
        {
            matrix[0][0] = Col1.X; matrix[1][0] = Col1.Y; matrix[2][0] = Col1.Z;
            matrix[0][1] = Col2.X; matrix[1][1] = Col2.Y; matrix[2][1] = Col2.Z;
            matrix[0][2] = Col3.X; matrix[1][2] = Col3.Y; matrix[2][2] = Col3.Z;
        }


        //! Modifies the matrix  M so that applying it to any number
        //! triple (X, Y, Z) produces the same result as the cross
        //! product of Ref and the number triple (X, Y, Z):
        //! i.e.: M * {X,Y,Z}t = Ref.Cross({X, Y ,Z})
        //! this matrix is anti symmetric. To apply this matrix to the
        //! triplet  {XYZ} is the same as to do the cross product between the
        //! triplet Ref and the triplet {XYZ}.
        //! Note: this matrix is anti-symmetric.
        public void SetCross(gp_XYZ Ref)
        {
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            double X = Ref.X;
            double Y = Ref.Y;
            double Z = Ref.Z;
            matrix[0][0] = matrix[1][1] = matrix[2][2] = 0.0;
            matrix[0][1] = -Z;
            matrix[0][2] = Y;
            matrix[1][2] = -X;
            matrix[1][0] = Z;
            matrix[2][0] = -Y;
            matrix[2][1] = X;
        }


        //! Modifies the main diagonal of the matrix.
        //! <me>.Value (1, 1) = X1
        //! <me>.Value (2, 2) = X2
        //! <me>.Value (3, 3) = X3
        //! The other coefficients of the matrix are not modified.
        public void SetDiagonal(double X1, double X2, double X3)
        {
            matrix[0][0] = X1; matrix[1][1] = X2; matrix[2][2] = X3;
        }


        //! Modifies this matrix so that applying it to any number
        //! triple (X, Y, Z) produces the same result as the scalar
        //! product of Ref and the number triple (X, Y, Z):
        //! this * (X,Y,Z) = Ref.(X,Y,Z)
        //! Note: this matrix is symmetric.
        public void SetDot(gp_XYZ Ref)
        {
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            double X = Ref.X;
            double Y = Ref.Y;
            double Z = Ref.Z;
            matrix[0][0] = X * X;
            matrix[1][1] = Y * Y;
            matrix[2][2] = Z * Z;
            matrix[0][1] = X * Y;
            matrix[0][2] = X * Z;
            matrix[1][2] = Y * Z;
            matrix[1][0] = matrix[0][1];
            matrix[2][0] = matrix[0][2];
            matrix[2][1] = matrix[1][2];

        }

        //! Modifies this matrix so that it represents the Identity matrix.
        public void SetIdentity()
        {
            matrix[0][0] = matrix[1][1] = matrix[2][2] = 1.0;
            matrix[0][1] = matrix[0][2] = matrix[1][0] = matrix[1][2] = matrix[2][0] = matrix[2][1] = 0.0;
        }


        //! Modifies this matrix so that it represents a rotation. Ang is the angular value in
        //! radians and the XYZ axis gives the direction of the
        //! rotation.
        //! Raises ConstructionError if XYZ.Modulus() <= Resolution()
        public void SetRotation(gp_XYZ Axis, double Ang)
        {
            //    Rot = I + sin(Ang) * M + (1. - cos(Ang)) * M*M
            //    avec  M . XYZ = Axis ^ XYZ

            //  const Standard_Address M = (Standard_Address)&(matrix[0][0]);
            gp_XYZ V = Axis.Normalized();
            SetCross(V);
            Multiply(Math.Sin(Ang));
            gp_Mat Temp = new gp_Mat();
            Temp.SetScale(1.0);
            Add(Temp);
            double A = V.X;
            double B = V.Y;
            double C = V.Z;
            Temp.SetRow(1, new gp_XYZ(-C * C - B * B, A * B, A * C));
            Temp.SetRow(2, new gp_XYZ(A * B, -A * A - C * C, B * C));
            Temp.SetRow(3, new gp_XYZ(A * C, B * C, -A * A - B * B));
            Temp.Multiply(1.0 - Math.Cos(Ang));
            Add(Temp);
        }

        //! Assigns the three coordinates of Value to the row of index
        //! Row of this matrix. Raises OutOfRange if Row < 1 or Row > 3.
        public void SetRow(int Row, gp_XYZ Value)
        {
            if (Row < 1 || Row > 3)
                throw new Exception("");
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            if (Row == 1)
            {
                M00 = Value.X; M01 = Value.Y; M02 = Value.Z;
            }
            else if (Row == 2)
            {
                M10 = Value.X; M11 = Value.Y; M12 = Value.Z;
            }
            else
            {
                M20 = Value.X; M21 = Value.Y; M22 = Value.Z;
            }
        }

        //! Assigns the number triples Row1, Row2, Row3 to the three
        //! rows of this matrix.
        public void SetRows(gp_XYZ Row1, gp_XYZ Row2, gp_XYZ Row3)
        {
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            M00 = Row1.X; M01 = Row1.Y; M02 = Row1.Z;
            M10 = Row2.X; M11 = Row2.Y; M12 = Row2.Z;
            M20 = Row3.X; M21 = Row3.Y; M22 = Row3.Z;
        }


        //! Modifies the the matrix so that it represents
        //! a scaling transformation, where S is the scale factor. :
        //! | S    0.0  0.0 |
        //! <me> =  | 0.0   S   0.0 |
        //! | 0.0  0.0   S  |
        public void SetScale(double S)
        {
            M00 = M11 = M22 = S;
            M01 = M02 = M10 = M12 = M20 = M21 = 0.0;
        }

        //! Assigns <Value> to the coefficient of row Row, column Col of   this matrix.
        //! Raises OutOfRange if Row < 1 or Row > 3 or Col < 1 or Col > 3
        public void SetValue(int Row, int Col, double Value)
        {
            if (Row < 1 || Row > 3 || Col < 1 || Col > 3)
                throw new Exception("");
            matrix[Row - 1][Col - 1] = Value;
        }

        //! Returns the column of Col index.
        //! Raises OutOfRange if Col < 1 or Col > 3
        public gp_XYZ Column(int Col)
        {
            if (Col < 1 || Col > 3) throw new Exception("gp_Mat::Column() - wrong index");
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            if (Col == 1) return new gp_XYZ(M00, M10, M20);
            if (Col == 2) return new gp_XYZ(M01, M11, M21);
            return new gp_XYZ(M02, M12, M22);
        }

        //! Computes the determinant of the matrix.
        public double Determinant()
        {
            return M00 * (M11 * M22 - M21 * M12) - M01 * (M10 * M22 - M20 * M12) + M02 * (M10 * M21 - M20 * M11);
        }

        //! Returns the main diagonal of the matrix.
        public gp_XYZ Diagonal()
        {
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            return new gp_XYZ(M00, M11, M22);
        }

        //! returns the row of Row index.
        //! Raises OutOfRange if Row < 1 or Row > 3
        public gp_XYZ Row(int Row)
        {
            if (Row < 1 || Row > 3) throw new Exception("gp_Mat::Row() - wrong index");
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            if (Row == 1) return new gp_XYZ(M00, M01, M02);
            if (Row == 2) return new gp_XYZ(M10, M11, M12);
            return new gp_XYZ(M20, M21, M22);
        }

        //! Returns the coefficient of range (Row, Col)
        //! Raises OutOfRange if Row < 1 or Row > 3 or Col < 1 or Col > 3
        public double Value(int Row, int Col)
        {
            if (Row < 1 || Row > 3 || Col < 1 || Col > 3) throw new Exception(" ");
            return matrix[Row - 1][Col - 1];
        }
        //public static double operator() (int Row, int Col) 
        //{
        //  return Value(Row, Col);
        //}

        //! Returns the coefficient of range (Row, Col)
        //! Raises OutOfRange if Row < 1 or Row > 3 or Col < 1 or Col > 3
        public double ChangeValue(int Row, int Col)
        {
            if (Row < 1 || Row > 3 || Col < 1 || Col > 3) throw new Exception(" ");
            return matrix[Row - 1][Col - 1];
        }
        //public double operator() (int Row, int Col)
        //{
        //  return ChangeValue(Row, Col);
        //}


        //! The Gauss LU decomposition is used to invert the matrix
        //! (see Math package) so the matrix is considered as singular if
        //! the largest pivot found is lower or equal to Resolution from gp.
        public bool IsSingular()
        {
            // Pour etre sur que Gauss va fonctionner, il faut faire Gauss ...
            double val = Determinant();
            if (val < 0) val = -val;
            return val <= gp.Resolution();
        }

        public void Add(gp_Mat Other)
        {
            M00 = M00 + Other.M00;
            M01 = M01 + Other.M01;
            M02 = M02 + Other.M02;
            M10 = M10 + Other.M10;
            M11 = M11 + Other.M11;
            M12 = M12 + Other.M12;
            M20 = M20 + Other.M20;
            M21 = M21 + Other.M21;
            M22 = M22 + Other.M22;
        }
        //public void operator += (gp_Mat Other)
        //{
        //    Add(Other);
        //}

        //! Computes the sum of this matrix and
        //! the matrix Other for each coefficient of the matrix :
        //! <me>.Coef(i,j) + <Other>.Coef(i,j)
        public static gp_Mat Added(gp_Mat theMat, gp_Mat Other)
        {
            gp_Mat NewMat = new gp_Mat();
            NewMat.M00 = theMat.M00 + Other.M00;
            NewMat.M01 = theMat.M01 + Other.M01;
            NewMat.M02 = theMat.M02 + Other.M02;
            NewMat.M10 = theMat.M10 + Other.M10;
            NewMat.M11 = theMat.M11 + Other.M11;
            NewMat.M12 = theMat.M12 + Other.M12;
            NewMat.M20 = theMat.M20 + Other.M20;
            NewMat.M21 = theMat.M21 + Other.M21;
            NewMat.M22 = theMat.M22 + Other.M22;
            return NewMat;
        }
        public static gp_Mat operator +(gp_Mat theMat, gp_Mat Other)
        {
            return Added(theMat, Other);
        }

        public void Divide(double Scalar)
        {
            double val = Scalar;
            if (val < 0) val = -val;
            if (val <= gp.Resolution()) throw new Exception("gp_Mat : Divide by 0");
            double UnSurScalar = 1.0 / Scalar;
            M00 *= UnSurScalar;
            M01 *= UnSurScalar;
            M02 *= UnSurScalar;
            M10 *= UnSurScalar;
            M11 *= UnSurScalar;
            M12 *= UnSurScalar;
            M20 *= UnSurScalar;
            M21 *= UnSurScalar;
            M22 *= UnSurScalar;
        }
        //public void operator /= (double Scalar)
        //{
        //    Divide(Scalar);
        //}

        //! Divides all the coefficients of the matrix by Scalar
        public static gp_Mat Divided(gp_Mat theMat, double Scalar)
        {
            double val = Scalar;
            if (val < 0) val = -val;
            if (val <= gp.Resolution()) throw new Exception("gp_Mat : Divide by 0");
            gp_Mat NewMat = new gp_Mat();
            double UnSurScalar = 1.0 / Scalar;
            NewMat.M00 = theMat.M00 * UnSurScalar;
            NewMat.M01 = theMat.M01 * UnSurScalar;
            NewMat.M02 = theMat.M02 * UnSurScalar;
            NewMat.M10 = theMat.M10 * UnSurScalar;
            NewMat.M11 = theMat.M11 * UnSurScalar;
            NewMat.M12 = theMat.M12 * UnSurScalar;
            NewMat.M20 = theMat.M20 * UnSurScalar;
            NewMat.M21 = theMat.M21 * UnSurScalar;
            NewMat.M22 = theMat.M22 * UnSurScalar;
            return NewMat;
        }
        public static gp_Mat operator /(gp_Mat theMat, double Scalar)
        {
            return Divided(theMat, Scalar);
        }

        public void Invert()
        {
            double[][] N = new double[3][] { new double[3] { 0.0, 0.0, 0.0 }, new double[3] { 0.0, 0.0, 0.0 }, new double[3] { 0.0, 0.0, 0.0 } };
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            //const Standard_Address N = (Standard_Address) & (new_array[0][0]);

            //
            // calcul de  la transposee de la commatrice
            //
            N[0][0] = M11 * M22 - M12 * M21;
            N[1][0] = -(M10 * M22 - M20 * M12);
            N[2][0] = M10 * M21 - M20 * M11;
            N[0][1] = -(M01 * M22 - M21 * M02);
            N[1][1] = M00 * M22 - M20 * M02;
            N[2][1] = -(M00 * M21 - M20 * M01);
            N[0][2] = M01 * M12 - M11 * M02;
            N[1][2] = -(M00 * M12 - M10 * M02);
            N[2][2] = M00 * M11 - M01 * M10;
            double det = M00 * N[0][0] + M01 * N[1][0] + M02 * N[2][0];
            double val = det;
            if (val < 0) val = -val;
            if (val <= gp.Resolution()) throw new Exception("gp_Mat::Invert() - matrix has zero determinant");
            det = 1.0e0 / det;
            M00 = N[0][0];
            M10 = N[1][0];
            M20 = N[2][0];
            M01 = N[0][1];
            M11 = N[1][1];
            M21 = N[2][1];
            M02 = N[0][2];
            M12 = N[1][2];
            M22 = N[2][2];
            Multiply(det);
        }


        //! Inverses the matrix and raises if the matrix is singular.
        //! -   Invert assigns the result to this matrix, while
        //! -   Inverted creates a new one.
        //! Warning
        //! The Gauss LU decomposition is used to invert the matrix.
        //! Consequently, the matrix is considered as singular if the
        //! largest pivot found is less than or equal to gp::Resolution().
        //! Exceptions
        //! Standard_ConstructionError if this matrix is singular,
        //! and therefore cannot be inverted.
        public gp_Mat Inverted()
        {
            gp_Mat NewMat = new gp_Mat();
            //const Standard_Address M = (Standard_Address) & (matrix[0][0]);
            //const Standard_Address N = (Standard_Address) & (NewMat.matrix[0][0]);
            //
            // calcul de  la transposee de la commatrice
            //
            NewMat.M00 = M11 * M22 - M12 * M21;
            NewMat.M10 = -(M10 * M22 - M20 * M12);
            NewMat.M20 = M10 * M21 - M20 * M11;
            NewMat.M01 = -(M01 * M22 - M21 * M02);
            NewMat.M11 = M00 * M22 - M20 * M02;
            NewMat.M21 = -(M00 * M21 - M20 * M01);
            NewMat.M02 = M01 * M12 - M11 * M02;
            NewMat.M12 = -(M00 * M12 - M10 * M02);
            NewMat.M22 = M00 * M11 - M01 * M10;
            double det = M00 * NewMat.M00 + M01 * NewMat.M10 + M02 * NewMat.M20;
            double val = det;
            if (val < 0) val = -val;
            if (val <= gp.Resolution()) throw new Exception("gp_Mat::Inverted() - matrix has zero determinant");
            det = 1.0e0 / det;
            NewMat.Multiply(det);
            return NewMat;
        }


        //! Computes  the product of two matrices <me> * <Other>
        public static gp_Mat Multiplied(gp_Mat theMat, gp_Mat Other)
        {
            gp_Mat NewMat = theMat;
            NewMat.Multiply(Other);
            return NewMat;
        }
        public static gp_Mat operator *(gp_Mat theMat, gp_Mat Other)
        {
            return Multiplied(theMat, Other);
        }

        //! Computes the product of two matrices <me> = <Other> * <me>.
        public void Multiply(gp_Mat Other)
        {
            double T00, T01, T02, T10, T11, T12, T20, T21, T22;
            T00 = M00 * Other.M00 + M01 * Other.M10 + M02 * Other.M20;
            T01 = M00 * Other.M01 + M01 * Other.M11 + M02 * Other.M21;
            T02 = M00 * Other.M02 + M01 * Other.M12 + M02 * Other.M22;
            T10 = M10 * Other.M00 + M11 * Other.M10 + M12 * Other.M20;
            T11 = M10 * Other.M01 + M11 * Other.M11 + M12 * Other.M21;
            T12 = M10 * Other.M02 + M11 * Other.M12 + M12 * Other.M22;
            T20 = M20 * Other.M00 + M21 * Other.M10 + M22 * Other.M20;
            T21 = M20 * Other.M01 + M21 * Other.M11 + M22 * Other.M21;
            T22 = M20 * Other.M02 + M21 * Other.M12 + M22 * Other.M22;
            M00 = T00;
            M01 = T01;
            M02 = T02;
            M10 = T10;
            M11 = T11;
            M12 = T12;
            M20 = T20;
            M21 = T21;
            M22 = T22;

        }
        //public void operator *= (gp_Mat Other)
        //{
        //    Multiply(Other);
        //}

        public void PreMultiply(gp_Mat Other)
        {
            double T00, T01, T02, T10, T11, T12, T20, T21, T22;
            T00 = Other.M00 * M00 + Other.M01 * M10 + Other.M02 * M20;
            T01 = Other.M00 * M01 + Other.M01 * M11 + Other.M02 * M21;
            T02 = Other.M00 * M02 + Other.M01 * M12 + Other.M02 * M22;
            T10 = Other.M10 * M00 + Other.M11 * M10 + Other.M12 * M20;
            T11 = Other.M10 * M01 + Other.M11 * M11 + Other.M12 * M21;
            T12 = Other.M10 * M02 + Other.M11 * M12 + Other.M12 * M22;
            T20 = Other.M20 * M00 + Other.M21 * M10 + Other.M22 * M20;
            T21 = Other.M20 * M01 + Other.M21 * M11 + Other.M22 * M21;
            T22 = Other.M20 * M02 + Other.M21 * M12 + Other.M22 * M22;
            M00 = T00;
            M01 = T01;
            M02 = T02;
            M10 = T10;
            M11 = T11;
            M12 = T12;
            M20 = T20;
            M21 = T21;
            M22 = T22;
        }

        public static gp_Mat Multiplied(gp_Mat theMat, double Scalar) {
            gp_Mat NewMat = new gp_Mat();
            NewMat.M00 = Scalar * theMat.M00;
            NewMat.M01 = Scalar * theMat.M01;
            NewMat.M02 = Scalar * theMat.M02;
            NewMat.M10 = Scalar * theMat.M10;
            NewMat.M11 = Scalar * theMat.M11;
            NewMat.M12 = Scalar * theMat.M12;
            NewMat.M20 = Scalar * theMat.M20;
            NewMat.M21 = Scalar * theMat.M21;
            NewMat.M22 = Scalar * theMat.M22;
            return NewMat;
        }
        public static gp_Mat operator *(gp_Mat theMat, double Scalar)
        {
            return Multiplied(theMat, Scalar);
        }


        //! Multiplies all the coefficients of the matrix by Scalar
        public void Multiply(double Scalar) {
            M00 *= Scalar;
            M01 *= Scalar;
            M02 *= Scalar;
            M10 *= Scalar;
            M11 *= Scalar;
            M12 *= Scalar;
            M20 *= Scalar;
            M21 *= Scalar;
            M22 *= Scalar;
        }
        //public void operator *= (double Scalar)
        //{
        //    Multiply(Scalar);
        //}

        public void Power(int N) { }


        //! Computes <me> = <me> * <me> * .......* <me>,   N time.
        //! if N = 0 <me> = Identity
        //! if N < 0 <me> = <me>.Invert() *...........* <me>.Invert().
        //! If N < 0 an exception will be raised if the matrix is not
        //! inversible
        public gp_Mat Powered(int N) {
            gp_Mat MatN = this;
            MatN.Power(N);
            return MatN;
        }

        public void Subtract(gp_Mat Other) {
            M00 -= Other.M00;
            M01 -= Other.M01;
            M02 -= Other.M02;
            M10 -= Other.M10;
            M11 -= Other.M11;
            M12 -= Other.M12;
            M20 -= Other.M20;
            M21 -= Other.M21;
            M22 -= Other.M22;
        }
        //public void operator -= (gp_Mat Other)
        //{
        //    Subtract(Other);
        //}


        //! cOmputes for each coefficient of the matrix :
        //! <me>.Coef(i,j) - <Other>.Coef(i,j)
        public static gp_Mat Subtracted(gp_Mat theMat, gp_Mat Other) {
            gp_Mat NewMat = new gp_Mat();
            NewMat.M00 = theMat.M00 - Other.M00;
            NewMat.M01 = theMat.M01 - Other.M01;
            NewMat.M02 = theMat.M02 - Other.M02;
            NewMat.M10 = theMat.M10 - Other.M10;
            NewMat.M11 = theMat.M11 - Other.M11;
            NewMat.M12 = theMat.M12 - Other.M12;
            NewMat.M20 = theMat.M20 - Other.M20;
            NewMat.M21 = theMat.M21 - Other.M21;
            NewMat.M22 = theMat.M22 - Other.M22;
            return NewMat;
        }
        public static gp_Mat operator -(gp_Mat theMat, gp_Mat Other)
        {
            return Subtracted(theMat, Other);
        }

        public void Transpose() {
            double Temp;
            Temp = M01;
            M01 = M10;
            M10 = Temp;
            Temp = M02;
            M02 = M20;
            M20 = Temp;
            Temp = M12;
            M12 = M21;
            M21 = Temp;
        }


        //! Transposes the matrix. A(j, i) -> A (i, j)
        public gp_Mat Transposed() {
            gp_Mat NewMat = this;
            NewMat.Transpose();
            return NewMat;
        }

        ////! Dumps the content of me into the stream
        //public void DumpJson(Standard_OStream theOStream, int theDepth = -1){ }

        public double[][] matrix { get; set; }

        public double M00 { get { return matrix[0][0]; } set { matrix[0][0] = value; } }
        public double M01 { get { return matrix[0][1]; } set { matrix[0][1] = value; } }
        public double M02 { get { return matrix[0][2]; } set { matrix[0][2] = value; } }
        public double M10 { get { return matrix[1][0]; } set { matrix[1][0] = value; } }
        public double M11 { get { return matrix[1][1]; } set { matrix[1][1] = value; } }
        public double M12 { get { return matrix[1][2]; } set { matrix[1][2] = value; } }
        public double M20 { get { return matrix[2][0]; } set { matrix[2][0] = value; } }
        public double M21 { get { return matrix[2][1]; } set { matrix[2][1] = value; } }
        public double M22 { get { return matrix[2][2]; } set { matrix[2][2] = value; } }

    }
    //#include <gp_Mat.lxx>
}
