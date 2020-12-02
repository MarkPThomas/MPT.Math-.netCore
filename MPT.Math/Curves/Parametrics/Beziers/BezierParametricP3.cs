// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="BezierParametricX3.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Math.Curves.Parametrics.Beziers
{
    /// <summary>
    /// Class BezierParametricX3.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.Beziers.BezierParametricBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.Beziers.BezierParametricBase" />
    internal class BezierParametricP3 : BezierParametricBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierParametricP3" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public BezierParametricP3(BezierCurve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// The component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override CartesianCoordinate BaseByParameter(double parameter)
        {
            return _parent.B_0() * (1 - parameter).Cubed() 
                + 3 * _parent.B_1() * parameter * (1 - parameter).Squared() 
                + 3 * _parent.B_2() * parameter.Squared() * (1 - parameter) 
                + _parent.B_3() * parameter.Cubed();
        }


        /// <summary>
        /// The component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override CartesianCoordinate PrimeByParameter(double parameter)
        {
            return -3 * _parent.B_0() * (1 - parameter).Squared()
                + 3 * _parent.B_1() * (1 - 4 * parameter + 3 * parameter.Squared())
                + 3 * _parent.B_2() * (2 * parameter - 3 * parameter.Squared())
                + 3 * _parent.B_3() * parameter.Squared();
        }


        /// <summary>
        /// The component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override CartesianCoordinate PrimeDoubleByParameter(double parameter)
        {
            return 6 * _parent.B_0() * (1 - parameter)
                + 3 * _parent.B_1() * (- 4 * parameter + 6 * parameter)
                + 3 * _parent.B_2() * (2 - 6 * parameter)
                + 6 * _parent.B_3() * parameter;
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
        public BezierParametricP3 CloneParametric()
        {
            BezierParametricP3 parametric = new BezierParametricP3(_parent as BezierCurve);
            return parametric;
        }
        #endregion
    }
}
