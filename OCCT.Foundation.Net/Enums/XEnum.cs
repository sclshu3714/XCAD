﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCCT.Foundation.Net
{
	public enum CurrentModel
	{
		Zero = 0,
		Left = 1,
		Middle = 2,
		Right = 4,
		LeftRight = 5,
		LeftMiddle = 3,
		RightMiddle = 6,
		All = 7
	}

	[Flags]
	public enum CurrentUpdateFlags
	{
		Model = 1,
		Selection = 2,
		Material = 4,
		Camera = 0x10,
		Light = 0x20,
		Scene = 0x40
	}

	[Flags]
	public enum CurrentAction3d
	{
		CurAction3d_Nothing,
		CurAction3d_DynamicZooming,
		CurAction3d_WindowZooming,
		CurAction3d_DynamicPanning,
		CurAction3d_GlobalPanning,
		CurAction3d_DynamicRotation
	}

	[Flags]
	public enum CurrentPressedKey
	{
		CurPressedKey_Nothing,
		CurPressedKey_Ctrl,
		CurPressedKey_Shift,
		CurPressedKey_Alt
	}

	public enum CurrentModelFormat
	{
		BREP,
		STEP,
		IGES,
		VRML,
		STL,
		IMAGE
	}
	/// <summary>
	/// 运动模式
	/// </summary>
	public enum ActivityType
	{
		/// <summary>
		/// 平移
		/// </summary>
		Translation,
		/// <summary>
		/// 旋转
		/// </summary>
		Rotation,
		/// <summary>
		/// 形变
		/// </summary>
		Deformation,
	}
}
