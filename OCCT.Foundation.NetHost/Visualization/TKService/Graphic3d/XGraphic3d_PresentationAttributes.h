// Created on: 2016-08-24
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

#ifndef _XGraphic3d_PresentationAttributes_HeaderFile
#define _XGraphic3d_PresentationAttributes_HeaderFile
#pragma once
#include "Graphic3d_PresentationAttributes.hxx"
#include "NCollection_Haft.h"
#include "XAspect_TypeOfHighlightMethod.h"
#include "XQuantity_NameOfColor.h"
#include "XQuantity_Color.h"
#include "XQuantity_ColorRGBA.h"
#include "XGraphic3d_AspectFillArea3d.h"

#include <Aspect_TypeOfHighlightMethod.hxx>
#include <Graphic3d_AspectFillArea3d.hxx>
#include <Graphic3d_ZLayerId.hxx>
#include <Standard_Transient.hxx>
#include <Standard_Type.hxx>
#include <Quantity_ColorRGBA.hxx>

using namespace TKernel;
namespace TKService {
    ref class TKernel::XQuantity_Color;
    ref class TKernel::XQuantity_ColorRGBA;
    ref class XGraphic3d_AspectFillArea3d;
    //! Class defines presentation properties.
    public ref class XGraphic3d_PresentationAttributes    // : public Standard_Transient
    {
        //! DEFINE_STANDARD_RTTIEXT(Graphic3d_PresentationAttributes, Standard_Transient)
    public:

        //! Empty constructor.
        XGraphic3d_PresentationAttributes();

        //! Empty constructor.
        XGraphic3d_PresentationAttributes(Handle(Graphic3d_PresentationAttributes) pos);

        //! Destructor.
        virtual ~XGraphic3d_PresentationAttributes();

        !XGraphic3d_PresentationAttributes() { };//{ IHandle = NULL; };

        virtual Handle(Graphic3d_PresentationAttributes) GetPresentationAttributes();

        void SetNativeHandle(Handle(Graphic3d_PresentationAttributes) pos);

        //! Returns highlight method, Aspect_TOHM_COLOR by default.
        XAspect_TypeOfHighlightMethod Method();

        //! Changes highlight method to the given one.
        virtual void SetMethod(XAspect_TypeOfHighlightMethod theMethod);

        //! Returns basic presentation color (including alpha channel).
        XQuantity_ColorRGBA^ ColorRGBA();

        //! Returns basic presentation color, Quantity_NOC_WHITE by default.
        XQuantity_Color^ Color();

        //! Sets basic presentation color (RGB components, does not modifies transparency).
        virtual void SetColor(XQuantity_Color^ theColor);

        //! Returns basic presentation transparency (0 - opaque, 1 - fully transparent), 0 by default (opaque).
        Standard_ShortReal Transparency();

        //! Sets basic presentation transparency (0 - opaque, 1 - fully transparent).
        virtual void SetTransparency(Standard_ShortReal theTranspCoef);

        //! Returns presentation Zlayer, Graphic3d_ZLayerId_Default by default.
        //! Graphic3d_ZLayerId_UNKNOWN means undefined (a layer of main presentation to be used).
        Graphic3d_ZLayerId ZLayer();

        //! Sets presentation Zlayer.
        virtual void SetZLayer(Graphic3d_ZLayerId theLayer);

        //! Returns display mode, 0 by default.
        //! -1 means undefined (main display mode of presentation to be used).
        Standard_Integer DisplayMode();

        //! Sets display mode.
        virtual void SetDisplayMode(Standard_Integer theMode);

        //! Return basic presentation fill area aspect, NULL by default.
        //! When set, might be used instead of Color() property.
        XGraphic3d_AspectFillArea3d^ BasicFillAreaAspect();

        //! Sets basic presentation fill area aspect.
        virtual void SetBasicFillAreaAspect(XGraphic3d_AspectFillArea3d^ theAspect);

        /// <summary>
        /// ±¾µØ¾ä±ú
        /// </summary>
        virtual property Handle(Standard_Transient) IHandle {
            Handle(Standard_Transient) get() {//Standard_OVERRIDE {
                return NativeHandle();
            }
            void set(Handle(Standard_Transient) handle) {//Standard_OVERRIDE {
                if (!handle.IsNull())
                    NativeHandle() = Handle(Graphic3d_PresentationAttributes)::DownCast(handle);
                else
                    NativeHandle() = NULL;
            }
        };
    private:
        NCollection_Haft<Handle(Graphic3d_PresentationAttributes)> NativeHandle;

    };

    //!DEFINE_STANDARD_HANDLE(Graphic3d_PresentationAttributes, Standard_Transient)
}
#endif // _XGraphic3d_PresentationAttributes_HeaderFile
