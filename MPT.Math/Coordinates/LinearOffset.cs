// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-17-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 05-17-2020
// ***********************************************************************
// <copyright file="LinearOffset.cs" company="Mark P Thomas, Inc.">
//     2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// Represents the linear difference between coordinates I (first) and J (second) in one-dimensional space.
    /// Implements the <see cref="IEquatable{LinearOffset}" />
    /// </summary>
    /// <seealso cref="IEquatable{LinearOffset}" />
    public struct LinearOffset : IEquatable<LinearOffset>
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
        public double I { get; set; }

        /// <summary>
        /// Gets or sets the second coordinate value.
        /// </summary>
        /// <value>The second coordinate.</value>
        public double J { get; set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearOffset"/> struct.
        /// </summary>
        /// <param name="i">The first coordinate.</param>
        /// <param name="j">The second coordinate.</param>
        /// <param name="tolerance">The tolerance.</param>
        public LinearOffset(
            double i, double j,
            double tolerance = Numbers.ZeroTolerance)
        {
            I = i;
            J = j;
            Tolerance = tolerance;
        }
        #endregion

        #region Conversion
        /// <summary>
        /// Converts to a single value.
        /// </summary>
        /// <returns>double.</returns>
        public double ToValue()
        {
            return Delta();
        }
        #endregion

        #region Methods
        /// <summary>
        /// j - i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Delta()
        {
            return (J - I);
        }

        /// <summary>
        /// The total straight length of the offset.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return Delta();
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(LinearOffset other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return Delta().IsEqualTo(other.Delta(), tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is LinearOffset) { return Equals((LinearOffset)obj); }
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
        public static bool operator ==(LinearOffset a, LinearOffset b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(LinearOffset a, LinearOffset b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static LinearOffset operator -(LinearOffset offset1, LinearOffset offset2)
        {
            return new LinearOffset(
                offset1.I - offset2.I,
                offset1.J - offset2.J,
                NMath.Max(offset1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator -(double point1, LinearOffset offset2)
        {
            return point1 - offset2.Delta();
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator -(LinearOffset offset1, double point2)
        {
            return offset1.Delta() - point2;
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static LinearOffset operator +(LinearOffset offset1, LinearOffset offset2)
        {
            return new LinearOffset(
                offset1.I + offset2.I,
                offset1.J + offset2.J, 
                NMath.Max(offset1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator +(double point1, LinearOffset offset2)
        {
            return offset2 + point1;
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator +(LinearOffset offset1, double point2)
        {
            return offset1.Delta() + point2;
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static LinearOffset operator *(LinearOffset offset, double multiplier)
        {
            return multiplier * offset;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The result of the operator.</returns>
        public static LinearOffset operator *(double multiplier, LinearOffset offset)
        {
            return new LinearOffset(
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
        public static LinearOffset operator /(LinearOffset offset, double denominator)
        {
            return new LinearOffset(
                offset.I / denominator,
                offset.J / denominator,
                offset.Tolerance);
        }
        #endregion
    }
}
