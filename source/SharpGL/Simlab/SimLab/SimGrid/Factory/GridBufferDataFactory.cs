using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.GridSource.Factory
{

    /// <summary>
    /// GridderSource 用于创建三维可视对象的抽象
    /// </summary>
    public abstract class GridBufferDataFactory
    {

        protected Vertex MinVertex(Vertex min, Vertex value)
        {
            if (min.X > value.X)
                min.X = value.X;
            if (min.Y > value.Y)
                min.Y = value.Y;
            if (min.Z > value.Z)
                min.Z = value.Z;
            return min;
        }

        protected Vertex MaxVertex(Vertex max, Vertex value)
        {
            if (max.X < value.X)
                max.X = value.X;
            if (max.Y < value.Y)
                max.Y = value.Y;
            if (max.Z < value.Z)
                max.Z = value.Z;
            return max;
        }

        /// <summary>
        /// 通过网格数据源生成
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public abstract MeshBase CreateMesh(GridderSource source);

        public abstract TextureCoordinatesBufferData CreateTextureCoordinates(GridderSource source, int[] gridIndexes, float[] values, float minValue, float maxValue);

    }
}
