// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="LinearParametricP.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.LinearCurveComponents
{
    /// <summary>
    /// Represents a linear curve in parametric equations defining the <see cref="CartesianCoordinate"/> component and differentials.
    /// This class tends to be used as a base from which the x- and y-components are derived.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearCurveComponents.LinearParametricCartesianComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearCurveComponents.LinearParametricCartesianComponents" />
    internal class LinearParametricP : LinearParametricCartesianComponents
    {
        /// <summary>
        /// The offset between points defining the linear curve.
        /// </summary>
        private CartesianOffset _offset;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearParametricP" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LinearParametricP(LinearCurve parent) : base(parent)
        {
            _offset = parent.Range.End.Limit.OffsetFrom(parent.Range.Start.Limit);
        }

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// The component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override CartesianCoordinate BaseByParameter(double parameter)
        {
            return _parent.Range.Start.Limit + _offset * parameter;
        }


        /// <summary>
        /// The component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        public override CartesianCoordinate PrimeByParameter(double parameter)
        {
            return _offset.ToCartesianCoordinate();
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

        //ncrunch: no coverage start
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {  // This is placed here due to LinearParametricP not needing a cloning method in usage or access, but derives from base classes that require it.
            return null;
        }
        //ncrunch: no coverage end 
    }
}
