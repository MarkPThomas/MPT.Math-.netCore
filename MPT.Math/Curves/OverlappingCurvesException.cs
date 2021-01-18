// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 01-17-2021
//
// Last Modified By : Mark P Thomas
// Last Modified On : 01-17-2021
// ***********************************************************************
// <copyright file="OverlappingCurvesException.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Class OverlappingCurvesException.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class OverlappingCurvesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OverlappingCurvesException"/> class.
        /// </summary>
        public OverlappingCurvesException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OverlappingCurvesException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OverlappingCurvesException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OverlappingCurvesException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public OverlappingCurvesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
