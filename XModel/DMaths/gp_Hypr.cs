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

#ifndef _gp_Hypr_HeaderFile
#define _gp_Hypr_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax2.hxx>
#include <double.hxx>
#include <gp_Ax1.hxx>
#include <gp_Pnt.hxx>
public class Standard_ConstructionError;
public class Standard_DomainError;
public class gp_Ax2;
public class gp_Ax1;
public class gp_Pnt;
public class gp_Trsf;
public class gp_Vec;


//! Describes a branch of a hyperbola in 3D space.
//! A hyperbola is defined by its major and minor radii and
//! positioned in space with a coordinate system (a gp_Ax2
//! object) of which:
//! -   the origin is the center of the hyperbola,
//! -   the "X Direction" defines the major axis of the
//! hyperbola, and
//! - the "Y Direction" defines the minor axis of the hyperbola.
//! The origin, "X Direction" and "Y Direction" of this
//! coordinate system together define the plane of the
//! hyperbola. This coordinate system is the "local
//! coordinate system" of the hyperbola. In this coordinate
//! system, the equation of the hyperbola is:
//! X*X/(MajorRadius**2)-Y*Y/(MinorRadius**2) = 1.0
//! The branch of the hyperbola described is the one located
//! on the positive side of the major axis.
//! The "main Direction" of the local coordinate system is a
//! normal vector to the plane of the hyperbola. This vector
//! gives an implicit orientation to the hyperbola. We refer to
//! the "main Axis" of the local coordinate system as the
//! "Axis" of the hyperbola.
//! The following schema shows the plane of the hyperbola,
//! and in it, the respective positions of the three branches of
//! hyperbolas ructed with the functions OtherBranch,
//! ConjugateBranch1, and ConjugateBranch2:
//!
//! ^YAxis
//! |
//! FirstConjugateBranch
//! |
//! Other            |                Main
//! --------------------- C ------------------------------>XAxis
//! Branch           |                Branch
//! |
//! |
//! SecondConjugateBranch
//! |                  ^YAxis
//! Warning
//! The major radius can be less than the minor radius.
//! See Also
//! gce_MakeHypr which provides functions for more
//! complex hyperbola ructions
//! Geom_Hyperbola which provides additional functions for
//! ructing hyperbolas and works, in particular, with the
//! parametric equations of hyperbolas
public class gp_Hypr 
{


  

  
  //! Creates of an indefinite hyperbola.
    gp_Hypr(){ }
  
  //! Creates a hyperbola with radii MajorRadius and
  //! MinorRadius, positioned in the space by the
  //! coordinate system A2 such that:
  //! -   the origin of A2 is the center of the hyperbola,
  //! -   the "X Direction" of A2 defines the major axis of
  //! the hyperbola, that is, the major radius
  //! MajorRadius is measured along this axis, and
  //! -   the "Y Direction" of A2 defines the minor axis of
  //! the hyperbola, that is, the minor radius
  //! MinorRadius is measured along this axis.
  //! Note: This public class does not prevent the creation of a
  //! hyperbola where:
  //! -   MajorAxis is equal to MinorAxis, or
  //! -   MajorAxis is less than MinorAxis.
  //! Exceptions
  //! Standard_ConstructionError if MajorAxis or MinorAxis is negative.
  //! Raises ConstructionError if MajorRadius < 0.0 or MinorRadius < 0.0
  //! Raised if MajorRadius < 0.0 or MinorRadius < 0.0
    gp_Hypr( gp_Ax2 A2,  double MajorRadius,  double MinorRadius){ }
  
  //! Modifies this hyperbola, by redefining its local coordinate
  //! system so that:
  //! -   its origin and "main Direction" become those of the
  //! axis A1 (the "X Direction" and "Y Direction" are then
  //! recomputed in the same way as for any gp_Ax2).
  //! Raises ConstructionError if the direction of A1 is parallel to the direction of
  //! the "XAxis" of the hyperbola.
    public void SetAxis ( gp_Ax1 A1){ }
  
  //! Modifies this hyperbola, by redefining its local coordinate
  //! system so that its origin becomes P.
    public void SetLocation ( gp_Pnt P){ }
  

  //! Modifies the major  radius of this hyperbola.
  //! Exceptions
  //! Standard_ConstructionError if MajorRadius is negative.
    public void SetMajorRadius ( double MajorRadius){ }
  

  //! Modifies the minor  radius of this hyperbola.
  //! Exceptions
  //! Standard_ConstructionError if MinorRadius is negative.
    public void SetMinorRadius ( double MinorRadius){ }
  
  //! Modifies this hyperbola, by redefining its local coordinate
  //! system so that it becomes A2.
    public void SetPosition ( gp_Ax2 A2){ }
  

  //! In the local coordinate system of the hyperbola the equation of
  //! the hyperbola is (X*X)/(A*A) - (Y*Y)/(B*B) = 1.0 and the
  //! equation of the first asymptote is Y = (B/A)*X
  //! where A is the major radius and B is the minor radius. Raises ConstructionError if MajorRadius = 0.0
    gp_Ax1 Asymptote1(){ }
  

