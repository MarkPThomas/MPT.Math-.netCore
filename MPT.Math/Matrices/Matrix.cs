// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-16-2020
// ***********************************************************************
// <copyright file="Matrix.cs" company="Mark P Thomas, Inc.">
//     2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

namespace MPT.Math.Matrices
{
    /// <summary>
    /// Struct Matrix
    /// </summary>
    /// <seealso cref="System.IEquatable{Matrix}" />
    public struct Matrix : IEquatable<Matrix>
    {
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the size of the dimension.
        /// </summary>
        /// <value>The size of the dimension.</value>
        public int DimensionSize { get; private set; }
        /// <summary>
        /// The matrix
        /// </summary>
        private readonly double[,] _matrix;

        // allow callers to initialize
        /// <summary>
        /// Gets or sets the <see cref="System.Double"/> with the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>System.Double.</returns>
        public double this[int x, int y]
        {
            get { return _matrix[x, y]; }
            set { _matrix[x, y] = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> struct.
        /// </summary>
        /// <param name="dimensionSize">Size of the dimension.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Matrix(int dimensionSize = 3, 
            double tolerance = Numbers.ZeroTolerance)
        {
            DimensionSize = dimensionSize;
            _matrix = new double[DimensionSize, DimensionSize];
            Tolerance = tolerance;
        }

        /// <summary>
        /// Gets the determinant.
        /// </summary>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static double GetDeterminant()
        {
            throw new NotImplementedException();
        }

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Matrix other)
        {
            if (DimensionSize != other.DimensionSize) return false;
            for (int x = 0; x < DimensionSize; x++)
            {
                for (int y = 0; y < DimensionSize; y++)
                {
                    if (!(NMath.Abs(_matrix[x, y] - other[x, y]) < Tolerance)) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Matrix) { return Equals((Matrix)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return _matrix.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Matrix a, Matrix b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Matrix a, Matrix b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="mat1">The mat1.</param>
        /// <param name="mat2">The mat2.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            if (mat1.DimensionSize != mat2.DimensionSize)
                throw new ArgumentOutOfRangeException($"Matrices have incompatible dimensions: {mat1.DimensionSize} vs. {mat2.DimensionSize}");
            Matrix newMatrix = new Matrix();

            for (int x = 0; x < mat1.DimensionSize; x++)
                for (int y = 0; y < mat1.DimensionSize; y++)
                    newMatrix[x, y] = mat1[x, y] + mat2[x, y];

            return newMatrix;
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="mat1">The mat1.</param>
        /// <param name="mat2">The mat2.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {
            if (mat1.DimensionSize != mat2.DimensionSize)
                throw new ArgumentOutOfRangeException($"Matrices have incompatible dimensions: {mat1.DimensionSize} vs. {mat2.DimensionSize}");
            Matrix newMatrix = new Matrix();

            for (int x = 0; x < mat1.DimensionSize; x++)
                for (int y = 0; y < mat1.DimensionSize; y++)
                    newMatrix[x, y] = mat1[x, y] - mat2[x, y];

            return newMatrix;
        }
        #endregion
    }
}
