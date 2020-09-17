using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Class Curve.
    /// Implements the <see cref="MPT.Math.Curves.ICurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ICurve" />
    public abstract class Curve : ICurve, ICloneable
    {
        #region Properties        
        /// <summary>
        /// The tolerance.
        /// </summary>
        protected double _tolerance = 10E-6;
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        public virtual double Tolerance 
        {
            get => _tolerance;
            set => _tolerance = value; 
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public abstract object Clone();
        #endregion
    }
}
