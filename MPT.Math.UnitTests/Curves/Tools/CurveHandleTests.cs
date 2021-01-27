using MPT.Math.Coordinates;
using MPT.Math.Curves.Tools;
using NUnit.Framework;

namespace MPT.Math.UnitTests.Tools
{
    [TestFixture]
    public static class CurveHandleTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization        
        [Test]
        public static void Initialize_with_Control_Point_and_Radius()
        {
            CartesianCoordinate controlPoint = new CartesianCoordinate(2, 5);
            double radius = 5;
            CurveHandle curveHandle = new CurveHandle(controlPoint, radius);

            Assert.AreEqual(controlPoint, curveHandle.ControlPoint);
            Assert.AreEqual(radius, curveHandle.Radius);
            Assert.AreEqual(Angle.Origin(), curveHandle.Rotation);
        }

        [Test]
        public static void Initialize_with_Control_Point_and_Radius_and_Rotation()
        {
            CartesianCoordinate controlPoint = new CartesianCoordinate(2, 5);
            double radius = 5;
            Angle rotation = new Angle(Numbers.PiOver4);
            CurveHandle curveHandle = new CurveHandle(controlPoint, radius, rotation);

            Assert.AreEqual(controlPoint, curveHandle.ControlPoint);
            Assert.AreEqual(radius, curveHandle.Radius);
            Assert.AreEqual(rotation, curveHandle.Rotation);
        }
        #endregion

        #region Methods: Public   
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            CartesianCoordinate controlPoint = new CartesianCoordinate(2, 5);
            double radius = 5;
            Angle rotation = new Angle(Numbers.PiOver4);
            CurveHandle curveHandle = new CurveHandle(controlPoint, radius, rotation);

            Assert.AreEqual("MPT.Math.Curves.Tools.CurveHandle - Center: {X: 2, Y: 5} - Radius: 5, Rotation: 45 deg", curveHandle.ToString());
        }

        [Test]
        public static void GetHandleTip()
        {
            double tolerance = 0.0001;
            CartesianCoordinate controlPoint = new CartesianCoordinate(2, 5, tolerance);
            double radius = 5;
            Angle rotation = new Angle(Numbers.PiOver4);
            CurveHandle curveHandle = new CurveHandle(controlPoint, radius, rotation);

            CartesianCoordinate expectedHandleTip = new CartesianCoordinate(5.53553, 8.53553, tolerance);
            CartesianCoordinate handleTip = curveHandle.GetHandleTip();

            Assert.AreEqual(expectedHandleTip, handleTip);
        }

        [Test]
        public static void SetHandleTip()
        {
            double tolerance = 0.0001;

            CartesianCoordinate controlPoint = new CartesianCoordinate(2, 5, tolerance);
            double radius = 5;
            Angle rotation = new Angle(Numbers.PiOver4);
            CurveHandle curveHandle = new CurveHandle(controlPoint, radius, rotation);

            Assert.AreEqual(controlPoint, curveHandle.ControlPoint);
            Assert.AreEqual(radius, curveHandle.Radius);
            Assert.AreEqual(rotation, curveHandle.Rotation);

            CartesianCoordinate expectedHandleTip = new CartesianCoordinate(5.53553, 8.53553, tolerance);
            CartesianCoordinate handleTip = curveHandle.GetHandleTip();

            Assert.AreEqual(expectedHandleTip, handleTip);

            CartesianCoordinate newHandleTipExpected = new CartesianCoordinate(2, 7, tolerance);
            curveHandle.SetHandleTip(newHandleTipExpected);
            CartesianCoordinate newHandleTip = curveHandle.GetHandleTip();

            Assert.AreEqual(controlPoint, curveHandle.ControlPoint);
            Assert.AreEqual(2, curveHandle.Radius);
            Assert.AreEqual(Numbers.PiOver2, curveHandle.Rotation.Radians, Tolerance);

            Assert.AreEqual(newHandleTipExpected, newHandleTip);
        }
        #endregion

        #region ICloneable
        [Test]
        public static void Clone()
        {
            CartesianCoordinate controlPoint = new CartesianCoordinate(2, 5);
            double radius = 5;
            Angle rotation = new Angle(Numbers.PiOver4);
            CurveHandle curveHandle = new CurveHandle(controlPoint, radius, rotation);

            Assert.AreEqual(controlPoint, curveHandle.ControlPoint);
            Assert.AreEqual(radius, curveHandle.Radius);
            Assert.AreEqual(rotation, curveHandle.Rotation);

            CurveHandle clonedCurveHandle = curveHandle.Clone() as CurveHandle;

            Assert.AreEqual(controlPoint, clonedCurveHandle.ControlPoint);
            Assert.AreEqual(radius, clonedCurveHandle.Radius);
            Assert.AreEqual(rotation, clonedCurveHandle.Rotation);
        }

        [Test]
        public static void CloneCurve()
        {
            CartesianCoordinate controlPoint = new CartesianCoordinate(2, 5);
            double radius = 5;
            Angle rotation = new Angle(Numbers.PiOver4);
            CurveHandle curveHandle = new CurveHandle(controlPoint, radius, rotation);

            Assert.AreEqual(controlPoint, curveHandle.ControlPoint);
            Assert.AreEqual(radius, curveHandle.Radius);
            Assert.AreEqual(rotation, curveHandle.Rotation);

            CurveHandle clonedCurveHandle = curveHandle.CloneCurve();

            Assert.AreEqual(controlPoint, clonedCurveHandle.ControlPoint);
            Assert.AreEqual(radius, clonedCurveHandle.Radius);
            Assert.AreEqual(rotation, clonedCurveHandle.Rotation);
        }
        #endregion
    }
}
