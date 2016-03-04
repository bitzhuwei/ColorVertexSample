using SharpGL;
using SharpGL.SceneComponent;
using SimLab.Geometry;
using SimLab.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{

    /// <summary>
    /// 描述由线段组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public abstract class FractureTexCoordBuffer : TexCoordBuffer
    {

    }

    /// <summary>
    /// 描述由线段组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public sealed class LineFractureTexCoordBufer : FractureTexCoordBuffer
    {
        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
            return new UnmanagedArray<LineTexCoord>(elementCount);
        }

    }

    /// <summary>
    /// 描述由三角形组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public sealed class TriangleFractureTexCoordBuffer : FractureTexCoordBuffer
    {
        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
            return new UnmanagedArray<TriangleTexCoord>(elementCount);
        }

    }


    /// <summary>
    /// 描述由四边形组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public sealed class QuadFractureTexCoordBuffer : FractureTexCoordBuffer
    {
        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
            return new UnmanagedArray<QuadTexCoord>(elementCount);
        }

    }

}
