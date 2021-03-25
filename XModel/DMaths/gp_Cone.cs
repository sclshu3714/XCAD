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

#ifndef _gp_Cone_HeaderFile
#define _gp_Cone_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax3.hxx>
#include <double.hxx>
#include <gp_Pnt.hxx>
#include <Standard_Boolean.hxx>
#include <gp_Ax1.hxx>
public class Standard_ConstructionError;
public class gp_Ax3;
public class gp_Ax1;
public class gp_Pnt;
public class gp_Ax2;
public class gp_Trsf;
public class gp_Vec;



//! Defines an infinite conical surface.
//! A cone is defined by its half-angle (can be negative) at the apex and
//! positioned in space with a coordinate system (a gp_Ax3
//! object) and a "reference radius" where:
//! -   the "main Axis" of the coordinate system is the axis of   revolution of the cone,
//! -   the plane defined by the origin, the "X Direction" and
//! the "Y Direction" of the coordinate system is the
//! reference plane of the cone; the intersection of the
//! cone with this reference plane is a circle of radius
//! equal to the reference radius,
//! if the half-angle is positive, the apex of the cone is on
//! the negative side of the "main Axis" of the coordinate
//! system. If the half-angle is negative, the apex is on the   positive side.
//! This coordinate system is the "local coordinate system" of the cone.
//! Note: when a gp_Cone cone is converted into a
//! Geom_ConicalSurface cone, some implicit properties of
//! its local coordinate system are used explicitly:
//! -   its origin, "X Direction", "Y Direction" and "main
//! Direction" are used directly to define the parametric
//! directions on the cone and the origin of the parameters,
//! -   its implicit orientation (right-handed or left-handed)
//! gives the orientation (direct or indirect) of the
//! Geom_ConicalSurface cone.
//! See Also
//! gce_MakeCone which provides functions for more
//! complex cone ructions
//! Geom_ConicalSurface which provides additional
//! functions for ructing cones and works, in particular,
//! with the parametric equations of cones gp_Ax3
public class gp_Cone 
{


  

  
  //! Creates an indefinite Cone.
    gp_Cone(){ }
  

  //! Creates an infinite conical surface. A3 locates the cone
  //! in the space and defines the reference plane of the surface.
  //! Ang is the conical surface semi-angle. Its absolute value is in range
  //! ]0, PI/2[.
  //! Radius is the radius of the circle in the reference plane of
  //! the cone.
  //! Raises ConstructionError
  //! * if Radius is lower than 0.0
  //! * Abs(Ang) < Resolution from gp  or Abs(Ang) >= (PI/2) - Resolution.
    gp_Cone( gp_Ax3 A3,  double Ang,  double Radius){ }
  
  //! Changes the symmetry axis of the cone.  Raises ConstructionError
  //! the direction of A1 is parallel to the "XDirection"
  //! of the coordinate system of the cone.
    public void SetAxis ( gp_Ax1 A1){ }
  
  //! Changes the location of the cone.
    public void SetLocation ( gp_Pnt Loc){ }
  

  //! Changes the local coordinate system of the cone.
  //! This coordinate system defines the reference plane of the cone.
    public void SetPosition ( gp_Ax3 A3){ }
  

  //! Changes the radius of the cone in the reference plane of
  //! the cone.
  //! Raised if R < 0.0
    public void SetRadius ( double R){ }
  

  //! Changes the semi-angle of the cone.
  //! Semi-angle can be negative. Its absolute value
  //! Abs(Ang) is in range ]0,PI/2[.
  //! Raises ConstructionError if Abs(Ang) < Resolution from gp or Abs(Ang) >= PI/2 - Resolution
    public void SetSemiAngle ( double Ang){ }
  

  //! Computes the cone's top. The Apex of the cone is on the
  //! negative side of the symmetry axis of the cone.
    gp_Pnt Apex(){ }
  
  //! Reverses the   U   parametrization of   the  cone
  //! reversing the YAxis.
    public void UReverse(){ }
  
  //! Reverses the   V   parametrization of   the  cone  reversing the ZAxis.
    public void VReverse(){ }
  
  //! Returns true if the local coordinate system of this cone is right-handed.
    Standard_Boolean Direct(){ }
  
  //! returns the symmetry axis of the cone.
     gp_Ax1 Axis(){ }
  

  //! Computes the coefficients of the implicit equation of the quadric
  //! in the absolute cartesian coordinates system :
  //! A1.X**2 + A2.Y**2 + A3.Z**2 + 2.(B1.X.Y + B2.X.Z + B3.Y.Z) +
  //! 2.(C1.X + C2.Y + C3.Z) + D = 0.0
   public void Coefficients (double A1, double A2, double A3, double B1, double B2, double B3, double C1, double C2, double C3, double D){ }
  
  //! returns the "Location" point of the cone.
     gp_Pnt Location(){ }
  

  //! Returns the local coordinates system of the cone.
     gp_Ax3 Position(){ }
  

  //! Returns the radius of the cone in the reference plane.
    double RefRadius(){ }
  
  //! Returns the half-angle at the apex of this cone.
  //! Attention! Semi-angle can be negative.
    double SemiAngle(){ }
  
  //! Returns the XAxis of the reference plane.
    gp_Ax1 XAxis(){ }
  
  //! Returns the YAxis of the reference plane.
    gp_Ax1 YAxis(){ }
  
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of a cone
  //! with respect to the point P which is the center of the
  //! symmetry.
    gp_Cone Mirrored ( gp_Pnt P){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of a cone with
  //! respect to an axis placement which is the axis of the
  //! symmetry.
    gp_Cone Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs the symmetrical transformation of a cone with respect
  //! to a plane. The axis placement A2 locates the plane of the
  //! of the symmetry : (Location, XDirection, YDirection).
    gp_Cone Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! Rotates a cone. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Cone Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Scales a cone. S is the scaling value.
  //! The absolute value of S is used to scale the cone
     gp_Cone Scaled ( gp_Pnt P,  double S){ }
  
    public void Transform ( gp_Trsf T){ }
  

  //! Transforms a cone with the transformation T from public class Trsf.
     gp_Cone Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates a cone in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Cone Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates a cone from the point P1 to the point P2.
     gp_Cone Translated ( gp_Pnt P1,  gp_Pnt P2){ }




protected:





private:



  gp_Ax3 pos;
  double radius;
  double semiAngle;


};


#include <gp_Cone.lxx>





#endif // _gp_Cone_HeaderFile
