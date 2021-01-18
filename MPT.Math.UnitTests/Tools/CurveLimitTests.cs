using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Curves.Parametrics;
using MPT.Math.Curves.Tools;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Tools
{
    [TestFixture]
    public static class CurveLimitTests
    {
        public static double Tolerance = 0.00001;

        public static Curve curve;
        public static CurveLimit curveLimit;

        [SetUp]
        public static void Initialize()
        {
            curve = new LinearCurve(new CartesianCoordinate(-1, -2), new CartesianCoordinate(4, 3));
            curve.Tolerance = Tolerance;
            curveLimit = new CurveLimit(curve);
        }

        #region Initialization
        [Test]
        public static void Initialization()
        {
            CurveLimit curveLimitInitialize = new CurveLimit(curve);

            Assert.AreEqual(CartesianCoordinate.Origin().X, curveLimitInitialize.Limit.X);
            Assert.AreEqual(CartesianCoordinate.Origin().Y, curveLimitInitialize.Limit.Y);
        }
        #endregion

        #region Methods        
        [TestCase(2, 2, 1)]
        public static void SetLimitByX(double xCoordinate, double xLimitExpected, double yLimitExpected)
        {
            curveLimit.SetLimitByX(xCoordinate);

            CartesianCoordinate limitExpected = new CartesianCoordinate(xLimitExpected, yLimitExpected, Tolerance);

            Assert.AreEqual(limitExpected, curveLimit.Limit);
        }

        [Test]
        public static void SetLimitByX_Throws_NotSupportedException_if_Curve_Not_In_Cartesian_Coordinates()
        {
            CurveLimit nonCartesianLimit = new CurveLimit(new NonCartesianCurve());
            Assert.Throws<NotSupportedException>(() => nonCartesianLimit.SetLimitByX(1));
        }


        [TestCase(1, 2, 1)]
        public static void SetLimitByY(double yCoordinate, double xLimitExpected, double yLimitExpected)
        {
            curveLimit.SetLimitByY(yCoordinate);

            CartesianCoordinate limitExpected = new CartesianCoordinate(xLimitExpected, yLimitExpected, Tolerance);

            Assert.AreEqual(limitExpected, curveLimit.Limit);
        }

        [Test]
        public static void SetLimitByY_Throws_NotSupportedException_if_Curve_Not_In_Cartesian_Coordinates()
        {
            CurveLimit nonCartesianLimit = new CurveLimit(new NonCartesianCurve());
            Assert.Throws<NotSupportedException>(() => nonCartesianLimit.SetLimitByY(1));
        }

        [Test]
        public static void SetLimitByRotation()
        {
            curveLimit.SetLimitByRotation(-Numbers.PiOver2);

            CartesianCoordinate limitExpected = new CartesianCoordinate(0, -1, Tolerance);

            Assert.AreEqual(limitExpected, curveLimit.Limit);
        }

        [Test]
        public static void SetLimitByRotation_Throws_NotSupportedException_if_Curve_Not_In_Polar_Coordinates()
        {
            CurveLimit nonPolarLimit = new CurveLimit(new NonPolarCurve());
            Assert.Throws<NotSupportedException>(() => nonPolarLimit.SetLimitByRotation(1));
        }

        [TestCase(2, 1, 2, 1)]
        public static void SetLimitByCoordinate(double xCoordinate, double yCoordinate, double xLimitExpected, double yLimitExpected)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(xCoordinate, yCoordinate);
            curveLimit.SetLimitByCoordinate(coordinate);

            CartesianCoordinate limitExpected = new CartesianCoordinate(xLimitExpected, yLimitExpected, Tolerance);

            Assert.AreEqual(limitExpected, curveLimit.Limit);
        }

        [Test]
        public static void SetLimitByCoordinate_Throws_NotSupportedException_if_Curve_Not_In_Cartesian_Coordinates()
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(1, 2);
            CurveLimit nonCartesianLimit = new CurveLimit(new NonCartesianCurve());

            Assert.Throws<NotSupportedException>(() => nonCartesianLimit.SetLimitByCoordinate(coordinate));
        }

        [Test]
        public static void SetLimitByCoordinate_Throws_ArgumentOutOfRangeException_if_Coordinate_Does_Not_Lie_On_Curve()
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(1, 2);

            Assert.Throws<ArgumentOutOfRangeException>(() => curveLimit.SetLimitByCoordinate(coordinate));
        }

        [Test]
        public static void LimitPolar_Returns_Limit_As_Polar_Coordinate()
        {
            curveLimit.SetLimitByX(2);
            PolarCoordinate limit = curveLimit.LimitPolar();

            PolarCoordinate expectedLimit = new PolarCoordinate(2.236068, 0.463648, Tolerance);

            Assert.AreEqual(expectedLimit, limit);
        }
        #endregion

        #region Static      
        [Test]
        public static void GetLimitByX()
        {
            double xCoordinate = 2;
            ICurvePositionCartesian curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));
            CartesianCoordinate limit = CurveLimit.GetLimitByX(xCoordinate, curve);

            CartesianCoordinate expectedLimit = new CartesianCoordinate(xCoordinate, 1);

            Assert.AreEqual(expectedLimit, limit);
        }

        [Test]
        public static void GetLimitByY()
        {
            double yCoordinate = 1;
            ICurvePositionCartesian curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));
            CartesianCoordinate limit = CurveLimit.GetLimitByY(yCoordinate, curve);

            CartesianCoordinate expectedLimit = new CartesianCoordinate(2, yCoordinate, Tolerance);

            Assert.AreEqual(expectedLimit, limit);
        }

        [Test]
        public static void GetLimitByRotation()
        {
            Curve curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));
            CartesianCoordinate limit = CurveLimit.GetLimitByRotation(-Numbers.PiOver2, curve);

            CartesianCoordinate expectedLimit = new CartesianCoordinate(0, -1, Tolerance);

            Assert.AreEqual(expectedLimit, limit);
        }

        [Test]
        public static void GetLimitByCoordinate()
        {
            CartesianCoordinate expectedLimit = new CartesianCoordinate(2, 1, Tolerance);
            ICurvePositionCartesian curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));

            CartesianCoordinate limit = CurveLimit.GetLimitByCoordinate(expectedLimit, curve);

            Assert.AreEqual(expectedLimit, limit);
        }

        [Test]
        public static void GetLimitByCoordinate_Throws_ArgumentOutOfRangeException_if_Coordinate_Does_Not_Lie_On_Curve()
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(2, 2);
            ICurvePositionCartesian curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));

            Assert.Throws<ArgumentOutOfRangeException>(() => CurveLimit.GetLimitByCoordinate(coordinate, curve));
        }
        #endregion


        #region ICloneable
        [Test]
        public static void Clone()
        {
            Curve curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));
            CurveLimit curveLimit = new CurveLimit(curve);
            curveLimit.SetLimitByX(2);

            CurveLimit curveLimitClone = curveLimit.Clone() as CurveLimit;

            Assert.AreEqual(curveLimit.Limit, curveLimitClone.Limit);
        }

        [Test]
        public static void CloneLimit()
        {
            Curve curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));
            CurveLimit curveLimit = new CurveLimit(curve);
            curveLimit.SetLimitByX(2);

            CurveLimit curveLimitClone = curveLimit.CloneLimit();

            Assert.AreEqual(curveLimit.Limit, curveLimitClone.Limit);
        }
        #endregion
    }

    public class NonCartesianCurve : Curve
    {
        public override object Clone()
        {
            throw new NotImplementedException();
        }

        protected override LinearParametricEquation createParametricEquation()
        {
            throw new NotImplementedException();
        }
    }

    public class NonPolarCurve : Curve
    {
        public override object Clone()
        {
            throw new NotImplementedException();
        }

        protected override LinearParametricEquation createParametricEquation()
        {
            throw new NotImplementedException();
        }
    }
}
