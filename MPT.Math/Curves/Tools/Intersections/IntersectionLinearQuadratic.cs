using MPT.Math.Coordinates;
using System;

namespace MPT.Math.Curves.Tools.Intersections
{
    public class IntersectionLinearQuadratic : IntersectionAbstract<LinearCurve, QuadraticCurve>
    {
        #region Properties
        /// <summary>
        /// Gets the linear curve.
        /// </summary>
        /// <value>The linear curve.</value>
        public LinearCurve LinearCurve => Curve1;
        /// <summary>
        /// Gets the quadratic curve.
        /// </summary>
        /// <value>The quadratic curve.</value>
        public QuadraticCurve QuadraticCurve => Curve2;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearQuadratic"/> class.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="quadraticCurve">The quadratic curve.</param>
        public IntersectionLinearQuadratic(LinearCurve linearCurve, QuadraticCurve quadraticCurve) : base(linearCurve, quadraticCurve)
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
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="quadraticCurve">The quadratic curve.</param>
        /// <returns>System.Double.</returns>
        public static double CenterSeparations(LinearCurve linearCurve, QuadraticCurve quadraticCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the curves are tangent to each other.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="quadraticCurve">The quadratic curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreTangent(LinearCurve linearCurve, QuadraticCurve quadraticCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="quadraticCurve">The quadratic curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(LinearCurve linearCurve, QuadraticCurve quadraticCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="quadraticCurve">The quadratic curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(LinearCurve linearCurve, QuadraticCurve quadraticCurve)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
