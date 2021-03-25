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

#ifndef _gp_Vec_HeaderFile
#define _gp_Vec_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_XYZ.hxx>
#include <double.hxx>
#include <int.hxx>
#include <Standard_Boolean.hxx>
public class Standard_ConstructionError;
public class Standard_DomainError;
public class Standard_OutOfRange;
public class gp_VectorWithNullMagnitude;
public class gp_Dir;
public class gp_XYZ;
public class gp_Pnt;
public class gp_Ax1;
public class gp_Ax2;
public class gp_Trsf;



//! Defines a non-persistent vector in 3D space.
public class gp_Vec 
{


  

  
  //! Creates a zero vector.
    gp_Vec(){ }
  
  //! Creates a unitary vector from a direction V.
    gp_Vec( gp_Dir V){ }
  
  //! Creates a vector with a triplet of coordinates.
    gp_Vec( gp_XYZ Coord){ }
  
  //! Creates a point with its three cartesian coordinates.
    gp_Vec( double Xv,  double Yv,  double Zv){ }
  

  //! Creates a vector from two points. The length of the vector
  //! is the distance between P1 and P2
    gp_Vec( gp_Pnt P1,  gp_Pnt P2){ }
  
  //! Changes the coordinate of range Index
  //! Index = 1 => X is modified
  //! Index = 2 => Y is modified
  //! Index = 3 => Z is modified
  //! Raised if Index != {1, 2, 3}.
    public void SetCoord ( int Index,  double Xi){ }
  
  //! For this vector, assigns
  //! -   the values Xv, Yv and Zv to its three coordinates.
    public void SetCoord ( double Xv,  double Yv,  double Zv){ }
  
  //! Assigns the given value to the X coordinate of this vector.
    public void SetX ( double X){ }
  
  //! Assigns the given value to the X coordinate of this vector.
    public void SetY ( double Y){ }
  
  //! Assigns the given value to the X coordinate of this vector.
    public void SetZ ( double Z){ }
  
  //! Assigns the three coordinates of Coord to this vector.
    public void SetXYZ ( gp_XYZ Coord){ }
  

  //! Returns the coordinate of range Index :
  //! Index = 1 => X is returned
  //! Index = 2 => Y is returned
  //! Index = 3 => Z is returned
  //! Raised if Index != {1, 2, 3}.
    double Coord ( int Index){ }
  
  //! For this vector returns its three coordinates Xv, Yv, and Zvinline
    public void Coord (double Xv, double Yv, double Zv){ }
  
  //! For this vector, returns its X coordinate.
    double X(){ }
  
  //! For this vector, returns its Y coordinate.
    double Y(){ }
  
  //! For this vector, returns its Z  coordinate.
    double Z(){ }
  
  //! For this vector, returns
  //! -   its three coordinates as a number triple
     gp_XYZ XYZ(){ }
  

  //! Returns True if the two vectors have the same magnitude value
  //! and the same direction. The precision values are LinearTolerance
  //! for the magnitude and AngularTolerance for the direction.
   Standard_Boolean IsEqual ( gp_Vec Other,  double LinearTolerance,  double AngularTolerance){ }
  

  //! Returns True if abs(<me>.Angle(Other) - PI/2.) <= AngularTolerance
  //! Raises VectorWithNullMagnitude if <me>.Magnitude() <= Resolution or
  //! Other.Magnitude() <= Resolution from gp
    Standard_Boolean IsNormal ( gp_Vec Other,  double AngularTolerance){ }
  

  //! Returns True if PI - <me>.Angle(Other) <= AngularTolerance
  //! Raises VectorWithNullMagnitude if <me>.Magnitude() <= Resolution or
  //! Other.Magnitude() <= Resolution from gp
    Standard_Boolean IsOpposite ( gp_Vec Other,  double AngularTolerance){ }
  

  //! Returns True if Angle(<me>, Other) <= AngularTolerance or
  //! PI - Angle(<me>, Other) <= AngularTolerance
  //! This definition means that two parallel vectors cannot define
  //! a plane but two vectors with opposite directions are considered
  //! as parallel. Raises VectorWithNullMagnitude if <me>.Magnitude() <= Resolution or
  //! Other.Magnitude() <= Resolution from gp
    Standard_Boolean IsParallel ( gp_Vec Other,  double AngularTolerance){ }
  

  //! Computes the angular value between <me> and <Other>
  //! Returns the angle value between 0 and PI in radian.
  //! Raises VectorWithNullMagnitude if <me>.Magnitude() <= Resolution from gp or
  //! Other.Magnitude() <= Resolution because the angular value is
  //! indefinite if one of the vectors has a null magnitude.
    double Angle ( gp_Vec Other){ }
  
  //! Computes the angle, in radians, between this vector and
  //! vector Other. The result is a value between -Pi and Pi.
  //! For this, VRef defines the positive sense of rotation: the
  //! angular value is positive, if the cross product this ^ Other
  //! has the same orientation as VRef relative to the plane
  //! defined by the vectors this and Other. Otherwise, the
  //! angular value is negative.
  //! Exceptions
  //! gp_VectorWithNullMagnitude if the magnitude of this
  //! vector, the vector Other, or the vector VRef is less than or
  //! equal to gp::Resolution().
  //! Standard_DomainError if this vector, the vector Other,
  //! and the vector VRef are coplanar, unless this vector and
  //! the vector Other are parallel.
    double AngleWithRef ( gp_Vec Other,  gp_Vec VRef){ }
  
  //! Computes the magnitude of this vector.
    double Magnitude(){ }
  
  //! Computes the square magnitude of this vector.
    double SquareMagnitude(){ }
  
  //! Adds two vectors
    public void Add ( gp_Vec Other){ }
  public void operator += ( gp_Vec Other)
{
  Add(Other){ }
}
  
