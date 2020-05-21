using System;
using NMath = System.Math;

namespace MPT.Math.NumberTypeExtensions
{
    /// <summary>
    /// Contains extension methods dealing generically with numbers. 
    /// </summary>
    public static class NumberTypeExtensionLibrary
    {
        #region Signs
        /// <summary>
        /// Value is greater than 0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPositiveSign(this int value)
        {
            return (value > 0);
        }

        /// <summary>
        /// Value is greater than the zero-tolerance.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsPositiveSign(this double value, double tolerance = Numbers.ZeroTolerance)
        {
            if (value.IsZeroSign(tolerance)) { return false; }
            return (value > NMath.Abs(tolerance));
        }

        /// <summary>
        /// Value is less than zero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNegativeSign(this int value)
        {
            return (value < 0);
        }

        /// <summary>
        /// Value is less than the zero-tolerance.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsNegativeSign(this double value, double tolerance = Numbers.ZeroTolerance)
        {
            if (value.IsZeroSign(tolerance)) { return false; }
           
            return (value < NMath.Abs(tolerance));
        }

        /// <summary>
        /// Value is within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsZeroSign(this double value, double tolerance = Numbers.ZeroTolerance)
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
        public static bool IsEqualTo(this double value1, double value2, double tolerance = Numbers.ZeroTolerance)
        {
            return Numbers.AreEqual(value1, value2, tolerance);
        }

        /// <summary>
        /// Value is greater than the provided value, within the absolute value of the zero-tolerance.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsGreaterThan(this double value1, double value2, double tolerance = Numbers.ZeroTolerance)
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
        public static bool IsLessThan(this double value1, double value2, double tolerance = Numbers.ZeroTolerance)
        {
            if (value1.IsEqualTo(value2, tolerance)) { return false; }
            return ((value1 - value2) < NMath.Abs(tolerance));
        }

        /// <summary>
        /// Determines whether [is greater than or equal to] [the specified value2].
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is greater than or equal to] [the specified value2]; otherwise, <c>false</c>.</returns>
        public static bool IsGreaterThanOrEqualTo(this double value1, double value2, double tolerance = Numbers.ZeroTolerance)
        {
            return (value1.IsGreaterThan(value2, tolerance) || value1.IsEqualTo(value2, tolerance));
        }

        /// <summary>
        /// Determines whether [is less than or equal to] [the specified value2].
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns><c>true</c> if [is less than or equal to] [the specified value2]; otherwise, <c>false</c>.</returns>
        public static bool IsLessThanOrEqualTo(this double value1, double value2, double tolerance = Numbers.ZeroTolerance)
        {
            return (value1.IsLessThan(value2, tolerance) || value1.IsEqualTo(value2, tolerance));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Value is an odd number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsOdd(this int value)
        {
            return (value % 2 != 0);
        }

        /// <summary>
        /// Value is an even number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEven(this int value)
        {
            return !IsOdd(value);
        }

        /// <summary>
        /// A whole number greater than 1, whose only two whole-number factors are 1 and itself. 
        /// Uses the 'Sieve of Eratosthenes', which is very efficient for solving small primes (i.e. &lt; 10,000,000,000).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPrime(this int value)
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
        public static bool IsComposite(this int value)
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
        public static int LastDigit(this int value)
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
        public static int Squared(this int value)
        {
            return (value * value);
        }
        /// <summary>
        /// Returns the value squared.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Squared(this double value)
        {
            return (value * value);
        }

        /// <summary>
        /// Returns the value cubed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Cubed(this int value)
        {
            return (value * value * value);
        }
        /// <summary>
        /// Returns the value cubed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Cubed(this double value)
        {
            return (value * value * value);
        }

        /// <summary>
        /// Returns the square root of the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double Sqrt(this int value)
        {
            return Sqrt((double)value);
        }
        /// <summary>
        /// Returns the square root of the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double Sqrt(this double value)
        {
            return NMath.Sqrt(value);
        }

        /// <summary>
        /// Returns the cube root of the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double CubeRoot(this int value)
        {
            return CubeRoot((double)value);
        }
        /// <summary>
        /// Returns the cube root of the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double CubeRoot(this double value)
        {
            return NMath.Sign(value) * NMath.Abs(value).Pow(1d / 3d);
        }

        /// <summary>
        /// Returns the root of the number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="root">The root.</param>
        /// <returns>System.Double.</returns>
        public static double Root(this double value, int root)
        {
            return root.IsOdd() ? NMath.Sign(value) * NMath.Abs(value).Pow(1d / root) : value.Pow(1d / root);
        }
        /// <summary>
        /// Returns the root of the number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="root">The root.</param>
        /// <returns>System.Double.</returns>
        public static double Root(this double value, double root)
        {
            return value.Pow(1d / root);
        }

        /// <summary>
        /// Returns the value raised to the power provided.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static double Pow(this int value, double power)
        {
            return ((double)value).Pow(power);
        }
        /// <summary>
        /// Returns the value raised to the power provided.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static double Pow(this double value, double power)
        {
            if (value == 0 && power.IsNegativeSign())
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
        public static int Factorial(this int value)
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
        public static int[] PlusMinus(this int baseValue, int plusMinusValue)
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
        public static double[] PlusMinus(this int baseValue, double plusMinusValue)
        {
            return ((double)baseValue).PlusMinus(plusMinusValue);
        }
        /// <summary>
        /// Returns the paired result of adding and subtracting the provided value from the base value.
        /// </summary>
        /// <param name="baseValue"></param>
        /// <param name="plusMinusValue">Value to add and subtract from the base value.</param>
        /// <returns></returns>
        public static double[] PlusMinus(this double baseValue, double plusMinusValue)
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
        public static int Limit(this int value, int min, int max)
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
        /// <returns>The clamped value.
        /// If value &gt; max, max will be returned.
        /// If value &lt; min, min will be returned.
        /// If min ≤ value ≥ max, value will be returned.</returns>
        public static double Limit(this int value, double min, double max)
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
        public static double Limit(this double value, double min, double max, double tolerance = Numbers.ZeroTolerance)
        {
            if (max < min) throw new ArgumentException($"Max limit, {max}, is less than the min limit, {min}");
            if (value.IsGreaterThan(max, tolerance)) { return max; }
            if (value.IsLessThan(min, tolerance)) { return min; }
            return value;
        }
        #endregion
    }
}

