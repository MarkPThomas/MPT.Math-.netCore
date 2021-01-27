using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics.LinearCurves;
using NUnit.Framework;

namespace MPT.Math.UnitTests.Curves.Parametrics.Components
{
    [TestFixture]
    public static class CartesianParametricEquationXYTests
    {
        [Test]
        public static void Clone()
        {
            double position = 1;
            double xExpected = 5;
            double yExpected = 4;
            LinearCurve curve = new LinearCurve(new CartesianCoordinate(1, 2), new CartesianCoordinate(5, 4));
            LinearCurveParametric parametric = new LinearCurveParametric(curve);

            double xValue = parametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(xExpected, xValue);

            double yValue = parametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(yExpected, yValue);

            CartesianParametricEquationXY parametricClone = parametric.Clone() as CartesianParametricEquationXY;

            double xValueClone = parametricClone.Xcomponent.ValueAt(position);
            Assert.AreEqual(xExpected, xValueClone);

            double yValueClone = parametricClone.Ycomponent.ValueAt(position);
            Assert.AreEqual(yExpected, yValueClone);

        }

        [Test]
        public static void CloneParametric()
        {
            double position = 1;
            double xExpected = 5;
            double yExpected = 4;
            LinearCurve curve = new LinearCurve(new CartesianCoordinate(1, 2), new CartesianCoordinate(5, 4));
            LinearCurveParametric parametric = new LinearCurveParametric(curve);

            double xValue = parametric.Xcomponent.ValueAt(position);
            Assert.AreEqual(xExpected, xValue);

            double yValue = parametric.Ycomponent.ValueAt(position);
            Assert.AreEqual(yExpected, yValue);

            CartesianParametricEquationXY parametricClone = parametric.CloneParametric();

            double xValueClone = parametricClone.Xcomponent.ValueAt(position);
            Assert.AreEqual(xExpected, xValueClone);

            double yValueClone = parametricClone.Ycomponent.ValueAt(position);
            Assert.AreEqual(yExpected, yValueClone);

        }
    }
}
