// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-16-2020
// ***********************************************************************
// <copyright file="CartesianCoordinate.cs" company="Mark P Thomas, Inc.">
//     Copyright © 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.CoordinateConverters;
using MPT.Math.Curves;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using System;
using NMath = System.Math;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// A two-dimensional coordinate system that specifies each point uniquely in a plane by a set of numerical coordinates, which are the signed distances to the point from two fixed perpendicular oriented lines, measured in the same unit of length.
    /// </summary>
    /// <seealso ref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
    /// <seealso ref="https://en.wikipedia.org/wiki/Euclidean_space"/>
    /// <seealso cref="System.IEquatable{CartesianCoordinate}" />
    public struct CartesianCoordinate : IEquatable<CartesianCoordinate>, ICoordinate
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the x-coordinate.
        /// </summary>
        /// <value>The x-coordinate.</value>
        public double X { get; private set; }

        /// <summary>
        /// Gets the y-coordinate.
        /// </summary>
        /// <value>The y-coordinate.</value>
        public double Y { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianCoordinate"/> struct.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="tolerance">The tolerance to be used in relating coordinates.</param>
        public CartesianCoordinate(double x, double y,
            double tolerance = Numbers.ZeroTolerance)
        {
            X = x;
            Y = y;
            Tolerance = tolerance;
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " - X: " + X + ", Y: " + Y;
        }

        /// <summary>
        /// Returns the cross product/determinant of the coordinates.
        /// x1*y2 - x2*y1
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>System.Double.</returns>
        public double CrossProduct(CartesianCoordinate coordinate)
        {
            return VectorLibrary.CrossProduct(X, Y, coordinate.X, coordinate.Y);
        }

        /// <summary>
        /// Returns the dot product of the coordinates.
        /// x1*x2 + y1*y2
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>System.Double.</returns>
        public double DotProduct(CartesianCoordinate coordinate)
        {
            return VectorLibrary.DotProduct(X, Y, coordinate.X, coordinate.Y);
        }

        /// <summary>
        /// Returns the cartesian offset of the current coordinate from the provided coordinate.
        /// i.e. the current coordinate subtracting the provided coordinate.
        /// </summary>
        /// <param name="coordinateI">The coordinate i.</param>
        /// <returns>AngularOffset.</returns>
        public CartesianOffset OffsetFrom(CartesianCoordinate coordinateI)
        {
            return new CartesianOffset(coordinateI, this);
        }

        /// <summary>
        /// Returns a new coordinate offset by the provided parameters.
        /// </summary>
        /// <param name="distance">The distance to offset.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate OffsetCoordinate(double distance, Angle rotation)
        {
            return OffsetCoordinate(this, distance, rotation);
        }

        /// <summary>
        /// The linear distance the coordinate is from the origin.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double DistanceFromOrigin()
        {
            return AlgebraLibrary.SRSS(X, Y);
        }
        #endregion

        #region Methods: Static
        /// <summary>
        /// Returns a default static coordinate at the origin.
        /// </summary>
        /// <returns></returns>
        public static CartesianCoordinate Origin()
        {
            return new CartesianCoordinate(0, 0);
        }

        /// <summary>
        /// Returns a new coordinate offset from the provided coordinate.
        /// </summary>
        /// <param name="distance">The distance to offset.</param>
        /// <param name="center">The center.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate OffsetCoordinate(CartesianCoordinate center, double distance, Angle rotation)
        {
            return new CartesianCoordinate(
                center.X + distance * Trig.Cos(rotation.Radians), 
                center.Y + distance * Trig.Sin(rotation.Radians), 
                center.Tolerance);
        }
        #endregion

        #region Methods: Static/ITransform      
        /// <summary>
        /// Rotates the the specified coordinate by the specified angle about a point.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="centerOfRotation">The center of rotation.</param>
        /// <param name="angleRadians">The angle [radians], where counter-clockwise is positive.</param>
        /// <returns>MPT.Math.Coordinates.CartesianCoordinate.</returns>
        public static CartesianCoordinate RotateAboutPoint(
            CartesianCoordinate coordinate, 
            CartesianCoordinate centerOfRotation, 
            double angleRadians)
        {
            // Move coordinate such that center of rotation is at origin
            CartesianCoordinate centeredCoordinate = coordinate - centerOfRotation;

            // Rotate coordinate
            CartesianCoordinate rotatedCoordinate = Rotate(centeredCoordinate, angleRadians);

            // Move coordinate such that center of rotation is back at original coordinate
            return rotatedCoordinate + centerOfRotation;
        }

        /// <summary>
        /// Rotates the specified coordinate by the specifed angle about the origin.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="angleRadians">The angle [radians], where counter-clockwise is positive.</param>
        /// <returns>MPT.Math.Coordinates.CartesianCoordinate.</returns>
        public static CartesianCoordinate Rotate(CartesianCoordinate coordinate, double angleRadians)
        {
            double xRotated = coordinate.X * Trig.Cos(angleRadians) - coordinate.Y * Trig.Sin(angleRadians);
            double yRotated = coordinate.X * Trig.Sin(angleRadians) + coordinate.Y * Trig.Cos(angleRadians);

            return new CartesianCoordinate(xRotated, yRotated);
        }

        /// <summary>
        /// Scales the specified coordinate from the specified point.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="referencePoint">The reference point.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate ScaleFromPoint(
            CartesianCoordinate coordinate, 
            CartesianCoordinate referencePoint, 
            double scale)
        {
            // Move coordinate such that reference point is at origin
            CartesianCoordinate centeredCoordinate = coordinate - referencePoint;

            // Scale coordinate
            CartesianCoordinate rotatedCoordinate = Scale(centeredCoordinate, scale);

            // Move coordinate such that reference point is back at original coordinate
            return rotatedCoordinate + referencePoint;
        }

        /// <summary>
        /// Scales the specified coordinate about the origin.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate Scale(CartesianCoordinate coordinate, double scale)
        {
            return scale * coordinate;
        }

        /// <summary>
        /// Skews the specified coordinate to the skewing of a containing box.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="stationaryReferencePoint">The stationary reference point of the skew box.</param>
        /// <param name="skewingReferencePoint">The skewing reference point of the skew box.</param>
        /// <param name="magnitude">The magnitude to skew along the x-axis and y-axis.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate SkewWithinBox(
            CartesianCoordinate coordinate, 
            CartesianCoordinate stationaryReferencePoint,
            CartesianCoordinate skewingReferencePoint,
            CartesianOffset magnitude)
        {
            CartesianOffset skewBoxOffset = skewingReferencePoint.OffsetFrom(stationaryReferencePoint);
            double lambdaX = magnitude.X() / skewBoxOffset.Y();
            double lambdaY = magnitude.Y() / skewBoxOffset.X();

            return Skew(coordinate, lambdaX, lambdaY);
        }

        /// <summary>
        /// Skews the specified coordinate about the origin.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="lambda">The magnitude to skew along the x-axis and y-axis.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate Skew(CartesianCoordinate coordinate, CartesianOffset lambda)
        {
            return Skew(coordinate, lambda.X(), lambda.Y());
        }

        /// <summary>
        /// Skews the specified coordinate about the origin.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="lambdaX">The magnitude to skew along the x-axis.</param>
        /// <param name="lambdaY">The magnitude to skew along the y-axis.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate Skew(CartesianCoordinate coordinate, double lambdaX, double lambdaY)
        {
            return new CartesianCoordinate(coordinate.X + lambdaX * coordinate.Y, coordinate.Y + lambdaY * coordinate.X);
        }

        /// <summary>
        /// Mirrors the specified coordinate about the specified line.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="referenceLine">The reference line.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate MirrorAboutLine(
            CartesianCoordinate coordinate, 
            LinearCurve referenceLine)
        {
            CartesianCoordinate reflectionLinePoint = referenceLine.CoordinateOfPerpendicularProjection(coordinate);
            CartesianOffset deltaReflection = 2 * reflectionLinePoint.OffsetFrom(coordinate);

            return coordinate + deltaReflection;
        }

        /// <summary>
        /// Mirrors the specified coordinate about the x-axis.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate MirrorAboutAxisX(CartesianCoordinate coordinate)
        {
            return new CartesianCoordinate(coordinate.X, -coordinate.Y);
        }

        /// <summary>
        /// Mirrors the specified coordinate about the y-axis.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate MirrorAboutAxisY(CartesianCoordinate coordinate)
        {
            return new CartesianCoordinate(-coordinate.X, coordinate.Y);
        }
        #endregion

        #region Methods: Conversion        
        /// <summary>
        /// Converts the cartesian coordinate to a polar coordinate.
        /// </summary>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate ToPolar()
        {
            return Cartesian2DPolarConverter.ToPolar(this);
        }
        #endregion

        #region Operators: Equals
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CartesianCoordinate a, CartesianCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CartesianCoordinate a, CartesianCoordinate b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CartesianCoordinate a, ICoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CartesianCoordinate a, ICoordinate b)
        {
            return !a.Equals(b);
        }
        #endregion

        #region Operators: Combining
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator -(CartesianCoordinate a, CartesianCoordinate b)
        {
            return new CartesianCoordinate(
                a.X - b.X,
                a.Y - b.Y,
                Generics.GetTolerance(a, b));
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">Coordinate a.</param>
        /// <param name="b">Coordinate b.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator +(CartesianCoordinate a, CartesianCoordinate b)
        {
            return new CartesianCoordinate(
                a.X + b.X,
                a.Y + b.Y,
                Generics.GetTolerance(a, b));
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="multiplier">Multiplier value.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator *(CartesianCoordinate coordinate, double multiplier)
        {
            return multiplier * coordinate;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="multiplier">Multiplier value.</param>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator *(double multiplier, CartesianCoordinate coordinate)
        {
            return new CartesianCoordinate(
                coordinate.X * multiplier,
                coordinate.Y * multiplier,
                coordinate.Tolerance);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="denominator">The denominator value.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator /(CartesianCoordinate coordinate, double denominator)
        {
            if (denominator == 0) { throw new DivideByZeroException(); }
            double x = coordinate.X / denominator;
            double y = coordinate.Y / denominator;

            return new CartesianCoordinate(x, y, coordinate.Tolerance);
        }
        #endregion

        #region Operators: Conversion        
        /// <summary>
        /// Performs an implicit conversion from <see cref="CartesianCoordinate"/> to <see cref="PolarCoordinate"/>.
        /// </summary>
        /// <param name="cartesian">The cartesian.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PolarCoordinate(CartesianCoordinate cartesian) => cartesian.ToPolar();
        #endregion

        #region IEquatable
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CartesianCoordinate other)
        {
            double tolerance = NMath.Min(Tolerance, other.Tolerance);
            return X.IsEqualTo(other.X, tolerance) &&
                   Y.IsEqualTo(other.Y, tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CartesianCoordinate) { return Equals((CartesianCoordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same ICoordinate interface.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(ICoordinate other)
        {
            if (other is CartesianCoordinate)
            {
                return Equals((CartesianCoordinate)other);
            }
            if (other is PolarCoordinate)
            {
                return Equals(Cartesian2DPolarConverter.ToCartesian((PolarCoordinate)other));
            }
            return Equals((object)other);
        }
        #endregion
    }
}
