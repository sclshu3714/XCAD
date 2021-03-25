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

#ifndef _gp_Hypr2d_HeaderFile
#define _gp_Hypr2d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax22d.hxx>
#include <double.hxx>
#include <Standard_Boolean.hxx>
#include <gp_Ax2d.hxx>
#include <gp_Pnt2d.hxx>
public class Standard_ConstructionError;
public class Standard_DomainError;
public class gp_Ax2d;
public class gp_Ax22d;
public class gp_Pnt2d;
public class gp_Trsf2d;
public class gp_Vec2d;



//! Describes a branch of a hyperbola in the plane (2D space).
//! A hyperbola is defined by its major and minor radii, and
//! positioned in the plane with a coordinate system (a
//! gp_Ax22d object) of which:
//! -   the origin is the center of the hyperbola,
//! -   the "X Direction" defines the major axis of the hyperbola, and
//! -   the "Y Direction" defines the minor axis of the hyperbola.
//! This coordinate system is the "local coordinate system"
//! of the hyperbola. The orientation of this coordinate
//! system (direct or indirect) gives an implicit orientation to
//! the hyperbola. In this coordinate system, the equation of
//! the hyperbola is:
//! X*X/(MajorRadius**2)-Y*Y/(MinorRadius**2) = 1.0
//! The branch of the hyperbola described is the one located
//! on the positive side of the major axis.
//! The following schema shows the plane of the hyperbola,
//! and in it, the respective positions of the three branches of
//! hyperbolas ructed with the functions OtherBranch,
//! ConjugateBranch1, and ConjugateBranch2:
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
//! |
//!
//! Warning
//! The major radius can be less than the minor radius.
//! See Also
//! gce_MakeHypr2d which provides functions for more
//! complex hyperbola ructions
//! Geom2d_Hyperbola which provides additional functions
//! for ructing hyperbolas and works, in particular, with
//! the parametric equations of hyperbolas
public class gp_Hypr2d 
{


  

  
  //! Creates of an indefinite hyperbola.
    gp_Hypr2d(){ }
  

  //! Creates a hyperbola with radii MajorRadius and
  //! MinorRadius, centered on the origin of MajorAxis
  //! and where the unit vector of MajorAxis is the "X
  //! Direction" of the local coordinate system of the
  //! hyperbola. This coordinate system is direct if Sense
  //! is true (the default value), and indirect if Sense is false.
  //! Warnings :
  //! It is yet  possible to create an Hyperbola with
  //! MajorRadius <= MinorRadius.
  //! Raises ConstructionError if MajorRadius < 0.0 or MinorRadius < 0.0
  gp_Hypr2d( gp_Ax2d MajorAxis,  double MajorRadius,  double MinorRadius,  Standard_Boolean Sense = Standard_True){ }
  

  //! a hyperbola with radii MajorRadius and
  //! MinorRadius, positioned in the plane by coordinate system A where:
  //! -   the origin of A is the center of the hyperbola,
  //! -   the "X Direction" of A defines the major axis of
  //! the hyperbola, that is, the major radius
  //! MajorRadius is measured along this axis, and
  //! -   the "Y Direction" of A defines the minor axis of
  //! the hyperbola, that is, the minor radius
  //! MinorRadius is measured along this axis, and
  //! -   the orientation (direct or indirect sense) of A
  //! gives the implicit orientation of the hyperbola.
  //! Warnings :
  //! It is yet  possible to create an Hyperbola with
  //! MajorRadius <= MinorRadius.
  //! Raises ConstructionError if MajorRadius < 0.0 or MinorRadius < 0.0
    gp_Hypr2d( gp_Ax22d A,  double MajorRadius,  double MinorRadius){ }
  
  //! Modifies this hyperbola, by redefining its local
  //! coordinate system so that its origin becomes P.
    public void SetLocation ( gp_Pnt2d P){ }
  
