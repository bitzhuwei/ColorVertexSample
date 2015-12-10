using SharpGL.SceneComponent;
using SimLab2.SimGrid;
using SimLab2.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
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

        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<TrianglePosition>(elementCount);
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

        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<TetrahedronPosition>(elementCount);
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
