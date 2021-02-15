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

#ifndef _XPrs3d_DatumParts_HeaderFile
#define _XPrs3d_DatumParts_HeaderFile

//! Enumeration defining a part of datum aspect, see Prs3d_Datum.
public enum class XPrs3d_DatumParts
{
  Prs3d_DP_Origin = 0,
  Prs3d_DP_XAxis,
  Prs3d_DP_YAxis,
  Prs3d_DP_ZAxis,
  Prs3d_DP_XArrow,
  Prs3d_DP_YArrow,
  Prs3d_DP_ZArrow,
  Prs3d_DP_XOYAxis,
  Prs3d_DP_YOZAxis,
  Prs3d_DP_XOZAxis,
  Prs3d_DP_None
};

#endif // _XPrs3d_DatumParts_HeaderFile