// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="EllipticParametricY.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Trigonometry;


namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Elliptics
{
    /// <summary>
    /// Represents an elliptical curve in parametric equations defining the y-component and differentials.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicParametricDoubleComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicParametricDoubleComponents" />
    internal class EllipticalParametricY : ConicParametricDoubleComponents
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalParametricY"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public EllipticalParametricY(ConicSectionEllipticCurve parent) : base(parent)
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
            return _parent.DistanceFromVertexMinorToMajorAxis * TrigonometryLibrary.Sin(angleRadians);
        }


        /// <summary>
        /// Primes the by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double angleRadians)
        {
            return _parent.DistanceFromVertexMinorToMajorAxis * TrigonometryLibrary.Cos(angleRadians);
        }


        /// <summary>
        /// Primes the double by angle.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double angleRadians)
        {
            return -1 * _parent.DistanceFromVertexMinorToMajorAxis * TrigonometryLibrary.Sin(angleRadians);
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
        public EllipticalParametricY CloneParametric()
        {
            EllipticalParametricY parametric = new EllipticalParametricY(_parent as EllipticalCurve);
            return parametric;
        }
        #endregion
    }
}
