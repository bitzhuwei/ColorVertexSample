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

      
        /// <summary>
        /// 帮助生成基质点的颜色
        /// </summary>
        /// <param name="source">无结构网格数据源</param>
        /// <param name="gridIndexes">网格中的位置</param>
        /// <param name="mixedColors">基质，断层，裂缝的颜色</param>
        /// <returns></returns>
        public unsafe static UnmanagedArray<vec4> BuildElementColors(DynamicUnstructuredGridderSource source,int[] gridIndexes, ColorF[] mixedColors){


            //基质的颜色是
            if (source.ElementFormat == DynamicUnstructureGeometryLoader.ElEMENT_FORMAT3_TRIANGLE)
            {
                int elementVertexCount = DynamicUnstructureGeometryLoader.ElEMENT_FORMAT3_TRIANGLE;
                //三角形时有三个点；
                //基质颜色的偏移量为 
                int colorOffset = source.FractureNum;
                int triangleCount = source.ElementNum;
                UnmanagedArray<vec4> vertexColors = new UnmanagedArray<vec4>(triangleCount * elementVertexCount);
                vec4* vecColor = (vec4*)vertexColors.FirstElement();
                for (int t = 0; t < triangleCount; t++)
                {
                    ColorF triangleColor = mixedColors[colorOffset+t];
                    vec4 vertexColor;
                    vertexColor.x = triangleColor.R;
                    vertexColor.y = triangleColor.G;
                    vertexColor.z = triangleColor.B;
                    vertexColor.w = triangleColor.A;
                    //每个三角形点的颜色一致，即三角形同一个颜色
                    for (int vertexIndex = 0; vertexIndex < elementVertexCount; vertexIndex++)
                    {
                        vecColor[t * elementVertexCount + vertexIndex] = vertexColor;
                    }
                }
                return vertexColors;
            }
            return null;
        }

        public unsafe static UnmanagedArray<vec4> BuildFractureColors(DynamicUnstructuredGridderSource source, int[] gridIndexes, ColorF[] mixedColors)
        {
            if (source.FractureFormat == DynamicUnstructureGeometryLoader.FRACTURE_FORMAT2_LINE)
            {
                //每条裂缝或断层的点
                int fractureVertexCount = source.FractureFormat;
                //mixedColors中的颜色起始描述的是裂缝和断层的颜色
                int colorOffset = 0;
                int lineCount = source.FractureNum;
                UnmanagedArray<vec4> vertexColors = new UnmanagedArray<vec4>(lineCount * fractureVertexCount);
                vec4* vecColor = (vec4*)vertexColors.FirstElement();
                for (int i = 0; i < lineCount; i++)
                {
                    ColorF lineColor = mixedColors[colorOffset + i];
                    vec4 vertexColor;
                    vertexColor.x = lineColor.R;
                    vertexColor.y = lineColor.G;
                    vertexColor.z = lineColor.B;
                    vertexColor.w = lineColor.A;
                    for (int j = 0; j < fractureVertexCount; j++)
                    {
                        vertexColors[i * fractureVertexCount + j] = vertexColor;
                    }
                }
                return vertexColors;
            }



            return null;
        }
        

    }
}
