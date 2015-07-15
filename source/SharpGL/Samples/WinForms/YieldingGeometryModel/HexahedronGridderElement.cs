using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class HexahedronGridderElement : SceneElement, IRenderable, IBoundingBox
    {

        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

        //  Constants that specify the attribute indexes.
        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        //  The vertex buffer array which contains the vertex and colour buffers.
        /// <summary>
        /// VAO.
        /// </summary>
        VertexBufferArray vertexBufferArray;
        /// <summary>
        /// Index buffer.
        /// </summary>
        IndexBuffer indexDataBuffer;
        int indexArrayElementCount;

        //  The shader program for our vertex and fragment shader.
        private ShaderProgram shaderProgram;
        private ShaderProgram pickingShaderProgram;
        private bool initialised;

        /// <summary>
        /// 用于生成网格内所有元素的数据源。
        /// </summary>
        private HexahedronGridderSource source;

        /// <summary>
        /// <para>Use <see cref="IHasObjectSpace"/> and <see cref="IScientificCamera"/> to update projection and view matrices.</para>
        /// </summary>
        public IScientificCamera camera;

        /// <summary>
        /// 用于渲染六面体网格。
        /// Rendering gridder of hexadrons.
        /// </summary>
        /// <param name="source">用于生成网格内所有元素的数据源。</param>
        /// <param name="camera"></param>
        public HexahedronGridderElement(HexahedronGridderSource source, IScientificCamera camera)
        {
            if (source == null) { throw new ArgumentNullException("source"); }

            this.source = source;
            this.camera = camera;
        }

        /// <summary>
        /// 初始化Shader、Index和VAO。
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public void Initialise(OpenGL gl)
        {
            //  Create the shader program.
            InitShader(gl);
            //  Create the picking shader program.
            InitPickingShader(gl);

            // 初始化索引并立即释放内存
            InitializeIndexArray(gl);

            // 初始化VAO并立即释放内存
            InitializeVAO(gl);

            this.initialised = true;
        }


        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (renderMode != RenderMode.Render) { return; }

            if (!initialised)
            {
                this.Initialise(gl);

                this.initialised = true;
            }

            // Update matrices.
            UpdateMatrixes(gl, renderMode);

            var shader = (renderMode == RenderMode.HitTest) ? pickingShaderProgram : shaderProgram;

            //  Bind the shader, set the matrices.
            shader.Bind(gl);
            shader.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shader.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            DrawWithVAO(gl, renderMode);

            shader.Unbind(gl);
        }


        #endregion

    }
}
