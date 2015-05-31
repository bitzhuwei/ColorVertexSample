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
        /// <para>Returns -1 if <paramref name="stageVertexID"/> is an illigal number or in some other element.</para>
        /// </summary>
        /// <param name="picking"></param>
        /// <param name="stageVertexID"></param>
        /// <returns></returns>
        public static int GetLastVertexIDOfPickedPrimitive(this IColorCodedPicking picking, int stageVertexID)
        {
            int lastVertexID = invalid;

            if (picking == null) { return lastVertexID; }

            if (stageVertexID < 0) // Illigal ID.
            { return lastVertexID; }

            if (stageVertexID < picking.PickingBaseID) // ID is in some previous element.
            { return lastVertexID; }

            if (picking.PickingBaseID + picking.VertexCount <= stageVertexID) // ID is in some subsequent element.
            { return lastVertexID; }

            lastVertexID = stageVertexID - picking.PickingBaseID;

            return lastVertexID;
        }
    }
}
