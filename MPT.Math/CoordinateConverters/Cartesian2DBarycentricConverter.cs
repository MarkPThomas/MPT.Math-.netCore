using System;
using System.Collections.Generic;
using System.Text;

namespace MPT.Math.CoordinateConverters
{
    class Cartesian2DBarycentricConverter
    {
        /// <summary>
        /// Converts to Barycentric coordinates.
        /// </summary>
        /// <param name="vertexA">The vertex a coordinate of the simplex shape.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>BarycentricCoordinate.</returns>
        public BarycentricCoordinate ToBarycentric(CartesianCoordinate vertexA, CartesianCoordinate vertexB, CartesianCoordinate vertexC)
        {
            double determinate = (vertexB.Y - vertexC.Y) * (vertexA.X - vertexC.X) +
                                 (vertexC.X - vertexB.X) * (vertexA.Y - vertexC.Y);

            double alpha = ((vertexB.Y - vertexC.Y) * (X - vertexC.X) +
                            (vertexC.X - vertexB.X) * (Y - vertexC.Y)) / determinate;

            double beta = ((vertexC.Y - vertexA.Y) * (X - vertexC.X) +
                            (vertexA.X - vertexC.X) * (Y - vertexC.Y)) / determinate;

            double gamma = 1 - alpha - beta;

            return new BarycentricCoordinate(alpha, beta, gamma, Tolerance);
        }
    }
}
