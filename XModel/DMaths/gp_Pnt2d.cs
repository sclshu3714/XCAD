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

#ifndef _gp_Pnt2d_HeaderFile
#define _gp_Pnt2d_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_XY.hxx>
#include <double.hxx>
#include <int.hxx>
#include <Standard_Boolean.hxx>
public class Standard_OutOfRange;
public class gp_XY;
public class gp_Ax2d;
public class gp_Trsf2d;
public class gp_Vec2d;


//! Defines  a non-persistent 2D cartesian point.
public class gp_Pnt2d 
{


  

  
  //! Creates a point with zero coordinates.
    gp_Pnt2d(){ }
  
  //! Creates a point with a doublet of coordinates.
    gp_Pnt2d( gp_XY Coord){ }
  

  //! Creates a  point with its 2 cartesian's coordinates : Xp, Yp.
    gp_Pnt2d( double Xp,  double Yp){ }
  

  //! Assigns the value Xi to the coordinate that corresponds to Index:
  //! Index = 1 => X is modified
  //! Index = 2 => Y is modified
  //! Raises OutOfRange if Index != {1, 2}.
    public void SetCoord ( int Index,  double Xi){ }
  
  //! For this point, assigns the values Xp and Yp to its two coordinates
    public void SetCoord ( double Xp,  double Yp){ }
  
  //! Assigns the given value to the X  coordinate of this point.
    public void SetX ( double X){ }
  
  //! Assigns the given value to the Y  coordinate of this point.
    public void SetY ( double Y){ }
  
  //! Assigns the two coordinates of Coord to this point.
    public void SetXY ( gp_XY Coord){ }
  

  //! Returns the coordinate of range Index :
  //! Index = 1 => X is returned
  //! Index = 2 => Y is returned
  //! Raises OutOfRange if Index != {1, 2}.
  double Coord ( int Index){ }
  
  //! For this point returns its two coordinates as a number pair.
    public void Coord (double Xp, double Yp){ }
  
  //! For this point, returns its X  coordinate.
    double X(){ }
  
  //! For this point, returns its Y coordinate.
    double Y(){ }
  
  //! For this point, returns its two coordinates as a number pair.
     gp_XY XY(){ }
  
  //! For this point, returns its two coordinates as a number pair.
     gp_XY Coord(){ }
  

  //! Returns the coordinates of this point.
  //! Note: This syntax allows direct modification of the returned value.
    gp_XY ChangeCoord(){ }
  
  //! Comparison
  //! Returns True if the distance between the two
  //! points is lower or equal to LinearTolerance.
    Standard_Boolean IsEqual ( gp_Pnt2d Other,  double LinearTolerance){ }
  
  //! Computes the distance between two points.
    double Distance ( gp_Pnt2d Other){ }
  
  //! Computes the square distance between two points.
    double SquareDistance ( gp_Pnt2d Other){ }
  

  //! Performs the symmetrical transformation of a point
  //! with respect to the point P which is the center of
  //! the  symmetry.
   public void Mirror ( gp_Pnt2d P){ }
  

  //! Performs the symmetrical transformation of a point
  //! with respect to an axis placement which is the axis
    gp_Pnt2d Mirrored ( gp_Pnt2d P){ }
  
   public void Mirror ( gp_Ax2d A){ }
  

  //! Rotates a point. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
    gp_Pnt2d Mirrored ( gp_Ax2d A){ }
  
    public void Rotate ( gp_Pnt2d P,  double Ang){ }
  
  //! Scales a point. S is the scaling value.
     gp_Pnt2d Rotated ( gp_Pnt2d P,  double Ang){ }
  
    public void Scale ( gp_Pnt2d P,  double S){ }
  
  //! Transforms a point with the transformation T.
     gp_Pnt2d Scaled ( gp_Pnt2d P,  double S){ }
  
   public void Transform ( gp_Trsf2d T){ }
  

  //! Translates a point in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
   gp_Pnt2d Transformed ( gp_Trsf2d T){ }
  
    public void Translate ( gp_Vec2d V){ }
  

  //! Translates a point from the point P1 to the point P2.
     gp_Pnt2d Translated ( gp_Vec2d V){ }
  
    public void Translate ( gp_Pnt2d P1,  gp_Pnt2d P2){ }
  
     gp_Pnt2d Translated ( gp_Pnt2d P1,  gp_Pnt2d P2){ }


  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }



protected:





private:



  gp_XY coord;


};


#include <gp_Pnt2d.lxx>





#endif // _gp_Pnt2d_HeaderFile
