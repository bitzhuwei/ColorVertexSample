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
        VertexBufferArray vertexBufferArray;
        IndexBuffer indexDataBuffer;

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
            this.source = source;
            this.camera = camera;
        }

        /// <summary>
        /// Initialises the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public void Initialise(OpenGL gl)
        {
            ////  Set a blue clear colour.
            //gl.ClearColor(0.4f, 0.6f, 0.9f, 0.5f);

            //  Create the shader program.
            InitShader(gl);
            //  Create the picking shader program.
            InitPickingShader(gl);
            // init vertex attributes.
            InitVertexAttributes(gl);
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

            DrawWithVAO(gl, renderMode);
        }


        #endregion

    }
}
