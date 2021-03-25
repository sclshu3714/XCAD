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

#ifndef _gp_Pnt_HeaderFile
#define _gp_Pnt_HeaderFile

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_XYZ.hxx>
#include <double.hxx>
#include <int.hxx>
#include <Standard_Boolean.hxx>
public class Standard_OutOfRange;
public class gp_XYZ;
public class gp_Ax1;
public class gp_Ax2;
public class gp_Trsf;
public class gp_Vec;


//! Defines a 3D cartesian point.
public class gp_Pnt 
{


  

  
  //! Creates a point with zero coordinates.
    gp_Pnt(){ }
  
  //! Creates a point from a XYZ object.
    gp_Pnt( gp_XYZ Coord){ }
  

  //! Creates a  point with its 3 cartesian's coordinates : Xp, Yp, Zp.
    gp_Pnt( double Xp,  double Yp,  double Zp){ }
  

  //! Changes the coordinate of range Index :
  //! Index = 1 => X is modified
  //! Index = 2 => Y is modified
  //! Index = 3 => Z is modified
  //! Raised if Index != {1, 2, 3}.
    public void SetCoord ( int Index,  double Xi){ }
  
  //! For this point, assigns  the values Xp, Yp and Zp to its three coordinates.
    public void SetCoord ( double Xp,  double Yp,  double Zp){ }
  
  //! Assigns the given value to the X coordinate of this point.
    public void SetX ( double X){ }
  
  //! Assigns the given value to the Y coordinate of this point.
    public void SetY ( double Y){ }
  
  //! Assigns the given value to the Z coordinate of this point.
    public void SetZ ( double Z){ }
  
  //! Assigns the three coordinates of Coord to this point.
    public void SetXYZ ( gp_XYZ Coord){ }
  

  //! Returns the coordinate of corresponding to the value of  Index :
  //! Index = 1 => X is returned
  //! Index = 2 => Y is returned
  //! Index = 3 => Z is returned
  //! Raises OutOfRange if Index != {1, 2, 3}.
  //! Raised if Index != {1, 2, 3}.
    double Coord ( int Index){ }
  
  //! For this point gives its three coordinates Xp, Yp and Zp.
    public void Coord (double Xp, double Yp, double Zp){ }
  
  //! For this point, returns its X coordinate.
    double X(){ }
  
  //! For this point, returns its Y coordinate.
    double Y(){ }
  
  //! For this point, returns its Z coordinate.
    double Z(){ }
  
  //! For this point, returns its three coordinates as a XYZ object.
     gp_XYZ XYZ(){ }
  
  //! For this point, returns its three coordinates as a XYZ object.
     gp_XYZ Coord(){ }
  

  //! Returns the coordinates of this point.
  //! Note: This syntax allows direct modification of the returned value.
    gp_XYZ ChangeCoord(){ }
  
  //! Assigns the result of the following expression to this point
  //! (Alpha*this + Beta*P) / (Alpha + Beta)
    public void BaryCenter ( double Alpha,  gp_Pnt P,  double Beta){ }
  
  //! Comparison
  //! Returns True if the distance between the two points is
  //! lower or equal to LinearTolerance.
    Standard_Boolean IsEqual ( gp_Pnt Other,  double LinearTolerance){ }
  
  //! Computes the distance between two points.
    double Distance ( gp_Pnt Other){ }
  
  //! Computes the square distance between two points.
    double SquareDistance ( gp_Pnt Other){ }
  

  //! Performs the symmetrical transformation of a point
  //! with respect to the point P which is the center of
  //! the  symmetry.
   public void Mirror ( gp_Pnt P){ }
  

  //! Performs the symmetrical transformation of a point
  //! with respect to an axis placement which is the axis
  //! of the symmetry.
    gp_Pnt Mirrored ( gp_Pnt P){ }
  
   public void Mirror ( gp_Ax1 A1){ }
  

  //! Performs the symmetrical transformation of a point
  //! with respect to a plane. The axis placement A2 locates
  //! the plane of the symmetry : (Location, XDirection, YDirection).
    gp_Pnt Mirrored ( gp_Ax1 A1){ }
  
   public void Mirror ( gp_Ax2 A2){ }
  

  //! Rotates a point. A1 is the axis of the rotation.
  //! Ang is the angular value of the rotation in radians.
    gp_Pnt Mirrored ( gp_Ax2 A2){ }
  
    public void Rotate ( gp_Ax1 A1,  double Ang){ }
  
  //! Scales a point. S is the scaling value.
     gp_Pnt Rotated ( gp_Ax1 A1,  double Ang){ }
  
    public void Scale ( gp_Pnt P,  double S){ }
  
  //! Transforms a point with the transformation T.
     gp_Pnt Scaled ( gp_Pnt P,  double S){ }
  
   public void Transform ( gp_Trsf T){ }
  

  //! Translates a point in the direction of the vector V.
  //! The magnitude of the translation is the vector's magnitude.
   gp_Pnt Transformed ( gp_Trsf T){ }
  
    public void Translate ( gp_Vec V){ }
  

  //! Translates a point from the point P1 to the point P2.
     gp_Pnt Translated ( gp_Vec V){ }
  
    public void Translate ( gp_Pnt P1,  gp_Pnt P2){ }
  
     gp_Pnt Translated ( gp_Pnt P1,  gp_Pnt P2){ }


  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }

  //! Inits the content of me from the stream
   Standard_Boolean InitFromJson ( Standard_SStream theSStream, int theStreamPos){ }

protected:





private:



  gp_XYZ coord;


};


#include <gp_Pnt.lxx>





#endif // _gp_Pnt_HeaderFile
