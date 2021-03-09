// Created on: 1993-03-24
// Created by: JCV
// Copyright (c) 1993-1999 Matra Datavision
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

#ifndef _XGeom2d_Geometry_HeaderFile
#define _XGeom2d_Geometry_HeaderFile
#pragma once
#include "NCollection_Haft.h"
#include <Geom2d_Geometry.hxx>
#include <xgp_Pnt2d.h>
#include <xgp_Ax2d.h>
#include <xgp_Vec2d.h>
#include <xgp_Trsf2d.h>


#include <Standard.hxx>
#include <Standard_Type.hxx>

#include <Standard_Transient.hxx>
#include <Standard_Real.hxx>
class Standard_ConstructionError;



//class Geom2d_Geometry;
//DEFINE_STANDARD_HANDLE(Geom2d_Geometry, Standard_Transient)


//! The general abstract class Geometry in 2D space describes
//! the common behaviour of all the geometric entities.
//!
//! All the objects derived from this class can be move with a
//! geometric transformation. Only the transformations which
//! doesn't modify the nature of the geometry are available in
//! this package.
//! The method Transform which defines a general transformation
//! is deferred. The other specifics transformations used the
//! method Transform.
//! All the following transformations modify the object itself.
//! Warning
//! Only transformations which do not modify the nature
//! of the geometry can be applied to Geom2d objects:
//! this is the case with translations, rotations,
//! symmetries and scales; this is also the case with
//! gp_Trsf2d composite transformations which are
//! used to define the geometric transformations applied
//! using the Transform or Transformed functions.
//! Note: Geometry defines the "prototype" of the
//! abstract method Transform which is defined for each
//! concrete type of derived object. All other
//! transformations are implemented using the Transform method.
//! 
using namespace TKMath;
namespace TKG2d {
	ref class TKMath::xgp_Pnt2d;
	ref class TKMath::xgp_Ax2d;
	ref class TKMath::xgp_Vec2d;
	ref class TKMath::xgp_Trsf2d;
	public ref class XGeom2d_Geometry //: public Standard_Transient
	{

	public:

		!XGeom2d_Geometry() { IHandle = NULL; };
		~XGeom2d_Geometry() { IHandle = NULL; };

		XGeom2d_Geometry();

		XGeom2d_Geometry(Handle(Geom2d_Geometry) pos);

		void SetGeometryHandle(Handle(Geom2d_Geometry) pos);

		virtual Handle(Geom2d_Geometry) GetGeometry();

		//! Performs the symmetrical transformation of a Geometry
		//! with respect to the point P which is the center of the
		//! symmetry and assigns the result to this geometric object.
		void Mirror(xgp_Pnt2d^ P);

		//! Performs the symmetrical transformation of a Geometry
		//! with respect to an axis placement which is the axis of the symmetry.
		void Mirror(xgp_Ax2d^ A);

		//! Rotates a Geometry. P is the center of the rotation.
		//! Ang is the angular value of the rotation in radians.
		void Rotate(xgp_Pnt2d^ P, Standard_Real Ang);

		//! Scales a Geometry. S is the scaling value.
		void Scale(xgp_Pnt2d^ P, Standard_Real S);

		//! Translates a Geometry.  V is the vector of the tanslation.
		void Translate(xgp_Vec2d^ V);

		//! Translates a Geometry from the point P1 to the point P2.
		void Translate(xgp_Pnt2d^ P1, xgp_Pnt2d^ P2);

		//! Transformation of a geometric object. This tansformation
		//! can be a translation, a rotation, a symmetry, a scaling
		//! or a complex transformation obtained by combination of
		//! the previous elementaries transformations.
		//! (see class Transformation of the package Geom2d).
		//! The following transformations have the same properties
		//! as the previous ones but they don't modified the object
		//! itself. A copy of the object is returned.
		virtual void Transform(xgp_Trsf2d^ T);

		XGeom2d_Geometry^ Mirrored(xgp_Pnt2d^ P);

		XGeom2d_Geometry^ Mirrored(xgp_Ax2d^ A);

		XGeom2d_Geometry^ Rotated(xgp_Pnt2d^ P, Standard_Real Ang);

		XGeom2d_Geometry^ Scaled(xgp_Pnt2d^ P, Standard_Real S);

		XGeom2d_Geometry^ Transformed(xgp_Trsf2d^ T);

		XGeom2d_Geometry^ Translated(xgp_Vec2d^ V);

		XGeom2d_Geometry^ Translated(xgp_Pnt2d^ P1, xgp_Pnt2d^ P2);

		virtual XGeom2d_Geometry^ Copy();

		//! DEFINE_STANDARD_RTTIEXT(Geom2d_Geometry,Standard_Transient)
		/// <summary>
		/// ���ؾ��
		/// </summary>
		virtual property Handle(Standard_Transient) IHandle {
			Handle(Standard_Transient) get() {
				return 	NativeHandle();
			}
			void set(Handle(Standard_Transient) handle) {
				//NativeHandle() = Handle(Geom2d_Geometry)::DownCast(handle);
				if (!handle.IsNull())
					NativeHandle() = Handle(Geom2d_Geometry)::DownCast(handle);
				else
					NativeHandle() = NULL;
			}
		}

	private:
		NCollection_Haft<Handle(Geom2d_Geometry)> NativeHandle;
	};
}
#endif // _XGeom2d_Geometry_HeaderFile
