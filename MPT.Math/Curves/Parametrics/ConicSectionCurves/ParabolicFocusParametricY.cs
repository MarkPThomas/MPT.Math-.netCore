// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ParabolicFocusParametricY.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Trigonometry;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Represents a parabolic curve in parametric equations defining the y-component about the focus and differentials.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicParametricDoubleComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicParametricDoubleComponents" />
    internal class ParabolicFocusParametricY : ConicParametricDoubleComponents
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicFocusParametricY"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ParabolicFocusParametricY(ParabolicCurve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials

        /// <summary>
        /// Y-coordinate on the curve in local coordinates about the focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the focus, in radians.</param>
        /// <returns>System.Double.</returns>
        public override double BaseByParameter(double angleRadians)
        {
            return (_parent.RadiusAboutFocusRight((Coordinates.Angle)angleRadians) * TrigonometryLibrary.Sin(angleRadians));
        }


        /// <summary>
        /// Primes the by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double angleRadians)
        {
            return (_parent.RadiusAboutFocusRight((Coordinates.Angle)angleRadians) * TrigonometryLibrary.Cos(angleRadians));
        }


        /// <summary>
        /// Primes the double by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double angleRadians)
        {
            return (-1 * _parent.RadiusAboutFocusRight((Coordinates.Angle)angleRadians) * TrigonometryLibrary.Sin(angleRadians));
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
        public ParabolicParametricX CloneParametric()
        {
            ParabolicParametricX parametric = new ParabolicParametricX(_parent as ParabolicCurve);
            return parametric;
        }
        #endregion
    }
}
