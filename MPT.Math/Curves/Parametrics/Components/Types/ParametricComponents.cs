// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ParametricComponents.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace MPT.Math.Curves.Parametrics.Components.Types
{
    /// <summary>
    /// Class containing the component placeholders of a parametric equation of a given data type (e.g. number, angle, etc.) and the corresponding curve object.
    /// This class has the basic implementation of differentiating and accessing the different parametric equations.
    /// Also contains abstract functions to implement scaling for a specific set of parametric equations.
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// <typeparam name="T2">The type of the t2.</typeparam>
    /// <typeparam name="T3">The type of the t3.</typeparam>
    internal abstract class ParametricComponents<T1, T2, T3> 
        where T2: ParametricEquation<T1>
        where T3: Curve
    {
        /// <summary>
        /// Gets the parent object whose properties are used in the associated parametric equations.
        /// </summary>
        /// <value>The parent.</value>
        protected virtual T3 _parentCurve { get; }

        /// <summary>
        /// The function base.
        /// </summary>
        protected T2 _base;
        /// <summary>
        /// The first derivative of the function.
        /// </summary>
        protected T2 _prime;
        /// <summary>
        /// The second derivative of the function.
        /// </summary>
        protected T2 _doublePrime;

        /// <summary>
        /// The components
        /// </summary>
        protected List<T2> _components = new List<T2>();

        // Indexer declaration
        /// <summary>
        /// Gets the parametric component at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>T2.</returns>
        public T2 this[int index]
        {
            get { return _components[index]; }
        }

        /// <summary>
        /// The differentiation index
        /// </summary>
        internal int _differentiationIndex = 0;

        /// <summary>
        /// The scale
        /// </summary>
        internal double _scale = 1;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="parent">The parent.</param>
        protected ParametricComponents(T3 parent)
        {
            _parentCurve = parent;
        }

        protected void initializeComponentList()
        {
            _components.Add(_base);
            _components.Add(_prime);
            _components.Add(_doublePrime);
        }

        #region Methods: Public
        /// <summary>
        /// Returns the differential of the current parametric function.
        /// </summary>
        /// <param name="index">The index to differentiate to, which must be greater than 0.</param>
        /// <returns>HyperbolicParametric.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index must not be greater than {_components.Count - 1} in order to differentiate.</exception>
        public ParametricComponents<T1, T2, T3> Differentiate(int index)
        {
            index++;
            if (_components.Count <= index)
            {
                throw new ArgumentOutOfRangeException($"Index: {index} must not be greater than {_components.Count - 1} in order to differentiate.");
            }
            ParametricComponents<T1, T2, T3> differential = Clone() as ParametricComponents<T1, T2, T3>;
            differential._differentiationIndex = index;
            return differential;
        }

        /// <summary>
        /// Returns the current parametric function, differentiated to the specified # of times.
        /// </summary>
        /// <param name="index">The index to differentiate to, which must be greater than 0.</param>
        /// <returns>HyperbolicParametric.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index: {index} must not be less than 0.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Index: {index} must not be greater than {_components.Count - 1} in order to differentiate.</exception>
        public ParametricComponents<T1, T2, T3> DifferentiateBy(int index)
        {
            if (index < 0) { 
                throw new ArgumentOutOfRangeException($"Index: {index} must not be less than 0."); 
            }
            if (index < _differentiationIndex)
            {
                throw new ArgumentOutOfRangeException($"Differentiation index {index} cannot be less than than current index {_differentiationIndex}.");
            }
            if (_components.Count <= index)
            {
                throw new ArgumentOutOfRangeException($"Index: {index} must not be greater than {_components.Count - 1} in order to differentiate.");
            }

            ParametricComponents<T1, T2, T3> differential = Clone() as ParametricComponents<T1, T2, T3>;
            differential._differentiationIndex = index;
            return differential;
        }

        // TODO: Might not be used. Consider removing.
        ///// <summary>
        ///// Returns the first differential of the parametric function.
        ///// </summary>
        ///// <returns>HyperbolicParametric.</returns>
        //public ParametricComponents<T1, T2, T3> DifferentialFirst()
        //{
        //    return DifferentiateBy(1);
        //}

        ///// <summary>
        ///// Returns the second differential of the parametric function.
        ///// </summary>
        ///// <returns>HyperbolicParametric.</returns>
        //public ParametricComponents<T1, T2, T3> DifferentialSecond()
        //{
        //    return DifferentiateBy(2);
        //}

        /// <summary>
        /// Determines whether this instance can be differentiated further.
        /// </summary>
        /// <returns><c>true</c> if this instance has differential; otherwise, <c>false</c>.</returns>
        public bool HasDifferential()
        {
            return _differentiationIndex < _components.Count - 1;
        }
        #endregion

        #region Methods: Parametric Equations and Differentials
        /// <summary>
        /// The scaled component as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected abstract T1 baseByParameterScaled(double parameter);

        /// <summary>
        /// The component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        public abstract T1 BaseByParameter(double parameter);


        /// <summary>
        /// The scaled component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected abstract T1 primeByParameterScaled(double parameter);

        /// <summary>
        /// The component first differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        public abstract T1 PrimeByParameter(double parameter);


        /// <summary>
        /// The scaled component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        protected abstract T1 primeDoubleByParameterScaled(double parameter);
        /// <summary>
        /// The component second differentical as a function of the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter, such as relative position between 0 &amp; 1, or the angle in radians.</param>
        /// <returns>T1.</returns>
        public abstract T1 PrimeDoubleByParameter(double parameter);
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
