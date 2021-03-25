using System;

namespace XModel.DMaths
{
    //! This public class describes a cartesian coordinate entity in
    //! 3D space {X,Y,Z}. This entity is used for algebraic
    //! calculation. This entity can be transformed
    //! with a "Trsf" or a  "GTrsf" from package "gp".
    //! It is used in vectorial computations or for holding this type
    //! of information in data structures.
    public class gp_XYZ
    {

        //! Creates an XYZ object with zero co-ordinates (0,0,0)
        public gp_XYZ() {
            x = 0;y = 0;z = 0;
        }

        //! creates an XYZ with given coordinates
        public gp_XYZ(double X, double Y, double Z) {
            x = X;y = Y;z = Z;
        }

        //! For this XYZ object, assigns
        //! the values X, Y and Z to its three coordinates
        public void SetCoord(double X, double Y, double Z) {
            x = X; y = Y; z = Z;
        }


        //! modifies the coordinate of range Index
        //! Index = 1 => X is modified
        //! Index = 2 => Y is modified
        //! Index = 3 => Z is modified
        //! Raises OutOfRange if Index != {1, 2, 3}.s
        public void SetCoord(int Index, double Xi) {
            switch (Index)
            {
                case 1:
                    x = Xi;
                    break;
                case 2:
                    y = Xi;
                    break;
                case 3:
                    z = Xi;
                    break;
                default:
                    break;
            }
        }

        //! Assigns the given value to the X coordinate
        public void SetX(double X) => x = X;

        //! Assigns the given value to the Y coordinate
        public void SetY(double Y) => y = Y;

        //! Assigns the given value to the Z coordinate
        public void SetZ(double Z) => z = Z;


        //! returns the coordinate of range Index :
        //! Index = 1 => X is returned
        //! Index = 2 => Y is returned
        //! Index = 3 => Z is returned
        //!
        //! Raises OutOfRange if Index != {1, 2, 3}.
        public double Coord(int Index) {
            switch (Index)
            {
                case 1:
                    return x;
                case 2:
                    return y;
                case 3:
                    return z;
                default:
                    return double.NaN;
            }
        }

        public double ChangeCoord(int theIndex) { return Coord(theIndex); }

        public void Coord(double X, double Y, double Z) { x = X; y = Y; z = Z; }

        //! Returns a  ptr to coordinates location.
        //! Is useful for algorithms, but DOES NOT PERFORM
        //! ANY CHECKS!
        public double GetData() { return (x); }

        //! Returns a ptr to coordinates location.
        //! Is useful for algorithms, but DOES NOT PERFORM
        //! ANY CHECKS!
        public double ChangeData() { return (x); }

        //! Returns the X coordinate
        public double X => x;

        //! Returns the Y coordinate
        public double Y => y;

        //! Returns the Z coordinate
        public double Z => z;

        //! computes Sqrt (X*X + Y*Y + Z*Z) where X, Y and Z are the three coordinates of this XYZ object.
        public double Modulus() => Math.Sqrt(x* x + y* y + z* z);

        //! Computes X*X + Y*Y + Z*Z where X, Y and Z are the three coordinates of this XYZ object.
        public double SquareModulus() => (x * x + y * y + z * z);


        //! Returns True if he coordinates of this XYZ object are
        //! equal to the respective coordinates Other,
        //! within the specified tolerance Tolerance. I.e.:
        //! abs(<me>.X() - Other.X()) <= Tolerance and
        //! abs(<me>.Y() - Other.Y()) <= Tolerance and
        //! abs(<me>.Z() - Other.Z()) <= Tolerance.
        bool IsEqual(gp_XYZ Other, double Tolerance) {
            double val;
            val = x - Other.x;
            if (val < 0) val = -val;
            if (val > Tolerance) return false;
            val = y - Other.y;
            if (val < 0) val = -val;
            if (val > Tolerance) return false;
            val = z - Other.z;
            if (val < 0) val = -val;
            if (val > Tolerance) return false;
            return true;
        }


