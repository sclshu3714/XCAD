namespace XModel.DMaths {
    //! The geometric processor package, called gp, provides an
    //! implementation of entities used  :
    //! . for algebraic calculation such as "XYZ" coordinates, "Mat"
    //! matrix
    //! . for basis analytic geometry such as Transformations, point,
    //! vector, line, plane, axis placement, conics, and elementary
    //! surfaces.
    //! These entities are defined in 2d and 3d space.
    //! All the public classes of this package are non-persistent.
    public class gp
    {
        //! Method of package gp
        //!
        //! In geometric computations, defines the tolerance criterion
        //! used to determine when two numbers can be considered equal.
        //! Many public class functions use this tolerance criterion, for
        //! example, to apublic void division by zero in geometric
        //! computations. In the documentation, tolerance criterion is
        //! always referred to as gp::Resolution().
        public static double Resolution() { return 2.2250738585072014e-308; }

        ////! Identifies a Cartesian point with coordinates X = Y = Z = 0.0.0
        //static gp_Pnt Origin() { }

        ////! Returns a unit vector with the combination (1,0,0)
        //static gp_Dir DX() { }

        ////! Returns a unit vector with the combination (0,1,0)
        //static gp_Dir DY() { }

        ////! Returns a unit vector with the combination (0,0,1)
        //static gp_Dir DZ() { }

        ////! Identifies an axis where its origin is Origin
        ////! and its unit vector coordinates  X = 1.0,  Y = Z = 0.0
        //static gp_Ax1 OX() { }

        ////! Identifies an axis where its origin is Origin
        ////! and its unit vector coordinates Y = 1.0,  X = Z = 0.0
        //static gp_Ax1 OY() { }

        ////! Identifies an axis where its origin is Origin
        ////! and its unit vector coordinates Z = 1.0,  Y = X = 0.0
        //static gp_Ax1 OZ() { }

        ////! Identifies a coordinate system where its origin is Origin,
        ////! and its "main Direction" and "X Direction" coordinates
        ////! Z = 1.0, X = Y =0.0 and X direction coordinates X = 1.0, Y = Z = 0.0
        //static gp_Ax2 XOY() { }

        ////! Identifies a coordinate system where its origin is Origin,
        ////! and its "main Direction" and "X Direction" coordinates
        ////! Y = 1.0, X = Z =0.0 and X direction coordinates Z = 1.0, X = Y = 0.0
        //static gp_Ax2 ZOX() { }

        ////! Identifies a coordinate system where its origin is Origin,
        ////! and its "main Direction" and "X Direction" coordinates
        ////! X = 1.0, Z = Y =0.0 and X direction coordinates Y = 1.0, X = Z = 0.0
        ////! In 2D space
        //static gp_Ax2 YOZ() { }

        ////! Identifies a Cartesian point with coordinates X = Y = 0.0
        //static gp_Pnt2d Origin2d() { }

        ////! Returns a unit vector with the combinations (1,0)
        //static gp_Dir2d DX2d() { }

        ////! Returns a unit vector with the combinations (0,1)
        //static gp_Dir2d DY2d() { }

        ////! Identifies an axis where its origin is Origin2d
        ////! and its unit vector coordinates are: X = 1.0,  Y = 0.0
        //static gp_Ax2d OX2d() { }

        ////! Identifies an axis where its origin is Origin2d
        ////! and its unit vector coordinates are Y = 1.0,  X = 0.0
        //static gp_Ax2d OY2d() { }
    };
//#include <gp.lxx>
}
