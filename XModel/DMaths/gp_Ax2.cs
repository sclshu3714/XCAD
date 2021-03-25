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

#ifndef _gp_Ax2_HeaderFile
#define _gp_Ax2_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax1.hxx>
#include <gp_Dir.hxx>
#include <double.hxx>
#include <Standard_Boolean.hxx>
public class Standard_ConstructionError;
public class gp_Pnt;
public class gp_Dir;
public class gp_Ax1;
public class gp_Trsf;
public class gp_Vec;



//! Describes a right-handed coordinate system in 3D space.
//! A coordinate system is defined by:
//! -   its origin (also referred to as its "Location point"), and
//! -   three orthogonal unit vectors, termed respectively the
//! "X Direction", the "Y Direction" and the "Direction" (also
//! referred to as the "main Direction").
//! The "Direction" of the coordinate system is called its
//! "main Direction" because whenever this unit vector is
//! modified, the "X Direction" and the "Y Direction" are
//! recomputed. However, when we modify either the "X
//! Direction" or the "Y Direction", "Direction" is not modified.
//! The "main Direction" is also the "Z Direction".
//! Since an Ax2 coordinate system is right-handed, its
//! "main Direction" is always equal to the cross product of
//! its "X Direction" and "Y Direction". (To define a
//! left-handed coordinate system, use gp_Ax3.)
//! A coordinate system is used:
//! -   to describe geometric entities, in particular to position
//! them. The local coordinate system of a geometric
//! entity serves the same purpose as the STEP function
//! "axis placement two axes", or
//! -   to define geometric transformations.
//! Note: we refer to the "X Axis", "Y Axis" and "Z Axis",
//! respectively, as to axes having:
//! - the origin of the coordinate system as their origin, and
//! -   the unit vectors "X Direction", "Y Direction" and "main
//! Direction", respectively, as their unit vectors.
//! The "Z Axis" is also the "main Axis".
public class gp_Ax2 
{


  

  
  //! Creates an object corresponding to the reference
  //! coordinate system (OXYZ).
    gp_Ax2(){ }
  

  //! Creates an axis placement with an origin P such that:
  //! -   N is the Direction, and
  //! -   the "X Direction" is normal to N, in the plane
  //! defined by the vectors (N, Vx): "X
  //! Direction" = (N ^ Vx) ^ N,
  //! Exception: raises ConstructionError if N and Vx are parallel (same or opposite orientation).
    gp_Ax2( gp_Pnt P,  gp_Dir N,  gp_Dir Vx){ }
  

  //! Creates -   a coordinate system with an origin P, where V
  //! gives the "main Direction" (here, "X Direction" and "Y
  //! Direction" are defined automatically).
   gp_Ax2( gp_Pnt P,  gp_Dir V){ }
  
  //! Assigns the origin and "main Direction" of the axis A1 to
  //! this coordinate system, then recomputes its "X Direction" and "Y Direction".
  //! Note: The new "X Direction" is computed as follows:
  //! new "X Direction" = V1 ^(previous "X Direction" ^ V)
  //! where V is the "Direction" of A1.
  //! Exceptions
  //! Standard_ConstructionError if A1 is parallel to the "X
  //! Direction" of this coordinate system.
  public void SetAxis ( gp_Ax1 A1){ }
  

  //! Changes the "main Direction" of this coordinate system,
  //! then recomputes its "X Direction" and "Y Direction".
  //! Note: the new "X Direction" is computed as follows:
  //! new "X Direction" = V ^ (previous "X Direction" ^ V)
  //! Exceptions
  //! Standard_ConstructionError if V is parallel to the "X
  //! Direction" of this coordinate system.
  public void SetDirection ( gp_Dir V){ }
  

  //! Changes the "Location" point (origin) of <me>.
  public void SetLocation ( gp_Pnt P){ }
  

