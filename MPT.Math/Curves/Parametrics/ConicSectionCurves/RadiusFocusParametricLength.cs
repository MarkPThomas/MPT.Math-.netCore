// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="RadiusFocusParametricLength.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;
using System;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Class RadiusFocusParametricLength.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicLinearParametricBase" />
    /// <a href="https://web.ma.utexas.edu/users/m408s/m408d/CurrentWeb/LM10-6-3.php">Reference</a>
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicLinearParametricBase" />
    internal class RadiusFocusParametricLength : ConicLinearParametricBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadiusFocusParametricLength"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public RadiusFocusParametricLength(ConicSectionCurve parent) : base(parent)
        {
        }

        /// <summary>
        /// The radius measured from the focus as a function of the angle in local coordinates.
        /// <a href="https://web.ma.utexas.edu/users/m408s/m408d/CurrentWeb/LM10-6-3.php">Reference</a>
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double BaseByParameter(double angleRadians)
        {
            return _parent.Eccentricity * _parent.DistanceFromFocusToDirectrix / (1 - _parent.Eccentricity * Trig.Cos(angleRadians));
        }

        /// <summary>
        /// The differential change in radius corresponding with a differential change in the angle, measured from the focus as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double angleRadians)
        {
            return -1 * _parent.Eccentricity.Squared() * _parent.DistanceFromFocusToDirectrix * Trig.Sin(angleRadians) / (1 - _parent.Eccentricity * Trig.Cos(angleRadians));
        }

        /// <summary>
        /// Primes the double by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>Angle.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override double PrimeDoubleByParameter(double angleRadians)
        {
            throw new NotImplementedException();
        }
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
        public RadiusFocusParametricLength CloneParametric()
        {
            RadiusFocusParametricLength parametric = new RadiusFocusParametricLength(_parent);
            return parametric;
        }
        #endregion
    }
}
