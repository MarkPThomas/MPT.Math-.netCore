﻿using System;

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
        /// Represents the value of pi.
        /// </summary>
        public const double Pi = NMath.PI;

        /// <summary>
        /// Represents the value of pi times two.
        /// </summary>
        public const double TwoPi = 2 * NMath.PI;

        /// <summary>
        /// Represents the value of pi divided by two.
        /// </summary>
        public const double PiOver2 = NMath.PI / 2;

        /// <summary>
        /// Represents the value of pi divided by four.
        /// </summary>
        public const double PiOver4 = NMath.PI / 4;

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
        /// <returns></returns>
        public static double GoldenRatio() => 0.5 * (1 + NMath.Sqrt(5)); 
        #endregion

        #region Signs
        /// <summary>
        /// Value is greater than the zero-tolerance.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsPositive(double value, double tolerance = ZeroTolerance)
        {
            if (IsZero(value, tolerance)) { return false; }
            return (value > NMath.Abs(tolerance));
        }

        /// <summary>
        /// Value is less than the zero-tolerance.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsNegative(double value, double tolerance = ZeroTolerance)
        {
            if (IsZero(value, tolerance)) { return false; }
            return (value < NMath.Abs(tolerance));
        }

        /// <summary>
        /// Value is within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsZero(double value, double tolerance = ZeroTolerance)
        {
            return (NMath.Abs(value) < NMath.Abs(tolerance));
        }
        #endregion

        #region Comparisons
        /// <summary>
        /// Value is equal to the provided value within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsEqualTo(double value1, double value2, double tolerance = ZeroTolerance)
        {
            return AreEqual(value1, value2, tolerance);
        }

        /// <summary>
        /// Value is greater than the provided value, within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsGreaterThan(double value1, double value2, double tolerance = ZeroTolerance)
        {
            return ((value1 - value2) > NMath.Abs(tolerance));
        }

        /// <summary>
        /// Value is less than the provided value, within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsLessThan(double value1, double value2, double tolerance = ZeroTolerance)
        {
            if (IsEqualTo(value1, value2, tolerance)) { return false; }
            return ((value1 - value2) < NMath.Abs(tolerance));
        }

        /// <summary>
        /// Values are equal within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
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
        #endregion

        #region Properties
        /// <summary>
        /// Value is an odd number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsOdd(int value)
        {
            return (value % 2 != 0);
        }

        /// <summary>
        /// Value is an even number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEven(int value)
        {
            return !IsOdd(value);
        }

        /// <summary>
        /// A whole number greater than 1, whose only two whole-number factors are 1 and itself. 
        /// Uses the 'Sieve of Eratosthenes', which is very efficient for solving small primes (i.e. &lt; 10,000,000,000).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// <param name="value"></param>
        /// <returns></returns>
        public static int LastDigit(int value)
        {
            return NMath.Abs((value % 10));
        }
        #endregion

        #region Powers 
        /// <summary>
        /// Returns the value squared..
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Squared(int value)
        {
            return (value * value);
        }
        /// <summary>
        /// Returns the value squared.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Squared(double value)
        {
            return (value * value);
        }

        /// <summary>
        /// Returns the value cubed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Cubed(int value)
        {
            return (value * value * value);
        }
        /// <summary>
        /// Returns the value cubed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Cubed(double value)
        {
            return (value * value * value);
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
            return Pow(value, 1d / root);
        }

        /// <summary>
        /// Returns the value raised to the power provided.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static double Pow(double value, double power)
        {
            if (value == 0 && IsNegative(power))
            {
                throw new DivideByZeroException($"{value}^{power} results in a division by zero, which is undefined.");
            }
            return NMath.Pow(value, power);
        }
        #endregion

        #region Other Modifications
        /// <summary>
        /// The product of an integer and all the integers below it; e.g., factorial four ( 4! ) is equal to 24.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// <param name="baseValue"></param>
        /// <param name="plusMinusValue">Value to add and subtract from the base value.</param>
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
        /// <param name="baseValue"></param>
        /// <param name="plusMinusValue">Value to add and subtract from the base value.</param>
        /// <returns></returns>
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
        public static double Limit(double value, double min, double max, double tolerance = ZeroTolerance)
        {
            if (max < min) throw new ArgumentException($"Max limit, {max}, is less than the min limit, {min}");
            if (IsGreaterThan(value, max, tolerance)) { return max; }
            if (IsLessThan(value, min, tolerance)) { return min; }
            return value;
        }
        #endregion
    }
}

