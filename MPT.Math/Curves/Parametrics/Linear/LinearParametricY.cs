// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="LinearParametricY.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace MPT.Math.Curves.Parametrics.Linear
{
    /// <summary>
    /// Class LinearParametricY.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.Linear.LinearParametricLinearBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.Linear.LinearParametricLinearBase" />
    internal class LinearParametricY : LinearParametricLinearBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearParametricY" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LinearParametricY(LinearCurve parent) : base(parent)
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
            return new LinearParametricP(_parent).BaseByParameter(parameter).Y;
        }


        /// <summary>
        /// The component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double parameter)
        {
            return new LinearParametricP(_parent).PrimeByParameter(parameter).Y;
        }


        /// <summary>
        /// The component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double parameter)
        {
            return new LinearParametricP(_parent).PrimeDoubleByParameter(parameter).Y;
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
        public LinearParametricY CloneParametric()
        {
            LinearParametricY parametric = new LinearParametricY(_parent as LinearCurve);
            return parametric;
        }
        #endregion
    }
}
