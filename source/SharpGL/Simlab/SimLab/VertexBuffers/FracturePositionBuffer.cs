using SharpGL;
using SharpGL.SceneComponent;
using SimLab.SimGrid;
using SimLab.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{
    /// <summary>
    /// 描述裂缝(fracture)的顶点的位置。
    /// </summary>
    public abstract class FracturePositionBuffer : PositionBuffer
    {
        public abstract FractureFormat Shape { get; }
    }

    /// <summary>
    /// 描述由三角形组成的裂缝(fracture)的顶点的位置。
    /// </summary>
    public class TriangleFracturePositionBuffer : FracturePositionBuffer
    {
        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
            return new UnmanagedArray<TrianglePosition>(elementCount);
        }

        public override FractureFormat Shape
        {
            get { return FractureFormat.Triange; }
        }
    }

    /// <summary>
    /// 描述由线段组成的裂缝(fracture)的顶点的位置。
    /// </summary>
    public class LineFracturePositionBuffer : FracturePositionBuffer
    {
        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
            return new UnmanagedArray<LinePosition>(elementCount);
        }

        public override FractureFormat Shape
        {
            get { return FractureFormat.Line; }
        }
    }

    /// <summary>
    /// 裂缝的类型
    /// </summary>
    public enum FractureFormat
    {
        Line = DynamicUnstructuredGridderSource.FRACTURE_FORMAT2_LINE,
        Triange = DynamicUnstructuredGridderSource.FRACTURE_FORMAT3_TRIANGLE,
    }
}
