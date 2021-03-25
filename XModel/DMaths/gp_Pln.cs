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

#ifndef _gp_Pln_HeaderFile
#define _gp_Pln_HeaderFile

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
public class gp_Dir;
public class gp_Ax1;
public class gp_Lin;
public class gp_Ax2;
public class gp_Trsf;
public class gp_Vec;


//! Describes a plane.
//! A plane is positioned in space with a coordinate system
//! (a gp_Ax3 object), such that the plane is defined by the
//! origin, "X Direction" and "Y Direction" of this coordinate
//! system, which is the "local coordinate system" of the
//! plane. The "main Direction" of the coordinate system is a
//! vector normal to the plane. It gives the plane an implicit
//! orientation such that the plane is said to be "direct", if the
//! coordinate system is right-handed, or "indirect" in the other case.
//! Note: when a gp_Pln plane is converted into a
//! Geom_Plane plane, some implicit properties of its local
//! coordinate system are used explicitly:
//! -   its origin defines the origin of the two parameters of
//! the planar surface,
//! -   its implicit orientation is also that of the Geom_Plane.
//! See Also
//! gce_MakePln which provides functions for more complex
//! plane ructions
//! Geom_Plane which provides additional functions for
//! ructing planes and works, in particular, with the
//! parametric equations of planes
public class gp_Pln 
{


  

  
  //! Creates a plane coincident with OXY plane of the
  //! reference coordinate system.
    gp_Pln(){ }
  

  //! The coordinate system of the plane is defined with the axis
  //! placement A3.
  //! The "Direction" of A3 defines the normal to the plane.
  //! The "Location" of A3 defines the location (origin) of the plane.
  //! The "XDirection" and "YDirection" of A3 define the "XAxis" and
  //! the "YAxis" of the plane used to parametrize the plane.
    gp_Pln( gp_Ax3 A3){ }
  

  //! Creates a plane with the  "Location" point <P>
  //! and the normal direction <V>.
   gp_Pln( gp_Pnt P,  gp_Dir V){ }
  

  //! Creates a plane from its cartesian equation :
  //! A * X + B * Y + C * Z + D = 0.0
  //! Raises ConstructionError if Sqrt (A*A + B*B + C*C) <= Resolution from gp.
   gp_Pln( double A,  double B,  double C,  double D){ }
  

  //! Returns the coefficients of the plane's cartesian equation :
  //! A * X + B * Y + C * Z + D = 0.
    public void Coefficients (double A, double B, double C, double D){ }
  
  //! Modifies this plane, by redefining its local coordinate system so that
  //! -   its origin and "main Direction" become those of the
  //! axis A1 (the "X Direction" and "Y Direction" are then recomputed).
  //! Raises ConstructionError if the A1 is parallel to the "XAxis" of the plane.
    public void SetAxis ( gp_Ax1 A1){ }
  
  //! Changes the origin of the plane.
    public void SetLocation ( gp_Pnt Loc){ }
  
  //! Changes the local coordinate system of the plane.
    public void SetPosition ( gp_Ax3 A3){ }
  
  //! Reverses the   U   parametrization of   the  plane
  //! reversing the XAxis.
    public void UReverse(){ }
  
  //! Reverses the   V   parametrization of   the  plane
  //! reversing the YAxis.
    public void VReverse(){ }
  
  //! returns true if the Ax3 is right handed.
    Standard_Boolean Direct(){ }
  
  //! Returns the plane's normal Axis.
     gp_Ax1 Axis(){ }
  
  //! Returns the plane's location (origin).
     gp_Pnt Location(){ }
  
  //! Returns the local coordinate system of the plane .
     gp_Ax3 Position(){ }
  
  //! Computes the distance between <me> and the point <P>.
    double Distance ( gp_Pnt P){ }
  
  //! Computes the distance between <me> and the line <L>.
    double Distance ( gp_Lin L){ }
  
  //! Computes the distance between two planes.
    double Distance ( gp_Pln Other){ }
  

  //! Computes the square distance between <me> and the point <P>.
    double SquareDistance ( gp_Pnt P){ }
  

  //! Computes the square distance between <me> and the line <L>.
    double SquareDistance ( gp_Lin L){ }
  

  //! Computes the square distance between two planes.
    double SquareDistance ( gp_Pln Other){ }
  
  //! Returns the X axis of the plane.
    gp_Ax1 XAxis(){ }
  
  //! Returns the Y axis  of the plane.
    gp_Ax1 YAxis(){ }
  
  //! Returns true if this plane contains the point P. This means that
  //! -   the distance between point P and this plane is less
  //! than or equal to LinearTolerance, or
  //! -   line L is normal to the "main Axis" of the local
  //! coordinate system of this plane, within the tolerance
  //! AngularTolerance, and the distance between the origin
  //! of line L and this plane is less than or equal to
  //! LinearTolerance.
    Standard_Boolean Contains ( gp_Pnt P,  double LinearTolerance){ }
  
  //! Returns true if this plane contains the line L. This means that
  //! -   the distance between point P and this plane is less
  //! than or equal to LinearTolerance, or
  //! -   line L is normal to the "main Axis" of the local
  //! coordinate system of this plane, within the tolerance
  //! AngularTolerance, and the distance between the origin
  //! of line L and this plane is less than or equal to
  //! LinearTolerance.
    Standard_Boolean Contains ( gp_Lin L,  double LinearTolerance,  double AngularTolerance){ }
  
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of a plane with respect
  //! to the point <P> which is the center of the symmetry
  //! Warnings :
  //! The normal direction to the plane is not changed.
  //! The "XAxis" and the "YAxis" are reversed.
    gp_Pln Mirrored ( gp_Pnt P){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  
  //! Performs   the symmetrical transformation  of a
  //! plane with respect to an axis placement  which is the axis
  //! of  the symmetry.  The  transformation is performed on the
  //! "Location" point, on  the "XAxis"  and the "YAxis".    The
  //! resulting normal  direction  is  the cross product between
  //! the "XDirection" and the "YDirection" after transformation
  //! if  the  initial plane was right  handed,  else  it is the
  //! opposite.
    gp_Pln Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  
  //! Performs the  symmetrical transformation  of  a
  //! plane    with respect to    an axis  placement.   The axis
  //! placement  <A2> locates the plane  of  the symmetry.   The
  //! transformation is performed  on  the  "Location" point, on
  //! the  "XAxis" and  the    "YAxis".  The resulting    normal
  //! direction is the cross  product between   the "XDirection"
  //! and the "YDirection"  after  transformation if the initial
  //! plane was right handed, else it is the opposite.
    gp_Pln Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! rotates a plane. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Pln Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Scales a plane. S is the scaling value.
     gp_Pln Scaled ( gp_Pnt P,  double S){ }
  
    public void Transform ( gp_Trsf T){ }
  

  //! Transforms a plane with the transformation T from public class Trsf.
  //! The transformation is performed on the "Location"
  //! point, on the "XAxis" and the "YAxis".
  //! The resulting normal direction is the cross product between
  //! the "XDirection" and the "YDirection" after transformation.
     gp_Pln Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates a plane in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Pln Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates a plane from the point P1 to the point P2.
     gp_Pln Translated ( gp_Pnt P1,  gp_Pnt P2){ }


  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }



protected:





private:



  gp_Ax3 pos;


};


#include <gp_Pln.lxx>





#endif // _gp_Pln_HeaderFile
