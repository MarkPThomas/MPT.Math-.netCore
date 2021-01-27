using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Curves.Parametrics.LinearCurves
{
    [TestFixture]
    public static class LinearCurveParametricTests
    {
        public static LinearCurve curve;
        public static LinearCurveParametric parametric;

        [SetUp]
        public static void SetUp()
        {
            curve = new LinearCurve(new CartesianCoordinate(1, 2), new CartesianCoordinate(5, 4));
            parametric = new LinearCurveParametric(curve);
        }

        [TestCase(0, 1, 2)]
        [TestCase(0.5, 3, 3)]
        [TestCase(1, 5, 4)]
        public static void BaseByParameter_Value_at_Position(double position, double expectedValueX, double expectedValueY)
        {
            double xValue = parametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);

            double yValue = parametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);
        }

        [TestCase(0, 2, 2.5)]
        [TestCase(0.25, 2.5, 2.75)]
        [TestCase(0.5, 3, 3)]
        [TestCase(0.75, 3.5, 3.25)]
        [TestCase(1, 4, 3.5)]
        public static void BaseByParameter_Value_at_Position_with_Custom_Limits(double position, double expectedValueX, double expectedValueY)
        {
            // Set custom limits
            curve.Range.Start.SetLimitByX(2);
            curve.Range.End.SetLimitByX(4);

            double xValue = parametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);

            double yValue = parametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);
        }

        [TestCase(0, 4, 2)]
        [TestCase(0.5, 4, 2)]
        [TestCase(1, 4, 2)]
        public static void PrimeByParameter_Value_at_Position(double position, double expectedValueX, double expectedValueY)
        {
            Assert.IsTrue(parametric.Xcomponent.HasDifferential());
            double xValue = parametric.Xcomponent.Differential.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);

            Assert.IsTrue(parametric.Ycomponent.HasDifferential());
            double yValue = parametric.Ycomponent.Differential.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);
        }

        [TestCase(0, 0, 0)]
        [TestCase(0.5, 0, 0)]
        [TestCase(1, 0, 0)]
        public static void PrimeDoubleByParameter_Value_at_Position(double position, double expectedValueX, double expectedValueY)
        {
            Assert.IsTrue(parametric.Xcomponent.Differential.HasDifferential());
            double xValue = parametric.Xcomponent.Differential.Differential.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);

            Assert.IsTrue(parametric.Ycomponent.Differential.HasDifferential());
            double yValue = parametric.Ycomponent.Differential.Differential.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);
        }

        [Test]
        public static void HasDifferential()
        {
            Assert.IsTrue(parametric.HasDifferential());
        }


        [TestCase(0, 1, 2)]
        [TestCase(0.5, 3, 3)]
        [TestCase(1, 5, 4)]
        public static void DifferentiateBy_0_Returns_Base_Function(double position, double expectedValueX, double expectedValueY)
        {
            CartesianParametricEquationXY parametricDifferential = parametric.DifferentiateBy(0);
            double xValue = parametricDifferential.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);
            double yValue = parametricDifferential.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);
        }

        [TestCase(1, 0, 4, 2)]
        [TestCase(1, 0.5, 4, 2)]
        [TestCase(1, 1, 4, 2)]
        [TestCase(2, 0, 0, 0)]
        [TestCase(2, 0.5, 0, 0)]
        [TestCase(2, 1, 0, 0)]
        public static void DifferentiateBy_Differentiates_Equations_by_Specified_Number(
            int differentialNumber, double position, 
            double expectedValueX, double expectedValueY)
        {
            CartesianParametricEquationXY parametricDifferential = parametric.DifferentiateBy(differentialNumber);
            double xValue = parametricDifferential.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);
            double yValue = parametricDifferential.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);
        }



        [TestCase(0, 4, 2)]
        [TestCase(0.5, 4, 2)]
        [TestCase(1, 4, 2)]
        public static void DifferentialFirst(double position, double expectedValueX, double expectedValueY)
        {
            CartesianParametricEquationXY parametricPrimeFirst = parametric.DifferentialFirst();

            double xValue = parametricPrimeFirst.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);
            double yValue = parametricPrimeFirst.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);
        }

        [TestCase(0, 0, 0)]
        [TestCase(0.5, 0, 0)]
        [TestCase(1, 0, 0)]
        public static void DifferentialSecond(double position, double expectedValueX, double expectedValueY)
        {
            CartesianParametricEquationXY parametricPrimeSecond = parametric.DifferentialSecond();

            double xValue = parametricPrimeSecond.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);
            double yValue = parametricPrimeSecond.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);

            // No third differential
            Assert.IsFalse(parametricPrimeSecond.HasDifferential());
        }

        [TestCase(0, 1, 2, 4, 2, 0, 0)]
        [TestCase(0.5, 3, 3, 4, 2, 0, 0)]
        [TestCase(1, 5, 4, 4, 2, 0, 0)]
        public static void Differentiate_Differentiates_Equations(double position, 
            double expectedX, double expectedY,
            double expectedXPrime, double expectedYPrime,
            double expectedXPrimePrime, double expectedYPrimePrime)
        {
            // Base function
            double xValue = parametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedX, xValue);
            double yValue = parametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedY, yValue);

            // First differential
            Assert.IsTrue(parametric.HasDifferential());
            CartesianParametricEquationXY parametricPrime = parametric.Differentiate();
            double xValuePrime = parametricPrime.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedXPrime, xValuePrime);
            double yValuePrime = parametricPrime.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedYPrime, yValuePrime);

            // Second differential
            Assert.IsTrue(parametricPrime.HasDifferential());
            CartesianParametricEquationXY parametricPrimePrime = parametricPrime.Differentiate();
            double xValuePrimePrime = parametricPrimePrime.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedXPrimePrime, xValuePrimePrime);
            double yValuePrimePrime = parametricPrimePrime.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedYPrimePrime, yValuePrimePrime);

            // No third differential
            Assert.IsFalse(parametricPrimePrime.HasDifferential());
        }

        [Test]
        public static void DifferentiateBy_Throws_ArgumentOutOfRangeException_if_Index_is_Negative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => parametric.DifferentiateBy(-1));
        }

        [Test]
        public static void DifferentiateBy_Throws_ArgumentOutOfRangeException_if_Index_is_Less_Than_Current_Differentiation_Index()
        {
            CartesianParametricEquationXY parametricPrimePrime = parametric.DifferentiateBy(2);
            Assert.Throws<ArgumentOutOfRangeException>(() => parametricPrimePrime.DifferentiateBy(1));
        }

        [Test]
        public static void DifferentiateBy_Throws_ArgumentOutOfRangeException_if_Index_is_Greater_Than_Differentiations_Available()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => parametric.DifferentiateBy(3));
        }

        [Test]
        public static void Differentiate_Throws_ArgumentOutOfRangeException_if_Index_is_Greater_Than_Differentiations_Available()
        {
            CartesianParametricEquationXY parametricPrimePrime = parametric.DifferentiateBy(2);
            Assert.Throws<ArgumentOutOfRangeException>(() => parametricPrimePrime.Differentiate());
        }

        [TestCase(0, 1, 2)]
        [TestCase(0.5, 3, 3)]
        [TestCase(1, 5, 4)]
        public static void Multiplied_by_Constant_Scales_Values_Up(double position, double expectedValueX, double expectedValueY)
        {
            double scale = 2;

            double yValue = parametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);

            double xValue = parametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);

            CartesianParametricEquationXY linearParametric = parametric.DifferentiateBy(0) * scale;

            double xValueScaled = linearParametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX * scale, xValueScaled);

            double yValueScaled = linearParametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY * scale, yValueScaled);
        }

        [TestCase(0, 1, 2)]
        [TestCase(0.5, 3, 3)]
        [TestCase(1, 5, 4)]
        public static void Divided_by_Constant_Scales_Values_Down(double position, double expectedValueX, double expectedValueY)
        {
            double scale = 2;

            double yValue = parametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY, yValue);

            double xValue = parametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX, xValue);

            CartesianParametricEquationXY linearParametric = parametric.DifferentiateBy(0) / scale;

            double xValueScaled = linearParametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(expectedValueX / scale, xValueScaled);

            double yValueScaled = linearParametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(expectedValueY / scale, yValueScaled);
        }
    }
}
