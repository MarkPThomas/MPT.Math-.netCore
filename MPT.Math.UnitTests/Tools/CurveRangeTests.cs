using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Curves.Tools;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Tools
{
    [TestFixture]
    public static class CurveRangeTests
    {
        public static double Tolerance = 0.00001;

        public static CurveRange RangeWithLimits;

        [SetUp]
        public static void InintializeCurve()
        {
            Curve curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));
            curve.Tolerance = Tolerance;
            RangeWithLimits = new CurveRange(curve);
            RangeWithLimits.Start.SetLimitByX(-0.5);
            RangeWithLimits.End.SetLimitByX(2);
        }

        #region Initialize
        [Test]
        public static void Initialization()
        {
            Curve curve = new LinearCurve(new CartesianCoordinate(-1, -2, Tolerance), new CartesianCoordinate(4, 3, Tolerance));
            curve.Tolerance = Tolerance;
            CurveRange range = new CurveRange(curve);

            Assert.AreEqual(CartesianCoordinate.Origin(), range.Start.Limit);
            Assert.AreEqual(CartesianCoordinate.Origin(), range.End.Limit);
        }
        #endregion

        #region Methods         
        [Test]
        public static void ToString_Overrides_ToString()
        {
            Assert.AreEqual("MPT.Math.Curves.Tools.CurveRange - Start: {X: -0.5, Y: -1.5}, End: {X: 2, Y: 1}", RangeWithLimits.ToString());
        }

        [Test]
        public static void ToOffset()
        {
            CartesianOffset offset = RangeWithLimits.ToOffset();
            CartesianOffset offsetExpected = new CartesianOffset(new CartesianCoordinate(-0.5, -1.5), new CartesianCoordinate(2, 1), Tolerance);
            Assert.AreEqual(offsetExpected, offset);
        }

        [Test]
        public static void ToOffsetPolar()
        {
            PolarOffset polarOffset = RangeWithLimits.ToOffsetPolar();
            PolarOffset offsetExpected = new PolarOffset(new CartesianCoordinate(-0.5, -1.5), new CartesianCoordinate(2, 1), Tolerance);
            Assert.AreEqual(offsetExpected, polarOffset);
        }

        [Test]
        public static void LengthLinear()
        {
            double lengthLinear = RangeWithLimits.LengthLinear();
            double expectedLength = 3.535534;

            Assert.AreEqual(expectedLength, lengthLinear, Tolerance);
        }

        [Test]
        public static void LengthX()
        {
            double lengthX = RangeWithLimits.LengthX();
            double expectedLength = 2.5;

            Assert.AreEqual(expectedLength, lengthX, Tolerance);
        }

        [Test]
        public static void LengthY()
        {
            double lengthY = RangeWithLimits.LengthY();
            double expectedLength = 2.5;

            Assert.AreEqual(expectedLength, lengthY, Tolerance);
        }

        [Test]
        public static void LengthRadius()
        {
            double lengthRadius = RangeWithLimits.LengthRadius();
            double expectedLength = 0.654929;

            Assert.AreEqual(expectedLength, lengthRadius, Tolerance);
        }

        [Test]
        public static void LengthRotation()
        {
            Angle lengthRotation = RangeWithLimits.LengthRotation();
            double expectedLengthDegrees = 135;

            Assert.AreEqual(expectedLengthDegrees, lengthRotation.Degrees, Tolerance);
        }

        [Test]
        public static void LengthRotationRadians()
        {
            double lengthRotationRadians = RangeWithLimits.LengthRotationRadians();
            double expectedLengthRadians = 3 * Numbers.PiOver4;

            Assert.AreEqual(expectedLengthRadians, lengthRotationRadians, Tolerance);
        }

        [Test]
        public static void LengthRotationDegrees()
        {
            double lengthRotationDegrees = RangeWithLimits.LengthRotationDegrees();
            double expectedLengthDegrees = 135;

            Assert.AreEqual(expectedLengthDegrees, lengthRotationDegrees, Tolerance);
        }
        #endregion

        #region Static
        [TestCase(Numbers.Pi)]
        [TestCase(-Numbers.Pi)]
        [TestCase(Numbers.PiOver2)]
        [TestCase(-Numbers.PiOver2)]
        [TestCase(0)]
        public static void ValidateRangeLimitRotationalHalfCirclePosition_Does_Nothing_if_Position_Inside_of_Positive_Negative_Half_Rotation(double position)
        {
            CurveRange.ValidateRangeLimitRotationalHalfCirclePosition(position, Tolerance);
            Assert.IsTrue(true);
        }

        [TestCase(1.1 * Numbers.Pi)]
        [TestCase(-1.1 * Numbers.Pi)]
        public static void ValidateRangeLimitRotationalHalfCirclePosition_Throws_ArgumentOutOfRangeException_if_Position_Outside_of_Positive_Negative_Half_Rotation(double position)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CurveRange.ValidateRangeLimitRotationalHalfCirclePosition(position, Tolerance));
        }

        [TestCase(0)]
        [TestCase(Numbers.PiOver2)]
        [TestCase(Numbers.Pi)]
        [TestCase(Numbers.TwoPi)]
        public static void ValidateRangeLimitRotationalFullCirclePosition_Does_Nothing_if_Position_Inside_of_Single_Positive_Rotation(double position)
        {
            CurveRange.ValidateRangeLimitRotationalFullCirclePosition(position, Tolerance);
            Assert.IsTrue(true);
        }

        [TestCase(-Numbers.Pi)]
        [TestCase(-Numbers.PiOver2)]
        [TestCase(1.1 * Numbers.TwoPi)]
        [TestCase(-0.1)]
        public static void ValidateRangeLimitRotationalFullCirclePosition_Throws_ArgumentOutOfRangeException_if_Position_Outside_of_Single_Positive_Rotation(double position)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CurveRange.ValidateRangeLimitRotationalFullCirclePosition(position, Tolerance));
        }
        #endregion

        #region ICloneable
        [Test]
        public static void Clone()
        {
            CartesianCoordinate startCoord = new CartesianCoordinate(-0.5, -1.5, Tolerance);
            CartesianCoordinate endCoord = new CartesianCoordinate(2, 1, Tolerance);

            Assert.AreEqual(startCoord, RangeWithLimits.Start.Limit);
            Assert.AreEqual(endCoord, RangeWithLimits.End.Limit);

            CurveRange rangeClone = RangeWithLimits.Clone() as CurveRange;

            Assert.AreEqual(startCoord, rangeClone.Start.Limit);
            Assert.AreEqual(endCoord, rangeClone.End.Limit);
        }
        
        [Test]
        public static void CloneRange()
        {
            CartesianCoordinate startCoord = new CartesianCoordinate(-0.5, -1.5, Tolerance);
            CartesianCoordinate endCoord = new CartesianCoordinate(2, 1, Tolerance);

            Assert.AreEqual(startCoord, RangeWithLimits.Start.Limit);
            Assert.AreEqual(endCoord, RangeWithLimits.End.Limit);

            CurveRange rangeClone = RangeWithLimits.CloneRange();

            Assert.AreEqual(startCoord, rangeClone.Start.Limit);
            Assert.AreEqual(endCoord, rangeClone.End.Limit);
        }
        #endregion
    }
}
