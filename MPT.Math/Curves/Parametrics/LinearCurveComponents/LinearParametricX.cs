﻿// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="LinearParametricX.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.LinearCurveComponents
{
    /// <summary>
    /// Represents a linear curve in parametric equations defining the x-component and differentials.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearCurveComponents.LinearParametricDoubleComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearCurveComponents.LinearParametricDoubleComponents" />
    internal class LinearParametricX : LinearParametricDoubleComponents
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearParametricX" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LinearParametricX(LinearCurve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// The component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override double BaseByParameter(double parameter)
        {
            return new LinearParametricP(_parent).BaseByParameter(parameter).X;
        }


        /// <summary>
        /// The component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double parameter)
        {
            return new LinearParametricP(_parent).PrimeByParameter(parameter).X;
        }


        /// <summary>
        /// The component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double parameter)
        {
            return new LinearParametricP(_parent).PrimeDoubleByParameter(parameter).X;
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return CloneParametric();
        }

        /// <summary>
        /// Clones the curve.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public LinearParametricX CloneParametric()
        {
            LinearParametricX parametric = new LinearParametricX(_parent as LinearCurve);
            return parametric;
        }
        #endregion
    }
}
