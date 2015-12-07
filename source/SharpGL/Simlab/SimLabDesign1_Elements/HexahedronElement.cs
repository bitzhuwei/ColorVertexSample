using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SimLabDesign1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1_Elements
{
    /// <summary>
    /// 可渲染多个六面体的场景元素。
    /// <para>初始化和使用类似这种Element的操作顺序：</para>
    /// <para>1. 用SetupVertexBuffer设定所有的VBO（及时暂时没有数据也要setup）</para>
    /// <para>2. 用InitShader初始化shader并获取其中各个in数组的location</para>
    /// <para>3. 用GenerateVAO创建VAO</para>
    /// <para>4. 指定renderer（用MultiDrawArrays还是DrawElements）</para>
    /// </summary>
    public class HexahedronElement : RenderableElementBase, IRenderable, IDisposable
    {
        /// <summary>
        /// 对应shader中的in变量；对应vboDict中的某个key。
        /// </summary>
        public const string key_in_Position = "in_Position";

        /// <summary>
        /// 对应shader中的in变量；对应vboDict中的某个key。
        /// </summary>
        public const string key_in_Color = "in_Color";

        /// <summary>
        /// 对应shader中的in变量；对应vboDict中的某个key。
        /// </summary>
        public const string key_in_visible = "in_visible";



        #region 初始化shader、VAO和渲染

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderer">指定renderer（用MultiDrawArrays或DrawElements）</param>
        public HexahedronElement(Renderer renderer, IScientificCamera camera)
            : base(renderer, camera)
        {
        }

        ShaderProgram shaderProgram;
        uint[] vertexBufferArray;
        Renderer renderer;

        void IRenderable.Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            //if (vertexBufferArray == null)
            //{
            //    lock (this)
            //    {
            //        if (vertexBufferArray == null)
            //        {
            //            this.shaderProgram = InitShader(gl, renderMode);
            //            GenerateVAO(gl, renderMode);

            //        }
            //    }
            //}

            //IScientificCamera camera = this.camera;
            //if (camera != null)
            //{
            //    if (camera.CameraType == CameraTypes.Perspecitive)
            //    {
            //        IPerspectiveViewCamera perspective = camera;
            //        this.projectionMatrix = perspective.GetProjectionMat4();
            //        this.viewMatrix = perspective.GetViewMat4();
            //    }
            //    else if (camera.CameraType == CameraTypes.Ortho)
            //    {
            //        IOrthoViewCamera ortho = camera;
            //        this.projectionMatrix = ortho.GetProjectionMat4();
            //        this.viewMatrix = ortho.GetViewMat4();
            //    }
            //    else
            //    { throw new NotImplementedException(); }
            //}

            //modelMatrix = GlmNet.mat4.identity();
            ////  Bind the shader, set the matrices.
            //shaderProgram.Bind(gl);
            //shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            //shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            //shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            //gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
            //gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);

            //this.renderer.Render(gl, renderMode);

            //gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

            //shaderProgram.Unbind(gl);
        }

        //private ShaderProgram InitShader(OpenGL gl, RenderMode renderMode)
        //{
        //    String vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronElement.vert");
        //    String fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronElement.frag");
        //    ShaderProgram shaderProgram = new ShaderProgram();
        //    shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);

        //    foreach (var vbo in this.vboDict)
        //    {
        //        vbo.Value.FetchInfoFromShaderProgram(gl, shaderProgram);
        //    }

        //    shaderProgram.AssertValid(gl);
        //    return shaderProgram;
        //}

        //private void GenerateVAO(OpenGL gl, RenderMode renderMode)
        //{
        //    vertexBufferArray = new uint[1];
        //    gl.GenVertexArrays(1, vertexBufferArray);
        //    gl.BindVertexArray(vertexBufferArray[0]);

        //    foreach (var vbo in vboDict)
        //    {
        //        vbo.Value.LayoutForVAO(gl);
        //    }

        //    gl.BindVertexArray(0);
        //}

        #endregion 初始化shader、VAO和渲染

    

    }
}
