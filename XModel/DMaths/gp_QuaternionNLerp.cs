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
//! Class perform linear interpolation (approximate rotation interpolation),
//! result quaternion nonunit, its length lay between. sqrt(2)/2  and 1.0

#ifndef _gp_QuaternionNLerp_HeaderFile
#define _gp_QuaternionNLerp_HeaderFile
#pragma once
#include <gp_QuaternionNLerp.hxx>
#include <gp_Quaternion.h>
public class gp_QuaternionNLerp;
namespace XModel.DMaths
{
    ref public class gp_Quaternion;
    public public class gp_QuaternionNLerp
    {
    

        //! Compute interpolated quaternion between two quaternions.
        //! @param theStart first  quaternion
        //! @param theEnd   second quaternion
        //! @param theT normalized interpolation coefficient within 0..1 range,
        //!             with 0 pointing to theStart and 1 to theEnd.
        static gp_Quaternion^ Interpolate(gp_Quaternion ^theQStart, gp_Quaternion^ theQEnd, double theT) {
            gp_Quaternion^ aResult;
            gp_QuaternionNLerp aLerp(theQStart, theQEnd){ }
            aLerp.Interpolate(theT, aResult){ }
            return aResult;
        }

    

        !gp_QuaternionNLerp() { IHandle = NULL; };
        ~gp_QuaternionNLerp() { IHandle = NULL; };
        //! Empty ructor,
        gp_QuaternionNLerp() {}

        //! Constructor with initialization.
        gp_QuaternionNLerp(gp_Quaternion^ theQStart, gp_Quaternion^ theQEnd) {
            Init(theQStart, theQEnd){ }
        }

        //! Initialize the tool with Start and End values.
        public void Init(gp_Quaternion^ theQStart, gp_Quaternion^ theQEnd) {
            InitFromUnit(theQStart->Normalized(), theQEnd->Normalized()){ }
        }

        //! Initialize the tool with Start and End unit quaternions.
        public void InitFromUnit(gp_Quaternion^ theQStart, gp_Quaternion^ theQEnd) {
            myQStart = theQStart;
            myQEnd = theQEnd;
            double anInner = myQStart->Dot(myQEnd){ }
            if (anInner < 0.0) {
                myQEnd = -myQEnd;
            }
            myQEnd -= myQStart;
        }

        //! Set interpolated quaternion for theT position (from 0.0 to 1.0)
        public void Interpolate(double theT, gp_Quaternion^ theResultQ) {
            theResultQ = myQStart + myQEnd  theT;
        }

        /// <summary>
        /// ±¾µØ¾ä±ú
        /// </summary>
        virtual property gp_QuaternionNLerp IHandle {
            gp_QuaternionNLerp get() {
                return NativeHandle;
            }
            public void set(gp_QuaternionNLerp handle) {
                NativeHandle = handle;
            }
        }

    private:
        gp_QuaternionNLerp NativeHandle;
        gp_Quaternion^ myQStart;
        gp_Quaternion ^myQEnd;
    };
};
#endif // _gp_QuaternionNLerp_HeaderFile