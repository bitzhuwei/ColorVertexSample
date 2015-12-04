using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// TODO：用DrawElements()渲染（待完成）
    /// </summary>
    public partial class Texture2dHexahedronGridderElement_DrawElements : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable
    {
        private Texture texture = new Texture();
        private Bitmap textureImage;

        private uint vertexArrayObject = 0;

        private uint vertexsBufferObject = 0;

        private uint colorsBufferObject = 0;

        protected uint visiblesBufferObject = 0;

        //private uint triangleStripIndexBuffer = 0;

        //private uint lineIndexBuffer = 0;

        //private int triangleIndexCount = 0;

        //private int lineIndexCount = 0;

        ///// <summary>
        ///// Specifies what kind of primitives to render. Symbolic constants OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON are accepted
        ///// </summary>
        //protected uint primitiveMode;

        protected ShaderProgram shader;

        protected bool isInitialized = false;

        //internal HexahedronGridderSource source;
        internal IScientificCamera camera;
        //private ShaderProgram pickingShader;
        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

        /// <summary>
        /// 元素内的顶点数。（8）
        /// </summary>
        internal const int vertexCountInHexahedron = 8;
        /// <summary>
        /// 顶点的分量数。（3）
        /// </summary>
        internal const int componentCountInVertex = 3;
        /// <summary>
        /// 用三角形带画六面体，需要14个顶点（索引值）
        /// </summary>
        internal const int triangleStrip = 14;

        ////  Constants that specify the attribute indexes.
        //internal const uint attributeIndexPosition = 0;
        //internal const uint attributeIndexColour = 1;
        //internal const uint attributeIndexVisible = 2;
        //  Constants that specify the attribute indexes.
        public const uint ATTRIB_INDEX_POSITION = 0;
        public const uint ATTRIB_INDEX_COLOUR = 1;
        public const uint ATTRIB_INDEX_VISIBLE = 2;
        private HexahedronGridderSource source;

        public HexahedronGridderSource Source
        {
            get { return source; }
            set { source = value; }
        }

        internal uint visualBuffer;

        /// <summary>
        /// 用于渲染六面体网格。
        /// Rendering gridder of hexadrons.
        /// </summary>
        public Texture2dHexahedronGridderElement_DrawElements(Bitmap textureImage, HexahedronGridderSource source, IScientificCamera camera)
        {
            if (source == null) { throw new ArgumentNullException("source"); }

            this.textureImage = textureImage;
            this.source = source;
            this.camera = camera;
        }


        public void Initialize(OpenGL gl, Texture2dMeshGeometry mesh)
        {
            gl.MakeCurrent();

            this.texture.Create(gl, textureImage);

            this.shader = this.CreateShaderProgram(gl);
            this.InitVertexes(gl, mesh.Vertexes, mesh.VertexColors, mesh.Visibles);
            

            this.isInitialized = true;
        }




    }
}
