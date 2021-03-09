// Created on: 1993-04-26
// Created by: Jean-Louis Frenkel
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

#ifndef _XPrs3d_ShadingAspect_HeaderFile
#define _XPrs3d_ShadingAspect_HeaderFile
#pragma once
#include <Prs3d_ShadingAspect.hxx>
#include <XPrs3d_BasicAspect.h>
#include <XQuantity_Color.h>
#include <XAspect_TypeOfFacingModel.h>
#include <XGraphic3d_MaterialAspect.h>
#include <XAspect_TypeOfFacingModel.h>

#include <Aspect_TypeOfFacingModel.hxx>
#include <Graphic3d_AspectFillArea3d.hxx>
#include <Graphic3d_MaterialAspect.hxx>
#include <Prs3d_BasicAspect.hxx>

using namespace TKernel;
using namespace TKService;
namespace TKV3d {
	ref class TKernel::XQuantity_Color;
	ref class TKService::XGraphic3d_MaterialAspect;
	ref class XPrs3d_BasicAspect;
	//! A framework to define the display of shading.
	//! The attributes which make up this definition include:
	//! -   fill aspect
	//! -   color, and
	//! -   material
	public ref class XPrs3d_ShadingAspect : public XPrs3d_BasicAspect
	{
	public:

		//! Constructs an empty framework to display shading.
		XPrs3d_ShadingAspect();

		//! Constructor with initialization.
		XPrs3d_ShadingAspect(Handle(Graphic3d_AspectFillArea3d) theAspect);


		!XPrs3d_ShadingAspect();// { };// { IHandle = NULL; };

		~XPrs3d_ShadingAspect();// { IHandle = NULL; };

		//! Constructor with initialization.
		XPrs3d_ShadingAspect(Handle(Prs3d_ShadingAspect) theAspect);

		void SetShadingAspectHandle(Handle(Prs3d_ShadingAspect) pos);

		Handle(Prs3d_BasicAspect) GetBasicAspectHandle() Standard_OVERRIDE;

		Handle(Prs3d_ShadingAspect) GetShadingAspectHandle();

		//! Change the polygons interior color and material ambient color.
		//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_BOTH_SIDE
		void SetColor(XQuantity_Color^ aColor, XAspect_TypeOfFacingModel aModel);

		//! Change the polygons material aspect.
		//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_BOTH_SIDE
		void SetMaterial(XGraphic3d_MaterialAspect^ aMaterial, XAspect_TypeOfFacingModel aModel);

		//! Change the polygons transparency value.
		//! Warning : aValue must be in the range 0,1. 0 is the default (NO transparent)
		//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_BOTH_SIDE
		void SetTransparency(Standard_Real aValue, XAspect_TypeOfFacingModel aModel);

		//! Returns the polygons color.
		//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_FRONT_SIDE
		XQuantity_Color^ Color(XAspect_TypeOfFacingModel aModel);

		//! Returns the polygons material aspect.
		//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_FRONT_SIDE
		XGraphic3d_MaterialAspect^ Material(XAspect_TypeOfFacingModel aModel);

		//! Returns the polygons transparency value.
		//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_FRONT_SIDE
		Standard_Real Transparency(XAspect_TypeOfFacingModel aModel);

		//! Returns the polygons aspect properties.
		Handle(Graphic3d_AspectFillArea3d) Aspect();

		void SetAspect(Handle(Graphic3d_AspectFillArea3d) theAspect);

		//! Dumps the content of me into the stream
		virtual void DumpJson(Standard_OStream theOStream, Standard_Integer theDepth) Standard_OVERRIDE;

		/// <summary>
		/// ±¾µØ¾ä±ú
		/// </summary>
		virtual property Handle(Standard_Transient) IHandle {
			Handle(Standard_Transient) get() Standard_OVERRIDE {
				return NativeHandle();
			}
			void set(Handle(Standard_Transient) handle) Standard_OVERRIDE {
				if (!handle.IsNull())
					NativeHandle() = Handle(Prs3d_ShadingAspect)::DownCast(handle);
				else
					NativeHandle() = NULL;
			}
		}
	private:
		NCollection_Haft<Handle(Prs3d_ShadingAspect)> NativeHandle;
	};
}
#endif // _XPrs3d_ShadingAspect_HeaderFile
