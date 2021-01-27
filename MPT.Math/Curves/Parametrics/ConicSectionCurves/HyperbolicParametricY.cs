// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 01-27-2021
// ***********************************************************************
// <copyright file="HyperbolicParametricY.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Trigonometry;


namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Represents a hyperbolic curve in parametric equations defining the y-component and differentials.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicParametricDoubleComponents" />
    /// <a href="https://www.ck12.org/book/ck-12-calculus-concepts/section/10.3/">Reference</a>.
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicParametricDoubleComponents" />
    internal class HyperbolicParametricY : ConicParametricDoubleComponents
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HyperbolicParametricY"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public HyperbolicParametricY(HyperbolicCurve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// Y-coordinate on the right curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// <a href="https://www.ck12.org/book/ck-12-calculus-concepts/section/10.3/">Reference</a>.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public override double BaseByParameter(double angleRadians)
        {
            return (_parent.DistanceFromVertexMinorToMajorAxis * TrigonometryLibrary.SinH(angleRadians));
        }

        /// <summary>
        /// Y-component of the curve slope in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double angleRadians)
        {
            return (_parent.DistanceFromVertexMinorToMajorAxis * TrigonometryLibrary.Cos(angleRadians));
        }

        /// <summary>
        /// Y-component of the curve curvature in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double angleRadians)
        {
            return (_parent.DistanceFromVertexMinorToMajorAxis * -1 * TrigonometryLibrary.Sin(angleRadians));
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
        public HyperbolicParametricY CloneParametric()
        {
            HyperbolicParametricY parametric = new HyperbolicParametricY(_parent as HyperbolicCurve);
            return parametric;
        }
        #endregion
    }
}
