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

        [TestCase(4, 6, 3, 5, 1, 1, 2, 1)]  // Displaced Coord system in local Quad 1
        [TestCase(4, 6, 5, 5, -1, 1, 0, 1)]  // Displaced Coord system in local Quad 2
        [TestCase(4, 6, 5, 7, -1, -1, 0, -1)]  // Displaced Coord system in local Quad 3
        [TestCase(4, 6, 3, 7, 1, -1, 2, -1)]  // Displaced Coord system in local Quad 4
        [TestCase(4, 6, 7.071068, 1.414214, 0, 0, 1, 1)]  // Rotated system x-axis towards global Quad 1, local Quad 1
        [TestCase(4, 6, 1.414214, -7.071068, 0, 0, -1, 1)]  // Rotated system x-axis towards global Quad 2, local Quad 4
        [TestCase(4, 6, -7.071068, -1.414214, 0, 0, -1, -1)]  // Rotated system x-axis towards global Quad 3, local Quad 3
        [TestCase(4, 6, -1.414214, 7.071068, 0, 0, 1, -1)]  // Rotated system x-axis towards global Quad 4, local Quad 2
        [TestCase(4, 6, 2.12132, -3.535534, 3, 2, 2, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 4
        [TestCase(4, 6, -3.535534, -2.12132, 3, 2, 2, 1)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 3
        [TestCase(4, 6, -2.12132, 3.535534, 3, 2, 4, 1)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 2
        [TestCase(4, 6, 3.535534, 2.12132, 3, 2, 4, 3)]  // Translated & Rotated system with x-axis towards global Quad 1, local coord in Quad 1
        [TestCase(-7, 3, 3.535534, 2.12132, -3, 2, -4, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 1
        [TestCase(-4, -6, 3.535534, 2.12132, -3, -2, -4, -3)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 1
        [TestCase(7, -3, 3.535534, 2.12132, 3, -2, 4, -3)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 1
        [TestCase(7, 3, 3.535534, -2.12132, 3, 2, 4, 3)]  // Translated & Rotated system with x-axis towards global Quad 1, local coord in Quad 4
        [TestCase(-4, 6, 3.535534, -2.12132, -3, 2, -4, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 4
        [TestCase(-7, -3, 3.535534, -2.12132, -3, -2, -4, -3)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 4
        [TestCase(4, -6, 3.535534, -2.12132, 3, -2, 4, -3)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 4
        [TestCase(-1, 1, -3.535534, 2.12132, 3, 2, 4, 3)]  // Translated & Rotated system with x-axis towards global Quad 1, local coord in Quad 2
        [TestCase(-2, -2, -3.535534, 2.12132, -3, 2, -4, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 2
        [TestCase(1, -1, -3.535534, 2.12132, -3, -2, -4, -3)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 2
        [TestCase(2, 2, -3.535534, 2.12132, 3, -2, 4, -3)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 2
        [TestCase(2, -2, -3.535534, -2.12132, 3, 2, 4, 3)]  // Translated & Rotated system with x-axis towards global Quad 1, local coord in Quad 3
        [TestCase(1, 1, -3.535534, -2.12132, -3, 2, -4, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 3
        [TestCase(-2, 2, -3.535534, -2.12132, -3, -2, -4, -3)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 3
        [TestCase(-1, -1, -3.535534, -2.12132, 3, -2, 4, -3)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 3
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


        [TestCase(4, 6, 3, 5, 1, 1, 2, 1)]  // Displaced Coord system in local Quad 1
        [TestCase(4, 6, 5, 5, -1, 1, 0, 1)]  // Displaced Coord system in local Quad 2
        [TestCase(4, 6, 5, 7, -1, -1, 0, -1)]  // Displaced Coord system in local Quad 3
        [TestCase(4, 6, 3, 7, 1, -1, 2, -1)]  // Displaced Coord system in local Quad 4
        [TestCase(4, 6, 7.071068, 1.414214, 0, 0, 1, 1)]  // Rotated system x-axis towards global Quad 1, local Quad 1
        [TestCase(4, 6, 1.414214, -7.071068, 0, 0, -1, 1)]  // Rotated system x-axis towards global Quad 2, local Quad 4
        [TestCase(4, 6, -7.071068, -1.414214, 0, 0, -1, -1)]  // Rotated system x-axis towards global Quad 3, local Quad 3
        [TestCase(4, 6, -1.414214, 7.071068, 0, 0, 1, -1)]  // Rotated system x-axis towards global Quad 4, local Quad 2
        [TestCase(4, 6, 2.12132, -3.535534, 3, 2, 2, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 4
        [TestCase(4, 6, -3.535534, -2.12132, 3, 2, 2, 1)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 3
        [TestCase(4, 6, -2.12132, 3.535534, 3, 2, 4, 1)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 2
        [TestCase(4, 6, 3.535534, 2.12132, 3, 2, 4, 3)]  // Translated & Rotated system with x-axis towards global Quad 1, local coord in Quad 1
        [TestCase(-7, 3, 3.535534, 2.12132, -3, 2, -4, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 1
        [TestCase(-4, -6, 3.535534, 2.12132, -3, -2, -4, -3)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 1
        [TestCase(7, -3, 3.535534, 2.12132, 3, -2, 4, -3)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 1
        [TestCase(7, 3, 3.535534, -2.12132, 3, 2, 4, 3)]  // Translated & Rotated system with x-axis towards global Quad 1, local coord in Quad 4
        [TestCase(-4, 6, 3.535534, -2.12132, -3, 2, -4, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 4
        [TestCase(-7, -3, 3.535534, -2.12132, -3, -2, -4, -3)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 4
        [TestCase(4, -6, 3.535534, -2.12132, 3, -2, 4, -3)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 4
        [TestCase(-1, 1, -3.535534, 2.12132, 3, 2, 4, 3)]  // Translated & Rotated system with x-axis towards global Quad 1, local coord in Quad 2
        [TestCase(-2, -2, -3.535534, 2.12132, -3, 2, -4, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 2
        [TestCase(1, -1, -3.535534, 2.12132, -3, -2, -4, -3)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 2
        [TestCase(2, 2, -3.535534, 2.12132, 3, -2, 4, -3)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 2
        [TestCase(2, -2, -3.535534, -2.12132, 3, 2, 4, 3)]  // Translated & Rotated system with x-axis towards global Quad 1, local coord in Quad 3
        [TestCase(1, 1, -3.535534, -2.12132, -3, 2, -4, 3)]  // Translated & Rotated system with x-axis towards global Quad 2, local coord in Quad 3
        [TestCase(-2, 2, -3.535534, -2.12132, -3, -2, -4, -3)]  // Translated & Rotated system with x-axis towards global Quad 3, local coord in Quad 3
        [TestCase(-1, -1, -3.535534, -2.12132, 3, -2, 4, -3)]  // Translated & Rotated system with x-axis towards global Quad 4, local coord in Quad 3
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
