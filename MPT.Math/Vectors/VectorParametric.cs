// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="VectorParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.Math.Curves.Parametrics.Components;
using System;

namespace MPT.Math.Vectors
{
    // TODO: Make Generic form of VectorParametric for this to derive from.
    /// <summary>
    /// Represents a parametric vector in 2D space.
    /// This is used for non-linear curves.
    /// Implements the <see cref="IEquatable{VectorParametric}" />
    /// Implements the <see cref="ITolerance" />
    /// </summary>
    /// <seealso cref="IEquatable{Vector}" />
    /// <seealso cref="ITolerance" />
    public class VectorParametric: ITolerance 
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; } = 0.00001;

        /// <summary>
        /// The associated parametric function.
        /// </summary>
        protected CartesianParametricEquationXY _curveParametric;
        /// <summary>
        /// The associated parametric function.
        /// </summary>
        /// <value>The curve parametric.</value>
        public CartesianParametricEquationXY CurveParametric => _curveParametric;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorParametric" /> class that is based on the provided parametric function.
        /// </summary>
        /// <param name="parametricCartesian">The parametric function.</param>
        /// <param name="tolerance">The tolerance.</param>
        public VectorParametric(CartesianParametricEquationXY parametricCartesian, double tolerance = Numbers.ZeroTolerance)
        {
            _curveParametric = parametricCartesian;

            if (tolerance != Numbers.ZeroTolerance)
            {
                Tolerance = tolerance;
            }
        }

        #region Methods: Public
        /// <summary>
        /// Returns a differential of the parametric vector.
        /// For any component that no longer has a differential, the associated function returns a pre-defined constant value.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        public VectorParametric Differentiate()
        {
            CartesianParametricEquationXY differential = _curveParametric.Differentiate() as CartesianParametricEquationXY;
            return new VectorParametric(differential, Tolerance);
        }

        /// <summary>
        /// Determines whether this instance can be differentiated further.
        /// </summary>
        /// <returns><c>true</c> if this instance has differential; otherwise, <c>false</c>.</returns>
        public bool HasDifferential()
        {
            return _curveParametric.HasDifferential();
        }

        /// <summary>
        /// Magnitudes the specified angle radians.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public double Magnitude(double angleRadians)
        {
            return VectorLibrary.Magnitude(
                _curveParametric.Xcomponent.ValueAt(angleRadians),
                _curveParametric.Ycomponent.ValueAt(angleRadians));
        }

        /// <summary>
        /// Curvatures the specified angle radians.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public double Curvature(double angleRadians)
        {
            VectorParametric unitTangentVector = UnitTangentVectorAt(angleRadians);
            VectorParametric unitTangentVectorPrime = unitTangentVector.Differentiate();
            VectorParametric vectorPrime = Differentiate();

            return unitTangentVectorPrime.Magnitude(angleRadians) / vectorPrime.Magnitude(angleRadians);
        }

        /// <summary>
        /// Units the vector at.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>VectorParametric.</returns>
        public VectorParametric UnitVectorAt(double angleRadians)
        {
            VectorParametric vector = new VectorParametric(_curveParametric, Tolerance);

            return vector / vector.Magnitude(angleRadians);
        }

        /// <summary>
        /// Units the tangent vector at.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>VectorParametric.</returns>
        public VectorParametric UnitTangentVectorAt(double angleRadians)
        {
            VectorParametric vectorPrime = Differentiate();

            return vectorPrime / vectorPrime.Magnitude(angleRadians);
        }

        /// <summary>
        /// Units the normal vector at.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>VectorParametric.</returns>
        public VectorParametric UnitNormalVectorAt(double angleRadians)
        {
            VectorParametric unitTangentVector = UnitTangentVectorAt(angleRadians);
            VectorParametric unitTangentVectorPrime = unitTangentVector.Differentiate();

            return unitTangentVectorPrime / unitTangentVectorPrime.Magnitude(angleRadians);
        }

        /// <summary>
        /// Converts to vectorat.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>Vector.</returns>
        public Vector ToVectorAt(double angleRadians)
        {
            double xComponent = _curveParametric.Xcomponent.ValueAt(angleRadians);
            double yComponent = _curveParametric.Ycomponent.ValueAt(angleRadians);
            return new Vector(xComponent, yComponent);
        }

        /// <summary>
        /// Dot product of two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public double DotProductAt(VectorParametric vector, double angleRadians)
        {
            return VectorLibrary.DotProduct(
                _curveParametric.Xcomponent.ValueAt(angleRadians),
                _curveParametric.Ycomponent.ValueAt(angleRadians),
                vector._curveParametric.Xcomponent.ValueAt(angleRadians),
                vector._curveParametric.Ycomponent.ValueAt(angleRadians));
        }

        /// <summary>
        /// Cross-product of two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public double CrossProductAt(VectorParametric vector, double angleRadians)
        {
            return VectorLibrary.CrossProduct(
                _curveParametric.Xcomponent.ValueAt(angleRadians), 
                _curveParametric.Ycomponent.ValueAt(angleRadians), 
                vector._curveParametric.Xcomponent.ValueAt(angleRadians), 
                vector._curveParametric.Ycomponent.ValueAt(angleRadians));
        }

        #endregion

        #region Operators
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static VectorParametric operator *(VectorParametric a, double b)
        {
            return new VectorParametric(a._curveParametric * b);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static VectorParametric operator /(VectorParametric a, double b)
        {
            return new VectorParametric(a._curveParametric / b);
        }
        #endregion
    }
}
