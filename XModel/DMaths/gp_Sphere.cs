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

#ifndef _gp_Sphere_HeaderFile
#define _gp_Sphere_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax3.hxx>
#include <double.hxx>
#include <Standard_Boolean.hxx>
#include <gp_Ax1.hxx>
public class Standard_ConstructionError;
public class gp_Ax3;
public class gp_Pnt;
public class gp_Ax1;
public class gp_Ax2;
public class gp_Trsf;
public class gp_Vec;



//! Describes a sphere.
//! A sphere is defined by its radius and positioned in space
//! with a coordinate system (a gp_Ax3 object). The origin of
//! the coordinate system is the center of the sphere. This
//! coordinate system is the "local coordinate system" of the sphere.
//! Note: when a gp_Sphere sphere is converted into a
//! Geom_SphericalSurface sphere, some implicit
//! properties of its local coordinate system are used explicitly:
//! -   its origin, "X Direction", "Y Direction" and "main
//! Direction" are used directly to define the parametric
//! directions on the sphere and the origin of the parameters,
//! -   its implicit orientation (right-handed or left-handed)
//! gives the orientation (direct, indirect) to the
//! Geom_SphericalSurface sphere.
//! See Also
//! gce_MakeSphere which provides functions for more
//! complex sphere ructions
//! Geom_SphericalSurface which provides additional
//! functions for ructing spheres and works, in
//! particular, with the parametric equations of spheres.
public class gp_Sphere 
{


  

  
  //! Creates an indefinite sphere.
    gp_Sphere(){ }
  

  //! Constructs a sphere with radius Radius, centered on the origin
  //! of A3.  A3 is the local coordinate system of the sphere.
  //! Warnings :
  //! It is not forbidden to create a sphere with null radius.
  //! Raises ConstructionError if Radius < 0.0
    gp_Sphere( gp_Ax3 A3,  double Radius){ }
  
  //! Changes the center of the sphere.
    public void SetLocation ( gp_Pnt Loc){ }
  
  //! Changes the local coordinate system of the sphere.
    public void SetPosition ( gp_Ax3 A3){ }
  
  //! Assigns R the radius of the Sphere.
  //! Warnings :
  //! It is not forbidden to create a sphere with null radius.
  //! Raises ConstructionError if R < 0.0
    public void SetRadius ( double R){ }
  

  //! Computes the aera of the sphere.
    double Area(){ }
  

  //! Computes the coefficients of the implicit equation of the quadric
  //! in the absolute cartesian coordinates system :
  //! A1.X**2 + A2.Y**2 + A3.Z**2 + 2.(B1.X.Y + B2.X.Z + B3.Y.Z) +
  //! 2.(C1.X + C2.Y + C3.Z) + D = 0.0
   public void Coefficients (double A1, double A2, double A3, double B1, double B2, double B3, double C1, double C2, double C3, double D){ }
  
  //! Reverses the   U   parametrization of   the sphere
  //! reversing the YAxis.
    public void UReverse(){ }
  
  //! Reverses the   V   parametrization of   the  sphere
  //! reversing the ZAxis.
    public void VReverse(){ }
  
  //! Returns true if the local coordinate system of this sphere
  //! is right-handed.
    Standard_Boolean Direct(){ }
  
  //! --- Purpose ;
  //! Returns the center of the sphere.
     gp_Pnt Location(){ }
  

  //! Returns the local coordinates system of the sphere.
     gp_Ax3 Position(){ }
  
  //! Returns the radius of the sphere.
    double Radius(){ }
  
  //! Computes the volume of the sphere
    double Volume(){ }
  
  //! Returns the axis X of the sphere.
    gp_Ax1 XAxis(){ }
  
  //! Returns the axis Y of the sphere.
    gp_Ax1 YAxis(){ }
  
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of a sphere
  //! with respect to the point P which is the center of the
  //! symmetry.
    gp_Sphere Mirrored ( gp_Pnt P){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of a sphere with
  //! respect to an axis placement which is the axis of the
  //! symmetry.
    gp_Sphere Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs the symmetrical transformation of a sphere with respect
  //! to a plane. The axis placement A2 locates the plane of the
  //! of the symmetry : (Location, XDirection, YDirection).
    gp_Sphere Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! Rotates a sphere. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Sphere Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Scales a sphere. S is the scaling value.
  //! The absolute value of S is used to scale the sphere
     gp_Sphere Scaled ( gp_Pnt P,  double S){ }
  
    public void Transform ( gp_Trsf T){ }
  

  //! Transforms a sphere with the transformation T from public class Trsf.
     gp_Sphere Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates a sphere in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Sphere Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates a sphere from the point P1 to the point P2.
     gp_Sphere Translated ( gp_Pnt P1,  gp_Pnt P2){ }




protected:





private:



  gp_Ax3 pos;
  double radius;


};


#include <gp_Sphere.lxx>





#endif // _gp_Sphere_HeaderFile
