// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="BezierParametricX2.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.Beziers
{
    /// <summary>
    /// Class BezierParametricX2.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.Beziers.BezierParametricLinearBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.Beziers.BezierParametricLinearBase" />
    internal class BezierParametricX2 : BezierParametricLinearBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BezierParametricX2" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public BezierParametricX2(BezierCurve parent) : base(parent)
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
            return new BezierParametricP2(_parent).BaseByParameter(parameter).X;
        }


        /// <summary>
        /// The component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double parameter)
        {
            return new BezierParametricP2(_parent).PrimeByParameter(parameter).X;
        }


        /// <summary>
        /// The component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double parameter)
        {
            return new BezierParametricP2(_parent).PrimeDoubleByParameter(parameter).X;
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
        public BezierParametricX2 CloneParametric()
        {
            BezierParametricX2 parametric = new BezierParametricX2(_parent as BezierCurve);
            return parametric;
        }
        #endregion
    }
}
