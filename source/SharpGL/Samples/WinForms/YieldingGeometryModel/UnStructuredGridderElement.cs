using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.DataSource;
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于渲染六面体网格。
    /// Rendering gridder of hexadrons.
    /// </summary>
    public partial class UnStructuredGridderElement : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable
    {
        private uint[] vertexArrayObject = new uint[2];

        private uint[] positionBufferObject = new uint[1];

        private uint[] colorsBufferObject = new uint[1];

        //private uint fractionsIndexBuffer = 0;

        //private uint tetrasIndexBuffer = 0;

        protected ShaderProgram shaderProgram;

        protected bool isInitialized = false;

        internal IScientificCamera camera;
        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

        uint in_PositionLocation = 0;
        uint in_ColorLocation = 1;

        UnStructuredGridderSource source;

        /// <summary>
        /// 用于渲染UnStructuredGridder
        /// Rendering gridder of UnStructuredGridder.
        /// </summary>
        public UnStructuredGridderElement(UnStructuredGridderSource source, IScientificCamera camera)
        {
            if (source == null) { throw new ArgumentNullException("source"); }

            this.source = source;
            this.camera = camera;
        }

        public void Initialize(OpenGL gl)
        {
            this.shaderProgram = this.CreateShaderProgram(gl);
            this.InitVAO(gl);

            this.isInitialized = true;
        }

    }
}
