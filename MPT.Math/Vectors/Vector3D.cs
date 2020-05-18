// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 01-31-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="Vector3D.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates3D;
using System;
using NMath = System.Math;

namespace MPT.Math.Vectors
{
    /// <summary>
    /// Represents a vector in 3D space.
    /// </summary>
    /// <seealso cref="System.IEquatable{Vector3D}" />
    public struct Vector3D : IEquatable<Vector3D>
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the x-component of this vector.
        /// </summary>
        /// <value>The xcomponent.</value>
        public double Xcomponent { get; private set; }

        /// <summary>
        /// Gets the y-component of this vector.
        /// </summary>
        /// <value>The ycomponent.</value>
        public double Ycomponent { get; private set; }

        /// <summary>
        /// Gets the z-component of this vector.
        /// </summary>
        /// <value>The zcomponent.</value>
        public double Zcomponent { get; private set; }


        /// <summary>
        /// The location
        /// </summary>
        private CartesianCoordinate3D _location;
        /// <summary>
        /// Gets the location of this vector in Euclidean space.
        /// </summary>
        /// <value>The location.</value>
        public CartesianCoordinate3D Location => _location;
        #endregion


        /// <summary>
        /// Initializes the class with a vector structure.
        /// </summary>
        /// <param name="xMagnitude">The x magnitude.</param>
        /// <param name="yMagnitude">The y magnitude.</param>
        /// <param name="zMagnitude">The z magnitude.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Vector3D(
            double xMagnitude,
            double yMagnitude,
            double zMagnitude,
            double tolerance = Numbers.ZeroTolerance)
        {
            _location = new CartesianCoordinate3D();
            Xcomponent = xMagnitude;
            Ycomponent = yMagnitude;
            Zcomponent = zMagnitude;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Initializes the class with a vector structure and a point coinciding with the location of the vector.
        /// </summary>
        /// <param name="xMagnitude">The x magnitude.</param>
        /// <param name="yMagnitude">The y magnitude.</param>
        /// <param name="zMagnitude">The z magnitude.</param>
        /// <param name="location">The location.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Vector3D(
            double xMagnitude,
            double yMagnitude,
            double zMagnitude,
            CartesianCoordinate3D location,
            double tolerance = Numbers.ZeroTolerance)
        {
            Xcomponent = xMagnitude;
            Ycomponent = yMagnitude;
            Zcomponent = zMagnitude;
            _location = location;
            Tolerance = tolerance;
        }

        #region Inherit Methods
        // https://msdn.microsoft.com/en-us/library/system.windows.media.media3d.vector3d(v=vs.110).aspx


        #endregion

        #region Methods

        /// <summary>
        /// Length of this vector.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Magnitude()
        {
            return NMath.Sqrt(MagnitudeSquared());
        }

        /// <summary>
        /// Gets the square of the length of this vector.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double MagnitudeSquared()
        {
            return Xcomponent.Squared() + Ycomponent.Squared() + Zcomponent.Squared();
        }

        /// <summary>
        /// Crosses the product.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>Vector3D.</returns>
        public Vector3D CrossProduct(Vector3D point)
        {
            double[] matrix = VectorLibrary.CrossProduct(
                Xcomponent, Ycomponent, Zcomponent, 
                point.Xcomponent, point.Ycomponent, point.Zcomponent);
            return new Vector3D(matrix[0], matrix[1], matrix[2]);
        }

        /// <summary>
        /// Dots the product.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>System.Double.</returns>
        public double DotProduct(Vector3D point)
        {
            return VectorLibrary.DotProduct(
                Xcomponent, Ycomponent, Zcomponent, 
                point.Xcomponent, point.Ycomponent, point.Zcomponent);
        }

        #endregion

        #region Operators & Equals


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Vector3D other)
        {
            return (NMath.Abs(Xcomponent - other.Xcomponent) < Tolerance) &&
                   (NMath.Abs(Ycomponent - other.Ycomponent) < Tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector3D) { return Equals((Vector3D)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Xcomponent.GetHashCode() ^ Ycomponent.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Vector3D a, Vector3D b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Vector3D a, Vector3D b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.Xcomponent + b.Xcomponent, a.Ycomponent + b.Ycomponent, a.Zcomponent + b.Zcomponent);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.Xcomponent - b.Xcomponent, a.Ycomponent - b.Ycomponent, a.Zcomponent - b.Zcomponent);
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator *(Vector3D a, double b)
        {
            return new Vector3D(a.Xcomponent * b, a.Ycomponent * b, a.Zcomponent * b);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator *(Vector3D a, Vector3D b)
        {
            return a.DotProduct(b);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator /(Vector3D a, double b)
        {
            return new Vector3D(a.Xcomponent / b, a.Ycomponent / b, a.Zcomponent / b);
        }


        #endregion
    }
}