  //! Modifies the major or minor radius of this hyperbola.
  //! Exceptions
  //! Standard_ConstructionError if MajorRadius or
  //! MinorRadius is negative.
    public void SetMajorRadius ( double MajorRadius){ }
  
  //! Modifies the major or minor radius of this hyperbola.
  //! Exceptions
  //! Standard_ConstructionError if MajorRadius or
  //! MinorRadius is negative.
    public void SetMinorRadius ( double MinorRadius){ }
  
  //! Modifies this hyperbola, by redefining its local
  //! coordinate system so that it becomes A.
    public void SetAxis ( gp_Ax22d A){ }
  

  //! Changes the major axis of the hyperbola. The minor axis is
  //! recomputed and the location of the hyperbola too.
    public void SetXAxis ( gp_Ax2d A){ }
  

  //! Changes the minor axis of the hyperbola.The minor axis is
  //! recomputed and the location of the hyperbola too.
    public void SetYAxis ( gp_Ax2d A){ }
  

  //! In the local coordinate system of the hyperbola the equation of
  //! the hyperbola is (X*X)/(A*A) - (Y*Y)/(B*B) = 1.0 and the
  //! equation of the first asymptote is Y = (B/A)*X
  //! where A is the major radius of the hyperbola and B the minor
  //! radius of the hyperbola.
  //! Raises ConstructionError if MajorRadius = 0.0
    gp_Ax2d Asymptote1(){ }
  

  //! In the local coordinate system of the hyperbola the equation of
  //! the hyperbola is (X*X)/(A*A) - (Y*Y)/(B*B) = 1.0 and the
  //! equation of the first asymptote is Y = -(B/A)*X
  //! where A is the major radius of the hyperbola and B the minor
  //! radius of the hyperbola.
  //! Raises ConstructionError if MajorRadius = 0.0
    gp_Ax2d Asymptote2(){ }
  

  //! Computes the coefficients of the implicit equation of
  //! the hyperbola :
  //! A * (X**2) + B * (Y**2) + 2*C*(X*Y) + 2*D*X + 2*E*Y + F = 0.
   public void Coefficients (double A, double B, double C, double D, double E, double F){ }
  

  //! Computes the branch of hyperbola which is on the positive side of the
  //! "YAxis" of <me>.
    gp_Hypr2d ConjugateBranch1(){ }
  

  //! Computes the branch of hyperbola which is on the negative side of the
  //! "YAxis" of <me>.
    gp_Hypr2d ConjugateBranch2(){ }
  

  //! Computes the directrix which is the line normal to the XAxis of the hyperbola
  //! in the local plane (Z = 0) at a distance d = MajorRadius / e
  //! from the center of the hyperbola, where e is the eccentricity of
  //! the hyperbola.
  //! This line is parallel to the "YAxis". The intersection point
  //! between the "Directrix1" and the "XAxis" is the "Location" point
  //! of the "Directrix1".
  //! This point is on the positive side of the "XAxis".
    gp_Ax2d Directrix1(){ }
  

  //! This line is obtained by the symmetrical transformation
  //! of "Directrix1" with respect to the "YAxis" of the hyperbola.
    gp_Ax2d Directrix2(){ }
  

  //! Returns the excentricity of the hyperbola (e > 1).
  //! If f is the distance between the location of the hyperbola
  //! and the Focus1 then the eccentricity e = f / MajorRadius. Raises DomainError if MajorRadius = 0.0.
    double Eccentricity(){ }
  

  //! Computes the focal distance. It is the distance between the
  //! "Location" of the hyperbola and "Focus1" or "Focus2".
    double Focal(){ }
  

  //! Returns the first focus of the hyperbola. This focus is on the
  //! positive side of the "XAxis" of the hyperbola.
    gp_Pnt2d Focus1(){ }
  

  //! Returns the second focus of the hyperbola. This focus is on the
  //! negative side of the "XAxis" of the hyperbola.
    gp_Pnt2d Focus2(){ }
  

  //! Returns  the location point of the hyperbola.
  //! It is the intersection point between the "XAxis" and
  //! the "YAxis".
     gp_Pnt2d Location(){ }
  

