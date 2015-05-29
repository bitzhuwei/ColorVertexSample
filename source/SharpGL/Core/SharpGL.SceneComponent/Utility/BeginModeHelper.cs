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
    }
}
