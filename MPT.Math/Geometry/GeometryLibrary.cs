using MPT.Math.Coordinates;
using MPT.Math.Vectors;

namespace MPT.Math.Geometry
{
    /// <summary>
    /// Contains static methods for common geometry operations.
    /// </summary>
    public static class GeometryLibrary
    {
        /// <summary>
        /// Returns the tangent vector to the supplied points.
        /// </summary>
        /// <param name="i">First point.</param>
        /// <param name="j">Second point.</param>
        /// <returns></returns>
        public static Vector TangentVector(CartesianCoordinate i, CartesianCoordinate j)
        {
            return new Vector((j.X - i.X), (j.Y - i.Y));
        }

        /// <summary>
        /// Returns a normal vector to a line connecting two points.
        /// </summary>
        /// <param name="i">First point.</param>
        /// <param name="j">Second point.</param> 
        /// <returns></returns>
        public static Vector NormalVector(CartesianCoordinate i, CartesianCoordinate j)
        {
            CartesianOffset delta = new CartesianOffset(i, j);
            return new Vector(new CartesianCoordinate(-delta.Y(), delta.X()),
                              new CartesianCoordinate(delta.Y(), -delta.X()));
        }
    }
}
