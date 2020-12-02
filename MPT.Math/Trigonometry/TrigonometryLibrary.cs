// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-23-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 05-23-2020
// ***********************************************************************
// <copyright file="TrigonometryLibrary.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;

namespace MPT.Math.Trigonometry
{
    /// <summary>
    /// Library of trigonometric calculations.
    /// </summary>
    public static class TrigonometryLibrary
    {

        // TODO: Work out if better to call System.Math often with this library, or just encapsulate methods.
        #region Functions from System.Math
        #region Sin/Cos/Tan & Arc variations
        /// <summary>
        /// Returns the sine of the specified angle.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>System.Double.</returns>
        public static double Sin(double radians)
        {
            return NMath.Sin(radians);
        }


        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>System.Double.</returns>
        public static double Cos(double radians)
        {
            return NMath.Cos(radians);
        }


        /// <summary>
        /// Returns the tangent of the specified angle.
        /// Returns <see cref="double.NegativeInfinity" /> if angle is a multiple of -π/2 or -(3/2)*π.
        /// Returns <see cref="double.PositiveInfinity" /> if angle is a multiple of +π/2 or +(3/2)*π.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double Tan(double radians, double tolerance = Numbers.ZeroTolerance)
        {
            if (radians.IsEqualTo(Numbers.PiOver2, tolerance) ||
                radians.IsEqualTo(3 * Numbers.PiOver2, tolerance)) { return double.PositiveInfinity; } // 90 deg

            if (radians.IsEqualTo(-Numbers.PiOver2, tolerance) ||
                radians.IsEqualTo(-3 * Numbers.PiOver2, tolerance)) { return double.NegativeInfinity; } // -90 deg

            return NMath.Tan(radians);
        }

        /// <summary>
        /// Returns the angle whose sine is the specified ratio.
        /// </summary>
        /// <param name="ratio">The ratio of 'opposite / hypotenuse'.</param>
        /// <returns>System.Double.</returns>
        public static double ArcSin(double ratio)
        {
            return NMath.Asin(ratio);
        }

        /// <summary>
        /// Returns the angle whose cosine is the specified ratio.
        /// </summary>
        /// <param name="ratio">The ratio of 'adjacent / hypotenuse'.</param>
        /// <returns>System.Double.</returns>
        public static double ArcCos(double ratio)
        {
            return NMath.Acos(ratio);
        }

        /// <summary>
        /// Returns the angle whose tangent is the specified ratio.
        /// </summary>
        /// <param name="ratio">The ratio of 'opposite / adjacent'.</param>
        /// <returns>System.Double.</returns>
        public static double ArcTan(double ratio)
        {
            return NMath.Atan(ratio);
        }
        #endregion
        #region Hyperbolics
        // https://en.wikipedia.org/wiki/Hyperbolic_functions

        /// <summary>
        /// Returns the hyperbolic sine of the specified angle.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>System.Double.</returns>
        public static double SinH(double radians)
        {
            return NMath.Sinh(radians);
        }

        /// <summary>
        /// Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>System.Double.</returns>
        public static double CosH(double radians)
        {
            return NMath.Cosh(radians);
        }

        /// <summary>
        /// Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>System.Double.</returns>
        public static double TanH(double radians)
        {
            return NMath.Tanh(radians);
        }
        #endregion
        #endregion

        #region Angle/Ratio Methods (Sec, Csc, Cot, etc.)
        /// <summary>
        /// Returns the secant of the specified angle.
        /// Returns <see cref="double.NegativeInfinity" /> if angle is a multiple of +/- π/2.
        /// Returns <see cref="double.PositiveInfinity" /> if 0 or angle is a multiple of +/- (3/2)*π.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double Sec(double radians, double tolerance = Numbers.ZeroTolerance)
        {
            if(NMath.Abs(radians).IsEqualTo(Numbers.PiOver2, tolerance)) { return double.PositiveInfinity; }
            if (NMath.Abs(radians).IsEqualTo(3 * Numbers.PiOver2, tolerance)) { return double.NegativeInfinity; }

            return 1d / NMath.Cos(radians);
        }

        /// <summary>
        /// Returns the cosecant of the specified angle.
        /// Returns <see cref="double.NegativeInfinity" /> if angle is a multiple of -π or +2π.
        /// Returns <see cref="double.PositiveInfinity" /> if 0 or angle is a multiple of +π or -2π.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double Csc(double radians, double tolerance = Numbers.ZeroTolerance)
        {
            if (NMath.Abs(radians).IsZeroSign()) { return double.PositiveInfinity; }
            if (radians.IsEqualTo(Numbers.Pi, tolerance)) { return double.PositiveInfinity; }       // 180 deg
            if (radians.IsEqualTo(Numbers.TwoPi, tolerance)) { return double.NegativeInfinity; }    // 360 deg
            if (radians.IsEqualTo(-Numbers.Pi, tolerance)) { return double.NegativeInfinity; }      // -180 deg
            if (radians.IsEqualTo(-Numbers.TwoPi, tolerance)) { return double.PositiveInfinity; }   // -360 deg

            return 1d / NMath.Sin(radians);
        }

        /// <summary>
        /// Returns the cotangent of the specified angle.
        /// Returns <see cref="double.NegativeInfinity" /> if angle is a multiple of +π or +2π.
        /// Returns <see cref="double.PositiveInfinity" /> if 0 or angle is a multiple of -π or -2π.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double Cot(double radians, double tolerance = Numbers.ZeroTolerance)
        {
            if (NMath.Abs(radians).IsZeroSign()) { return double.PositiveInfinity; }
            if (radians.IsEqualTo(Numbers.Pi, tolerance)) { return double.NegativeInfinity; }       // 180 deg
            if (radians.IsEqualTo(Numbers.TwoPi, tolerance)) { return double.NegativeInfinity; }    // 360 deg
            if (radians.IsEqualTo(-Numbers.Pi, tolerance)) { return double.PositiveInfinity; }      // -180 deg
            if (radians.IsEqualTo(-Numbers.TwoPi, tolerance)) { return double.PositiveInfinity; }   // -360 deg

            return 1d / NMath.Tan(radians);
        }
        #endregion
    }
}
