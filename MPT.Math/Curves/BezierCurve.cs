// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="BezierCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics;
using MPT.Math.Curves.Parametrics.Beziers;
using MPT.Math.Curves.Tools;
using MPT.Math.Geometry;
using MPT.Math.Vectors;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Class BezierCurve.
    /// Implements the <see cref="MPT.Math.Curves.Curve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Curve" />
    public class BezierCurve : Curve,
        ICurveLimits,
        ICurvePositionCartesian
    {
        #region Properties          
        /// <summary>
        /// The maximum number of control points.
        /// </summary>
        protected const int _maxNumberOfControlPoints = 3;

        /// <summary>
        /// Gets the number of control points.
        /// </summary>
        /// <value>The number of control points.</value>
        public int NumberOfControlPoints { get; } = _maxNumberOfControlPoints;


        /// <summary>
        /// Gets the handle at starting point, i.
        /// </summary>
        /// <value>The handle i.</value>
        public BezierCurveHandle HandleI { get; }
        /// <summary>
        /// Gets the handle at ending point, j.
        /// </summary>
        /// <value>The handle j.</value>
        public BezierCurveHandle HandleJ { get; }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierCurve"/> class.
        /// </summary>
        /// <param name="handleStart">The handle start.</param>
        /// <param name="handleEnd">The handle end.</param>
        /// <param name="numberOfControlPoints">The number of control points.</param>
        public BezierCurve(BezierCurveHandle handleStart, BezierCurveHandle handleEnd, int numberOfControlPoints = _maxNumberOfControlPoints)
        {
            NumberOfControlPoints = getNumberOfControlPoints(numberOfControlPoints);

            HandleI = handleStart;
            HandleJ = handleEnd;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierCurve"/> class.
        /// </summary>
        /// <param name="pointI">The point i.</param>
        /// <param name="pointJ">The point j.</param>
        /// <param name="numberOfControlPoints">The number of control points.</param>
        public BezierCurve(CartesianCoordinate pointI, CartesianCoordinate pointJ, int numberOfControlPoints = _maxNumberOfControlPoints)
        {
            NumberOfControlPoints = getNumberOfControlPoints(numberOfControlPoints);

            double handleLength = getHandleLength(pointI, pointJ);
            HandleI = new BezierCurveHandle(pointI, handleLength);
            HandleJ = getBezierCurveHandleJ(pointJ, handleLength);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierCurve"/> class.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="numberOfControlPoints">The number of control points.</param>
        public BezierCurve(double length, int numberOfControlPoints = _maxNumberOfControlPoints)
        {
            NumberOfControlPoints = getNumberOfControlPoints(numberOfControlPoints);

            CartesianCoordinate pointI = CartesianCoordinate.Origin();
            CartesianCoordinate pointJ = new CartesianCoordinate(length, 0);
            double handleLength = getHandleLength(pointI, pointJ);
            HandleI = new BezierCurveHandle(pointI, handleLength);
            HandleJ = getBezierCurveHandleJ(pointJ, handleLength);
        }

        /// <summary>
        /// Gets the bezier curve handle j, with appropriate slope defaults.
        /// </summary>
        /// <param name="pointJ">The point j.</param>
        /// <param name="handleLength">Length of the handle.</param>
        /// <returns>BezierCurveHandle.</returns>
        private BezierCurveHandle getBezierCurveHandleJ(CartesianCoordinate pointJ, double handleLength)
        {
            return new BezierCurveHandle(pointJ, handleLength, new Angle(-1 * Numbers.Pi));
        }

        /// <summary>
        /// Gets the length of the handle for handles at the control points.
        /// </summary>
        /// <param name="pointI">The point i.</param>
        /// <param name="pointJ">The point j.</param>
        /// <returns>System.Double.</returns>
        private double getHandleLength(CartesianCoordinate pointI, CartesianCoordinate pointJ)
        {
            return 0.1 * pointJ.OffsetFrom(pointI).Length();
        }

        /// <summary>
        /// Gets the number of control points.
        /// </summary>
        /// <param name="numberOfControlPoints">The number of control points.</param>
        /// <returns>System.Int32.</returns>
        private int getNumberOfControlPoints(int numberOfControlPoints)
        {
            return Generics.Min(Generics.Max(0, numberOfControlPoints), _maxNumberOfControlPoints);
        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override LinearParametricEquation createParametricEquation()
        {
            switch (NumberOfControlPoints)
            {
                case 1:
                    return new BezierParametric1(this);
                case 2:
                    return new BezierParametric2(this);
                case 3:
                    return new BezierParametric3(this);
                default:
                    return new BezierParametric3(this);
            }
        }
        #endregion

        #region Methods: Properties  
        /// <summary>
        /// Slope of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="relativePosition">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <returns>System.Double.</returns>
        public virtual double SlopeByPosition(double relativePosition)
        {
            double xPrime = xPrimeByParameter(relativePosition);
            double yPrime = yPrimeByParameter(relativePosition);

            return GeometryLibrary.SlopeParametric(xPrime, yPrime);
        }

        /// <summary>
        /// Curvature of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="relativePosition">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <returns>System.Double.</returns>
        public virtual double CurvatureByPosition(double relativePosition)
        {
            double xPrime = xPrimeByParameter(relativePosition);
            double yPrime = yPrimeByParameter(relativePosition);
            double xPrimeDouble = xPrimeDoubleByParameter(relativePosition);
            double yPrimeDouble = yPrimeDoubleByParameter(relativePosition);

            return GeometryLibrary.CurvatureParametric(xPrime, yPrime, xPrimeDouble, yPrimeDouble);
        }

        /// <summary>
        /// Vector that is tangential to the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="relativePosition">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <returns>Vector.</returns>
        public virtual Vector TangentVectorByPosition(double relativePosition)
        {
            // TODO: Compare methods of components vs. parametric vector for TangentVectorByAngle? Commented method might be slower?
            //return vectorParametric.UnitTangentVectorAt(angleRadians).ToVectorAt(angleRadians);
            double xPrime = xPrimeByParameter(relativePosition);
            double yPrime = yPrimeByParameter(relativePosition);
            return Vector.UnitTangentVector(xPrime, yPrime, Tolerance);
        }

        /// <summary>
        /// Vector that is tangential to the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="relativePosition">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <returns>Vector.</returns>
        public virtual Vector NormalVectorByPosition(double relativePosition)
        {
            // TODO: Compare methods of components vs. parametric vector for NormalVectorByAngle? Commented method might be slower?
            //return vectorParametric.UnitNormalVectorAt(angleRadians).ToVectorAt(angleRadians);
            double xPrime = xPrimeByParameter(relativePosition);
            double yPrime = yPrimeByParameter(relativePosition);
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
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started. Relative position must be between 0 and 1.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended. Relative position must be between 0 and 1.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="NotImplementedException"></exception>
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
            return LinearCurve.Length(Range.Start.Limit, Range.End.Limit);
        }
        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started. Relative position must be between 0 and 1.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended. Relative position must be between 0 and 1.</param>
        /// <returns>System.Double.</returns>
        public double ChordLengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            return LinearCurve.Length(CoordinateCartesian(relativePositionStart), CoordinateCartesian(relativePositionEnd));
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public LinearCurve Chord()
        {
            return new LinearCurve(Range.Start.Limit, Range.End.Limit);
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started. Relative position must be between 0 and 1.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended. Relative position must be between 0 and 1.</param>
        /// <returns>LinearCurve.</returns>
        public LinearCurve ChordBetween(double relativePositionStart, double relativePositionEnd)
        {
            return new LinearCurve(CoordinateCartesian(relativePositionStart), CoordinateCartesian(relativePositionEnd));
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// </summary>
        /// <param name="relativePosition">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <returns>Vector.</returns>
        public Vector TangentVector(double relativePosition)
        {
            return TangentVectorByPosition(relativePosition);
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// </summary>
        /// <param name="relativePosition">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <returns>Vector.</returns>
        public Vector NormalVector(double relativePosition)
        {
            return NormalVectorByPosition(relativePosition);
        }

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// </summary>
        /// <param name="relativePosition">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateCartesian(double relativePosition)
        {
            return CoordinateByPosition(relativePosition);
        }

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public PolarCoordinate CoordinatePolar(double relativePosition)
        {
            return CoordinateCartesian(relativePosition);
        }
        #endregion

        #region ICurvePositionCartesian
        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public double XatY(double y)
        {
            return XsAtY(y)[0];
        }

        /// <summary>
        /// Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public double YatX(double x)
        {
            return YsAtX(x)[0];
        }

        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public double[] XsAtY(double y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public double[] YsAtX(double x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provided point lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if [is intersecting coordinate] [the specified coordinate]; otherwise, <c>false</c>.</returns>
        public bool IsIntersectingCoordinate(CartesianCoordinate coordinate)
        {
            double tolerance = Generics.GetTolerance(coordinate, Tolerance);
            double[] yIntersections = YsAtX(coordinate.X);
            return Array.Exists(yIntersections, element => element == coordinate.Y);
        }
        #endregion

        #region Methods: Curve Position
        /// <summary>
        /// The cartesian coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="relativePosition">The relative position, s. Relative position must be between 0 and 1.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateByPosition(double relativePosition)
        {
            return new CartesianCoordinate(xByParameter(relativePosition), yByParameter(relativePosition));
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " - I: " + HandleI + " - J: " + HandleJ + ", N: " + NumberOfControlPoints;
        }

        /// <summary>
        /// bs the 0.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate B_0()
        {
            return HandleI.ControlPoint;
        }
        /// <summary>
        /// bs the 1.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate B_1()
        {
            return HandleI.GetHandleTip();
        }
        /// <summary>
        /// bs the 2.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate B_2()
        {
            return HandleJ.GetHandleTip();
        }
        /// <summary>
        /// bs the 3.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate B_3()
        {
            return HandleJ.ControlPoint;
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
        public BezierCurve CloneCurve()
        {
            BezierCurve curve = new BezierCurve(HandleI.CloneCurve(), HandleJ.CloneCurve(), NumberOfControlPoints);
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
