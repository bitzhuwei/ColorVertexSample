using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// This helps to get last vertex's id of picked primitive.
    /// </summary>
    public static class IColorCodedPickkingHelper
    {
        const int invalid = -1;

        /// <summary>
        /// Get last vertex's id of picked Primitive if it belongs to this <paramref name="picking"/> instance.
        /// <para>Returns -1 if <paramref name="vertexID"/> is an illigal number or the <paramref name="vertexID"/> is in some other element.</para>
        /// </summary>
        /// <param name="picking"></param>
        /// <param name="vertexID"></param>
        /// <returns></returns>
        public static int GetLastVertexIDOfPickedPrimitive(this IColorCodedPicking picking, int vertexID)
        {
            int lastVertexID = invalid;

            if (picking == null) { return lastVertexID; }

            if (vertexID < 0) // Illigal ID.
            { return lastVertexID; }

            if (vertexID < picking.PickingBaseID) // ID is in some previous element.
            { return lastVertexID; }

            if (picking.PickingBaseID + picking.PrimitiveCount <= vertexID) // ID is in some subsequent element.
            { return lastVertexID; }

            lastVertexID = vertexID - picking.PickingBaseID;

            return lastVertexID;
        }
    }
}
