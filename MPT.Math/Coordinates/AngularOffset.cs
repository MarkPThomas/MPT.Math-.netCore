// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-17-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 09-16-2020
// ***********************************************************************
// <copyright file="AngularOffset.cs" company="Mark P Thomas, Inc.">
//     Copyright © 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// Represents the angular difference between angles I (first) and J (second) in one-dimensional space.
    /// Implements the <see cref="IEquatable{AngleOffset}" />
    /// </summary>
    /// <seealso cref="IEquatable{AngleOffset}" />
    public struct AngularOffset : IEquatable<AngularOffset>, IComparable<AngularOffset>, ITolerance
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
        /// Initializes a new instance of the <see cref="AngularOffset" /> struct.
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AngularOffset" /> struct.
        /// </summary>
        /// <param name="deltaAngle">The angle offset from the origin axis.</param>
        /// <param name="tolerance">The tolerance.</param>
        public AngularOffset(
            double deltaAngle,
            double tolerance = Numbers.ZeroTolerance)
        {
            I = new Angle(0);
            J = new Angle(deltaAngle);
            Tolerance = tolerance;
        }
        #endregion

        #region Methods: Static
        /// <summary>
        /// Creates an angular offset between 3 points.
        /// The sign of the offset is dependent upon the ordering of the points.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <param name="point3">The point3.</param>
        /// <returns>Angle.</returns>
        public static AngularOffset CreateFromPoints(
            CartesianCoordinate point1,
            CartesianCoordinate point2,
            CartesianCoordinate point3)
        {
            Angle angle1 = new Vector(point1, point2).Angle();
            Angle angle2 = new Vector(point2, point3).Angle();
            return angle1.OffsetFrom(angle2);
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

        /// <summary>
        /// Performs an explicit conversion from <see cref="AngularOffset" /> to <see cref="double" />.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator double(AngularOffset a)
        {
            return a.Delta().Radians;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="double" /> to <see cref="AngularOffset" />.
        /// </summary>
        /// <param name="radian">Angle in radians.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator AngularOffset(double radian)
        {
            return new AngularOffset(new Angle(0), new Angle(radian)); ;
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " - Radians_i: " + I.Radians + " - Radians_j: " + J.Radians;
        }

        /// <summary>
        /// j - i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public Angle Delta()
        {
            return (J.RadiansRaw - I.RadiansRaw);
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
            return radius * Delta().RadiansRaw;
        }
        #endregion

        #region Operators: Equals
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
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(double a, AngularOffset b)
        {
            return a == b.Delta().Radians;
        }
        /// <summary>
        /// Implements the == operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(AngularOffset a, double b)
        {
            return a.Delta().Radians == b;
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
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(double a, AngularOffset b)
        {
            return a != b.Delta().Radians;
        }
        /// <summary>
        /// Implements the != operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(AngularOffset a, double b)
        {
            return a.Delta().Radians != b;
        }
        #endregion

        #region Operators: Comparison
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(AngularOffset a, AngularOffset b)
        {
            return a.CompareTo(b) == 1;
        }
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(double a, AngularOffset b)
        {
            return b.CompareTo(a) == -1;
        }
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(AngularOffset a, double b)
        {
            return a.CompareTo(b) == 1;
        }


        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(AngularOffset a, AngularOffset b)
        {
            return a.CompareTo(b) == -1;
        }
        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(double a, AngularOffset b)
        {
            return b.CompareTo(a) == 1;
        }
        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(AngularOffset a, double b)
        {
            return a.CompareTo(b) == -1;
        }


        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(AngularOffset a, AngularOffset b)
        {
            return a.CompareTo(b) >= 0;
        }
        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(double a, AngularOffset b)
        {
            return b.CompareTo(a) <= 0;
        }
        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(AngularOffset a, double b)
        {
            return a.CompareTo(b) >= 0;
        }


        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(AngularOffset a, AngularOffset b)
        {
            return a.CompareTo(b) <= 0;
        }
        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(double a, AngularOffset b)
        {
            return b.CompareTo(a) >= 0;
        }
        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(AngularOffset a, double b)
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
        public static AngularOffset operator -(AngularOffset offset1, AngularOffset offset2)
        {
            return new AngularOffset(
                new Angle(),
                offset1.ToAngle() - offset2.ToAngle(),
                Generics.GetTolerance(offset1, offset2));
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
                Generics.GetTolerance(offset1, offset2));
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
        /// <exception cref="DivideByZeroException"></exception>
        public static AngularOffset operator /(AngularOffset offset, double denominator)
        {
            if (denominator == 0) { throw new DivideByZeroException(); }
            return new AngularOffset(
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
        public bool Equals(AngularOffset other)
        {
            double tolerance = Generics.GetTolerance(this, other);
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
        public int CompareTo(AngularOffset other)
        {
            if (Equals(other)) { return 0; }

            double tolerance = Generics.GetTolerance(this, other);
            return Delta().Radians.IsLessThan(other.Delta().Radians, tolerance) ? -1 : 1;
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

            return Delta().Radians.IsLessThan(other, Tolerance) ? -1 : 1;
        }
        #endregion
    }
}
