// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-20-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-20-2020
// ***********************************************************************
// <copyright file="Transformations.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math
{
    /// <summary>
    /// Handles transformations between global coordinates and local coordinates.
    /// </summary>
    public class Transformations
    {
        /// <summary>
        /// Gets the local origin.
        /// </summary>
        /// <value>The local origin.</value>
        public CartesianCoordinate LocalOrigin { get; }
        /// <summary>
        /// Gets the local axis x.
        /// </summary>
        /// <value>The local axis x.</value>
        public CartesianCoordinate LocalAxisX { get; }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public AngularOffset Rotation { get; }

        /// <summary>
        /// Gets the displacement.
        /// </summary>
        /// <value>The displacement.</value>
        public CartesianOffset Displacement { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transformations"/> class.
        /// </summary>
        /// <param name="localOriginInGlobal">The local origin in global coordinates.</param>
        /// <param name="localAxisXPtInGlobal">Any point along the local x-axis in global coordinates.</param>
        public Transformations(CartesianCoordinate localOriginInGlobal, CartesianCoordinate localAxisXPtInGlobal)
        {
            LocalOrigin = localOriginInGlobal;
            LocalAxisX = localAxisXPtInGlobal;

            Displacement = localOriginInGlobal.OffsetFrom(CartesianCoordinate.Origin());
            Rotation = AngularOffset.CreateFromPoints(
                localAxisXPtInGlobal,
                localOriginInGlobal,
                new CartesianCoordinate(localOriginInGlobal.X - 1, localOriginInGlobal.Y)
                );
        }

        /// <summary>
        /// Transforms to global.
        /// </summary>
        /// <param name="localCoordinate">The local coordinate.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate TransformToGlobal(CartesianCoordinate localCoordinate)
        {
            CartesianCoordinate rotatedCoordinate = CartesianCoordinate.RotateAboutPoint(localCoordinate, LocalOrigin, Rotation.ToAngle().Radians);
            CartesianCoordinate translatedCoordinate = new CartesianCoordinate(LocalOrigin.X + localCoordinate.X, LocalOrigin.Y + localCoordinate.Y);
            return CartesianCoordinate.RotateAboutPoint(translatedCoordinate, LocalOrigin, Rotation.ToAngle().Radians); ;
        }

        /// <summary>
        /// Transforms to local.
        /// </summary>
        /// <param name="globalCoordinate">The global coordinate.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate TransformToLocal(CartesianCoordinate globalCoordinate)
        {
            CartesianCoordinate rotatedCoordinate = CartesianCoordinate.RotateAboutPoint(globalCoordinate, LocalOrigin, -1 * Rotation.ToAngle().Radians);
            return rotatedCoordinate - LocalOrigin;
        }
    }
}
