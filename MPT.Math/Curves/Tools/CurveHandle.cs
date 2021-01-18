// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="CurveHandle.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using System;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;

namespace MPT.Math.Curves.Tools
{
    /// <summary>
    /// Class CurveHandle.
    /// </summary>
    public class CurveHandle : ICloneable
    {
        #region Properties
        /// <summary>
        /// Gets the control point.
        /// </summary>
        /// <value>The control point.</value>
        public CartesianCoordinate ControlPoint { get; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public Angle Rotation { get; set; }
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; set; }
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="CurveHandle" /> class.
        /// </summary>
        /// <param name="controlPoint">The control point, at the center of the handle.</param>
        /// <param name="radius">The radius of the handle.</param>
        public CurveHandle(CartesianCoordinate controlPoint, double radius)
        {
            ControlPoint = controlPoint;
            Radius = radius;
            Rotation = Angle.Origin();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurveHandle" /> class.
        /// </summary>
        /// <param name="controlPoint">The control point, at the center of the handle.</param>
        /// <param name="radius">The radius of the handle.</param>
        /// <param name="rotation">The rotation of the handle.</param>
        public CurveHandle(CartesianCoordinate controlPoint, double radius, Angle rotation)
        {
            ControlPoint = controlPoint;
            Radius = radius;
            Rotation = rotation;
        }
        #endregion

        #region Methods: Public   
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " - Center: " + ControlPoint + " - Radius: " + Radius + ", Rotation: " + Rotation;
        }

        /// <summary>
        /// The coordinate of the handle tip.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate GetHandleTip()
        {
            return ControlPoint + new CartesianCoordinate(Radius * Trig.Cos(Rotation.Radians), Radius * Trig.Sin(Rotation.Radians));
        }

        /// <summary>
        /// Sets the handle tip to the provided coordinate.
        /// </summary>
        /// <param name="handleTip">The handle tip.</param>
        public void SetHandleTip(CartesianCoordinate handleTip)
        {
            CartesianOffset offset = handleTip.OffsetFrom(ControlPoint);
            Radius = offset.Length();
            Rotation = offset.SlopeAngle();
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return CloneCurve();
        }

        /// <summary>
        /// Clones the curve.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public CurveHandle CloneCurve()
        {
            CurveHandle curve = new CurveHandle(ControlPoint, Radius, Rotation);
            return curve;
        }
        #endregion
    }
}
