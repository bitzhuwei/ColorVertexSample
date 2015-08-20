using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.GLPrimitive;

namespace YieldingGeometryModel.Builder
{

    public class MeshGeometry : TriangleMesh, IDisposable
    {

        /// <summary>
        /// 颜色
        /// </summary>
        public override UnmanagedArray<ColorF> VertexColors { get; set; }

        /// <summary>
        /// 点的集合
        /// </summary>
        public override UnmanagedArray<Vertex> Vertexes { get; set; }

        public override UnmanagedArray<uint> StripTriangles { get; set; }

        public override Vertex Min { get; set; }

        public override Vertex Max { get; set; }

        /// <summary>
        /// 控制网格是否显示，大小为DimenSize
        /// </summary>
        public override UnmanagedArray<float> Visibles { get; set; }

        /// <summary>
        /// Dispose data
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Free()
        {

            if (Vertexes != null)
            {
                Vertexes.Dispose();
                Vertexes = null;
            }


            if (VertexColors != null)
            {
                VertexColors.Dispose();
                VertexColors = null;
            }

            if (StripTriangles != null)
            {
                StripTriangles.Dispose();
            }

            if (Visibles != null)
            {
                Visibles.Dispose();
                Visibles = null;
            }
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.

                }
                this.Free();
                this.disposed = true;
            }
        }
        private bool disposed = false;

    }

    public class HexahedronGridderHelper
    {
        public const int HEXAHEDRON_VERTEX_COUNT = 16;

        public static void MinMax(Vertex value, ref Vertex min, ref Vertex max)
        {

            if (value.X < min.X)
                min.X = value.X;
            if (value.Y < min.Y)
                min.Y = value.Y;
            if (value.Z < min.Z)
                min.Z = value.Z;

            if (value.X > max.X)
                max.X = value.X;
            if (value.Y > max.Y)
                max.Y = value.Y;
            if (value.Z > max.Z)
                max.Z = value.Z;
        }

        /// <summary>
        /// 随机生成size个网格上的属性
        /// </summary>
        /// <param name="size"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="gridIndexes"></param>
        /// <param name="gridValues"></param>
        public static void RandomValue(int size, int minValue, int maxValue, out int[] gridIndexes, out float[] gridValues)
        {
            Random random = new Random();
            gridIndexes = new int[size];
            gridValues = new float[size];
            for (int i = 0; i < size; i++)
            {
                float value = random.Next(minValue, maxValue) * 1.0f;
                gridIndexes[i] = i;
                gridValues[i] = value;
            }
        }

        /// <summary>
        /// visibles 如果属性无颜色，修改为不可见
        /// </summary>
        /// <param name="source"></param>
        /// <param name="gridIndexes"></param>
        /// <param name="colors"></param>
        /// <param name="visibles"></param>
        /// <returns></returns>
        public static UnmanagedArray<ColorF> FromColors(HexahedronGridderSource source, int[] gridIndexes, ColorF[] colors, UnmanagedArray<float> visibles)
        {
            UnmanagedArray<ColorF> colorArray = new UnmanagedArray<ColorF>(source.DimenSize * HEXAHEDRON_VERTEX_COUNT);
            UnmanagedArray<byte> hasColorArray = new UnmanagedArray<byte>(source.DimenSize);
            unsafe
            {

                //是否有颜色

                int i = 0;
                for (i = 0; i < gridIndexes.Length; i++)
                {
                    int gridIndex = gridIndexes[i];
                    //*hasColorArray[gridIndex] = 1;
                    hasColorArray[gridIndex] = 1;
                    int cellOffset = gridIndex * HEXAHEDRON_VERTEX_COUNT;
                    ColorF cf = colors[i];

                    colorArray[cellOffset + 0] = cf;
                    colorArray[cellOffset + 1] = cf;
                    colorArray[cellOffset + 2] = cf;
                    colorArray[cellOffset + 3] = cf;
                    colorArray[cellOffset + 4] = cf;
                    colorArray[cellOffset + 5] = cf;
                    colorArray[cellOffset + 6] = cf;
                    colorArray[cellOffset + 7] = cf;
                    colorArray[cellOffset + 8] = cf;
                    colorArray[cellOffset + 9] = cf;
                    colorArray[cellOffset + 10] = cf;
                    colorArray[cellOffset + 11] = cf;
                    colorArray[cellOffset + 12] = cf;
                    colorArray[cellOffset + 13] = cf;
                    colorArray[cellOffset + 14] = cf;
                    colorArray[cellOffset + 15] = cf;
                }
                int j, k;
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    int cellOffset = gridIndex * HEXAHEDRON_VERTEX_COUNT;
                    source.InvertIJK(gridIndex, out i, out j, out k);

                    //无颜色，不可见
                    if (hasColorArray[gridIndex] == 0)
                    {
                        visibles[cellOffset + 0] = 0;
                        visibles[cellOffset + 1] = 0;
                        visibles[cellOffset + 2] = 0;
                        visibles[cellOffset + 3] = 0;
                        visibles[cellOffset + 4] = 0;
                        visibles[cellOffset + 5] = 0;
                        visibles[cellOffset + 6] = 0;
                        visibles[cellOffset + 7] = 0;
                        visibles[cellOffset + 8] = 0;
                        visibles[cellOffset + 9] = 0;
                        visibles[cellOffset + 10] = 0;
                        visibles[cellOffset + 11] = 0;
                        visibles[cellOffset + 12] = 0;
                        visibles[cellOffset + 13] = 0;
                        visibles[cellOffset + 14] = 0;
                        visibles[cellOffset + 15] = 0;
                    }

                }

            }
            hasColorArray.Dispose();
            return colorArray;
        }


        /// <summary>
        /// 确定网格几何可见，几何不可见
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static UnmanagedArray<float> GridVisibleFromActive(HexahedronGridderSource source)
        {
            UnmanagedArray<float> visibles = new UnmanagedArray<float>(source.DimenSize * HEXAHEDRON_VERTEX_COUNT);
            unsafe
            {
                int i, j, k;
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    source.InvertIJK(gridIndex, out i, out j, out k);
                    int visible = source.IsActiveBlock(i, j, k) == true ? 1 : 0;
                    if (visible > 0)
                    {
                        visible = source.IsSliceBlock(i, j, k) == true ? 1 : 0;
                    }
                    int offset = gridIndex * HEXAHEDRON_VERTEX_COUNT;
                    visibles[offset + 0] = visible;
                    visibles[offset + 1] = visible;
                    visibles[offset + 2] = visible;
                    visibles[offset + 3] = visible;
                    visibles[offset + 4] = visible;
                    visibles[offset + 5] = visible;
                    visibles[offset + 6] = visible;
                    visibles[offset + 7] = visible;
                    visibles[offset + 8] = visible;
                    visibles[offset + 9] = visible;
                    visibles[offset + 10] = visible;
                    visibles[offset + 11] = visible;
                    visibles[offset + 12] = visible;
                    visibles[offset + 13] = visible;
                    visibles[offset + 14] = visible;
                    visibles[offset + 15] = visible;
                }
            }
            return visibles;
        }

        



        public static MeshGeometry CreateMesh(HexahedronGridderSource source)
        {

            if (source.DimenSize <= 0)
                return null;

            //每个六面体有8个点
            UnmanagedArray<Vertex> vertexes = new UnmanagedArray<Vertex>(source.DimenSize * HEXAHEDRON_VERTEX_COUNT);

            //是否六面体是否可见
            UnmanagedArray<float> visibles = new UnmanagedArray<float>(source.DimenSize * HEXAHEDRON_VERTEX_COUNT);

            Vertex min = new Vertex();
            Vertex max = new Vertex();
            min.X = 0; min.Y = 0; min.Z = 0;
            max.X = 1; max.Y = 1; max.Z = -1;

            unsafe
            {
                int i, j, k;
                bool assigned = false;
                //顺序很重要
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    source.InvertIJK(gridIndex, out i, out j, out k);
                    Vertex p0 = source.PointFLT(i, j, k);
                    Vertex p1 = source.PointFRT(i, j, k);
                    Vertex p2 = source.PointFLB(i, j, k);
                    Vertex p3 = source.PointFRB(i, j, k);
                    Vertex p4 = source.PointBLT(i, j, k);
                    Vertex p5 = source.PointBRT(i, j, k);
                    Vertex p6 = source.PointBLB(i, j, k);
                    Vertex p7 = source.PointBRB(i, j, k);
                    if (source.IsActiveBlock(i, j, k))
                    {
                        if (!assigned)
                        {
                            assigned = true;
                            min = p0;
                            max = p0;
                        }
                        MinMax(p0, ref min, ref max);
                        MinMax(p1, ref min, ref max);
                        MinMax(p2, ref min, ref max);
                        MinMax(p3, ref min, ref max);
                        MinMax(p4, ref min, ref max);
                        MinMax(p5, ref min, ref max);
                        MinMax(p6, ref min, ref max);
                        MinMax(p7, ref min, ref max);
                    }

                    int cellOffset = gridIndex * HEXAHEDRON_VERTEX_COUNT;

                    vertexes[cellOffset + 0] = p0;
                    vertexes[cellOffset + 1] = p4;
                    vertexes[cellOffset + 2] = p1;
                    vertexes[cellOffset + 3] = p5;
                    vertexes[cellOffset + 4] = p3;
                    vertexes[cellOffset + 5] = p7;
                    vertexes[cellOffset + 6] = p2;
                    vertexes[cellOffset + 7] = p6;
                    vertexes[cellOffset + 8] = p1;
                    vertexes[cellOffset + 9] = p3;
                    vertexes[cellOffset + 10] = p0;
                    vertexes[cellOffset + 11] = p2;
                    vertexes[cellOffset + 12] = p4;
                    vertexes[cellOffset + 13] = p6;
                    vertexes[cellOffset + 14] = p5;
                    vertexes[cellOffset + 15] = p7;

                    float visible = source.IsActiveBlock(i, j, k) == true ? 1.0f : 0;
                    if (visible > 0)
                    {
                        visible =source.IsSliceBlock(i, j, k) == true ? 1.0f : 0;
                    }

                    
                    visibles[cellOffset + 0] = visible;
                    visibles[cellOffset + 1] = visible;
                    visibles[cellOffset + 2] = visible;
                    visibles[cellOffset + 3] = visible;
                    visibles[cellOffset + 4] = visible;
                    visibles[cellOffset + 5] = visible;
                    visibles[cellOffset + 6] = visible;
                    visibles[cellOffset + 7] = visible;
                    visibles[cellOffset + 8] = visible;
                    visibles[cellOffset + 9] = visible;
                    visibles[cellOffset + 10] = visible;
                    visibles[cellOffset + 11] = visible;
                    visibles[cellOffset + 12] = visible;
                    visibles[cellOffset + 13] = visible;
                    visibles[cellOffset + 14] = visible;
                    visibles[cellOffset + 15] = visible;

                }
            }

            MeshGeometry mesh = new MeshGeometry();
            mesh.Vertexes = vertexes;
            mesh.Min = min;
            mesh.Max = max;
            mesh.Visibles = visibles;

            return mesh;
        }

    }


    public static class HexahedronGridderBuilder
    {


        /// <summary>
        /// 依次获取网格内的所有六面体。
        /// Get hexahedrons of gridder from specified source successively.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Hexahedron>
            GetGridderCells(this HexahedronGridderSource source)
        {
            Random random = new Random();

            int NI = source.NX;
            int NJ = source.NY;
            int NK = source.NZ;

            int total = NI * NJ * NK;

            //Hexahedron[] cells = new Hexahedron[total];
            //Dictionary<int, int> gridCellMap = new Dictionary<int, int>();
            for (int index = 0; index < total; index++)
            {
                int i, j, k;
                source.InvertIJK(index, out i, out j, out k);
                //int indexOfArray = index;
                //gridCellMap[index] = indexOfArray;

                Hexahedron cell = new Hexahedron();
                cell.flt = source.PointFLT(i, j, k);
                cell.frt = source.PointFRT(i, j, k);
                cell.flb = source.PointFLB(i, j, k);
                cell.frb = source.PointFRB(i, j, k);
                cell.blt = source.PointBLT(i, j, k);
                cell.brt = source.PointBRT(i, j, k);
                cell.blb = source.PointBLB(i, j, k);
                cell.brb = source.PointBRB(i, j, k);
                //cell.gridIndex = index;

                // set random color for now
                //cell.color = new SharpGL.SceneGraph.GLColor((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                //float r = (float)random.Next(0, int.MaxValue) / (float)int.MaxValue;
                //float g = (float)random.Next(0, int.MaxValue) / (float)int.MaxValue;
                //float b = (float)random.Next(0, int.MaxValue) / (float)int.MaxValue;
                //float a = (float)random.Next(0, int.MaxValue) / (float)int.MaxValue;
                //cell.color = new SharpGL.SceneGraph.GLColor(r, g, b, a);
                byte[] bytes = new byte[4];
                random.NextBytes(bytes);
                cell.color = new SharpGL.SceneGraph.GLColor(
                    (0.0f + bytes[0]) / byte.MaxValue,
                    (0.0f + bytes[1]) / byte.MaxValue,
                    (0.0f + bytes[2]) / byte.MaxValue,
                    (0.0f + bytes[3]) / byte.MaxValue);

                yield return cell;
                //cells[index] = cell;
            }
            //HexahedronGridder gridder = new HexahedronGridder();
            //gridder.Cells = cells;
            //gridder.IJKCellsMap = gridCellMap;
            //return gridder;

        }

    }


}
