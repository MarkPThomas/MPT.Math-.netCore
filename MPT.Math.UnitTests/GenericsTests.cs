using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests
{
    [TestFixture]
    public static class GenericsTests
    {
        #region Properties
        [Test]
        public static void GetTolerance_of_Single_Object()
        {
            double tolerance = 0.1;
            ObjectWithTolerance item1 = new ObjectWithTolerance() { Tolerance = tolerance };
            Assert.AreEqual(tolerance, Generics.GetTolerance(item1));
        }

        [Test]
        public static void GetTolerance_Between_Single_Object_and_Specified_Governing_Tolerance()
        {
            double tolerance = 0.0001;
            ObjectWithTolerance item1 = new ObjectWithTolerance() { Tolerance = tolerance };
            double governingTolerance = 0.1;
            Assert.AreEqual(governingTolerance, Generics.GetTolerance(item1, governingTolerance));
        }

        [Test]
        public static void GetTolerance_Between_Two_Objects_of_Same_Type()
        {
            double tolerance1 = 0.1;
            ObjectWithTolerance item1 = new ObjectWithTolerance() { Tolerance = tolerance1 };
            double tolerance2 = 0.2;
            ObjectWithTolerance item2 = new ObjectWithTolerance() { Tolerance = tolerance2 };
            Assert.AreEqual(tolerance2, Generics.GetTolerance(item1, item2));
        }

        [Test]
        public static void GetTolerance_Between_Two_Objects_of_Different_Type()
        {
            double tolerance1 = 0.1;
            ObjectWithTolerance item1 = new ObjectWithTolerance() { Tolerance = tolerance1 };
            double tolerance2 = 0.2;
            AnotherObjectWithTolerance item2 = new AnotherObjectWithTolerance() { Tolerance = tolerance2 };
            Assert.AreEqual(tolerance2, Generics.GetTolerance(item1, item2));
        }

        [Test]
        public static void GetTolerance_Between_Two_Objects_of_Different_Type_and_Specified_Governing_Tolerance()
        {
            double tolerance1 = 0.01;
            ObjectWithTolerance item1 = new ObjectWithTolerance() { Tolerance = tolerance1 };
            double tolerance2 = 0.02;
            AnotherObjectWithTolerance item2 = new AnotherObjectWithTolerance() { Tolerance = tolerance2 };
            double governingTolerance = 0.1;
            Assert.AreEqual(governingTolerance, Generics.GetTolerance(item1, item2, governingTolerance));
        }
        #endregion

        #region Comparisons
        [TestCase(2, 1, 3, ExpectedResult = true)]
        [TestCase(1, 1, 3, ExpectedResult = true)]
        [TestCase(1, 3, 1, ExpectedResult = true)]
        [TestCase(0.999, 1, 3, ExpectedResult = false)]
        [TestCase(0.999, 3, 1, ExpectedResult = false)]
        [TestCase(3, 1, 3, ExpectedResult = true)]
        [TestCase(3, 3, 1, ExpectedResult = true)]
        [TestCase(3.001, 1, 3, ExpectedResult = false)]
        [TestCase(3.001, 3, 1, ExpectedResult = false)]
        public static bool IsWithinInclusive(double value, double value1, double value2)
        {
            Comparables comparablesValue1 = new Comparables(value1);
            Comparables comparablesValue = new Comparables(value);
            Comparables comparablesValue2 = new Comparables(value2);

            return Generics.IsWithinInclusive(comparablesValue, comparablesValue1, comparablesValue2);
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
        public static bool IsWithinExclusive(double value, double value1, double value2)
        {
            Comparables comparablesValue1 = new Comparables(value1);
            Comparables comparablesValue = new Comparables(value);
            Comparables comparablesValue2 = new Comparables(value2);

            return Generics.IsWithinExclusive(comparablesValue, comparablesValue1, comparablesValue2);
        }

        [Test]
        public static void Max_Throws_Exception_If_Argument_Is_Null()
        {
            Assert.Throws<ArgumentException>(() => Generics.Max<Comparables>(null));
        }

        [Test]
        public static void Max_Throws_Exception_If_Array_Is_Not_Dimensioned()
        {
            Comparables[] comparables = new Comparables[0];
            Assert.Throws<ArgumentException>(() => Generics.Max(comparables));
        }

        [Test]
        public static void Max_Returns_Max_Object_Of_Comparable_Objects()
        {
            Comparables[] comparables = new Comparables[3];
            comparables[0] = new Comparables(6);
            comparables[1] = new Comparables(-1);
            comparables[2] = new Comparables(9);

            Comparables maxComparables = Generics.Max(comparables);
            Assert.AreEqual(comparables[2], maxComparables);

            // An alternative form of inputs
            maxComparables = Generics.Max(comparables[0], comparables[1], comparables[2]);
            Assert.AreEqual(comparables[2], maxComparables);
        }

        [Test]
        public static void Min_Throws_Exception_If_Argument_Is_Null()
        {
            Assert.Throws<ArgumentException>(() => Generics.Min<Comparables>(null));
        }

        [Test]
        public static void Min_Throws_Exception_If_Array_Is_Not_Dimensioned()
        {
            Comparables[] comparables = new Comparables[0];
            Assert.Throws<ArgumentException>(() => Generics.Min(comparables));
        }

        [Test]
        public static void Min_Returns_Min_Object_Of_Comparable_Objects()
        {
            Comparables[] comparables = new Comparables[3];
            comparables[0] = new Comparables(6);
            comparables[1] = new Comparables(-1);
            comparables[2] = new Comparables(9);

            Comparables minComparables = Generics.Min(comparables);
            Assert.AreEqual(comparables[1], minComparables);

            // An alternative form of inputs
            minComparables = Generics.Min(comparables[0], comparables[1], comparables[2]);
            Assert.AreEqual(comparables[1], minComparables);
        }
        #endregion
    }

    public class ObjectWithTolerance : ITolerance
    {
        public double Tolerance { get; set; }
    }
    public class AnotherObjectWithTolerance : ITolerance
    {
        public double Tolerance { get; set; }
    }

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
}
