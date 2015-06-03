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

            if (element.PickingBaseID + element.GetVertexCount() <= stageVertexID) // ID is in some subsequent element.
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
        /// <typeparam name="T">Subclass of <see cref="PickedGeometryBase"/></typeparam>
        /// <param name="element">the scene's element that contains the primitive.</param>
        /// <param name="mode">specifies what type of primitive it is.</param>
        /// <param name="stageVertexID">Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</param>
        /// <returns></returns>
        public static T TryPick<T>(
            this IColorCodedPicking element, Enumerations.BeginMode mode, int stageVertexID)
            where T : PickedGeometryBase, new()
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
        /// <typeparam name="T">Subclass of <see cref="PickedGeometryBase"/></typeparam>
        /// <param name="element">the scene's element that contains the primitive.</param>
        /// <param name="mode">specifies what type of primitive it is.</param>
        /// <param name="stageVertexID">Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</param>
        /// <param name="positions">element's vertices' position array.</param>
        /// <returns></returns>
        public static T TryPick<T>(
            this IColorCodedPicking element, Enumerations.BeginMode mode, int stageVertexID, float[] positions)
            where T : PickedGeometryBase, new()
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


        /// <summary>
        /// Get primitive's index(start from 0) according to <paramref name="lastVertexID"/> and <paramref name="mode"/>.
        /// <para>Returns -1, -2 if failed.</para>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="mode"></param>
        /// <param name="lastVertexID">Refers to the last vertex that constructs the primitive.
        /// <para>Ranges from 0 to (<paramref name="element"/>'s vertices' count - 1).</para></param>
        /// <returns></returns>
        public static int GetPrimitiveIndex(this IColorCodedPicking element, SharpGL.Enumerations.BeginMode mode, int lastVertexID)
        {
            int result = -1;
            if (element == null || lastVertexID < 0) { return result; }

            int vertexCount = element.GetVertexCount();

            if (lastVertexID < vertexCount)
            {
                switch (mode)
                {
                    case SharpGL.Enumerations.BeginMode.Points:
                        // vertexID should range from 0 to vertexCount - 1.
                        result = lastVertexID;
                        break;
                    case SharpGL.Enumerations.BeginMode.Lines:
                        // vertexID should range from 0 to vertexCount - 1.
                        result = lastVertexID / 2;
                        break;
                    case SharpGL.Enumerations.BeginMode.LineLoop:
                        // vertexID should range from 0 to vertexCount.
                        if (lastVertexID == 0) // This is the last primitive.
                        { result = vertexCount - 1; }
                        else
                        { result = lastVertexID - 1; }
                        break;
                    case SharpGL.Enumerations.BeginMode.LineStrip:
                        result = lastVertexID - 1;// If lastVertexID is 0, this returns -1.
                        break;
                    case SharpGL.Enumerations.BeginMode.Triangles:
                        result = lastVertexID / 3;
                        break;
                    case SharpGL.Enumerations.BeginMode.TriangleString:
                        result = lastVertexID - 2;// if lastVertexID is 0 or 1, this returns -2 or -1.
                        break;
                    case SharpGL.Enumerations.BeginMode.TriangleFan:
                        result = lastVertexID - 2;// if lastVertexID is 0 or 1, this returns -2 or -1.
                        break;
                    case SharpGL.Enumerations.BeginMode.Quads:
                        result = lastVertexID / 4;
                        break;
                    case SharpGL.Enumerations.BeginMode.QuadStrip:
                        result = lastVertexID / 2 - 1;// If lastVertexID is 0 or 1, this returns -1.
                        break;
                    case SharpGL.Enumerations.BeginMode.Polygon:
                        result = 0;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return result;
        }

        /// <summary>
        /// Get primitive's count according to specified <paramref name="mode"/> and <paramref name="vertexCount"/>.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="vertexCount"></param>
        /// <returns></returns>
        public static int GetPrimitiveCount(this IColorCodedPicking element, SharpGL.Enumerations.BeginMode mode)
        {
            int result = 0;

            if (element != null)
            {
                int vertexCount = element.GetVertexCount();

                if (vertexCount > 0)
                {
                    switch (mode)
                    {
                        case SharpGL.Enumerations.BeginMode.Points:
                            result = vertexCount;
                            break;
                        case SharpGL.Enumerations.BeginMode.Lines:
                            result = vertexCount / 2;
                            break;
                        case SharpGL.Enumerations.BeginMode.LineLoop:
                            result = vertexCount;
                            break;
                        case SharpGL.Enumerations.BeginMode.LineStrip:
                            result = vertexCount - 1;
                            break;
                        case SharpGL.Enumerations.BeginMode.Triangles:
                            result = vertexCount / 3;
                            break;
                        case SharpGL.Enumerations.BeginMode.TriangleString:
                            result = vertexCount - 2;
                            break;
                        case SharpGL.Enumerations.BeginMode.TriangleFan:
                            result = vertexCount - 2;
                            break;
                        case SharpGL.Enumerations.BeginMode.Quads:
                            result = vertexCount / 4;
                            break;
                        case SharpGL.Enumerations.BeginMode.QuadStrip:
                            result = vertexCount / 2 - 1;
                            break;
                        case SharpGL.Enumerations.BeginMode.Polygon:
                            result = 1;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            return result;
        }
    }
}
