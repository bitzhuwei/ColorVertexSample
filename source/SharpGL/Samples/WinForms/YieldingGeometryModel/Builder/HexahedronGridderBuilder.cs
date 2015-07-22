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

    public class MeshGeometry : TriangleMesh,IDisposable 
    {

        /// <summary>
        /// 点的集合
        /// </summary>
        private Vertex3DArray vertexes;


        /// <summary>
        /// 颜色
        /// </summary>
        private ColorArray vertexColors;



        private UIntArray triangles;

        /// <summary>
        /// 控制网格是否显示，大小为DimenSize
        /// </summary>
        private FloatArray visibles;


        private Vertex min;

        private Vertex max;


        public override Vertex3DArray Vertexes
        {
            get
            {
                return vertexes;
            }
            set
            {
                this.vertexes = value;
            }
        }

        public override UIntArray StripTriangles
        {
            get
            {
                return this.triangles;
            }
            set
            {
                this.triangles = value;
            }
        }

        public override Vertex Min
        {
            get
            {
                return this.min;
            }
            set
            {
                this.min = value;
            }
        }

        public override Vertex Max
        {
            get
            {
                return this.max;
            }
            set
            {
                this.max = value;
            }
        }

        public override FloatArray Visibles
        {
            get
            {
                return this.visibles;
            }
            set
            {
                this.visibles = value;
            }
        }




        /// <summary>
        /// Dispose data
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Free()
        {

            if (vertexes != null)
            {
                vertexes.Dispose();
                vertexes = null;
            }


            if (vertexColors != null)
            {
                vertexColors.Dispose();
                vertexColors = null;
            }

            if (triangles != null)
            {
                triangles.Dispose();
            }

            if (visibles != null)
            {
                visibles.Dispose();
                visibles = null;
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


        

        

       

       

        

        public override ColorArray VertexColors
        {
            get { return this.vertexColors; }
            set
            {
                this.vertexColors = value;
            }
        }
    }
       


    public class HexahedronGridderHelper
    {

        public const int HEXAHEDRON_TRIANGLE_STRIP = 14;

        public const int HEXAHEDRON_VERTEX_COUNT = 8;



        


        public static void MinMax(Vertex value, ref Vertex min, ref Vertex max){
           
            if(value.X < min.X)
                min.X = value.X;
            if(value.Y < min.Y)
                min.Y = value.Y;
            if(value.Z < min.Z)
                min.Z = value.Z;
            if(value.X > max.X)
                max.X = value.X;
            if(value.Y > max.Y)
                max.Y = value.Y;
            if(value.Z > max.Z)
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
        public static void RandomValue(int size, int minValue, int  maxValue, out int[] gridIndexes, out float[] gridValues)
        {
            Random random = new Random();
            gridIndexes = new int[size];
            gridValues = new float[size];
            for (int i = 0; i < size; i++)
            {
                float value = random.Next(minValue, maxValue)*1.0f;
                gridIndexes[i] = i;
                gridValues[i] = value;
            }
        }




        public static ColorArray FromColors(HexahedronGridderSource source,int[] gridIndexes, ColorF[] colors,FloatArray visibles)
        {
            ColorArray colorArray = new ColorArray(source.DimenSize*HEXAHEDRON_VERTEX_COUNT);
            ByteArray hasColorArray = new ByteArray(source.DimenSize);
            unsafe{
                
                //是否有颜色

                int i=0;
                for (i = 0; i < gridIndexes.Length; i++)
                {
                    int gridIndex = gridIndexes[i];
                    *hasColorArray[gridIndex] = 1;
                    int cellOffset = gridIndex * HEXAHEDRON_VERTEX_COUNT;
                    ColorF cf = colors[i];

                    *colorArray[cellOffset+0]= cf;
                    *colorArray[cellOffset+1]= cf;
                    *colorArray[cellOffset+2]= cf;
                    *colorArray[cellOffset+3]= cf;
                    *colorArray[cellOffset+4]= cf;
                    *colorArray[cellOffset+5]= cf;
                    *colorArray[cellOffset+6]= cf;
                    *colorArray[cellOffset+7]= cf;
                }
                int j,k;
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    int cellOffset = gridIndex * HEXAHEDRON_VERTEX_COUNT;
                    source.InvertIJK(gridIndex,out i, out j, out k);
                    if (*hasColorArray[gridIndex] == 0||!source.IsActiveBlock(i,j,k))
                    {
                        *visibles[cellOffset + 0] = 0;
                        *visibles[cellOffset + 1] = 0;
                        *visibles[cellOffset + 2] = 0;
                        *visibles[cellOffset + 3] = 0;
                        *visibles[cellOffset + 4] = 0;
                        *visibles[cellOffset + 5] = 0;
                        *visibles[cellOffset + 6] = 0;
                        *visibles[cellOffset + 7] = 0;
                    }

                }

            }
            hasColorArray.Dispose();
            return colorArray;
        }

        public static FloatArray GridVisibleFromActive(HexahedronGridderSource source)
        {
            FloatArray visibles = new FloatArray(source.DimenSize * HEXAHEDRON_VERTEX_COUNT);
            unsafe
            {
                int i, j, k;
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    source.InvertIJK(gridIndex, out i, out j, out k);
                    int visible = source.IsActiveBlock(i, j, k) == true ? 1 : 0;
                    int offset = gridIndex * HEXAHEDRON_VERTEX_COUNT;
                    *visibles[offset + 0] = visible;
                    *visibles[offset + 1] = visible;
                    *visibles[offset + 2] = visible;
                    *visibles[offset + 3] = visible;
                    *visibles[offset + 4] = visible;
                    *visibles[offset + 5] = visible;
                    *visibles[offset + 6] = visible;
                    *visibles[offset + 7] = visible;
                }
            }
            return visibles;
        }



        public static MeshGeometry CreateMesh(HexahedronGridderSource source)
        {

            if (source.DimenSize <= 0)
                return null;

             //每个六面体有8个点
            Vertex3DArray vertexes = new Vertex3DArray(source.DimenSize*HEXAHEDRON_VERTEX_COUNT);


            //是否六面体是否可见
            FloatArray visibles = new FloatArray(source.DimenSize*HEXAHEDRON_VERTEX_COUNT);

            //三角形条带索引个数
            int triangleStripIndexCount = source.DimenSize * (HEXAHEDRON_TRIANGLE_STRIP + 1);

            //三角形条带索引数组
            UIntArray triangleStripIndexes = new UIntArray(triangleStripIndexCount);

            Vertex min = new Vertex();
            Vertex max = new Vertex();
            min.X = 0; min.Y = 0; min.Z = 0;
            max.X = 1; max.Y = 1; max.Z = -1;


            unsafe
            {
                int i,j,k;
                bool assigned = false;
                //顺序很重要
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {



                    source.InvertIJK(gridIndex, out i, out j, out k);
                    Vertex p0 = source.PointFLT(i,j,k);
                    Vertex p1 = source.PointFRT(i,j,k);
                    Vertex p2 = source.PointFLB(i,j,k);
                    Vertex p3 = source.PointFRB(i,j,k);
                    Vertex p4 = source.PointBLT(i,j,k);
                    Vertex p5 = source.PointBRT(i,j,k);
                    Vertex p6 = source.PointBLB(i,j,k);
                    Vertex p7 = source.PointBRB(i,j,k);
                    if(!assigned){
                       assigned = true;
                       min = p1;
                       max = p1;
                    }
                    MinMax(p0,ref min,ref max);
                    MinMax(p1,ref min,ref max);
                    MinMax(p2,ref min,ref max);
                    MinMax(p3,ref min,ref max);
                    MinMax(p4,ref min,ref max);
                    MinMax(p5,ref min,ref max);
                    MinMax(p6,ref min,ref max);
                    MinMax(p7,ref min,ref max);

                    int cellOffset = gridIndex * HEXAHEDRON_VERTEX_COUNT;

                    *vertexes[cellOffset + 0] = (Vertex3D)p0;
                    *vertexes[cellOffset + 1] = (Vertex3D)p1;
                    *vertexes[cellOffset + 2] = (Vertex3D)p2;
                    *vertexes[cellOffset + 3] = (Vertex3D)p3;
                    *vertexes[cellOffset + 4] = (Vertex3D)p4;
                    *vertexes[cellOffset + 5] = (Vertex3D)p5;
                    *vertexes[cellOffset + 6] = (Vertex3D)p6;
                    *vertexes[cellOffset + 7] = (Vertex3D)p7;




                    float visible = source.IsActiveBlock(i, j, k) == true ? 1.0f : 0;
                    *visibles[cellOffset + 0] = visible;
                    *visibles[cellOffset + 1] = visible;
                    *visibles[cellOffset + 2] = visible;
                    *visibles[cellOffset + 3] = visible;
                    *visibles[cellOffset + 4] = visible;
                    *visibles[cellOffset + 5] = visible;
                    *visibles[cellOffset + 6] = visible;
                    *visibles[cellOffset + 7] = visible;




                    int cellTriangleOffset = (int)gridIndex * (HEXAHEDRON_TRIANGLE_STRIP + 1);
                    *triangleStripIndexes[cellTriangleOffset + 0] = (uint)cellOffset + 0;
                    *triangleStripIndexes[cellTriangleOffset + 1] = (uint)cellOffset + 2;
                    *triangleStripIndexes[cellTriangleOffset + 2] = (uint)cellOffset + 4;
                    *triangleStripIndexes[cellTriangleOffset + 3] = (uint)cellOffset + 6;
                    *triangleStripIndexes[cellTriangleOffset + 4] = (uint)cellOffset + 7;
                    *triangleStripIndexes[cellTriangleOffset + 5] = (uint)cellOffset + 2;
                    *triangleStripIndexes[cellTriangleOffset + 6] = (uint)cellOffset + 3;
                    *triangleStripIndexes[cellTriangleOffset + 7] = (uint)cellOffset + 0;
                    *triangleStripIndexes[cellTriangleOffset + 8] = (uint)cellOffset + 1;
                    *triangleStripIndexes[cellTriangleOffset + 9] = (uint)cellOffset + 4;
                    *triangleStripIndexes[cellTriangleOffset + 10] = (uint)cellOffset + 5;
                    *triangleStripIndexes[cellTriangleOffset + 11] = (uint)cellOffset + 7;
                    *triangleStripIndexes[cellTriangleOffset + 12] = (uint)cellOffset + 1;
                    *triangleStripIndexes[cellTriangleOffset + 13] = (uint)cellOffset + 3;
                    *triangleStripIndexes[cellTriangleOffset + 14] = (uint)uint.MaxValue;
                }
            }

            MeshGeometry mesh = new MeshGeometry();
            mesh.Vertexes = vertexes;
            mesh.StripTriangles = triangleStripIndexes;
            mesh.Min= min;
            mesh.Max = max;
            mesh.Visibles = visibles;

            return mesh;
        }


        /// <summary>
        /// 使用条带三角形描述描述一个网格
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static UIntArray TriangleStripFrom(HexahedronGridderSource source)
        {
            int indexCount = source.DimenSize * (HEXAHEDRON_TRIANGLE_STRIP + 1);
            UIntArray indexes = new UIntArray(indexCount);
            unsafe
            {
                for (uint gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    int cellOffset =  (int)gridIndex * (HEXAHEDRON_TRIANGLE_STRIP + 1);
                    *indexes[cellOffset + 0] = (uint)cellOffset + 0;
                    *indexes[cellOffset + 1] = (uint)cellOffset + 2;
                    *indexes[cellOffset + 2] = (uint)cellOffset + 4;
                    *indexes[cellOffset + 3] = (uint)cellOffset + 6;
                    *indexes[cellOffset + 4] = (uint)cellOffset + 7;
                    *indexes[cellOffset + 5] = (uint)cellOffset + 2;
                    *indexes[cellOffset + 6] = (uint)cellOffset + 3;
                    *indexes[cellOffset + 7] = (uint)cellOffset + 0;
                    *indexes[cellOffset + 8] = (uint)cellOffset + 1;
                    *indexes[cellOffset + 9] = (uint)cellOffset + 4;
                    *indexes[cellOffset + 10] =(uint)cellOffset + 5;
                    *indexes[cellOffset + 11] =(uint)cellOffset + 7;
                    *indexes[cellOffset + 12] =(uint)cellOffset + 1;
                    *indexes[cellOffset + 13] =(uint)cellOffset + 3;
                    *indexes[cellOffset + 14] =(uint) int.MaxValue;
                }

            }
            return indexes;
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
