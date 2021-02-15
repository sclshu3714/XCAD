// Created on: 1992-09-28
// Created by: Remi GILET
// Copyright (c) 1992-1999 Matra Datavision
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

#ifndef _XGC_MakeTranslation_HeaderFile
#define _XGC_MakeTranslation_HeaderFile
#pragma once
#include <GC_MakeTranslation.hxx>
#include <XGC_Root.h>
#include <xgp_Pnt.h >
#include <xgp_Vec.h >
#include <XGeom_Transformation.h>


#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

class Geom_Transformation;
class gp_Vec;
class gp_Pnt;

using namespace TKMath;
using namespace TKG3d;
namespace TKGeomBase {
	ref class TKMath::xgp_Pnt;
	ref class TKMath::xgp_Vec;
	ref class TKG3d::XGeom_Transformation;
	//! This class implements elementary construction algorithms for a
	//! translation in 3D space. The result is a
	//! Geom_Transformation transformation.
	//! A MakeTranslation object provides a framework for:
	//! -   defining the construction of the transformation,
	//! -   implementing the construction algorithm, and
	//! -   consulting the result.
	public ref class XGC_MakeTranslation
	{
	public:


		!XGC_MakeTranslation() { IHandle = NULL; };
		~XGC_MakeTranslation() { IHandle = NULL; };
		//! DEFINE_STANDARD_ALLOC
		XGC_MakeTranslation();

		XGC_MakeTranslation(GC_MakeTranslation* pos);

		void SetMakeTranslation(GC_MakeTranslation* pos);

		virtual GC_MakeTranslation* GetMakeTranslation();

		//! Constructs a translation along the vector " Vect "
		XGC_MakeTranslation(xgp_Vec^ Vect);

		//! Constructs a translation along the vector (Point1,Point2)
		//! defined from the point Point1 to the point Point2.
		XGC_MakeTranslation(xgp_Pnt^ Point1, xgp_Pnt^ Point2);

		//! Returns the constructed transformation.
		XGeom_Transformation^ Value();
		operator XGeom_Transformation^() { return Value(); }

		/// <summary>
		/// ���ؾ��
		/// </summary>
		property  GC_MakeTranslation* IHandle {
			GC_MakeTranslation* get() {
				return 	NativeHandle;
			}
			void set(GC_MakeTranslation* handle) {
				NativeHandle = handle;
			}
		}

	private:
		GC_MakeTranslation* NativeHandle;
	};
}
#endif // _XGC_MakeTranslation_HeaderFile