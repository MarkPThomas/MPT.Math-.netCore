// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-18-2020
// ***********************************************************************
// <copyright file="ParametricEquation.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Delegate that is used for calling a function that returns a value based on the provided parametric position.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="s">The parametric position, s.</param>
    /// <returns>T.</returns>
    public delegate T ValueAtPosition<T>(double s);

    /// <summary>
    /// Base class for any type of parametric equation that returns a value of the defined type.
    /// Implements the <see cref="IParametricEquation{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IParametricEquation{T}" />
    public abstract class ParametricEquation<T> : IParametricEquation<T>
    {
        /// <summary>
        /// The constant value that is returned, regardless of the parametric position.
        /// </summary>
        protected T _constantValue;

        /// <summary>
        /// The function that returns a value based on the provided parametric position.
        /// </summary>
        protected ValueAtPosition<T> _valueAtPosition;

        /// <summary>
        /// The differential of the parametric equation
        /// </summary>
        protected ParametricEquation<T> _differential;
        /// <summary>
        /// Gets the differential of the parametric equation.
        /// </summary>
        /// <value>The differential.</value>
        public ParametricEquation<T> Differential => _differential;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricEquation{T}"/> class that returns the supplied constant value.
        /// </summary>
        /// <param name="constantValue">The constant value that is returned, regardless of the parametric position.</param>
        public ParametricEquation(T constantValue)
        {
            _constantValue = constantValue;
            _valueAtPosition = ConstantValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricEquation{T}"/> class that returns the result of the supplied function.
        /// </summary>
        /// <param name="function">The function that returns a value based on the provided parametric position.</param>
        public ParametricEquation(ValueAtPosition<T> function)
        {
            _valueAtPosition = function;
        }

        /// <summary>
        /// The value, at parametric position s.
        /// </summary>
        /// <param name="s">The parametric position, s.</param>
        /// <returns>System.Double.</returns>
        /// <value>The radial length.</value>
        public T ValueAt(double s) { return _valueAtPosition(s); }

        /// <summary>
        /// Determines whether this instance has a differential equation assigned.
        /// </summary>
        /// <returns><c>true</c> if this instance has differential; otherwise, <c>false</c>.</returns>
        public bool HasDifferential()
        {
            return _differential != null;
        }

        /// <summary>
        /// The constant value, at parametric position s, that is the same for all positions.
        /// </summary>
        /// <param name="s">The parametric position, s.</param>
        /// <returns>T.</returns>
        protected T ConstantValue(double s) { return _constantValue; }
    }
}
