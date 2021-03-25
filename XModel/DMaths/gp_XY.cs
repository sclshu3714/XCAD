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

#ifndef _gp_XY_HeaderFile
#define _gp_XY_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <double.hxx>
#include <int.hxx>
#include <Standard_Boolean.hxx>
public class Standard_ConstructionError;
public class Standard_OutOfRange;
public class gp_Mat2d;



//! This public class describes a cartesian coordinate entity in 2D
//! space {X,Y}. This public class is non persistent. This entity used
//! for algebraic calculation. An XY can be transformed with a
//! Trsf2d or a  GTrsf2d from package gp.
//! It is used in vectorial computations or for holding this type
//! of information in data structures.
public class gp_XY 
{


  

  
  //! Creates XY object with zero coordinates (0,0).
    gp_XY(){ }
  
  //! a number pair defined by the XY coordinates
    gp_XY( double X,  double Y){ }
  

  //! modifies the coordinate of range Index
  //! Index = 1 => X is modified
  //! Index = 2 => Y is modified
  //! Raises OutOfRange if Index != {1, 2}.
    public void SetCoord ( int Index,  double Xi){ }
  
  //! For this number pair, assigns
  //! the values X and Y to its coordinates
    public void SetCoord ( double X,  double Y){ }
  
  //! Assigns the given value to the X coordinate of this number pair.
    public void SetX ( double X){ }
  
  //! Assigns the given value to the Y  coordinate of this number pair.
    public void SetY ( double Y){ }
  

  //! returns the coordinate of range Index :
  //! Index = 1 => X is returned
  //! Index = 2 => Y is returned
  //! Raises OutOfRange if Index != {1, 2}.
    double Coord ( int Index){ }
  
    double ChangeCoord ( int theIndex){ }
  
  //! For this number pair, returns its coordinates X and Y.
    public void Coord (double X, double Y){ }
  
  //! Returns the X coordinate of this number pair.
    double X(){ }
  
  //! Returns the Y coordinate of this number pair.
    double Y(){ }
  
  //! Computes Sqrt (X*X + Y*Y) where X and Y are the two coordinates of this number pair.
    double Modulus(){ }
  
  //! Computes X*X + Y*Y where X and Y are the two coordinates of this number pair.
    double SquareModulus(){ }
  

  //! Returns true if the coordinates of this number pair are
  //! equal to the respective coordinates of the number pair
  //! Other, within the specified tolerance Tolerance. I.e.:
  //! abs(<me>.X() - Other.X()) <= Tolerance and
  //! abs(<me>.Y() - Other.Y()) <= Tolerance and
  //! computations
   Standard_Boolean IsEqual ( gp_XY Other,  double Tolerance){ }
  
  //! Computes the sum of this number pair and number pair Other
  //! <me>.X() = <me>.X() + Other.X()
  //! <me>.Y() = <me>.Y() + Other.Y()
    public void Add ( gp_XY Other){ }
  public void operator += ( gp_XY Other)
{
  Add(Other){ }
}
  
  //! Computes the sum of this number pair and number pair Other
  //! new.X() = <me>.X() + Other.X()
  //! new.Y() = <me>.Y() + Other.Y()
   gp_XY Added ( gp_XY Other){ }
   gp_XY operator + ( gp_XY Other) 
{
  return Added(Other){ }
}
  

  //! Real D = <me>.X() * Other.Y() - <me>.Y() * Other.X()
   double Crossed ( gp_XY Right){ }
   double operator ^ ( gp_XY Right) 
{
  return Crossed(Right){ }
}
  

  //! computes the magnitude of the cross product between <me> and
  //! Right. Returns || <me> ^ Right ||
    double CrossMagnitude ( gp_XY Right){ }
  

  //! computes the square magnitude of the cross product between <me> and
  //! Right. Returns || <me> ^ Right ||**2
    double CrossSquareMagnitude ( gp_XY Right){ }
  
  //! divides <me> by a real.
    public void Divide ( double Scalar){ }
  public void operator /= ( double Scalar)
{
  Divide(Scalar){ }
}
  
  //! Divides <me> by a real.
   gp_XY Divided ( double Scalar){ }
   gp_XY operator / ( double Scalar) 
{
  return Divided(Scalar){ }
}
  
