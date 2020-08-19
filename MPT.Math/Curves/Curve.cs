namespace MPT.Math.Curves
{
    /// <summary>
    /// Class Curve.
    /// Implements the <see cref="MPT.Math.Curves.ICurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ICurve" />
    public abstract class Curve : ICurve
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        public double Tolerance { get; set; } = 10E-6;
        #endregion
    }
}
