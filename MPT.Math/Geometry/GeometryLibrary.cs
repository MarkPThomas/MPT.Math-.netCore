using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;

namespace MPT.Math.Geometry
{
    /// <summary>
    /// Contains static methods for common geometry operations.
    /// For curvature, see <a href="https://en.wikipedia.org/wiki/Curvature">Reference</a>.
    /// </summary>
    public static class GeometryLibrary
    {
        #region Parametric Representations
        // Let γ(t) = (x(t), y(t)) be a proper parametric representation 
        // Here proper means that on the domain of definition of the parametrization, the derivative dγ/dt is defined, differentiable and nowhere equal to the zero vector.

        /// <summary>
        /// Slope of the curve based on all differentiated components being parametric.
        /// </summary>
        /// <param name="xPrime">The first differential of x w.r.t. some parameter.</param>
        /// <param name="yPrime">The first differential of y w.r.t. some parameter.</param>
        /// <returns>System.Double.</returns>
        public static double SlopeParametric(double xPrime, double yPrime)
        {
            return yPrime / xPrime;
        }

        /// <summary>
        /// Curvature of the curve based on all differentiated components being parametric.
        /// <a href="https://en.wikipedia.org/wiki/Curvature">Reference</a>.
        /// </summary>
        /// <param name="xPrime">The first differential of x w.r.t. some parameter.</param>
        /// <param name="yPrime">The first differential of y w.r.t. some parameter.</param>
        /// <param name="xPrimeDouble">The second differential of x w.r.t. some parameter.</param>
        /// <param name="yPrimeDouble">The second differential of y w.r.t. some parameter.</param>
        /// <returns>System.Double.</returns>
        public static double CurvatureParametric(double xPrime, double yPrime, double xPrimeDouble, double yPrimeDouble)
        {
            return (xPrime * yPrimeDouble - yPrime * xPrimeDouble) / (xPrime.Squared() + yPrime.Squared()).Pow(3d / 2);
        }
        #endregion

        #region Graph of a Function
        // The graph of a function y = f(x), is a special case of a parametrized curve, of the form x = t, y = f(t).

        /// <summary>
        /// Slope of the curve based on all differentiated components being from the graph of a function y = f(x).
        /// </summary>
        /// <param name="yPrime">The first differential of y w.r.t. some parameter.</param>
        /// <returns>System.Double.</returns>
        public static double SlopeGraph(double yPrime)
        {
            return yPrime;
        }

        /// <summary>
        /// Curvature of the curve based on all differentiated components being from the graph of a function y = f(x).
        /// <a href="https://en.wikipedia.org/wiki/Curvature">Reference</a>.
        /// </summary>
        /// <param name="yPrime">The first differential of y w.r.t. some parameter.</param>
        /// <param name="yPrimeDouble">The second differential of y w.r.t. some parameter.</param>
        /// <returns>System.Double.</returns>
        public static double CurvatureGraph(double yPrime, double yPrimeDouble)
        {
            return yPrimeDouble / (1 + yPrime.Squared()).Pow(3d / 2);
        }
        #endregion

        #region Polar Coordinates
        // If a curve is defined in polar coordinates by the radius expressed as a function of the polar angle, that is r is a function of θ

        /// <summary>
        /// Slope of the curve based on all differentiated components being the polar radius at θ differentiated by θ.
        /// <a href="https://socratic.org/questions/how-do-you-find-the-slope-of-a-polar-curve">Reference</a>.
        /// </summary>
        /// <param name="thetaRadians">The position θ, in radians.</param>
        /// <param name="radius">The polar radius at position θ.</param>
        /// <param name="radiusPrime">The first differential of the polar radius w.r.t. θ.</param>
        /// <returns>System.Double.</returns>
        public static double SlopePolar(double thetaRadians, double radius, double radiusPrime)
        {
            return (radiusPrime * Trig.Sin(thetaRadians) + radius * Trig.Cos(thetaRadians))/ (radiusPrime * Trig.Cos(thetaRadians) - radius * Trig.Sin(thetaRadians));
        }

        /// <summary>
        /// Curvature of the curve based on all differentiated components being the polar radius at θ differentiated by θ.
        /// <a href="https://en.wikipedia.org/wiki/Curvature">Reference</a>.
        /// </summary>
        /// <param name="radius">The polar radius at position θ.</param>
        /// <param name="radiusPrime">The first differential of the polar radius w.r.t. θ.</param>
        /// <param name="radiusPrimeDouble">The second differential of the polar radius w.r.t. θ.</param>
        /// <returns>System.Double.</returns>
        public static double CurvaturePolar(double radius, double radiusPrime, double radiusPrimeDouble)
        {
            return NMath.Abs(radius.Squared() + 2 * radiusPrime.Squared() - radius * radiusPrimeDouble) / (radius.Squared() + radiusPrime.Squared()).Pow(3d / 2);
        }
        #endregion

        #region Implicit Curves
        // For a curve defined by an implicit equation F(x, y) = 0 with partial derivatives denoted Fx, Fy, Fxx, Fxy, Fyy
        // https://en.wikipedia.org/wiki/Implicit_function

        /// <summary>
        /// Slope of the curve based on all differentiated components being being partial derivatives of an implicit equation.
        /// <a href="https://en.wikipedia.org/wiki/Implicit_function">Reference</a>.
        /// </summary>
        /// <param name="Fx">For function F(x,y), partial derivative dF/dx</param>
        /// <param name="Fy">For function F(x,y), partial derivative dF/dy</param>
        /// <returns>System.Double.</returns>
        public static double SlopeImplicit(double Fx, double Fy)
        {
            return -1 * Fx / Fy;
        }

        /// <summary>
        /// Curvature of the curve based on all differentiated components being partial derivatives of an implicit equation.
        /// <a href="https://en.wikipedia.org/wiki/Curvature">Reference</a>.
        /// </summary>
        /// <param name="Fx">For function F(x,y), partial derivative dF/dx</param>
        /// <param name="Fy">For function F(x,y), partial derivative dF/dy</param>
        /// <param name="Fxx">For function F(x,y), partial derivative (dF/dx)/dx</param>
        /// <param name="Fyy">For function F(x,y), partial derivative (dF/dy)/dy</param>
        /// <param name="Fxy">For function F(x,y), partial derivative (dF/dx)/dy</param>
        /// <returns>System.Double.</returns>
        public static double CurvatureImplicit(double Fx, double Fy, double Fxx, double Fxy, double Fyy)
        {
            return NMath.Abs(Fy.Squared() * Fxx - 2 * Fx * Fy * Fxy + Fx.Squared() * Fyy )/ (Fx.Squared() + Fy.Squared()).Pow(3d / 2);
        }
        #endregion
    }
}
