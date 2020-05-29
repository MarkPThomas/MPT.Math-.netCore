// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-17-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 05-17-2020
// ***********************************************************************
// <copyright file="LinearOffset.cs" company="Mark P Thomas, Inc.">
//     Copyright © 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// Represents the linear difference between coordinates I (first) and J (second) in one-dimensional space.
    /// Implements the <see cref="IEquatable{LinearOffset}" />
    /// </summary>
    /// <seealso cref="IEquatable{LinearOffset}" />
    public struct LinearOffset : IEquatable<LinearOffset>, ITolerance
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

        /// <summary>
        /// Performs an explicit conversion from <see cref="LinearOffset" /> to <see cref="double" />.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator double(LinearOffset a)
        {
            return a.Delta();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="double" /> to <see cref="LinearOffset" />.
        /// </summary>
        /// <param name="distance">Offset distance.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator LinearOffset(double distance)
        {
            return new LinearOffset(0, distance);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " - I: " + I + ", J: " + J;
        }

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

        #region Operators: Equals
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
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(double a, LinearOffset b)
        {
            return b.Equals(a);
        }
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(LinearOffset a, double b)
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
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(double a, LinearOffset b)
        {
            return !b.Equals(a);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(LinearOffset a, double b)
        {
            return !a.Equals(b);
        }
        #endregion

        #region Operators: Comparison
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(LinearOffset a, LinearOffset b)
        {
            return a.CompareTo(b) == 1;
        }
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Offset a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(double a, LinearOffset b)
        {
            return b.CompareTo(a) == -1;
        }
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(LinearOffset a, double b)
        {
            return a.CompareTo(b) == 1;
        }


        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(LinearOffset a, LinearOffset b)
        {
            return a.CompareTo(b) == -1;
        }
        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Offset a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(double a, LinearOffset b)
        {
            return b.CompareTo(a) == 1;
        }
        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(LinearOffset a, double b)
        {
            return a.CompareTo(b) == -1;
        }


        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(LinearOffset a, LinearOffset b)
        {
            return a.CompareTo(b) >= 0;
        }
        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Offset a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(double a, LinearOffset b)
        {
            return b.CompareTo(a) <= 0;
        }
        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(LinearOffset a, double b)
        {
            return a.CompareTo(b) >= 0;
        }


        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(LinearOffset a, LinearOffset b)
        {
            return a.CompareTo(b) <= 0;
        }
        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Offset a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(double a, LinearOffset b)
        {
            return b.CompareTo(a) >= 0;
        }
        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(LinearOffset a, double b)
        {
            return a.CompareTo(b) <= 0;
        }
        #endregion

        #region Operators: Combining
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
                Helper.GetTolerance(offset1, offset2));
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
                Helper.GetTolerance(offset1, offset2));
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
            if (denominator == 0) { throw new DivideByZeroException(); }
            return new LinearOffset(
                offset.I / denominator,
                offset.J / denominator,
                offset.Tolerance);
        }
        #endregion

        #region IEquatable
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
        #endregion

        #region IComparable
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This instance precedes <paramref name="other">other</paramref> in the sort order.
        /// Zero
        /// This instance occurs in the same position in the sort order as <paramref name="other">other</paramref>.
        /// Greater than zero
        /// This instance follows <paramref name="other">other</paramref> in the sort order.</returns>
        public int CompareTo(LinearOffset other)
        {
            if (Equals(other)) { return 0; }

            double tolerance = Helper.GetTolerance(this, other);
            return Delta().IsLessThan(other.Delta(), tolerance) ? -1 : 1;
        }
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This instance precedes <paramref name="other">other</paramref> in the sort order.
        /// Zero
        /// This instance occurs in the same position in the sort order as <paramref name="other">other</paramref>.
        /// Greater than zero
        /// This instance follows <paramref name="other">other</paramref> in the sort order.</returns>
        public int CompareTo(double other)
        {
            if (Equals(other)) { return 0; }

            return Delta().IsLessThan(other, Tolerance) ? -1 : 1;
        }
        #endregion
    }
}