  //! Returns the major radius of the hyperbola (it is the radius
  //! corresponding to the "XAxis" of the hyperbola).
    double MajorRadius(){ }
  

  //! Returns the minor radius of the hyperbola (it is the radius
  //! corresponding to the "YAxis" of the hyperbola).
    double MinorRadius(){ }
  

  //! Returns the branch of hyperbola obtained by doing the
  //! symmetrical transformation of <me> with respect to the
  //! "YAxis" of <me>.
    gp_Hypr2d OtherBranch(){ }
  

  //! Returns p = (e * e - 1) * MajorRadius where e is the
  //! eccentricity of the hyperbola.
  //! Raises DomainError if MajorRadius = 0.0
    double Parameter(){ }
  
  //! Returns the axisplacement of the hyperbola.
     gp_Ax22d Axis(){ }
  
  //! Computes an axis whose
  //! -   the origin is the center of this hyperbola, and
  //! -   the unit vector is the "X Direction" or "Y Direction"
  //! respectively of the local coordinate system of this hyperbola
  //! Returns the major axis of the hyperbola.
  gp_Ax2d XAxis(){ }
  
  //! Computes an axis whose
  //! -   the origin is the center of this hyperbola, and
  //! -   the unit vector is the "X Direction" or "Y Direction"
  //! respectively of the local coordinate system of this hyperbola
  //! Returns the minor axis of the hyperbola.
    gp_Ax2d YAxis(){ }
  
    public void Reverse(){ }
  
  //! Reverses the orientation of the local coordinate system
  //! of this hyperbola (the "Y Axis" is reversed). Therefore,
  //! the implicit orientation of this hyperbola is reversed.
  //! Note:
  //! -   Reverse assigns the result to this hyperbola, while
  //! -   Reversed creates a new one.
     gp_Hypr2d Reversed(){ }
  
  //! Returns true if the local coordinate system is direct
  //! and false in the other case.
    Standard_Boolean IsDirect(){ }
  
   public void Mirror ( gp_Pnt2d P){ }
  

  //! Performs the symmetrical transformation of an hyperbola with
  //! respect  to the point P which is the center of the symmetry.
    gp_Hypr2d Mirrored ( gp_Pnt2d P){ }
  
   public void Mirror ( gp_Ax2d A){ }
  

  //! Performs the symmetrical transformation of an hyperbola with
  //! respect to an axis placement which is the axis of the symmetry.
    gp_Hypr2d Mirrored ( gp_Ax2d A){ }
  
    public void Rotate ( gp_Pnt2d P,  double Ang){ }
  

  //! Rotates an hyperbola. P is the center of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Hypr2d Rotated ( gp_Pnt2d P,  double Ang){ }
  
    public void Scale ( gp_Pnt2d P,  double S){ }
  

  //! Scales an hyperbola. <S> is the scaling value.
  //! If <S> is positive only the location point is
  //! modified. But if <S> is negative the "XAxis" is
  //! reversed and the "YAxis" too.
     gp_Hypr2d Scaled ( gp_Pnt2d P,  double S){ }
  
    public void Transform ( gp_Trsf2d T){ }
  

  //! Transforms an hyperbola with the transformation T from
  //! public class Trsf2d.
     gp_Hypr2d Transformed ( gp_Trsf2d T){ }
  
    public void Translate ( gp_Vec2d V){ }
  

  //! Translates an hyperbola in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Hypr2d Translated ( gp_Vec2d V){ }
  
    public void Translate ( gp_Pnt2d P1,  gp_Pnt2d P2){ }
  

  //! Translates an hyperbola from the point P1 to the point P2.
     gp_Hypr2d Translated ( gp_Pnt2d P1,  gp_Pnt2d P2){ }




protected:





private:



  gp_Ax22d pos;
  double majorRadius;
  double minorRadius;


};


#include <gp_Hypr2d.lxx>





#endif // _gp_Hypr2d_HeaderFile
