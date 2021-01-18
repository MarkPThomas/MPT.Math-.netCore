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
using MPT.Math.Curves.Parametrics.ComponentsLinear;
using System;

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Class CurveParametric.
    /// Implements the <see cref="IParametricLinear{CurveParametricComponent}" />
    /// Implements the <see cref="ICloneable" />
    /// </summary>
    /// <seealso cref="IParametricLinear{CurveParametricComponent}" />
    /// <seealso cref="ICloneable" />
    public class LinearParametricEquation : IParametricLinear<LinearParametricComponent>, ICloneable
    {
        /// <summary>
        /// The differentiation index
        /// </summary>
        protected int _differentiationIndex = 0;

        /// <summary>
        /// The x
        /// </summary>
        internal LinearParametricComponentBase _x;
        /// <summary>
        /// The x-component, at position s.
        /// </summary>
        /// <value>The xcomponent.</value>
        public LinearParametricComponent Xcomponent => _x[_differentiationIndex];

        /// <summary>
        /// The y
        /// </summary>
        internal LinearParametricComponentBase _y;
        /// <summary>
        /// The y-component, at position s.
        /// </summary>
        /// <value>The ycomponent.</value>
        public LinearParametricComponent Ycomponent => _y[_differentiationIndex];

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearParametricEquation" /> class.
        /// </summary>
        protected LinearParametricEquation()
        {

        }

        #region Methods: Public
        /// <summary>
        /// Returns the differential of the current parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public LinearParametricEquation Differentiate()
        {
            LinearParametricEquation differential = CloneParametric();
            differential._x = _x.Differentiate(_differentiationIndex) as LinearParametricComponentBase;
            differential._y = _y.Differentiate(_differentiationIndex) as LinearParametricComponentBase;
            differential._differentiationIndex++;
            return differential;
        }

        /// <summary>
        /// Returns the current parametric function, differentiated to the specified # of times.
        /// </summary>
        /// <param name="index">The index to differentiate to, which must be greater than 0.</param>
        /// <returns>HyperbolicParametric.</returns>
        public LinearParametricEquation DifferentiateBy(int index)
        {
            LinearParametricEquation differential = CloneParametric();
            differential._x = _x.DifferentiateBy(index) as LinearParametricComponentBase;
            differential._y = _y.DifferentiateBy(index) as LinearParametricComponentBase;
            differential._differentiationIndex = index;
            return differential;
        }

        /// <summary>
        /// Returns the first differential of the parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public LinearParametricEquation DifferentialFirst()
        {
            return DifferentiateBy(1);
        }

        /// <summary>
        /// Returns the second differential of the parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public LinearParametricEquation DifferentialSecond()
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
        public static LinearParametricEquation operator *(LinearParametricEquation a, double b)
        {
            LinearParametricEquation parametric = a.CloneParametric();
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
        public static LinearParametricEquation operator /(LinearParametricEquation a, double b)
        {
            LinearParametricEquation parametric = a.CloneParametric();
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
        public LinearParametricEquation CloneParametric()
        {
            LinearParametricEquation parametric = new LinearParametricEquation();
            parametric._x = _x;
            parametric._y = _y;
            parametric._differentiationIndex = _differentiationIndex;
            return parametric;
        }
        #endregion
    }
}
