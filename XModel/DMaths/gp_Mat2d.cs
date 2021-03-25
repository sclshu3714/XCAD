// Copyright (c) 1991-1999 Matra Datavision
// Copyright (c) 1999-2014 OPEN CASCADE SAS
//
// This file is part of Open CASCADE Technology software library.
//
// This library is free software; you can redistribute it and/or modify it under
// the terms of the GNU Lesser General Public License version 2.1 as published
// by the Free Software Foundation, with special exception defined in the file
// OCCT_LGPL_EXCEPTION.txt. Consult the file LICENSE_LGPL_21.txt included in OCCT
// distribution for complete text of the license and disclaimer of any warranty.
//
// Alternatively, this file may be used under the terms of Open CASCADE
// commercial license or contractual agreement.

#ifndef _gp_Mat2d_HeaderFile
#define _gp_Mat2d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <double.hxx>
#include <int.hxx>
#include <Standard_Boolean.hxx>
public class Standard_ConstructionError;
public class Standard_OutOfRange;
public class gp_Trsf2d;
public class gp_GTrsf2d;
public class gp_XY;



//! Describes a two column, two row matrix. This sort of
//! object is used in various vectorial or matrix computations.
public class gp_Mat2d 
{


  

  
  //! Creates  a matrix with null coefficients.
    gp_Mat2d(){ }
  

  //! Col1, Col2 are the 2 columns of the matrix.
   gp_Mat2d( gp_XY Col1,  gp_XY Col2){ }
  
  //! Assigns the two coordinates of Value to the column of range
  //! Col of this matrix
  //! Raises OutOfRange if Col < 1 or Col > 2.
   public void SetCol ( int Col,  gp_XY Value){ }
  
  //! Assigns the number pairs Col1, Col2 to the two columns of   this matrix
   public void SetCols ( gp_XY Col1,  gp_XY Col2){ }
  

  //! Modifies the main diagonal of the matrix.
  //! <me>.Value (1, 1) = X1
  //! <me>.Value (2, 2) = X2
  //! The other coefficients of the matrix are not modified.
    public void SetDiagonal ( double X1,  double X2){ }
  
  //! Modifies this matrix, so that it represents the Identity matrix.
    public void SetIdentity(){ }
  

  //! Modifies this matrix, so that it representso a rotation. Ang is the angular
  //! value in radian of the rotation.
    public void SetRotation ( double Ang){ }
  
  //! Assigns the two coordinates of Value to the row of index Row of this matrix.
  //! Raises OutOfRange if Row < 1 or Row > 2.
   public void SetRow ( int Row,  gp_XY Value){ }
  
  //! Assigns the number pairs Row1, Row2 to the two rows of this matrix.
   public void SetRows ( gp_XY Row1,  gp_XY Row2){ }
  

  //! Modifies the matrix such that it
  //! represents a scaling transformation, where S is the scale   factor :
  //! | S    0.0 |
  //! <me> =  | 0.0   S  |
    public void SetScale ( double S){ }
  
  //! Assigns <Value> to the coefficient of row Row, column Col of this matrix.
  //! Raises OutOfRange if Row < 1 or Row > 2 or Col < 1 or Col > 2
    public void SetValue ( int Row,  int Col,  double Value){ }
  
  //! Returns the column of Col index.
  //! Raises OutOfRange if Col < 1 or Col > 2
   gp_XY Column ( int Col){ }
  
  //! Computes the determinant of the matrix.
    double Determinant(){ }
  
  //! Returns the main diagonal of the matrix.
   gp_XY Diagonal(){ }
  
  //! Returns the row of index Row.
  //! Raised if Row < 1 or Row > 2
   gp_XY Row ( int Row){ }
  
  //! Returns the coefficient of range (Row, Col)
  //! Raises OutOfRange
  //! if Row < 1 or Row > 2 or Col < 1 or Col > 2
     double Value ( int Row,  int Col){ }
   double operator() ( int Row,  int Col) 
{
  return Value(Row,Col){ }
}
  
