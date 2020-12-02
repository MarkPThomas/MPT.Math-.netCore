// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="CartesianParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.ComponentsCartesian;
using System;

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Class CartesianParametric.
    /// Implements the <see cref="IParametricCartesian{CartesianParametricComponent}" />
    /// Implements the <see cref="System.ICloneable" />
    /// </summary>
    /// <seealso cref="IParametricCartesian{CartesianParametricComponent}" />
    /// <seealso cref="System.ICloneable" />
    public class CartesianParametricEquation : IParametricCartesian<CartesianParametricComponent>, ICloneable
    {
        /// <summary>
        /// The differentiation index
        /// </summary>
        protected int _differentiationIndex = 0;

        /// <summary>
        /// The x
        /// </summary>
        internal CartesianParametricComponentBase _component;
        /// <summary>
        /// The x-component, at position s.
        /// </summary>
        /// <value>The xcomponent.</value>
        public CartesianParametricComponent Component => _component[_differentiationIndex];

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianParametricEquation" /> class.
        /// </summary>
        protected CartesianParametricEquation()
        {

        }

        #region Methods: Public
        /// <summary>
        /// Returns the differential of the current parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public CartesianParametricEquation Differentiate()
        {
            CartesianParametricEquation differential = CloneParametric();
            differential._component.Differentiate();
            _differentiationIndex++;
            return differential;
        }

        /// <summary>
        /// Returns the current parametric function, differentiated to the specified # of times.
        /// </summary>
        /// <param name="index">The index to differentiate to, which must be greater than 0.</param>
        /// <returns>HyperbolicParametric.</returns>
        public CartesianParametricEquation DifferentiateBy(int index)
        {
            CartesianParametricEquation differential = CloneParametric();
            differential._component.DifferentiateBy(index);
            _differentiationIndex = index;
            return differential;
        }

        /// <summary>
        /// Returns the first differential of the parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public CartesianParametricEquation DifferentialFirst()
        {
            return DifferentiateBy(1);
        }

        /// <summary>
        /// Returns the second differential of the parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public CartesianParametricEquation DifferentialSecond()
        {
            return DifferentiateBy(2);
        }

        /// <summary>
        /// Determines whether this instance can be differentiated further.
        /// </summary>
        /// <returns><c>true</c> if this instance has differential; otherwise, <c>false</c>.</returns>
        public bool HasDifferential()
        {
            return (_component.HasDifferential());
        }

        #endregion

        #region Operators
        ///// <summary>
        ///// Implements the + operator.
        ///// </summary>
        ///// <param name="a">a.</param>
        ///// <param name="b">The b.</param>
        ///// <returns>The result of the operator.</returns>
        //public static VectorParametric operator +(VectorParametric a, VectorParametric b)
        //{
        //    return new VectorParametric(a.Xcomponent + b.Xcomponent, a.Ycomponent + b.Ycomponent);
        //}

        ///// <summary>
        ///// Implements the - operator.
        ///// </summary>
        ///// <param name="a">a.</param>
        ///// <param name="b">The b.</param>
        ///// <returns>The result of the operator.</returns>
        //public static VectorParametric operator -(VectorParametric a, VectorParametric b)
        //{
        //    return new VectorParametric(a.Xcomponent - b.Xcomponent, a.Ycomponent - b.Ycomponent);
        //}


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianParametricEquation operator *(CartesianParametricEquation a, double b)
        {
            CartesianParametricEquation parametric = a.CloneParametric();
            parametric._component *= b;
            return parametric;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianParametricEquation operator /(CartesianParametricEquation a, double b)
        {
            CartesianParametricEquation parametric = a.CloneParametric();
            parametric._component /= b;
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
        public CartesianParametricEquation CloneParametric()
        {
            CartesianParametricEquation parametric = new CartesianParametricEquation();
            parametric._component.Clone();
            parametric._differentiationIndex = _differentiationIndex;
            return parametric;
        }
        #endregion
    }
}
