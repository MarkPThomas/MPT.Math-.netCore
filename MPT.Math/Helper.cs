// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-26-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="Helper.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math
{
    /// <summary>
    /// Class Helper.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets the tolerance between two items.
        /// This is the maximum defined, or the overwrite, if provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double GetTolerance<T, U>(T item1, U item2, double tolerance = Numbers.ZeroTolerance) 
            where T : ITolerance
            where U : ITolerance
        {
            return tolerance == Numbers.ZeroTolerance ? Numbers.Max(item1.Tolerance, item2.Tolerance, Numbers.ZeroTolerance) : tolerance;
        }
    }
}
