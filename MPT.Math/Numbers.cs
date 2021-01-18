// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-20-2018
//
// Last Modified By : Mark P Thomas
// Last Modified On : 06-05-2020
// ***********************************************************************
// <copyright file="Numbers.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using NMath = System.Math;

// TODO: See if later implementing SIMD: https://msdn.microsoft.com/en-us/library/system.numerics(v=vs.111).aspx
// The SIMD-enabled vector types, which include Vector2, Vector3, Vector4, Matrix3x2, Matrix4x4, Plane, and Quaternion.

namespace MPT.Math
{
    /// <summary>
    /// Contains static methods dealing generically with numbers.
    /// </summary>
    public static class Numbers
    {
        #region Constants
        /// <summary>
        /// Default zero tolerance for operations.
        /// </summary>
        public const double ZeroTolerance = 1E-20;

        /// <summary>
        /// Represents the value of pi (180&#176;).
        /// </summary>
        public const double Pi = NMath.PI;

        /// <summary>
        /// Represents the value of pi times two (360&#176;).
        /// </summary>
        public const double TwoPi = 2 * NMath.PI;

        /// <summary>
        /// Represents the value of pi divided by two (90&#176;).
        /// </summary>
        public const double PiOver2 = NMath.PI / 2;

        /// <summary>
        /// Represents the value of pi divided by four (45&#176;).
        /// </summary>
        public const double PiOver4 = NMath.PI / 4;

        /// <summary>
        /// Represents the value of pi divided by six (30&#176;).
        /// </summary>
        public const double PiOver6 = NMath.PI / 6;

        /// <summary>
        /// Represents the value of pi divided by three (60&#176;).
        /// </summary>
        public const double PiOver3 = NMath.PI / 3;

        /// <summary>
        /// Represents the mathematical constant e.
        /// </summary>
        public const double E = NMath.E;

        /// <summary>
        /// Represents the log base ten of e.
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double Log10E() => NMath.Log10(NMath.E);

        /// <summary>
        /// Represents the log base two of e.
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double Log2E() => NMath.Log(NMath.E, 2);

        /// <summary>
        /// The golden ratio, also known as the divine proportion, golden mean, or golden section, is a number often encountered when taking the ratios of distances in simple geometric figures such as the pentagon, pentagram, decagon and dodecahedron.
        /// It is denoted phi and is approximately 1.618033988749...
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double GoldenRatio() => 0.5 * (1 + NMath.Sqrt(5));
        #endregion

        #region Signs
        /// <summary>
        /// Value is greater than the zero-tolerance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is positive sign] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsPositiveSign(double value, double tolerance = ZeroTolerance)
        {
            if (IsZeroSign(value, tolerance)) { return false; }
            return (value > NMath.Abs(tolerance));
        }

        /// <summary>
        /// Value is less than the zero-tolerance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is negative sign] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsNegativeSign(double value, double tolerance = ZeroTolerance)
        {
            if (IsZeroSign(value, tolerance)) { return false; }
            return (value < NMath.Abs(tolerance));
        }

        /// <summary>
        /// Value is within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is zero sign] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsZeroSign(double value, double tolerance = ZeroTolerance)
        {
            return (NMath.Abs(value) < NMath.Abs(tolerance));
        }

        /// <summary>
        /// Returns the sign of a value as either 1 (positive or 0), or -1 (negative).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Int32.</returns>
        public static int Sign(double value, double tolerance = ZeroTolerance)
        {
            if (IsZeroSign(value, tolerance)) { return 1; }
            return (value > -NMath.Abs(tolerance) ? 1 : -1);
        }
        #endregion

        #region Comparisons
        /// <summary>
        /// Value is equal to the provided value within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is equal to] [the specified value1]; otherwise, <c>false</c>.</returns>
        public static bool IsEqualTo(double value1, double value2, double tolerance = ZeroTolerance)
        {
            return AreEqual(value1, value2, tolerance);
        }

