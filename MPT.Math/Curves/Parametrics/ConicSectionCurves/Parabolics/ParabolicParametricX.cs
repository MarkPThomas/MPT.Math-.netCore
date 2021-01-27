// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ParabolicParametricX.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;


namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Parabolics
{
    /// <summary>
    /// Class ParabolicParametricX.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicLinearParametricBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ConicSectionCurves.ConicLinearParametricBase" />
    internal class ParabolicParametricX : ConicLinearParametricBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicParametricX"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ParabolicParametricX(ParabolicCurve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials

        /// <summary>
        /// X-coordinate on the curve in local coordinates that corresponds to the parametric coordinate given.
        /// <a href="http://amsi.org.au/ESA_Senior_Years/SeniorTopic2/2a/2a_2content_11.html">Reference</a>.
        /// </summary>
        /// <param name="t">Parametric coordinate.</param>
        /// <returns>System.Double.</returns>
        public override double BaseByParameter(double t)
        {
            return _parent.DistanceFromVertexMajorToLocalOrigin * t.Squared();
        }


        /// <summary>
        /// Primes the by angle.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeByParameter(double t)
        {
            return 2 * _parent.DistanceFromVertexMajorToLocalOrigin * t;
        }


        /// <summary>
        /// Primes the double by angle.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>System.Double.</returns>
        public override double PrimeDoubleByParameter(double t)
        {
            return 2 * _parent.DistanceFromVertexMajorToLocalOrigin;
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
