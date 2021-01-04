using MPT.Math.Curves;
using MPT.Math.Curves.Tools.Intersections;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Tools.Intersections
{
    [TestFixture]
    public static class IntersectionLinearLinearTests
    {
        #region Initialization

        public static void Initialization(LinearCurve curve1, LinearCurve curve2) 
        {
            IntersectionLinearLinear intersections = new IntersectionLinearLinear(curve1, curve2);
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

        public static void AreTangent_Static(LinearCurve curve1, LinearCurve curve2)
        {

        }


        public static void AreIntersecting_Static(LinearCurve curve1, LinearCurve curve2)
        {

        }


        public static void IntersectionCoordinates_Static(LinearCurve curve1, LinearCurve curve2)
        {

        }
        #endregion
    }
}
