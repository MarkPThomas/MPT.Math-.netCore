// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-23-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-23-2020
// ***********************************************************************
// <copyright file="CurveRange.cs" company="Mark P Thomas, Inc.">
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
    /// Handles limit ranges applied to curves.
    /// </summary>
    public class CurveRange
    {
        #region Properties
        /// <summary>
        /// The limit where the curve starts.
        /// </summary>
        protected CurveLimit _limitStart;
        /// <summary>
        /// The limit where the curve starts.
        /// </summary>
        /// <value>The limit start.</value>
        public CurveLimit Start => _limitStart;

        /// <summary>
        /// The limit where the curve ends.
        /// </summary>
        protected CurveLimit _limitEnd;
        /// <summary>
        /// The limit where the curve ends.
        /// </summary>
        /// <value>The limit end.</value>
        public CurveLimit End => _limitEnd;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CurveRange"/> class.
        /// </summary>
        /// <param name="curve">The curve.</param>
        public CurveRange(Curve curve)
        {
            _limitStart = new CurveLimit(curve);
            _limitEnd = new CurveLimit(curve);
        }

        #region Methods         
        /// <summary>
        /// Converts to cartesian offset.
        /// </summary>
        /// <returns>CartesianOffset.</returns>
        public CartesianOffset ToOffset()
        {
            return End.Limit.OffsetFrom(Start.Limit);
        }

        /// <summary>
        /// Converts to polar offset.
        /// </summary>
        /// <returns>PolarOffset.</returns>
        public PolarOffset ToOffsetPolar()
        {
            return End.Limit.OffsetFrom(Start.Limit);
        }

        /// <summary>
        /// The linear distance of the range.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double LengthLinear()
        {
            return Start.Limit.OffsetFrom(End.Limit).Length();
        }

        /// <summary>
        /// The x-axis distance of the range.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double LengthX()
        {
            return Start.Limit.OffsetFrom(End.Limit).X();
        }

        /// <summary>
        /// The y-axis distance of the range.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double LengthY()
        {
            return Start.Limit.OffsetFrom(End.Limit).Y();
        }

        /// <summary>
        /// The radial distance of the range.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double LengthRadius()
        {
            return ((PolarOffset)Start.Limit.OffsetFrom(End.Limit)).Radius();
        }

        /// <summary>
        /// The rotational distance of the range.
        /// </summary>
        /// <returns>System.Double.</returns>
        public Angle LengthRotation()
        {
            return ((PolarOffset)Start.Limit.OffsetFrom(End.Limit)).Azimuth();
        }

        /// <summary>
        /// The rotational distance of the range, in radians.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double LengthRotationRadians()
        {
            return LengthRotation().Radians;
        }

        /// <summary>
        /// The rotational distance of the range, in degrees.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double LengthRotationDegrees()
        {
            return LengthRotation().Degrees;
        }
        #endregion

        #region Static
        /// <summary>
        /// Validates the angular position provided based on +/- values of a half circle.
        /// </summary>
        /// <param name="position">The angular position, must be between -π and +π.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <exception cref="ArgumentOutOfRangeException">Relative position must be between -π and +π, but was {sRelative}.</exception>
        public static void ValidateRangeLimitRotationalHalfCirclePosition(double position, double tolerance = Numbers.ZeroTolerance)
        {
            if (!position.IsWithinInclusive(-1 * Numbers.Pi, Numbers.Pi, tolerance))
            {
                throw new ArgumentOutOfRangeException($"Position must be between -π and +π, but was {position}.");
            }
        }

        /// <summary>
        /// Validates the angular position provided based on + values of a full circle.
        /// </summary>
        /// <param name="position">The relative position, s. Relative position must be between 0 and +2π.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <exception cref="ArgumentOutOfRangeException">Relative position must be between 0 and +2π, but was {sRelative}.</exception>
        public static void ValidateRangeLimitRotationalFullCirclePosition(double position, double tolerance = Numbers.ZeroTolerance)
        {
            if (!position.IsWithinInclusive(0, Numbers.TwoPi, tolerance))
            {
                throw new ArgumentOutOfRangeException($"Position must be between 0 and +2π, but was {position}.");
            }
        }
        #endregion
    }
}
