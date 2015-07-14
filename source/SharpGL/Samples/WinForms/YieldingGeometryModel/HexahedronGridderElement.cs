using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于渲染六面体网格。
    /// Rendering gridder of hexadrons.
    /// </summary>
    public class HexahedronGridderElement : SceneElement, IRenderable
    {
        private bool preparedForRendering = false;
        private float[] positions;
        private float[] colors;
        private float[] indexes;

        private HexahedronGridderSource source;
        private ShaderProgram shaderProgram;
        //  Constants that specify the attribute indexes.
        protected uint attributeIndexPosition = 0;
        protected uint attributeIndexColor = 1;

        /// <summary>
        /// 用于渲染六面体网格。
        /// Rendering gridder of hexadrons. 
        /// </summary>
        /// <param name="source">用于生成网格内所有元素的数据源。</param>
        public HexahedronGridderElement(HexahedronGridderSource source)
        {
            this.source = source;
        }


        #region IRenderable 成员

        void IRenderable.Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            if (renderMode != RenderMode.Render) { return; }

            if (!preparedForRendering)
            {
                PrepareVertexAttributes(this.source);
                this.shaderProgram = InitializeShaderProgram(gl);
                CreateVAO(gl);
                preparedForRendering = true;
            }

            DoRender(gl, renderMode);
        }


        #endregion

        /// <summary>
        /// 执行渲染。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        private void DoRender(OpenGL gl, RenderMode renderMode)
        {

        }

        /// <summary>
        /// 创建VAO和VBO。
        /// </summary>
        /// <param name="gl"></param>
        private void CreateVAO(OpenGL gl)
        {
            //  Create the vertex array object(VAO).
            var vao = new VertexBufferArray();
            vao.Create(gl);
            vao.Bind(gl);

            //  Create a vertex buffer(VBO) for the position data.
            var positionBuffer = new VertexBuffer();
            positionBuffer.Create(gl);
            positionBuffer.Bind(gl);
            //positionBuffer.SetData(gl, (uint)attributeIndexPosition, positions.length, positions.ptr, false, vertexDimension);
            positionBuffer.SetData(gl, 0u, this.positions, false, 3);

            //  Now do the same for the color data.
            var colorBuffer = new VertexBuffer();
            colorBuffer.Create(gl);
            colorBuffer.Bind(gl);
            //colorBuffer.SetData(gl, (uint)attributeIndexColor, colors.length, colors.ptr, false, colorDimension);
            colorBuffer.SetData(gl, 1u, this.colors, false, 3);

            //  Unbind the vertex array, we've finished specifying data for it.
            vao.Unbind(gl);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        /// <summary>
        /// 初始化Shader。
        /// </summary>
        private ShaderProgram InitializeShaderProgram(OpenGL gl)
        {
            var vertexShaderSource = File.ReadAllText(@"HexahedronGridder.vert");
            var fragmentShaderSource = File.ReadAllText(@"HexahedronGridder.frag");
            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
            shaderProgram.BindAttributeLocation(gl, attributeIndexColor, "in_Color");
            //gl.BindFragDataLocation(shaderProgram.ShaderProgramObject, 0, "out_Color");
            shaderProgram.AssertValid(gl);

            return shaderProgram;
        }

        /// <summary>
        /// 准备各项顶点属性。
        /// </summary>
        /// <param name="source"></param>
        private void PrepareVertexAttributes(HexahedronGridderSource source)
        {
            const int vertexCountInHexahedron = 8;// 元素内的顶点数
            const int elementCountInVertex = 3;// 顶点的元素数
            int arrayLength = source.DimenSize * vertexCountInHexahedron * elementCountInVertex;

            const int triangleStrip = 14;
            // 用三角形带画六面体，需要14个顶点（索引值），为切断三角形带，还需要附加一个。
            int indexLength = source.DimenSize * (triangleStrip + 1);

            // 稍后将用InPtr代替float[]
            float[] positions = new float[arrayLength];
            float[] colors = new float[arrayLength];
            float[] indexes = new float[indexLength];

            int gridderElementIndex = 0;
            foreach (Hexahedron hexahedron in source.GetGridderCells())
            {
                // 计算位置信息。
                {
                    int vertexIndex = 0;
                    foreach (Vertex vertex in hexahedron.GetVertexes())
                    {
                        positions[gridderElementIndex + (vertexIndex++)] = vertex.X;
                        positions[gridderElementIndex + (vertexIndex++)] = vertex.Y;
                        positions[gridderElementIndex + (vertexIndex++)] = vertex.Z;
                    }
                }
                // 计算颜色信息。
                {
                    GLColor color = hexahedron.color;
                    for (int vertexIndex = 0; vertexIndex < vertexCountInHexahedron; vertexIndex++)
                    {
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 0] = color.R;
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 1] = color.R;
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 2] = color.R;
                    }
                }

                gridderElementIndex += vertexCountInHexahedron * elementCountInVertex;
            }

            // 计算索引信息。
            for (int i = 0; i < indexLength / (triangleStrip + 1); i++)
            {
                // 索引值的指定必须配合hexahedron.GetVertexes()的次序。
                indexes[i * (triangleStrip + 1) + 00] = (i * vertexCountInHexahedron) + 0;
                indexes[i * (triangleStrip + 1) + 01] = (i * vertexCountInHexahedron) + 2;
                indexes[i * (triangleStrip + 1) + 02] = (i * vertexCountInHexahedron) + 4;
                indexes[i * (triangleStrip + 1) + 03] = (i * vertexCountInHexahedron) + 6;
                indexes[i * (triangleStrip + 1) + 04] = (i * vertexCountInHexahedron) + 7;
                indexes[i * (triangleStrip + 1) + 05] = (i * vertexCountInHexahedron) + 2;
                indexes[i * (triangleStrip + 1) + 06] = (i * vertexCountInHexahedron) + 3;
                indexes[i * (triangleStrip + 1) + 07] = (i * vertexCountInHexahedron) + 0;
                indexes[i * (triangleStrip + 1) + 08] = (i * vertexCountInHexahedron) + 1;
                indexes[i * (triangleStrip + 1) + 09] = (i * vertexCountInHexahedron) + 4;
                indexes[i * (triangleStrip + 1) + 10] = (i * vertexCountInHexahedron) + 5;
                indexes[i * (triangleStrip + 1) + 11] = (i * vertexCountInHexahedron) + 7;
                indexes[i * (triangleStrip + 1) + 12] = (i * vertexCountInHexahedron) + 1;
                indexes[i * (triangleStrip + 1) + 13] = (i * vertexCountInHexahedron) + 3;
                indexes[i * (triangleStrip + 1) + 14] = int.MaxValue;// 截断三角形带的索引值。
            }

            this.positions = positions;
            this.colors = colors;
            this.indexes = indexes;
        }
    }
}
