// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="PolarParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.ComponentsAngular;
using MPT.Math.Curves.Parametrics.ComponentsLinear;
using System;

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Class PolarParametric.
    /// Implements the <see cref="IParametricPolar{LinearParametricComponent, AngularParametricComponent}" />
    /// Implements the <see cref="System.ICloneable" />
    /// </summary>
    /// <seealso cref="IParametricPolar{LinearParametricComponent, AngularParametricComponent}" />
    /// <seealso cref="System.ICloneable" />
    public class PolarParametricEquation : IParametricPolar<LinearParametricComponent, AngularParametricComponent>, ICloneable
    {
        /// <summary>
        /// The differentiation index
        /// </summary>
        protected int _differentiationIndex = 0;

        /// <summary>
        /// The x
        /// </summary>
        internal LinearParametricComponentBase _radius;
        /// <summary>
        /// The x-component, at position s.
        /// </summary>
        /// <value>The xcomponent.</value>
        public LinearParametricComponent Radius => _radius[_differentiationIndex];

        /// <summary>
        /// The y
        /// </summary>
        internal AngularParametricComponentBase _azimuth;
        /// <summary>
        /// The y-component, at position s.
        /// </summary>
        /// <value>The ycomponent.</value>
        public AngularParametricComponent Azimuth => _azimuth[_differentiationIndex];

        /// <summary>
        /// Initializes a new instance of the <see cref="PolarParametricEquation" /> class.
        /// </summary>
        protected PolarParametricEquation()
        {

        }

        #region Methods: Public
        /// <summary>
        /// Returns the differential of the current parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public PolarParametricEquation Differentiate()
        {
            PolarParametricEquation differential = CloneParametric();
            differential._radius.Differentiate();
            differential._azimuth.Differentiate();
            _differentiationIndex++;
            return differential;
        }

        /// <summary>
        /// Returns the current parametric function, differentiated to the specified # of times.
        /// </summary>
        /// <param name="index">The index to differentiate to, which must be greater than 0.</param>
        /// <returns>HyperbolicParametric.</returns>
        public PolarParametricEquation DifferentiateBy(int index)
        {
            PolarParametricEquation differential = CloneParametric();
            differential._radius.DifferentiateBy(index);
            differential._azimuth.DifferentiateBy(index);
            _differentiationIndex = index;
            return differential;
        }

        /// <summary>
        /// Returns the first differential of the parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public PolarParametricEquation DifferentialFirst()
        {
            return DifferentiateBy(1);
        }

        /// <summary>
        /// Returns the second differential of the parametric function.
        /// </summary>
        /// <returns>HyperbolicParametric.</returns>
        public PolarParametricEquation DifferentialSecond()
        {
            return DifferentiateBy(2);
        }

        /// <summary>
        /// Determines whether this instance can be differentiated further.
        /// </summary>
        /// <returns><c>true</c> if this instance has differential; otherwise, <c>false</c>.</returns>
        public bool HasDifferential()
        {
            return (_radius.HasDifferential() || _azimuth.HasDifferential());
        }

        #endregion

        #region Operators
        ///// <summary>
        ///// Implements the + operator.
        ///// </summary>
        ///// <param name="a">a.</param>
        ///// <param name="b">The b.</param>
        ///// <returns>The result of the operator.</returns>
        //public static VectorParametric operator +(VectorParametric a, VectorParametric b)
        //{
        //    return new VectorParametric(a.Xcomponent + b.Xcomponent, a.Ycomponent + b.Ycomponent);
        //}

        ///// <summary>
        ///// Implements the - operator.
        ///// </summary>
        ///// <param name="a">a.</param>
        ///// <param name="b">The b.</param>
        ///// <returns>The result of the operator.</returns>
        //public static VectorParametric operator -(VectorParametric a, VectorParametric b)
        //{
        //    return new VectorParametric(a.Xcomponent - b.Xcomponent, a.Ycomponent - b.Ycomponent);
        //}


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarParametricEquation operator *(PolarParametricEquation a, double b)
        {
            PolarParametricEquation parametric = a.CloneParametric();
            parametric._radius *= b;
            parametric._azimuth *= b;
            return parametric;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarParametricEquation operator /(PolarParametricEquation a, double b)
        {
            PolarParametricEquation parametric = a.CloneParametric();
            parametric._radius /= b;
            parametric._azimuth /= b;
            return parametric;
        }

        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return CloneParametric();
        }

        /// <summary>
        /// Clones the curve.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public PolarParametricEquation CloneParametric()
        {
            PolarParametricEquation parametric = new PolarParametricEquation();
            parametric._radius.Clone();
            parametric._azimuth.Clone();
            parametric._differentiationIndex = _differentiationIndex;
            return parametric;
        }
        #endregion
    }
}
