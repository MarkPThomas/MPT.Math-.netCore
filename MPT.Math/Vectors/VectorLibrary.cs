using NMath = System.Math;

using Num = MPT.Math.Numbers;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Math.Vectors
{
    /// <summary>
    /// Contains static methods for common vector operations.
    /// </summary>
    public static class VectorLibrary
    {
        #region 2D Vectors

        /// <summary>
        /// Returns the dot product of the points. 
        /// x1*x2 + y1*y2
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double DotProduct(
            double x1, double y1,
            double x2, double y2)
        {
            return (x1 * x2 + y1 * y2);
        }

        /// <summary>
        /// Returns the cross product/determinant of the points. 
        /// x1*y2 - x2*y1
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double CrossProduct(
            double x1, double y1,
            double x2, double y2)
        {
            return ((x1 * y2) - (y1 * x2));
        }

        /// <summary>
        /// Returns the angle [radians] from the x-axis, counter clockwise.
        /// </summary>
        /// <returns></returns>
        public static double Angle(double y, double x)
        {
            return NMath.Atan(y / x);
        }

        /// <summary>
        /// Returns the angle [radians] between the two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double Angle(Vector vector1, Vector vector2)
        {
            return NMath.Acos(ConcavityCollinearity(vector1, vector2));
        }


        /// <summary>
        /// True: Segments are parallel, on the same line, oriented in the same direction.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsCollinearSameDirection(Vector vector1, Vector vector2, double tolerance = Num.ZeroTolerance)
        {
            return (ConcavityCollinearity(vector1, vector2).IsEqualTo(1, tolerance));
        }

        /// <summary>
        /// Vectors form a concave angle.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsConcave(Vector vector1, Vector vector2, double tolerance = Num.ZeroTolerance)
        {
            return (ConcavityCollinearity(vector1, vector2).IsGreaterThan(0, tolerance));
        }

        /// <summary>
        /// Vectors form a 90 degree angle.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsOrthogonal(Vector vector1, Vector vector2, double tolerance = Num.ZeroTolerance)
        {
            return (ConcavityCollinearity(vector1, vector2).IsEqualTo(0, tolerance));
        }

        /// <summary>
        /// Vectors form a convex angle.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsConvex(Vector vector1, Vector vector2, double tolerance = Num.ZeroTolerance)
        {
            return (ConcavityCollinearity(vector1, vector2).IsLessThan(0, tolerance));
        }

        /// <summary>
        ///  True: Segments are parallel, on the same line, oriented in the opposite direction.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsCollinearOppositeDirection(Vector vector1, Vector vector2, double tolerance = Num.ZeroTolerance)
        {
            return (ConcavityCollinearity(vector1, vector2).IsEqualTo(-1, tolerance));
        }



        /// <summary>
        /// True: The concave side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsConcaveInside(Vector vector1, Vector vector2, double tolerance = Num.ZeroTolerance)
        {
            return (vector1.Area(vector2).IsGreaterThan(0, tolerance));
        }

        /// <summary>
        /// True: The convex side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsConvexInside(Vector vector1, Vector vector2, double tolerance = Num.ZeroTolerance)
        {
            return (vector1.Area(vector2).IsLessThan(0, tolerance));
        }

        /// <summary>
        /// Returns a value indicating the concavity of the vectors. 
        /// 1 = Pointing the same way. 
        /// &gt; 0 = Concave. 
        /// 0 = Orthogonal. 
        /// &lt; 0 = Convex. 
        /// -1 = Pointing the exact opposite way.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double ConcavityCollinearity(Vector vector1, Vector vector2)
        {
            return (vector1.DotProduct(vector2) / (vector1.Magnitude() * vector2.Magnitude()));
        }

        #endregion

        #region 3D Vectors
        /// <summary>
        /// Returns the dot product of the points. 
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double DotProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            return (x1 * x2 + y1 * y2 + z1 * z2);
        }


        /// <summary>
        /// Returns the cross product/determinant of the points. 
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double[] CrossProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            double x = (y1 * z2) - (z1 * y2);
            double y = (z1 * x2) - (x1 * z2);
            double z = (x1 * y2) - (y1 * x2);

            double[] matrix = { x, y, z };

            return matrix;
        }
        #endregion
    }
}
