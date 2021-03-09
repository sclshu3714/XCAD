#include <XBRepBuilderAPI_MakeShape.h>

namespace TKTopAlgo {

	XBRepBuilderAPI_MakeShape::XBRepBuilderAPI_MakeShape() {
		//NativeHandle = new BRepBuilderAPI_MakeShape();
	};

	void XBRepBuilderAPI_MakeShape::SetMakeShapeHandle(BRepBuilderAPI_MakeShape* handle) {
		NativeHandle = handle;// new BRepBuilderAPI_MakeShape(*handle);
	};

	BRepBuilderAPI_MakeShape* XBRepBuilderAPI_MakeShape::GetMakeShape() {
		return IHandle;
	};

	//! This is  called by  Shape().  It does  nothing but
	//! may be redefined.
	void XBRepBuilderAPI_MakeShape::Build() {
		IHandle->Build();
	};

	//! Returns a shape built by the shape construction algorithm.
	//! Raises exception StdFail_NotDone if the shape was not built.
	XTopoDS_Shape^ XBRepBuilderAPI_MakeShape::Shape() {
		TopoDS_Shape* Shape = new TopoDS_Shape(IHandle->Shape());
		return gcnew XTopoDS_Shape(Shape);
	};

	XBRepBuilderAPI_MakeShape::operator XTopoDS_Shape^()
	{
		TopoDS_Shape* Shape = new TopoDS_Shape(IHandle->Shape());
		return gcnew XTopoDS_Shape(Shape);
	}


	//! Returns the  list   of shapes generated   from the
	//! shape <S>.
	XTopTools_ListOfShape^ XBRepBuilderAPI_MakeShape::Generated(XTopoDS_Shape^ S) {
		TopTools_ListOfShape* ListOfShape = new TopTools_ListOfShape(IHandle->Generated(*S->GetShape()));
		return gcnew XTopTools_ListOfShape(ListOfShape);
	};

	//! Returns the list  of shapes modified from the shape
	//! <S>.
	XTopTools_ListOfShape^ XBRepBuilderAPI_MakeShape::Modified(XTopoDS_Shape^ S) {
		TopTools_ListOfShape* temp = new TopTools_ListOfShape(IHandle->Modified(*S->GetShape()));
		return gcnew XTopTools_ListOfShape(temp);
	};

	//! Returns true if the shape S has been deleted.
	Standard_Boolean XBRepBuilderAPI_MakeShape::IsDeleted(XTopoDS_Shape^ S) {
		return IHandle->IsDeleted(*S->GetShape());
	};
	Standard_Boolean XBRepBuilderAPI_MakeShape::IsDone() {
		return IHandle->IsDone();
	};

	//! Raises NotDone if done is false.
	void XBRepBuilderAPI_MakeShape::Check() {
		IHandle->Check();
	};
}



