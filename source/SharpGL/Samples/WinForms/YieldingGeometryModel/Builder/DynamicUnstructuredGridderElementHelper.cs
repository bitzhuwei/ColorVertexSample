using GlmNet;
using SharpGL.SceneComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.DataSource;
using YieldingGeometryModel.loader;

namespace YieldingGeometryModel.Builder
{
    public class DynamicUnstructuredGridderElementHelper
    {


        public unsafe static UnmanagedArray<float> BuildFracturesVertexVisibles(DynamicUnstructuredGridderSource source, int[] gridIndexes, ColorF[] mixedColors)
        {
            int fractureCount = source.FractureNum;

            int fractureVertexCount = 0;
            if (source.FractureFormat == DynamicUnstructureGeometryLoader.FRACTURE_FORMAT3_TRIANGLE)
                fractureVertexCount = 3;
            else if (source.FractureFormat == DynamicUnstructureGeometryLoader.FRACTURE_FORMAT2_LINE)
                fractureVertexCount = 2;
            else
                throw new ArgumentException("Fracture Format Error");

            int totalVertex = fractureCount*fractureVertexCount;
            UnmanagedArray<float> vertexVisibles = new UnmanagedArray<float>(totalVertex);
            int fractureStartIndex = 0;
            int fractureEndIndex = source.FractureNum - 1;

            int[] activeCells = source.ActNums;
            //initialize elements geometry visibles;
            float* vv = (float*)vertexVisibles.FirstElement();
            byte[] fractureVisibles = new byte[fractureCount];
            byte[] colorVisibles    = new byte[fractureCount];

            byte visible = 1;
            for (int fracIndex = fractureStartIndex; fracIndex <= fractureEndIndex; fracIndex++)
            {
                if (activeCells == null || activeCells.Length <= 0)
                    visible = 1;
                else
                {
                    if (activeCells[fracIndex] >0)
                        visible = 1;
                    else
                        visible = 0;
                }
                fractureVisibles[fracIndex - fractureStartIndex] = visible;
                colorVisibles[fracIndex - fractureStartIndex] = 0;
            }

            for (int index = 0; index < gridIndexes.Length; index++)
            {
                int fracIndex = gridIndexes[index];
                if (fracIndex >= fractureStartIndex && fracIndex <= fractureEndIndex)
                {
                    colorVisibles[fracIndex - fractureStartIndex] = 1;
                }
            }

            for (int fracIndex = 0; fracIndex < fractureCount; fracIndex++)
            {
                float vecVisible;
                if (fractureVisibles[fracIndex] > 0 && colorVisibles[fracIndex] > 0)
                {
                    vecVisible = 1.0f;
                }
                else
                {
                    vecVisible = 0.0f;
                }
                int vertexIndexOffset = fracIndex * fractureVertexCount;
                for (int vexIndex = 0; vexIndex < fractureVertexCount; vexIndex++)
                {
                    vv[vertexIndexOffset + vexIndex] = vecVisible;
                }
            }
            return vertexVisibles;

        }

        public unsafe static UnmanagedArray<float> BuildElementsVertexVisibles(DynamicUnstructuredGridderSource source, int[] gridIndexes, ColorF[] mixedColors)
        {
             
            int elementCount = source.ElementNum;

            int elementVertexCount = DynamicUnstructureGeometryLoader.ElEMENT_FORMAT3_TRIANGLE;
            if (source.ElementFormat == DynamicUnstructureGeometryLoader.ElEMENT_FORMAT3_TRIANGLE)
                elementVertexCount = 3;
            else if (source.ElementFormat == DynamicUnstructureGeometryLoader.ELEMENT_FORMAT4_TETRAHEDRON)
                elementVertexCount = 6;
            else
                throw new ArgumentException("format error");

            int totalVertex = elementCount * elementVertexCount;
            UnmanagedArray<float> vertexVisibles = new UnmanagedArray<float>(totalVertex);
               
            int elementStartIndex = source.FractureNum;
            int elementendIndex   = source.DimenSize - 1;
                //tranverse all the elements to decide
            int[] activeCells = source.ActNums;
            //initialize elements geometry visibles;
            float* vv = (float*)vertexVisibles.FirstElement();
            byte[] elementVisibles = new byte[elementCount];
            byte[] colorVisibles =   new byte[elementCount];

            byte visible = 1;
            for (int elementIndex = elementStartIndex; elementIndex <= elementendIndex; elementIndex++)
            {
                if (activeCells == null || activeCells.Length <= 0)
                    visible = 1;
                else
                {
                   if (activeCells[elementIndex] > 0.5)
                      visible = 1;
                   else
                      visible = 0;
                }
                elementVisibles[elementIndex - elementStartIndex] = visible;
                colorVisibles[elementIndex - elementStartIndex] = 0;
            }

            for (int index = 0; index < gridIndexes.Length; index++)
            {
                int elementIndex = gridIndexes[index];
                if (elementIndex >= elementStartIndex && elementIndex <= elementendIndex)
                {
                    colorVisibles[elementIndex - elementStartIndex] = 1;
                }
            }

            for (int index = 0; index < elementCount; index++)
            {
                float vecVisible;
                
                if (elementVisibles[index] > 0 && colorVisibles[index] > 0)
                {
                    vecVisible = 1.0f;
                }
                else
                {
                    vecVisible = 0.0f;
                }
                int vertexIndexOffset = index * elementVertexCount;
                for (int vexIndex = 0; vexIndex < elementVertexCount; vexIndex++)
                {
                    vv[vertexIndexOffset + vexIndex] = vecVisible;
                }
            }
            return vertexVisibles;
        }
      
