using MPT.Math.Algebra;
using MPT.Math.Coordinates3D;
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;

namespace MPT.Math.CoordinateConverters
{
    /// <summary>
    /// Class Cartesian3DCylindricalConverter.
    /// </summary>
    public static class Cartesian3DCylindricalConverter
    {
        /// <summary>
        /// Converts to cylindrical coordinates.
        /// </summary>
        /// <returns>CylindricalCoordinate.</returns>
        public static CylindricalCoordinate ToCylindrical(CartesianCoordinate3D coordinate)
        {
            double radialDistance = AlgebraLibrary.SRSS(coordinate.X, coordinate.Y);
            double height = coordinate.Z;
            double angleAzimuth = 0;
            if (coordinate.X.IsZeroSign() && coordinate.Y.IsZeroSign())
            {
                angleAzimuth = 0;
            }
            else if (coordinate.X.IsGreaterThanOrEqualTo(0) && !coordinate.Y.IsZeroSign())
            {
                angleAzimuth = NMath.Asin(coordinate.Y / radialDistance);
            }
            else if (coordinate.X.IsNegativeSign())
            {
                angleAzimuth = -NMath.Asin(coordinate.Y / radialDistance) + Numbers.Pi;
            }

            return new CylindricalCoordinate(radialDistance, height, angleAzimuth, coordinate.Tolerance);
        }


        /// <summary>
        /// Converts to Cartesian coordinates.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate3D ToCartesian(CylindricalCoordinate coordinate)
        {
            double x = coordinate.Radius * NMath.Cos(coordinate.Azimuth.Radians);
            double y = coordinate.Radius * NMath.Sin(coordinate.Azimuth.Radians);
            double z = coordinate.Height;
            return new CartesianCoordinate3D(x, y, z, coordinate.Tolerance);
        }
    }
}
