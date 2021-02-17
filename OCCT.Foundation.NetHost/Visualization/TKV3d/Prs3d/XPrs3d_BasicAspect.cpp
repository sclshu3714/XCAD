#include <XPrs3d_BasicAspect.h>

namespace TKV3d {

	XPrs3d_BasicAspect::XPrs3d_BasicAspect() {

	};

	XPrs3d_BasicAspect::XPrs3d_BasicAspect(Handle(Prs3d_BasicAspect) pos) {
		NativeHandle() = pos;
	};

	void XPrs3d_BasicAspect::SetBasicAspectHandle(Handle(Prs3d_BasicAspect) pos) {
		NativeHandle() = pos;
	};

	Handle(Prs3d_BasicAspect) XPrs3d_BasicAspect::GetBasicAspectHandle() {
		return NativeHandle();
	};

	void XPrs3d_BasicAspect::DumpJson(Standard_OStream theOStream, Standard_Integer theDepth) {
		NativeHandle()->DumpJson(theOStream, theDepth);
	}
}
