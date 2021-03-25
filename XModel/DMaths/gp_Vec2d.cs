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

#ifndef _gp_Vec2d_HeaderFile
#define _gp_Vec2d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_XY.hxx>
#include <double.hxx>
#include <int.hxx>
#include <Standard_Boolean.hxx>
public class Standard_ConstructionError;
public class Standard_OutOfRange;
public class gp_VectorWithNullMagnitude;
public class gp_Dir2d;
public class gp_XY;
public class gp_Pnt2d;
public class gp_Ax2d;
public class gp_Trsf2d;



//! Defines a non-persistent vector in 2D space.
public class gp_Vec2d 
{


  

  
  //! Creates a zero vector.
    gp_Vec2d(){ }
  
  //! Creates a unitary vector from a direction V.
    gp_Vec2d( gp_Dir2d V){ }
  
  //! Creates a vector with a doublet of coordinates.
    gp_Vec2d( gp_XY Coord){ }
  
  //! Creates a point with its two Cartesian coordinates.
    gp_Vec2d( double Xv,  double Yv){ }
  

  //! Creates a vector from two points. The length of the vector
  //! is the distance between P1 and P2
    gp_Vec2d( gp_Pnt2d P1,  gp_Pnt2d P2){ }
  
  //! Changes the coordinate of range Index
  //! Index = 1 => X is modified
  //! Index = 2 => Y is modified
  //! Raises OutOfRange if Index != {1, 2}.
    public void SetCoord ( int Index,  double Xi){ }
  
  //! For this vector, assigns
  //! the values Xv and Yv to its two coordinates
    public void SetCoord ( double Xv,  double Yv){ }
  
  //! Assigns the given value to the X coordinate of this vector.
    public void SetX ( double X){ }
  
  //! Assigns the given value to the Y coordinate of this vector.
    public void SetY ( double Y){ }
  
  //! Assigns the two coordinates of Coord to this vector.
    public void SetXY ( gp_XY Coord){ }
  

  //! Returns the coordinate of range Index :
  //! Index = 1 => X is returned
  //! Index = 2 => Y is returned
  //! Raised if Index != {1, 2}.
    double Coord ( int Index){ }
  
  //! For this vector, returns  its two coordinates Xv and Yv
    public void Coord (double Xv, double Yv){ }
  
  //! For this vector, returns its X  coordinate.
    double X(){ }
  
  //! For this vector, returns its Y  coordinate.
    double Y(){ }
  
  //! For this vector, returns its two coordinates as a number pair
     gp_XY XY(){ }
  

  //! Returns True if the two vectors have the same magnitude value
  //! and the same direction. The precision values are LinearTolerance
  //! for the magnitude and AngularTolerance for the direction.
   Standard_Boolean IsEqual ( gp_Vec2d Other,  double LinearTolerance,  double AngularTolerance){ }
  

  //! Returns True if abs(Abs(<me>.Angle(Other)) - PI/2.)
  //! <= AngularTolerance
  //! Raises VectorWithNullMagnitude if <me>.Magnitude() <= Resolution or
  //! Other.Magnitude() <= Resolution from gp.
    Standard_Boolean IsNormal ( gp_Vec2d Other,  double AngularTolerance){ }
  

  //! Returns True if PI - Abs(<me>.Angle(Other)) <= AngularTolerance
  //! Raises VectorWithNullMagnitude if <me>.Magnitude() <= Resolution or
  //! Other.Magnitude() <= Resolution from gp.
  Standard_Boolean IsOpposite ( gp_Vec2d Other,  double AngularTolerance){ }
  

  //! Returns true if Abs(Angle(<me>, Other)) <= AngularTolerance or
  //! PI - Abs(Angle(<me>, Other)) <= AngularTolerance
  //! Two vectors with opposite directions are considered as parallel.
  //! Raises VectorWithNullMagnitude if <me>.Magnitude() <= Resolution or
  //! Other.Magnitude() <= Resolution from gp
  Standard_Boolean IsParallel ( gp_Vec2d Other,  double AngularTolerance){ }
  

  //! Computes the angular value between <me> and <Other>
  //! returns the angle value between -PI and PI in radian.
  //! The orientation is from <me> to Other. The positive sense is the
  //! trigonometric sense.
  //! Raises VectorWithNullMagnitude if <me>.Magnitude() <= Resolution from gp or
  //! Other.Magnitude() <= Resolution because the angular value is
  //! indefinite if one of the vectors has a null magnitude.
   double Angle ( gp_Vec2d Other){ }
  
  //! Computes the magnitude of this vector.
    double Magnitude(){ }
  
  //! Computes the square magnitude of this vector.
    double SquareMagnitude(){ }
  
    public void Add ( gp_Vec2d Other){ }
    public void operator += ( gp_Vec2d Other)
    {
      Add(Other){ }
    }
  
