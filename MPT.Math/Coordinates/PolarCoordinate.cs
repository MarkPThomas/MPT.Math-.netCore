// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-20-2018
//
// Last Modified By : Mark P Thomas
// Last Modified On : 05-16-2020
// ***********************************************************************
// <copyright file="PolarCoordinate.cs" company="Mark P Thomas, Inc.">
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
    /// A two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction. 
    /// Polar coordinates are points labeled (r,θ) and plotted on a polar grid.
    /// </summary>
    /// <seealso ref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
    /// <seealso cref="System.IEquatable{PolarCoordinate}" />
    public struct PolarCoordinate : IEquatable<PolarCoordinate>, ICoordinate, ITolerance
    {
        // TODO: Handle ability to make polar coordinates unique:
        // Where a unique representation is needed for any point besides the pole, it is usual to limit r to positive numbers (r > 0) and φ to the interval [0, 360°) or (−180°, 180°] (in radians, [0, 2π) or (−π, π]).

        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the radius, r.
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; private set; }

        /// <summary>
        /// Gets the azimuth angle, φ,.
        /// </summary>
        /// <value>The azimuth.</value>
        public Angle Azimuth { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="PolarCoordinate"/> struct.
        /// </summary>
        /// <param name="radius">The distance from a reference point.</param>
        /// <param name="azimuth">The angle from a reference direction.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public PolarCoordinate(
            double radius,
            Angle azimuth,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radius = radius;
            Azimuth = azimuth;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolarCoordinate"/> struct.
        /// </summary>
        /// <param name="radius">The distance from a reference point.</param>
        /// <param name="azimuth">The angle from a reference direction [radians].</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public PolarCoordinate(
            double radius,
            double azimuth,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radius = radius;
            Azimuth = azimuth;
            Tolerance = tolerance;
        }
        #endregion

        #region Methods: Azimuth Angle Add/Subtract/Multiply/Divide
        /// <summary>
        /// Adds the angle, in radians, to the angle of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate AddAngleAzimuthRadians(double angle)
        {
            return changeAngleAzimuthRadians(Azimuth.Radians + angle);
        }

        /// <summary>
        /// Subtracts the angle, in radians, from the angle of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate SubtractAngleAzimuthRadians(double angle)
        {
            return changeAngleAzimuthRadians(Azimuth.Radians - angle);
        }

        /// <summary>
        /// Adds the angle, in degrees, to the angle of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [degrees].</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate AddAngleAzimuthDegrees(double angle)
        {
            return changeAngleAzimuthRadians(Azimuth.Radians + Angle.DegreesToRadians(angle));
        }

        /// <summary>
        /// Subtracts the angle, in degrees, from the angle of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [degrees].</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate SubtractAngleAzimuthDegrees(double angle)
        {
            return changeAngleAzimuthRadians(Azimuth.Radians - Angle.DegreesToRadians(angle));
        }

        /// <summary>
        /// Multiplies the angle of the current coordinate by the amount.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate MultiplyAngleAzimuthBy(double multiplier)
        {
            return changeAngleAzimuthRadians(Azimuth.Radians * multiplier);
        }

        /// <summary>
        /// Divides the angle of the current coordinate by the amount.
        /// </summary>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate DivideAngleAzimuthBy(double denominator)
        {
            return changeAngleAzimuthRadians(Azimuth.Radians / denominator);
        }

        /// <summary>
        /// Changes the angle of the current coordinate to the provided angle, in radians.
        /// </summary>
        /// <param name="angle">The new angle [radians].</param>
        /// <returns>PolarCoordinate.</returns>
        private PolarCoordinate changeAngleAzimuthRadians(double angle)
        {
            return new PolarCoordinate(
                Radius,
                angle,
                Tolerance);
        }
        #endregion

        #region Methods: Radius Add/Subtract/Multiply/Divide
        /// <summary>
        /// Adds the amount to the radius of the current coordinate.
        /// </summary>
        /// <param name="value">The amount to add to the radius.</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate AddToRadius(double value)
        {
            return changeRadius(Radius + value);
        }

        /// <summary>
        /// Subtracts the amount from the radius of the current coordinate.
        /// </summary>
        /// <param name="value">The amount to subtract from the radius.</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate SubtractFromRadius(double value)
        {
            return changeRadius(Radius - value);
        }

        /// <summary>
        /// Multiplies the radius of the current coordinate by the amount.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate MultiplyRadiusBy(double multiplier)
        {
            return changeRadius(Radius * multiplier);
        }

        /// <summary>
        /// Divides the radius of the current coordinate by the amount.
        /// </summary>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate DivideRadiusBy(double denominator)
        {
            return changeRadius(Radius / denominator);
        }

        /// <summary>
        /// Changes the radius of the current coordinate to the provided radius.
        /// </summary>
        /// <param name="radius">The new radius.</param>
        /// <returns>PolarCoordinate.</returns>
        private PolarCoordinate changeRadius(double radius)
        {
            return new PolarCoordinate(
                radius,
                Azimuth,
                Tolerance);
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(PolarCoordinate other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return Azimuth.Radians.IsEqualTo(other.Azimuth.Radians, tolerance) &&
                   Radius.IsEqualTo(other.Radius, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is PolarCoordinate) { return Equals((PolarCoordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Radius.GetHashCode() ^ Azimuth.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Polar coordinate a.</param>
        /// <param name="b">Polar coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(PolarCoordinate a, PolarCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Polar coordinate a.</param>
        /// <param name="b">Polar coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(PolarCoordinate a, PolarCoordinate b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the * operator for a polar coordinate and a double which represents a multiplier.
        /// </summary>
        /// <param name="coordinate">Polar coordinate.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarCoordinate operator *(PolarCoordinate coordinate, double multiplier)
        {
            return multiplier * coordinate;
        }
        /// <summary>
        /// Implements the * operator for a polar coordinate and a double which represents a multiplier.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="coordinate">Polar coordinate.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarCoordinate operator *(double multiplier, PolarCoordinate coordinate)
        {
            return new PolarCoordinate(
                coordinate.Radius * multiplier,
                coordinate.Azimuth * multiplier,
                coordinate.Tolerance);
        }

        /// <summary>
        /// Implements the / operator for a polar coordinate and a double which represents a denominator.
        /// </summary>
        /// <param name="coordinate">Polar coordinate.</param>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarCoordinate operator /(PolarCoordinate coordinate, double denominator)
        {
            return new PolarCoordinate(
                coordinate.Radius / denominator,
                coordinate.Azimuth / denominator,
                coordinate.Tolerance);
        }

        #endregion
    }
}
