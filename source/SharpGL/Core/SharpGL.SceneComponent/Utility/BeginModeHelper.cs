using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public static class BeginModeHelper
    {

        /// <summary>
        /// Convert <see cref="BeginMode"/> to <see cref="<PrimitiveType"/>.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static PrimitiveType ToPrimitiveType(SharpGL.Enumerations.BeginMode mode)
        {
            PrimitiveType result = PrimitiveType.Point;
            switch (mode)
            {
                case SharpGL.Enumerations.BeginMode.Points:
                    result = PrimitiveType.Point;
                    break;
                case SharpGL.Enumerations.BeginMode.Lines:
                    result = PrimitiveType.Line;
                    break;
                case SharpGL.Enumerations.BeginMode.LineLoop:
                    result = PrimitiveType.Line;
                    break;
                case SharpGL.Enumerations.BeginMode.LineStrip:
                    result = PrimitiveType.Line;
                    break;
                case SharpGL.Enumerations.BeginMode.Triangles:
                    result = PrimitiveType.Triangle;
                    break;
                case SharpGL.Enumerations.BeginMode.TriangleString:
                    result = PrimitiveType.Triangle;
                    break;
                case SharpGL.Enumerations.BeginMode.TriangleFan:
                    result = PrimitiveType.Triangle;
                    break;
                case SharpGL.Enumerations.BeginMode.Quads:
                    result = PrimitiveType.Quad;
                    break;
                case SharpGL.Enumerations.BeginMode.QuadStrip:
                    result = PrimitiveType.Quad;
                    break;
                case SharpGL.Enumerations.BeginMode.Polygon:
                    result = PrimitiveType.Polygon;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }

        /// <summary>
        /// Get primitive's count according to specified <paramref name="mode"/> and <paramref name="vertexCount"/>.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="vertexCount"></param>
        /// <returns></returns>
        public static int GetPrimitiveCount(SharpGL.Enumerations.BeginMode mode, int vertexCount)
        {
            if (vertexCount <= 0) { return 0; }

            int result = 0;

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

            return result;
        }
    }
}
