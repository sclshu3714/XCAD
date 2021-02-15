// Created on: 1992-08-26
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

#ifndef _xgce_MakeHypr_HeaderFile
#define _xgce_MakeHypr_HeaderFile
#pragma once
#include <gce_MakeHypr.hxx>
#include <xgce_Root.h>
#include <xgp_Ax2.h>
#include <xgp_Pnt.h>
#include <xgp_Hypr.h>

#include <Standard.hxx>
#include <Standard_DefineAlloc.hxx>
#include <Standard_Handle.hxx>

#include <gp_Hypr.hxx>
#include <gce_Root.hxx>
#include <Standard_Real.hxx>
class StdFail_NotDone;
class gp_Ax2;
class gp_Pnt;
class gp_Hypr;

using namespace TKMath;
namespace TKGeomBase {
	ref class TKMath::xgp_Ax2;
	ref class TKMath::xgp_Pnt;
	ref class TKMath::xgp_Hypr;
	//! This class implements the following algorithms used to
	//! create Hyperbola from gp.
	//! * Create an Hyperbola from its center, and two points:
	//! one on its axis of symmetry giving the major radius, the
	//! other giving the value of the small radius.
	//! The three points give the plane of the hyperbola.
	//! * Create an hyperbola from its axisplacement and its
	//! MajorRadius and its MinorRadius.
	//!
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
	//! The local cartesian coordinate system of the ellipse is an
	//! axis placement (two axis).
	//!
	//! The "XDirection" and the "YDirection" of the axis placement
	//! define the plane of the hyperbola.
	//!
	//! The "Direction" of the axis placement defines the normal axis
	//! to the hyperbola's plane.
	//!
	//! The "XAxis" of the hyperbola ("Location", "XDirection") is the
	//! major axis  and the  "YAxis" of the hyperbola ("Location",
	//! "YDirection") is the minor axis.
	//!
	//! Warnings :
	//! The major radius (on the major axis) can be lower than the
	//! minor radius (on the minor axis).
	public ref class xgce_MakeHypr : public xgce_Root
	{
	public:

		//DEFINE_STANDARD_ALLOC

		xgce_MakeHypr();
		!xgce_MakeHypr() { IHandle = NULL; };
		~xgce_MakeHypr() { IHandle = NULL; };
		xgce_MakeHypr(gce_MakeHypr* pos);

		void SetMakeHypr(gce_MakeHypr* pos);

		virtual gce_MakeHypr* GetMakeHypr();

		virtual gce_Root* GetRoot() Standard_OVERRIDE;


		//! A2 is the local coordinate system of the hyperbola.
		//! In the local coordinates system A2 the equation of the
		//! hyperbola is :
		//! X*X / MajorRadius*MajorRadius - Y*Y / MinorRadius*MinorRadius = 1.0
		//! It is not forbidden to create an Hyperbola with MajorRadius =
		//! MinorRadius.
		//! For the hyperbola the MajorRadius can be lower than the
		//! MinorRadius.
		//! The status is "NegativeRadius" if MajorRadius < 0.0 and
		//! "InvertRadius" if MinorRadius > MajorRadius.
		xgce_MakeHypr(xgp_Ax2^ A2, Standard_Real MajorRadius, Standard_Real MinorRadius);

		//! Constructs a hyperbola
		//! -   centered on the point Center, where:
		//! -   the plane of the hyperbola is defined by Center, S1 and S2,
		//! -   its major axis is defined by Center and S1,
		//! -   its major radius is the distance between Center and S1, and
		//! -   its minor radius is the distance between S2 and the major axis.
		//! Warning
		//! If an error occurs (that is, when IsDone returns
		//! false), the Status function returns:
		//! -   gce_NegativeRadius if MajorRadius is less than 0.0;
		//! -   gce_InvertRadius if:
		//! -   the major radius (computed with Center, S1) is
		//! less than the minor radius (computed with Center, S1 and S2), or
		//! -   MajorRadius is less than MinorRadius; or
		//! -   gce_ColinearPoints if S1, S2 and Center are collinear.
		xgce_MakeHypr(xgp_Pnt^ S1, xgp_Pnt^ S2, xgp_Pnt^ Center);

		//! Returns theructed hyperbola.
		//! Exceptions StdFail_NotDone if no hyperbola isructed.
		xgp_Hypr^ Value();

		xgp_Hypr^ Operator();
		operator xgp_Hypr^();
		//! Returns true if the construction is successful.
		virtual Standard_Boolean IsDone() Standard_OVERRIDE;

		//! Returns the status of the construction:
		//! -   gce_Done, if the construction is successful, or
		//! -   another value of the gce_ErrorType enumeration
		//! indicating why the construction failed.
		virtual xgce_ErrorType Status() Standard_OVERRIDE;

		/// <summary>
		/// ���ؾ��
		/// </summary>
		property gce_MakeHypr* IHandle {
			gce_MakeHypr* get() {
				return 	NativeHandle;
			}
			void set(gce_MakeHypr* handle) {
				NativeHandle = handle;
			}
		}

	private:
		gce_MakeHypr* NativeHandle;
	};
}
#endif // _xgce_MakeHypr_HeaderFile