  //! Returns the coefficient of range (Row, Col)
  //! Raises OutOfRange
  //! if Row < 1 or Row > 2 or Col < 1 or Col > 2
    double ChangeValue ( int Row,  int Col){ }
  double operator() ( int Row,  int Col)
{
  return ChangeValue(Row,Col){ }
}
  

  //! Returns true if this matrix is singular (and therefore, cannot be inverted).
  //! The Gauss LU decomposition is used to invert the matrix
  //! so the matrix is considered as singular if the largest
  //! pivot found is lower or equal to Resolution from gp.
    Standard_Boolean IsSingular(){ }
  
    public void Add ( gp_Mat2d Other){ }
  public void operator += ( gp_Mat2d Other)
{
  Add(Other){ }
}
  

  //! Computes the sum of this matrix and the matrix
  //! Other.for each coefficient of the matrix :
  //! <me>.Coef(i,j) + <Other>.Coef(i,j)
  //! Note:
  //! -   operator += assigns the result to this matrix, while
  //! -   operator + creates a new one.
   gp_Mat2d Added ( gp_Mat2d Other){ }
   gp_Mat2d operator + ( gp_Mat2d Other) 
{
  return Added(Other){ }
}
  
    public void Divide ( double Scalar){ }
  public void operator /= ( double Scalar)
{
  Divide(Scalar){ }
}
  

  //! Divides all the coefficients of the matrix by a scalar.
   gp_Mat2d Divided ( double Scalar){ }
   gp_Mat2d operator / ( double Scalar) 
{
  return Divided(Scalar){ }
}
  
   public void Invert(){ }
  

  //! Inverses the matrix and raises exception if the matrix
  //! is singular.
   gp_Mat2d Inverted(){ }
  
   gp_Mat2d Multiplied ( gp_Mat2d Other){ }
   gp_Mat2d operator * ( gp_Mat2d Other) 
{
  return Multiplied(Other){ }
}
  

  //! Computes the product of two matrices <me> * <Other>
    public void Multiply ( gp_Mat2d Other){ }
  
  //! Modifies this matrix by premultiplying it by the matrix Other
  //! <me> = Other * <me>.
    public void PreMultiply ( gp_Mat2d Other){ }
  
     gp_Mat2d Multiplied ( double Scalar){ }
   gp_Mat2d operator * ( double Scalar) 
{
  return Multiplied(Scalar){ }
}
  

  //! Multiplies all the coefficients of the matrix by a scalar.
    public void Multiply ( double Scalar){ }
  public void operator *= ( double Scalar)
{
  Multiply(Scalar){ }
}
  
   public void Power ( int N){ }
  

  //! computes <me> = <me> * <me> * .......* <me>, N time.
  //! if N = 0 <me> = Identity
  //! if N < 0 <me> = <me>.Invert() *...........* <me>.Invert().
  //! If N < 0 an exception can be raised if the matrix is not
  //! inversible
   gp_Mat2d Powered ( int N){ }
  
    public void Subtract ( gp_Mat2d Other){ }
  public void operator -= ( gp_Mat2d Other)
{
  Subtract(Other){ }
}
  

  //! Computes for each coefficient of the matrix :
  //! <me>.Coef(i,j) - <Other>.Coef(i,j)
   gp_Mat2d Subtracted ( gp_Mat2d Other){ }
   gp_Mat2d operator - ( gp_Mat2d Other) 
{
  return Subtracted(Other){ }
}
  
    public void Transpose(){ }
  

  //! Transposes the matrix. A(j, i) -> A (i, j)
     gp_Mat2d Transposed(){ }


friend public class gp_Trsf2d;
friend public class gp_GTrsf2d;
friend public class gp_XY;


protected:





private:



  double matrix[2][2];


};


#include <gp_Mat2d.lxx>





#endif // _gp_Mat2d_HeaderFile
