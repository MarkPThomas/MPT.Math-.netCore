// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="CartesianParametricBase.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.ComponentsCartesian
{
    /// <summary>
    /// Class CartesianParametricBase.
    /// Implements the <see cref="CartesianParametricComponents{CartesianParametricComponent}" />
    /// </summary>
    /// <seealso cref="CartesianParametricComponents{CartesianParametricComponent}" />
    internal abstract class CartesianParametricComponentBase : CartesianParametricComponents<CartesianParametricComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianParametricComponentBase" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public CartesianParametricComponentBase(Curve parent) : base(parent)
        {
            initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void initialize()
        {
            _doublePrime = new CartesianParametricComponent(primeDoubleByParameterScaled);
            _prime = new CartesianParametricComponent(primeByParameterScaled, _doublePrime);
            _base = new CartesianParametricComponent(baseByParameterScaled, _prime);
        }

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
        public static CartesianParametricComponentBase operator *(CartesianParametricComponentBase a, double b)
        {
            CartesianParametricComponentBase parametric = a.Clone() as CartesianParametricComponentBase;
            parametric._scale = b;
            return parametric;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianParametricComponentBase operator /(CartesianParametricComponentBase a, double b)
        {
            CartesianParametricComponentBase parametric = a.Clone() as CartesianParametricComponentBase;
            parametric._scale = 1d / b;
            return parametric;
        }

        #endregion
    }
}
