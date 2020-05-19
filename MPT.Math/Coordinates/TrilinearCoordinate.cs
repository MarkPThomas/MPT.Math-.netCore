// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-18-2020
// ***********************************************************************
// <copyright file="TrilinearCoordinate.cs" company="Mark P Thomas, Inc.">
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
    /// A point relative to a given triangle describe the relative directed distances from the three sidelines of the triangle.
    /// See <a href="https://en.wikipedia.org/wiki/Trilinear_coordinates#Formulas">Wikipedia</a>. 
    /// </summary>
    /// <seealso cref="System.IEquatable{TrilinearCoordinate}" />
    public struct TrilinearCoordinate : IEquatable<TrilinearCoordinate>, ICoordinate
    {
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X { get; private set; }


        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; private set; }


        /// <summary>
        /// Gets the z.
        /// </summary>
        /// <value>The z.</value>
        public double Z { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrilinearCoordinate" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="tolerance">The tolerance.</param>
        public TrilinearCoordinate(double x, double y, double z,
            double tolerance = Numbers.ZeroTolerance)
        {
            X = x;
            Y = y;
            Z = z;
            Tolerance = tolerance;
        }

        /// <summary>
        /// To the cartesian.
        /// </summary>
        /// <param name="vertexA">The vertex a.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate ToCartesian(CartesianCoordinate vertexA, CartesianCoordinate vertexB, CartesianCoordinate vertexC)
        {
            double sideA = (vertexC - vertexB).Length();
            double sideB = (vertexA - vertexC).Length();
            double sideC = (vertexB - vertexA).Length();

            double denominator = sideA * X + sideB * Y + sideC * Z;

            double weight1 = (sideA * X) / denominator;
            double weight2 = (sideB * Y) / denominator;
            double weight3 = (sideC * Z) / denominator;

            CartesianCoordinate pVector = weight1 * vertexA + weight2 * vertexB + weight3 * vertexC;

            return new CartesianCoordinate(pVector.X, pVector.Y, Tolerance);
        }

        /// <summary>
        /// To the barycentric.
        /// </summary>
        /// <param name="vertexA">The vertex a.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>BarycentricCoordinate.</returns>
        public BarycentricCoordinate ToBarycentric(CartesianCoordinate vertexA, CartesianCoordinate vertexB, CartesianCoordinate vertexC)
        {
            double sideA = (vertexC - vertexB).Length();
            double sideB = (vertexA - vertexC).Length();
            double sideC = (vertexB - vertexA).Length();

            return new BarycentricCoordinate
                        (
                            alpha: X * sideA,
                            beta: Y * sideB,
                            gamma: Z * sideC
                        );
        }

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(TrilinearCoordinate other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return X.IsEqualTo(other.X, tolerance) &&
                   Y.IsEqualTo(other.Y, tolerance) &&
                   Z.IsEqualTo(other.Z, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is TrilinearCoordinate) { return Equals((TrilinearCoordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TrilinearCoordinate a, TrilinearCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TrilinearCoordinate a, TrilinearCoordinate b)
        {
            return !a.Equals(b);
        }
        #endregion
    }
}
