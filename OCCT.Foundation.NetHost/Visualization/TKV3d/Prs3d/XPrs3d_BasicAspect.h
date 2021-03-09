// Created on: 2000-02-15
// Created by: Gerard GRAS
// Copyright (c) 2000-2014 OPEN CASCADE SAS
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

#ifndef _XPrs3d_BasicAspect_HeaderFile
#define _XPrs3d_BasicAspect_HeaderFile
#pragma once
#include <Prs3d_BasicAspect.hxx>
#include <NCollection_Haft.h>

#include <Standard.hxx>
#include <Standard_OStream.hxx>
#include <Standard_Type.hxx>
#include <Standard_Transient.hxx>

namespace TKV3d {
	//! All basic Prs3d_xxxAspect must inherits from this class
	//! The aspect classes qualifies how to represent a given kind of object.
	public ref class XPrs3d_BasicAspect //: public Standard_Transient
	{
    public:
        XPrs3d_BasicAspect();

        XPrs3d_BasicAspect(Handle(Prs3d_BasicAspect) pos);

        void SetBasicAspectHandle(Handle(Prs3d_BasicAspect) pos);

        virtual Handle(Prs3d_BasicAspect) GetBasicAspectHandle();

		//! Dumps the content of me into the stream
		//! const Standard_Integer theDepth = -1
		virtual void DumpJson(Standard_OStream theOStream, Standard_Integer theDepth);

        /// <summary>
        /// ���ؾ��
        /// </summary>
        virtual property Handle(Standard_Transient) IHandle {
            Handle(Standard_Transient) get() {//Standard_OVERRIDE {
                return NativeHandle();
            }
            void set(Handle(Standard_Transient) handle) {//Standard_OVERRIDE {
                if (!handle.IsNull())
                    NativeHandle() = Handle(Prs3d_BasicAspect)::DownCast(handle);
                else
                    NativeHandle() = NULL;
            }
        }
    private:
        NCollection_Haft<Handle(Prs3d_BasicAspect)> NativeHandle;
	};
};
#endif // _XPrs3d_BasicAspect_HeaderFile
