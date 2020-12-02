// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="AngularParametricComponents.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.ComponentsAngular
{
    /// <summary>
    /// Class AngularParametricComponents.
    /// Implements the <see cref="CurveParametricComponents{Angle, T, Curve}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CurveParametricComponents{Angle, T, Curve}" />
    internal abstract class AngularParametricComponents<T> :
        CurveParametricComponents<Angle, T, Curve>
        where T : ParametricEquation<Angle>
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="parent">The parent.</param>
        protected AngularParametricComponents(Curve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// The scaled component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected override Angle baseByParameterScaled(double parameter)
        {
            return _scale * BaseByParameter(parameter);
        }


        /// <summary>
        /// The scaled component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected override Angle primeByParameterScaled(double parameter)
        {
            return _scale * PrimeByParameter(parameter);
        }


        /// <summary>
        /// The scaled component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected override Angle primeDoubleByParameterScaled(double parameter)
        {
            return _scale * PrimeDoubleByParameter(parameter);
        }
        #endregion
    }
}
