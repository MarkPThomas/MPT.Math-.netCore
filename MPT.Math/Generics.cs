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

using System;

namespace MPT.Math
{
    /// <summary>
    /// Contains static Math-related methods that deal with generic object types.
    /// </summary>
    public static class Generics
    {
        #region Properties
        /// <summary>
        /// Gets the tolerance between two items.
        /// This is the maximum defined, or the overwrite, if provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item1">The item1.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double GetTolerance<T>(T item1, double tolerance = Numbers.ZeroTolerance)
            where T : ITolerance
        {
            return tolerance == Numbers.ZeroTolerance ? Max(item1.Tolerance, Numbers.ZeroTolerance) : tolerance;
        }

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
            return tolerance == Numbers.ZeroTolerance ? Max(item1.Tolerance, item2.Tolerance, Numbers.ZeroTolerance) : tolerance;
        }
        #endregion

        #region Comparisons
        /// <summary>
        /// Determines whether the specified value is within the value bounds, including the values themselves.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="valueBound1">First value bound.</param>
        /// <param name="valueBound2">Second value bound.</param>
        /// <returns><c>true</c> if [is within inclusive] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsWithinInclusive<T>(T value, T valueBound1, T valueBound2) where T : IComparable<T>
        {
            T maxValue = Max(valueBound1, valueBound2);
            T minValue = Min(valueBound1, valueBound2);
            return IsWithinExclusive(value, valueBound1, valueBound2) ||
                minValue.CompareTo(value) == 0 ||
                maxValue.CompareTo(value) == 0;
        }


        /// <summary>
        /// Determines whether the specified value is within the value bounds, not including the values bounds themselves.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="valueBound1">First value bound.</param>
        /// <param name="valueBound2">Second value bound.</param>
        /// <returns><c>true</c> if [is within inclusive] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsWithinExclusive<T>(T value, T valueBound1, T valueBound2) where T : IComparable<T>
        {
            T maxValue = Max(valueBound1, valueBound2);
            T minValue = Min(valueBound1, valueBound2);
            return minValue.CompareTo(value) < 0 && maxValue.CompareTo(value) > 0;
        }



        /// <summary>
        /// Determines the maximum of the parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentException">Argument cannot be null.</exception>
        /// <exception cref="ArgumentException">Array has not been dimensioned.</exception>
        public static T Max<T>(params T[] items) where T : IComparable<T>
        {
            if (items == null) { throw new ArgumentException("Argument cannot be null."); }
            if (items.Length < 1) { throw new ArgumentException("Array has not been dimensioned."); }

            T max = items[0]; ;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].CompareTo(max) > 0)
                {
                    max = items[i];
                }
            }
            return max;
        }

        /// <summary>
        /// Determines the minimum of the parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentException">Argument cannot be null.</exception>
        /// <exception cref="ArgumentException">Array has not been dimensioned.</exception>
        public static T Min<T>(params T[] items) where T : IComparable<T>
        {
            if (items == null) { throw new ArgumentException("Argument cannot be null."); }
            if (items.Length < 1) { throw new ArgumentException("Array has not been dimensioned."); }

            T min = items[0]; ;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].CompareTo(min) < 0)
                {
                    min = items[i];
                }
            }
            return min;
        }
        #endregion
    }
}
