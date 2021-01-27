// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="LogarithmicSpiralParametricY.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;
using System;

namespace MPT.Math.Curves.Parametrics.LogarithmicSpiralCurveComponents
{
    /// <summary>
    /// Represents a logarithmic spiral curve in parametric equations defining the y-component and differentials.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LogarithmicSpiralCurveComponents.LogarithmicSpiralParametricDoubleComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LogarithmicSpiralCurveComponents.LogarithmicSpiralParametricDoubleComponents" />
    internal class LogarithmicSpiralParametricY : LogarithmicSpiralParametricDoubleComponents
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LogarithmicSpiralParametricX" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LogarithmicSpiralParametricY(LogarithmicSpiralCurve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// Bases the by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double BaseByParameter(double angleRadians)
        {
            return _parent.RadiusAtOrigin * Numbers.E.Pow(_parent.RadiusChangeWithRotation * angleRadians) * Trig.Sin(angleRadians);
        }


        /// <summary>
        /// Primes the by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double angleRadians)
        {
            return _parent.RadiusAtOrigin * Numbers.E.Pow(_parent.RadiusChangeWithRotation * angleRadians) * (Trig.Cos(angleRadians) + Trig.Sin(angleRadians));
        }


        /// <summary>
        /// Primes the double by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double angleRadians)
        {
            return _parent.RadiusAtOrigin * Numbers.E.Pow(_parent.RadiusChangeWithRotation * angleRadians) * 2 * Trig.Cos(angleRadians);
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
        public LogarithmicSpiralParametricY CloneParametric()
        {
            LogarithmicSpiralParametricY parametric = new LogarithmicSpiralParametricY(_parent as LogarithmicSpiralCurve);
            return parametric;
        }
        #endregion
    }
}
