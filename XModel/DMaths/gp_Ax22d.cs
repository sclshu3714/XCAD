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

#ifndef _gp_Ax22d_HeaderFile
#define _gp_Ax22d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Pnt2d.hxx>
#include <gp_Dir2d.hxx>
#include <Standard_Boolean.hxx>
#include <gp_Ax2d.hxx>
#include <double.hxx>
public class Standard_ConstructionError;
public class gp_Pnt2d;
public class gp_Dir2d;
public class gp_Ax2d;
public class gp_Trsf2d;
public class gp_Vec2d;



//! Describes a coordinate system in a plane (2D space).
//! A coordinate system is defined by:
//! -   its origin (also referred to as its "Location point"), and
//! -   two orthogonal unit vectors, respectively, called the "X
//! Direction" and the "Y Direction".
//! A gp_Ax22d may be right-handed ("direct sense") or
//! left-handed ("inverse" or "indirect sense").
//! You use a gp_Ax22d to:
//! - describe 2D geometric entities, in particular to position
//! them. The local coordinate system of a geometric
//! entity serves for the same purpose as the STEP
//! function "axis placement two axes", or
//! -   define geometric transformations.
//! Note: we refer to the "X Axis" and "Y Axis" as the axes having:
//! -   the origin of the coordinate system as their origin, and
//! -   the unit vectors "X Direction" and "Y Direction",
//! respectively, as their unit vectors.
public class gp_Ax22d 
{


  

  
  //! Creates an object representing the reference
  //! co-ordinate system (OXY).
    gp_Ax22d(){ }
  

  //! Creates a coordinate system with origin P and where:
  //! -   Vx is the "X Direction", and
  //! -   the "Y Direction" is orthogonal to Vx and
  //! oriented so that the cross products Vx^"Y
  //! Direction" and Vx^Vy have the same sign.
  //! Raises ConstructionError if Vx and Vy are parallel (same or opposite orientation).
    gp_Ax22d( gp_Pnt2d P,  gp_Dir2d Vx,  gp_Dir2d Vy){ }
  

  //! Creates -   a coordinate system with origin P and "X Direction"
  //! V, which is:
  //! -   right-handed if Sense is true (default value), or
  //! -   left-handed if Sense is false
    gp_Ax22d( gp_Pnt2d P,  gp_Dir2d V,  Standard_Boolean Sense = Standard_True){ }
  

  //! Creates -   a coordinate system where its origin is the origin of
  //! A and its "X Direction" is the unit vector of A, which   is:
  //! -   right-handed if Sense is true (default value), or
  //! -   left-handed if Sense is false.
    gp_Ax22d( gp_Ax2d A,  Standard_Boolean Sense = Standard_True){ }
  

  //! Assigns the origin and the two unit vectors of the
  //! coordinate system A1 to this coordinate system.
    public void SetAxis ( gp_Ax22d A1){ }
  

  //! Changes the XAxis and YAxis ("Location" point and "Direction")
  //! of <me>.
  //! The "YDirection" is recomputed in the same sense as before.
    public void SetXAxis ( gp_Ax2d A1){ }
  
  //! Changes the XAxis and YAxis ("Location" point and "Direction") of <me>.
  //! The "XDirection" is recomputed in the same sense as before.
    public void SetYAxis ( gp_Ax2d A1){ }
  

  //! Changes the "Location" point (origin) of <me>.
    public void SetLocation ( gp_Pnt2d P){ }
  

  //! Assigns Vx to the "X Direction"  of
  //! this coordinate system. The other unit vector of this
  //! coordinate system is recomputed, normal to Vx ,
  //! without modifying the orientation (right-handed or
  //! left-handed) of this coordinate system.
    public void SetXDirection ( gp_Dir2d Vx){ }
  
  //! Assignsr Vy to the  "Y Direction" of
  //! this coordinate system. The other unit vector of this
  //! coordinate system is recomputed, normal to Vy,
  //! without modifying the orientation (right-handed or
  //! left-handed) of this coordinate system.
    public void SetYDirection ( gp_Dir2d Vy){ }
  
  //! Returns an axis, for which
  //! -   the origin is that of this coordinate system, and
  //! -   the unit vector is either the "X Direction"  of this coordinate system.
  //! Note: the result is the "X Axis" of this coordinate system.
    gp_Ax2d XAxis(){ }
  
  //! Returns an axis, for which
  //! -   the origin is that of this coordinate system, and
  //! - the unit vector is either the  "Y Direction" of this coordinate system.
  //! Note: the result is the "Y Axis" of this coordinate system.
    gp_Ax2d YAxis(){ }
  

  //! Returns the "Location" point (origin) of <me>.
     gp_Pnt2d Location(){ }
  

  //! Returns the "XDirection" of <me>.
     gp_Dir2d XDirection(){ }
  

  //! Returns the "YDirection" of <me>.
     gp_Dir2d YDirection(){ }
  
   public void Mirror ( gp_Pnt2d P){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to the point P which is the
  //! center of the symmetry.
  //! Warnings :
  //! The main direction of the axis placement is not changed.
  //! The "XDirection" and the "YDirection" are reversed.
  //! So the axis placement stay right handed.
    gp_Ax22d Mirrored ( gp_Pnt2d P){ }
  
   public void Mirror ( gp_Ax2d A){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to an axis placement which
  //! is the axis of the symmetry.
  //! The transformation is performed on the "Location"
  //! point, on the "XDirection" and "YDirection".
  //! The resulting main "Direction" is the cross product between
  //! the "XDirection" and the "YDirection" after transformation.
    gp_Ax22d Mirrored ( gp_Ax2d A){ }
  
    public void Rotate ( gp_Pnt2d P,  double Ang){ }
  

  //! Rotates an axis placement. <A1> is the axis of the
  //! rotation . Ang is the angular value of the rotation
  //! in radians.
     gp_Ax22d Rotated ( gp_Pnt2d P,  double Ang){ }
  
    public void Scale ( gp_Pnt2d P,  double S){ }
  

  //! Applies a scaling transformation on the axis placement.
  //! The "Location" point of the axisplacement is modified.
  //! Warnings :
  //! If the scale <S> is negative :
  //! . the main direction of the axis placement is not changed.
  //! . The "XDirection" and the "YDirection" are reversed.
  //! So the axis placement stay right handed.
     gp_Ax22d Scaled ( gp_Pnt2d P,  double S){ }
  
    public void Transform ( gp_Trsf2d T){ }
  

  //! Transforms an axis placement with a Trsf.
  //! The "Location" point, the "XDirection" and the
  //! "YDirection" are transformed with T.  The resulting
  //! main "Direction" of <me> is the cross product between
  //! the "XDirection" and the "YDirection" after transformation.
     gp_Ax22d Transformed ( gp_Trsf2d T){ }
  
    public void Translate ( gp_Vec2d V){ }
  

  //! Translates an axis plaxement in the direction of the vector
  //! <V>. The magnitude of the translation is the vector's magnitude.
     gp_Ax22d Translated ( gp_Vec2d V){ }
  
    public void Translate ( gp_Pnt2d P1,  gp_Pnt2d P2){ }
  

  //! Translates an axis placement from the point <P1> to the
  //! point <P2>.
     gp_Ax22d Translated ( gp_Pnt2d P1,  gp_Pnt2d P2){ }

  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }




protected:





private:



  gp_Pnt2d point;
  gp_Dir2d vydir;
  gp_Dir2d vxdir;


};


#include <gp_Ax22d.lxx>





#endif // _gp_Ax22d_HeaderFile
