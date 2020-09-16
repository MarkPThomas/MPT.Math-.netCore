// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-18-2020
// ***********************************************************************
// <copyright file="CartesianOffset.cs" company="Mark P Thomas, Inc.">
//     Copyright © 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// Represents the difference between Cartesian coordinates I (first) and J (second) in two-dimensional space.
    /// </summary>
    /// <seealso cref="System.IEquatable{CartesianOffset}" />
    public struct CartesianOffset : IEquatable<CartesianOffset>, ITolerance
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets or sets the first coordinate value.
        /// </summary>
        /// <value>The first coordinate.</value>
        public CartesianCoordinate I { get; private set; }

        /// <summary>
        /// Gets or sets the second coordinate value.
        /// </summary>
        /// <value>The second coordinate.</value>
        public CartesianCoordinate J { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianOffset"/> struct.
        /// </summary>
        /// <param name="i">The first coordinate.</param>
        /// <param name="j">The second coordinate.</param>
        /// <param name="tolerance">The tolerance.</param>
        public CartesianOffset(
            CartesianCoordinate i, CartesianCoordinate j,
            double tolerance = Numbers.ZeroTolerance)
        {
            I = i;
            J = j;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianOffset"/> struct.
        /// </summary>
        /// <param name="deltaX">The x-axis offset from the origin.</param>
        /// <param name="deltaY">The y-axis offset from the origin.</param>
        /// <param name="tolerance">The tolerance.</param>
        public CartesianOffset(
            double deltaX, double deltaY,
            double tolerance = Numbers.ZeroTolerance)
        {
            I = new CartesianCoordinate(0, 0);
            J = new CartesianCoordinate(deltaX, deltaY);
            Tolerance = tolerance;
        }
        #endregion

        #region Conversion
        /// <summary>
        /// Converts to a single <see cref="CartesianCoordinate"/> coordinate.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate ToCartesianCoordinate()
        {
            return new CartesianCoordinate(
                X(),
                Y(),
                Tolerance);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " - I: (" + I.X + ", " + I.Y + "), J: (" + J.X + ", " + J.Y + ")";
        }

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
        /// The total straight length of the offset.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return AlgebraLibrary.SRSS(X(), Y());
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CartesianOffset other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return  X().IsEqualTo(other.X(), tolerance) &&
                    Y().IsEqualTo(other.Y(), tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CartesianOffset) { return Equals((CartesianOffset)obj); }
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
        public static bool operator ==(CartesianOffset a, CartesianOffset b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CartesianOffset a, CartesianOffset b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset operator -(CartesianOffset offset1, CartesianOffset offset2)
        {
            return new CartesianOffset(
                new CartesianCoordinate(),
                offset1.ToCartesianCoordinate() - offset2,
                Generics.GetTolerance(offset1, offset2));
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator -(CartesianCoordinate point1, CartesianOffset offset2)
        {
            return new CartesianCoordinate(
                point1.X - offset2.X(),
                point1.Y - offset2.Y(),
                Generics.GetTolerance(point1, offset2));
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator -(CartesianOffset offset1, CartesianCoordinate point2)
        {
            return new CartesianCoordinate(
                offset1.X() - point2.X,
                offset1.Y() - point2.Y,
                Generics.GetTolerance(offset1, point2));
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset operator +(CartesianOffset offset1, CartesianOffset offset2)
        {
            return new CartesianOffset(
                new CartesianCoordinate(),
                new CartesianCoordinate(
                    offset1.X() + offset2.X(),
                    offset1.Y() + offset2.Y()),
                Generics.GetTolerance(offset1, offset2));
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator +(CartesianCoordinate point1, CartesianOffset offset2)
        {
            return new CartesianCoordinate(
                point1.X + offset2.X(),
                point1.Y + offset2.Y(),
                Generics.GetTolerance(point1, offset2));
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator +(CartesianOffset offset1, CartesianCoordinate point2)
        {
            return point2 + offset1;
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset operator *(CartesianOffset offset, double multiplier)
        {
            return multiplier * offset;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset operator *(double multiplier, CartesianOffset offset)
        {
            return new CartesianOffset(
                offset.I * multiplier,
                offset.J * multiplier,
                offset.Tolerance);
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="denominator">Denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianOffset operator /(CartesianOffset offset, double denominator)
        {
            if (denominator == 0) { throw new DivideByZeroException(); }
            return new CartesianOffset(
                offset.I / denominator,
                offset.J / denominator,
                offset.Tolerance);
        }
        #endregion
    }
}
