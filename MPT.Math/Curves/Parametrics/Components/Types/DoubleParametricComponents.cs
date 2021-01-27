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

namespace MPT.Math.Curves.Parametrics.Components.Types
{
    /// <summary>
    /// Class containing the assigned components of a parametric equation in <see cref="double"/> coordinates and the corresponding curve object.
    /// This class has the basic components of differentiating and accessing the different parametric equations.
    /// Also contains operators to implement scaling of the parametric equations.
    /// Implements the <see cref="LinearParametricComponents{LinearParametricComponent}" />
    /// </summary>
    /// <seealso cref="LinearParametricComponents{LinearParametricComponent}" />
    internal abstract class DoubleParametricComponents : LinearParametricComponents<DoubleParametricComponent>
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public DoubleParametricComponents(Curve parent) : base(parent)
        {
            initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void initialize()
        {
            _doublePrime = new DoubleParametricComponent(primeDoubleByParameterScaled);
            _prime = new DoubleParametricComponent(primeByParameterScaled, _doublePrime);
            _base = new DoubleParametricComponent(baseByParameterScaled, _prime);

            initializeComponentList();
        }

        #region Operators
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static DoubleParametricComponents operator *(DoubleParametricComponents a, double b)
        {
            DoubleParametricComponents parametric = a.Clone() as DoubleParametricComponents;
            parametric._scale = b;
            return parametric;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static DoubleParametricComponents operator /(DoubleParametricComponents a, double b)
        {
            DoubleParametricComponents parametric = a.Clone() as DoubleParametricComponents;
            parametric._scale = 1d / b;
            return parametric;
        }

        #endregion
    }

    /// <summary>
    /// Class containing the type-defined component placeholders of a parametric equation in <see cref="double"/> coordinates and the corresponding curve object.
    /// This class has the basic components of differentiating and accessing the different parametric equations.
    /// Also contains functions to implement scaling of the parametric equations.
    /// Implements the <see cref="ParametricComponents{Double, T, Curve}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ParametricComponents{Double, T, Curve}" />
    internal abstract class LinearParametricComponents<T> :
        ParametricComponents<double, T, Curve>
        where T : ParametricEquation<double>
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="parent">The parent.</param>
        protected LinearParametricComponents(Curve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// The scaled component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected override double baseByParameterScaled(double parameter)
        {
            return _scale * BaseByParameter(parameter);
        }


        /// <summary>
        /// The scaled component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected override double primeByParameterScaled(double parameter)
        {
            return _scale * PrimeByParameter(parameter);
        }


        /// <summary>
        /// The scaled component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected override double primeDoubleByParameterScaled(double parameter)
        {
            return _scale * PrimeDoubleByParameter(parameter);
        }
        #endregion
    }
}
