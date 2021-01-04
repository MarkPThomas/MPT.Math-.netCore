using MPT.Math.Curves;
using MPT.Math.Curves.Tools.Intersections;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Tools.Intersections
{
    [TestFixture]
    public static class IntersectionLinearCircularTests
    {
        #region Initialization

        public static void Initialization(LinearCurve linearCurve, CircularCurve circularCurve)
        {
            IntersectionLinearCircular intersections = new IntersectionLinearCircular(linearCurve, circularCurve);
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

        public static void AreTangent_Static(LinearCurve linearCurve, CircularCurve circularCurve)
        {

        }


        public static void AreIntersecting_Static(LinearCurve linearCurve, CircularCurve circularCurve)
        {

        }


        public static void IntersectionCoordinates_Static(LinearCurve linearCurve, CircularCurve circularCurve)
        {

        }
        #endregion
    }
}
