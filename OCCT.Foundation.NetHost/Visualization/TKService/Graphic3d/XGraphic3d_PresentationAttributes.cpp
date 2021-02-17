// Created on: 2016-09-23
// Created by: Varvara POSKONINA
// Copyright (c) 2016 OPEN CASCADE SAS
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

#include <XGraphic3d_PresentationAttributes.h>

namespace TKService {
    //! Empty constructor.
    XGraphic3d_PresentationAttributes::XGraphic3d_PresentationAttributes() {
        NativeHandle() = new Graphic3d_PresentationAttributes();
    };

    //! Empty constructor.
    XGraphic3d_PresentationAttributes::XGraphic3d_PresentationAttributes(Handle(Graphic3d_PresentationAttributes) pos) {
        NativeHandle() = pos;
    };

    //! Destructor.
    XGraphic3d_PresentationAttributes::~XGraphic3d_PresentationAttributes() {
        NativeHandle()->~Graphic3d_PresentationAttributes();
    };

    Handle(Graphic3d_PresentationAttributes) XGraphic3d_PresentationAttributes::GetPresentationAttributes() {
        return NativeHandle();
    };

    void XGraphic3d_PresentationAttributes::SetNativeHandle(Handle(Graphic3d_PresentationAttributes) pos) {
        NativeHandle() = pos;
    };

    //! Returns highlight method, Aspect_TOHM_COLOR by default.
    XAspect_TypeOfHighlightMethod XGraphic3d_PresentationAttributes::Method() {
        return safe_cast<XAspect_TypeOfHighlightMethod>(NativeHandle()->Method());
    };

    //! Changes highlight method to the given one.
    void XGraphic3d_PresentationAttributes::SetMethod(XAspect_TypeOfHighlightMethod theMethod) {
        NativeHandle()->SetMethod(safe_cast<Aspect_TypeOfHighlightMethod>(theMethod));
    };

    //! Returns basic presentation color (including alpha channel).
    XQuantity_ColorRGBA^ XGraphic3d_PresentationAttributes::ColorRGBA() {
        Quantity_ColorRGBA* temp = new Quantity_ColorRGBA(NativeHandle()->ColorRGBA());
        return gcnew XQuantity_ColorRGBA(temp);
    };

    //! Returns basic presentation color, Quantity_NOC_WHITE by default.
    XQuantity_Color^ XGraphic3d_PresentationAttributes::Color() {
        return gcnew  XQuantity_Color(NativeHandle()->Color());
    };

    //! Sets basic presentation color (RGB components, does not modifies transparency).
    void XGraphic3d_PresentationAttributes::SetColor(XQuantity_Color^ theColor) {
        NativeHandle()->SetColor(*theColor->GetColor());
    };

    //! Returns basic presentation transparency (0 - opaque, 1 - fully transparent), 0 by default (opaque).
    Standard_ShortReal XGraphic3d_PresentationAttributes::Transparency() {
        return NativeHandle()->Transparency();
    };

    //! Sets basic presentation transparency (0 - opaque, 1 - fully transparent).
    void XGraphic3d_PresentationAttributes::SetTransparency(Standard_ShortReal theTranspCoef) {
        NativeHandle()->SetTransparency(theTranspCoef);
    };

    //! Returns presentation Zlayer, Graphic3d_ZLayerId_Default by default.
    //! Graphic3d_ZLayerId_UNKNOWN means undefined (a layer of main presentation to be used).
    Graphic3d_ZLayerId XGraphic3d_PresentationAttributes::ZLayer() {
        return NativeHandle()->ZLayer();
    };

    //! Sets presentation Zlayer.
    void XGraphic3d_PresentationAttributes::SetZLayer(Graphic3d_ZLayerId theLayer) {
        NativeHandle()->SetZLayer(theLayer);
    };

    //! Returns display mode, 0 by default.
    //! -1 means undefined (main display mode of presentation to be used).
    Standard_Integer XGraphic3d_PresentationAttributes::DisplayMode() {
        return NativeHandle()->DisplayMode();
    };

    //! Sets display mode.
    void XGraphic3d_PresentationAttributes::SetDisplayMode(Standard_Integer theMode) {
        NativeHandle()->SetDisplayMode(theMode);
    };

    //! Return basic presentation fill area aspect, NULL by default.
    //! When set, might be used instead of Color() property.
    XGraphic3d_AspectFillArea3d^ XGraphic3d_PresentationAttributes::BasicFillAreaAspect() {
        Handle(Graphic3d_AspectFillArea3d) temp = NativeHandle()->BasicFillAreaAspect();
        if (!temp.IsNull())
            return gcnew XGraphic3d_AspectFillArea3d(temp);
        else
            return gcnew XGraphic3d_AspectFillArea3d();
    };

    //! Sets basic presentation fill area aspect.
    void XGraphic3d_PresentationAttributes::SetBasicFillAreaAspect(XGraphic3d_AspectFillArea3d^ theAspect) {
        NativeHandle()->SetBasicFillAreaAspect(theAspect->GetAspectFillArea3dHandle());
    };
}

//! IMPLEMENT_STANDARD_RTTIEXT (Graphic3d_PresentationAttributes, Standard_Transient)
