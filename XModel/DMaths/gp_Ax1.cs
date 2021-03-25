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

#ifndef _gp_Ax1_HeaderFile
#define _gp_Ax1_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Pnt.hxx>
#include <gp_Dir.hxx>
#include <Standard_Boolean.hxx>
#include <double.hxx>
public class gp_Pnt;
public class gp_Dir;
public class gp_Ax2;
public class gp_Trsf;
public class gp_Vec;


//! Describes an axis in 3D space.
//! An axis is defined by:
//! -   its origin (also referred to as its "Location point"), and
//! -   its unit vector (referred to as its "Direction" or "main   Direction").
//! An axis is used:
//! -   to describe 3D geometric entities (for example, the
//! axis of a revolution entity). It serves the same purpose
//! as the STEP function "axis placement one axis", or
//! -   to define geometric transformations (axis of
//! symmetry, axis of rotation, and so on).
//! For example, this entity can be used to locate a geometric entity
//! or to define a symmetry axis.
public class gp_Ax1 
{


  

  
  //! Creates an axis object representing Z axis of
  //! the reference co-ordinate system.
    gp_Ax1(){ }
  

  //! P is the location point and V is the direction of <me>.
    gp_Ax1( gp_Pnt P,  gp_Dir V){ }
  
  //! Assigns V as the "Direction"  of this axis.
    public void SetDirection ( gp_Dir V){ }
  
  //! Assigns  P as the origin of this axis.
    public void SetLocation ( gp_Pnt P){ }
  
  //! Returns the direction of <me>.
     gp_Dir Direction(){ }
  
  //! Returns the location point of <me>.
     gp_Pnt Location(){ }
  

  //! Returns True if  :
  //! . the angle between <me> and <Other> is lower or equal
  //! to <AngularTolerance> and
  //! . the distance between <me>.Location() and <Other> is lower
  //! or equal to <LinearTolerance> and
  //! . the distance between <Other>.Location() and <me> is lower
  //! or equal to LinearTolerance.
   Standard_Boolean IsCoaxial ( gp_Ax1 Other,  double AngularTolerance,  double LinearTolerance){ }
  

  //! Returns True if the direction of the <me> and <Other>
  //! are normal to each other.
  //! That is, if the angle between the two axes is equal to Pi/2.
  //! Note: the tolerance criterion is given by AngularTolerance..
    Standard_Boolean IsNormal ( gp_Ax1 Other,  double AngularTolerance){ }
  

  //! Returns True if the direction of <me> and <Other> are
  //! parallel with opposite orientation. That is, if the angle
  //! between the two axes is equal to Pi.
  //! Note: the tolerance criterion is given by AngularTolerance.
    Standard_Boolean IsOpposite ( gp_Ax1 Other,  double AngularTolerance){ }
  

  //! Returns True if the direction of <me> and <Other> are
  //! parallel with same orientation or opposite orientation. That
  //! is, if the angle between the two axes is equal to 0 or Pi.
  //! Note: the tolerance criterion is given by
  //! AngularTolerance.
    Standard_Boolean IsParallel ( gp_Ax1 Other,  double AngularTolerance){ }
  

  //! Computes the angular value, in radians, between <me>.Direction() and
  //! <Other>.Direction(). Returns the angle between 0 and 2*PI
  //! radians.
    double Angle ( gp_Ax1 Other){ }
  
  //! Reverses the unit vector of this axis.
  //! and  assigns the result to this axis.
    public void Reverse(){ }
  
  //! Reverses the unit vector of this axis and creates a new one.
     gp_Ax1 Reversed(){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to the point P which is the
  //! center of the symmetry and assigns the result to this axis.
   public void Mirror ( gp_Pnt P){ }
  
  //! Performs the symmetrical transformation of an axis
  //! placement with respect to the point P which is the
  //! center of the symmetry and creates a new axis.
    gp_Ax1 Mirrored ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to an axis placement which
  //! is the axis of the symmetry and assigns the result to this axis.
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to an axis placement which
  //! is the axis of the symmetry and creates a new axis.
    gp_Ax1 Mirrored ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to a plane. The axis placement
  //! <A2> locates the plane of the symmetry :
  //! (Location, XDirection, YDirection) and assigns the result to this axis.
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to a plane. The axis placement
  //! <A2> locates the plane of the symmetry :
  //! (Location, XDirection, YDirection) and creates a new axis.
    gp_Ax1 Mirrored ( gp_Ax2 A2){ }
  
  //! Rotates this axis at an angle Ang (in radians) about the axis A1
  //! and assigns the result to this axis.
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  
  //! Rotates this axis at an angle Ang (in radians) about the axis A1
  //! and creates a new one.
     gp_Ax1 Rotated ( gp_Ax1 A1,  double Ang){ }
  

  //! Applies a scaling transformation to this axis with:
  //! -   scale factor S, and
  //! -   center P and assigns the result to this axis.
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Applies a scaling transformation to this axis with:
  //! -   scale factor S, and
  //! -   center P and creates a new axis.
     gp_Ax1 Scaled ( gp_Pnt P,  double S){ }
  
  //! Applies the transformation T to this axis.
  //! and assigns the result to this axis.
    public void Transform ( gp_Trsf T){ }
  

  //! Applies the transformation T to this axis and creates a new one.
  //!
  //! Translates an axis plaxement in the direction of the vector
  //! <V>. The magnitude of the translation is the vector's magnitude.
     gp_Ax1 Transformed ( gp_Trsf T){ }
  

  //! Translates this axis by the vector V,
  //! and assigns the result to this axis.
    public void Translate ( gp_Vec V){ }
  

  //! Translates this axis by the vector V,
  //! and creates a new one.
     gp_Ax1 Translated ( gp_Vec V){ }
  

  //! Translates this axis by:
  //! the vector (P1, P2) defined from point P1 to point P2.
  //! and assigns the result to this axis.
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates this axis by:
  //! the vector (P1, P2) defined from point P1 to point P2.
  //! and creates a new one.
     gp_Ax1 Translated ( gp_Pnt P1,  gp_Pnt P2){ }


  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }

  //! Inits the content of me from the stream
   Standard_Boolean InitFromJson ( Standard_SStream theSStream, int theStreamPos){ }

protected:





private:



  gp_Pnt loc;
  gp_Dir vdir;


};


#include <gp_Ax1.lxx>





#endif // _gp_Ax1_HeaderFile
