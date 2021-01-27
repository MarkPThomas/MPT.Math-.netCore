// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="EllipticParametricX.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Trigonometry;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurveComponents
{
    /// <summary>
    /// Represents an elliptical curve in parametric equations defining the x-component and differentials.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ConicSectionCurveComponents.ConicParametricDoubleComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ConicSectionCurveComponents.ConicParametricDoubleComponents" />
    internal class EllipticalParametricX : ConicParametricDoubleComponents
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalParametricX" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public EllipticalParametricX(ConicSectionEllipticCurve parent) : base(parent)
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
            return _parent.DistanceFromVertexMajorToLocalOrigin * TrigonometryLibrary.Cos(angleRadians);
        }


        /// <summary>
        /// Primes the by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double angleRadians)
        {
            return -1 * _parent.DistanceFromVertexMajorToLocalOrigin * TrigonometryLibrary.Sin(angleRadians);
        }


        /// <summary>
        /// Primes the double by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double angleRadians)
        {
            return -1 * _parent.DistanceFromVertexMajorToLocalOrigin * TrigonometryLibrary.Cos(angleRadians);
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
        public EllipticalParametricX CloneParametric()
        {
            EllipticalParametricX parametric = new EllipticalParametricX(_parent as EllipticalCurve);
            return parametric;
        }
        #endregion
    }
}
