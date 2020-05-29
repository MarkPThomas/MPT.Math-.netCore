using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates3D
{
    /// <summary>
    /// A coordinate system for three-dimensional space where the position of a point is specified by three numbers: the radial length of that point from a fixed origin, r, its polar angle measured from a fixed zenith direction, θ, and the azimuthal angle, φ, of its orthogonal projection on a reference plane that passes through the origin and is orthogonal to the zenith, measured from a fixed reference direction on that plane. 
    /// It can be seen as the three-dimensional version of the polar coordinate system.
    /// </summary>
    /// <seealso ref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
    /// <seealso cref="System.IEquatable{SphericalCoordinate}" />
    public struct SphericalCoordinate : IEquatable<SphericalCoordinate>, ICoordinate3D, ITolerance
    {
        // TODO: Handle ability to make spherical coordinates unique:
        // If it is necessary to define a unique set of spherical coordinates for each point, one must restrict their ranges. A common choice is
        // r ≥ 0,
        // 0° ≤ θ ≤ 180° (π rad),
        // 0° ≤ φ < 360° (2π rad).
        // However, the azimuth φ is often restricted to the interval(−180°, +180°], or(−π, +π] in radians, instead of[0, 360°). This is the standard convention for geographic longitude.
        // The range[0°, 180°] for inclination is equivalent to [−90°, +90°] for elevation(latitude).

        #region Properties
        /// <summary>
        /// Default zero tolerance for operations.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the radial length, r.
        /// </summary>
        /// <value>The radial length.</value>
        public double Radius { get; private set; }

        /// <summary>
        /// Gets or sets the inclination angle, θ, which lies in the vertical plane sweeping out from the Z-axis.
        /// </summary>
        /// <value>The theta.</value>
        public Angle Inclination { get; private set; }

        /// <summary>
        /// Gets or sets the azimuth angle, φ, which lies in the x-y plane sweeping out from the X-axis.
        /// </summary>
        /// <value>The phi.</value>
        public Angle Azimuth { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="SphericalCoordinate" /> struct.
        /// </summary>
        /// <param name="radius">The distance from a reference point.</param>
        /// <param name="inclination">The polar angle, θ, which lies in the vertical plane sweeping out from the Z-axis [radians].</param>
        /// <param name="azimuth">The azimuth angle, φ, which lies in the x-y plane sweeping out from the X-axis [radians].</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public SphericalCoordinate(
            double radius,
            double inclination,
            double azimuth,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radius = radius;
            Inclination = new Angle(inclination);
            Azimuth = new Angle(azimuth);
            Tolerance = tolerance;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SphericalCoordinate" /> struct.
        /// </summary>
        /// <param name="radius">The distance from a reference point.</param>
        /// <param name="inclination">The polar angle, θ, which lies in the vertical plane sweeping out from the Z-axis.</param>
        /// <param name="azimuth">The azimuth angle, φ, which lies in the x-y plane sweeping out from the X-axis.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public SphericalCoordinate(
            double radius,
            Angle inclination,
            Angle azimuth,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radius = radius;
            Inclination = new Angle(inclination.Radians);
            Azimuth = new Angle(azimuth.Radians);
            Tolerance = tolerance;
        }
        #endregion

        #region Methods: Inclination Angle Add/Subtract/Multiply/Divide
        /// <summary>
        /// Adds the angle, in radians, to the inclination angle, θ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate AddAngleInclinationRadians(double angle)
        {
            return changeAngleInclinationRadians(Inclination.Radians + angle);
        }

        /// <summary>
        /// Subtracts the angle, in radians, from the inclination angle, θ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate SubtractAngleInclinationRadians(double angle)
        {
            return changeAngleInclinationRadians(Inclination.Radians - angle);
        }

        /// <summary>
        /// Adds the angle, in degrees, to the inclination angle, θ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [degrees].</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate AddAngleInclinationDegrees(double angle)
        {
            return changeAngleInclinationRadians(Inclination.Radians + Angle.DegreesToRadians(angle));
        }

        /// <summary>
        /// Subtracts the angle, in degrees, from the inclination angle, θ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [degrees].</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate SubtractAngleInclinationDegrees(double angle)
        {
            return changeAngleInclinationRadians(Inclination.Radians - Angle.DegreesToRadians(angle));
        }

        /// <summary>
        /// Multiplies the inclination angle, θ, of the current coordinate by the amount.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate MultiplyAngleInclinationBy(double multiplier)
        {
            return changeAngleInclinationRadians(Inclination.Radians * multiplier);
        }

        /// <summary>
        /// Divides the inclination angle, θ, of the current coordinate by the amount.
        /// </summary>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate DivideAngleInclinationBy(double denominator)
        {
            return changeAngleInclinationRadians(Inclination.Radians / denominator);
        }

        /// <summary>
        /// Changes the inclination angle, θ, of the current coordinate to the provided angle, in radians.
        /// </summary>
        /// <param name="angle">The new angle [radians].</param>
        /// <returns>SphericalCoordinate.</returns>
        private SphericalCoordinate changeAngleInclinationRadians(double angle)
        {
            return new SphericalCoordinate(
                Radius,
                angle,
                Azimuth,
                Tolerance);
        }
        #endregion

        #region Methods: Azimuth Angle Add/Subtract/Multiply/Divide
        /// <summary>
        /// Adds the angle, in radians, to the azimuth angle, φ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate AddAngleAzimuthRadians(double angle)
        {
            return changePhiAngleAzimuthRadians(Azimuth.Radians + angle);
        }

        /// <summary>
        /// Subtracts the angle, in radians, from the azimuth angle, φ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate SubtractAngleAzimuthRadians(double angle)
        {
            return changePhiAngleAzimuthRadians(Azimuth.Radians - angle);
        }

        /// <summary>
        /// Adds the angle, in degrees, to the azimuth angle, φ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [degrees].</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate AddAngleAzimuthDegrees(double angle)
        {
            return changePhiAngleAzimuthRadians(Azimuth.Radians + Angle.DegreesToRadians(angle));
        }

        /// <summary>
        /// Subtracts the angle, in degrees, from the azimuth angle, φ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [degrees].</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate SubtractAngleAzimuthDegrees(double angle)
        {
            return changePhiAngleAzimuthRadians(Azimuth.Radians - Angle.DegreesToRadians(angle));
        }

        /// <summary>
        /// Multiplies the azimuth angle, φ, of the current coordinate by the amount.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate MultiplyAngleAzimuthBy(double multiplier)
        {
            return changePhiAngleAzimuthRadians(Azimuth.Radians * multiplier);
        }

        /// <summary>
        /// Divides the azimuth angle, φ, of the current coordinate by the amount.
        /// </summary>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate DivideAngleAzimuthBy(double denominator)
        {
            return changePhiAngleAzimuthRadians(Azimuth.Radians / denominator);
        }

        /// <summary>
        /// Changes the azimuth angle, φ, of the current coordinate to the provided angle, in radians.
        /// </summary>
        /// <param name="angle">The new angle [radians].</param>
        /// <returns>SphericalCoordinate.</returns>
        private SphericalCoordinate changePhiAngleAzimuthRadians(double angle)
        {
            return new SphericalCoordinate(
                Radius,
                Inclination,
                angle,
                Tolerance);
        }
        #endregion

        #region Methods: Radial Length Add/Subtract/Multiply/Divide
        /// <summary>
        /// Adds the amount to the radius of the current coordinate.
        /// </summary>
        /// <param name="value">The amount to add to the radial length.</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate AddToRadialLength(double value)
        {
            return changeRadialLength(Radius + value);
        }

        /// <summary>
        /// Subtracts the amount from the radius of the current coordinate.
        /// </summary>
        /// <param name="value">The amount to subtract from the radius.</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate SubtractFromRadialLength(double value)
        {
            return changeRadialLength(Radius - value);
        }

        /// <summary>
        /// Multiplies the radius of the current coordinate by the amount.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate MultiplyRadialLengthBy(double multiplier)
        {
            return changeRadialLength(Radius * multiplier);
        }

        /// <summary>
        /// Divides the radius of the current coordinate by the amount.
        /// </summary>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate DivideRadialLengthBy(double denominator)
        {
            return changeRadialLength(Radius / denominator);
        }

        /// <summary>
        /// Changes the radius of the current coordinate to the provided radius.
        /// </summary>
        /// <param name="radius">The new radius.</param>
        /// <returns>SphericalCoordinate.</returns>
        private SphericalCoordinate changeRadialLength(double radius)
        {
            return new SphericalCoordinate(
                radius,
                Inclination,
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
        public bool Equals(SphericalCoordinate other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return Inclination.Radians.IsEqualTo(other.Inclination.Radians, tolerance) &&
                   Azimuth.Radians.IsEqualTo(other.Azimuth.Radians, tolerance) &&
                   Radius.IsEqualTo(other.Radius, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SphericalCoordinate) { return Equals((SphericalCoordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => Inclination.GetHashCode() ^ Azimuth.GetHashCode() ^ Radius.GetHashCode();


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(SphericalCoordinate a, SphericalCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SphericalCoordinate a, SphericalCoordinate b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the * operator for a coordinate and a double which represents a multiplier.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The result of the operator.</returns>
        public static SphericalCoordinate operator *(double multiplier, SphericalCoordinate coordinate)
        {
            return coordinate * multiplier;
        }
        /// <summary>
        /// Implements the * operator for a coordinate and a double which represents a multiplier.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static SphericalCoordinate operator *(SphericalCoordinate coordinate, double multiplier)
        {
            return new SphericalCoordinate(
                coordinate.Radius * multiplier, 
                coordinate.Inclination * multiplier,
                coordinate.Azimuth * multiplier,
                coordinate.Tolerance);
        }

        /// <summary>
        /// Implements the / operator for a coordinate and a double which represents the denominator.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static SphericalCoordinate operator /(SphericalCoordinate coordinate, double denominator)
        {
            return new SphericalCoordinate(
                coordinate.Radius / denominator,
                coordinate.Inclination / denominator,
                coordinate.Azimuth / denominator,
                coordinate.Tolerance);
        }
        #endregion
    }
}
