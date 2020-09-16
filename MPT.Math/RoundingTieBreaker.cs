namespace MPT.Math
{
    /// <summary>
    /// Methods for breaking ties in rounding (i.e. when the number triggering rounding is 5).
    /// </summary>
    public enum RoundingTieBreaker
    {
        /// <summary>
        /// Rounds to the number farthest away from 0 (e.g. 1.25 -> 1.3, -1.25 -> -1.3).
        /// </summary>
        HalfAwayFromZero = 0,
        /// <summary>
        /// Rounds to the nearest even number (e.g. 1.25 -> 1.2, 1.35 -> 1.4) 
        /// This is the method preferred by many scientific disciplines, because, for example, it avoids skewing the average value of a long list of values upwards.
        /// </summary>
        HalfToEven = 1
    }
}
