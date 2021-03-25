// Created on: 2010-05-11
// Created by: Kirill GAVRILOV
// Copyright (c) 2010-2014 OPEN CASCADE SAS
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

#ifndef _gp_Quaternion_HeaderFile
#define _gp_Quaternion_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <double.hxx>
#include <Standard_Boolean.hxx>
#include <gp_EulerSequence.hxx>
#include <gp_Vec.hxx>
public class gp_Vec;
public class gp_Mat;


//! Represents operation of rotation in 3d space as queternion
//! and implements operations with rotations basing on
//! quaternion mathematics.
//!
//! In addition, provides methods for conversion to and from other
//! representatons of rotation (3*3 matrix, vector and
//! angle, Euler angles)
public class gp_Quaternion 
{


  

  
  //! Creates an identity quaternion
  gp_Quaternion(){ }
  
  //! Creates quaternion directly from component values
  gp_Quaternion( double x,  double y,  double z,  double w){ }
  
  //! Creates quaternion representing shortest-arc rotation
  //! operator producing vector theVecTo from vector theVecFrom.
  gp_Quaternion( gp_Vec theVecFrom,  gp_Vec theVecTo){ }
  
  //! Creates quaternion representing shortest-arc rotation
  //! operator producing vector theVecTo from vector theVecFrom.
  //! Additional vector theHelpCrossVec defines preferred direction for
  //! rotation and is used when theVecTo and theVecFrom are directed
  //! oppositely.
  gp_Quaternion( gp_Vec theVecFrom,  gp_Vec theVecTo,  gp_Vec theHelpCrossVec){ }
  
  //! Creates quaternion representing rotation on angle
  //! theAngle around vector theAxis
  gp_Quaternion( gp_Vec theAxis,  double theAngle){ }
  
  //! Creates quaternion from rotation matrix 3*3
  //! (which should be orthonormal skew-symmetric matrix)
  gp_Quaternion( gp_Mat theMat){ }
  
  //! Simple equal test without precision
   Standard_Boolean IsEqual ( gp_Quaternion theOther){ }
  
  //! Sets quaternion to shortest-arc rotation producing
  //! vector theVecTo from vector theVecFrom.
  //! If vectors theVecFrom and theVecTo are opposite then rotation
  //! axis is computed as theVecFrom ^ (1,0,0) or theVecFrom ^ (0,0,1).
   public void SetRotation ( gp_Vec theVecFrom,  gp_Vec theVecTo){ }
  
  //! Sets quaternion to shortest-arc rotation producing
  //! vector theVecTo from vector theVecFrom.
  //! If vectors theVecFrom and theVecTo are opposite then rotation
  //! axis is computed as theVecFrom ^ theHelpCrossVec.
   public void SetRotation ( gp_Vec theVecFrom,  gp_Vec theVecTo,  gp_Vec theHelpCrossVec){ }
  
  //! Create a unit quaternion from Axis+Angle representation
   public void SetVectorAndAngle ( gp_Vec theAxis,  double theAngle){ }
  
  //! Convert a quaternion to Axis+Angle representation,
  //! preserve the axis direction and angle from -PI to +PI
   public void GetVectorAndAngle (gp_Vec theAxis, double theAngle){ }
  
  //! Create a unit quaternion by rotation matrix
  //! matrix must contain only rotation (not scale or shear)
  //!
  //! For numerical stability we find first the greatest component of quaternion
  //! and than search others from this one
   public void SetMatrix ( gp_Mat theMat){ }
  
  //! Returns rotation operation as 3*3 matrix
   gp_Mat GetMatrix(){ }
  
  //! Create a unit quaternion representing rotation defined
  //! by generalized Euler angles
   public void SetEulerAngles ( gp_EulerSequence theOrder,  double theAlpha,  double theBeta,  double theGamma){ }
  
  //! Returns Euler angles describing current rotation
   public void GetEulerAngles ( gp_EulerSequence theOrder, double theAlpha, double theBeta, double theGamma){ }
  
  public void Set ( double x,  double y,  double z,  double w){ }
  
  public void Set ( gp_Quaternion theQuaternion){ }
  
  double X(){ }
  
