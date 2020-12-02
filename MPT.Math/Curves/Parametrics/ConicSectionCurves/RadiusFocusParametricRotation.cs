// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="RadiusFocusParametricRotation.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using System;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Class RadiusFocusParametricRotation.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicAngularParametricBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicAngularParametricBase" />
    internal class RadiusFocusParametricRotation : ConicAngularParametricBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadiusFocusParametricRotation"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public RadiusFocusParametricRotation(ConicSectionCurve parent) : base(parent)
        {
        }

        /// <summary>
        /// Bases the by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>Angle.</returns>
        public override Angle BaseByParameter(double angleRadians)
        {
            return new Angle(angleRadians);
        }

        /// <summary>
        /// Primes the by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>Angle.</returns>
        public override Angle PrimeByParameter(double angleRadians)
        {
            return new Angle(0);
        }

        /// <summary>
        /// Primes the double by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>Angle.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Angle PrimeDoubleByParameter(double angleRadians)
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
        public RadiusFocusParametricRotation CloneParametric()
        {
            RadiusFocusParametricRotation parametric = new RadiusFocusParametricRotation(_parent);
            return parametric;
        }
        #endregion
    }
}
