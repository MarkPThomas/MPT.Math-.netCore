// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-16-2020
// ***********************************************************************
// <copyright file="CartesianCoordinate3D.cs" company="Mark P Thomas, Inc.">
//     2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates3D
{
    /// <summary>
    /// A three-dimensional coordinate system that specifies each point uniquely in a plane by a set of numerical coordinates, which are the signed distances to the point from two fixed perpendicular oriented lines, measured in the same unit of length.
    /// </summary>
    /// <seealso ref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
    /// <seealso ref="https://en.wikipedia.org/wiki/Euclidean_space"/>
    /// <seealso cref="System.IEquatable{CartesianCoordinate3D}" />
    public struct CartesianCoordinate3D : IEquatable<CartesianCoordinate3D>, ICoordinate3D, ITolerance
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X { get; private set; }

        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the z.
        /// </summary>
        /// <value>The z.</value>
        public double Z { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianCoordinate3D"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="tolerance">The tolerance.</param>
        public CartesianCoordinate3D(double x, double y, double z,
            double tolerance = Numbers.ZeroTolerance)
        {
            X = x;
            Y = y;
            Z = z;
            Tolerance = tolerance;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Crosses the product.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>Point3D.</returns>
        public CartesianCoordinate3D CrossProduct(CartesianCoordinate3D point)
        {
            double[] matrix = VectorLibrary.CrossProduct(X, Y, Z, point.X, point.Y, point.Z);
            return new CartesianCoordinate3D(matrix[0], matrix[1], matrix[2], Tolerance);
        }

        /// <summary>
        /// Dots the product.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>System.Double.</returns>
        public double DotProduct(CartesianCoordinate3D point)
        {
            return VectorLibrary.DotProduct(X, Y, Z, point.X, point.Y, point.Z);
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CartesianCoordinate3D other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return X.IsEqualTo(other.X, tolerance) &&
                   Y.IsEqualTo(other.Y, tolerance) &&
                   Z.IsEqualTo(other.Z, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CartesianCoordinate3D) { return Equals((CartesianCoordinate3D)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CartesianCoordinate3D a, CartesianCoordinate3D b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CartesianCoordinate3D a, CartesianCoordinate3D b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset3D operator -(CartesianCoordinate3D point1, CartesianCoordinate3D point2)
        {
            return new CartesianOffset3D(
                point1, 
                point2,
                NMath.Max(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate3D operator +(CartesianCoordinate3D point1, CartesianCoordinate3D point2)
        {
            return new CartesianCoordinate3D(
                point1.X + point2.X, 
                point1.Y + point2.Y, 
                point1.Z + point2.Z,
                NMath.Max(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the * operator for a coordinate and a double which represents a multiplier.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate3D operator *(CartesianCoordinate3D coordinate, double multiplier)
        {
            return new CartesianCoordinate3D(
                coordinate.X * multiplier, 
                coordinate.Y * multiplier, 
                coordinate.Z * multiplier,
                coordinate.Tolerance);
        }


        /// <summary>
        /// Implements the / operator for a coordinate and a double which represents the denominator.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="denominator">Denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate3D operator /(CartesianCoordinate3D coordinate, double denominator)
        {
            return new CartesianCoordinate3D(
                coordinate.X / denominator, 
                coordinate.Y / denominator, 
                coordinate.Z / denominator,
                coordinate.Tolerance);
        }
        #endregion
    }
}
