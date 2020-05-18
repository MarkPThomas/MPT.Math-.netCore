using MPT.Math.Coordinates;
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates3D
{
    /// <summary>
    /// Represents the difference between cylindrical coordinates I (first) and J (second) in two-dimensional space.
    /// </summary>
    /// <seealso cref="System.IEquatable{CylindricalOffset}" />
    public struct CylindricalOffset : IEquatable<CylindricalOffset>
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
        public CylindricalCoordinate I { get; private set; }

        /// <summary>
        /// Gets or sets the second coordinate value.
        /// </summary>
        /// <value>The second coordinate.</value>
        public CylindricalCoordinate J { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CylindricalOffset"/> struct.
        /// </summary>
        /// <param name="i">The first coordinate.</param>
        /// <param name="j">The second coordinate.</param>
        /// <param name="tolerance">The tolerance.</param>
        public CylindricalOffset(
            CylindricalCoordinate i, CylindricalCoordinate j,
            double tolerance = Numbers.ZeroTolerance)
        {
            I = i;
            J = j;
            Tolerance = tolerance;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Radius_j - Radius_i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Radius()
        {
            return (J.Radius - I.Radius);
        }

        /// <summary>
        /// Azimuth_j - Azimuth_i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public Angle Azimuth()
        {
            return (J.Azimuth - I.Azimuth);
        }

        /// <summary>
        /// Height_j - Height_i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Height()
        {
            return (J.Height - I.Height);
        }

        /// <summary>
        /// The total straight length between the offset points.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return NMath.Sqrt(I.Radius.Squared() + J.Radius.Squared() - 2 * I.Radius * J.Radius * (NMath.Cos(I.Azimuth.Radians - J.Azimuth.Radians))+(J.Height-I.Height).Squared());
        }
        #endregion

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CylindricalOffset other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return Radius().IsEqualTo(other.Radius(), tolerance) &&
                   Height().IsEqualTo(other.Height(), tolerance) &&
                   Azimuth().Radians.IsEqualTo(other.Azimuth().Radians, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CylindricalOffset) { return Equals((CylindricalOffset)obj); }
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
        public static bool operator ==(CylindricalOffset a, CylindricalOffset b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Offset a.</param>
        /// <param name="b">Offset b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CylindricalOffset a, CylindricalOffset b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalCoordinate operator -(CylindricalCoordinate point1, CylindricalOffset offset2)
        {
            return new CylindricalCoordinate(
                point1.Radius - offset2.Radius(),
                point1.Height - offset2.Height(),
                point1.Azimuth - offset2.Azimuth(),
                NMath.Max(point1.Tolerance, offset2.Tolerance));
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalCoordinate operator -(CylindricalOffset offset1, CylindricalCoordinate point2)
        {
            return offset1 + (-1 * point2);
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="offset2">The offset2.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalCoordinate operator +(CylindricalCoordinate point1, CylindricalOffset offset2)
        {
            return offset2 + point1;
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="offset1">The offset1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalCoordinate operator +(CylindricalOffset offset1, CylindricalCoordinate point2)
        {
            return new CylindricalCoordinate(
                offset1.Radius() + point2.Radius,
                offset1.Height() + point2.Height,
                offset1.Azimuth() + point2.Azimuth,
                NMath.Max(offset1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalOffset operator *(CylindricalOffset offset, double multiplier)
        {
            return multiplier * offset;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalOffset operator *(double multiplier, CylindricalOffset offset)
        {
            return new CylindricalOffset(
                offset.I * multiplier,
                offset.J * multiplier,
                offset.Tolerance);
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The result of the operator.</returns>
        public static CylindricalOffset operator /(CylindricalOffset offset, double denominator)
        {
            return new CylindricalOffset(
                offset.I / denominator,
                offset.J / denominator,
                offset.Tolerance);
        }
        #endregion
    }
}
