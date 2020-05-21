using System;
using System.Collections.Generic;
using System.Text;

namespace MPT.Math.CoordinateConverters
{
    class Cartesian2DTrilinearConverter
    {
        /// <summary>
        /// Converts to Trilinear coordinates.
        /// </summary>
        /// <param name="vertexA">The vertex a.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>TrilinearCoordinate.</returns>
        public TrilinearCoordinate ToTrilinear(CartesianCoordinate vertexA, CartesianCoordinate vertexB, CartesianCoordinate vertexC)
        {
            BarycentricCoordinate barycentric = ToBarycentric(vertexA, vertexB, vertexC);
            return barycentric.ToTrilinear(vertexA, vertexB, vertexC);
        }
    }
}
