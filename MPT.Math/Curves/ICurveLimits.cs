// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="ICurveSingular.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.Curves.Tools;
using MPT.Math.Vectors;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Interface for a curve that is comprised of a path with some form of bounding limits.
    /// </summary>
    public interface ICurveLimits
    {
        /// <summary>
        /// The range of max/min limits that apply to the curve.
        /// </summary>
        /// <value>The range.</value>
        CurveRange Range { get; }


        /// <summary>
        /// Length of the curve.
        /// </summary>
        /// <returns>System.Double.</returns>
        double Length();

        /// <summary>
        /// Length of the curve between two points.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        double LengthBetween(double relativePositionStart, double relativePositionEnd);

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        double ChordLength();

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        double ChordLengthBetween(double relativePositionStart, double relativePositionEnd);

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        LinearCurve Chord();

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the linear curve is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the linear curve is ended.</param>
        /// <returns>LinearCurve.</returns>
        LinearCurve ChordBetween(double relativePositionStart, double relativePositionEnd);

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        Vector TangentVector(double relativePosition);

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        Vector NormalVector(double relativePosition);

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        CartesianCoordinate CoordinateCartesian(double relativePosition);

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        PolarCoordinate CoordinatePolar(double relativePosition);
    }
}
