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

#ifndef _XAspect_GridDrawMode_HeaderFile
#define _XAspect_GridDrawMode_HeaderFile

//! Defines the grid draw mode. The grid may be drawn
//! by using lines or points.
public enum class XAspect_GridDrawMode
{
	Aspect_GDM_Lines,
	Aspect_GDM_Points,
	Aspect_GDM_None
};

#endif // _XAspect_GridDrawMode_HeaderFile