  //! Adds two vectors
   gp_Vec Added ( gp_Vec Other){ }
   gp_Vec operator + ( gp_Vec Other) 
{
  return Added(Other){ }
}
  
  //! Subtracts two vectors
    public void Subtract ( gp_Vec Right){ }
  public void operator -= ( gp_Vec Right)
{
  Subtract(Right){ }
}
  
  //! Subtracts two vectors
   gp_Vec Subtracted ( gp_Vec Right){ }
   gp_Vec operator - ( gp_Vec Right) 
{
  return Subtracted(Right){ }
}
  
  //! Multiplies a vector by a scalar
    public void Multiply ( double Scalar){ }
  public void operator *= ( double Scalar)
{
  Multiply(Scalar){ }
}
  
  //! Multiplies a vector by a scalar
   gp_Vec Multiplied ( double Scalar){ }
   gp_Vec operator * ( double Scalar) 
{
  return Multiplied(Scalar){ }
}
  
  //! Divides a vector by a scalar
    public void Divide ( double Scalar){ }
  public void operator /= ( double Scalar)
{
  Divide(Scalar){ }
}
  
  //! Divides a vector by a scalar
   gp_Vec Divided ( double Scalar){ }
   gp_Vec operator / ( double Scalar) 
{
  return Divided(Scalar){ }
}
  
  //! computes the cross product between two vectors
    public void Cross ( gp_Vec Right){ }
  public void operator ^= ( gp_Vec Right)
{
  Cross(Right){ }
}
  
  //! computes the cross product between two vectors
   gp_Vec Crossed ( gp_Vec Right){ }
   gp_Vec operator ^ ( gp_Vec Right) 
{
  return Crossed(Right){ }
}
  

  //! Computes the magnitude of the cross
  //! product between <me> and Right.
  //! Returns || <me> ^ Right ||
    double CrossMagnitude ( gp_Vec Right){ }
  

  //! Computes the square magnitude of
  //! the cross product between <me> and Right.
  //! Returns || <me> ^ Right ||**2
    double CrossSquareMagnitude ( gp_Vec Right){ }
  
  //! Computes the triple vector product.
  //! <me> ^= (V1 ^ V2)
    public void CrossCross ( gp_Vec V1,  gp_Vec V2){ }
  
  //! Computes the triple vector product.
  //! <me> ^ (V1 ^ V2)
     gp_Vec CrossCrossed ( gp_Vec V1,  gp_Vec V2){ }
  
  //! computes the scalar product
    double Dot ( gp_Vec Other){ }
  double operator * ( gp_Vec Other) 
{
  return Dot(Other){ }
}
  
  //! Computes the triple scalar product <me> * (V1 ^ V2).
    double DotCross ( gp_Vec V1,  gp_Vec V2){ }
  
  //! normalizes a vector
  //! Raises an exception if the magnitude of the vector is
  //! lower or equal to Resolution from gp.
    public void Normalize(){ }
  
  //! normalizes a vector
  //! Raises an exception if the magnitude of the vector is
  //! lower or equal to Resolution from gp.
     gp_Vec Normalized(){ }
  
  //! Reverses the direction of a vector
    public void Reverse(){ }
  
  //! Reverses the direction of a vector
     gp_Vec Reversed(){ }
   gp_Vec operator -() 
{
  return Reversed(){ }
}
  

  //! <me> is set to the following linear form :
  //! A1 * V1 + A2 * V2 + A3 * V3 + V4
    public void SetLinearForm ( double A1,  gp_Vec V1,  double A2,  gp_Vec V2,  double A3,  gp_Vec V3,  gp_Vec V4){ }
  

  //! <me> is set to the following linear form :
  //! A1 * V1 + A2 * V2 + A3 * V3
    public void SetLinearForm ( double A1,  gp_Vec V1,  double A2,  gp_Vec V2,  double A3,  gp_Vec V3){ }
  

  //! <me> is set to the following linear form :
  //! A1 * V1 + A2 * V2 + V3
    public void SetLinearForm ( double A1,  gp_Vec V1,  double A2,  gp_Vec V2,  gp_Vec V3){ }
  

  //! <me> is set to the following linear form :
  //! A1 * V1 + A2 * V2
    public void SetLinearForm ( double A1,  gp_Vec V1,  double A2,  gp_Vec V2){ }
  

  //! <me> is set to the following linear form : A1 * V1 + V2
    public void SetLinearForm ( double A1,  gp_Vec V1,  gp_Vec V2){ }
  

  //! <me> is set to the following linear form : V1 + V2
    public void SetLinearForm ( gp_Vec V1,  gp_Vec V2){ }
  
   public void Mirror ( gp_Vec V){ }
  

  //! Performs the symmetrical transformation of a vector
  //! with respect to the vector V which is the center of
  //! the  symmetry.
    gp_Vec Mirrored ( gp_Vec V){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of a vector
  //! with respect to an axis placement which is the axis
  //! of the symmetry.
    gp_Vec Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs the symmetrical transformation of a vector
  //! with respect to a plane. The axis placement A2 locates
  //! the plane of the symmetry : (Location, XDirection, YDirection).
    gp_Vec Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! Rotates a vector. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Vec Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( double S){ }
  
  //! Scales a vector. S is the scaling value.
     gp_Vec Scaled ( double S){ }
  
  //! Transforms a vector with the transformation T.
   public void Transform ( gp_Trsf T){ }
  
  //! Transforms a vector with the transformation T.
   gp_Vec Transformed ( gp_Trsf T){ }
  
  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }




protected:





private:



  gp_XYZ coord;


};


#include <gp_Vec.lxx>





#endif // _gp_Vec_HeaderFile