  //! In the local coordinate system of the hyperbola the equation of
  //! the hyperbola is (X*X)/(A*A) - (Y*Y)/(B*B) = 1.0 and the
  //! equation of the first asymptote is Y = -(B/A)*X.
  //! where A is the major radius and B is the minor radius. Raises ConstructionError if MajorRadius = 0.0
    gp_Ax1 Asymptote2(){ }
  
  //! Returns the axis passing through the center,
  //! and normal to the plane of this hyperbola.
     gp_Ax1 Axis(){ }
  

  //! Computes the branch of hyperbola which is on the positive side of the
  //! "YAxis" of <me>.
    gp_Hypr ConjugateBranch1(){ }
  

  //! Computes the branch of hyperbola which is on the negative side of the
  //! "YAxis" of <me>.
    gp_Hypr ConjugateBranch2(){ }
  

  //! This directrix is the line normal to the XAxis of the hyperbola
  //! in the local plane (Z = 0) at a distance d = MajorRadius / e
  //! from the center of the hyperbola, where e is the eccentricity of
  //! the hyperbola.
  //! This line is parallel to the "YAxis". The intersection point
  //! between the directrix1 and the "XAxis" is the "Location" point
  //! of the directrix1. This point is on the positive side of the
  //! "XAxis".
    gp_Ax1 Directrix1(){ }
  

  //! This line is obtained by the symmetrical transformation
  //! of "Directrix1" with respect to the "YAxis" of the hyperbola.
    gp_Ax1 Directrix2(){ }
  

  //! Returns the excentricity of the hyperbola (e > 1).
  //! If f is the distance between the location of the hyperbola
  //! and the Focus1 then the eccentricity e = f / MajorRadius. Raises DomainError if MajorRadius = 0.0
    double Eccentricity(){ }
  

  //! Computes the focal distance. It is the distance between the
  //! the two focus of the hyperbola.
    double Focal(){ }
  

  //! Returns the first focus of the hyperbola. This focus is on the
  //! positive side of the "XAxis" of the hyperbola.
    gp_Pnt Focus1(){ }
  

  //! Returns the second focus of the hyperbola. This focus is on the
  //! negative side of the "XAxis" of the hyperbola.
    gp_Pnt Focus2(){ }
  

  //! Returns  the location point of the hyperbola. It is the
  //! intersection point between the "XAxis" and the "YAxis".
     gp_Pnt Location(){ }
  

  //! Returns the major radius of the hyperbola. It is the radius
  //! on the "XAxis" of the hyperbola.
    double MajorRadius(){ }
  

  //! Returns the minor radius of the hyperbola. It is the radius
  //! on the "YAxis" of the hyperbola.
    double MinorRadius(){ }
  

  //! Returns the branch of hyperbola obtained by doing the
  //! symmetrical transformation of <me> with respect to the
  //! "YAxis"  of <me>.
    gp_Hypr OtherBranch(){ }
  

  //! Returns p = (e * e - 1) * MajorRadius where e is the
  //! eccentricity of the hyperbola.
  //! Raises DomainError if MajorRadius = 0.0
    double Parameter(){ }
  
  //! Returns the coordinate system of the hyperbola.
     gp_Ax2 Position(){ }
  
  //! Computes an axis, whose
  //! -   the origin is the center of this hyperbola, and
  //! -   the unit vector is the "X Direction"
  //! of the local coordinate system of this hyperbola.
  //! These axes are, the major axis (the "X
  //! Axis") and  of this hyperboReturns the "XAxis" of the hyperbola.
    gp_Ax1 XAxis(){ }
  
  //! Computes an axis, whose
  //! -   the origin is the center of this hyperbola, and
  //! -   the unit vector is the "Y Direction"
  //! of the local coordinate system of this hyperbola.
  //! These axes are the minor axis (the "Y Axis") of this hyperbola
    gp_Ax1 YAxis(){ }
  
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of an hyperbola with
  //! respect  to the point P which is the center of the symmetry.
    gp_Hypr Mirrored ( gp_Pnt P){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of an hyperbola with
  //! respect to an axis placement which is the axis of the symmetry.
    gp_Hypr Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs the symmetrical transformation of an hyperbola with
  //! respect to a plane. The axis placement A2 locates the plane
  //! of the symmetry (Location, XDirection, YDirection).
    gp_Hypr Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! Rotates an hyperbola. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Hypr Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Scales an hyperbola. S is the scaling value.
     gp_Hypr Scaled ( gp_Pnt P,  double S){ }
  
    public void Transform ( gp_Trsf T){ }
  

  //! Transforms an hyperbola with the transformation T from
  //! public class Trsf.
     gp_Hypr Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates an hyperbola in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Hypr Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates an hyperbola from the point P1 to the point P2.
     gp_Hypr Translated ( gp_Pnt P1,  gp_Pnt P2){ }




protected:





private:



  gp_Ax2 pos;
  double majorRadius;
  double minorRadius;


};


#include <gp_Hypr.lxx>





#endif // _gp_Hypr_HeaderFile