        //! <me>.X() = <me>.X() + Other.X()
        //! <me>.Y() = <me>.Y() + Other.Y()
        //! <me>.Z() = <me>.Z() + Other.Z()
        //! +=
        public void Add(gp_XYZ Other) {
            x += Other.x;
            y += Other.y;
            z += Other.z;
        }
        //public static gp_XYZ operator += (gp_XYZ Other)
        //{
        //    return Add(Other);
        //}


        //! new.X() = <me>.X() + Other.X()
        //! new.Y() = <me>.Y() + Other.Y()
        //! new.Z() = <me>.Z() + Other.Z()
        public static gp_XYZ Added(gp_XYZ theXYZ, gp_XYZ Other) { return new gp_XYZ(theXYZ.X + Other.X, theXYZ.Y + Other.Y, theXYZ.Z + Other.Z); }
        public static gp_XYZ operator +(gp_XYZ theXYZ, gp_XYZ Other)
        {
            return Added(theXYZ, Other);
        }


        //! <me>.X() = <me>.Y() * Other.Z() - <me>.Z() * Other.Y()
        //! <me>.Y() = <me>.Z() * Other.X() - <me>.X() * Other.Z()
        //! <me>.Z() = <me>.X() * Other.Y() - <me>.Y() * Other.X()
        public void Cross(gp_XYZ Right) {
            double Xresult = y * Right.z - z * Right.y;
            double Yresult = z * Right.x - x * Right.z;
            z = x * Right.y - y * Right.x;
            x = Xresult;
            y = Yresult;
        }
        //public static void operator ^= (gp_XYZ Right)
        //{
        //    Cross(Right);
        //}


        //! new.X() = <me>.Y() * Other.Z() - <me>.Z() * Other.Y()
        //! new.Y() = <me>.Z() * Other.X() - <me>.X() * Other.Z()
        //! new.Z() = <me>.X() * Other.Y() - <me>.Y() * Other.X()
        public static gp_XYZ Crossed(gp_XYZ theXYZ, gp_XYZ Right) {
            return  new gp_XYZ(theXYZ.Y * Right.Z - theXYZ.Z * Right.Y,
                               theXYZ.Z * Right.X - theXYZ.X * Right.Z,
                               theXYZ.X * Right.Y - theXYZ.Y * Right.X);
        }
        public static gp_XYZ operator ^(gp_XYZ theXYZ, gp_XYZ Right)
        {
            return Crossed(theXYZ, Right);
        }


        //! Computes the magnitude of the cross product between <me> and
        //! Right. Returns || <me> ^ Right ||
        public double CrossMagnitude(gp_XYZ Right) {
            double Xresult = y * Right.Z - z * Right.Y;
            double Yresult = z * Right.X - x * Right.Z;
            double Zresult = x * Right.Y - y * Right.X;
            return Math.Sqrt(Xresult * Xresult + Yresult * Yresult + Zresult * Zresult);
        }


        //! Computes the square magnitude of the cross product between <me> and
        //! Right. Returns || <me> ^ Right ||**2
        public double CrossSquareMagnitude(gp_XYZ Right) {
            double Xresult = y * Right.Z - z * Right.Y;
            double Yresult = z * Right.X - x * Right.Z;
            double Zresult = x * Right.Y - y * Right.X;
            return Xresult * Xresult + Yresult * Yresult + Zresult * Zresult;
        }

        //! Triple vector product
        //! Computes <me> = <me>.Cross(Coord1.Cross(Coord2))
        public void CrossCross(gp_XYZ Coord1, gp_XYZ Coord2) {
            double Xresult = y * (Coord1.X * Coord2.y - Coord1.y * Coord2.x) - z * (Coord1.z * Coord2.x - Coord1.x * Coord2.z);
            double Yresult = z * (Coord1.Y * Coord2.z - Coord1.z * Coord2.y) - x * (Coord1.x * Coord2.y - Coord1.y * Coord2.x);
            z = x * (Coord1.z * Coord2.x - Coord1.x * Coord2.z) - y * (Coord1.y * Coord2.z - Coord1.z * Coord2.y);
            x = Xresult;
            y = Yresult;
        }