  //! Changes the "Xdirection" of <me>. The main direction
  //! "Direction" is not modified, the "Ydirection" is modified.
  //! If <Vx> is not normal to the main direction then <XDirection>
  //! is computed as follows XDirection = Direction ^ (Vx ^ Direction).
  //! Exceptions
  //! Standard_ConstructionError if Vx or Vy is parallel to
  //! the "main Direction" of this coordinate system.
  public void SetXDirection ( gp_Dir Vx){ }
  

  //! Changes the "Ydirection" of <me>. The main direction is not
  //! modified but the "Xdirection" is changed.
  //! If <Vy> is not normal to the main direction then "YDirection"
  //! is computed as  follows
  //! YDirection = Direction ^ (<Vy> ^ Direction).
  //! Exceptions
  //! Standard_ConstructionError if Vx or Vy is parallel to
  //! the "main Direction" of this coordinate system.
  public void SetYDirection ( gp_Dir Vy){ }
  

  //! Computes the angular value, in radians, between the main direction of
  //! <me> and the main direction of <Other>. Returns the angle
  //! between 0 and PI in radians.
  double Angle ( gp_Ax2 Other){ }
  

  //! Returns the main axis of <me>. It is the "Location" point
  //! and the main "Direction".
     gp_Ax1 Axis(){ }
  

  //! Returns the main direction of <me>.
     gp_Dir Direction(){ }
  

  //! Returns the "Location" point (origin) of <me>.
     gp_Pnt Location(){ }
  

  //! Returns the "XDirection" of <me>.
     gp_Dir XDirection(){ }
  

  //! Returns the "YDirection" of <me>.
     gp_Dir YDirection(){ }
  
  Standard_Boolean IsCoplanar ( gp_Ax2 Other,  double LinearTolerance,  double AngularTolerance){ }
  

  //! Returns True if
  //! . the distance between <me> and the "Location" point of A1
  //! is lower of equal to LinearTolerance and
  //! . the main direction of <me> and the direction of A1 are normal.
  //! Note: the tolerance criterion for angular equality is given by AngularTolerance.
    Standard_Boolean IsCoplanar ( gp_Ax1 A1,  double LinearTolerance,  double AngularTolerance){ }
  

  //! Performs a symmetrical transformation of this coordinate
  //! system with respect to:
  //! -   the point P, and assigns the result to this coordinate system.
  //! Warning
  //! This transformation is always performed on the origin.
  //! In case of a reflection with respect to a point:
  //! - the main direction of the coordinate system is not changed, and
  //! - the "X Direction" and the "Y Direction" are simply reversed
  //! In case of a reflection with respect to an axis or a plane:
  //! -   the transformation is applied to the "X Direction"
  //! and the "Y Direction", then
  //! -   the "main Direction" is recomputed as the cross
  //! product "X Direction" ^ "Y   Direction".
  //! This maintains the right-handed property of the
  //! coordinate system.
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs a symmetrical transformation of this coordinate
  //! system with respect to:
  //! -   the point P, and creates a new one.
  //! Warning
  //! This transformation is always performed on the origin.
  //! In case of a reflection with respect to a point:
  //! - the main direction of the coordinate system is not changed, and
  //! - the "X Direction" and the "Y Direction" are simply reversed
  //! In case of a reflection with respect to an axis or a plane:
  //! -   the transformation is applied to the "X Direction"
  //! and the "Y Direction", then
  //! -   the "main Direction" is recomputed as the cross
  //! product "X Direction" ^ "Y   Direction".
  //! This maintains the right-handed property of the
  //! coordinate system.
    gp_Ax2 Mirrored ( gp_Pnt P){ }
  

