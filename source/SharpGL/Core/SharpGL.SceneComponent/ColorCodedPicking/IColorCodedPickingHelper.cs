using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// This helps to get last vertex's id of picked primitive.
    /// </summary>
    public static class IColorCodedPickingHelper
    {
        const int invalid = -1;

        /// <summary>
        /// Returns last vertex's id of picked primitive if the primitive represented by <paramref name="stageVertexID"/> belongs to this <paramref name="element"/> instance.
        /// <para>Returns -1 if <paramref name="stageVertexID"/> is an illigal number or the primitive is in some other element.</para>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="stageVertexID"></param>
        /// <returns></returns>
        public static int GetLastVertexIDOfPickedPrimitive(this IColorCodedPicking element, int stageVertexID)
        {
            int lastVertexID = invalid;

            if (element == null) { return lastVertexID; }

            if (stageVertexID < 0) // Illigal ID.
            { return lastVertexID; }

            if (stageVertexID < element.PickingBaseID) // ID is in some previous element.
            { return lastVertexID; }

            if (element.PickingBaseID + element.VertexCount <= stageVertexID) // ID is in some subsequent element.
            { return lastVertexID; }

            lastVertexID = stageVertexID - element.PickingBaseID;

            return lastVertexID;
        }

        /// <summary>
        /// Get the primitive of <paramref name="element"/> according to vertex's id.
        /// <para>Returns <code>null</code> if <paramref name="element"/> is null or <paramref name="stageVertexID"/> is not in the range of this <paramref name="element"/>.</para>
        /// <para>Note: the <paramref name="stageVertexID"/> Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</para>
        /// <para>Note: The result's positions property is not set up as there will be different kinds of storage mode for positions(float[], IntPtr, etc). You have to initialize the positions property and fill correct position information afterwards.</para>
        /// </summary>
        /// <typeparam name="T">Subclass of <see cref="PickedPrimitiveBase"/></typeparam>
        /// <param name="element">the scene's element that contains the primitive.</param>
        /// <param name="mode">specifies what type of primitive it is.</param>
        /// <param name="stageVertexID">Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</param>
        /// <returns></returns>
        public static T TryPick<T>(
            this IColorCodedPicking element, Enumerations.BeginMode mode, int stageVertexID)
            where T : PickedPrimitiveBase, new()
        {
            T primitive = null;

            if (element != null)
            {
                int lastVertexID = element.GetLastVertexIDOfPickedPrimitive(stageVertexID);
                if (lastVertexID >= 0)
                {
                    primitive = new T();

                    primitive.GeometryType = mode.ToGeometryType();
                    primitive.StageVertexID = stageVertexID;
                    primitive.Element = element;
                }
            }

            return primitive;
        }

        /// <summary>
        /// Get the primitive of <paramref name="element"/> according to vertex's id.
        /// <para>Returns <code>null</code> if <paramref name="element"/> is null or <paramref name="stageVertexID"/> does not belong to any of this <paramref name="element"/>'s vertices.</para>
        /// <para>Note: the <paramref name="stageVertexID"/> refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</para>
        /// </summary>
        /// <typeparam name="T">Subclass of <see cref="PickedPrimitiveBase"/></typeparam>
        /// <param name="element">the scene's element that contains the primitive.</param>
        /// <param name="mode">specifies what type of primitive it is.</param>
        /// <param name="stageVertexID">Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</param>
        /// <param name="positions">element's vertices' position array.</param>
        /// <returns></returns>
        public static T TryPick<T>(
            this IColorCodedPicking element, Enumerations.BeginMode mode, int stageVertexID, float[] positions)
            where T : PickedPrimitiveBase, new()
        {
            if (positions == null) { throw new ArgumentNullException("positions"); }

            T primitive = element.TryPick<T>(mode, stageVertexID);

            // Fill primitive's positions and colors. This maybe changes much more than lines above in second dev.
            if (primitive != null)
            {
                int lastVertexID = element.GetLastVertexIDOfPickedPrimitive(stageVertexID);
                if (lastVertexID >= 0)
                {
                    int vertexCount = primitive.GeometryType.GetVertexCount();
                    if (vertexCount == -1) { vertexCount = positions.Length / 3; }
                    float[] primitivPositions = new float[vertexCount * 3];
                    for (int i = lastVertexID * 3 + 2, j = primitivPositions.Length - 1; j >= 0; i--, j--)
                    {
                        if (i < 0)
                        { i += positions.Length; }
                        primitivPositions[j] = positions[i];
                    }

                    primitive.positions = primitivPositions;
                }
            }

            return primitive;
        }
    }
}
