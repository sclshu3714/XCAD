// Created on: 1991-11-04
// Created by: NW,JPB,CAL
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

#ifndef _XGraphic3d_AspectFillArea3d_HeaderFile
#define _XGraphic3d_AspectFillArea3d_HeaderFile
#pragma once
#include <Graphic3d_AspectFillArea3d.hxx>
#include <XGraphic3d_Aspects.h>
#include <XQuantity_Color.h>



using namespace TKernel;
namespace TKService {
    ref class TKernel::XQuantity_Color;
    //! This class defines graphic attributes for opaque 3d primitives (polygons, triangles, quadrilaterals).
    public ref class XGraphic3d_AspectFillArea3d : public XGraphic3d_Aspects
    {
    public:

        !XGraphic3d_AspectFillArea3d();// { };// { IHandle = NULL; };

        ~XGraphic3d_AspectFillArea3d();// { IHandle = NULL; };

        XGraphic3d_AspectFillArea3d(Handle(Graphic3d_AspectFillArea3d) pos);

        void SetAspectFillArea3dHandle(Handle(Graphic3d_AspectFillArea3d) pos);

        Handle(Graphic3d_AspectFillArea3d) GetAspectFillArea3dHandle();

        Handle(Graphic3d_Aspects) GetAspectsHandle() Standard_OVERRIDE;

        //! Creates a context table for fill area primitives defined with the following default values:
        //!
        //! InteriorStyle : Aspect_IS_EMPTY
        //! InteriorColor : Quantity_NOC_CYAN1
        //! EdgeColor     : Quantity_NOC_WHITE
        //! EdgeLineType  : Aspect_TOL_SOLID
        //! EdgeWidth     : 1.0
        //! FrontMaterial : NOM_BRASS
        //! BackMaterial  : NOM_BRASS
        //! HatchStyle    : Aspect_HS_SOLID
        //!
        //! Display of back-facing filled polygons.
        //! No distinction between external and internal faces of FillAreas.
        //! The edges are not drawn.
        //! Polygon offset parameters: mode = Aspect_POM_None, factor = 1., units = 0.
        XGraphic3d_AspectFillArea3d();

        //! Creates a context table for fill area primitives defined with the specified values.
        //! Display of back-facing filled polygons.
        //! No distinction between external and internal faces of FillAreas.
        //! The edges are not drawn.
        //! Polygon offset parameters: mode = Aspect_POM_None, factor = 1., units = 0.
        XGraphic3d_AspectFillArea3d(XAspect_InteriorStyle theInterior,XQuantity_Color^ theInteriorColor, XQuantity_Color^ theEdgeColor,
            XAspect_TypeOfLine theEdgeLineType, Standard_Real theEdgeWidth, XGraphic3d_MaterialAspect^ theFrontMaterial, XGraphic3d_MaterialAspect^ theBackMaterial);

        /// <summary>
        /// ±¾µØ¾ä±ú
        /// </summary>
        virtual property Handle(Standard_Transient) IHandle {
            Handle(Standard_Transient) get() Standard_OVERRIDE {
                return NativeHandle();
            }
            void set(Handle(Standard_Transient) handle) Standard_OVERRIDE {
                if (!handle.IsNull())
                    NativeHandle() = Handle(Graphic3d_AspectFillArea3d)::DownCast(handle);
                else
                    NativeHandle() = NULL;
            }
        }
    private:
        NCollection_Haft<Handle(Graphic3d_AspectFillArea3d)> NativeHandle;
    };
};
#endif // _XGraphic3d_AspectFillArea3d_HeaderFile
