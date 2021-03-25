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

#ifndef _gp_Parab2d_HeaderFile
#define _gp_Parab2d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax22d.hxx>
#include <double.hxx>
#include <Standard_Boolean.hxx>
#include <gp_Ax2d.hxx>
#include <gp_Pnt2d.hxx>
public class Standard_ConstructionError;
public class gp_Ax2d;
public class gp_Ax22d;
public class gp_Pnt2d;
public class gp_Trsf2d;
public class gp_Vec2d;


//! Describes a parabola in the plane (2D space).
//! A parabola is defined by its focal length (that is, the
//! distance between its focus and apex) and positioned in
//! the plane with a coordinate system (a gp_Ax22d object) where:
//! -   the origin of the coordinate system is on the apex of
//! the parabola, and
//! -   the "X Axis" of the coordinate system is the axis of
//! symmetry; the parabola is on the positive side of this axis.
//! This coordinate system is the "local coordinate system"
//! of the parabola. Its orientation (direct or indirect sense)
//! gives an implicit orientation to the parabola.
//! In this coordinate system, the equation for the parabola is:
//! Y**2 = (2*P) * X.
//! where P, referred to as the parameter of the parabola, is
//! the distance between the focus and the directrix (P is
//! twice the focal length).
//! See Also
//! GCE2d_MakeParab2d which provides functions for
//! more complex parabola ructions
//! Geom2d_Parabola which provides additional functions
//! for ructing parabolas and works, in particular, with
//! the parametric equations of parabolas
public class gp_Parab2d 
{


  

  
  //! Creates an indefinite parabola.
  gp_Parab2d(){ }
  

  //! Creates a parabola with its vertex point, its axis of symmetry
  //! ("XAxis") and its focal length.
  //! The sense of parametrization is given by theSense. If theSense == TRUE
  //! (by default) then right-handed coordinate system is used,
  //! otherwise - left-handed.
  //! Warnings : It is possible to have FocalLength = 0. In this case,
  //! the parabola looks like a line, which is parallel to the symmetry-axis.
  //! Raises ConstructionError if FocalLength < 0.0
  gp_Parab2d( gp_Ax2d theMirrorAxis,
              double theFocalLength,
              Standard_Boolean theSense = Standard_True){ }
  

  //! Creates a parabola with its vertex point, its axis of symmetry
  //! ("XAxis"), correspond Y-axis and its focal length.
  //! Warnings : It is possible to have FocalLength = 0. In this case,
  //! the parabola looks like a line, which is parallel to the symmetry-axis.
  //! Raises ConstructionError if Focal < 0.0
  gp_Parab2d( gp_Ax22d theAxes,  double theFocalLength){ }
  

  //! Creates a parabola with the directrix and the focus point.
  //! Y-axis of the parabola (in User Coordinate System - UCS) is
  //! the direction of theDirectrix. X-axis always directs from theDirectrix
  //! to theFocus point and always comes through theFocus.
  //! Apex of the parabola is a middle point between the theFocus and the
  //! intersection point of theDirectrix and the X-axis.
  //! Warnings : It is possible to have FocalLength = 0 (when theFocus lies
  //! in theDirectrix). In this case, X-direction of the parabola is defined 
  //! by theSense parameter. If theSense == TRUE (by default) then right-handed
  //! coordinate system is used, otherwise - left-handed. Result parabola will look
  //! like a line, which is perpendicular to the directrix.
   gp_Parab2d( gp_Ax2d theDirectrix,
                              gp_Pnt2d theFocus,
                              Standard_Boolean theSense = Standard_True){ }
  
  //! Changes the focal distance of the parabola
  //! Warnings : It is possible to have Focal = 0.
  //! Raises ConstructionError if Focal < 0.0
    public void SetFocal ( double Focal){ }
  

  //! Changes the "Location" point of the parabola. It is the
  //! vertex of the parabola.
    public void SetLocation ( gp_Pnt2d P){ }
  
  //! Modifies this parabola, by redefining its local coordinate system so that
  //! its origin and "X Direction" become those of the axis
  //! MA. The "Y Direction" of the local coordinate system is
  //! then recomputed. The orientation of the local
  //! coordinate system is not modified.
    public void SetMirrorAxis ( gp_Ax2d A){ }
  

