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

#ifndef _gp_Cylinder_HeaderFile
#define _gp_Cylinder_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax3.hxx>
#include <double.hxx>
#include <Standard_Boolean.hxx>
#include <gp_Ax1.hxx>
public class Standard_ConstructionError;
public class gp_Ax3;
public class gp_Ax1;
public class gp_Pnt;
public class gp_Ax2;
public class gp_Trsf;
public class gp_Vec;



//! Describes an infinite cylindrical surface.
//! A cylinder is defined by its radius and positioned in space
//! with a coordinate system (a gp_Ax3 object), the "main
//! Axis" of which is the axis of the cylinder. This coordinate
//! system is the "local coordinate system" of the cylinder.
//! Note: when a gp_Cylinder cylinder is converted into a
//! Geom_CylindricalSurface cylinder, some implicit
//! properties of its local coordinate system are used explicitly:
//! -   its origin, "X Direction", "Y Direction" and "main
//! Direction" are used directly to define the parametric
//! directions on the cylinder and the origin of the parameters,
//! -   its implicit orientation (right-handed or left-handed)
//! gives an orientation (direct or indirect) to the
//! Geom_CylindricalSurface cylinder.
//! See Also
//! gce_MakeCylinder which provides functions for more
//! complex cylinder ructions
//! Geom_CylindricalSurface which provides additional
//! functions for ructing cylinders and works, in
//! particular, with the parametric equations of cylinders gp_Ax3
public class gp_Cylinder 
{


  

  
  //! Creates a indefinite cylinder.
    gp_Cylinder(){ }
  
  //! Creates a cylinder of radius Radius, whose axis is the "main
  //! Axis" of A3. A3 is the local coordinate system of the cylinder.   Raises ConstructionErrord if R < 0.0
    gp_Cylinder( gp_Ax3 A3,  double Radius){ }
  
  //! Changes the symmetry axis of the cylinder. Raises ConstructionError if the direction of A1 is parallel to the "XDirection"
  //! of the coordinate system of the cylinder.
    public void SetAxis ( gp_Ax1 A1){ }
  
  //! Changes the location of the surface.
    public void SetLocation ( gp_Pnt Loc){ }
  
  //! Change the local coordinate system of the surface.
    public void SetPosition ( gp_Ax3 A3){ }
  
  //! Modifies the radius of this cylinder.
  //! Exceptions
  //! Standard_ConstructionError if R is negative.
    public void SetRadius ( double R){ }
  
  //! Reverses the   U   parametrization of   the cylinder
  //! reversing the YAxis.
    public void UReverse(){ }
  
  //! Reverses the   V   parametrization of   the  plane
  //! reversing the Axis.
    public void VReverse(){ }
  
  //! Returns true if the local coordinate system of this cylinder is right-handed.
    Standard_Boolean Direct(){ }
  
  //! Returns the symmetry axis of the cylinder.
     gp_Ax1 Axis(){ }
  

  //! Computes the coefficients of the implicit equation of the quadric
  //! in the absolute cartesian coordinate system :
  //! A1.X**2 + A2.Y**2 + A3.Z**2 + 2.(B1.X.Y + B2.X.Z + B3.Y.Z) +
  //! 2.(C1.X + C2.Y + C3.Z) + D = 0.0
   public void Coefficients (double A1, double A2, double A3, double B1, double B2, double B3, double C1, double C2, double C3, double D){ }
  
  //! Returns the "Location" point of the cylinder.
     gp_Pnt Location(){ }
  

  //! Returns the local coordinate system of the cylinder.
     gp_Ax3 Position(){ }
  
  //! Returns the radius of the cylinder.
    double Radius(){ }
  
  //! Returns the axis X of the cylinder.
    gp_Ax1 XAxis(){ }
  
  //! Returns the axis Y of the cylinder.
    gp_Ax1 YAxis(){ }
  
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of a cylinder
  //! with respect to the point P which is the center of the
  //! symmetry.
    gp_Cylinder Mirrored ( gp_Pnt P){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of a cylinder with
  //! respect to an axis placement which is the axis of the
  //! symmetry.
    gp_Cylinder Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs the symmetrical transformation of a cylinder with respect
  //! to a plane. The axis placement A2 locates the plane of the
  //! of the symmetry : (Location, XDirection, YDirection).
    gp_Cylinder Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! Rotates a cylinder. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Cylinder Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Scales a cylinder. S is the scaling value.
  //! The absolute value of S is used to scale the cylinder
     gp_Cylinder Scaled ( gp_Pnt P,  double S){ }
  
    public void Transform ( gp_Trsf T){ }
  

  //! Transforms a cylinder with the transformation T from public class Trsf.
     gp_Cylinder Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates a cylinder in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Cylinder Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates a cylinder from the point P1 to the point P2.
     gp_Cylinder Translated ( gp_Pnt P1,  gp_Pnt P2){ }




protected:





private:



  gp_Ax3 pos;
  double radius;


};


#include <gp_Cylinder.lxx>





#endif // _gp_Cylinder_HeaderFile
