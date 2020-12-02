// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-23-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-23-2020
// ***********************************************************************
// <copyright file="CurveLimit.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using System;

namespace MPT.Math.Curves.Tools
{
    /// <summary>
    /// Handles limits applied to curves.
    /// </summary>
    public class CurveLimit
    {
        #region Properties        
        /// <summary>
        /// The curve
        /// </summary>
        private Curve _curve;

        /// <summary>
        /// The limit on a cruve
        /// </summary>
        protected CartesianCoordinate _limit;
        /// <summary>
        /// The limit on a cruve.
        /// </summary>
        /// <value>The limit on a cruve.</value>
        public CartesianCoordinate Limit => _limit;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CurveLimit"/> class.
        /// </summary>
        /// <param name="curve">The curve.</param>
        public CurveLimit(Curve curve)
        {
            _curve = curve;
        }

        #region Methods        
        /// <summary>
        /// Sets the limit by the x-coordinate.
        /// </summary>
        /// <param name="xCoordinate">The x coordinate.</param>
        public void SetLimitByX(double xCoordinate)
        {
            _limit = GetLimitByX(xCoordinate, _curve);
        }

        /// <summary>
        /// Sets the limit by the y-coordinate.
        /// </summary>
        /// <param name="yCoordinate">The y coordinate.</param>
        public void SetLimitByY(double yCoordinate)
        {
            _limit = GetLimitByY(yCoordinate, _curve);
        }

        /// <summary>
        /// Sets the limit by rotation.
        /// </summary>
        /// <param name="angleRadians">The angle in radians.</param>
        public void SetLimitByRotation(double angleRadians)
        {
            _limit = GetLimitByRotation(angleRadians, _curve);
        }

        /// <summary>
        /// Sets the limit by coordinate, if the coordinate lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public void SetLimitByCoordinate(CartesianCoordinate coordinate)
        {
            _limit = GetLimitByCoordinate(coordinate, _curve);
        }

        /// <summary>
        /// The limit in polar coordinates.
        /// </summary>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate LimitPolar()
        {
            return Limit;
        }
        #endregion

        #region Static      
        /// <summary>
        /// Sets the limit by the x-coordinate.
        /// </summary>
        /// <param name="xCoordinate">The x coordinate.</param>
        /// <param name="curve">The curve.</param>
        public static CartesianCoordinate GetLimitByX(double xCoordinate, Curve curve)
        {
            // TODO: Throw exception if the coordinate does not exist
            double yCoordinate = curve.YatX(xCoordinate);
            return new CartesianCoordinate(xCoordinate, yCoordinate);
        }

        /// <summary>
        /// Sets the limit by the y-coordinate.
        /// </summary>
        /// <param name="yCoordinate">The y coordinate.</param>
        /// <param name="curve">The curve.</param>
        public static CartesianCoordinate GetLimitByY(double yCoordinate, Curve curve)
        {
            // TODO: Throw exception if the coordinate does not exist
            double xCoordinate = curve.XatY(yCoordinate);
            return new CartesianCoordinate(xCoordinate, yCoordinate);
        }

        /// <summary>
        /// Sets the limit by rotation.
        /// </summary>
        /// <param name="angleRadians">The angle in radians.</param>
        /// <param name="curve">The curve.</param>
        public static CartesianCoordinate GetLimitByRotation(double angleRadians, Curve curve)
        {
            double xCoordinate = curve.XbyRotationAboutOrigin(angleRadians);
            double yCoordinate = curve.YbyRotationAboutOrigin(angleRadians);
            return new CartesianCoordinate(xCoordinate, yCoordinate);
        }

        /// <summary>
        /// Sets the limit by coordinate, if the coordinate lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="curve">The curve.</param>
        public static CartesianCoordinate GetLimitByCoordinate(CartesianCoordinate coordinate, Curve curve)
        {
            if (!curve.IsIntersectingCoordinate(coordinate))
            {
                throw new ArgumentOutOfRangeException($"Coordinate {coordinate} cannot be used as a limit as it does not lie on the curve {curve} provided.");
            }
            return coordinate;
        }

        #endregion
    }
}
