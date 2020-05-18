// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-16-2020
// ***********************************************************************
// <copyright file="Angle.cs" company="Mark P Thomas, Inc.">
//     2020
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
    /// Represents an Angle based on a radian value.
    /// </summary>
    /// <seealso cref="System.IEquatable{Angle}" />
    public struct Angle : IEquatable<Angle>
    {
        #region Properties
        // TODO: Make immutable

        /// <summary>
        /// Default zero tolerance for operations.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// The radians
        /// </summary>
        private double _radians;
        /// <summary>
        /// Gets or sets the radians, which is a value between -π and +π.
        /// </summary>
        /// <value>The radians.</value>
        public double Radians
        {
            get { return _radians; }
            set
            {
                _radians = WrapAngle(value);
            }
        }
        /// <summary>
        /// Gets or sets the clockwise (inverted) radians.
        /// </summary>
        /// <value>The clockwise radians.</value>
        public double ClockwiseRadians { get { return -_radians; } set { Radians = -value; } }


        /// <summary>
        /// Gets or sets the degree.
        /// </summary>
        /// <value>The degree.</value>
        public double Degrees { get { return ToDegrees(_radians); } set { Radians = ToRadians(value); } }
        /// <summary>
        /// Gets or sets the clockwise (inverted) degree.
        /// </summary>
        /// <value>The clockwise degree.</value>
        public double ClockwiseDegrees { get { return ToDegrees(-_radians); } set { Radians = ToRadians(-value); } }
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
            _radians = WrapAngle(radians);
            Tolerance = tolerance;
        }
        #endregion

        #region Static Methods

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
            return new Angle(ToRadians(degrees), tolerance);
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
            Angle a = new Angle(0, tolerance);
            a.SetDirectionVector(direction);
            return a;
        }

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="radians">The radians.</param>
        /// <returns>System.Double.</returns>
        public static double WrapAngle(double radians)
        {
            int revolutions = (int)NMath.Floor(radians / Numbers.TwoPi);
            double wrappedAngle = radians - revolutions*Numbers.TwoPi;

            if (NMath.Abs(wrappedAngle) > Numbers.Pi)
            {
                return -NMath.Sign(wrappedAngle)*(Numbers.TwoPi - NMath.Abs(wrappedAngle));
            }
            return wrappedAngle;
        }

        /// <summary>
        /// Converts the radian angle to degrees.
        /// </summary>
        /// <param name="radians">The angle to convert [radians].</param>
        /// <returns>System.Double.</returns>
        public static double ToDegrees(double radians)
        {
            return (radians / Numbers.Pi);
        }

        /// <summary>
        /// Converts the degrees angle to radians.
        /// </summary>
        /// <param name="degrees">The angle to convert [degrees].</param>
        /// <returns>System.Double.</returns>
        public static double ToRadians(double degrees)
        {
            return (degrees * Numbers.Pi);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the direction vector, which is a normalized vector pointing to the direction of this angle.
        /// </summary>
        /// <returns>Vector.</returns>
        public Vector GetDirectionVector()
        {
            return new Vector(NMath.Cos(ClockwiseRadians), NMath.Sin(ClockwiseRadians));
        }
        /// <summary>
        /// Sets the Angle by using a direction vector.
        /// </summary>
        /// <param name="direction">The direction vector.</param>
        public void SetDirectionVector(Vector direction)
        {
            ClockwiseRadians = NMath.Atan2(direction.Ycomponent, direction.Xcomponent);
        }

        /// <summary>
        /// Rotates the given Vector around the zero point.
        /// </summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <returns>The rotated vector.</returns>
        public Vector RotateVector(Vector vector)
        {
            if (_radians == 0)
                return vector;

            Angle completeAngle = CreateFromVector(vector) + _radians;
            return completeAngle.GetDirectionVector() * vector.Magnitude();
        }

        #endregion

        #region Operators & Equals

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Angle other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return _radians.IsEqualTo(other._radians, tolerance);
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
        public override int GetHashCode() => _radians.GetHashCode();


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
        /// Implements the == operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Angle a, double b)
        {
            return a._radians == b;
        }
        /// <summary>
        /// Implements the != operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Angle a, double b)
        {
            return a._radians != b;
        }
        /// <summary>
        /// Implements the == operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(double a, Angle b)
        {
            return a == b._radians;
        }
        /// <summary>
        /// Implements the != operator for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(double a, Angle b)
        {
            return a != b._radians;
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator +(Angle a, Angle b)
        {
            return new Angle(a._radians + b._radians);
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
            return new Angle(a._radians + b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator -(Angle a, Angle b)
        {
            return new Angle(a._radians - b._radians);
        }
        /// <summary>
        /// Implements the operator - for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a, in radians.</param>
        /// <param name="b">Angle b.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator -(double a, Angle b)
        {
            return b - a;
        }
        /// <summary>
        /// Implements the operator - for an angle and a double which represents radians.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <param name="b">Angle b, in radians.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator -(Angle a, double b)
        {
            return new Angle(a._radians - b);
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
            return new Angle(angle._radians * multiplier);
        }

        /// <summary>
        /// Implements the / operator for an angle and a double which represents the denominator.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator /(Angle angle, double denominator)
        {
            return new Angle(angle._radians / denominator);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Angle"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="a">Angle a.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator double(Angle a)
        {
            return a.Radians;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="Angle"/>.
        /// </summary>
        /// <param name="radian">Angle in radians.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Angle(double radian)
        {
            return new Angle(radian);
        }

        #endregion

    }
}
