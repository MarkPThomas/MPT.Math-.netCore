using MPT.Math.Curves;
using MPT.Math.Curves.Tools.Intersections;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Tools.Intersections
{
    [TestFixture]
    public static class IntersectionCircularCircularTests
    {
        #region Initialization

        public static void Initialization(CircularCurve curve1, CircularCurve curve2)
        {
            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
        }
        #endregion

        #region Methods: Public

        public static void AreTangent()
        {

        }


        public static void AreIntersecting()
        {

        }


        public static void IntersectionCoordinates()
        {

        }
        #endregion

        #region Methods: Static

        public static void AreTangent_Static(CircularCurve curve1, CircularCurve curve2)
        {

        }


        public static void AreIntersecting_Static(CircularCurve curve1, CircularCurve curve2)
        {

        }


        public static void IntersectionCoordinates_Static(CircularCurve curve1, CircularCurve curve2)
        {

        }
        #endregion
    }
}
