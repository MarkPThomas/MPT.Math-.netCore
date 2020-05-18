// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-17-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 05-17-2020
// ***********************************************************************
// <copyright file="AngularOffset.cs" company="Mark P Thomas, Inc.">
//     2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// Represents the angular difference between angles I (first) and J (second) in one-dimensional space.
    /// Implements the <see cref="IEquatable{AngleOffset}" />
    /// </summary>
    /// <seealso cref="IEquatable{AngleOffset}" />
    public struct AngularOffset : IEquatable<AngularOffset>
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets or sets the first angular value.
        /// </summary>
        /// <value>The first angle.</value>
        public Angle I { get; private set; }

        /// <summary>
        /// Gets or sets the second angular value.
        /// </summary>
        /// <value>The second angle.</value>
        public Angle J { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AngularOffset"/> struct.
        /// </summary>
        /// <param name="i">The first angle.</param>
        /// <param name="j">The second angle.</param>
        /// <param name="tolerance">The tolerance.</param>
        public AngularOffset(
            Angle i, Angle j,
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
        public Angle ToAngle()
        {
            return new Angle(Delta().Radians, Tolerance);
        }
        #endregion

        #region Methods
        /// <summary>
        /// j - i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public Angle Delta()
        {
            return (J - I);
        }

        /// <summary>
        /// The total straight length of the offset.
        /// </summary>
        /// <param name="radius">The radius to use with the angular offset.</param>
        /// <returns>System.Double.</returns>
        public double LengthChord(double radius = 1)
        {
            return radius * 2 * NMath.Sin(Delta().Radians/2);
        }

        /// <summary>
        /// The total arc length of the offset.
        /// </summary>
        /// <param name="radius">The radius to use with the angular offset.</param>
        /// <returns>System.Double.</returns>
        public double LengthArc(double radius = 1)
        {
            return radius * Delta().Radians;
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(AngularOffset other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return Delta().Radians.IsEqualTo(other.Delta().Radians, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is AngularOffset) { return Equals((AngularOffset)obj); }
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
        public static bool operator ==(AngularOffset a, AngularOffset b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(AngularOffset a, AngularOffset b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static AngularOffset operator -(AngularOffset offset1, AngularOffset offset2)
        {
            return new AngularOffset(
                new Angle(),
                offset1.ToAngle() - offset2.ToAngle(),
                NMath.Max(offset1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="angle1">The angle1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator -(double angle1, AngularOffset offset2)
        {
            return angle1 - offset2.Delta().Radians;
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="angle2">The angle2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator -(AngularOffset offset1, double angle2)
        {
            return offset1.Delta().Radians - angle2;
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static AngularOffset operator +(AngularOffset offset1, AngularOffset offset2)
        {
            return new AngularOffset(
                offset1.I + offset2.I,
                offset1.J + offset2.J,
                NMath.Min(offset1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="angle1">The angle1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator +(double angle1, AngularOffset offset2)
        {
            return offset2 + angle1;
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="angle2">The angle2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator +(AngularOffset offset1, double angle2)
        {
            return offset1.Delta().Radians + angle2;
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static AngularOffset operator *(AngularOffset offset, double multiplier)
        {
            return multiplier * offset;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The result of the operator.</returns>
        public static AngularOffset operator *(double multiplier, AngularOffset offset)
        {
            return new AngularOffset(
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
        public static AngularOffset operator /(AngularOffset offset, double denominator)
        {
            return new AngularOffset(
                offset.I / denominator,
                offset.J / denominator,
                offset.Tolerance);
        }
        #endregion
    }
}
