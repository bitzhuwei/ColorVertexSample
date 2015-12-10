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
    /// 描述Matrix的顶点的位置。
    /// </summary>
    public abstract class MatrixPositionBuffer : PositionBuffer
    {
        /// <summary>
        /// 组成基质的形状，四面体或者三角形。
        /// </summary>
        public abstract MatrixFormat Shape { get; }

    }

    /// <summary>
    /// 描述由三角形组成的Matrix的顶点的位置。
    /// </summary>
    public class TriangleMatrixPositionBuffer : MatrixPositionBuffer
    {
        public override MatrixFormat Shape
        {
            get { return MatrixFormat.Triangle; }
        }

        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
             return new UnmanagedArray<TrianglePosition>(elementCount);
        }
    }

    /// <summary>
    /// 描述由四面体组成的Matrix的顶点的位置。
    /// </summary>
    public class TetrahedronMatrixPositionBuffer : MatrixPositionBuffer
    {
        public override MatrixFormat Shape
        {
            get { return MatrixFormat.Tetrahedron; }
        }

        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
             return new UnmanagedArray<TetrahedronPosition>(elementCount);
        }
    }

    /// <summary>
    /// Matrix的格式。
    /// </summary>
    public enum MatrixFormat
    {
        Triangle = DynamicUnstructuredGridderSource.MATRIX_FORMAT3_TRIANGLE,
        Tetrahedron = DynamicUnstructuredGridderSource.MATRIX_FORMAT4_TETRAHEDRON,
    }
}
