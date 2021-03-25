namespace XModel.DMaths
{
    //! Describes a unit vector in the plane (2D space). This unit
    //! vector is also called "Direction".
    //! See Also
    //! gce_MakeDir2d which provides functions for more
    //! complex unit vector ructions
    //! Geom2d_Direction which provides additional functions
    //! for ructing unit vectors and works, in particular, with
    //! the parametric equations of unit vectors
  public public class gp_Dir2d 
{

        //! Creates a direction corresponding to X axis.
        public gp_Dir2d(){ }

        //! Normalizes the vector V and creates a Direction. Raises ConstructionError if V.Magnitude() <= Resolution from gp.
        public gp_Dir2d( gp_Vec2d V){ }
  
  //! Creates a Direction from a doublet of coordinates. Raises ConstructionError if Coord.Modulus() <= Resolution from gp.
    public gp_Dir2d( gp_XY Coord){ }
  
  //! Creates a Direction with its 2 cartesian coordinates. Raises ConstructionError if Sqrt(Xv*Xv + Yv*Yv) <= Resolution from gp.
    public gp_Dir2d( double Xv,  double Yv){ }
  

  //! For this unit vector, assigns:
  //! the value Xi to:
  //! -   the X coordinate if Index is 1, or
  //! -   the Y coordinate if Index is 2, and then normalizes it.
  //! Warning
  //! Remember that all the coordinates of a unit vector are
  //! implicitly modified when any single one is changed directly.
  //! Exceptions
  //! Standard_OutOfRange if Index is not 1 or 2.
  //! Standard_ConstructionError if either of the following
  //! is less than or equal to gp::Resolution():
  //! -   Sqrt(Xv*Xv + Yv*Yv), or
  //! -   the modulus of the number pair formed by the new
  //! value Xi and the other coordinate of this vector that
  //! was not directly modified.
  //! Raises OutOfRange if Index != {1, 2}.
    public void SetCoord ( int Index,  double Xi){ }
  

  //! For this unit vector, assigns:
  //! -   the values Xv and Yv to its two coordinates,
  //! Warning
  //! Remember that all the coordinates of a unit vector are
  //! implicitly modified when any single one is changed directly.
  //! Exceptions
  //! Standard_OutOfRange if Index is not 1 or 2.
  //! Standard_ConstructionError if either of the following
  //! is less than or equal to gp::Resolution():
  //! -   Sqrt(Xv*Xv + Yv*Yv), or
  //! -   the modulus of the number pair formed by the new
  //! value Xi and the other coordinate of this vector that
  //! was not directly modified.
  //! Raises OutOfRange if Index != {1, 2}.
    public void SetCoord ( double Xv,  double Yv){ }
  

  //! Assigns the given value to the X coordinate of this unit   vector,
  //! and then normalizes it.
  //! Warning
  //! Remember that all the coordinates of a unit vector are
  //! implicitly modified when any single one is changed directly.
  //! Exceptions
  //! Standard_ConstructionError if either of the following
  //! is less than or equal to gp::Resolution():
  //! -   the modulus of Coord, or
  //! -   the modulus of the number pair formed from the new
  //! X or Y coordinate and the other coordinate of this
  //! vector that was not directly modified.
    public void SetX ( double X){ }
  

  //! Assigns  the given value to the Y coordinate of this unit   vector,
  //! and then normalizes it.
  //! Warning
  //! Remember that all the coordinates of a unit vector are
  //! implicitly modified when any single one is changed directly.
  //! Exceptions
  //! Standard_ConstructionError if either of the following
  //! is less than or equal to gp::Resolution():
  //! -   the modulus of Coord, or
  //! -   the modulus of the number pair formed from the new
  //! X or Y coordinate and the other coordinate of this
  //! vector that was not directly modified.
    public void SetY ( double Y){ }
  

  //! Assigns:
  //! -   the two coordinates of Coord to this unit vector,
  //! and then normalizes it.
  //! Warning
  //! Remember that all the coordinates of a unit vector are
  //! implicitly modified when any single one is changed directly.
  //! Exceptions
  //! Standard_ConstructionError if either of the following
  //! is less than or equal to gp::Resolution():
  //! -   the modulus of Coord, or
  //! -   the modulus of the number pair formed from the new
  //! X or Y coordinate and the other coordinate of this
  //! vector that was not directly modified.
    public void SetXY ( gp_XY Coord){ }
  

  //! For this unit vector returns the coordinate of range Index :
  //! Index = 1 => X is returned
  //! Index = 2 => Y is returned
  //! Raises OutOfRange if Index != {1, 2}.
    double Coord ( int Index){ }
  
  //! For this unit vector returns its two coordinates Xv and Yv.
  //! Raises OutOfRange if Index != {1, 2}.
    public void Coord (double Xv, double Yv){ }
  
  //! For this unit vector, returns its X coordinate.
    double X(){ }
  
  //! For this unit vector, returns its Y coordinate.
    double Y(){ }
  
  //! For this unit vector, returns its two coordinates as a number pair.
  //! Comparison between Directions
  //! The precision value is an input data.
     gp_XY XY(){ }
  

  //! Returns True if the two vectors have the same direction
  //! i.e. the angle between this unit vector and the
  //! unit vector Other is less than or equal to AngularTolerance.
    Standard_Boolean IsEqual ( gp_Dir2d Other,  double AngularTolerance){ }
  

  //! Returns True if the angle between this unit vector and the
  //! unit vector Other is equal to Pi/2 or -Pi/2 (normal)
  //! i.e. Abs(Abs(<me>.Angle(Other)) - PI/2.) <= AngularTolerance
    Standard_Boolean IsNormal ( gp_Dir2d Other,  double AngularTolerance){ }
  

  //! Returns True if the angle between this unit vector and the
  //! unit vector Other is equal to Pi or -Pi (opposite).
  //! i.e.  PI - Abs(<me>.Angle(Other)) <= AngularTolerance
    Standard_Boolean IsOpposite ( gp_Dir2d Other,  double AngularTolerance){ }
  

  //! returns true if if the angle between this unit vector and unit
  //! vector Other is equal to 0, Pi or -Pi.
  //! i.e.  Abs(Angle(<me>, Other)) <= AngularTolerance or
  //! PI - Abs(Angle(<me>, Other)) <= AngularTolerance
    Standard_Boolean IsParallel ( gp_Dir2d Other,  double AngularTolerance){ }
  

  //! Computes the angular value in radians between <me> and
  //! <Other>. Returns the angle in the range [-PI, PI].
   double Angle ( gp_Dir2d Other){ }
  

  //! Computes the cross product between two directions.
   double Crossed ( gp_Dir2d Right){ }
   double operator ^ ( gp_Dir2d Right) 
{
  return Crossed(Right){ }
}
  
  //! Computes the scalar product
    double Dot ( gp_Dir2d Other){ }
  double operator * ( gp_Dir2d Other) 
{
  return Dot(Other){ }
}
  
    public void Reverse(){ }
  
  //! Reverses the orientation of a direction
     gp_Dir2d Reversed(){ }
   gp_Dir2d operator -() 
{
  return Reversed(){ }
}
  
   public void Mirror ( gp_Dir2d V){ }
  

  //! Performs the symmetrical transformation of a direction
  //! with respect to the direction V which is the center of
  //! the  symmetry.
    gp_Dir2d Mirrored ( gp_Dir2d V){ }
  
   public void Mirror ( gp_Ax2d A){ }
  

  //! Performs the symmetrical transformation of a direction
  //! with respect to an axis placement which is the axis
  //! of the symmetry.
    gp_Dir2d Mirrored ( gp_Ax2d A){ }
  
    public void Rotate ( double Ang){ }
  

  //! Rotates a direction.  Ang is the angular value of
  //! the rotation in radians.
     gp_Dir2d Rotated ( double Ang){ }
  
   public void Transform ( gp_Trsf2d T){ }
  

  //! Transforms a direction with the "Trsf" T.
  //! Warnings :
  //! If the scale factor of the "Trsf" T is negative then the
  //! direction <me> is reversed.
   gp_Dir2d Transformed ( gp_Trsf2d T){ }

  //! Dumps the content of me into the stream
   public void DumpJson (Standard_OStream theOStream, int theDepth = -1){ }




protected:





private:



  gp_XY coord;


}
}
