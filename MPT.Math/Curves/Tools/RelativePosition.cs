// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-23-2020
// ***********************************************************************
// <copyright file="RelativePosition.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using System;

namespace MPT.Math.Curves.Tools
{
    /// <summary>
    /// Returns coordinates on a curve for the relative position provided.
    /// </summary>
    public static class RelativePosition
    {
        #region Methods 
        /// <summary>
        /// Validates the relative position provided.
        /// </summary>
        /// <param name="sRelative">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <param name="tolerance">The tolerance used for relative comparisons of floating point numbers.</param>
        /// <exception cref="ArgumentOutOfRangeException">Relative position must be between 0 and 1, but was {sRelative}.</exception>
        public static void ValidateRangeLimitRelativePosition(double sRelative, double tolerance = Numbers.ZeroTolerance)
        {
            if (!sRelative.IsWithinInclusive(0, 1, tolerance))
            {
                throw new ArgumentOutOfRangeException($"Relative position must be between 0 and 1, but was {sRelative}.");
            }
        }

        /// <summary>
        /// Returns a coordinate that is a linear interpolation in cartesian coordinates within the range provided.
        /// </summary>
        /// <param name="sRelative">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <param name="range">The range.</param>
        /// <param name="tolerance">The tolerance used for relative comparisons of floating point numbers.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate CoordinateInterpolated(double sRelative, CurveRange range, double tolerance = Numbers.ZeroTolerance)
        {
            ValidateRangeLimitRelativePosition(sRelative, tolerance);
            return (sRelative * range.ToOffset()).ToCartesianCoordinate();
        }

        /// <summary>
        /// Returns a coordinate that is a linear interpolation in polar coordinates within the range provided.
        /// </summary>
        /// <param name="sRelative">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <param name="range">The range.</param>
        /// <param name="tolerance">The tolerance used for relative comparisons of floating point numbers.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate CoordinateInterpolatedPolar(double sRelative, CurveRange range, double tolerance = Numbers.ZeroTolerance)
        {
            ValidateRangeLimitRelativePosition(sRelative, tolerance);
            return (sRelative * range.ToOffset()).ToCartesianCoordinate();
        }

        ///// <summary>
        ///// Returns a coordinate that is a polar interpolation within the range provided for +/- half circles.
        ///// </summary>
        ///// <param name="sRelative">The relative position, s. Relative position must be between -π and +π.</param>
        ///// <param name="range">The range.</param>
        ///// <returns>CartesianCoordinate.</returns>
        //public static CartesianCoordinate CoordinateInterpolatedPolarHalfCirclePosition(double sRelative, CurveRange range)
        //{
        //    return (sRelative * range.ToOffsetPolar()).ToPolarCoordinate();
        //}

        ///// <summary>
        ///// Returns a coordinate that is a polar interpolation within the range provided for full circle.
        ///// </summary>
        ///// <param name="sRelative">The relative position, s. Relative position must be between 0 and +2π.</param>
        ///// <param name="range">The range.</param>
        ///// <returns>CartesianCoordinate.</returns>
        //public static CartesianCoordinate CoordinateInterpolatedPolarFullCirclePosition(double sRelative, CurveRange range)
        //{
        //    return (sRelative * range.ToOffsetPolar()).ToPolarCoordinate();
        //}

        /// <summary>
        /// Returns a coordinate that is at the relative position on the curve provided within ranges specified for the curve.
        /// </summary>
        /// <param name="sRelative">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="tolerance">The tolerance used for relative comparisons of floating point numbers.</param>
        /// <returns>CartesianCoordinate.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static CartesianCoordinate Coordinate(double sRelative, Curve curve, double tolerance = Numbers.ZeroTolerance)
        {
            ValidateRangeLimitRelativePosition(sRelative, tolerance);
            throw new NotImplementedException();
        }
        #endregion
    }
}
