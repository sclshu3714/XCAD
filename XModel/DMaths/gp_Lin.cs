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

#ifndef _gp_Lin_HeaderFile
#define _gp_Lin_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax1.hxx>
#include <double.hxx>
#include <Standard_Boolean.hxx>
public class Standard_ConstructionError;
public class gp_Ax1;
public class gp_Pnt;
public class gp_Dir;
public class gp_Ax2;
public class gp_Trsf;
public class gp_Vec;



//! Describes a line in 3D space.
//! A line is positioned in space with an axis (a gp_Ax1
//! object) which gives it an origin and a unit vector.
//! A line and an axis are similar objects, thus, we can
//! convert one into the other. A line provides direct access
//! to the majority of the edit and query functions available
//! on its positioning axis. In addition, however, a line has
//! specific functions for computing distances and positions.
//! See Also
//! gce_MakeLin which provides functions for more complex
//! line ructions
//! Geom_Line which provides additional functions for
//! ructing lines and works, in particular, with the
//! parametric equations of lines
public class gp_Lin 
{


  

  
  //! Creates a Line corresponding to Z axis of the
  //! reference coordinate system.
    gp_Lin(){ }
  
  //! Creates a line defined by axis A1.
    gp_Lin( gp_Ax1 A1){ }
  
  //! Creates a line passing through point P and parallel to
  //! vector V (P and V are, respectively, the origin and
  //! the unit vector of the positioning axis of the line).
  gp_Lin( gp_Pnt P,  gp_Dir V){ }

    public void Reverse(){ }
  
  //! Reverses the direction of the line.
  //! Note:
  //! -   Reverse assigns the result to this line, while
  //! -   Reversed creates a new one.
     gp_Lin Reversed(){ }
  
  //! Changes the direction of the line.
    public void SetDirection ( gp_Dir V){ }
  
  //! Changes the location point (origin) of the line.
    public void SetLocation ( gp_Pnt P){ }
  

  //! Complete redefinition of the line.
  //! The "Location" point of <A1> is the origin of the line.
  //! The "Direction" of <A1> is  the direction of the line.
    public void SetPosition ( gp_Ax1 A1){ }
  
  //! Returns the direction of the line.
     gp_Dir Direction(){ }
  

  //! Returns the location point (origin) of the line.
     gp_Pnt Location(){ }
  

  //! Returns the axis placement one axis whith the same
  //! location and direction as <me>.
     gp_Ax1 Position(){ }
  
  //! Computes the angle between two lines in radians.
    double Angle ( gp_Lin Other){ }
  
  //! Returns true if this line contains the point P, that is, if the
  //! distance between point P and this line is less than or
  //! equal to LinearTolerance..
    Standard_Boolean Contains ( gp_Pnt P,  double LinearTolerance){ }
  
  //! Computes the distance between <me> and the point P.
    double Distance ( gp_Pnt P){ }
  
  //! Computes the distance between two lines.
   double Distance ( gp_Lin Other){ }
  

  //! Computes the square distance between <me> and the point P.
    double SquareDistance ( gp_Pnt P){ }
  
  //! Computes the square distance between two lines.
    double SquareDistance ( gp_Lin Other){ }
  

  //! Computes the line normal to the direction of <me>, passing
  //! through the point P.  Raises ConstructionError
  //! if the distance between <me> and the point P is lower
  //! or equal to Resolution from gp because there is an infinity of
  //! solutions in 3D space.
    gp_Lin Normal ( gp_Pnt P){ }
  
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of a line
  //! with respect to the point P which is the center of
  //! the symmetry.
    gp_Lin Mirrored ( gp_Pnt P){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of a line
  //! with respect to an axis placement which is the axis
  //! of the symmetry.
    gp_Lin Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Performs the symmetrical transformation of a line
  //! with respect to a plane. The axis placement  <A2>
  //! locates the plane of the symmetry :
  //! (Location, XDirection, YDirection).
    gp_Lin Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  

  //! Rotates a line. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Lin Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  

  //! Scales a line. S is the scaling value.
  //! The "Location" point (origin) of the line is modified.
  //! The "Direction" is reversed if the scale is negative.
     gp_Lin Scaled ( gp_Pnt P,  double S){ }
  
    public void Transform ( gp_Trsf T){ }
  

  //! Transforms a line with the transformation T from public class Trsf.
     gp_Lin Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates a line in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Lin Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  

  //! Translates a line from the point P1 to the point P2.
     gp_Lin Translated ( gp_Pnt P1,  gp_Pnt P2){ }




protected:





private:



  gp_Ax1 pos;


};


#include <gp_Lin.lxx>





#endif // _gp_Lin_HeaderFile
