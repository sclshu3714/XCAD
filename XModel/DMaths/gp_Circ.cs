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

#ifndef _gp_Circ_HeaderFile
#define _gp_Circ_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax2.hxx>
#include <double.hxx>
#include <gp_Ax1.hxx>
#include <Standard_Boolean.hxx>
public class Standard_ConstructionError;
public class gp_Ax2;
public class gp_Ax1;
public class gp_Pnt;
public class gp_Trsf;
public class gp_Vec;



//! Describes a circle in 3D space.
//! A circle is defined by its radius and positioned in space
//! with a coordinate system (a gp_Ax2 object) as follows:
//! -   the origin of the coordinate system is the center of the circle, and
//! -   the origin, "X Direction" and "Y Direction" of the
//! coordinate system define the plane of the circle.
//! This positioning coordinate system is the "local
//! coordinate system" of the circle. Its "main Direction"
//! gives the normal vector to the plane of the circle. The
//! "main Axis" of the coordinate system is referred to as
//! the "Axis" of the circle.
//! Note: when a gp_Circ circle is converted into a
//! Geom_Circle circle, some implicit properties of the
//! circle are used explicitly:
//! -   the "main Direction" of the local coordinate system
//! gives an implicit orientation to the circle (and defines
//! its trigonometric sense),
//! -   this orientation corresponds to the direction in
//! which parameter values increase,
//! -   the starting point for parameterization is that of the
//! "X Axis" of the local coordinate system (i.e. the "X Axis" of the circle).
//! See Also
//! gce_MakeCirc which provides functions for more complex circle ructions
//! Geom_Circle which provides additional functions for
//! ructing circles and works, in particular, with the
//! parametric equations of circles
public class gp_Circ 
{


  

  
  //! Creates an indefinite circle.
    gp_Circ(){ }
  

  //! A2 locates the circle and gives its orientation in 3D space.
  //! Warnings :
  //! It is not forbidden to create a circle with Radius = 0.0  Raises ConstructionError if Radius < 0.0
    gp_Circ( gp_Ax2 A2,  double Radius){ }
  

  //! Changes the main axis of the circle. It is the axis
  //! perpendicular to the plane of the circle.
  //! Raises ConstructionError if the direction of A1
  //! is parallel to the "XAxis" of the circle.
    public void SetAxis ( gp_Ax1 A1){ }
  

  //! Changes the "Location" point (center) of the circle.
    public void SetLocation ( gp_Pnt P){ }
  
  //! Changes the position of the circle.
    public void SetPosition ( gp_Ax2 A2){ }
  
  //! Modifies the radius of this circle.
  //! Warning. This public class does not prevent the creation of a circle where Radius is null.
  //! Exceptions
  //! Standard_ConstructionError if Radius is negative.
    public void SetRadius ( double Radius){ }
  
  //! Computes the area of the circle.
    double Area(){ }
  

  //! Returns the main axis of the circle.
  //! It is the axis perpendicular to the plane of the circle,
  //! passing through the "Location" point (center) of the circle.
     gp_Ax1 Axis(){ }
  
  //! Computes the circumference of the circle.
    double Length(){ }
  

  //! Returns the center of the circle. It is the
  //! "Location" point of the local coordinate system
  //! of the circle
     gp_Pnt Location(){ }
  

  //! Returns the position of the circle.
  //! It is the local coordinate system of the circle.
     gp_Ax2 Position(){ }
  
  //! Returns the radius of this circle.
    double Radius(){ }
  

  //! Returns the "XAxis" of the circle.
  //! This axis is perpendicular to the axis of the conic.
  //! This axis and the "Yaxis" define the plane of the conic.
    gp_Ax1 XAxis(){ }
  

  //! Returns the "YAxis" of the circle.
  //! This axis and the "Xaxis" define the plane of the conic.
  //! The "YAxis" is perpendicular to the "Xaxis".
    gp_Ax1 YAxis(){ }
  

  //! Computes the minimum of distance between the point P and
  //! any point on the circumference of the circle.
    double Distance ( gp_Pnt P){ }
  

  //! Computes the square distance between <me> and the point P.
    double SquareDistance ( gp_Pnt P){ }
  

  //! Returns True if the point P is on the circumference.
  //! The distance between <me> and <P> must be lower or
  //! equal to LinearTolerance.
    Standard_Boolean Contains ( gp_Pnt P,  double LinearTolerance){ }
  
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of a circle
  //! with respect to the point P which is the center of the
  //! symmetry.
    gp_Circ Mirrored ( gp_Pnt P){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of a circle with
  //! respect to an axis placement which is the axis of the
  //! symmetry.
    gp_Circ Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs the symmetrical transformation of a circle with respect
  //! to a plane. The axis placement A2 locates the plane of the
  //! of the symmetry : (Location, XDirection, YDirection).
    gp_Circ Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! Rotates a circle. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Circ Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Scales a circle. S is the scaling value.
  //! Warnings :
  //! If S is negative the radius stay positive but
  //! the "XAxis" and the "YAxis" are  reversed as for
  //! an ellipse.
     gp_Circ Scaled ( gp_Pnt P,  double S){ }
  
    public void Transform ( gp_Trsf T){ }
  

  //! Transforms a circle with the transformation T from public class Trsf.
     gp_Circ Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates a circle in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Circ Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates a circle from the point P1 to the point P2.
     gp_Circ Translated ( gp_Pnt P1,  gp_Pnt P2){ }




protected:





private:



  gp_Ax2 pos;
  double radius;


};


#include <gp_Circ.lxx>





#endif // _gp_Circ_HeaderFile
