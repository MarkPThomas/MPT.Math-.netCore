using MPT.Math.Coordinates;
using System;

namespace MPT.Math.Curves.Tools.Intersections
{
    public class IntersectionLinearBezier : IntersectionAbstract<LinearCurve, BezierCurve>
    {
        #region Properties
        /// <summary>
        /// Gets the linear curve.
        /// </summary>
        /// <value>The linear curve.</value>
        public LinearCurve LinearCurve => Curve1;
        /// <summary>
        /// Gets the bezier curve.
        /// </summary>
        /// <value>The bezier curve.</value>
        public BezierCurve BezierCurve => Curve2;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearBezier"/> class.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="bezierCurve">The bezier curve.</param>
        public IntersectionLinearBezier(LinearCurve linearCurve, BezierCurve bezierCurve) : base(linearCurve, bezierCurve)
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
        /// <param name="bezierCurve">The bezier curve.</param>
        /// <returns>System.Double.</returns>
        public static double CenterSeparations(LinearCurve linearCurve, BezierCurve bezierCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the curves are tangent to each other.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="bezierCurve">The bezier curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreTangent(LinearCurve linearCurve, BezierCurve bezierCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="bezierCurve">The bezier curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(LinearCurve linearCurve, BezierCurve bezierCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="bezierCurve">The bezier curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(LinearCurve linearCurve, BezierCurve bezierCurve)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
