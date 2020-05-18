// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 01-28-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="Vector.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using System;
using NMath = System.Math;

namespace MPT.Math.Vectors
{
    /// <summary>
    /// Library of methods related to vectors.
    /// </summary>
    /// <seealso cref="System.IEquatable{Vector}" />
    public struct Vector : IEquatable<Vector>
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
        /// The location
        /// </summary>
        private CartesianCoordinate _location;
        /// <summary>
        /// Gets the location of this vector in Euclidean space.
        /// </summary>
        /// <value>The location.</value>
        public CartesianCoordinate Location => _location;
        #endregion

        /// <summary>
        /// Initializes the class with a vector structure.
        /// </summary>
        /// <param name="xMagnitude">The x magnitude.</param>
        /// <param name="yMagnitude">The y magnitude.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Vector(
            double xMagnitude, 
            double yMagnitude,
            double tolerance = Numbers.ZeroTolerance)
        {
            _location = new CartesianCoordinate();
            Xcomponent = xMagnitude;
            Ycomponent = yMagnitude;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Initializes the class with a vector structure and a point coinciding with the location of the vector.
        /// </summary>
        /// <param name="xMagnitude">The x magnitude.</param>
        /// <param name="yMagnitude">The y magnitude.</param>
        /// <param name="location">The location.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Vector(
            double xMagnitude, 
            double yMagnitude,
            CartesianCoordinate location,
            double tolerance = Numbers.ZeroTolerance)
        {
            Xcomponent = xMagnitude;
            Ycomponent = yMagnitude;
            _location = location;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> struct.
        /// </summary>
        /// <param name="pointI">The point i.</param>
        /// <param name="pointJ">The point j.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Vector(
            CartesianCoordinate pointI,
            CartesianCoordinate pointJ,
            double tolerance = Numbers.ZeroTolerance)
        {
            Xcomponent = pointI.X - pointJ.X;
            Ycomponent = pointI.Y - pointJ.Y;
            _location = pointI;
            Tolerance = tolerance;
        }

        #region Inherit Methods
        // https://msdn.microsoft.com/en-us/library/system.windows.vector(v=vs.110).aspx


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
            return Xcomponent.Squared() + Ycomponent.Squared();
        }

        /// <summary>
        /// True: Segments are parallel, on the same line, oriented in the same direction.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if [is collinear same direction] [the specified vector]; otherwise, <c>false</c>.</returns>
        public bool IsCollinearSameDirection(Vector vector)
        {
            return VectorLibrary.IsCollinearSameDirection(this, vector, Tolerance);
        }

        /// <summary>
        /// Vectors form a concave angle.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if the specified vector is concave; otherwise, <c>false</c>.</returns>
        public bool IsConcave(Vector vector)
        {
            return VectorLibrary.IsConcave(this, vector);
        }

        /// <summary>
        /// Vectors form a 90 degree angle.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if the specified vector is orthogonal; otherwise, <c>false</c>.</returns>
        public bool IsOrthogonal(Vector vector)
        {
            return VectorLibrary.IsOrthogonal(this, vector, Tolerance);
        }

        /// <summary>
        /// Vectors form a convex angle.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if the specified vector is convex; otherwise, <c>false</c>.</returns>
        public bool IsConvex(Vector vector)
        {
            return VectorLibrary.IsConvex(this, vector, Tolerance);
        }

        /// <summary>
        /// True: Segments are parallel, on the same line, oriented in the opposite direction.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if [is collinear opposite direction] [the specified vector]; otherwise, <c>false</c>.</returns>
        public bool IsCollinearOppositeDirection(Vector vector)
        {
            return VectorLibrary.IsCollinearOppositeDirection(this, vector, Tolerance);
        }



        /// <summary>
        /// True: The concave side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool IsConcaveInside(Vector vector)
        {
            return VectorLibrary.IsConcaveInside(this, vector, Tolerance);
        }

        /// <summary>
        /// True: The convex side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool IsConvexInside(Vector vector)
        {
            return VectorLibrary.IsConvexInside(this, vector, Tolerance);
        }





        /// <summary>
        /// Returns a value indicating the concavity of the vectors.
        /// 1 = Pointing the same way.
        /// &gt; 0 = Concave.
        /// 0 = Orthogonal.
        /// &lt; 0 = Convex.
        /// -1 = Pointing the exact opposite way.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double ConcavityCollinearity(Vector vector)
        {
            return VectorLibrary.ConcavityCollinearity(this, vector);
        }

        /// <summary>
        /// Dot product of two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double DotProduct(Vector vector)
        {
            return VectorLibrary.DotProduct(Xcomponent, vector.Xcomponent, Ycomponent, vector.Ycomponent);
        }

        /// <summary>
        /// Cross-product of two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double CrossProduct(Vector vector)
        {
            return VectorLibrary.CrossProduct(Xcomponent, vector.Xcomponent, Ycomponent, vector.Ycomponent);
        }



        /// <summary>
        /// Returns the angle [radians] of a vector from the origin axis (x, positive, +ccw).
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Angle()
        {
            return VectorLibrary.Angle(Ycomponent, Xcomponent);
        }

        /// <summary>
        /// Returns the angle [radians] between the two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double Angle(Vector vector)
        {
            return VectorLibrary.Angle(this, vector);
        }

        /// <summary>
        /// Returns the area between two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double Area(Vector vector)
        {
            return (0.5 * (CrossProduct(vector)));
        }


        #endregion

        #region Operators & Equals


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Vector other)
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
            if (obj is Vector) { return Equals((Vector)obj); }
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
        public static bool operator ==(Vector a, Vector b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Vector a, Vector b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.Xcomponent + b.Xcomponent, a.Ycomponent + b.Ycomponent);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.Xcomponent - b.Xcomponent, a.Ycomponent - b.Ycomponent);
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator *(Vector a, double b)
        {
            return new Vector(a.Xcomponent * b, a.Ycomponent * b);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator *(Vector a, Vector b)
        {
            return a.DotProduct(b);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator /(Vector a, double b)
        {
            return new Vector(a.Xcomponent / b, a.Ycomponent / b);
        }

        
        #endregion
    }
}