        //! Triple vector product
        //! computes New = <me>.Cross(Coord1.Cross(Coord2))
        public gp_XYZ CrossCrossed(gp_XYZ Coord1, gp_XYZ Coord2) {
            gp_XYZ Coord0 = this;
            Coord0.CrossCross(Coord1, Coord2);
            return Coord0;
        }

        //! divides <me> by a real.
        public void Divide(double Scalar) {
            x /= Scalar;
            y /= Scalar;
            z /= Scalar;
        }
        //public static gp_XYZ operator /= (gp_XYZ theXYZ, double Scalar) => Divide(theXYZ, Scalar);

        //! divides <me> by a real.
        public static gp_XYZ Divided(gp_XYZ theXYZ, double Scalar) {
            return new gp_XYZ(theXYZ.X / Scalar, theXYZ.Y / Scalar, theXYZ.Z / Scalar);
        }
        public static gp_XYZ operator /(gp_XYZ theXYZ, double Scalar)
        {
            return Divided(theXYZ, Scalar);
        }

        //! computes the scalar product between <me> and Other
        public static double Dot(gp_XYZ theXYZ, gp_XYZ Other)  => (theXYZ.X * Other.X + theXYZ.Y * Other.Y + theXYZ.Z * Other.Z);
        public static double operator *(gp_XYZ theXYZ, gp_XYZ Other)
        {
            return Dot(theXYZ,Other);
        }

        //! computes the triple scalar product
        public double DotCross(gp_XYZ Coord1, gp_XYZ Coord2) {
            double Xresult = Coord1.Y * Coord2.Z - Coord1.Z * Coord2.Y;
            double Yresult = Coord1.Z * Coord2.X - Coord1.X * Coord2.Z;
            double Zresult = Coord1.X * Coord2.Y - Coord1.Y * Coord2.X;
            return (x * Xresult + y * Yresult + z * Zresult);
        }


        ////! <me>.X() = <me>.X() * Scalar;
        ////! <me>.Y() = <me>.Y() * Scalar;
        ////! <me>.Z() = <me>.Z() * Scalar;
        public void Multiply(double Scalar) {
            x *= Scalar;
            y *= Scalar;
            z *= Scalar;
        }
        //public static void operator *= (double Scalar)
        //{
        //    Multiply(Scalar);
        //}


        ////! <me>.X() = <me>.X() * Other.X(){ }
        ////! <me>.Y() = <me>.Y() * Other.Y(){ }
        ////! <me>.Z() = <me>.Z() * Other.Z(){ }
        public void Multiply(gp_XYZ Other) {
            x *= Other.x;
            y *= Other.y;
            z *= Other.z;
        }
        //public static void operator *= (gp_XYZ Other)
        //{
        //    Multiply(Other);
        //}

        ////! <me> = Matrix * <me>
        public void Multiply(gp_Mat Matrix) {
            double Xresult = Matrix.matrix[0][0] * x + Matrix.matrix[0][1] * y + Matrix.matrix[0][2] * z;
            double Yresult = Matrix.matrix[1][0] * x + Matrix.matrix[1][1] * y + Matrix.matrix[1][2] * z;
            z = Matrix.matrix[2][0] * x + Matrix.matrix[2][1] * y + Matrix.matrix[2][2] * z;
            x = Xresult;
            y = Yresult;
        }
        //public static gp_Mat operator *= (gp_Mat Matrix)
        //{
        //    return Multiply(Matrix);
        //}


