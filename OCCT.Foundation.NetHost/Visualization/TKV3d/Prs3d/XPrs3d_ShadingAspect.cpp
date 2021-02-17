// Copyright (c) 1995-1999 Matra Datavision
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

#include <XPrs3d_ShadingAspect.h>

namespace TKV3d {
	//! Constructs an empty framework to display shading.
	XPrs3d_ShadingAspect::XPrs3d_ShadingAspect() {
		NativeHandle() = new Prs3d_ShadingAspect();
	};

	//! Constructor with initialization.
	XPrs3d_ShadingAspect::XPrs3d_ShadingAspect(Handle(Graphic3d_AspectFillArea3d) theAspect) {
		NativeHandle() = new Prs3d_ShadingAspect(theAspect);
	};

	XPrs3d_ShadingAspect::!XPrs3d_ShadingAspect() { 
	
	};// { IHandle = NULL; };

	XPrs3d_ShadingAspect::~XPrs3d_ShadingAspect() {
		IHandle = NULL;
	};

	//! Constructor with initialization.
	//! Constructor with initialization.
	XPrs3d_ShadingAspect::XPrs3d_ShadingAspect(Handle(Prs3d_ShadingAspect) theAspect) {
		NativeHandle() = theAspect;
	};

	void XPrs3d_ShadingAspect::SetShadingAspectHandle(Handle(Prs3d_ShadingAspect) theAspect) {
		NativeHandle() = theAspect;
	};

	Handle(Prs3d_BasicAspect) XPrs3d_ShadingAspect::GetBasicAspectHandle() {
		return NativeHandle();
	};


	Handle(Prs3d_ShadingAspect) XPrs3d_ShadingAspect::GetShadingAspectHandle() {
		return NativeHandle();
	};

	//! Change the polygons interior color and material ambient color.
	//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_BOTH_SIDE
	void XPrs3d_ShadingAspect::SetColor(XQuantity_Color^ aColor, XAspect_TypeOfFacingModel aModel) {
		NativeHandle()->SetColor(*aColor->GetColor(), safe_cast<Aspect_TypeOfFacingModel>(aModel));
	};

	//! Change the polygons material aspect.
	//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_BOTH_SIDE
	void XPrs3d_ShadingAspect::SetMaterial(XGraphic3d_MaterialAspect^ aMaterial, XAspect_TypeOfFacingModel aModel) {
		NativeHandle()->SetMaterial(*aMaterial->GetMaterialAspect(), safe_cast<Aspect_TypeOfFacingModel>(aModel));
	};

	//! Change the polygons transparency value.
	//! Warning : aValue must be in the range 0,1. 0 is the default (NO transparent)
	//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_BOTH_SIDE
	void XPrs3d_ShadingAspect::SetTransparency(Standard_Real aValue, XAspect_TypeOfFacingModel aModel) {
		NativeHandle()->SetTransparency(aValue, safe_cast<Aspect_TypeOfFacingModel>(aModel));
	};

	//! Returns the polygons color.
	//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_FRONT_SIDE
	XQuantity_Color^ XPrs3d_ShadingAspect::Color(XAspect_TypeOfFacingModel aModel) {
		return gcnew XQuantity_Color(NativeHandle()->Color(safe_cast<Aspect_TypeOfFacingModel>(aModel)));
	};

	//! Returns the polygons material aspect.
	//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_FRONT_SIDE
	XGraphic3d_MaterialAspect^ XPrs3d_ShadingAspect::Material(XAspect_TypeOfFacingModel aModel) {
		Graphic3d_MaterialAspect* temp = new Graphic3d_MaterialAspect(NativeHandle()->Material(safe_cast<Aspect_TypeOfFacingModel>(aModel)));
		return gcnew XGraphic3d_MaterialAspect(temp);
	};

	//! Returns the polygons transparency value.
	//! Aspect_TypeOfFacingModel aModel = Aspect_TOFM_FRONT_SIDE
	Standard_Real XPrs3d_ShadingAspect::Transparency(XAspect_TypeOfFacingModel aModel) {
		return NativeHandle()->Transparency(safe_cast<Aspect_TypeOfFacingModel>(aModel));
	};

	//! Returns the polygons aspect properties.
	Handle(Graphic3d_AspectFillArea3d) XPrs3d_ShadingAspect::Aspect() {
		return NativeHandle()->Aspect();
	};

	void XPrs3d_ShadingAspect::SetAspect(Handle(Graphic3d_AspectFillArea3d) theAspect) {
		NativeHandle()->SetAspect(theAspect);
	};

	//! Dumps the content of me into the stream
	void XPrs3d_ShadingAspect::DumpJson(Standard_OStream theOStream, Standard_Integer theDepth) {
		NativeHandle()->DumpJson(theOStream, theDepth);
	};
}

