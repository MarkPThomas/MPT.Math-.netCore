using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates3D
{
    /// <summary>
    /// A three-dimensional coordinate system that specifies point positions by the distance from a chosen reference axis, the direction from the axis relative to a chosen reference direction, and the distance from a chosen reference plane perpendicular to the axis. 
    /// The latter distance is given as a positive or negative number depending on which side of the reference plane faces the point.
    /// </summary>
    /// <seealso ref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
    /// <seealso cref="System.IEquatable{CylindricalCoordinate}" />
    public struct CylindricalCoordinate : IEquatable<CylindricalCoordinate>, ICoordinate3D
    {
        // TODO: Handle ability to make spherical coordinates unique:
        // In situations where someone wants a unique set of coordinates for each point, one may restrict the radius to be non-negative (ρ ≥ 0) and the azimuth φ to lie in a specific interval spanning 360°, such as [−180°,+180°] or [0,360°].

        #region Properties
        /// <summary>
        /// Default zero tolerance for operations.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the radial length, ρ, which is the Euclidean distance from the z-axis to the point P.
        /// </summary>
        /// <value>The radial length.</value>
        public double Radius { get; private set; }

        /// <summary>
        /// Gets the height, z, which is the signed distance from the chosen plane to the point P.
        /// </summary>
        /// <value>The height.</value>
        public double Height { get; private set; }

        /// <summary>
        /// The azimuth angle, φ, which lies in the x-y plane sweeping out from the X-axis.
        /// </summary>
        private Angle _azimuth;
        /// <summary>
        /// Gets or sets the azimuth angle, φ, which is the angle between the reference direction on the chosen plane and the line from the origin to the projection of P on the plane.
        /// </summary>
        /// <value>The phi.</value>
        public Angle Azimuth => new Angle(_azimuth.Radians);
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="SphericalCoordinate" /> struct.
        /// </summary>
        /// <param name="radius">The distance from a reference point.</param>
        /// <param name="height">The height, z, which is the signed distance from the chosen plane to the point P.</param>
        /// <param name="azimuth">The azimuth angle, φ, which lies in the x-y plane sweeping out from the X-axis [radians].</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public CylindricalCoordinate(
            double radius,
            double height,
            double azimuth,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radius = radius;
            Height = height;
            _azimuth = new Angle(azimuth);
            Tolerance = tolerance;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SphericalCoordinate" /> struct.
        /// </summary>
        /// <param name="radius">The distance from a reference point.</param>
        /// <param name="height">The height, z, which is the signed distance from the chosen plane to the point P.</param>
        /// <param name="azimuth">The azimuth angle, φ, which lies in the x-y plane sweeping out from the X-axis.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public CylindricalCoordinate(
            double radius,
            double height,
            Angle azimuth,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radius = radius;
            Height = height;
            _azimuth = new Angle(azimuth.Radians);
            Tolerance = tolerance;
        }
        #endregion

        #region Conversion
        /// <summary>
        /// Converts to Cartesian coordinates.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate3D ToCartesian()
        {
            double x = Radius * NMath.Cos(_azimuth.Radians);
            double y = Radius * NMath.Sin(_azimuth.Radians);
            double z = Height;
            return new CartesianCoordinate3D(x, y, z, Tolerance);
        }

        /// <summary>
        /// Converts to spherical coordinates.
        /// </summary>
        /// <returns>SphericalCoordinate.</returns>
        public SphericalCoordinate ToSpherical()
        {
            double radius = Numbers.Sqrt(Radius.Squared() + Height.Squared());
            double azimuth = _azimuth.Radians;
            double inclination = NMath.Atan(Radius / Height);

            return new SphericalCoordinate(radius, inclination, azimuth, Tolerance);
        }

        #endregion

        #region Methods: Height Add/Subtract/Multiply/Divide
        /// <summary>
        /// Adds the value to the height, z, of the current coordinate.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>CylindricalCoordinate.</returns>
        public CylindricalCoordinate AddToHeight(double value)
        {
            return changeHeight(Height + value);
        }

        /// <summary>
        /// Subtracts the value from the height, z, of the current coordinate.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>CylindricalCoordinate.</returns>
        public CylindricalCoordinate SubtractFromHeight(double value)
        {
            return changeHeight(Height - value);
        }

        /// <summary>
        /// Multiplies the height, z, of the current coordinate by the amount.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>CylindricalCoordinate.</returns>
        public CylindricalCoordinate MultiplyHeightBy(double multiplier)
        {
            return changeHeight(Height * multiplier);
        }

        /// <summary>
        /// Divides the height, z, of the current coordinate by the amount.
        /// </summary>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>CylindricalCoordinate.</returns>
        public CylindricalCoordinate DivideHeightBy(double denominator)
        {
            return changeHeight(Height / denominator);
        }

        /// <summary>
        /// Changes the height, z, of the current coordinate to the provided height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns>CylindricalCoordinate.</returns>
        private CylindricalCoordinate changeHeight(double height)
        {
            return new CylindricalCoordinate(
                Radius,
                height,
                _azimuth,
                Tolerance);
        }
        #endregion

        #region Methods: Azimuth Angle Add/Subtract/Multiply/Divide
        /// <summary>
        /// Adds the angle, in radians, to the azimuth angle, φ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>PolarCoordinate.</returns>
        public CylindricalCoordinate AddAngleAzimuthRadians(double angle)
        {
            return changePhiAngleAzimuthRadians(_azimuth.Radians + angle);
        }

        /// <summary>
        /// Subtracts the angle, in radians, from the azimuth angle, φ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>PolarCoordinate.</returns>
        public CylindricalCoordinate SubtractAngleAzimuthRadians(double angle)
        {
            return changePhiAngleAzimuthRadians(_azimuth.Radians - angle);
        }

        /// <summary>
        /// Adds the angle, in degrees, to the azimuth angle, φ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [degrees].</param>
        /// <returns>PolarCoordinate.</returns>
        public CylindricalCoordinate AddAngleAzimuthDegrees(double angle)
        {
            return changePhiAngleAzimuthRadians(_azimuth.Radians + Angle.ToRadians(angle));
        }

        /// <summary>
        /// Subtracts the angle, in degrees, from the azimuth angle, φ, of the current coordinate.
        /// </summary>
        /// <param name="angle">The angle [degrees].</param>
        /// <returns>PolarCoordinate.</returns>
        public CylindricalCoordinate SubtractAngleAzimuthDegrees(double angle)
        {
            return changePhiAngleAzimuthRadians(_azimuth.Radians - Angle.ToRadians(angle));
        }

        /// <summary>
        /// Multiplies the azimuth angle, φ, of the current coordinate by the amount.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>PolarCoordinate.</returns>
        public CylindricalCoordinate MultiplyAngleAzimuthBy(double multiplier)
        {
            return changePhiAngleAzimuthRadians(_azimuth.Radians * multiplier);
        }

        /// <summary>
        /// Divides the azimuth angle, φ, of the current coordinate by the amount.
        /// </summary>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>PolarCoordinate.</returns>
        public CylindricalCoordinate DivideAngleAzimuthBy(double denominator)
        {
            return changePhiAngleAzimuthRadians(_azimuth.Radians / denominator);
        }

        /// <summary>
        /// Changes the azimuth angle, φ, of the current coordinate to the provided angle, in radians.
        /// </summary>
        /// <param name="angle">The new angle [radians].</param>
        /// <returns>PolarCoordinate.</returns>
        private CylindricalCoordinate changePhiAngleAzimuthRadians(double angle)
        {
            return new CylindricalCoordinate(
                Radius,
                Height,
                angle,
                Tolerance);
        }
        #endregion

        #region Methods: Radial Length Add/Subtract/Multiply/Divide
        /// <summary>
        /// Adds the amount to the radial length of the current coordinate.
        /// </summary>
        /// <param name="value">The amount to add to the radial length.</param>
        /// <returns>CylindricalCoordinate.</returns>
        public CylindricalCoordinate AddToRadius(double value)
        {
            return changeRadius(Radius + value);
        }

        /// <summary>
        /// Subtracts the amount from the radial length of the current coordinate.
        /// </summary>
        /// <param name="value">The amount to subtract from the radial length.</param>
        /// <returns>CylindricalCoordinate.</returns>
        public CylindricalCoordinate SubtractFromRadius(double value)
        {
            return changeRadius(Radius - value);
        }

        /// <summary>
        /// Multiplies the radial length of the current coordinate by the amount.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>CylindricalCoordinate.</returns>
        public CylindricalCoordinate MultiplyRadiusBy(double multiplier)
        {
            return changeRadius(Radius * multiplier);
        }

        /// <summary>
        /// Divides the radial length of the current coordinate by the amount.
        /// </summary>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>CylindricalCoordinate.</returns>
        public CylindricalCoordinate DivideRadiusBy(double denominator)
        {
            return changeRadius(Radius / denominator);
        }

        /// <summary>
        /// Changes the radial length of the current coordinate to the provided radial length.
        /// </summary>
        /// <param name="radialLength">The new radial length.</param>
        /// <returns>CylindricalCoordinate.</returns>
        private CylindricalCoordinate changeRadius(double radialLength)
        {
            return new CylindricalCoordinate(
                radialLength,
                Height,
                _azimuth,
                Tolerance);
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CylindricalCoordinate other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return Height.IsEqualTo(other.Height, tolerance) &&
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
            if (obj is CylindricalCoordinate) { return Equals((CylindricalCoordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => Height.GetHashCode() ^ _azimuth.GetHashCode() ^ Radius.GetHashCode();


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CylindricalCoordinate a, CylindricalCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CylindricalCoordinate a, CylindricalCoordinate b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the * operator for a coordinate and a double which represents a multiplier.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalCoordinate operator *(double multiplier, CylindricalCoordinate coordinate)
        {
            return coordinate * multiplier;
        }
        /// <summary>
        /// Implements the * operator for a coordinate and a double which represents a multiplier.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalCoordinate operator *(CylindricalCoordinate coordinate, double multiplier)
        {
            return new CylindricalCoordinate(
                coordinate.Radius * multiplier,
                coordinate.Height * multiplier,
                coordinate._azimuth * multiplier,
                coordinate.Tolerance);
        }

        /// <summary>
        /// Implements the / operator for a coordinate and a double which represents the denominator.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalCoordinate operator /(CylindricalCoordinate coordinate, double denominator)
        {
            return new CylindricalCoordinate(
                coordinate.Radius / denominator,
                coordinate.Height / denominator,
                coordinate._azimuth / denominator,
                coordinate.Tolerance);
        }
        #endregion
    }
}
