using MPT.Math.Algebra;
using MPT.Math.Coordinates3D;
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;

namespace MPT.Math.CoordinateConverters
{
    /// <summary>
    /// Class CylindricalSphericalConverter.
    /// </summary>
    public static class CylindricalSphericalConverter
    {
        /// <summary>
        /// Converts to spherical coordinates.
        /// </summary>
        /// <returns>SphericalCoordinate.</returns>
        public static SphericalCoordinate ToSpherical(CylindricalCoordinate coordinate)
        {
            double radius = AlgebraLibrary.SRSS(coordinate.Radius, coordinate.Height);
            double azimuth = coordinate.Azimuth.Radians;
            double inclination = NMath.Atan(coordinate.Radius / coordinate.Height);

            return new SphericalCoordinate(radius, inclination, azimuth, coordinate.Tolerance);
        }

        /// <summary>
        /// Converts to cylindrical coordinates.
        /// </summary>
        /// <returns>CylindricalCoordinate.</returns>
        public static CylindricalCoordinate ToCylindrical(SphericalCoordinate coordinate)
        {
            double radius = coordinate.Radius * NMath.Sin(coordinate.Inclination.Radians);
            double height = coordinate.Radius * NMath.Cos(coordinate.Inclination.Radians);
            double azimuth = coordinate.Azimuth.Radians;

            return new CylindricalCoordinate(radius, height, azimuth, coordinate.Tolerance);
        }
    }
}
