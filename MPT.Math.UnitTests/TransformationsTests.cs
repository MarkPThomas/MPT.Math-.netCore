using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Math.UnitTests
{

    [TestFixture]
    public static class TransformationsTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void Initialization_with_Coordinates_Results_in_Object_with_Immutable_Coordinates_Properties_List()
        {
            CartesianCoordinate localOriginInGlobal = new CartesianCoordinate(3, 2);
            CartesianCoordinate localAxisXPtInGlobal = new CartesianCoordinate(5, 4);
            Transformations transformations = new Transformations(localOriginInGlobal, localAxisXPtInGlobal);

            AngularOffset angularOffset = new AngularOffset(Numbers.Pi / 4);
            CartesianOffset offset = localOriginInGlobal.OffsetFrom(CartesianCoordinate.Origin());

            Assert.AreEqual(localOriginInGlobal, transformations.LocalOrigin);
            Assert.AreEqual(localAxisXPtInGlobal, transformations.LocalAxisX);
            Assert.AreEqual(offset, transformations.Displacement);
            Assert.AreEqual(angularOffset.ToAngle().Degrees, transformations.Rotation.ToAngle().Degrees, Tolerance);
        }
        #endregion

        [TestCase(4, 6, 1, 4, 3, 2, 4, 2)] // Displaced Coordinate System
        [TestCase(4, 6, 7.071068, 1.414214, 0, 0, 2, 2)] // Rotated Coordinate System
        [TestCase(4, 6, 3.535534, 2.121320, 3, 2, 5, 4)] // Displaced & Rotated Coordinate System
        public static void TransformToGlobal_Transforms_Local_Coordinate_to_Global(
            double globalCoordinateX, double globalCoordinateY,
            double localCoordinateX, double localCoordinateY,
            double localOriginInGlobalX, double localOriginInGlobalY, 
            double localAxisXPtInGlobalX, double localAxisXPtInGlobalY)
        {
            CartesianCoordinate localOriginInGlobal = new CartesianCoordinate(localOriginInGlobalX, localOriginInGlobalY, Tolerance);
            CartesianCoordinate localAxisXPtInGlobal = new CartesianCoordinate(localAxisXPtInGlobalX, localAxisXPtInGlobalY, Tolerance);
            Transformations transformations = new Transformations(localOriginInGlobal, localAxisXPtInGlobal);

            CartesianCoordinate coordinateLocal = new CartesianCoordinate(localCoordinateX, localCoordinateY, Tolerance);
            CartesianCoordinate coordinateGlobalExpected = new CartesianCoordinate(globalCoordinateX, globalCoordinateY, Tolerance);

            CartesianCoordinate coordinateGlobal = transformations.TransformToGlobal(coordinateLocal);
            coordinateGlobal.Tolerance = Tolerance;

            Assert.AreEqual(coordinateGlobalExpected, coordinateGlobal);
        }


        [TestCase(4, 6, 1, 4, 3, 2, 4, 2)] // Displaced Coordinate System
        [TestCase(4, 6, 7.071068, 1.414214, 0, 0, 2, 2)] // Rotated Coordinate System
        [TestCase(4, 6, 3.535534, 2.121320, 3, 2, 5, 4)] // Displaced & Rotated Coordinate System
        public static void TransformToLocal_Transforms_Global_Coordinate_to_Local(
            double globalCoordinateX, double globalCoordinateY, 
            double localCoordinateX, double localCoordinateY,
            double localOriginInGlobalX, double localOriginInGlobalY,
            double localAxisXPtInGlobalX, double localAxisXPtInGlobalY)
        {
            CartesianCoordinate localOriginInGlobal = new CartesianCoordinate(localOriginInGlobalX, localOriginInGlobalY, Tolerance);
            CartesianCoordinate localAxisXPtInGlobal = new CartesianCoordinate(localAxisXPtInGlobalX, localAxisXPtInGlobalY, Tolerance);
            Transformations transformations = new Transformations(localOriginInGlobal, localAxisXPtInGlobal);

            CartesianCoordinate coordinateGlobal = new CartesianCoordinate(globalCoordinateX, globalCoordinateY, Tolerance);
            CartesianCoordinate coordinateLocalExpected = new CartesianCoordinate(localCoordinateX, localCoordinateY, Tolerance);

            CartesianCoordinate coordinateLocal = transformations.TransformToLocal(coordinateGlobal);
            coordinateLocal.Tolerance = Tolerance;

            Assert.AreEqual(coordinateLocalExpected, coordinateLocal);
        }
    }
}
