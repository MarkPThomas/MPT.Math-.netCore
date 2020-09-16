using System;
using NMath = System.Math;
using NUnit.Framework;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace MPT.Math.UnitTests
{
    public class Comparables : IComparable<Comparables>
    {
        public double Number { get; }

        public Comparables(double number)
        {
            Number = number;
        }

        public int CompareTo(Comparables other)
        {
            if (Number > other.Number)
            {
                return 1;
            }
            if (Number < other.Number)
            {
                return -1;
            }
            return 0;
        }
    }

    [TestFixture]
    public static class NumbersTests
    {
        public static double Tolerance = 0.00001;

        #region Constants
        [Test]
        public static void Log10E()
        {
            Assert.AreEqual(0.4342944819, NMath.Round(Numbers.Log10E(), 11));
        }

        [Test]
        public static void Log2E()
        {
            Assert.AreEqual(1.44269504, NMath.Round(Numbers.Log2E(), 8));
        }

        [Test]
        public static void GoldenRatio()
        {
            Assert.AreEqual(1.61803398875, NMath.Round(Numbers.GoldenRatio(), 11));
        }
        #endregion

        #region Signs
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = true)]
        public static bool IsPositiveSign_Int(int number)
        {
            return Numbers.IsPositiveSign(number);
        }

        [TestCase(-1, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        public static bool IsNegativeSign_Int(int number)
        {
            return Numbers.IsNegativeSign(number);
        }

        [TestCase(-1, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(-5.31256712, ExpectedResult = false)]
        [TestCase(5.31256712, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, ExpectedResult = false)]
        public static bool IsPositiveSign_Double_Default_Tolerance(double number)
        {
            return Numbers.IsPositiveSign(number);
        }

        [TestCase(-0.001, 0.1, ExpectedResult = false)]
        [TestCase(0, 0.1, ExpectedResult = false)]
        [TestCase(0.001, 0.1, ExpectedResult = false)]
        [TestCase(0.001, 0.0001, ExpectedResult = true)]
        [TestCase(0.001, -0.0001, ExpectedResult = true)]
        public static bool IsPositiveSign_Double_Custom_Tolerance(double number, double tolerance)
        {
            return Numbers.IsPositiveSign(number, tolerance);
        }

        [TestCase(-1, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(-5.31256712, ExpectedResult = true)]
        [TestCase(5.31256712, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, ExpectedResult = true)]
        public static bool IsNegativeSign_Double_Default_Tolerance(double number)
        {
            return Numbers.IsNegativeSign(number);
        }

        [TestCase(-0.001, 0.1, ExpectedResult = false)]
        [TestCase(0, 0.1, ExpectedResult = false)]
        [TestCase(0.001, 0.1, ExpectedResult = false)]
        [TestCase(-0.001, 0.0001, ExpectedResult = true)]
        [TestCase(-0.001, -0.0001, ExpectedResult = true)]
        public static bool IsNegativeSign_Double_Custom_Tolerance(double number, double tolerance)
        {
            return Numbers.IsNegativeSign(number, tolerance);
        }

        [TestCase(0, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(0.0001, ExpectedResult = false)]
        [TestCase(-0.0001, ExpectedResult = false)]
        [TestCase(1.2345E-7, ExpectedResult = false)]
        [TestCase(1.437821381955473E-07, ExpectedResult = false)]
        [TestCase(-1.2345E-7, ExpectedResult = false)]
        [TestCase(-1.437821381955473E-07, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, ExpectedResult = false)]
        public static bool IsZeroSign_Default_Tolerance(double value)
        {
            return Numbers.IsZeroSign(value);
        }

        [TestCase(0.0001, 0.001, ExpectedResult = true)]
        [TestCase(-0.0001, 0.001, ExpectedResult = true)]
        [TestCase(0.0001, -0.001, ExpectedResult = true)]
        [TestCase(-0.0001, -0.001, ExpectedResult = true)]
        [TestCase(0.0001, 0.0001, ExpectedResult = false)]
        [TestCase(-0.0001, 0.0001, ExpectedResult = false)]
        [TestCase(0.0001, -0.0001, ExpectedResult = false)]
        [TestCase(-0.0001, -0.0001, ExpectedResult = false)]
        [TestCase(1.2345E-7, 0.0001, ExpectedResult = true)]
        [TestCase(1.437821381955473E-07, 0.0001, ExpectedResult = true)]
        [TestCase(-1.2345E-7, 0.0001, ExpectedResult = true)]
        [TestCase(-1.437821381955473E-07, 0.0001, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, 0.0001, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, 0.0001, ExpectedResult = false)]
        public static bool IsZeroSign_Custom_Tolerance(double value, double tolerance)
        {
            return Numbers.IsZeroSign(value, tolerance);
        }
        #endregion

        #region Comparisons
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(-1, -1, ExpectedResult = true)]
        [TestCase(-1, 1, ExpectedResult = false)]
        [TestCase(5.6882, 5.6882, ExpectedResult = true)]
        [TestCase(5.6882, 5.6880, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, double.NegativeInfinity, ExpectedResult = false)]
        public static bool AreEqual_Default_Tolerance(double value1, double value2)
        {
            return Numbers.AreEqual(value1, value2);
        }

        [TestCase(5.555, 5.554, 0.001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, -0.001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, 0.001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, -0.001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, 0.0001, ExpectedResult = false)]
        [TestCase(-5.555, -5.554, 0.0001, ExpectedResult = false)]
        public static bool AreEqual_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return Numbers.AreEqual(value1, value2, tolerance);
        }

        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(-1, -1, ExpectedResult = true)]
        [TestCase(-1, 1, ExpectedResult = false)]
        [TestCase(5.6882, 5.6882, ExpectedResult = true)]
        [TestCase(5.6882, 5.6880, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, double.NegativeInfinity, ExpectedResult = false)]
        public static bool IsEqualTo_Default_Tolerance(double value1, double value2)
        {
            return Numbers.IsEqualTo(value1, value2);
        }

        [TestCase(5.555, 5.554, 0.001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, -0.001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, 0.001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, -0.001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, 0.0001, ExpectedResult = false)]
        [TestCase(-5.555, -5.554, 0.0001, ExpectedResult = false)]
        public static bool IsEqualTo_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return Numbers.IsEqualTo(value1, value2, tolerance);
        }

        [TestCase(0, 0, ExpectedResult = false)]
        [TestCase(1, 1, ExpectedResult = false)]
        [TestCase(-1, -1, ExpectedResult = false)]
        [TestCase(-1, -2, ExpectedResult = true)]
        [TestCase(-2, -1, ExpectedResult = false)]
        [TestCase(1, -1, ExpectedResult = true)]
        [TestCase(5.6882, 0, ExpectedResult = true)]
        [TestCase(5.6882, 5.68, ExpectedResult = true)]
        [TestCase(-5.6882, 0, ExpectedResult = false)]
        [TestCase(-5.6882, -5.68, ExpectedResult = false)]
        [TestCase(5.6882, double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(5.6882, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(-5.6882, double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(-5.6882, double.NegativeInfinity, ExpectedResult = true)]
        public static bool IsGreaterThan_Default_Tolerance(double value1, double value2)
        {
            return Numbers.IsGreaterThan(value1, value2);
        }

        [TestCase(5.555, 5.554, 0.001, ExpectedResult = false)]
        [TestCase(5.555, 5.554, 0.0001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, -0.0001, ExpectedResult = true)]
        [TestCase(-5.554, -5.555, 0.001, ExpectedResult = false)]
        [TestCase(-5.554, -5.555, 0.0001, ExpectedResult = true)]
        [TestCase(-5.554, -5.555, -0.0001, ExpectedResult = true)]
        public static bool IsGreaterThan_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return Numbers.IsGreaterThan(value1, value2, tolerance);
        }

        [TestCase(0, 0, ExpectedResult = false)]
        [TestCase(1, 1, ExpectedResult = false)]
        [TestCase(-1, -1, ExpectedResult = false)]
        [TestCase(-1, -2, ExpectedResult = false)]
        [TestCase(-2, -1, ExpectedResult = true)]
        [TestCase(1, -1, ExpectedResult = false)]
        [TestCase(5.6882, 0, ExpectedResult = false)]
        [TestCase(5.68, 5.6882, ExpectedResult = true)]
        [TestCase(-5.6882, 0, ExpectedResult = true)]
        [TestCase(-5.6882, -5.68, ExpectedResult = true)]
        [TestCase(5.6882, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(5.6882, double.NegativeInfinity, ExpectedResult = false)]
        [TestCase(-5.6882, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(-5.6882, double.NegativeInfinity, ExpectedResult = false)]
        public static bool IsLessThan_Default_Tolerance(double value1, double value2)
        {
            return Numbers.IsLessThan(value1, value2);
        }

        [TestCase(5.554, 5.555, 0.0001, ExpectedResult = true)]
        [TestCase(5.554, 5.555, -0.0001, ExpectedResult = true)]
        [TestCase(5.554, 5.555, 0.01, ExpectedResult = false)]
        [TestCase(-5.555, -5.554, 0.0001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, -0.0001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, 0.01, ExpectedResult = false)]
        public static bool IsLessThan_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return Numbers.IsLessThan(value1, value2, tolerance);
        }

        [TestCase(-1, 0, ExpectedResult = false)]
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(1, 0, ExpectedResult = true)]
        [TestCase(-1, 2.3, ExpectedResult = false)]
        [TestCase(1.5, -2.3, ExpectedResult = true)]
        [TestCase(0, 2.3, ExpectedResult = false)]
        [TestCase(2.29, 2.3, ExpectedResult = false)]
        [TestCase(2.3, 2.3, ExpectedResult = true)]
        [TestCase(2.31, 2.3, ExpectedResult = true)]
        [TestCase(-2.29, -2.3, ExpectedResult = true)]
        [TestCase(-2.3, -2.3, ExpectedResult = true)]
        [TestCase(-2.31, -2.3, ExpectedResult = false)]
        public static bool IsGreaterThanOrEqualTo_Default_Tolerance(double value1, double value2)
        {
            return Numbers.IsGreaterThanOrEqualTo(value1, value2);
        }

        [TestCase(-1, 0, 0.0001, ExpectedResult = false)]
        [TestCase(0, 0, 0.0001, ExpectedResult = true)]
        [TestCase(1, 0, 0.0001, ExpectedResult = true)]
        [TestCase(-1, 2.3, 0.0001, ExpectedResult = false)]
        [TestCase(1.5, -2.3, 0.0001, ExpectedResult = true)]
        [TestCase(0, 2.3, 0.0001, ExpectedResult = false)]
        [TestCase(2.29, 2.3, 0.0001, ExpectedResult = false)]
        [TestCase(2.3, 2.3, 0.0001, ExpectedResult = true)]
        [TestCase(2.31, 2.3, 0.0001, ExpectedResult = true)]
        [TestCase(-2.29, -2.3, 0.0001, ExpectedResult = true)]
        [TestCase(-2.3, -2.3, 0.0001, ExpectedResult = true)]
        [TestCase(-2.31, -2.3, 0.0001, ExpectedResult = false)]
        public static bool IsGreaterThanOrEqualTo_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return Numbers.IsGreaterThanOrEqualTo(value1, value2, tolerance);
        }

        [TestCase(-1, 0, ExpectedResult = true)]
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(1, 0, ExpectedResult = false)]
        [TestCase(-1, 2.3, ExpectedResult = true)]
        [TestCase(1.5, -2.3, ExpectedResult = false)]
        [TestCase(0, 2.3, ExpectedResult = true)]
        [TestCase(2.29, 2.3, ExpectedResult = true)]
        [TestCase(2.3, 2.3, ExpectedResult = true)]
        [TestCase(2.31, 2.3, ExpectedResult = false)]
        [TestCase(-2.29, -2.3, ExpectedResult = false)]
        [TestCase(-2.3, -2.3, ExpectedResult = true)]
        [TestCase(-2.31, -2.3, ExpectedResult = true)]
        public static bool IsLessThanOrEqualTo_Default_Tolerance(double value1, double value2)
        {
            return Numbers.IsLessThanOrEqualTo(value1, value2);
        }

        [TestCase(-1, 0, 0.0001, ExpectedResult = true)]
        [TestCase(0, 0, 0.0001, ExpectedResult = true)]
        [TestCase(1, 0, 0.0001, ExpectedResult = false)]
        [TestCase(-1, 2.3, 0.0001, ExpectedResult = true)]
        [TestCase(1.5, -2.3, 0.0001, ExpectedResult = false)]
        [TestCase(0, 2.3, 0.0001, ExpectedResult = true)]
        [TestCase(2.29, 2.3, 0.0001, ExpectedResult = true)]
        [TestCase(2.3, 2.3, 0.0001, ExpectedResult = true)]
        [TestCase(2.31, 2.3, 0.0001, ExpectedResult = false)]
        [TestCase(-2.29, -2.3, 0.0001, ExpectedResult = false)]
        [TestCase(-2.3, -2.3, 0.0001, ExpectedResult = true)]
        [TestCase(-2.31, -2.3, 0.0001, ExpectedResult = true)]
        public static bool IsLessThanOrEqualTo_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return Numbers.IsLessThanOrEqualTo(value1, value2, tolerance);
        }

        [Test]
        public static void Max_Throws_Exception_If_Argument_Is_Null()
        {
            Assert.Throws<ArgumentException>(() => Numbers.Max<Comparables>(null));
        }

        [Test]
        public static void Max_Throws_Exception_If_Array_Is_Not_Dimensioned()
        {
            Comparables[] comparables = new Comparables[0];
            Assert.Throws<ArgumentException>(() => Numbers.Max(comparables));
        }

        [Test]
        public static void Max_Returns_Max_Object_Of_Comparable_Objects()
        {
            Comparables[] comparables = new Comparables[3];
            comparables[0] = new Comparables(6);
            comparables[1] = new Comparables(-1);
            comparables[2] = new Comparables(9);

            Comparables maxComparables = Numbers.Max(comparables);
            Assert.AreEqual(comparables[2], maxComparables);
        }

        [Test]
        public static void Min_Throws_Exception_If_Argument_Is_Null()
        {
            Assert.Throws<ArgumentException>(() => Numbers.Min<Comparables>(null));
        }

        [Test]
        public static void Min_Throws_Exception_If_Array_Is_Not_Dimensioned()
        {
            Comparables[] comparables = new Comparables[0];
            Assert.Throws<ArgumentException>(() => Numbers.Min(comparables));
        }

        [Test]
        public static void Min_Returns_Min_Object_Of_Comparable_Objects()
        {
            Comparables[] comparables = new Comparables[3];
            comparables[0] = new Comparables(6);
            comparables[1] = new Comparables(-1);
            comparables[2] = new Comparables(9);

            Comparables maxComparables = Numbers.Min(comparables);
            Assert.AreEqual(comparables[1], maxComparables);
        }

        [TestCase(2, 1, 3, ExpectedResult = true)]
        [TestCase(1, 1, 3, ExpectedResult = true)]
        [TestCase(1, 3, 1, ExpectedResult = true)]
        [TestCase(0.999, 1, 3, ExpectedResult = false)]
        [TestCase(0.999, 3, 1, ExpectedResult = false)]
        [TestCase(3, 1, 3, ExpectedResult = true)]
        [TestCase(3, 3, 1, ExpectedResult = true)]
        [TestCase(3.001, 1, 3, ExpectedResult = false)]
        [TestCase(3.001, 3, 1, ExpectedResult = false)]
        [TestCase(1, 1.1, 3, ExpectedResult = false)]
        [TestCase(1.2, 1.1, 3, ExpectedResult = true)]
        [TestCase(3.01, 1, 3.001, ExpectedResult = false)]
        [TestCase(3.0001, 1, 3.001, ExpectedResult = true)]
        public static bool IsWithinInclusive(double value, double value1, double value2)
        {
            return Numbers.IsWithinInclusive(value, value1, value2, Tolerance);
        }

        [TestCase(2, 1, 3, ExpectedResult = true)]
        [TestCase(1, 1, 3, ExpectedResult = true)]
        [TestCase(1, 3, 1, ExpectedResult = true)]
        [TestCase(0.999, 1, 3, ExpectedResult = false)]
        [TestCase(0.999, 3, 1, ExpectedResult = false)]
        [TestCase(3, 1, 3, ExpectedResult = true)]
        [TestCase(3, 3, 1, ExpectedResult = true)]
        [TestCase(3.001, 1, 3, ExpectedResult = false)]
        [TestCase(3.001, 3, 1, ExpectedResult = false)]
        public static bool IsWithinInclusive_of_Generic_Type(double value, double value1, double value2)
        {
            Comparables comparablesValue1 = new Comparables(value1);
            Comparables comparablesValue = new Comparables(value);
            Comparables comparablesValue2 = new Comparables(value2);

            return Numbers.IsWithinInclusive(comparablesValue, comparablesValue1, comparablesValue2);
        }

        [TestCase(2, 1, 3, ExpectedResult = true)]
        [TestCase(1, 1, 3, ExpectedResult = false)]
        [TestCase(1, 3, 1, ExpectedResult = false)]
        [TestCase(0.999, 1, 3, ExpectedResult = false)]
        [TestCase(0.999, 3, 1, ExpectedResult = false)]
        [TestCase(3, 1, 3, ExpectedResult = false)]
        [TestCase(3, 3, 1, ExpectedResult = false)]
        [TestCase(3.001, 1, 3, ExpectedResult = false)]
        [TestCase(3.001, 3, 1, ExpectedResult = false)]
        [TestCase(1, 1.1, 3, ExpectedResult = false)]
        [TestCase(1.1, 1.1, 3, ExpectedResult = false)]
        [TestCase(3.01, 1, 3.001, ExpectedResult = false)]
        [TestCase(3.001, 1, 3.001, ExpectedResult = false)]
        public static bool IsWithinExclusive(double value, double value1, double value2)
        {
            return Numbers.IsWithinExclusive(value, value1, value2, Tolerance);
        }

        [TestCase(2, 1, 3, ExpectedResult = true)]
        [TestCase(1, 1, 3, ExpectedResult = false)]
        [TestCase(1, 3, 1, ExpectedResult = false)]
        [TestCase(0.999, 1, 3, ExpectedResult = false)]
        [TestCase(0.999, 3, 1, ExpectedResult = false)]
        [TestCase(3, 1, 3, ExpectedResult = false)]
        [TestCase(3, 3, 1, ExpectedResult = false)]
        [TestCase(3.001, 1, 3, ExpectedResult = false)]
        [TestCase(3.001, 3, 1, ExpectedResult = false)]
        public static bool IsWithinExclusive_of_Generic_Type(double value, double value1, double value2)
        {
            Comparables comparablesValue1 = new Comparables(value1);
            Comparables comparablesValue = new Comparables(value);
            Comparables comparablesValue2 = new Comparables(value2);

            return Numbers.IsWithinExclusive(comparablesValue, comparablesValue1, comparablesValue2);
        }
        #endregion

        #region Properties
        [TestCase(0, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = true)]
        [TestCase(54, ExpectedResult = true)]
        [TestCase(-2, ExpectedResult = true)]
        [TestCase(-6, ExpectedResult = true)]
        [TestCase(-54, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(-3, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = false)]
        public static bool IsEven_Int(int number)
        {
            return Numbers.IsEven(number);
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(6, ExpectedResult = false)]
        [TestCase(54, ExpectedResult = false)]
        [TestCase(-2, ExpectedResult = false)]
        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-54, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(-1, ExpectedResult = true)]
        [TestCase(-3, ExpectedResult = true)]
        [TestCase(-5, ExpectedResult = true)]
        public static bool IsOdd_Int(int number)
        {
            return Numbers.IsOdd(number);
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(7, ExpectedResult = true)]
        [TestCase(11, ExpectedResult = true)]
        [TestCase(13, ExpectedResult = true)]
        [TestCase(17, ExpectedResult = true)]
        [TestCase(19, ExpectedResult = true)]
        [TestCase(23, ExpectedResult = true)]
        [TestCase(29, ExpectedResult = true)]
        [TestCase(71, ExpectedResult = true)]
        [TestCase(113, ExpectedResult = true)]
        [TestCase(601, ExpectedResult = true)]
        [TestCase(733, ExpectedResult = true)]
        [TestCase(809, ExpectedResult = true)]
        [TestCase(863, ExpectedResult = true)]
        [TestCase(941, ExpectedResult = true)]
        [TestCase(967, ExpectedResult = true)]
        [TestCase(997, ExpectedResult = true)]
        [TestCase(-2, ExpectedResult = true)]
        [TestCase(-3, ExpectedResult = true)]
        [TestCase(-5, ExpectedResult = true)]
        [TestCase(-7, ExpectedResult = true)]
        [TestCase(-11, ExpectedResult = true)]
        [TestCase(-13, ExpectedResult = true)]
        [TestCase(-17, ExpectedResult = true)]
        [TestCase(-19, ExpectedResult = true)]
        [TestCase(-23, ExpectedResult = true)]
        [TestCase(-29, ExpectedResult = true)]
        [TestCase(-71, ExpectedResult = true)]
        [TestCase(-113, ExpectedResult = true)]
        [TestCase(-601, ExpectedResult = true)]
        [TestCase(-733, ExpectedResult = true)]
        [TestCase(-809, ExpectedResult = true)]
        [TestCase(-863, ExpectedResult = true)]
        [TestCase(-941, ExpectedResult = true)]
        [TestCase(-967, ExpectedResult = true)]
        [TestCase(-997, ExpectedResult = true)]
        public static bool IsPrime_Returns_True_for_Prime_Numbers(int number)
        {
            return Numbers.IsPrime(number);
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = false)]
        [TestCase(6, ExpectedResult = false)]
        [TestCase(8, ExpectedResult = false)]
        [TestCase(9, ExpectedResult = false)]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(25, ExpectedResult = false)]
        [TestCase(841, ExpectedResult = false)]
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(-4, ExpectedResult = false)]
        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-8, ExpectedResult = false)]
        [TestCase(-9, ExpectedResult = false)]
        [TestCase(-10, ExpectedResult = false)]
        [TestCase(-841, ExpectedResult = false)]
        public static bool IsPrime_Returns_False_for_Not_Prime_Numbers(int number)
        {
            return Numbers.IsPrime(number);
        }

        [TestCase(-1, ExpectedResult = 1)]
        [TestCase(-97, ExpectedResult = 7)]
        [TestCase(3, ExpectedResult = 3)]
        [TestCase(45, ExpectedResult = 5)]
        [TestCase(63, ExpectedResult = 3)]
        public static int LastDigit(int number)
        {
            return Numbers.LastDigit(number);
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(7, ExpectedResult = false)]
        [TestCase(11, ExpectedResult = false)]
        [TestCase(13, ExpectedResult = false)]
        [TestCase(17, ExpectedResult = false)]
        [TestCase(19, ExpectedResult = false)]
        [TestCase(23, ExpectedResult = false)]
        [TestCase(29, ExpectedResult = false)]
        [TestCase(71, ExpectedResult = false)]
        [TestCase(113, ExpectedResult = false)]
        [TestCase(601, ExpectedResult = false)]
        [TestCase(733, ExpectedResult = false)]
        [TestCase(809, ExpectedResult = false)]
        [TestCase(863, ExpectedResult = false)]
        [TestCase(941, ExpectedResult = false)]
        [TestCase(967, ExpectedResult = false)]
        [TestCase(997, ExpectedResult = false)]
        [TestCase(-2, ExpectedResult = false)]
        [TestCase(-3, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = false)]
        [TestCase(-7, ExpectedResult = false)]
        [TestCase(-11, ExpectedResult = false)]
        [TestCase(-13, ExpectedResult = false)]
        [TestCase(-17, ExpectedResult = false)]
        [TestCase(-19, ExpectedResult = false)]
        [TestCase(-23, ExpectedResult = false)]
        [TestCase(-29, ExpectedResult = false)]
        [TestCase(-71, ExpectedResult = false)]
        [TestCase(-113, ExpectedResult = false)]
        [TestCase(-601, ExpectedResult = false)]
        [TestCase(-733, ExpectedResult = false)]
        [TestCase(-809, ExpectedResult = false)]
        [TestCase(-863, ExpectedResult = false)]
        [TestCase(-941, ExpectedResult = false)]
        [TestCase(-967, ExpectedResult = false)]
        [TestCase(-997, ExpectedResult = false)]
        public static bool IsComposite_Returns_True_for_Prime_Numbers(int number)
        {
            return Numbers.IsComposite(number);
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = true)]
        [TestCase(8, ExpectedResult = true)]
        [TestCase(9, ExpectedResult = true)]
        [TestCase(10, ExpectedResult = true)]
        [TestCase(841, ExpectedResult = true)]
        [TestCase(-1, ExpectedResult = true)]
        [TestCase(-4, ExpectedResult = true)]
        [TestCase(-6, ExpectedResult = true)]
        [TestCase(-8, ExpectedResult = true)]
        [TestCase(-9, ExpectedResult = true)]
        [TestCase(-10, ExpectedResult = true)]
        [TestCase(-841, ExpectedResult = true)]
        public static bool IsComposite_Returns_False_for_Not_Prime_Numbers(int number)
        {
            return Numbers.IsComposite(number);
        }


        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(-1, 0)]
        [TestCase(0.0009, 4)]
        [TestCase(0.9, 1)]
        [TestCase(1.1, 1)]
        [TestCase(-0.0009, 4)]
        [TestCase(-0.9, 1)]
        [TestCase(-1.1, 1)]
        [TestCase(5000.12345678, 8)]
        [TestCase(-5000.12345678, 8)]
        [TestCase(-0.1234567891111111, 15)]  // 16 places = 16 > 15 so 1 places extra => 16 - 1 places = 15 places
        [TestCase(-5000.1234567891111111, 11)]  // 16 places + 4 whole = 20 > 15 so 5 places extra => 16 - 5 places = 11 places
        [TestCase(-1.437821381955473E-07, 15)]
        [TestCase(1E-1, 1)]
        [TestCase(1E-2, 2)]
        [TestCase(1E-3, 3)]
        [TestCase(1.34E-3, 5)]
        [TestCase(1E-15, 15)] // Compare to test w/ no rounding limits
        [TestCase(1E-16, 15)] // Compare to test w/ no rounding limits
        public static void DecimalPlaces(double value, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.DecimalPlaces(value));
        }

        [TestCase(-0.1234567891111111, 15)]  // 16 places + 1 whole = 16> 15 so 1 places extra => 16 - 1 places = 15 places
        [TestCase(-5000.1234567891111111, 11)]  // 16 places + 4 whole = 20 > 15 so 5 places extra => 16 - 5 places = 11 places
        [TestCase(-1.437821381955473E-07, 21)]
        [TestCase(1E-15, 15)] // Compare to test w/ rounding limits
        [TestCase(1E-16, 16)] // Compare to test w/ rounding limits
        public static void DecimalPlaces_Not_Limited_when_Overridden(double value, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.DecimalPlaces(value, limitForRounding: false));
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(10, ExpectedResult = 1)]
        [TestCase(10.0, ExpectedResult = 1)]
        [TestCase(15, ExpectedResult = 2)]
        [TestCase(15.0, ExpectedResult = 2)]
        [TestCase(666, ExpectedResult = 3)]
        [TestCase(6.66, ExpectedResult = 3)]
        [TestCase(0.1, ExpectedResult = 1)]
        [TestCase(0.01, ExpectedResult = 1)]
        [TestCase(0.21, ExpectedResult = 2)]
        [TestCase(0.001, ExpectedResult = 1)]
        [TestCase(0.321, ExpectedResult = 3)]
        [TestCase(0.00321, ExpectedResult = 3)]
        [TestCase(1.00321, ExpectedResult = 6)]
        public static int SignificantFigures(double value)
        {
            return Numbers.SignificantFigures(value);
        }

        [TestCase(1.23456, -10, ExpectedResult = 6)] // Limit negative
        [TestCase(1.23456, 0, ExpectedResult = 1)] // Limit zero
        [TestCase(1.23456, 1, ExpectedResult = 2)] // Limit one
        [TestCase(1.23456, 3, ExpectedResult = 4)] // Limit less than number provided
        [TestCase(1.23456, 5, ExpectedResult = 6)] // Limit equal to number provided
        [TestCase(1.23456, 6, ExpectedResult = 6)] // Limit greater than number provided
        public static int SignificantFigures_with_Limits(double value, int decimalDigitsTolerance)
        {
            return Numbers.SignificantFigures(value, decimalDigitsTolerance);
        }
        #endregion

        #region Powers
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 4)]
        [TestCase(3, ExpectedResult = 9)]
        public static int Squared_Integer(int value)
        {
            return Numbers.Squared(value);
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 8)]
        [TestCase(3, ExpectedResult = 27)]
        public static int Cubed_Integer(int value)
        {
            return Numbers.Cubed(value);
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 4)]
        [TestCase(3, ExpectedResult = 9)]
        [TestCase(4.4, ExpectedResult = 4.4 * 4.4)]
        [TestCase(double.PositiveInfinity, ExpectedResult = double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, ExpectedResult = double.PositiveInfinity)]
        public static double Squared_Double(double value)
        {
            return Numbers.Squared(value);
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 8)]
        [TestCase(3, ExpectedResult = 27)]
        [TestCase(4.4, ExpectedResult = 4.4 * 4.4 * 4.4)]
        [TestCase(double.PositiveInfinity, ExpectedResult = double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, ExpectedResult = double.NegativeInfinity)]
        public static double Cubed_Double(double value)
        {
            return Numbers.Cubed(value);
        }

        [TestCase(5, 2, ExpectedResult = 25)]
        [TestCase(5, -2, ExpectedResult = (1D / 25))]
        [TestCase(0, 2, ExpectedResult = 0)]
        [TestCase(2, 0, ExpectedResult = 1)]
        [TestCase(0, 0, ExpectedResult = 1)]
        [TestCase(-1, 0, ExpectedResult = 1)]
        public static double Pow_Integer(int number, int power)
        {
            return Numbers.Pow(number, power);
        }

        [TestCase(0, -1)]
        public static void Pow_Integer_Throws_Exception(int number, int power)
        {
            Assert.Throws<DivideByZeroException>(() => Numbers.Pow(number, power));
        }

        [TestCase(5, 2, ExpectedResult = 25)]
        [TestCase(5, -2, ExpectedResult = (1D / 25))]
        [TestCase(0, 2, ExpectedResult = 0)]
        [TestCase(2, 0, ExpectedResult = 1)]
        [TestCase(0, 0, ExpectedResult = 1)]
        [TestCase(-1, 0, ExpectedResult = 1)]
        [TestCase(double.PositiveInfinity, 0, ExpectedResult = 1)]
        [TestCase(double.NegativeInfinity, 0, ExpectedResult = 1)]
        [TestCase(double.PositiveInfinity, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, 3, ExpectedResult = double.NegativeInfinity)]
        [TestCase(2, double.PositiveInfinity, ExpectedResult = double.PositiveInfinity)]
        [TestCase(2, double.NegativeInfinity, ExpectedResult = 0)]
        public static double Pow_Double(double number, double power)
        {
            return Numbers.Pow(number, power);
        }

        [TestCase(0, -1)]
        public static void Pow_Double_Throws_Exception(double number, double power)
        {
            Assert.Throws<DivideByZeroException>(() => Numbers.Pow(number, power));
        }

        [TestCase(0, 0)]
        [TestCase(4, 2)]
        [TestCase(2, 1.414213562)]
        [TestCase(-4, double.NaN)]
        public static void Sqrt(double value, double expectedResult)
        {
            double actualResult = Numbers.Sqrt(value);
            Assert.AreEqual(expectedResult, actualResult, Tolerance);
        }

        [TestCase(-2.31)]
        [TestCase(0)]
        [TestCase(2.31)]
        public static void Sqrt_double(double value1)
        {
            Assert.AreEqual(NMath.Sqrt(value1), Numbers.Sqrt(value1));
        }

        [TestCase(-4)]
        [TestCase(-3)]
        [TestCase(-2)]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public static void Sqrt_integer(int value1)
        {
            Assert.AreEqual(NMath.Sqrt(value1), Numbers.Sqrt(value1));
        }

        [TestCase(-9, -2.0801)]
        [TestCase(-3, -1.4422)]
        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(3, 1.4422)]
        [TestCase(8, 2)]
        [TestCase(9, 2.0801)]
        [TestCase(15, 2.4662)]
        [TestCase(27, 3)]
        public static void CubeRoot_Integer(int value1, double valueExpected)
        {
            Assert.AreEqual(valueExpected, Numbers.CubeRoot(value1), 0.0001);
        }

        [TestCase(-9.9, -2.1472)]
        [TestCase(0, 0)]
        [TestCase(9.9, 2.1472)]
        public static void CubeRoot_Double(double value1, double valueExpected)
        {
            Assert.AreEqual(valueExpected, Numbers.CubeRoot(value1), 0.0001);
        }



        [Test]
        public static void Root_Integer_Throws_Exception_for_0_Root()
        {
            Assert.Throws<DivideByZeroException>(() => Numbers.Root(3, 0));
        }

        [TestCase(4, 2, 2)]
        [TestCase(9, 3, 2.0801)]
        [TestCase(-1, 3, -1)]
        [TestCase(-9, 3, -2.0801)]
        [TestCase(-1, -3, -1)]
        [TestCase(-9, -3, -0.4807)]
        public static void Root_Integer(double value1, int root, double valueExpected)
        {
            Assert.AreEqual(valueExpected, Numbers.Root(value1, root), 0.0001);
        }

        [TestCase(-1, 2)]
        public static void Root_Integer_of_Negative_Number_of_NonOdd_Root_Returns_NAN(double value1, int root)
        {
            Assert.IsNaN(Numbers.Root(value1, root));
        }

        [Test]
        public static void Root_Double_Throws_Exception_for_0_Root()
        {
            Assert.Throws<DivideByZeroException>(() => Numbers.Root(3.3, 0.0));
        }

        [TestCase(4.1, 2, 2.0248)]
        [TestCase(9.1, 3.1, 2.0388)]
        [TestCase(9.1, -3.1, 0.4905)]
        public static void Root_Double(double value1, double root, double valueExpected)
        {
            Assert.AreEqual(valueExpected, Numbers.Root(value1, root), 0.0001);
        }

        [TestCase(-1.1, 2.2)]
        public static void Root_Double_of_Negative_Number_Returns_NAN(double value1, double root)
        {
            Assert.IsNaN(Numbers.Root(value1, root));
        }
        #endregion

        #region Other Modifications
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 1 * 2)]
        [TestCase(3, ExpectedResult = 1 * 2 * 3)]
        [TestCase(4, ExpectedResult = 1 * 2 * 3 * 4)]
        public static int Factorial(int number)
        {
            return Numbers.Factorial(number);
        }


        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 1, 2, 0)]
        [TestCase(-2, -3, -5, 1)]
        public static void PlusMinus_BaseInteger_PlusMinus_Integer(int baseValue, int plusMinusValue, int valueExpected1, int valueExpected2)
        {
            int[] values = Numbers.PlusMinus(baseValue, plusMinusValue);
            Assert.AreEqual(valueExpected1, values[0], 0.000001);
            Assert.AreEqual(valueExpected2, values[1], 0.000001);
        }

        [TestCase(0, 0.0, 0.0, 0.0)]
        [TestCase(1, 1.1, 2.1, -0.1)]
        [TestCase(-2, -3.2, -5.2, 1.2)]
        public static void PlusMinus_BaseInteger_PlusMinus_Double(int baseValue, double plusMinusValue, double valueExpected1, double valueExpected2)
        {
            double[] values = Numbers.PlusMinus(baseValue, plusMinusValue);
            Assert.AreEqual(valueExpected1, values[0], 0.000001);
            Assert.AreEqual(valueExpected2, values[1], 0.000001);
        }

        [TestCase(0.0, 0.0, 0.0, 0.0)]
        [TestCase(1.1, 1, 2.1, 0.1)]
        [TestCase(-2.1, -3, -5.1, 0.9)]
        [TestCase(1.1, 1.1, 2.2, 0)]
        [TestCase(-2.2, -3.2, -5.4, 1)]
        public static void PlusMinus_BaseDouble_PlusMinus_Double(double baseValue, double plusMinusValue, double valueExpected1, double valueExpected2)
        {
            double[] values = Numbers.PlusMinus(baseValue, plusMinusValue);
            Assert.AreEqual(valueExpected1, values[0], 0.000001);
            Assert.AreEqual(valueExpected2, values[1], 0.000001);
        }

        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 1, 2, 1)]
        [TestCase(1, -1, 2, 1)]
        [TestCase(2, 1, 2, 2)]
        [TestCase(-1, -1, 2, -1)]
        [TestCase(3, 1, 2, 2)]
        [TestCase(-2, -1, 2, -1)]
        public static void Limit_Integer(int value, int min, int max, int valueExpected)
        {
            Assert.AreEqual(valueExpected, Numbers.Limit(value, min, max));
        }

        [TestCase(-2, 2, -1)]   // Flipped max/min
        [TestCase(3, 2, -1)]   // Flipped max/min
        public static void Limit_Integer_Throws_Argument_Exception_when_MaxMin_Reversed(int value, int min, int max)
        {
            Assert.Throws<ArgumentException>(() => Numbers.Limit(value, min, max));
        }

        [TestCase(1, 0.9, 1.1, 1)]
        [TestCase(1, 0.9, 2.1, 1)]
        [TestCase(1, -1.1, 2.1, 1)]
        [TestCase(2, 1.1, 2.1, 2)]
        [TestCase(-1, -1.1, 2.1, -1)]
        [TestCase(3, 1.1, 2.1, 2.1)]
        [TestCase(-2, -1.1, 2.1, -1.1)]
        public static void Limit_Integer_with_Double_Limits(int value, double min, double max, double valueExpected)
        {
            Assert.AreEqual(valueExpected, Numbers.Limit(value, min, max));
        }

        [TestCase(-2, 2.2, -1.2)]   // Flipped max/min
        [TestCase(3, 2.2, -1.2)]   // Flipped max/min
        public static void Limit_Integer_with_Double_Limits_Throws_Argument_Exception_when_MaxMin_Reversed(int value, double min, double max)
        {
            Assert.Throws<ArgumentException>(() => Numbers.Limit(value, min, max));
        }

        [TestCase(1.0, 0.9, 1.1, 1.0)]
        [TestCase(1.1, 0.9, 2.1, 1.1)]
        [TestCase(1.1, -1.1, 2.1, 1.1)]
        [TestCase(2.1, 1.1, 2.1, 2.1)]
        [TestCase(-1.1, -1.1, 2.1, -1.1)]
        [TestCase(3.1, 1.1, 2.1, 2.1)]
        [TestCase(-2.1, -1.1, 2.1, -1.1)]
        public static void Limit_Double_Default_Tolerance(double value, double min, double max, double valueExpected)
        {
            Assert.AreEqual(valueExpected, Numbers.Limit(value, min, max));
        }

        [TestCase(-2.2, 2.2, -1.2)]   // Flipped max/min
        [TestCase(3.2, 2.2, -1.2)]   // Flipped max/min
        public static void Limit_Double_Default_Throws_Argument_Exception_when_MaxMin_Reversed(double value, double min, double max)
        {
            Assert.Throws<ArgumentException>(() => Numbers.Limit(value, min, max));
        }

        [TestCase(1.0, 0.9, 1.1, 0.0001, 1.0)]
        [TestCase(1.1, 0.9, 2.1, 0.0001, 1.1)]
        [TestCase(1.1, -1.1, 2.1, 0.0001, 1.1)]
        [TestCase(2.1, 1.1, 2.1, 0.0001, 2.1)]
        [TestCase(-1.1, -1.1, 2.1, 0.0001, -1.1)]
        [TestCase(3.1, 1.1, 2.1, 0.0001, 2.1)]
        [TestCase(-2.1, -1.1, 2.1, 0.0001, -1.1)]
        public static void Limit_Double_Custom_Tolerance(double value, double min, double max, double tolerance, double valueExpected)
        {
            Assert.AreEqual(valueExpected, Numbers.Limit(value, min, max));
        }

        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(5, 0, 0)]
        [TestCase(-549, 1, -500)]
        [TestCase(-551, 1, -600)]
        [TestCase(12.345, 6, 12.345)] // technically 12.3450, but doubles do not retain trailing decimal zeros
        [TestCase(12.345, 5, 12.345)]
        [TestCase(12.345, 3, 12.3)]
        [TestCase(12.345, 2, 12)]
        [TestCase(12.345, 1, 10)]
        [TestCase(12.345, 0, 0)]
        [TestCase(-12.345, 6, -12.345)] // technically 12.3450, but doubles do not retain trailing decimal zeros
        [TestCase(-12.345, 5, -12.345)]
        [TestCase(-12.345, 3, -12.3)]
        [TestCase(-12.345, 2, -12)]
        [TestCase(-12.345, 1, -10)]
        [TestCase(-12.345, 0, 0)]
        [TestCase(0.012345, 7, 0.012345)] // technically 0.01234500, but doubles do not retain trailing decimal zeros
        [TestCase(0.012345, 6, 0.012345)] // technically 0.0123450, but doubles do not retain trailing decimal zeros
        [TestCase(0.012345, 5, 0.012345)]
        [TestCase(0.012345, 3, 0.0123)] 
        [TestCase(0.012345, 2, 0.012)]
        [TestCase(0.012345, 1, 0.01)]
        [TestCase(0.012345, 0, 0)]
        [TestCase(-0.012345, 7, -0.012345)] // technically 0.01234500, but doubles do not retain trailing decimal zeros
        [TestCase(-0.012345, 6, -0.012345)] // technically 0.0123450, but doubles do not retain trailing decimal zeros
        [TestCase(-0.012345, 5, -0.012345)]
        [TestCase(-0.012345, 3, -0.0123)]
        [TestCase(-0.012345, 2, -0.012)]
        [TestCase(-0.012345, 1, -0.01)]
        [TestCase(-0.012345, 0, 0)]
        public static void RoundToSignificantFigures(double value, int digits, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.RoundToSignificantFigures(value, digits), 0.000001);
        }

        [TestCase(12.345, 4, 12.35)] // 12.34 (even) or 12.35 (zero) *******************
        [TestCase(12.355, 4, 12.36)] // *******************
        [TestCase(-12.345, 4, -12.35)] // -12.34 or -12.35 *******************
        [TestCase(-12.355, 4, -12.36)] // *******************
        [TestCase(0.012345, 4, 0.01235)] // 0.01234 (even) or 0.01235 (zero) *******************
        [TestCase(0.012355, 4, 0.01236)] // *******************
        [TestCase(-0.012345, 4, -0.01235)] // -0.01234 or -0.01235 *******************
        [TestCase(-0.012355, 4, -0.01236)] // *******************
        public static void RoundToSignificantFigures_TieBreaker_Default(double value, int digits, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.RoundToSignificantFigures(value, digits), 0.000001);
        }

        [TestCase(12.345, 4, 12.35)] // 12.34 (even) or 12.35 (zero) *******************
        [TestCase(12.355, 4, 12.36)] // *******************
        [TestCase(-12.345, 4, -12.35)] // -12.34 or -12.35 *******************
        [TestCase(-12.355, 4, -12.36)] // *******************
        [TestCase(0.012345, 4, 0.01235)] // 0.01234 (even) or 0.01235 (zero) *******************
        [TestCase(0.012355, 4, 0.01236)] // *******************
        [TestCase(-0.012345, 4, -0.01235)] // -0.01234 or -0.01235 *******************
        [TestCase(-0.012355, 4, -0.01236)] // *******************
        public static void RoundToSignificantFigures_TieBreaker_AwayFromZero(double value, int digits, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.RoundToSignificantFigures(value, digits, RoundingTieBreaker.HalfAwayFromZero), 0.000001);
        }

        [TestCase(12.345, 4, 12.34)] // 12.34 (even) or 12.35 (zero) *******************
        [TestCase(12.355, 4, 12.36)] // *******************
        [TestCase(-12.345, 4, -12.34)] // -12.34 or -12.35 *******************
        [TestCase(-12.355, 4, -12.36)] // *******************
        [TestCase(0.012345, 4, 0.01234)] // 0.01234 (even) or 0.01235 (zero) *******************
        [TestCase(0.012355, 4, 0.01236)] // *******************
        [TestCase(-0.012345, 4, -0.01234)] // -0.01234 or -0.01235 *******************
        [TestCase(-0.012355, 4, -0.01236)] // *******************
        public static void RoundToSignificantFigures_TieBreaker_HalfToEven(double value, int digits, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.RoundToSignificantFigures(value, digits, RoundingTieBreaker.HalfToEven), 0.000001);
        }


        [TestCase(12.345, 6, 12.345)] // technically 12.345000, but doubles do not retain trailing decimal zeros
        [TestCase(12.345, 5, 12.345)] // technically 12.34500, but doubles do not retain trailing decimal zeros
        [TestCase(12.345, 4, 12.345)] // technically 12.3450, but doubles do not retain trailing decimal zeros
        [TestCase(12.345, 3, 12.345)]
        [TestCase(12.345, 1, 12.3)]
        [TestCase(12.345, 0, 12)]
        [TestCase(-12.345, 6, -12.345)] // technically 12.345000, but doubles do not retain trailing decimal zeros
        [TestCase(-12.345, 5, -12.345)] // technically 12.34500, but doubles do not retain trailing decimal zeros
        [TestCase(-12.345, 4, -12.345)] // technically 12.3450, but doubles do not retain trailing decimal zeros
        [TestCase(-12.345, 3, -12.345)]
        [TestCase(-12.345, 1, -12.3)]
        [TestCase(-12.345, 0, -12)]
        [TestCase(0.012345, 7, 0.012345)] // technically 0.0123450, but doubles do not retain trailing decimal zeros
        [TestCase(0.012345, 6, 0.012345)] 
        [TestCase(0.012345, 4, 0.0123)]
        [TestCase(0.012345, 3, 0.012)]
        [TestCase(0.012345, 2, 0.01)]
        [TestCase(0.012345, 1, 0)] // technically 0.0, but doubles do not retain trailing decimal zeros
        [TestCase(0.012345, 0, 0)]
        [TestCase(-0.012345, 7, -0.012345)] // technically 0.0123450, but doubles do not retain trailing decimal zeros
        [TestCase(-0.012345, 6, -0.012345)]
        [TestCase(-0.012345, 4, -0.0123)]
        [TestCase(-0.012345, 3, -0.012)]
        [TestCase(-0.012345, 2, -0.01)]
        [TestCase(-0.012345, 1, 0)] // technically 0.0, but doubles do not retain trailing decimal zeros
        [TestCase(-0.012345, 0, 0)]
        public static void RoundToDecimalPlaces(double value, int digits, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.RoundToDecimalPlaces(value, digits), 0.000001);
        }

        [TestCase(12.345, 2, 12.35)] // 12.34 (even) or 12.35 (zero) 
        [TestCase(12.355, 2, 12.36)] // Both methods
        [TestCase(-12.345, 2, -12.35)] // -12.34 (even) or -12.35 (zero) 
        [TestCase(-12.355, 2, -12.36)] // Both methods
        [TestCase(0.012345, 5, 0.01235)] // 0.01234 (even) or 0.01235 (zero) 
        [TestCase(0.012355, 5, 0.01236)] // Both methods
        [TestCase(-0.012345, 5, -0.01235)] // -0.01234 (even) or -0.01235 (zero) 
        [TestCase(-0.012355, 5, -0.01236)] // Both methods
        public static void RoundToDecimalPlaces_TieBreaker_Default(double value, int digits, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.RoundToDecimalPlaces(value, digits), 0.000001);
        }

        [TestCase(12.345, 2, 12.35)] // 12.34 (even) or 12.35 (zero) 
        [TestCase(12.355, 2, 12.36)] // Both methods
        [TestCase(-12.345, 2, -12.35)] // -12.34 (even) or -12.35 (zero) 
        [TestCase(-12.355, 2, -12.36)] // Both methods
        [TestCase(0.012345, 5, 0.01235)] // 0.01234 (even) or 0.01235 (zero) 
        [TestCase(0.012355, 5, 0.01236)] // Both methods
        [TestCase(-0.012345, 5, -0.01235)] // -0.01234 (even) or -0.01235 (zero) 
        [TestCase(-0.012355, 5, -0.01236)] // Both methods
        public static void RoundToDecimalPlaces_TieBreaker_AwayFromZero(double value, int digits, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.RoundToDecimalPlaces(value, digits, RoundingTieBreaker.HalfAwayFromZero), 0.000001);
        }

        [TestCase(12.345, 2, 12.34)] // 12.34 (even) or 12.35 (zero) 
        [TestCase(12.355, 2, 12.36)] // Both methods
        [TestCase(-12.345, 2, -12.34)] // -12.34 (even) or -12.35 (zero) 
        [TestCase(-12.355, 2, -12.36)] // Both methods
        [TestCase(0.012345, 5, 0.01234)] // 0.01234 (even) or 0.01235 (zero) 
        [TestCase(0.012355, 5, 0.01236)] // Both methods
        [TestCase(-0.012345, 5, -0.01234)] // -0.01234 (even) or -0.01235 (zero) 
        [TestCase(-0.012355, 5, -0.01236)] // Both methods
        public static void RoundToDecimalPlaces_TieBreaker_HalfToEven(double value, int digits, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.RoundToDecimalPlaces(value, digits, RoundingTieBreaker.HalfToEven), 0.000001);
        }
        #endregion

        #region Display
        [TestCase(0, 0, "0")]
        [TestCase(0, 1, "0.0")]
        [TestCase(5, 0, "0")]
        [TestCase(-549, 1, "-500")]
        [TestCase(-551, 1, "-600")]
        [TestCase(12.345, 7, "12.34500")]
        [TestCase(12.345, 6, "12.3450")] 
        [TestCase(12.345, 5, "12.345")]
        [TestCase(12.345, 3, "12.3")]
        [TestCase(12.345, 2, "12")]
        [TestCase(12.345, 1, "10")]
        [TestCase(12.345, 0, "0")]
        [TestCase(-12.345, 7, "-12.34500")]
        [TestCase(-12.345, 6, "-12.3450")] 
        [TestCase(-12.345, 5, "-12.345")]
        [TestCase(-12.345, 3, "-12.3")]
        [TestCase(-12.345, 2, "-12")]
        [TestCase(-12.345, 1, "-10")]
        [TestCase(-12.345, 0, "0")]
        [TestCase(0.012345, 7, "0.01234500")]
        [TestCase(0.012345, 6, "0.0123450")] 
        [TestCase(0.012345, 5, "0.012345")]
        [TestCase(0.012345, 3, "0.0123")]
        [TestCase(0.012345, 2, "0.012")]
        [TestCase(0.012345, 1, "0.01")]
        [TestCase(0.012345, 0, "0")]
        [TestCase(-0.012345, 7, "-0.01234500")] 
        [TestCase(-0.012345, 6, "-0.0123450")]  
        [TestCase(-0.012345, 5, "-0.012345")]
        [TestCase(-0.012345, 3, "-0.0123")]
        [TestCase(-0.012345, 2, "-0.012")]
        [TestCase(-0.012345, 1, "-0.01")]
        [TestCase(-0.012345, 0, "0")]
        public static void DisplayRoundedToSignificantFigures(double value, int sigFigs, string expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.DisplayRoundedToSignificantFigures(value, sigFigs));
        }

        [TestCase(12.345, 6, "12.345000")] 
        [TestCase(12.345, 5, "12.34500")] 
        [TestCase(12.345, 4, "12.3450")] 
        [TestCase(12.345, 3, "12.345")]
        [TestCase(12.345, 1, "12.3")]
        [TestCase(12.345, 0, "12")]
        [TestCase(-12.345, 6, "-12.345000")] 
        [TestCase(-12.345, 5, "-12.34500")] 
        [TestCase(-12.345, 4, "-12.3450")] 
        [TestCase(-12.345, 3, "-12.345")]
        [TestCase(-12.345, 1, "-12.3")]
        [TestCase(-12.345, 0, "-12")]
        [TestCase(0.012345, 7, "0.0123450")] 
        [TestCase(0.012345, 6, "0.012345")]
        [TestCase(0.012345, 4, "0.0123")]
        [TestCase(0.012345, 3, "0.012")]
        [TestCase(0.012345, 2, "0.01")]
        [TestCase(0.012345, 1, "0.0")] 
        [TestCase(0.012345, 0, "0")]
        [TestCase(-0.012345, 7, "-0.0123450")] 
        [TestCase(-0.012345, 6, "-0.012345")]
        [TestCase(-0.012345, 4, "-0.0123")]
        [TestCase(-0.012345, 3, "-0.012")]
        [TestCase(-0.012345, 2, "-0.01")]
        [TestCase(-0.012345, 1, "0.0")] 
        [TestCase(-0.012345, 0, "0")]
        public static void DisplayRoundedToDecimalPlaces(double value, int digits, string expectedResult)
        {
            Assert.AreEqual(expectedResult, Numbers.DisplayRoundedToDecimalPlaces(value, digits));
        }
        #endregion
    }
}