  //! Computes the scalar product between <me> and Other
    double Dot ( gp_XY Other){ }
  double operator * ( gp_XY Other) 
{
  return Dot(Other){ }
}
  

  //! <me>.X() = <me>.X() * Scalar;
  //! <me>.Y() = <me>.Y() * Scalar;
    public void Multiply ( double Scalar){ }
  public void operator *= ( double Scalar)
{
  Multiply(Scalar){ }
}
  

  //! <me>.X() = <me>.X() * Other.X(){ }
  //! <me>.Y() = <me>.Y() * Other.Y(){ }
    public void Multiply ( gp_XY Other){ }
  public void operator *= ( gp_XY Other)
{
  Multiply(Other){ }
}
  
  //! <me> = Matrix * <me>
    public void Multiply ( gp_Mat2d Matrix){ }
  public void operator *= ( gp_Mat2d Matrix)
{
  Multiply(Matrix){ }
}
  

  //! New.X() = <me>.X() * Scalar;
  //! New.Y() = <me>.Y() * Scalar;
   gp_XY Multiplied ( double Scalar){ }
   gp_XY operator * ( double Scalar) 
{
  return Multiplied(Scalar){ }
}
  

  //! new.X() = <me>.X() * Other.X(){ }
  //! new.Y() = <me>.Y() * Other.Y(){ }
   gp_XY Multiplied ( gp_XY Other){ }
  
  //! New = Matrix * <me>
   gp_XY Multiplied ( gp_Mat2d Matrix){ }
   gp_XY operator * ( gp_Mat2d Matrix) 
{
  return Multiplied(Matrix){ }
}
  

  //! <me>.X() = <me>.X()/ <me>.Modulus()
  //! <me>.Y() = <me>.Y()/ <me>.Modulus()
  //! Raises ConstructionError if <me>.Modulus() <= Resolution from gp
    public void Normalize(){ }
  

  //! New.X() = <me>.X()/ <me>.Modulus()
  //! New.Y() = <me>.Y()/ <me>.Modulus()
  //! Raises ConstructionError if <me>.Modulus() <= Resolution from gp
     gp_XY Normalized(){ }
  

  //! <me>.X() = -<me>.X()
  //! <me>.Y() = -<me>.Y()
    public void Reverse(){ }
  

  //! New.X() = -<me>.X()
  //! New.Y() = -<me>.Y()
     gp_XY Reversed(){ }
   gp_XY operator -() 
{
  return Reversed(){ }
}
  

  //! Computes  the following linear combination and
  //! assigns the result to this number pair:
  //! A1 * XY1 + A2 * XY2
    public void SetLinearForm ( double A1,  gp_XY XY1,  double A2,  gp_XY XY2){ }
  

  //! --  Computes  the following linear combination and
  //! assigns the result to this number pair:
  //! A1 * XY1 + A2 * XY2 + XY3
    public void SetLinearForm ( double A1,  gp_XY XY1,  double A2,  gp_XY XY2,  gp_XY XY3){ }
  

  //! Computes  the following linear combination and
  //! assigns the result to this number pair:
  //! A1 * XY1 + XY2
    public void SetLinearForm ( double A1,  gp_XY XY1,  gp_XY XY2){ }
  

  //! Computes  the following linear combination and
  //! assigns the result to this number pair:
  //! XY1 + XY2
    public void SetLinearForm ( gp_XY XY1,  gp_XY XY2){ }
  

  //! <me>.X() = <me>.X() - Other.X()
  //! <me>.Y() = <me>.Y() - Other.Y()
    public void Subtract ( gp_XY Right){ }
  public void operator -= ( gp_XY Right)
{
  Subtract(Right){ }
}
  

  //! new.X() = <me>.X() - Other.X()
  //! new.Y() = <me>.Y() - Other.Y()
   gp_XY Subtracted ( gp_XY Right){ }
   gp_XY operator - ( gp_XY Right) 
{
  return Subtracted(Right){ }
}




protected:





private:



  double x;
  double y;


};


#include <gp_XY.lxx>





#endif // _gp_XY_HeaderFile
