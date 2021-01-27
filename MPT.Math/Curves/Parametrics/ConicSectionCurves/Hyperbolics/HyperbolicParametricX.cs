// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="HyperbolicParametricX.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Trigonometry;


namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Hyperbolics
{
    /// <summary>
    /// Represents a hyperbolic curve in parametric equations defining the x-component and differentials.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicParametricDoubleComponents" />
    /// <a href="https://www.ck12.org/book/ck-12-calculus-concepts/section/10.3/">Reference</a>.
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicParametricDoubleComponents" />
    internal class HyperbolicParametricX : ConicParametricDoubleComponents
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HyperbolicParametricX"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public HyperbolicParametricX(HyperbolicCurve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// X-coordinate on the right curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// <a href="https://www.ck12.org/book/ck-12-calculus-concepts/section/10.3/">Reference</a>.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public override double BaseByParameter(double angleRadians)
        {
            return (_parent.DistanceFromVertexMajorToLocalOrigin * TrigonometryLibrary.CosH(angleRadians));
        }

        /// <summary>
        /// X-component of the curve slope in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double angleRadians)
        {
            return (_parent.DistanceFromVertexMajorToLocalOrigin * -1 * TrigonometryLibrary.Sin(angleRadians));
        }

        /// <summary>
        /// X-component of the curve curvature in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double angleRadians)
        {
            return (_parent.DistanceFromVertexMajorToLocalOrigin * -1 * TrigonometryLibrary.Cos(angleRadians));
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
        public HyperbolicParametricX CloneParametric()
        {
            HyperbolicParametricX parametric = new HyperbolicParametricX(_parent as HyperbolicCurve);
            return parametric;
        }
        #endregion
    }
}
