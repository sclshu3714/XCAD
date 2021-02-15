// Created on: 1992-11-13
// Created by: GG
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

#ifndef _XV3d_TypeOfVisualization_HeaderFile
#define _XV3d_TypeOfVisualization_HeaderFile

//! Determines the type of visualization in the view, either
//! WIREFRAME or ZBUFFER (shading).
public enum class XV3d_TypeOfVisualization
{
	V3d_WIREFRAME,
	V3d_ZBUFFER
};

#endif // _XV3d_TypeOfVisualization_HeaderFile