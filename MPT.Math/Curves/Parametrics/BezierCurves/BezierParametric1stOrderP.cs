// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="BezierParametricX1.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.BezierCurves
{
    /// <summary>
    /// Represents a 1st-order Bezier curve in parametric equations defining the <see cref="CartesianCoordinate"/> component and differentials.
    /// This class tends to be used as a base from which the x- and y-components are derived.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.BezierCurves.BezierParametricCartesianComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.BezierCurves.BezierParametricCartesianComponents" />
    internal class BezierParametric1stOrderP : BezierParametricCartesianComponents
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierParametric1stOrderP" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public BezierParametric1stOrderP(BezierCurve parent) : base(parent)
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
            return _parent.B_0() * (1 - parameter) 
                    + _parent.B_3() * parameter;
        }


        /// <summary>
        /// The component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override CartesianCoordinate PrimeByParameter(double parameter)
        {
            return -1 * _parent.B_0() + _parent.B_3();
        }


        /// <summary>
        /// The component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override CartesianCoordinate PrimeDoubleByParameter(double parameter)
        {
            return CartesianCoordinate.Origin();
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
        public BezierParametric1stOrderP CloneParametric()
        {
            BezierParametric1stOrderP parametric = new BezierParametric1stOrderP(_parent as BezierCurve);
            return parametric;
        }
        #endregion
    }
}
