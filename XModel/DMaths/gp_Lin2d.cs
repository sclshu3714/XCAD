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

#ifndef _gp_Lin2d_HeaderFile
#define _gp_Lin2d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Ax2d.hxx>
#include <double.hxx>
#include <Standard_Boolean.hxx>
public class Standard_ConstructionError;
public class gp_Ax2d;
public class gp_Pnt2d;
public class gp_Dir2d;
public class gp_Trsf2d;
public class gp_Vec2d;


//! Describes a line in 2D space.
//! A line is positioned in the plane with an axis (a gp_Ax2d
//! object) which gives the line its origin and unit vector. A
//! line and an axis are similar objects, thus, we can convert
//! one into the other.
//! A line provides direct access to the majority of the edit
//! and query functions available on its positioning axis. In
//! addition, however, a line has specific functions for
//! computing distances and positions.
//! See Also
//! GccAna and Geom2dGcc packages which provide
//! functions for ructing lines defined by geometric
//! raints
//! gce_MakeLin2d which provides functions for more
//! complex line ructions
//! Geom2d_Line which provides additional functions for
//! ructing lines and works, in particular, with the
//! parametric equations of lines
public class gp_Lin2d 
{


  

  
  //! Creates a Line corresponding to X axis of the
  //! reference coordinate system.
    gp_Lin2d(){ }
  
  //! Creates a line located with A.
    gp_Lin2d( gp_Ax2d A){ }
  

  //! <P> is the location point (origin) of the line and
  //! <V> is the direction of the line.
    gp_Lin2d( gp_Pnt2d P,  gp_Dir2d V){ }
  

  //! Creates the line from the equation A*X + B*Y + C = 0.0 Raises ConstructionError if Sqrt(A*A + B*B) <= Resolution from gp.
  //! Raised if Sqrt(A*A + B*B) <= Resolution from gp.
   gp_Lin2d( double A,  double B,  double C){ }
  
    public void Reverse(){ }
  

  //! Reverses the positioning axis of this line.
  //! Note:
  //! -   Reverse assigns the result to this line, while
  //! -   Reversed creates a new one.
     gp_Lin2d Reversed(){ }
  
  //! Changes the direction of the line.
    public void SetDirection ( gp_Dir2d V){ }
  
  //! Changes the origin of the line.
    public void SetLocation ( gp_Pnt2d P){ }
  

  //! Complete redefinition of the line.
  //! The "Location" point of <A> is the origin of the line.
  //! The "Direction" of <A> is  the direction of the line.
    public void SetPosition ( gp_Ax2d A){ }
  

  //! Returns the normalized coefficients of the line :
  //! A * X + B * Y + C = 0.
    public void Coefficients (double A, double B, double C){ }
  
  //! Returns the direction of the line.
     gp_Dir2d Direction(){ }
  
  //! Returns the location point (origin) of the line.
     gp_Pnt2d Location(){ }
  

  //! Returns the axis placement one axis whith the same
  //! location and direction as <me>.
     gp_Ax2d Position(){ }
  
  //! Computes the angle between two lines in radians.
    double Angle ( gp_Lin2d Other){ }
  
  //! Returns true if this line contains the point P, that is, if the
  //! distance between point P and this line is less than or
  //! equal to LinearTolerance.
    Standard_Boolean Contains ( gp_Pnt2d P,  double LinearTolerance){ }
  

  //! Computes the distance between <me> and the point <P>.
    double Distance ( gp_Pnt2d P){ }
  
  //! Computes the distance between two lines.
    double Distance ( gp_Lin2d Other){ }
  

  //! Computes the square distance between <me> and the point
  //! <P>.
    double SquareDistance ( gp_Pnt2d P){ }
  
  //! Computes the square distance between two lines.
    double SquareDistance ( gp_Lin2d Other){ }
  

  //! Computes the line normal to the direction of <me>,
  //! passing through the point <P>.
    gp_Lin2d Normal ( gp_Pnt2d P){ }
  
   public void Mirror ( gp_Pnt2d P){ }
  

  //! Performs the symmetrical transformation of a line
  //! with respect to the point <P> which is the center
  //! of the symmetry
    gp_Lin2d Mirrored ( gp_Pnt2d P){ }
  
   public void Mirror ( gp_Ax2d A){ }
  

  //! Performs the symmetrical transformation of a line
  //! with respect to an axis placement which is the axis
  //! of the symmetry.
    gp_Lin2d Mirrored ( gp_Ax2d A){ }
  
    public void Rotate ( gp_Pnt2d P,  double Ang){ }
  

  //! Rotates a line. P is the center of the rotation.
  //! Ang is the angular value of the rotation in radians.
     gp_Lin2d Rotated ( gp_Pnt2d P,  double Ang){ }
  
  public void Scale ( gp_Pnt2d P,  double S){ }
  

  //! Scales a line. S is the scaling value. Only the
  //! origin of the line is modified.
   gp_Lin2d Scaled ( gp_Pnt2d P,  double S){ }
  
    public void Transform ( gp_Trsf2d T){ }
  

  //! Transforms a line with the transformation T from public class Trsf2d.
     gp_Lin2d Transformed ( gp_Trsf2d T){ }
  
    public void Translate ( gp_Vec2d V){ }
  

  //! Translates a line in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
     gp_Lin2d Translated ( gp_Vec2d V){ }
  
    public void Translate ( gp_Pnt2d P1,  gp_Pnt2d P2){ }
  

  //! Translates a line from the point P1 to the point P2.
     gp_Lin2d Translated ( gp_Pnt2d P1,  gp_Pnt2d P2){ }




protected:





private:



  gp_Ax2d pos;


};


#include <gp_Lin2d.lxx>





#endif // _gp_Lin2d_HeaderFile
