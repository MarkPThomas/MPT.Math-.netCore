// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-16-2020
// ***********************************************************************
// <copyright file="CartesianOffset3D.cs" company="Mark P Thomas, Inc.">
//     2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates3D
{
    /// <summary>
    /// Represents the difference between 3D Cartesian coordinates I (first) and J (second) in three-dimensional space.
    /// </summary>
    /// <seealso cref="System.IEquatable{CartesianOffset3D}" />
    public struct CartesianOffset3D : IEquatable<CartesianOffset3D>
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        private CartesianCoordinate3D _i;
        /// <summary>
        /// Gets or sets the first coordinate value of this Point structure.
        /// </summary>
        /// <value>The i.</value>
        public CartesianCoordinate3D I => new CartesianCoordinate3D(_i.X, _i.Y, _i.Z);

        private CartesianCoordinate3D _j;
        /// <summary>
        /// Gets or sets the second coordinate value of this Point structure.
        /// </summary>
        /// <value>The j.</value>
        public CartesianCoordinate3D J => new CartesianCoordinate3D(_j.X, _j.Y, _j.Z);
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianOffset3D"/> struct.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="tolerance">The tolerance.</param>
        public CartesianOffset3D(CartesianCoordinate3D i, CartesianCoordinate3D j,
            double tolerance = Numbers.ZeroTolerance)
        {
            _i = i;
            _j = j;
            Tolerance = tolerance;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xj - Xi.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double X()
        {
            return (J.X - I.X);
        }

        /// <summary>
        /// Yj - Yi.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Y()
        {
            return (J.Y - I.Y);
        }

        /// <summary>
        /// Zj - Zi.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Z()
        {
            return (J.Z - I.Z);
        }


        /// <summary>
        /// The total straight length of the offset.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return AlgebraLibrary.SRSS(X(), Y(), Z());
        }
        #endregion

        #region Conversion
        /// <summary>
        /// Converts to a single <see cref="CartesianCoordinate3D"/> coordinate.
        /// </summary>
        /// <returns>CartesianCoordinate3D.</returns>
        public CartesianCoordinate3D ToCartesianCoordinate3D()
        {
            return new CartesianCoordinate3D(
                J.X - I.X,
                J.Y - I.Y,
                J.Z - I.Z,
                Tolerance);
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CartesianOffset3D other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return X().IsEqualTo(other.X(), tolerance) &&
                   Y().IsEqualTo(other.Y(), tolerance) &&
                   Z().IsEqualTo(other.Z(), tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CartesianOffset3D) { return Equals((CartesianOffset3D)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return I.GetHashCode() ^ J.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CartesianOffset3D a, CartesianOffset3D b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CartesianOffset3D a, CartesianOffset3D b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset3D operator -(CartesianOffset3D offset1, CartesianOffset3D offset2)
        {
            return new CartesianOffset3D(
                new CartesianCoordinate3D(),
                offset1.ToCartesianCoordinate3D() - offset2,
                NMath.Max(offset1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate3D operator -(CartesianCoordinate3D point1, CartesianOffset3D offset2)
        {
            return new CartesianCoordinate3D(
                point1.X - offset2.X(),
                point1.Y - offset2.Y(),
                point1.Z - offset2.Z(),
                NMath.Max(point1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate3D operator -(CartesianOffset3D offset1, CartesianCoordinate3D point2)
        {
            return new CartesianCoordinate3D(
                offset1.X() - point2.X,
                offset1.Y() - point2.Y,
                offset1.Z() - point2.Z,
                NMath.Max(offset1.Tolerance, point2.Tolerance));
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset3D operator +(CartesianOffset3D offset1, CartesianOffset3D offset2)
        {
            return new CartesianOffset3D(
                new CartesianCoordinate3D(),
                new CartesianCoordinate3D(
                    offset1.X() + offset2.X(),
                    offset1.Y() + offset2.Y(),
                    offset1.Z() + offset2.Z()),
                NMath.Max(offset1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate3D operator +(CartesianCoordinate3D point1, CartesianOffset3D offset2)
        {
            return new CartesianCoordinate3D(
                    point1.X + offset2.X(),
                    point1.Y + offset2.Y(),
                    point1.Z + offset2.Z(),
                NMath.Max(point1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate3D operator +(CartesianOffset3D offset1, CartesianCoordinate3D point2)
        {
            return point2 + offset1;
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset3D operator *(CartesianOffset3D offset, double multiplier)
        {
            return multiplier * offset;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset3D operator *(double multiplier, CartesianOffset3D offset)
        {
            return new CartesianOffset3D(
                offset.I * multiplier,
                offset.J * multiplier,
                offset.Tolerance);
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset3D operator /(CartesianOffset3D offset, double denominator)
        {
            return new CartesianOffset3D(
                offset.I / denominator,
                offset.J / denominator,
                offset.Tolerance);
        }
        #endregion
    }
}