        //! New.X() = <me>.X() * Scalar;
        //! New.Y() = <me>.Y() * Scalar;
        //! New.Z() = <me>.Z() * Scalar;
        public static gp_XYZ Multiplied(gp_XYZ theXYZ, double Scalar) {
            return new gp_XYZ(theXYZ.X * Scalar, theXYZ.Y * Scalar, theXYZ.Z * Scalar);
        }
        public static gp_XYZ operator *(gp_XYZ theXYZ, double Scalar)
        {
            return Multiplied(theXYZ, Scalar);
        }


        //! new.X() = <me>.X() * Other.X(){ }
        //! new.Y() = <me>.Y() * Other.Y(){ }
        //! new.Z() = <me>.Z() * Other.Z(){ }
        public gp_XYZ Multiplied(gp_XYZ Other) {
            return new gp_XYZ(x * Other.X, y * Other.Y, z * Other.Z);
        }

        //! New = Matrix * <me>
        public static gp_XYZ Multiplied(gp_XYZ theXYZ, gp_Mat Matrix) {
            return new gp_XYZ(Matrix.matrix[0][0] * theXYZ.X + Matrix.matrix[0][1] * theXYZ.Y + Matrix.matrix[0][2] * theXYZ.Z,
                         Matrix.matrix[1][0] * theXYZ.X + Matrix.matrix[1][1] * theXYZ.Y + Matrix.matrix[1][2] * theXYZ.Z,
                         Matrix.matrix[2][0] * theXYZ.X + Matrix.matrix[2][1] * theXYZ.Y + Matrix.matrix[2][2] * theXYZ.Z);
        }
        public static gp_XYZ operator *(gp_XYZ theXYZ,gp_Mat Matrix)
        {
            return Multiplied(theXYZ, Matrix);
        }


        //! <me>.X() = <me>.X()/ <me>.Modulus()
        //! <me>.Y() = <me>.Y()/ <me>.Modulus()
        //! <me>.Z() = <me>.Z()/ <me>.Modulus()
        //! Raised if <me>.Modulus() <= Resolution from gp
        public void Normalize() {
            double D = Modulus();
            if (D <= gp.Resolution())
                throw new DivideByZeroException("gp_XYZ.Normalize() - vector has zero norm");
            x = x / D; y = y / D; z = z / D;
        }


        //! New.X() = <me>.X()/ <me>.Modulus()
        //! New.Y() = <me>.Y()/ <me>.Modulus()
        //! New.Z() = <me>.Z()/ <me>.Modulus()
        //! Raised if <me>.Modulus() <= Resolution from gp
        public gp_XYZ Normalized() {
            double D = Modulus();
            if(D <= gp.Resolution())
                throw new DivideByZeroException("gp_XYZ.Normalize() - vector has zero norm");
            return  new gp_XYZ(x / D, y / D, z / D);
        }


        //! <me>.X() = -<me>.X()
        //! <me>.Y() = -<me>.Y()
        //! <me>.Z() = -<me>.Z()
        public void Reverse() {
            x = -x;
            y = -y;
            z = -z;
        }


        //! New.X() = -<me>.X()
        //! New.Y() = -<me>.Y()
        //! New.Z() = -<me>.Z()
        public gp_XYZ Reversed() => new gp_XYZ(-x, -y, -z);


        //! <me>.X() = <me>.X() - Other.X()
        //! <me>.Y() = <me>.Y() - Other.Y()
        //! <me>.Z() = <me>.Z() - Other.Z()
        public void Subtract(gp_XYZ Right) {
            x -= Right.X;
            y -= Right.Y;
            z -= Right.Z;
        }
        //public static gp_XYZ operator -= (gp_XYZ theXYZ, gp_XYZ Right)
        //{
        //    return Subtract(theXYZ, Right);
        //}


