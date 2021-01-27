// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="LogarithmicSpiralCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics;
using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics.LogarithmicSpiralCurveComponents;
using MPT.Math.Curves.Tools;
using MPT.Math.Geometry;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using System;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;
namespace MPT.Math.Curves
{
    /// <summary>
    /// A curve whose polar tangential angle is constant.
    /// </summary>
    public class LogarithmicSpiralCurve : Curve,
        ICurveLimits
    {
        #region Properties        
        /// <summary>
        /// Gets the radius at origin, a.
        /// </summary>
        /// <value>The radius at origin.</value>
        public double RadiusAtOrigin { get; }

        /// <summary>
        /// Gets the radius change with differential rotation, b.
        /// </summary>
        /// <value>The radius change with differential rotation.</value>
        public double RadiusChangeWithRotation { get; }

        /// <summary>
        /// Gets the differential radius change with differential rotation, dr/dθ.
        /// </summary>
        /// <value>The differential radius change with differential rotation, dr/dθ.</value>
        public double RadiusPrime => RadiusAtOrigin * RadiusChangeWithRotation;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LogarithmicSpiralCurve"/> class.
        /// </summary>
        /// <param name="radiusAtOrigin">The radius at origin.</param>
        /// <param name="radiusChangeWithRotation">The radius change with rotation.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        public LogarithmicSpiralCurve(double radiusAtOrigin, double radiusChangeWithRotation, double tolerance = DEFAULT_TOLERANCE) : base(tolerance)
        {
            RadiusAtOrigin = radiusAtOrigin;
            RadiusChangeWithRotation = radiusChangeWithRotation;
        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override CartesianParametricEquationXY createParametricEquation()
        {
            return new LogarithmicSpiralCurveParametric(this);
        }
        #endregion

        #region Methods: Properties  
        #region Radius
        /// <summary>
        /// The radius measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public virtual double RadiusAboutOrigin(double angleRadians)
        {
            return RadiusAtOrigin * Numbers.E.Pow(RadiusChangeWithRotation * angleRadians);
        }

        /// <summary>
        /// The radius measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <param name="offset">The offst of the shape center/origin from the coordinate origin.</param>
        /// <returns>System.Double.</returns>
        public virtual double RadiusAboutOffset(double angleRadians, CartesianCoordinate offset)
        {
            return ((offset.X + XbyRotationAboutOrigin(angleRadians)).Squared() + (offset.Y + YbyRotationAboutOrigin(angleRadians)).Squared()).Sqrt();
        }
        #endregion

        /// <summary>
        /// Slope of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public virtual double SlopeByAngle(double angleRadians)
        {
            double xPrime = xPrimeByParameter(angleRadians);
            double yPrime = yPrimeByParameter(angleRadians);

            return GeometryLibrary.SlopeParametric(xPrime, yPrime);
        }

        /// <summary>
        /// Curvature of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public virtual double CurvatureByAngle(double angleRadians)
        {
            // TODO: Compare methods of components vs. parametric vector for CurvatureByAngle? Commented method might be slower?
            //double xPrime = xPrimeByAngle(angleRadians);
            //double yPrime = yPrimeByAngle(angleRadians);
            //double xPrimeDouble = xPrimeDoubleByAngle(angleRadians);
            //double yPrimeDouble = yPrimeDoubleByAngle(angleRadians);

            //return GeometryLibrary.CurvatureParametric(xPrime, yPrime, xPrimeDouble, yPrimeDouble);
            return Numbers.E.Pow(-1 * RadiusChangeWithRotation * angleRadians) / (RadiusAtOrigin * (1 + RadiusChangeWithRotation.Squared()).Sqrt());
        }

        /// <summary>
        /// Tangential angle of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public double TangentialAngleByAngle(double angleRadians)
        {
            return angleRadians;
        }

        /// <summary>
        /// Angle between the tangent of the curve and the radius connecting the origin to the point considered.
        /// This is in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>System.Double.</returns>
        public double PolarTangentialAngleAboutOriginByAngle(double angleRadians)
        {
            return Trig.ArcTan(1 / RadiusChangeWithRotation);
        }

        /// <summary>
        /// Vector that is tangential to the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>Vector.</returns>
        public virtual Vector TangentVectorByAngle(double angleRadians)
        {
            // TODO: Compare methods of components vs. parametric vector for TangentVectorByAngle? Commented method might be slower?
            //return vectorParametric.UnitTangentVectorAt(angleRadians).ToVectorAt(angleRadians);
            double xPrime = xPrimeByParameter(angleRadians);
            double yPrime = yPrimeByParameter(angleRadians);
            return Vector.UnitTangentVector(xPrime, yPrime, Tolerance);
        }

        /// <summary>
        /// Vector that is tangential to the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>Vector.</returns>
        public virtual Vector NormalVectorByAngle(double angleRadians)
        {
            // TODO: Compare methods of components vs. parametric vector for NormalVectorByAngle? Commented method might be slower?
            //return vectorParametric.UnitNormalVectorAt(angleRadians).ToVectorAt(angleRadians);
            double xPrime = xPrimeByParameter(angleRadians);
            double yPrime = yPrimeByParameter(angleRadians);
            return Vector.UnitNormalVector(xPrime, yPrime, Tolerance);
        }
        #endregion

        #region ICurveLimits

        /// <summary>
        /// Length of the curve between the limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return LengthBetween(0, 1);
        }

        /// <summary>
        /// Length of the curve between two points.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public double LengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double ChordLength()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public double ChordLengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public LinearCurve Chord()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the linear curve is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the linear curve is ended.</param>
        /// <returns>LinearCurve.</returns>
        public LinearCurve ChordBetween(double relativePositionStart, double relativePositionEnd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative length along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        public Vector TangentVector(double relativePosition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative length along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        public Vector NormalVector(double relativePosition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateCartesian(double relativePosition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        public PolarCoordinate CoordinatePolar(double relativePosition)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods: Curve Position
        /// <summary>
        /// The cartesian coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angle">Angle of rotation about the local origin</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateByAngle(Angle angle)
        {
            return CoordinateByAngle(angle.Radians);
        }

        /// <summary>
        /// The cartesian coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the local origin, in radians.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateByAngle(double angleRadians)
        {
            double x = XbyRotationAboutOrigin(angleRadians);
            double y = YbyRotationAboutOrigin(angleRadians);
            return new CartesianCoordinate(x, y);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " - Radius at origin, a: " + RadiusAtOrigin  + ", Radius change w/ rotation, b: " + RadiusChangeWithRotation;
        }

        /// <summary>
        /// Length of the curve from the start to the specified position.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the local origin, in radians.</param>
        /// <returns>System.Double.</returns>
        public double LengthTo(double angleRadians)
        {
            return Numbers.E.Pow(RadiusChangeWithRotation * angleRadians) * (RadiusAtOrigin / RadiusChangeWithRotation ) * (1 + RadiusChangeWithRotation.Squared()).Sqrt();
        }

        /// <summary>
        /// The length within the provided rotation along a circular curve.
        /// </summary>
        /// <param name="rotation">Rotation to get arc length between.</param>
        /// <returns>System.Double.</returns>
        public double LengthBetween(AngularOffset rotation)
        {
            return LengthTo(rotation.J.RadiansRaw) - LengthTo(rotation.I.RadiansRaw);
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return CloneCurve();
        }

        /// <summary>
        /// Clones the curve.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public LogarithmicSpiralCurve CloneCurve()
        {
            LogarithmicSpiralCurve curve = new LogarithmicSpiralCurve(RadiusAtOrigin, RadiusChangeWithRotation);
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
