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
    public class CurveLimit : ICloneable
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

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CurveLimit"/> class.
        /// </summary>
        /// <param name="curve">The curve.</param>
        public CurveLimit(Curve curve)
        {
            _curve = curve;
            _limit = new CartesianCoordinate();
            _limit.Tolerance = curve.Tolerance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurveLimit"/> class.
        /// </summary>
        /// <param name="curve">The curve.</param>
        /// <param name="defaultLimit">The default limit.</param>
        internal CurveLimit(Curve curve, CartesianCoordinate defaultLimit)
        {
            _curve = curve;
            _limit = defaultLimit;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Sets the limit by the x-coordinate.
        /// </summary>
        /// <param name="xCoordinate">The x coordinate.</param>
        public void SetLimitByX(double xCoordinate)
        {
            if (!(_curve is ICurvePositionCartesian))
            {
                throw new NotSupportedException($"Curve {_curve} cannot be represented in cartesian coordinates.");
            }
            double tolerance = _limit.Tolerance;
            _limit = GetLimitByX(xCoordinate, _curve as ICurvePositionCartesian);
            _limit.Tolerance = tolerance;
        }

        /// <summary>
        /// Sets the limit by the y-coordinate.
        /// </summary>
        /// <param name="yCoordinate">The y coordinate.</param>
        public void SetLimitByY(double yCoordinate)
        {
            if (!(_curve is ICurvePositionCartesian))
            {
                throw new NotSupportedException($"Curve {_curve} cannot be represented in cartesian coordinates.");
            }
            double tolerance = _limit.Tolerance;
            _limit = GetLimitByY(yCoordinate, _curve as ICurvePositionCartesian);
            _limit.Tolerance = tolerance;
        }

        /// <summary>
        /// Sets the limit by rotation.
        /// </summary>
        /// <param name="angleRadians">The angle in radians.</param>
        public void SetLimitByRotation(double angleRadians)
        {   // Currently made to work with curves that have parametric equations in rotations. Need to flesh this out more. Parametric vector has polar transformation?
            if (!(_curve is ICurvePositionPolar))
            {
                throw new NotSupportedException($"Curve {_curve} cannot be represented in polar coordinates.");
            }
            double tolerance = _limit.Tolerance;
            _limit = GetLimitByRotation(angleRadians, _curve);
            _limit.Tolerance = tolerance;
        }

        /// <summary>
        /// Sets the limit by coordinate, if the coordinate lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public void SetLimitByCoordinate(CartesianCoordinate coordinate)
        {
            if (!(_curve is ICurvePositionCartesian))
            {
                throw new NotSupportedException($"Curve {_curve} cannot be represented in cartesian coordinates.");
            }
            double tolerance = _limit.Tolerance;
            _limit = GetLimitByCoordinate(coordinate, _curve as ICurvePositionCartesian);
            _limit.Tolerance = tolerance;
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
        /// Gets the limit by the x-coordinate.
        /// </summary>
        /// <param name="xCoordinate">The x coordinate.</param>
        /// <param name="curve">The curve.</param>
        public static CartesianCoordinate GetLimitByX(double xCoordinate, ICurvePositionCartesian curve) 
        {
            double yCoordinate = curve.YatX(xCoordinate);
            return new CartesianCoordinate(xCoordinate, yCoordinate);
        }

        /// <summary>
        /// Gets the limit by the y-coordinate.
        /// </summary>
        /// <param name="yCoordinate">The y coordinate.</param>
        /// <param name="curve">The curve.</param>
        public static CartesianCoordinate GetLimitByY(double yCoordinate, ICurvePositionCartesian curve)
        {
            double xCoordinate = curve.XatY(yCoordinate);
            return new CartesianCoordinate(xCoordinate, yCoordinate);
        }

        /// <summary>
        /// Gets the limit by rotation.
        /// </summary>
        /// <param name="angleRadians">The angle in radians.</param>
        /// <param name="curve">The curve.</param>
        public static CartesianCoordinate GetLimitByRotation(double angleRadians, Curve curve)
        {   // Currently made to work with curves that have parametric equations in rotations. Need to flesh this out more. Parametric vector has polar transformation?
            double xCoordinate = curve.XbyRotationAboutOrigin(angleRadians);
            double yCoordinate = curve.YbyRotationAboutOrigin(angleRadians);
            return new CartesianCoordinate(xCoordinate, yCoordinate);
        }

        /// <summary>
        /// Gets the limit by coordinate, if the coordinate lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="curve">The curve.</param>
        public static CartesianCoordinate GetLimitByCoordinate(CartesianCoordinate coordinate, ICurvePositionCartesian curve)
        {
            if (!curve.IsIntersectingCoordinate(coordinate))
            {
                throw new ArgumentOutOfRangeException(
                    $"Coordinate {coordinate} cannot be used as a limit as it does not lie on the curve {curve} provided."
                    );
            }
            return coordinate;
        }
        #endregion


        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return CloneLimit();
        }

        /// <summary>
        /// Clones the limit.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public CurveLimit CloneLimit()
        {
            CurveLimit curveLimit = new CurveLimit(_curve);
            curveLimit._limit = _limit;
            return curveLimit;
        }
        #endregion
    }
}
