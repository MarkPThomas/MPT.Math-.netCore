using MPT.Math.Trigonometry;
using System;


namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Parabolics
{
    internal class ParabolicFocusParametricX : ConicParametricDoubleComponents
    {

        public ParabolicFocusParametricX(ParabolicCurve parent) : base(parent)
        {
        }

        #region Methods: Parametric Equations and Differentials

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the focus, in radians.</param>
        /// <returns></returns>
        public override double BaseByParameter(double angleRadians)
        {
            return (_parent.DistanceFromVertexMajorToLocalOrigin - _parent.RadiusAboutFocusRight((Coordinates.Angle)angleRadians) * TrigonometryLibrary.Cos(angleRadians));
        }


        public override double PrimeByParameter(double angleRadians)
        {
            throw new NotImplementedException();
            //return (_parent.RadiusByAngle(angleRadians) * TrigonometryLibrary.Sin(angleRadians) - _parent.radiusPrimeByAngle(angleRadians) * TrigonometryLibrary.Cos(angleRadians));
        }


        public override double PrimeDoubleByParameter(double angleRadians)
        {
            throw new NotImplementedException();
            //return 2 * _parent.DistanceFromVertexMajorToOrigin;
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return CloneParametric();
        }

        /// <summary>
        /// Clones the curve.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public ParabolicParametricX CloneParametric()
        {
            ParabolicParametricX parametric = new ParabolicParametricX(_parent as ParabolicCurve);
            return parametric;
        }
        #endregion
    }
}
