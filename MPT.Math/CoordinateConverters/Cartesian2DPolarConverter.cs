using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;

namespace MPT.Math.CoordinateConverters
{
    /// <summary>
    /// Class Cartesian2DPolarConverter.
    /// </summary>
    public static class Cartesian2DPolarConverter
    {
        /// <summary>
        /// Converts to Polar coordinates.
        /// </summary>
        /// <returns>PolarCoordinate.</returns>
        public static PolarCoordinate ToPolar(CartesianCoordinate coordinate)
        {
            return new PolarCoordinate(
                radius: AlgebraLibrary.SRSS(coordinate.X, coordinate.Y),
                azimuth: Angle.AsRadians(coordinate.X, coordinate.Y),
                tolerance: coordinate.Tolerance);
        }

        /// <summary>
        /// Converts to Cartesian coordinates.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public static  CartesianCoordinate ToCartesian(PolarCoordinate coordinate)
        {
            double x = coordinate.Radius * NMath.Cos(coordinate.Azimuth.Radians);
            double y = coordinate.Radius * NMath.Sin(coordinate.Azimuth.Radians);
            return new CartesianCoordinate(x, y, coordinate.Tolerance);
        }
    }
}
