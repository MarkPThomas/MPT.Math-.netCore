// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-27-2020
// ***********************************************************************
// <copyright file="Angle.cs" company="Mark P Thomas, Inc.">
//     Copyright © 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;
using System;

using NMath = System.Math;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// Represents an Angle based on a radian value.
    /// </summary>
    /// <seealso cref="System.IEquatable{Angle}" />
    public struct Angle : IEquatable<Angle>, IComparable<Angle>, ITolerance
    {
        #region Properties
        /// <summary>
        /// Default zero tolerance for operations.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// The angle as radians, which is a value between -π (clockwise) and +π (counter-clockwise).
        /// </summary>
        /// <value>The radians.</value>
        public double Radians { get; }

        /// <summary>
        /// The angle as clockwise (inverted) radians, which is a value between -π (counter-clockwise) and +π (clockwise).
        /// </summary>
        /// <value>The clockwise radians.</value>
        public double ClockwiseRadians => -Radians;

        /// <summary>
        /// The angle as degrees, which is a value between -180 (clockwise) and +180 (counter-clockwise).
        /// </summary>
        /// <value>The degree.</value>
        public double Degrees => RadiansToDegrees(Radians);

        /// <summary>
        /// The angle as clockwise (inverted) degrees, which is a value between -180 (counter-clockwise) and +180 (clockwise).
        /// </summary>
        /// <value>The clockwise degree.</value>
        public double ClockwiseDegrees => RadiansToDegrees(-Radians);
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Angle" /> struct.
        /// </summary>
        /// <param name="radians">The radian value of the angle.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public Angle(
            double radians,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radians = WrapAngleWithinPositiveNegativePi(radians);
            Tolerance = tolerance;
        }
        #endregion

        #region Methods: Static
        /// <summary>
        /// Creates an <see cref="Angle" /> from a radian value.
        /// </summary>
        /// <param name="radians">The radian value of the angle.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        /// <returns>Angle.</returns>
        public static Angle CreateFromRadian(
            double radians,
            double tolerance = Numbers.ZeroTolerance)
        {
            return new Angle(radians, tolerance);
        }

        /// <summary>
        /// Creates an <see cref="Angle" /> from a degree value.
        /// </summary>
        /// <param name="degrees">The degree value of the angle.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        /// <returns>Angle.</returns>
        public static Angle CreateFromDegree(
            double degrees,
            double tolerance = Numbers.ZeroTolerance)
        {
            return new Angle(DegreesToRadians(degrees), tolerance);
        }

        /// <summary>
        /// Creates an <see cref="Angle" /> from a direction vector.
        /// </summary>
        /// <param name="direction">The direction vector of the angle.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        /// <returns>Angle.</returns>
        public static Angle CreateFromVector(
            Vector direction,
            double tolerance = Numbers.ZeroTolerance)
        {
            return new Angle(AsRadians(direction.Xcomponent, direction.Ycomponent), tolerance);
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>System.Double.</returns>
        public static double RadiansToDegrees(double radians)
        {
            return radians * (180d / Numbers.Pi);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <returns>System.Double.</returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Numbers.Pi / 180);
        }

        /// <summary>
        /// Returns the positive angle [degrees] from the x-axis, counter-clockwise, of the coordinates.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>System.Double.</returns>
        public static double AsDegrees(CartesianCoordinate coordinate)
        {
            return AsDegrees(coordinate.X, coordinate.Y);
        }

        /// <summary>
        /// Returns the positive angle [degrees] from the x-axis, counter-clockwise, of the coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>System.Double.</returns>
        public static double AsDegrees(double x, double y)
        {
            return RadiansToDegrees(AsRadians(x, y));
        }

        /// <summary>
        /// Returns the positive angle [degrees] from the x-axis, counter-clockwise, of the coordinates.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>System.Double.</returns>
        public static double AsRadians(CartesianCoordinate coordinate)
        {
            return AsRadians(coordinate.X, coordinate.Y);
        }

        /// <summary>
        /// Returns the positive angle [radians] from the x-axis, counter-clockwise, of the coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>System.Double.</returns>
        public static double AsRadians(double x, double y)
        {
            if (x.IsZeroSign())
            {
                if (y.IsPositiveSign()) { return Numbers.Pi / 2; } // 90 deg
                if (y.IsNegativeSign()) { return (3d / 2) * Numbers.Pi; } // 270 deg
                return 0;  // Assume 0 degrees for origin
            }

            double angleOffset = 0;
            if (x.IsNegativeSign()) // 2nd or 3rd quadrant
            {
                angleOffset = Numbers.Pi;
            }
            else if (y.IsNegativeSign()) // 4th quadrant
            {
                angleOffset = 2 * Numbers.Pi;
            }

            return angleOffset + Trig.ArcTan(y / x);
        }


        /// <summary>
        /// Reduces a given angle to a value between -π and +π radians.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double WrapAngleWithinPositiveNegativePi(double radians, double tolerance = Numbers.ZeroTolerance)
        {
            double wrappedAngleWithinTwoPi = WrapAngleWithinTwoPi(radians, tolerance); 

            if (NMath.Abs(wrappedAngleWithinTwoPi) > Numbers.Pi)
            {
                return -NMath.Sign(wrappedAngleWithinTwoPi) * (Numbers.TwoPi - NMath.Abs(wrappedAngleWithinTwoPi));
            }
            return wrappedAngleWithinTwoPi;
        }

        /// <summary>
        /// Reduces a given angle to a value between 0 and 2π radians, matching the sign of the angle.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double WrapAngleWithinTwoPi(double radians, double tolerance = Numbers.ZeroTolerance)
        {
            if (radians.IsZeroSign(tolerance))
            {
                radians = 0;
            }
            if (radians == double.PositiveInfinity || radians == double.NegativeInfinity)
            {
                return double.PositiveInfinity;
            }
            int inferredRounding = NMath.Max(Numbers.DecimalPlaces(radians), 6);
            double roundedPi = NMath.Round(Numbers.TwoPi, inferredRounding);
            int revolutions = (int)NMath.Round(NMath.Floor(radians / roundedPi), inferredRounding);   

            return radians - revolutions * roundedPi;
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " - Radians: " + Radians;
        }

        /// <summary>
        /// Gets the direction vector, which is a normalized vector pointing to the direction of this angle.
        /// </summary>
        /// <returns>Vector.</returns>
        public Vector GetDirectionVector()
        {
            return new Vector(NMath.Cos(Radians), NMath.Sin(Radians));
        }

        /// <summary>
        /// Rotates the given Vector around the zero point.
        /// </summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <returns>The rotated vector.</returns>
        public Vector RotateVector(Vector vector)
        {
            double tolerance = Helper.GetTolerance(this, vector);
            if (Radians.IsZeroSign(tolerance)) { return vector; }
                
            Angle completeAngle = CreateFromVector(vector) + Radians;
            return completeAngle.GetDirectionVector() * vector.Magnitude();
        }

        /// <summary>
        /// Returns the angular offset of the current angle from the provided angle.
        /// i.e. the current angle subtracting the provided angle.
        /// </summary>
        /// <param name="angleI">The angle i.</param>
        /// <returns>AngularOffset.</returns>
        public AngularOffset OffsetFrom(Angle angleI)
        {
            return new AngularOffset(angleI, this);
        }
        #endregion

        #region Operators: Equals
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Angle a, Angle b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the == operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(double a, Angle b)
        {
            return a == b.Radians;
        }
        /// <summary>
        /// Implements the == operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Angle a, double b)
        {
            return a.Radians == b;
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Angle a, Angle b)
        {
            return !a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(double a, Angle b)
        {
            return a != b.Radians;
        }
        /// <summary>
        /// Implements the != operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Angle a, double b)
        {
            return a.Radians != b;
        }
        #endregion

        #region Operators: Comparison
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Angle a, Angle b)
        {
            return a.CompareTo(b) == 1;
        }
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(double a, Angle b)
        {
            return b.CompareTo(a) == -1;
        }
        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Angle a, double b)
        {
            return a.CompareTo(b) == 1;
        }


        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Angle a, Angle b)
        {
            return a.CompareTo(b) == -1;
        }
        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(double a, Angle b)
        {
            return b.CompareTo(a) == 1;
        }
        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Angle a, double b)
        {
            return a.CompareTo(b) == -1;
        }


        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Angle a, Angle b)
        {
            return a.CompareTo(b) >= 0;
        }
        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(double a, Angle b)
        {
            return b.CompareTo(a) <= 0;
        }
        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Angle a, double b)
        {
            return a.CompareTo(b) >= 0;
        }


        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Angle a, Angle b)
        {
            return a.CompareTo(b) <= 0;
        }
        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(double a, Angle b)
        {
            return b.CompareTo(a) >= 0;
        }
        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Angle a, double b)
        {
            return a.CompareTo(b) <= 0;
        }
        #endregion

        #region Operators: Combining
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator +(Angle a, Angle b)
        {
            return new Angle(a.Radians + b.Radians, Helper.GetTolerance(a, b));
        }
        /// <summary>
        /// Implements the operator + for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator +(double a, Angle b)
        {
            return b + a;
        }
        /// <summary>
        /// Implements the operator + for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator +(Angle a, double b)
        {
            return new Angle(a.Radians + b, a.Tolerance);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator -(Angle a, Angle b)
        {
            return new Angle(a.Radians - b.Radians, Helper.GetTolerance(a, b));
        }
        /// <summary>
        /// Implements the operator - for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator -(double a, Angle b)
        {
            return new Angle(a - b.Radians, b.Tolerance);
        }
        /// <summary>
        /// Implements the operator - for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator -(Angle a, double b)
        {
            return new Angle(a.Radians - b, a.Tolerance);
        }

        /// <summary>
        /// Implements the * operator for an angle and a double which represents a multiplier.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator *(double multiplier, Angle angle)
        {
            return angle * multiplier;
        }
        /// <summary>
        /// Implements the * operator for an angle and a double which represents a multiplier.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator *(Angle angle, double multiplier)
        {
            return new Angle(angle.Radians * multiplier, angle.Tolerance);
        }

        /// <summary>
        /// Implements the / operator for an angle and a double which represents the denominator.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator /(Angle angle, double denominator)
        {
            if (denominator == 0) { throw new DivideByZeroException(); }
            return new Angle(angle.Radians / denominator, angle.Tolerance);
        }
        #endregion

        #region Conversion
        /// <summary>
        /// Performs an explicit conversion from <see cref="Angle" /> to <see cref="double" />.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator double(Angle a)
        {
            return a.Radians;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="double" /> to <see cref="Angle" />.
        /// </summary>
        /// <param name="radian">Angle in radians.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Angle(double radian)
        {
            return new Angle(radian);
        }
        #endregion

        #region IEquatable
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Angle other)
        {
            double tolerance = Helper.GetTolerance(this, other);
            return Radians.IsEqualTo(other.Radians, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Angle) { return Equals((Angle)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => Radians.GetHashCode();
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
        public int CompareTo(Angle other)
        {
            if (Equals(other)) { return 0; }

            double tolerance = Helper.GetTolerance(this, other);
            return Radians.IsLessThan(other.Radians, tolerance) ? -1 : 1;
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

            return Radians.IsLessThan(other, Tolerance) ? -1 : 1;
        }
        #endregion
    }
}
