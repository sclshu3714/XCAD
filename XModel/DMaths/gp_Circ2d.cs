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

#ifndef _gp_Circ2d_HeaderFile
#define _gp_Circ2d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax22d.hxx>
#include <double.hxx>
#include <Standard_Boolean.hxx>
#include <gp_Ax2d.hxx>
public class Standard_ConstructionError;
public class gp_Ax2d;
public class gp_Ax22d;
public class gp_Pnt2d;
public class gp_Trsf2d;
public class gp_Vec2d;


//! Describes a circle in the plane (2D space).
//! A circle is defined by its radius and positioned in the
//! plane with a coordinate system (a gp_Ax22d object) as follows:
//! -   the origin of the coordinate system is the center of the circle, and
//! -   the orientation (direct or indirect) of the coordinate
//! system gives an implicit orientation to the circle (and
//! defines its trigonometric sense).
//! This positioning coordinate system is the "local
//! coordinate system" of the circle.
//! Note: when a gp_Circ2d circle is converted into a
//! Geom2d_Circle circle, some implicit properties of the
//! circle are used explicitly:
//! -   the implicit orientation corresponds to the direction in
//! which parameter values increase,
//! -   the starting point for parameterization is that of the "X
//! Axis" of the local coordinate system (i.e. the "X Axis" of the circle).
//! See Also
//! GccAna and Geom2dGcc packages which provide
//! functions for ructing circles defined by geometric raints
//! gce_MakeCirc2d which provides functions for more
//! complex circle ructions
//! Geom2d_Circle which provides additional functions for
//! ructing circles and works, with the parametric
//! equations of circles in particular  gp_Ax22d
public class gp_Circ2d 
{


  

  
  //! creates an indefinite circle.
    gp_Circ2d(){ }
  

  //! The location point of XAxis is the center of the circle.
  //! Warnings :
  //! It is not forbidden to create a circle with Radius = 0.0   Raises ConstructionError if Radius < 0.0.
  //! Raised if Radius < 0.0.
    gp_Circ2d( gp_Ax2d XAxis,  double Radius,  Standard_Boolean Sense = Standard_True){ }
  

  //! Axis defines the Xaxis and Yaxis of the circle which defines
  //! the origin and the sense of parametrization.
  //! The location point of Axis is the center of the circle.
  //! Warnings :
  //! It is not forbidden to create a circle with Radius = 0.0 Raises ConstructionError if Radius < 0.0.
  //! Raised if Radius < 0.0.
    gp_Circ2d( gp_Ax22d Axis,  double Radius){ }
  
  //! Changes the location point (center) of the circle.
  public void SetLocation ( gp_Pnt2d P){ }
  
  //! Changes the X axis of the circle.
    public void SetXAxis ( gp_Ax2d A){ }
  
  //! Changes the X axis of the circle.
    public void SetAxis ( gp_Ax22d A){ }
  
  //! Changes the Y axis of the circle.
    public void SetYAxis ( gp_Ax2d A){ }
  
  //! Modifies the radius of this circle.
  //! This public class does not prevent the creation of a circle where
  //! Radius is null.
  //! Exceptions
  //! Standard_ConstructionError if Radius is negative.
    public void SetRadius ( double Radius){ }
  
  //! Computes the area of the circle.
    double Area(){ }
  

  //! Returns the normalized coefficients from the implicit equation
  //! of the circle :
  //! A * (X**2) + B * (Y**2) + 2*C*(X*Y) + 2*D*X + 2*E*Y + F = 0.0
    public void Coefficients (double A, double B, double C, double D, double E, double F){ }
  
  //! Does <me> contain P ?
  //! Returns True if the distance between P and any point on
  //! the circumference of the circle is lower of equal to
  //! <LinearTolerance>.
    Standard_Boolean Contains ( gp_Pnt2d P,  double LinearTolerance){ }
  

  //! Computes the minimum of distance between the point P and any
  //! point on the circumference of the circle.
    double Distance ( gp_Pnt2d P){ }
  

  //! Computes the square distance between <me> and the point P.
    double SquareDistance ( gp_Pnt2d P){ }
  
  //! computes the circumference of the circle.
    double Length(){ }
  
  //! Returns the location point (center) of the circle.
     gp_Pnt2d Location(){ }
  
  //! Returns the radius value of the circle.
    double Radius(){ }
  
  //! returns the position of the circle.
     gp_Ax22d Axis(){ }
  
  //! returns the position of the circle. Idem Axis(me).
     gp_Ax22d Position(){ }
  
  //! returns the X axis of the circle.
    gp_Ax2d XAxis(){ }
  
  //! Returns the Y axis of the circle.
  //! Reverses the direction of the circle.
    gp_Ax2d YAxis(){ }
  
  //! Reverses the orientation of the local coordinate system
  //! of this circle (the "Y Direction" is reversed) and therefore
  //! changes the implicit orientation of this circle.
  //! Reverse assigns the result to this circle,
    public void Reverse(){ }
  
  //! Reverses the orientation of the local coordinate system
  //! of this circle (the "Y Direction" is reversed) and therefore
  //! changes the implicit orientation of this circle.
  //! Reversed creates a new circle.
     gp_Circ2d Reversed(){ }
  
  //! Returns true if the local coordinate system is direct
  //! and false in the other case.
    Standard_Boolean IsDirect(){ }
  
   public void Mirror ( gp_Pnt2d P){ }
  

  //! Performs the symmetrical transformation of a circle with respect
  //! to the point P which is the center of the symmetry
    gp_Circ2d Mirrored ( gp_Pnt2d P){ }
  
   public void Mirror ( gp_Ax2d A){ }
  

  //! Performs the symmetrical transformation of a circle with respect
  //! to an axis placement which is the axis of the symmetry.
    gp_Circ2d Mirrored ( gp_Ax2d A){ }
  
    public void Rotate ( gp_Pnt2d P,  double Ang){ }
  

  //! Rotates a circle. P is the center of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Circ2d Rotated ( gp_Pnt2d P,  double Ang){ }
  
    public void Scale ( gp_Pnt2d P,  double S){ }
  

  //! Scales a circle. S is the scaling value.
  //! Warnings :
  //! If S is negative the radius stay positive but
  //! the "XAxis" and the "YAxis" are  reversed as for
  //! an ellipse.
     gp_Circ2d Scaled ( gp_Pnt2d P,  double S){ }
  
    public void Transform ( gp_Trsf2d T){ }
  

  //! Transforms a circle with the transformation T from public class Trsf2d.
     gp_Circ2d Transformed ( gp_Trsf2d T){ }
  
    public void Translate ( gp_Vec2d V){ }
  

  //! Translates a circle in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Circ2d Translated ( gp_Vec2d V){ }
  
    public void Translate ( gp_Pnt2d P1,  gp_Pnt2d P2){ }
  

  //! Translates a circle from the point P1 to the point P2.
     gp_Circ2d Translated ( gp_Pnt2d P1,  gp_Pnt2d P2){ }




protected:





private:



  gp_Ax22d pos;
  double radius;


};


#include <gp_Circ2d.lxx>





#endif // _gp_Circ2d_HeaderFile
