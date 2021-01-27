// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="LogarithmicSpiralParametricX.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;

namespace MPT.Math.Curves.Parametrics.LogarithmicSpiralCurves
{
    /// <summary>
    /// Represents a logarithmic spiral curve in parametric equations defining the x-component and differentials.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LogarithmicSpiralCurves.LogarithmicSpiralParametricDoubleComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LogarithmicSpiralCurves.LogarithmicSpiralParametricDoubleComponents" />
    internal class LogarithmicSpiralParametricX : LogarithmicSpiralParametricDoubleComponents
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LogarithmicSpiralParametricX" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LogarithmicSpiralParametricX(LogarithmicSpiralCurve parent) : base(parent)
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
            return _parent.RadiusAtOrigin * Numbers.E.Pow(_parent.RadiusChangeWithRotation * angleRadians) * Trig.Cos(angleRadians);
        }


        /// <summary>
        /// Primes the by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double angleRadians)
        {
            return _parent.RadiusAtOrigin * Numbers.E.Pow(_parent.RadiusChangeWithRotation * angleRadians) * (Trig.Cos(angleRadians) - Trig.Sin(angleRadians));
        }


        /// <summary>
        /// Primes the double by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double angleRadians)
        {
            return _parent.RadiusAtOrigin * Numbers.E.Pow(_parent.RadiusChangeWithRotation * angleRadians) * -2 * Trig.Sin(angleRadians);
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
        public LogarithmicSpiralParametricX CloneParametric()
        {
            LogarithmicSpiralParametricX parametric = new LogarithmicSpiralParametricX(_parent as LogarithmicSpiralCurve);
            return parametric;
        }
        #endregion
    }
}