        /// <summary>
        /// 帮助生成基质点的颜色
        /// </summary>
        /// <param name="source">无结构网格数据源</param>
        /// <param name="gridIndexes">网格中的位置</param>
        /// <param name="mixedColors">基质，断层，裂缝的颜色</param>
        /// <returns></returns>
        public unsafe static UnmanagedArray<vec4> BuildElementColors(DynamicUnstructuredGridderSource source,int[] gridIndexes, ColorF[] mixedColors){

            int elementVertexCount;
            if (source.ElementFormat == DynamicUnstructureGeometryLoader.ELEMENT_FORMAT4_TETRAHEDRON)
                elementVertexCount = 6;
            else if (source.ElementFormat == DynamicUnstructureGeometryLoader.ElEMENT_FORMAT3_TRIANGLE)
                elementVertexCount = 3;
            else
                throw new NotImplementedException("Unsupported Format");
            int elemStartIndex = source.FractureNum;
            int elemStopIndex = source.DimenSize - 1;
            int elementCount = source.ElementNum;
            UnmanagedArray<vec4> vertexColors = new UnmanagedArray<vec4>(elementCount * elementVertexCount);
            vec4* vexColors = (vec4*)vertexColors.FirstElement();
            for (int mixIndex = 0; mixIndex < gridIndexes.Length; mixIndex++)
            {
                int gridIndex = gridIndexes[mixIndex];
                if (gridIndex >= elemStartIndex && gridIndex <= elemStopIndex)
                {

                    ColorF triangleColor = mixedColors[mixIndex];
                    vec4 vertexColor;
                    vertexColor.x = triangleColor.R;
                    vertexColor.y = triangleColor.G;
                    vertexColor.z = triangleColor.B;
                    vertexColor.w = triangleColor.A;

                    int elementIndex = gridIndex - elemStartIndex;
                    int eleVertexOffset = elementIndex * elementVertexCount;
                    for (int vindex = 0; vindex < elementVertexCount; vindex++)
                    {
                        vexColors[eleVertexOffset + vindex] = vertexColor;
                    }
                }      
            }
            return vertexColors;
        }

        public unsafe static UnmanagedArray<vec4> BuildFractureColors(DynamicUnstructuredGridderSource source, int[] gridIndexes, ColorF[] mixedColors)
        {
            int fractureVertexCount;
            if (source.FractureFormat == DynamicUnstructureGeometryLoader.FRACTURE_FORMAT3_TRIANGLE)
                fractureVertexCount = 3;
            else if (source.FractureFormat == DynamicUnstructureGeometryLoader.FRACTURE_FORMAT2_LINE)
                fractureVertexCount = 2;
            else
                throw new NotImplementedException("Unsupported format");
            int fractureGridStartIndex = 0;
            int fractureGridStopIndex = source.FractureNum - 1;
            int fracturesCount = source.FractureNum;
            UnmanagedArray<vec4> vertexColors = new UnmanagedArray<vec4>(fracturesCount * fractureVertexCount);
            vec4* vecColors = (vec4*)vertexColors.FirstElement();
            for (int mixIndex = 0; mixIndex < gridIndexes.Length; mixIndex++)
            {
                int gridIndex = gridIndexes[mixIndex];
                if (gridIndex >= fractureGridStartIndex && gridIndex <= fractureGridStopIndex)
                {
                    ColorF fractureColor = mixedColors[mixIndex];
                    vec4 vertexColor;
                    vertexColor.x = fractureColor.R;
                    vertexColor.y = fractureColor.G;
                    vertexColor.z = fractureColor.B;
                    vertexColor.w = fractureColor.A;

                    int fractureIndex = gridIndex - fractureGridStartIndex;
                    int fractureVertexOffset = fractureIndex * fractureVertexCount;
                    for (int vindex = 0; vindex < fractureVertexCount; vindex++)
                    {
                        vecColors[fractureVertexOffset + vindex] = vertexColor;
                    }
                }

                if (gridIndex > fractureGridStopIndex)
                    break;
            }
            return  vertexColors;   
        }
        

    }
}
