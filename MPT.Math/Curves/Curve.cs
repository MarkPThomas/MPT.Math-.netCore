// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 07-06-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="Curve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics;
using MPT.Math.Vectors;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Class Curve.
    /// Implements the <see cref="MPT.Math.Curves.ICurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ICurve" />
    public abstract class Curve : ICurve, ICloneable
    {
        #region Properties        
        /// <summary>
        /// The tolerance.
        /// </summary>
        protected double _tolerance = 10E-6;
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public virtual double Tolerance 
        {
            get => _tolerance;
            set => _tolerance = value; 
        }

        /// <summary>
        /// The parametric vector that describes the curve.
        /// </summary>
        private VectorParametric _vectorParametric;
        /// <summary>
        /// The parametric vector that describes the curve.
        /// </summary>
        /// <value>The vector parametric.</value>
        protected VectorParametric vectorParametric
        {
            get
            {
                if (_vectorParametric == null)
                {
                    _vectorParametric = new VectorParametric(createParametricEquation());
                }
                return _vectorParametric;
            }
        }

        #endregion

        #region Curve Position
        /// <summary>
        /// X-coordinate on the right curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the local origin, in radians.</param>
        /// <returns>System.Double.</returns>
        public virtual double XbyRotationAboutOrigin(double angleRadians)
        {
            return vectorParametric.CurveParametric.Xcomponent.ValueAt(angleRadians);
        }

        /// <summary>
        /// Y-coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the local origin, in radians.</param>
        /// <returns>System.Double.</returns>
        public virtual double YbyRotationAboutOrigin(double angleRadians)
        {
            return vectorParametric.CurveParametric.Ycomponent.ValueAt(angleRadians);
        }
        #endregion

        #region Methods: Protected     
        /// <summary>
        /// The x-component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected virtual double xByParameter(double parameter)
        {
            return vectorParametric.CurveParametric.Xcomponent.ValueAt(parameter);
        }

        /// <summary>
        /// The y-component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected virtual double yByParameter(double parameter)
        {
            return vectorParametric.CurveParametric.Ycomponent.ValueAt(parameter);
        }

        /// <summary>
        /// The x-component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected virtual double xPrimeByParameter(double parameter)
        {
            return vectorParametric.CurveParametric.Xcomponent.Differential.ValueAt(parameter);
        }

        /// <summary>
        /// The y-component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected virtual double yPrimeByParameter(double parameter)
        {
            return vectorParametric.CurveParametric.Ycomponent.Differential.ValueAt(parameter);
        }

        /// <summary>
        /// The x-component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected virtual double xPrimeDoubleByParameter(double parameter)
        {
            return vectorParametric.CurveParametric.Xcomponent.Differential.Differential.ValueAt(parameter);
        }

        /// <summary>
        /// The y-component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>System.Double.</returns>
        protected virtual double yPrimeDoubleByParameter(double parameter)
        {
            return vectorParametric.CurveParametric.Ycomponent.Differential.Differential.ValueAt(parameter);
        }
        #endregion

        #region Abstract
        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected abstract LinearParametricEquation createParametricEquation();
        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public abstract object Clone();
        #endregion
    }
}