        /// <summary>
        /// Value is greater than the provided value, within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is greater than] [the specified value1]; otherwise, <c>false</c>.</returns>
        public static bool IsGreaterThan(double value1, double value2, double tolerance = ZeroTolerance)
        {
            return ((value1 - value2) > NMath.Abs(tolerance));
        }

        /// <summary>
        /// Value is less than the provided value, within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is less than] [the specified value1]; otherwise, <c>false</c>.</returns>
        public static bool IsLessThan(double value1, double value2, double tolerance = ZeroTolerance)
        {
            if (IsEqualTo(value1, value2, tolerance)) { return false; }
            return ((value1 - value2) < NMath.Abs(tolerance));
        }

        /// <summary>
        /// Values are equal within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreEqual(double value1, double value2, double tolerance = ZeroTolerance)
        {
            if (double.IsPositiveInfinity(value1) && double.IsPositiveInfinity(value2)) { return true; }
            if (double.IsNegativeInfinity(value1) && double.IsNegativeInfinity(value2)) { return true; }
            return (NMath.Abs(value1 - value2) < NMath.Abs(tolerance));
        }

        /// <summary>
        /// Determines whether [is greater than or equal to] [the specified value2].
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is greater than or equal to] [the specified value2]; otherwise, <c>false</c>.</returns>
        public static bool IsGreaterThanOrEqualTo(double value1, double value2, double tolerance = ZeroTolerance)
        {
            return (IsGreaterThan(value1, value2, tolerance) || IsEqualTo(value1, value2, tolerance));
        }

