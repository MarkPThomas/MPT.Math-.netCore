using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Curves.Parametrics;
using MPT.Math.Curves.Parametrics.ConicSectionCurves.Elliptics;
using MPT.Math.Curves.Parametrics.Linear;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Curves.Parametrics.ConicSectionCurves.Elliptics
{
    [TestFixture]
    public static class EllipticParametricTests
    {
        public static CircularCurve curveCircular;
        public static EllipticalCurve curveElliptical;
        public static EllipticParametric parametricCircular;
        public static EllipticParametric parametricElliptical;

        //[SetUp]
        //public static void SetUp()
        //{
        //    curveCircular = new CircularCurve(6, new CartesianCoordinate(4, 5));
        //    parametricCircular = new EllipticParametric(curveCircular);

        //    curveElliptical = new EllipticalCurve(3, 12, new CartesianCoordinate(4, 5), Angle.Origin());
        //    parametricElliptical = new EllipticParametric(curveElliptical);
        //}

        //[TestCase(0, 1, 2)]
        //[TestCase(0.5, 3, 3)]
        //[TestCase(1, 5, 4)]
        //public static void BaseByParameter_Value_at_Position_Circular(double position, double expectedValueX, double expectedValueY)
        //{
        //    double xValue = parametricCircular.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);

        //    double yValue = parametricCircular.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);
        //}

        //[TestCase(0, 4, 2)]
        //[TestCase(0.5, 4, 2)]
        //[TestCase(1, 4, 2)]
        //public static void PrimeByParameter_Value_at_Position_Circular(double position, double expectedValueX, double expectedValueY)
        //{
        //    Assert.IsTrue(parametricCircular.Xcomponent.HasDifferential());
        //    double xValue = parametricCircular.Xcomponent.Differential.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);

        //    Assert.IsTrue(parametricCircular.Ycomponent.HasDifferential());
        //    double yValue = parametricCircular.Ycomponent.Differential.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);
        //}

        //[TestCase(0, 0, 0)]
        //[TestCase(0.5, 0, 0)]
        //[TestCase(1, 0, 0)]
        //public static void PrimeDoubleByParameter_Value_at_Position_Circular(double position, double expectedValueX, double expectedValueY)
        //{
        //    Assert.IsTrue(parametricCircular.Xcomponent.Differential.HasDifferential());
        //    double xValue = parametricCircular.Xcomponent.Differential.Differential.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);

        //    Assert.IsTrue(parametricCircular.Ycomponent.Differential.HasDifferential());
        //    double yValue = parametricCircular.Ycomponent.Differential.Differential.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);
        //}

        //[Test]
        //public static void HasDifferential()
        //{
        //    Assert.IsTrue(parametricCircular.HasDifferential());
        //}


        //[TestCase(0, 1, 2)]
        //[TestCase(0.5, 3, 3)]
        //[TestCase(1, 5, 4)]
        //public static void DifferentiateBy_0_Returns_Base_Function_Circular(double position, double expectedValueX, double expectedValueY)
        //{
        //    LinearParametricEquation parametricCircularDifferential = parametricCircular.DifferentiateBy(0);
        //    double xValue = parametricCircularDifferential.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);
        //    double yValue = parametricCircularDifferential.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);
        //}

        //[TestCase(1, 0, 4, 2)]
        //[TestCase(1, 0.5, 4, 2)]
        //[TestCase(1, 1, 4, 2)]
        //[TestCase(2, 0, 0, 0)]
        //[TestCase(2, 0.5, 0, 0)]
        //[TestCase(2, 1, 0, 0)]
        //public static void DifferentiateBy_Differentiates_Equations_by_Specified_Number_Circular(
        //    int differentialNumber, double position,
        //    double expectedValueX, double expectedValueY)
        //{
        //    LinearParametricEquation parametricCircularDifferential = parametricCircular.DifferentiateBy(differentialNumber);
        //    double xValue = parametricCircularDifferential.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);
        //    double yValue = parametricCircularDifferential.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);
        //}



        //[TestCase(0, 4, 2)]
        //[TestCase(0.5, 4, 2)]
        //[TestCase(1, 4, 2)]
        //public static void DifferentialFirst_Circular(double position, double expectedValueX, double expectedValueY)
        //{
        //    LinearParametricEquation parametricCircularPrimeFirst = parametricCircular.DifferentialFirst();

        //    double xValue = parametricCircularPrimeFirst.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);
        //    double yValue = parametricCircularPrimeFirst.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);
        //}

        //[TestCase(0, 0, 0)]
        //[TestCase(0.5, 0, 0)]
        //[TestCase(1, 0, 0)]
        //public static void DifferentialSecond_Circular(double position, double expectedValueX, double expectedValueY)
        //{
        //    LinearParametricEquation parametricCircularPrimeSecond = parametricCircular.DifferentialSecond();

        //    double xValue = parametricCircularPrimeSecond.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);
        //    double yValue = parametricCircularPrimeSecond.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);

        //    // No third differential
        //    Assert.IsFalse(parametricCircularPrimeSecond.HasDifferential());
        //}

        //[TestCase(0, 1, 2, 4, 2, 0, 0)]
        //[TestCase(0.5, 3, 3, 4, 2, 0, 0)]
        //[TestCase(1, 5, 4, 4, 2, 0, 0)]
        //public static void Differentiate_Differentiates_Equations_Circular(double position,
        //    double expectedX, double expectedY,
        //    double expectedXPrime, double expectedYPrime,
        //    double expectedXPrimePrime, double expectedYPrimePrime)
        //{
        //    // Base function
        //    double xValue = parametricCircular.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedX, xValue);
        //    double yValue = parametricCircular.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedY, yValue);

        //    // First differential
        //    Assert.IsTrue(parametricCircular.HasDifferential());
        //    LinearParametricEquation parametricCircularPrime = parametricCircular.Differentiate();
        //    double xValuePrime = parametricCircularPrime.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedXPrime, xValuePrime);
        //    double yValuePrime = parametricCircularPrime.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedYPrime, yValuePrime);

        //    // Second differential
        //    Assert.IsTrue(parametricCircularPrime.HasDifferential());
        //    LinearParametricEquation parametricCircularPrimePrime = parametricCircularPrime.Differentiate();
        //    double xValuePrimePrime = parametricCircularPrimePrime.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedXPrimePrime, xValuePrimePrime);
        //    double yValuePrimePrime = parametricCircularPrimePrime.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedYPrimePrime, yValuePrimePrime);

        //    // No third differential
        //    Assert.IsFalse(parametricCircularPrimePrime.HasDifferential());
        //}

        //[Test]
        //public static void DifferentiateBy_Throws_ArgumentOutOfRangeException_if_Index_is_Negative_Circular()
        //{
        //    Assert.Throws<ArgumentOutOfRangeException>(() => parametricCircular.DifferentiateBy(-1));
        //}

        //[Test]
        //public static void DifferentiateBy_Throws_ArgumentOutOfRangeException_if_Index_is_Less_Than_Current_Differentiation_Index_Circular()
        //{
        //    LinearParametricEquation parametricCircularPrimePrime = parametricCircular.DifferentiateBy(2);
        //    Assert.Throws<ArgumentOutOfRangeException>(() => parametricCircularPrimePrime.DifferentiateBy(1));
        //}

        //[Test]
        //public static void DifferentiateBy_Throws_ArgumentOutOfRangeException_if_Index_is_Greater_Than_Differentiations_Available_Circular()
        //{
        //    Assert.Throws<ArgumentOutOfRangeException>(() => parametricCircular.DifferentiateBy(3));
        //}

        //[Test]
        //public static void Differentiate_Throws_ArgumentOutOfRangeException_if_Index_is_Greater_Than_Differentiations_Available_Circular()
        //{
        //    LinearParametricEquation parametricCircularPrimePrime = parametricCircular.DifferentiateBy(2);
        //    Assert.Throws<ArgumentOutOfRangeException>(() => parametricCircularPrimePrime.Differentiate());
        //}

        //[TestCase(0, 1, 2)]
        //[TestCase(0.5, 3, 3)]
        //[TestCase(1, 5, 4)]
        //public static void Multiplied_by_Constant_Scales_Values_Up_Circular(double position, double expectedValueX, double expectedValueY)
        //{
        //    double scale = 2;

        //    double yValue = parametricCircular.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);

        //    double xValue = parametricCircular.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);

        //    LinearParametricEquation linearParametric = parametricCircular.DifferentiateBy(0) * scale;

        //    double xValueScaled = linearParametric.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX * scale, xValueScaled);

        //    double yValueScaled = linearParametric.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY * scale, yValueScaled);
        //}

        //[TestCase(0, 1, 2)]
        //[TestCase(0.5, 3, 3)]
        //[TestCase(1, 5, 4)]
        //public static void Divided_by_Constant_Scales_Values_Down_Circular(double position, double expectedValueX, double expectedValueY)
        //{
        //    double scale = 2;

        //    double yValue = parametricCircular.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY, yValue);

        //    double xValue = parametricCircular.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX, xValue);

        //    LinearParametricEquation linearParametric = parametricCircular.DifferentiateBy(0) / scale;

        //    double xValueScaled = linearParametric.Xcomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueX / scale, xValueScaled);

        //    double yValueScaled = linearParametric.Ycomponent.ValueAt(position);
        //    Assert.AreEqual(expectedValueY / scale, yValueScaled);
        //}
    }
}
