// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-16-2020
// ***********************************************************************
// <copyright file="CartesianCoordinate.cs" company="Mark P Thomas, Inc.">
//     2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using System;
using NMath = System.Math;


namespace MPT.Math.Coordinates
{
    /// <summary>
    /// A two-dimensional coordinate system that specifies each point uniquely in a plane by a set of numerical coordinates, which are the signed distances to the point from two fixed perpendicular oriented lines, measured in the same unit of length.
    /// </summary>
    /// <seealso ref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
    /// <seealso ref="https://en.wikipedia.org/wiki/Euclidean_space"/>
    /// <seealso cref="System.IEquatable{CartesianCoordinate}" />
    public struct CartesianCoordinate : IEquatable<CartesianCoordinate>, ICoordinate
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the x-coordinate.
        /// </summary>
        /// <value>The x-coordinate.</value>
        public double X { get; private set; }

        /// <summary>
        /// Gets the y-coordinate.
        /// </summary>
        /// <value>The y-coordinate.</value>
        public double Y { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianCoordinate"/> struct.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public CartesianCoordinate(double x, double y,
            double tolerance = Numbers.ZeroTolerance)
        {
            X = x;
            Y = y;
            Tolerance = tolerance;
        }
        #endregion

        #region Conversion
        /// <summary>
        /// Converts to Polar coordinates.
        /// </summary>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate ToPolar()
        {
            return new PolarCoordinate(
                radius: AlgebraLibrary.SRSS(X, Y),
                azimuth: new Angle(NMath.Atan(Y / X)),
                Tolerance);
        }

        /// <summary>
        /// Converts to Barycentric coordinates.
        /// </summary>
        /// <param name="vertexA">The vertex a coordinate of the simplex shape.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>BarycentricCoordinate.</returns>
        public BarycentricCoordinate ToBarycentric(CartesianCoordinate vertexA, CartesianCoordinate vertexB, CartesianCoordinate vertexC)
        {
            double determinate = (vertexB.Y - vertexC.Y) * (vertexA.X - vertexC.X) +
                                 (vertexC.X - vertexB.X) * (vertexA.Y - vertexC.Y);

            double alpha = ((vertexB.Y - vertexC.Y) * (X - vertexC.X) +
                            (vertexC.X - vertexB.X) * (Y - vertexC.Y)) / determinate;

            double beta = ((vertexC.Y - vertexA.Y) * (X - vertexC.X) +
                            (vertexA.X - vertexC.X) * (Y - vertexC.Y)) / determinate;

            double gamma = 1 - alpha - beta;

            return new BarycentricCoordinate(alpha, beta, gamma, Tolerance);
        }

        /// <summary>
        /// Converts to Trilinear coordinates.
        /// </summary>
        /// <param name="vertexA">The vertex a.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>TrilinearCoordinate.</returns>
        public TrilinearCoordinate ToTrilinear(CartesianCoordinate vertexA, CartesianCoordinate vertexB, CartesianCoordinate vertexC)
        {
            BarycentricCoordinate barycentric = ToBarycentric(vertexA, vertexB, vertexC);
            return barycentric.ToTrilinear(vertexA, vertexB, vertexC);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the cross product/determinant of the coordinates.
        /// x1*y2 - x2*y1
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>System.Double.</returns>
        public double CrossProduct(CartesianCoordinate coordinate)
        {
            return VectorLibrary.CrossProduct(X, Y, coordinate.X, coordinate.Y);
        }

        /// <summary>
        /// Returns the dot product of the coordinates.
        /// x1*x2 + y1*y2
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>System.Double.</returns>
        public double DotProduct(CartesianCoordinate coordinate)
        {
            return VectorLibrary.DotProduct(X, Y, coordinate.X, coordinate.Y);
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CartesianCoordinate other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return X.IsEqualTo(other.X, tolerance) &&
                   Y.IsEqualTo(other.Y, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CartesianCoordinate) { return Equals((CartesianCoordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CartesianCoordinate a, CartesianCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CartesianCoordinate a, CartesianCoordinate b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset operator -(CartesianCoordinate a, CartesianCoordinate b)
        {
            return new CartesianOffset(
                a,
                b,
                NMath.Max(a.Tolerance, b.Tolerance));
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator +(CartesianCoordinate a, CartesianCoordinate b)
        {
            return new CartesianCoordinate(
                a.X + b.X,
                a.Y + b.Y,
                NMath.Max(a.Tolerance, b.Tolerance));
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator *(CartesianCoordinate coordinate, double multiplier)
        {
            return multiplier * coordinate;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator *(double multiplier, CartesianCoordinate coordinate)
        {
            return new CartesianCoordinate(
                coordinate.X * multiplier,
                coordinate.Y * multiplier,
                coordinate.Tolerance);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator /(CartesianCoordinate coordinate, double denominator)
        {
            return new CartesianCoordinate(
                coordinate.X / denominator,
                coordinate.Y / denominator,
                coordinate.Tolerance);
        }
        #endregion
    }
}
