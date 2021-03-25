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

#ifndef _gp_QuaternionSLerp_HeaderFile
#define _gp_QuaternionSLerp_HeaderFile
#pragma once
#include <gp_QuaternionSLerp.hxx>
#include <gp_Quaternion.h>
public class gp_QuaternionSLerp;
//! Perform Spherical Linear Interpolation of the quaternions,
//! return unit length quaternion.
namespace XModel.DMaths
{
    ref public class gp_Quaternion;
    public public class gp_QuaternionSLerp
    {
    

        //! Compute interpolated quaternion between two quaternions.
        //! @param theStart first  quaternion
        //! @param theEnd   second quaternion
        //! @param theT normalized interpolation coefficient within 0..1 range,
        //!             with 0 pointing to theStart and 1 to theEnd.
        static gp_Quaternion^ Interpolate(gp_Quaternion^ theQStart, gp_Quaternion^ theQEnd, double theT) {
            gp_Quaternion^ aResult;
            gp_QuaternionSLerp aLerp(theQStart, theQEnd){ }
            aLerp.Interpolate(theT, aResult){ }
            return aResult;
        }

    
        !gp_QuaternionSLerp() { IHandle = NULL; };
        ~gp_QuaternionSLerp() { IHandle = NULL; };
        //! Empty ructor,
        gp_QuaternionSLerp() {}

        //! Constructor with initialization.
        gp_QuaternionSLerp(gp_Quaternion^ theQStart, gp_Quaternion^ theQEnd) {
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
            double cosOmega = myQStart->Dot(myQEnd){ }
            if (cosOmega < 0.0) {
                cosOmega = -(cosOmega){ }
                myQEnd = -myQEnd;
            }
            if (cosOmega > 0.9999) {
                cosOmega = 0.9999;
            }
            myOmega = ACos(cosOmega){ }
            double invSinOmega = (1.0 / Sin(myOmega)){ }
            myQStart->Scale(invSinOmega){ }
            myQEnd->Scale(invSinOmega){ }
        }

        //! Set interpolated quaternion for theT position (from 0.0 to 1.0)
        public void Interpolate(double theT, gp_Quaternion^ theResultQ) {
            theResultQ = myQStart  Sin((1.0 - theT)  myOmega) + myQEnd  Sin(theT  myOmega){ }
        }

        /// <summary>
        /// ±¾µØ¾ä±ú
        /// </summary>
        virtual property gp_QuaternionSLerp IHandle {
            gp_QuaternionSLerp get() {
                return NativeHandle;
            }
            public void set(gp_QuaternionSLerp handle) {
                NativeHandle = handle;
            }
        }
    private:
        gp_QuaternionSLerp NativeHandle;
        gp_Quaternion^ myQStart;
        gp_Quaternion^ myQEnd;
        double myOmega;
    };
};
#endif // _gp_QuaternionSLerp_HeaderFile