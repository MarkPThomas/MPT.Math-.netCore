using MPT.Math.Algebra;
using MPT.Math.Coordinates3D;
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;

namespace MPT.Math.CoordinateConverters
{
    /// <summary>
    /// Class Cartesian3DSphericalConverter.
    /// </summary>
    public static class Cartesian3DSphericalConverter
    {
        /// <summary>
        /// Converts to spherical coordinates.
        /// </summary>
        /// <returns>SphericalCoordinate.</returns>
        public static SphericalCoordinate ToSpherical(CartesianCoordinate3D coordinate)
        {
            double radialDistance = AlgebraLibrary.SRSS(coordinate.X, coordinate.Y.Squared(), coordinate.Z.Squared());
            double angleAzimuth = NMath.Atan(coordinate.Y / coordinate.X);
            double angleInclination = NMath.Acos(coordinate.Z / radialDistance);

            return new SphericalCoordinate(radialDistance, angleInclination, angleAzimuth, coordinate.Tolerance);
        }

        /// <summary>
        /// Converts to Cartesian coordinates.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate3D ToCartesian(SphericalCoordinate coordinate)
        {
            double x = coordinate.Radius * NMath.Sin(coordinate.Inclination.Radians) * NMath.Cos(coordinate.Azimuth.Radians);
            double y = coordinate.Radius * NMath.Sin(coordinate.Inclination.Radians) * NMath.Sin(coordinate.Azimuth.Radians);
            double z = coordinate.Radius * NMath.Cos(coordinate.Inclination.Radians);
            return new CartesianCoordinate3D(x, y, z, coordinate.Tolerance);
        }
    }
}
