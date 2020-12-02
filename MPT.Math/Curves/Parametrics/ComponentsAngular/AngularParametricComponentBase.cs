// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="AngularParametricBase.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace MPT.Math.Curves.Parametrics.ComponentsAngular
{
    /// <summary>
    /// Class AngularParametricBase.
    /// Implements the <see cref="AngularParametricComponents{AngularParametricComponent}" />
    /// </summary>
    /// <seealso cref="AngularParametricComponents{AngularParametricComponent}" />
    internal abstract class AngularParametricComponentBase : AngularParametricComponents<AngularParametricComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AngularParametricComponentBase"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public AngularParametricComponentBase(Curve parent) : base(parent)
        {
            initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void initialize()
        {
            _doublePrime = new AngularParametricComponent(primeDoubleByParameterScaled);
            _prime = new AngularParametricComponent(primeByParameterScaled, _doublePrime);
            _base = new AngularParametricComponent(baseByParameterScaled, _prime);
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
        public static AngularParametricComponentBase operator *(AngularParametricComponentBase a, double b)
        {
            AngularParametricComponentBase parametric = a.Clone() as AngularParametricComponentBase;
            parametric._scale = b;
            return parametric;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static AngularParametricComponentBase operator /(AngularParametricComponentBase a, double b)
        {
            AngularParametricComponentBase parametric = a.Clone() as AngularParametricComponentBase;
            parametric._scale = 1d / b;
            return parametric;
        }

        #endregion
    }
}
