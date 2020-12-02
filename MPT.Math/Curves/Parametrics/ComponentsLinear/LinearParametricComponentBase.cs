// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="LinearParametricBase.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.ComponentsLinear
{
    /// <summary>
    /// Class LinearParametricBase.
    /// Implements the <see cref="LinearParametricComponents{LinearParametricComponent}" />
    /// </summary>
    /// <seealso cref="LinearParametricComponents{LinearParametricComponent}" />
    internal abstract class LinearParametricComponentBase : LinearParametricComponents<LinearParametricComponent>
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LinearParametricComponentBase(Curve parent) : base(parent)
        {
            initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void initialize()
        {
            _doublePrime = new LinearParametricComponent(primeDoubleByParameterScaled);
            _prime = new LinearParametricComponent(primeByParameterScaled, _doublePrime);
            _base = new LinearParametricComponent(baseByParameterScaled, _prime);
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
        public static LinearParametricComponentBase operator *(LinearParametricComponentBase a, double b)
        {
            LinearParametricComponentBase parametric = a.Clone() as LinearParametricComponentBase;
            parametric._scale = b;
            return parametric;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static LinearParametricComponentBase operator /(LinearParametricComponentBase a, double b)
        {
            LinearParametricComponentBase parametric = a.Clone() as LinearParametricComponentBase;
            parametric._scale = 1d / b;
            return parametric;
        }

        #endregion
    }
}