        /// <summary>
        /// Determines whether [is less than or equal to] [the specified value2].
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is less than or equal to] [the specified value2]; otherwise, <c>false</c>.</returns>
        public static bool IsLessThanOrEqualTo(double value1, double value2, double tolerance = ZeroTolerance)
        {
            return (IsLessThan(value1, value2, tolerance) || IsEqualTo(value1, value2, tolerance));
        }


        /// <summary>
        /// Determines whether the specified value is within the value bounds, including the values themselves.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="valueBound1">First value bound.</param>
        /// <param name="valueBound2">Second value bound.</param>
        /// <param name="tolerance">The tolerance used in comparing against the bounds.</param>
        /// <returns><c>true</c> if [is within inclusive] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsWithinInclusive(double value, double valueBound1, double valueBound2, double tolerance = ZeroTolerance)
        {
            double maxValue = NMath.Max(valueBound1, valueBound2);
            double minValue = NMath.Min(valueBound1, valueBound2);
            return IsLessThanOrEqualTo(minValue, value, tolerance) && IsGreaterThanOrEqualTo(maxValue, value, tolerance);
        }

        /// <summary>
        /// Determines whether the specified value is within the value bounds, not including the values bounds themselves.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="valueBound1">First value bound.</param>
        /// <param name="valueBound2">Second value bound.</param>
        /// <param name="tolerance">The tolerance used in comparing against the bounds.</param>
        /// <returns><c>true</c> if [is within inclusive] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsWithinExclusive(double value, double valueBound1, double valueBound2, double tolerance = ZeroTolerance)
        {
            double maxValue = NMath.Max(valueBound1, valueBound2);
            double minValue = NMath.Min(valueBound1, valueBound2);
            return IsLessThan(minValue, value, tolerance) && IsGreaterThan(maxValue, value, tolerance);
        }

        #endregion

        #region Properties
        /// <summary>
        /// Value is an odd number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is odd; otherwise, <c>false</c>.</returns>
        public static bool IsOdd(int value)
        {
            return (value % 2 != 0);
        }

        /// <summary>
        /// Value is an even number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is even; otherwise, <c>false</c>.</returns>
        public static bool IsEven(int value)
        {
            return !IsOdd(value);
        }

        /// <summary>
        /// A whole number greater than 1, whose only two whole-number factors are 1 and itself.
        /// Uses the 'Sieve of Eratosthenes', which is very efficient for solving small primes (i.e. &lt; 10,000,000,000).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is prime; otherwise, <c>false</c>.</returns>
        public static bool IsPrime(int value)
        {
            value = NMath.Abs(value);
            if (value == 0)
            {
                return false;
            }
            if (value == 1)
            {
                return false;
            }
            if (value == 2)
            {
                return true;
            }
            if (value == 3)
            {
                return true;
            }
            if (IsEven(value)) /* For all > 2*/
            {
                return false;
            }
            if (value == 5)
            {
                return true;
            }
            if (LastDigit(value) == 5) /* For all > 5*/
            {
                return false;
            }

            double limit = NMath.Sqrt(value);

            for (int i = 3; i <= limit; i += 2)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// A whole number that can be divided evenly by numbers other than 1 or itself.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is composite; otherwise, <c>false</c>.</returns>
        public static bool IsComposite(int value)
        {
            if (value == 0)
            {
                return false;
            }
            if (value == 1)
            {
                return false;
            }
            return !IsPrime(value);
        }

        /// <summary>
        /// Returns the last digit without sign of the value provided.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int LastDigit(int value)
        {
            return NMath.Abs((value % 10));
        }

        /// <summary>
        /// Number of decimal places.
        /// Note that for non-scientific notation, the maximum that may be returned is (# decimal places) &lt;= 15 - (# whole numbers, not including leading 0s).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="limitForRounding">If set to <c>true</c>, results are limited to a range that is appropriate for rounding methods (0 &lt;= value &lt;= 15).
        /// Not using this limit may result in a thrown exception when rounding.</param>
        /// <returns>System.Int32.</returns>
        public static int DecimalPlaces(double value, bool limitForRounding = true)
        {
            // Modified From: https://stackoverflow.com/questions/13477689/find-number-of-decimal-places-in-decimal-value-regardless-of-culture
            decimal argument = (decimal)value;
            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];
            //return decimalPlaces;
            return limitForRounding ? Limit(decimalPlaces, 0, 15) : decimalPlaces;
        }

        /// <summary>
        /// Number of significant figures.
        /// From: https://en.wikipedia.org/wiki/Significant_figures#Identifying_significant_figures
        /// </summary>
        /// <seealso cref="SignificantFigures">https://en.wikipedia.org/wiki/Significant_figures#Identifying_significant_figures</seealso>
        /// <param name="value">The value.</param>
        /// <param name="decimalDigitsTolerance">Tolerance limit to avoid spurious digits, in number of decimal places to include.
        /// Numbers beyond this are truncated.</param>
        /// <returns>System.Int32.</returns>
        public static int SignificantFigures(double value, int decimalDigitsTolerance = int.MaxValue)
        {
            string inputStr = value.ToString(CultureInfo.InvariantCulture);
            string[] numberPortions = inputStr.Split('.');
            string wholeNumber = numberPortions[0];

            // Remove leading zeros
            wholeNumber = wholeNumber.TrimStart('0');

            if (numberPortions.Length == 1 || decimalDigitsTolerance == 0)
            {   // Number has no decimal places. 
                // Remove trailing zeros as they are all to the left of the decimal point
                return wholeNumber.TrimEnd('0').Length;
            }

            string decimalNumber = numberPortions[1];
            if (decimalDigitsTolerance != int.MaxValue && 0 < decimalDigitsTolerance && decimalDigitsTolerance < decimalNumber.Length)
            {
                decimalNumber = decimalNumber.Remove(decimalDigitsTolerance);
            }

            if (wholeNumber.Length == 0)
            {   // Number has no whole numbers
                // Remove leading zeros as they are all to the left of the sig figs
                // Keep trailing zeros since they count as sig figs
                return decimalNumber.TrimStart('0').Length;
            }

            // Number contains whole numbers as well as decimals.
            // Leading zeros have been trimmed
            // Trailing zeros are in decimal portion
            // Remaining zeros are all between sig figs
            return wholeNumber.Length + decimalNumber.Length;
        }

        /// <summary>
        /// Determines the maximum of the parameters.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="ArgumentException">Argument cannot be null.</exception>
        /// <exception cref="ArgumentException">Array has not been dimensioned.</exception>
        public static double Max(params double[] items)
        {
            return Generics.Max(items);
        }

        /// <summary>
        /// Determines the minimum of the parameters.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="ArgumentException">Argument cannot be null.</exception>
        /// <exception cref="ArgumentException">Array has not been dimensioned.</exception>
        public static double Min(params double[] items)
        {
            return Generics.Min(items);
        }
        #endregion

        #region Powers 
        /// <summary>
        /// Returns the value squared..
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int Squared(int value)
        {
            return (value * value);
        }
        /// <summary>
        /// Returns the value squared.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double Squared(double value)
        {
            return (value * value);
        }

        /// <summary>
        /// Returns the value cubed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int Cubed(int value)
        {
            return (value * value * value);
        }
        /// <summary>
        /// Returns the value cubed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double Cubed(double value)
        {
            return (value * value * value);
        }

        /// <summary>
        /// Returns the value raised to the power provided.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="power">The power.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="DivideByZeroException"></exception>
        public static double Pow(double value, double power)
        {
            if (value == 0 && IsNegativeSign(power))
            {
                throw new DivideByZeroException($"{value}^{power} results in a division by zero, which is undefined.");
            }
            return NMath.Pow(value, power);
        }

        /// <summary>
        /// Returns the square root of the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double Sqrt(double value)
        {
            return NMath.Sqrt(value);
        }

        /// <summary>
        /// Returns the cube root of the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double CubeRoot(double value)
        {
            return Root(value, 3);
        }

        /// <summary>
        /// Returns the root of the number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="root">The root.</param>
        /// <returns>System.Double.</returns>
        public static double Root(double value, int root)
        {
            if (IsZeroSign(root))
            {
                throw new DivideByZeroException("Root cannot be zero.");
            }
            return IsOdd(root) ? NMath.Sign(value) * Pow(NMath.Abs(value), 1d / root) : Pow(value, 1d / root);
        }

        /// <summary>
        /// Returns the root of the number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="root">The root.</param>
        /// <returns>System.Double.</returns>
        public static double Root(double value, double root)
        {
            if (IsZeroSign(root))
            {
                throw new DivideByZeroException("Root cannot be zero.");
            }
            return Pow(value, 1d / root);
        }

        #endregion

        #region Other Modifications


        /// <summary>
        /// Sets value to zero if within absolute tolerance (exclusive), otherwise returns value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double ValueAsZeroIfWithinAbsoluteTolerance(double value, double tolerance = ZeroTolerance)
        {
            return IsZeroSign(value, tolerance) ? 0 : value;
        }

        /// <summary>
        /// The product of an integer and all the integers below it; e.g., factorial four ( 4! ) is equal to 24.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int Factorial(int value)
        {
            if (value == 0)
            {
                return 0;
            }
            int result = 1;
            for (int i = 1; i <= value; i++)
            {
                result *= i;
            }
            return result;
        }


        /// <summary>
        /// Returns the paired result of adding and subtracting the provided value from the base value.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="plusMinusValue">Value to add and subtract from the base value.</param>
        /// <returns>System.Int32[].</returns>
        public static int[] PlusMinus(int baseValue, int plusMinusValue)
        {
            return new[]
            {
                baseValue + plusMinusValue,
                baseValue - plusMinusValue
            };
        }
        /// <summary>
        /// Returns the paired result of adding and subtracting the provided value from the base value.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="plusMinusValue">Value to add and subtract from the base value.</param>
        /// <returns>System.Double[].</returns>
        public static double[] PlusMinus(double baseValue, double plusMinusValue)
        {
            return new[]
            {
                baseValue + plusMinusValue,
                baseValue - plusMinusValue
            };
        }


        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.
        /// If value is less than min, min will be returned.</param>
        /// <param name="max">The maximum value.
        /// If value is greater than max, max will be returned.</param>
        /// <returns>The clamped value.
        /// If value &gt; max, max will be returned.
        /// If value &lt; min, min will be returned.
        /// If min ≤ value ≥ max, value will be returned.</returns>
        /// <exception cref="ArgumentException">Max limit, {max}, is less than the min limit, {min}</exception>
        public static int Limit(int value, int min, int max)
        {
            if (max < min) throw new ArgumentException($"Max limit, {max}, is less than the min limit, {min}");
            if (value > max) { return max; }
            if (value < min) { return min; }
            return value;
        }
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.
        /// If value is less than min, min will be returned.</param>
        /// <param name="max">The maximum value.
        /// If value is greater than max, max will be returned.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>The clamped value.
        /// If value &gt; max, max will be returned.
        /// If value &lt; min, min will be returned.
        /// If min ≤ value ≥ max, value will be returned.</returns>
        /// <exception cref="ArgumentException">Max limit, {max}, is less than the min limit, {min}</exception>
        public static double Limit(double value, double min, double max, double tolerance = ZeroTolerance)
        {
            if (max < min) throw new ArgumentException($"Max limit, {max}, is less than the min limit, {min}");
            if (IsGreaterThan(value, max, tolerance)) { return max; }
            if (IsLessThan(value, min, tolerance)) { return min; }
            return value;
        }

        /// <summary>
        /// Rounds to significant figures.
        /// </summary>
        /// <seealso cref="RoundToSignificantFigures">https://en.wikipedia.org/wiki/Significant_figures</seealso>
        /// <param name="value">The value.</param>
        /// <param name="significantFigures">The number of significant figures.</param>
        /// <param name="roundingTieBreaker">Method by which rounding is performed if the triggering rounding number is 5.</param>
        /// <returns>System.Double.</returns>
        public static double RoundToSignificantFigures(
            double value, 
            int significantFigures, 
            RoundingTieBreaker roundingTieBreaker = RoundingTieBreaker.HalfAwayFromZero)
        {
            if (value == 0 || significantFigures == 0) { return 0; }

            // Get value scaled to having sig figs result as an integer e.g. 4th sig fig 12345 = 1234.5 or 0.00012345 = 1234.5, 7th sig fig 12.345 = 1234500
            double scale = NMath.Pow(10, NMath.Floor(NMath.Log10(NMath.Abs(value))) + 1 - significantFigures);
            double valueScale = value / scale;

            // Round in case floating point affects number outside of precision (e.g. for 0.545 vs. 0.5449999999 for rounding to 0.6)
            double valueScaleRounded = NMath.Round(valueScale, 2);

            // scale back up
            return scale * NMath.Round(valueScaleRounded, midpointRounding(roundingTieBreaker));
        }

        /// <summary>
        /// Rounds to decimal places.
        /// </summary>
        /// <seealso cref="RoundToDecimalPlaces">https://en.wikipedia.org/wiki/Significant_figures</seealso>
        /// <param name="value">The value.</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        /// <param name="roundingTieBreaker">Method by which rounding is performed if the triggering rounding number is 5.</param>
        /// <returns>System.Double.</returns>
        public static double RoundToDecimalPlaces(
            double value, 
            int decimalPlaces, 
            RoundingTieBreaker roundingTieBreaker = RoundingTieBreaker.HalfAwayFromZero)
        {
            return NMath.Round(value, decimalPlaces, midpointRounding(roundingTieBreaker));
        }

        /// <summary>
        /// Returns System.Math enum for the corresponding midpoint rounding method.
        /// </summary>
        /// <param name="roundingTieBreaker">The rounding tie breaker.</param>
        /// <returns>MidpointRounding.</returns>
        private static MidpointRounding midpointRounding(RoundingTieBreaker roundingTieBreaker)
        {
            return (roundingTieBreaker == RoundingTieBreaker.HalfAwayFromZero) ? MidpointRounding.AwayFromZero : MidpointRounding.ToEven;
        }
        #endregion

        #region Display
        /// <summary>
        /// Rounds to significant figures.
        /// </summary>
        /// <seealso cref="RoundToSignificantFigures">https://en.wikipedia.org/wiki/Significant_figures</seealso>
        /// <param name="value">The value.</param>
        /// <param name="significantFigures">The number of significant figures.</param>
        /// <param name="roundingTieBreaker">Method by which rounding is performed if the triggering rounding number is 5.</param>
        /// <returns>System.Double.</returns>
        public static string DisplayRoundedToSignificantFigures(
            double value, 
            int significantFigures, 
            RoundingTieBreaker roundingTieBreaker = RoundingTieBreaker.HalfAwayFromZero)
        {
            value = RoundToSignificantFigures(value, significantFigures, roundingTieBreaker);
            string valueAsString = value.ToString(CultureInfo.InvariantCulture);
            if (significantFigures <= 0)
            {
                return valueAsString;
            }

            // Get number of decimal places
            int prePaddingLength = valueAsString.Length;
            string wholeNumber;
            string decimalNumber = "";
            int wholeNumberLength;
            if (valueAsString.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator))
            {
                wholeNumber = valueAsString.Split(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)[0];
                decimalNumber = valueAsString.Split(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)[1];
                wholeNumberLength = (wholeNumber[0] == '-') ? wholeNumber.Length - 1 : wholeNumber.Length;
            }
            else
            {
                wholeNumber = valueAsString;
                wholeNumberLength = (wholeNumber[0] == '-') ? wholeNumber.Length - 1 : wholeNumber.Length;

                if (wholeNumber[0] == '0')
                {
                    prePaddingLength += 1;
                }
                else if(significantFigures <= wholeNumberLength)
                {
                    return valueAsString;
                }
            }
            int valueSigFigLength;
            char firstInteger = (wholeNumber[0] == '-') ? wholeNumber[1] : wholeNumber[0];
            if (valueAsString.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator) && firstInteger != '0')
            {
                valueSigFigLength = (wholeNumber[0] == '-') ? valueAsString.Length - 2 : valueAsString.Length - 1;
            }
            else
            {
                string sigFigAsString = valueAsString.TrimStart('-')
                                                     .TrimStart('0')
                                                     .Replace(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator, "")
                                                     .TrimStart('0');
                valueSigFigLength = sigFigAsString.Length;
            }
            string separator = ((significantFigures >= wholeNumberLength) || firstInteger == '0') ? CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator : "";

            string result = wholeNumber + separator + decimalNumber;
            int zeroPadding = significantFigures - valueSigFigLength;
            result = result.PadRight(prePaddingLength + zeroPadding, '0');
            return result;
        }

        /// <summary>
        /// Rounds to decimal places.
        /// </summary>
        /// <seealso cref="RoundToDecimalPlaces">https://en.wikipedia.org/wiki/Significant_figures</seealso>
        /// <param name="value">The value.</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        /// <param name="roundingTieBreaker">Method by which rounding is performed if the triggering rounding number is 5.</param>
        /// <returns>System.Double.</returns>
        public static string DisplayRoundedToDecimalPlaces(
            double value, 
            int decimalPlaces, 
            RoundingTieBreaker roundingTieBreaker = RoundingTieBreaker.HalfAwayFromZero)
        {
            value = RoundToDecimalPlaces(value, decimalPlaces, roundingTieBreaker);
            string valueAsString = value.ToString(CultureInfo.InvariantCulture);
            if (decimalPlaces <= 0)
            {
                return valueAsString;
            }

            // Get number of decimal places
            int prePaddingLength = valueAsString.Length;
            string wholeNumber;
            string decimalNumber = "";
            int valueDecimalLength = valueAsString.Length;
            if (valueAsString.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator))
            {
                wholeNumber = valueAsString.Split(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)[0];
                decimalNumber = valueAsString.Split(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)[1];
                valueDecimalLength -= (wholeNumber.Length + 1);
            }
            else
            {
                wholeNumber = valueAsString;
                valueDecimalLength = 0;

                if (wholeNumber[0] == '0')
                {
                    prePaddingLength += 1;
                }
            }

            string result = wholeNumber + CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator + decimalNumber;
            int zeroPadding = decimalPlaces - valueDecimalLength;
            result = result.PadRight(prePaddingLength + zeroPadding, '0');
            return result;
        }
        #endregion
    }
}

