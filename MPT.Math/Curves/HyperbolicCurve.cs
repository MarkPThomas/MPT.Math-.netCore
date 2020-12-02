// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="HyperbolicCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics;
using MPT.Math.Curves.Parametrics.ConicSectionCurves.Hyperbolics;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Trigonometry;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// A hyperbola is the set of all points where the difference between their distances from two fixed points (the foci) is constant. 
    /// In the case of a hyperbola, there are two foci and two directrices. Hyperbolas also have two asymptotes.
    /// Implements the <see cref="MPT.Math.Curves.ConicSectionCurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ConicSectionCurve" />
    public class HyperbolicCurve : ConicSectionCurve
    {
        #region Properties        
        /// <summary>
        /// The vertex in local coordinates that defines the asymptotes.
        /// </summary>
        private CartesianCoordinate _vertexAsymptoteLocal;
        /// <summary>
        /// Gets the asymptotes in local coordinates.
        /// </summary>
        /// <value>The asymptotes.</value>
        public Tuple<LinearCurve, LinearCurve> Asymptotes
        {
            get
            {
                return new Tuple<LinearCurve, LinearCurve>
                    (
                        new LinearCurve(CartesianCoordinate.Origin(), _vertexAsymptoteLocal),
                        new LinearCurve(CartesianCoordinate.Origin(), -1 * _vertexAsymptoteLocal)
                    );
            }
        }
        #endregion

        #region Initialization                
        /// <summary>
        /// Initializes a new instance of the <see cref="HyperbolicCurve" /> class.
        /// </summary>
        /// <param name="vertexMajor">The major vertex, a.</param>
        /// <param name="distanceFromFocusToLocalOrigin">The distance from focus, c, to local origin.</param>
        /// <param name="localOrigin">The coordinate of the local origin.</param>
        public HyperbolicCurve(
            CartesianCoordinate vertexMajor,
            double distanceFromFocusToLocalOrigin,
            CartesianCoordinate localOrigin)
            : base(vertexMajor, distanceFromFocusToLocalOrigin, localOrigin)
        {
            initialization();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalCurve" /> class.
        /// </summary>
        /// <param name="a">Distance from local origin to major vertex, a.</param>
        /// <param name="b">Distance from local origin to minor vertex, b.</param>
        /// <param name="center">The center.</param>
        /// <param name="rotation">The rotation.</param>
        public HyperbolicCurve(
            double a, 
            double b, 
            CartesianCoordinate center, 
            Angle rotation) 
            : base(center.OffsetCoordinate(a, rotation), distanceFromFocusToOrigin(a, b), center)
        {
            initialization();
        }

        /// <summary>
        /// Initializations the object,
        /// </summary>
        private void initialization()
        {
            _vertexAsymptoteLocal = new CartesianCoordinate(DistanceFromVertexMajorToOrigin, DistanceFromVertexMinorToOrigin);
        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override LinearParametricEquation createParametricEquation()
        {
            return new HyperbolicParametric(this);
        }
        #endregion

        #region Curve Position
        /// <summary>
        /// +X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which a +x-coordinate is desired.</param>
        /// <returns></returns>
        public override double XatY(double y)
        {
            return DistanceFromVertexMajorToOrigin * (1 + (y / DistanceFromVertexMinorToOrigin).Squared()).Sqrt();
        }

        /// <summary>
        /// +Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a +y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return DistanceFromVertexMinorToOrigin * ((x / DistanceFromVertexMajorToOrigin).Squared() - 1).Sqrt();
        }

        /// <summary>
        /// X-coordinate on the right curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// <a href="https://www.ck12.org/book/ck-12-calculus-concepts/section/10.3/">Reference</a>.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public double XbyRotationAboutOriginRight(double angleRadians)
        {
            return XbyRotationAboutOrigin(angleRadians);
        }

        /// <summary>
        /// X-coordinate on the left curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// <a href="https://www.ck12.org/book/ck-12-calculus-concepts/section/10.3/">Reference</a>.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public double XbyRotationAboutOriginLeft(double angleRadians)
        {
            return -1 * XbyRotationAboutOrigin(angleRadians);
        }


        /// <summary>
        /// X-coordinate on the curve in local coordinates about the focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusRight(double angleRadians)
        {
            return DistanceFromFocusToOrigin + RadiusAboutFocusRight(angleRadians) * TrigonometryLibrary.Cos(angleRadians);
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the left (-X) focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the left (-X) focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusLeft(double angleRadians)
        {
            return -1 * DistanceFromFocusToOrigin + RadiusAboutFocusLeft(angleRadians) * TrigonometryLibrary.Cos(angleRadians);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString()
                + " - Center: " + _originLocal
                + ", - Rotation: " + _localRotation
                + ", a: " + DistanceFromVertexMajorToOrigin
                + ", b: " + DistanceFromVertexMinorToOrigin;
        }
        #endregion

        #region Methods: Protected
        /// <summary>
        /// Distance from local origin to minor Vertex, b.
        /// </summary>
        /// <param name="a">Distance from local origin to major vertex, a.</param>
        /// <param name="c">Distance from local origin to the focus, c.</param>
        /// <returns>System.Double.</returns>
        protected override double distanceFromVertexMinorToOrigin(double a, double c)
        {
            return (c.Squared() - a.Squared()).Sqrt();
        }

        
        #endregion

        #region Methods: Static
        #region Protected
        /// <summary>
        /// Distances from focus to origin.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Double.</returns>
        protected static double distanceFromFocusToOrigin(double a, double b)
        {
            return AlgebraLibrary.SRSS(a, b);
        }
        #endregion
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
        public HyperbolicCurve CloneCurve()
        {
            HyperbolicCurve curve = new HyperbolicCurve(_vertexMajorLocal, DistanceFromFocusToOrigin, _originLocal);
            return curve;
        }
        #endregion
    }
}
