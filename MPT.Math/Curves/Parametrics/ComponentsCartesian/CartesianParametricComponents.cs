// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="CartesianParametricComponents.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.ComponentsCartesian
{
    /// <summary>
    /// Class CartesianParametricComponents.
    /// Implements the <see cref="CurveParametricComponents{CartesianCoordinate, T, Curve}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CurveParametricComponents{CartesianCoordinate, T, Curve}" />
    internal abstract class CartesianParametricComponents<T> :
        CurveParametricComponents<CartesianCoordinate, T, Curve>
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