        //! new.X() = <me>.X() - Other.X()
        //! new.Y() = <me>.Y() - Other.Y()
        //! new.Z() = <me>.Z() - Other.Z()
        public static gp_XYZ Subtracted(gp_XYZ theXYZ, gp_XYZ Right)=> new gp_XYZ(theXYZ.X - Right.X, theXYZ.Y - Right.Y, theXYZ.Z - Right.Z);
        public static gp_XYZ operator -(gp_XYZ theXYZ, gp_XYZ Right)
        {
            return Subtracted(theXYZ, Right);
        }


        //! <me> is set to the following linear form :
        //! A1 * XYZ1 + A2 * XYZ2 + A3 * XYZ3 + XYZ4
        public void SetLinearForm(double A1, gp_XYZ XYZ1, double A2, gp_XYZ XYZ2, double A3, gp_XYZ XYZ3, gp_XYZ XYZ4) {
            x = A1 * XYZ1.X + A2 * XYZ2.X + A3 * XYZ3.X + XYZ4.X;
            y = A1 * XYZ1.Y + A2 * XYZ2.Y + A3 * XYZ3.Y + XYZ4.Y;
            z = A1 * XYZ1.Z + A2 * XYZ2.Z + A3 * XYZ3.Z + XYZ4.Z;
        }


        //! <me> is set to the following linear form :
        //! A1 * XYZ1 + A2 * XYZ2 + A3 * XYZ3
        public void SetLinearForm(double A1, gp_XYZ XYZ1, double A2, gp_XYZ XYZ2, double A3, gp_XYZ XYZ3) {
            x = A1 * XYZ1.X + A2 * XYZ2.X + A3 * XYZ3.X;
            y = A1 * XYZ1.Y + A2 * XYZ2.Y + A3 * XYZ3.Y;
            z = A1 * XYZ1.Z + A2 * XYZ2.Z + A3 * XYZ3.Z;
        }


        //! <me> is set to the following linear form :
        //! A1 * XYZ1 + A2 * XYZ2 + XYZ3
        public void SetLinearForm(double A1, gp_XYZ XYZ1, double A2, gp_XYZ XYZ2, gp_XYZ XYZ3) {
            x = A1 * XYZ1.X + A2 * XYZ2.X + XYZ3.X;
            y = A1 * XYZ1.Y + A2 * XYZ2.Y + XYZ3.Y;
            z = A1 * XYZ1.Z + A2 * XYZ2.Z + XYZ3.Z;
        }


        //! <me> is set to the following linear form :
        //! A1 * XYZ1 + A2 * XYZ2
        public void SetLinearForm(double A1, gp_XYZ XYZ1, double A2, gp_XYZ XYZ2) {
            x = A1 * XYZ1.X + A2 * XYZ2.X;
            y = A1 * XYZ1.Y + A2 * XYZ2.Y;
            z = A1 * XYZ1.Z + A2 * XYZ2.Z;
        }


        //! <me> is set to the following linear form :
        //! A1 * XYZ1 + XYZ2
        public void SetLinearForm(double A1, gp_XYZ XYZ1, gp_XYZ XYZ2) {
            x = A1 * XYZ1.X + XYZ2.X;
            y = A1 * XYZ1.Y + XYZ2.Y;
            z = A1 * XYZ1.Z + XYZ2.Z;
        }


        //! <me> is set to the following linear form :
        //! XYZ1 + XYZ2
        public void SetLinearForm(gp_XYZ XYZ1, gp_XYZ XYZ2) {
            x = XYZ1.X + XYZ2.X;
            y = XYZ1.Y + XYZ2.Y;
            z = XYZ1.Z + XYZ2.Z;
        }


        ////! Dumps the content of me into the stream
        //public void DumpJson(Standard_OStream theOStream, int theDepth = -1) { }

        ////! Inits the content of me from the stream
        //bool InitFromJson(Standard_SStream theSStream, int theStreamPos) { }

        private double x;
        private double y;
        private double z;
    }
    //#include <gp_XYZ.lxx>
}
