using MPT.Math.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPT.Math.Curves.Tools.Intersections
{
    public class IntersectionLinearCubic : IntersectionAbstract<LinearCurve, CubicCurve>
    {
        #region Properties
        /// <summary>
        /// Gets the linear curve.
        /// </summary>
        /// <value>The linear curve.</value>
        public LinearCurve LinearCurve => Curve1;
        /// <summary>
        /// Gets the cubic curve.
        /// </summary>
        /// <value>The cubic curve.</value>
        public CubicCurve CubicCurve => Curve2;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearCubic"/> class.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="cubicCurve">The cubic curve.</param>
        public IntersectionLinearCubic(LinearCurve linearCurve, CubicCurve cubicCurve) : base(linearCurve, cubicCurve)
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
        public static double CenterSeparations(CircularCurve curve1, CircularCurve curve2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the curves are tangent to each other.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool AreTangent(CircularCurve curve1, CircularCurve curve2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(CircularCurve curve1, CircularCurve curve2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(CircularCurve curve1, CircularCurve curve2)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