  double Y(){ }
  
  double Z(){ }
  
  double W(){ }
  
  //! Make identity quaternion (zero-rotation)
  public void SetIdent(){ }
  
  //! Reverse direction of rotation (conjugate quaternion)
  public void Reverse(){ }
  
  //! Return rotation with reversed direction (conjugated quaternion)
   gp_Quaternion Reversed(){ }
  
  //! Inverts quaternion (both rotation direction and norm)
  public void Invert(){ }
  
  //! Return inversed quaternion q^-1
   gp_Quaternion Inverted(){ }
  
  //! Returns square norm of quaternion
  double SquareNorm(){ }
  
  //! Returns norm of quaternion
  double Norm(){ }
  
  //! Scale all components by quaternion by theScale; note that
  //! rotation is not changed by this operation (except 0-scaling)
  public void Scale ( double theScale){ }
public void operator *= ( double theScale)
{
  Scale(theScale){ }
}
  
  //! Returns scaled quaternion
public gp_Quaternion Scaled ( double theScale){ }
public gp_Quaternion operator * ( double theScale) 
{
  return Scaled(theScale){ }
}
  
  //! Stabilize quaternion length within 1 - 1/4.
  //! This operation is a lot faster than normalization
  //! and preserve length goes to 0 or infinity
   public void StabilizeLength(){ }
  
  //! Scale quaternion that its norm goes to 1.
  //! The appearing of 0 magnitude or near is a error,
  //! so we can be sure that can divide by magnitude
   public void Normalize(){ }
  
  //! Returns quaternion scaled so that its norm goes to 1.
   gp_Quaternion Normalized(){ }
  
  //! Returns quaternion with all components negated.
  //! Note that this operation does not affect neither
  //! rotation operator defined by quaternion nor its norm.
   gp_Quaternion Negated(){ }
public gp_Quaternion operator -() 
{
  return Negated(){ }
}
  
  //! Makes sum of quaternion components; result is "rotations mix"
public gp_Quaternion Added ( gp_Quaternion theOther){ }
public gp_Quaternion operator + ( gp_Quaternion theOther) 
{
  return Added(theOther){ }
}
  
  //! Makes difference of quaternion components; result is "rotations mix"
public gp_Quaternion Subtracted ( gp_Quaternion theOther){ }
public gp_Quaternion operator - ( gp_Quaternion theOther) 
{
  return Subtracted(theOther){ }
}
  
  //! Multiply function - work the same as Matrices multiplying.
  //! qq' = (cross(v,v') + wv' + w'v, ww' - dot(v,v'))
  //! Result is rotation combination: q' than q (here q=this, q'=theQ).
  //! Notices than:
  //! qq' != q'q;
  //! qq^-1 = q;
public gp_Quaternion Multiplied ( gp_Quaternion theOther){ }
public gp_Quaternion operator * ( gp_Quaternion theOther) 
{
  return Multiplied(theOther){ }
}
  
  //! Adds componnets of other quaternion; result is "rotations mix"
  public void Add ( gp_Quaternion theOther){ }
public void operator += ( gp_Quaternion theOther)
{
  Add(theOther){ }
}
  
  //! Subtracts componnets of other quaternion; result is "rotations mix"
  public void Subtract ( gp_Quaternion theOther){ }
public void operator -= ( gp_Quaternion theOther)
{
  Subtract(theOther){ }
}
  
  //! Adds rotation by multiplication
  public void Multiply ( gp_Quaternion theOther){ }
public void operator *= ( gp_Quaternion theOther)
{
  Multiply(theOther){ }
}
  
  //! Computes inner product / scalar product / Dot
  double Dot ( gp_Quaternion theOther){ }
  
  //! Return rotation angle from -PI to PI
   double GetRotationAngle(){ }
  
  //! Rotates vector by quaternion as rotation operator
   gp_Vec Multiply ( gp_Vec theVec){ }
public gp_Vec operator * ( gp_Vec theVec) 
{
  return Multiply(theVec){ }
}




protected:





private:



  double x;
  double y;
  double z;
  double w;


};


#include <gp_Quaternion.lxx>





#endif // _gp_Quaternion_HeaderFile