  //! Changes the local coordinate system of the parabola.
  //! The "Location" point of A becomes the vertex of the parabola.
    public void SetAxis ( gp_Ax22d A){ }
  

  //! Computes the coefficients of the implicit equation of the parabola
  //! (in WCS - World Coordinate System).
  //! A * (X**2) + B * (Y**2) + 2*C*(X*Y) + 2*D*X + 2*E*Y + F = 0.
   public void Coefficients (double A, double B,
                                     double C, double D,
                                     double E, double F){ }
  

  //! Computes the directrix of the parabola.
  //! The directrix is:
  //! -   a line parallel to the "Y Direction" of the local
  //! coordinate system of this parabola, and
  //! -   located on the negative side of the axis of symmetry,
  //! at a distance from the apex which is equal to the focal  length of this parabola.
  //! The directrix is returned as an axis (a gp_Ax2d object),
  //! the origin of which is situated on the "X Axis" of this parabola.
    gp_Ax2d Directrix(){ }
  

  //! Returns the distance between the vertex and the focus
  //! of the parabola.
    double Focal(){ }
  
  //! Returns the focus of the parabola.
    gp_Pnt2d Focus(){ }
  
  //! Returns the vertex of the parabola.
    gp_Pnt2d Location(){ }
  

  //! Returns the symmetry axis of the parabola.
  //! The "Location" point of this axis is the vertex of the parabola.
    gp_Ax2d MirrorAxis(){ }
  

  //! Returns the local coordinate system of the parabola.
  //! The "Location" point of this axis is the vertex of the parabola.
    gp_Ax22d Axis(){ }
  

  //! Returns the distance between the focus and the
  //! directrix of the parabola.
    double Parameter(){ }
  
    public void Reverse(){ }
  

  //! Reverses the orientation of the local coordinate system
  //! of this parabola (the "Y Direction" is reversed).
  //! Therefore, the implicit orientation of this parabola is reversed.
  //! Note:
  //! -   Reverse assigns the result to this parabola, while
  //! -   Reversed creates a new one.
     gp_Parab2d Reversed(){ }
  
  //! Returns true if the local coordinate system is direct
  //! and false in the other case.
    Standard_Boolean IsDirect(){ }
  
   public void Mirror ( gp_Pnt2d P){ }
  

  //! Performs the symmetrical transformation of a parabola with respect
  //! to the point P which is the center of the symmetry
    gp_Parab2d Mirrored ( gp_Pnt2d P){ }
  
   public void Mirror ( gp_Ax2d A){ }
  

  //! Performs the symmetrical transformation of a parabola with respect
  //! to an axis placement which is the axis of the symmetry.
    gp_Parab2d Mirrored ( gp_Ax2d A){ }
  
    public void Rotate ( gp_Pnt2d P,  double Ang){ }
  

  //! Rotates a parabola. P is the center of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Parab2d Rotated ( gp_Pnt2d P,  double Ang){ }
  
    public void Scale ( gp_Pnt2d P,  double S){ }
  

  //! Scales a parabola. S is the scaling value.
  //! If S is negative the direction of the symmetry axis
  //! "XAxis" is reversed and the direction of the "YAxis" too.
     gp_Parab2d Scaled ( gp_Pnt2d P,  double S){ }
  
    public void Transform ( gp_Trsf2d T){ }
  

  //! Transforms an parabola with the transformation T from public class Trsf2d.
     gp_Parab2d Transformed ( gp_Trsf2d T){ }
  
    public void Translate ( gp_Vec2d V){ }
  

  //! Translates a parabola in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Parab2d Translated ( gp_Vec2d V){ }
  
    public void Translate ( gp_Pnt2d P1,  gp_Pnt2d P2){ }
  

  //! Translates a parabola from the point P1 to the point P2.
     gp_Parab2d Translated ( gp_Pnt2d P1,  gp_Pnt2d P2){ }




protected:





private:



  gp_Ax22d pos;
  double focalLength;


};


#include <gp_Parab2d.lxx>





#endif // _gp_Parab2d_HeaderFile
