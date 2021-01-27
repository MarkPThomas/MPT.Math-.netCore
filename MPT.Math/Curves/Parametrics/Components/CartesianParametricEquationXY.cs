// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="CurveParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.Components.Types;
using System;

namespace MPT.Math.Curves.Parametrics.Components
{
    /// <summary>
    /// Represents a set of parametric equations in x- and y-components returned as <see cref="double"/>, such as 2D coordinate systems for x- and y-axes.
    /// These equations can be scaled and differentiated.
    /// Implements the <see cref="IParametricCartesianXY{CurveParametricComponent}" />
    /// Implements the <see cref="ICloneable" />
    /// </summary>
    /// <seealso cref="IParametricCartesianXY{CurveParametricComponent}" />
    /// <seealso cref="ICloneable" />
    public class CartesianParametricEquationXY : IParametricCartesianXY<DoubleParametricComponent>, ICloneable
    {
        /// <summary>
        /// The differentiation index
        /// </summary>
        protected int _differentiationIndex = 0;

        /// <summary>
        /// The x-component, at position s.
        /// </summary>
        internal DoubleParametricComponents _x;
        /// <summary>
        /// The x-component, at position s.
        /// </summary>
        /// <value>The xcomponent.</value>
        public DoubleParametricComponent Xcomponent => _x[_differentiationIndex];

        /// <summary>
        /// The y-component, at position s.
        /// </summary>
        internal DoubleParametricComponents _y;
        /// <summary>
        /// The y-component, at position s.
        /// </summary>
        /// <value>The ycomponent.</value>
        public DoubleParametricComponent Ycomponent => _y[_differentiationIndex];

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianParametricEquationXY" /> class.
        /// </summary>
        protected CartesianParametricEquationXY()
        {

        }

        #region Methods: Public
        /// <summary>
        /// Returns the differential of the current parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public CartesianParametricEquationXY Differentiate()
        {
            CartesianParametricEquationXY differential = CloneParametric();
            differential._x = _x.Differentiate(_differentiationIndex) as DoubleParametricComponents;
            differential._y = _y.Differentiate(_differentiationIndex) as DoubleParametricComponents;
            differential._differentiationIndex++;
            return differential;
        }

        /// <summary>
        /// Returns the current parametric function, differentiated to the specified # of times.
        /// </summary>
        /// <param name="index">The index to differentiate to, which must be greater than 0.</param>
        /// <returns>HyperbolicParametric.</returns>
        public CartesianParametricEquationXY DifferentiateBy(int index)
        {
            CartesianParametricEquationXY differential = CloneParametric();
            differential._x = _x.DifferentiateBy(index) as DoubleParametricComponents;
            differential._y = _y.DifferentiateBy(index) as DoubleParametricComponents;
            differential._differentiationIndex = index;
            return differential;
        }

        /// <summary>
        /// Returns the first differential of the parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public CartesianParametricEquationXY DifferentialFirst()
        {
            return DifferentiateBy(1);
        }

        /// <summary>
        /// Returns the second differential of the parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public CartesianParametricEquationXY DifferentialSecond()
        {
            return DifferentiateBy(2);
        }

        /// <summary>
        /// Determines whether this instance can be differentiated further.
        /// </summary>
        /// <returns><c>true</c> if this instance has differential; otherwise, <c>false</c>.</returns>
        public bool HasDifferential()
        {
            return (_x.HasDifferential() || _y.HasDifferential());
        }
        #endregion

        #region Operators
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianParametricEquationXY operator *(CartesianParametricEquationXY a, double b)
        {
            CartesianParametricEquationXY parametric = a.CloneParametric();
            parametric._x *= b;
            parametric._y *= b;
            return parametric;
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianParametricEquationXY operator /(CartesianParametricEquationXY a, double b)
        {
            CartesianParametricEquationXY parametric = a.CloneParametric();
            parametric._x /= b;
            parametric._y /= b;
            return parametric;
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return CloneParametric();
        }

        /// <summary>
        /// Clones the curve.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public CartesianParametricEquationXY CloneParametric()
        {
            CartesianParametricEquationXY parametric = new CartesianParametricEquationXY();
            parametric._x = _x;
            parametric._y = _y;
            parametric._differentiationIndex = _differentiationIndex;
            return parametric;
        }
        #endregion
    }
}
