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
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.Components.Types
{
    /// <summary>
    /// Class containing the assigned components of a parametric equation in <see cref="CartesianCoordinate"/> coordinates and the corresponding curve object.
    /// This class has the basic components of differentiating and accessing the different parametric equations.
    /// Also contains operators to implement scaling of the parametric equations.
    /// Implements the <see cref="CartesianParametricComponents{CartesianParametricComponent}" />
    /// </summary>
    /// <seealso cref="CartesianParametricComponents{CartesianParametricComponent}" />
    internal abstract class CartesianParametricComponents : CartesianParametricComponents<CartesianParametricComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianParametricComponents" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public CartesianParametricComponents(Curve parent) : base(parent)
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
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianParametricComponents operator *(CartesianParametricComponents a, double b)
        {
            CartesianParametricComponents parametric = a.Clone() as CartesianParametricComponents;
            parametric._scale = b;
            return parametric;
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianParametricComponents operator /(CartesianParametricComponents a, double b)
        {
            CartesianParametricComponents parametric = a.Clone() as CartesianParametricComponents;
            parametric._scale = 1d / b;
            return parametric;
        }
        #endregion
    }

    /// <summary>
    /// Class containing the type-defined component placeholders of a parametric equation in <see cref="CartesianCoordinate"/> coordinates and the corresponding curve object.
    /// This class has the basic components of differentiating and accessing the different parametric equations.
    /// Also contains functions to implement scaling of the parametric equations.
    /// Implements the <see cref="ParametricComponents{CartesianCoordinate, T, Curve}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ParametricComponents{CartesianCoordinate, T, Curve}" />
    internal abstract class CartesianParametricComponents<T> :
        ParametricComponents<CartesianCoordinate, T, Curve>
        where T : ParametricEquation<CartesianCoordinate>
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="parent">The parent.</param>
        protected CartesianParametricComponents(Curve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// The scaled component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected override CartesianCoordinate baseByParameterScaled(double parameter)
        {
            return _scale * BaseByParameter(parameter);
        }


        /// <summary>
        /// The scaled component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected override CartesianCoordinate primeByParameterScaled(double parameter)
        {
            return _scale * PrimeByParameter(parameter);
        }


        /// <summary>
        /// The scaled component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected override CartesianCoordinate primeDoubleByParameterScaled(double parameter)
        {
            return _scale * PrimeDoubleByParameter(parameter);
        }
        #endregion
    }
}