  //! Performs a symmetrical transformation of this coordinate
  //! system with respect to:
  //! -   the axis A1, and assigns the result to this coordinate systeme.
  //! Warning
  //! This transformation is always performed on the origin.
  //! In case of a reflection with respect to a point:
  //! - the main direction of the coordinate system is not changed, and
  //! - the "X Direction" and the "Y Direction" are simply reversed
  //! In case of a reflection with respect to an axis or a plane:
  //! -   the transformation is applied to the "X Direction"
  //! and the "Y Direction", then
  //! -   the "main Direction" is recomputed as the cross
  //! product "X Direction" ^ "Y   Direction".
  //! This maintains the right-handed property of the
  //! coordinate system.
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs a symmetrical transformation of this coordinate
  //! system with respect to:
  //! -   the axis A1, and  creates a new one.
  //! Warning
  //! This transformation is always performed on the origin.
  //! In case of a reflection with respect to a point:
  //! - the main direction of the coordinate system is not changed, and
  //! - the "X Direction" and the "Y Direction" are simply reversed
  //! In case of a reflection with respect to an axis or a plane:
  //! -   the transformation is applied to the "X Direction"
  //! and the "Y Direction", then
  //! -   the "main Direction" is recomputed as the cross
  //! product "X Direction" ^ "Y   Direction".
  //! This maintains the right-handed property of the
  //! coordinate system.
    gp_Ax2 Mirrored ( gp_Ax1 A1){ }
  

  //! Performs a symmetrical transformation of this coordinate
  //! system with respect to:
  //! -   the plane defined by the origin, "X Direction" and "Y
  //! Direction" of coordinate system A2 and  assigns the result to this coordinate systeme.
  //! Warning
  //! This transformation is always performed on the origin.
  //! In case of a reflection with respect to a point:
  //! - the main direction of the coordinate system is not changed, and
  //! - the "X Direction" and the "Y Direction" are simply reversed
  //! In case of a reflection with respect to an axis or a plane:
  //! -   the transformation is applied to the "X Direction"
  //! and the "Y Direction", then
  //! -   the "main Direction" is recomputed as the cross
  //! product "X Direction" ^ "Y   Direction".
  //! This maintains the right-handed property of the
  //! coordinate system.
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs a symmetrical transformation of this coordinate
  //! system with respect to:
  //! -   the plane defined by the origin, "X Direction" and "Y
  //! Direction" of coordinate system A2 and creates a new one.
  //! Warning
  //! This transformation is always performed on the origin.
  //! In case of a reflection with respect to a point:
  //! - the main direction of the coordinate system is not changed, and
  //! - the "X Direction" and the "Y Direction" are simply reversed
  //! In case of a reflection with respect to an axis or a plane:
  //! -   the transformation is applied to the "X Direction"
  //! and the "Y Direction", then
  //! -   the "main Direction" is recomputed as the cross
  //! product "X Direction" ^ "Y   Direction".
  //! This maintains the right-handed property of the
  //! coordinate system.
    gp_Ax2 Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! Rotates an axis placement. <A1> is the axis of the
  //! rotation . Ang is the angular value of the rotation
  //! in radians.
     gp_Ax2 Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Applies a scaling transformation on the axis placement.
  //! The "Location" point of the axisplacement is modified.
  //! Warnings :
  //! If the scale <S> is negative :
  //! . the main direction of the axis placement is not changed.
  //! . The "XDirection" and the "YDirection" are reversed.
  //! So the axis placement stay right handed.
     gp_Ax2 Scaled ( gp_Pnt P,  double S){ }
  
    public void Transform ( gp_Trsf T){ }
  

  //! Transforms an axis placement with a Trsf.
  //! The "Location" point, the "XDirection" and the
  //! "YDirection" are transformed with T.  The resulting
  //! main "Direction" of <me> is the cross product between
  //! the "XDirection" and the "YDirection" after transformation.
     gp_Ax2 Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates an axis plaxement in the direction of the vector
  //! <V>. The magnitude of the translation is the vector's magnitude.
     gp_Ax2 Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates an axis placement from the point <P1> to the
  //! point <P2>.
     gp_Ax2 Translated ( gp_Pnt P1,  gp_Pnt P2){ }


  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }

  //! Inits the content of me from the stream
   Standard_Boolean InitFromJson ( Standard_SStream theSStream, int theStreamPos){ }



protected:





private:



  gp_Ax1 axis;
  gp_Dir vydir;
  gp_Dir vxdir;


};


#include <gp_Ax2.lxx>





#endif // _gp_Ax2_HeaderFile
