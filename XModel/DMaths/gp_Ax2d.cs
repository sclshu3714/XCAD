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

#ifndef _gp_Ax2d_HeaderFile
#define _gp_Ax2d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Pnt2d.hxx>
#include <gp_Dir2d.hxx>
#include <Standard_Boolean.hxx>
#include <double.hxx>
public class gp_Pnt2d;
public class gp_Dir2d;
public class gp_Trsf2d;
public class gp_Vec2d;



//! Describes an axis in the plane (2D space).
//! An axis is defined by:
//! -   its origin (also referred to as its "Location point"),   and
//! -   its unit vector (referred to as its "Direction").
//! An axis implicitly defines a direct, right-handed
//! coordinate system in 2D space by:
//! -   its origin,
//! - its "Direction" (giving the "X Direction" of the coordinate system), and
//! -   the unit vector normal to "Direction" (positive angle
//! measured in the trigonometric sense).
//! An axis is used:
//! -   to describe 2D geometric entities (for example, the
//! axis which defines angular coordinates on a circle).
//! It serves for the same purpose as the STEP function
//! "axis placement one axis", or
//! -   to define geometric transformations (axis of
//! symmetry, axis of rotation, and so on).
//! Note: to define a left-handed 2D coordinate system, use gp_Ax22d.
public class gp_Ax2d 
{


  

  
  //! Creates an axis object representing X axis of
  //! the reference co-ordinate system.
    gp_Ax2d(){ }
  

  //! Creates an Ax2d. <P> is the "Location" point of
  //! the axis placement and V is the "Direction" of
  //! the axis placement.
    gp_Ax2d( gp_Pnt2d P,  gp_Dir2d V){ }
  
  //! Changes the "Location" point (origin) of <me>.
    public void SetLocation ( gp_Pnt2d Locat){ }
  
  //! Changes the direction of <me>.
    public void SetDirection ( gp_Dir2d V){ }
  
  //! Returns the origin of <me>.
     gp_Pnt2d Location(){ }
  
  //! Returns the direction of <me>.
     gp_Dir2d Direction(){ }
  

  //! Returns True if  :
  //! . the angle between <me> and <Other> is lower or equal
  //! to <AngularTolerance> and
  //! . the distance between <me>.Location() and <Other> is lower
  //! or equal to <LinearTolerance> and
  //! . the distance between <Other>.Location() and <me> is lower
  //! or equal to LinearTolerance.
   Standard_Boolean IsCoaxial ( gp_Ax2d Other,  double AngularTolerance,  double LinearTolerance){ }
  
  //! Returns true if this axis and the axis Other are normal to
  //! each other. That is, if the angle between the two axes is equal to Pi/2 or -Pi/2.
  //! Note: the tolerance criterion is given by AngularTolerance.
    Standard_Boolean IsNormal ( gp_Ax2d Other,  double AngularTolerance){ }
  
  //! Returns true if this axis and the axis Other are parallel,
  //! and have opposite orientations. That is, if the angle
  //! between the two axes is equal to Pi or -Pi.
  //! Note: the tolerance criterion is given by AngularTolerance.
    Standard_Boolean IsOpposite ( gp_Ax2d Other,  double AngularTolerance){ }
  
  //! Returns true if this axis and the axis Other are parallel,
  //! and have either the same or opposite orientations. That
  //! is, if the angle between the two axes is equal to 0, Pi or -Pi.
  //! Note: the tolerance criterion is given by AngularTolerance.
    Standard_Boolean IsParallel ( gp_Ax2d Other,  double AngularTolerance){ }
  

  //! Computes the angle, in radians, between this axis and
  //! the axis Other. The value of the angle is between -Pi and Pi.
    double Angle ( gp_Ax2d Other){ }
  
  //! Reverses the direction of <me> and assigns the result to this axis.
    public void Reverse(){ }
  

  //! Computes a new axis placement with a direction opposite to
  //! the direction of <me>.
   gp_Ax2d Reversed(){ }
  
   public void Mirror ( gp_Pnt2d P){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to the point P which is the
  //! center of the symmetry.
    gp_Ax2d Mirrored ( gp_Pnt2d P){ }
  
   public void Mirror ( gp_Ax2d A){ }
  

  //! Performs the symmetrical transformation of an axis
  //! placement with respect to an axis placement which
  //! is the axis of the symmetry.
    gp_Ax2d Mirrored ( gp_Ax2d A){ }
  
    public void Rotate ( gp_Pnt2d P,  double Ang){ }
  

  //! Rotates an axis placement. <P> is the center of the
  //! rotation . Ang is the angular value of the rotation
  //! in radians.
     gp_Ax2d Rotated ( gp_Pnt2d P,  double Ang){ }
  
   public void Scale ( gp_Pnt2d P,  double S){ }
  

  //! Applies a scaling transformation on the axis placement.
  //! The "Location" point of the axisplacement is modified.
  //! The "Direction" is reversed if the scale is negative.
   gp_Ax2d Scaled ( gp_Pnt2d P,  double S){ }
  
    public void Transform ( gp_Trsf2d T){ }
  
  //! Transforms an axis placement with a Trsf.
     gp_Ax2d Transformed ( gp_Trsf2d T){ }
  
    public void Translate ( gp_Vec2d V){ }
  

  //! Translates an axis placement in the direction of the vector
  //! <V>. The magnitude of the translation is the vector's magnitude.
     gp_Ax2d Translated ( gp_Vec2d V){ }
  
    public void Translate ( gp_Pnt2d P1,  gp_Pnt2d P2){ }
  

  //! Translates an axis placement from the point <P1> to the
  //! point <P2>.
     gp_Ax2d Translated ( gp_Pnt2d P1,  gp_Pnt2d P2){ }

  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }




protected:





private:



  gp_Pnt2d loc;
  gp_Dir2d vdir;


};


#include <gp_Ax2d.lxx>





#endif // _gp_Ax2d_HeaderFile
