using MPT.Math.Coordinates;
using System;

namespace MPT.Math.Curves.Tools.Intersections
{
    public class IntersectionLinearLogarithmicSpiral : IntersectionAbstract<LinearCurve, LogarithmicSpiralCurve>
    {
        #region Properties
        /// <summary>
        /// Gets the linear curve.
        /// </summary>
        /// <value>The linear curve.</value>
        public LinearCurve LinearCurve => Curve1;
        /// <summary>
        /// Gets the logarithmic spiral curve.
        /// </summary>
        /// <value>The logarithmic spiral curve.</value>
        public LogarithmicSpiralCurve LogarithmicSpiralCurve => Curve2;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearLogarithmicSpiral"/> class.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="logarithmicSpiralCurve">The logarithmic spiral curve.</param>
        public IntersectionLinearLogarithmicSpiral(LinearCurve linearCurve, LogarithmicSpiralCurve logarithmicSpiralCurve) : base(linearCurve, logarithmicSpiralCurve)
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
        /// <param name="logarithmicSpiralCurve">The logarithmic spiral curve.</param>
        /// <returns>System.Double.</returns>
        public static double CenterSeparations(LinearCurve linearCurve, LogarithmicSpiralCurve logarithmicSpiralCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the curves are tangent to each other.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="logarithmicSpiralCurve">The logarithmic spiral curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool AreTangent(LinearCurve linearCurve, LogarithmicSpiralCurve logarithmicSpiralCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="logarithmicSpiralCurve">The logarithmic spiral curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(LinearCurve linearCurve, LogarithmicSpiralCurve logarithmicSpiralCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="logarithmicSpiralCurve">The logarithmic spiral curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(LinearCurve linearCurve, LogarithmicSpiralCurve logarithmicSpiralCurve)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
