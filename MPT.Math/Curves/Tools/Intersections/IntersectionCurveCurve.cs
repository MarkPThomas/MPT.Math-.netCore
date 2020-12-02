
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using System;

namespace MPT.Math.Curves.Tools.Intersections
{
    public class IntersectionCurveCurve : IntersectionAbstract<Curve, Curve>
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionCurveCurve"/> class.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The second curve.</param>
        public IntersectionCurveCurve(Curve curve1, Curve curve2) : base(curve1, curve2)
        {

        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AreTangent()
        {
            return AreTangent(Curve1, Curve2);
        }

        /// <summary>
        /// The curves intersect and are not tangent.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AreIntersecting()
        {
            return AreIntersecting(Curve1, Curve2);
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <returns>CartesianCoordinate[].</returns>
        public override CartesianCoordinate[] IntersectionCoordinates()
        {
            return IntersectionCoordinates(Curve1, Curve2);
        }
        #endregion

        #region Static        
        /// <summary>
        /// The separation of the centers of the curves.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns>System.Double.</returns>
        public static double CenterSeparations(Curve curve1, Curve curve2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the curves are tangent to each other.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreTangent(Curve curve1, Curve curve2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(Curve curve1, Curve curve2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(Curve curve1, Curve curve2)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