  //! Adds two vectors
     gp_Vec2d Added ( gp_Vec2d Other){ }
     gp_Vec2d operator + ( gp_Vec2d Other) 
    {
      return Added(Other){ }
    }
  
  //! Computes the crossing product between two vectors
     double Crossed ( gp_Vec2d Right){ }
     double operator ^ ( gp_Vec2d Right) 
    {
      return Crossed(Right){ }
    }
  

  //! Computes the magnitude of the cross product between <me> and
  //! Right. Returns || <me> ^ Right ||
    double CrossMagnitude ( gp_Vec2d Right){ }
  

  //! Computes the square magnitude of the cross product between <me> and
  //! Right. Returns || <me> ^ Right ||**2
    double CrossSquareMagnitude ( gp_Vec2d Right){ }
  
    public void Divide ( double Scalar){ }
    public void operator /= ( double Scalar)
    {
      Divide(Scalar){ }
    }
  
  //! divides a vector by a scalar
     gp_Vec2d Divided ( double Scalar){ }
     gp_Vec2d operator / ( double Scalar) 
    {
      return Divided(Scalar){ }
    }
  
  //! Computes the scalar product
    double Dot ( gp_Vec2d Other){ }
    double operator * ( gp_Vec2d Other) 
    {
      return Dot(Other){ }
    }

    gp_Vec2d GetNormal(){ }
  
    public void Multiply ( double Scalar){ }
    public void operator *= ( double Scalar)
    {
      Multiply(Scalar){ }
    }
  
  //! Normalizes a vector
  //! Raises an exception if the magnitude of the vector is
  //! lower or equal to Resolution from package gp.
     gp_Vec2d Multiplied ( double Scalar){ }
     gp_Vec2d operator * ( double Scalar) 
    {
      return Multiplied(Scalar){ }
    }
  
    public void Normalize(){ }
  
  //! Normalizes a vector
  //! Raises an exception if the magnitude of the vector is
  //! lower or equal to Resolution from package gp.
  //! Reverses the direction of a vector
     gp_Vec2d Normalized(){ }
  
    public void Reverse(){ }
  
  //! Reverses the direction of a vector
     gp_Vec2d Reversed(){ }
     gp_Vec2d operator -() 
    {
      return Reversed(){ }
    }
  
  //! Subtracts two vectors
    public void Subtract ( gp_Vec2d Right){ }
    public void operator -= ( gp_Vec2d Right)
    {
      Subtract(Right){ }
    }
  
  //! Subtracts two vectors
     gp_Vec2d Subtracted ( gp_Vec2d Right){ }
     gp_Vec2d operator - ( gp_Vec2d Right) 
    {
      return Subtracted(Right){ }
    }
  

  //! <me> is set to the following linear form :
  //! A1 * V1 + A2 * V2 + V3
    public void SetLinearForm ( double A1,  gp_Vec2d V1,  double A2,  gp_Vec2d V2,  gp_Vec2d V3){ }
  

  //! <me> is set to the following linear form : A1 * V1 + A2 * V2
    public void SetLinearForm ( double A1,  gp_Vec2d V1,  double A2,  gp_Vec2d V2){ }
  

  //! <me> is set to the following linear form : A1 * V1 + V2
    public void SetLinearForm ( double A1,  gp_Vec2d V1,  gp_Vec2d V2){ }
  

  //! <me> is set to the following linear form : Left + Right
    public void SetLinearForm ( gp_Vec2d Left,  gp_Vec2d Right){ }
  

  //! Performs the symmetrical transformation of a vector
  //! with respect to the vector V which is the center of
  //! the  symmetry.
   public void Mirror ( gp_Vec2d V){ }
  

  //! Performs the symmetrical transformation of a vector
  //! with respect to the vector V which is the center of
  //! the  symmetry.
    gp_Vec2d Mirrored ( gp_Vec2d V){ }
  

  //! Performs the symmetrical transformation of a vector
  //! with respect to an axis placement which is the axis
  //! of the symmetry.
   public void Mirror ( gp_Ax2d A1){ }
  

  //! Performs the symmetrical transformation of a vector
  //! with respect to an axis placement which is the axis
  //! of the symmetry.
    gp_Vec2d Mirrored ( gp_Ax2d A1){ }
  
    public void Rotate ( double Ang){ }
  

  //! Rotates a vector. Ang is the angular value of the
  //! rotation in radians.
     gp_Vec2d Rotated ( double Ang){ }
  
    public void Scale ( double S){ }
  
  //! Scales a vector. S is the scaling value.
     gp_Vec2d Scaled ( double S){ }
  
   public void Transform ( gp_Trsf2d T){ }
  
  //! Transforms a vector with a Trsf from gp.
   gp_Vec2d Transformed ( gp_Trsf2d T){ }




protected:





private:



  gp_XY coord;


};


#include <gp_Vec2d.lxx>





#endif // _gp_Vec2d_HeaderFile
