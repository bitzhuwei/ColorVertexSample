using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public static class PrimitiveTypeHelper
    {
        /// <summary>
        /// Get vertex count of specified primitive's type.
        /// <para>returns -1 if type if <see cref="PrimitiveType.Polygon"/>.</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetVertexCount(PrimitiveType type)
        {
            int result = -1;

            switch (type)
            {
                case PrimitiveType.Point:
                    result = 1;
                    break;
                case PrimitiveType.Line:
                    result = 2;
                    break;
                case PrimitiveType.Triangle:
                    result = 3;
                    break;
                case PrimitiveType.Quad:
                    result = 4;
                    break;
                case PrimitiveType.Polygon:
                    result = -1;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }
    }
}
