using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 蛇形管道（井）
    /// </summary>
    public class WellPipe : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable, IDisposable
    {
        uint positionBufferObject;
        uint colorBufferObject;
        uint indexBufferObject;

        /// <summary>
        /// 蛇形管道（井）
        /// </summary>
        /// <param textContent="pipe">管道中心点轨迹</param>
        /// <param textContent="radius">圆柱半径</param>
        /// <param textContent="color">颜色</param>
        /// <param textContent="name">井名</param>
        /// <param textContent="position">文字渲染位置，模型坐标</param>
        public WellPipe(List<Vertex> pipe, float radius, GLColor color, IScientificCamera camera)
        {
            if (pipe == null || pipe.Count < 2 || radius <= 0.0f)
            { throw new ArgumentException(); }

            this.pipe = pipe; this.radius = radius; this.color = color;

            this.camera = camera;
        }

        /// <summary>
        /// shader program
        /// </summary>
        public ShaderProgram shaderProgram;
        const string strin_Position = "in_Position";
        const string strin_Color = "in_Color";
        public const string strprojectionMatrix = "projectionMatrix";
        public const string strviewMatrix = "viewMatrix";
        public const string strmodelMatrix = "modelMatrix";

        /// <summary>
        /// VAO
        /// </summary>
        protected uint[] vao;

        /// <summary>
        /// 图元类型
        /// </summary>
        protected BeginMode primitiveMode;

        /// <summary>
        /// 顶点数
        /// </summary>
        protected int vertexCount;
        private List<Vertex> pipe;
        private float radius;
        private GLColor color;
        const int faceCount = 18;
        private IScientificCamera camera;


        protected void InitializeShader(OpenGL gl, out ShaderProgram shaderProgram)
        {
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"WellPipe.vert");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"WellPipe.frag");

            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.AssertValid(gl);
        }

        protected void InitializeVAO(OpenGL gl, out uint[] vao, out BeginMode primitiveMode, out int vertexCount)
        {
            primitiveMode = BeginMode.QuadStrip;
            vertexCount = (faceCount * 2 + 2) * (this.pipe.Count - 1);

            UnmanagedArray<Vertex> positionArray = new UnmanagedArray<Vertex>(vertexCount);
            {
                int index = 0;
                for (int i = 1; i < this.pipe.Count; i++)
                {
                    Vertex p1 = this.pipe[i - 1];
                    Vertex p2 = this.pipe[i];
                    Vertex vector = p2 - p1;// 从p1到p2的向量
                    // 找到互相垂直的三个向量：vector, orthogontalVector1和orthogontalVector2
                    Vertex orthogontalVector1 = new Vertex(vector.Y - vector.Z, vector.Z - vector.X, vector.X - vector.Y);
                    Vertex orthogontalVector2 = vector.VectorProduct(orthogontalVector1);
                    orthogontalVector1.Normalize();
                    orthogontalVector2.Normalize();
                    orthogontalVector1 *= (float)Math.Sqrt(this.radius);
                    orthogontalVector2 *= (float)Math.Sqrt(this.radius);
                    for (int faceIndex = 0; faceIndex < faceCount + 1; faceIndex++)
                    {
                        double angle = (Math.PI * 2 * faceIndex) / faceCount;
                        Vertex point = orthogontalVector1 * (float)Math.Cos(angle) + orthogontalVector2 * (float)Math.Sin(angle);
                        positionArray[index++] = p2 + point;
                        positionArray[index++] = p1 + point;
                    }

                    //positionArray[index++] = new vec3();//用于分割圆柱体
                }
            }

            UnmanagedArray<Vertex> colorArray = new UnmanagedArray<Vertex>(vertexCount);
            {
                Vertex vColor = new Vertex(this.color.R, this.color.G, this.color.B);
                for (int i = 0; i < colorArray.Length; i++)
                {
                    colorArray[i] = vColor;
                    // 测试时用此代码区分各个圆柱
                    //if ((i / (faceCount * 2 + 2)) % 3 == 0)
                    //{
                    //    colorArray[i] = new vec3(1, 0, 0);
                    //}
                    //else if ((i / (faceCount * 2 + 2)) % 3 == 1)
                    //{
                    //    colorArray[i] = new vec3(0, 1, 0);
                    //}
                    //else
                    //{
                    //    colorArray[i] = new vec3(0, 0, 1);
                    //}
                }
            }

            UnmanagedArray<uint> indexArray = new UnmanagedArray<uint>(vertexCount + (this.pipe.Count - 1));
            {
                uint positionIndex = 0;
                for (int i = 0; i < indexArray.Length; i++)
                {
                    if (i % (faceCount * 2 + 2 + 1) == (faceCount * 2 + 2))
                    {
                        indexArray[i] = uint.MaxValue;//分割各个圆柱体
                    }
                    else
                    {
                        indexArray[i] = positionIndex++;
                    }
                }
            }

            vao = new uint[1];
            gl.GenVertexArrays(1, vao);
            gl.BindVertexArray(vao[0]);
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                int location = gl.GetAttribLocation(shaderProgram.ShaderProgramObject, strin_Position);
                if (location < 0) { throw new Exception(); }
                uint positionLocation = (uint)location;

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(positionLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(positionLocation);

                positionArray.Dispose();
                this.positionBufferObject = ids[0];
            }
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                int location = gl.GetAttribLocation(shaderProgram.ShaderProgramObject, strin_Color);
                if (location < 0) { throw new Exception(); }
                uint colorLocation = (uint)location;

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(colorLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(colorLocation);

                colorArray.Dispose();
                this.colorBufferObject = ids[0];
            }
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ids[0]);
                gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexArray.ByteLength, indexArray.Header, OpenGL.GL_STATIC_DRAW);

                indexArray.Dispose();
                this.indexBufferObject = ids[0];
            }

            //  Unbind the vertex array, we've finished specifying data for it.
            gl.BindVertexArray(0);
        }

        public void Initialize(SharpGL.OpenGL gl)
        {
            InitializeShader(gl, out shaderProgram);

            InitializeVAO(gl, out vao, out primitiveMode, out vertexCount);

        }

        public void Render(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode)
        {
            // update matrix and bind shader program
            mat4 projectionMatrix = camera.GetProjectionMat4();

            mat4 viewMatrix = camera.GetViewMat4();

            mat4 modelMatrix = mat4.identity();

            shaderProgram.Bind(gl);

            shaderProgram.SetUniformMatrix4(gl, strprojectionMatrix, projectionMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, strviewMatrix, viewMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, strmodelMatrix, modelMatrix.to_array());

            gl.BindVertexArray(vao[0]);

            // 启用Primitive restart
            gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
            gl.PrimitiveRestartIndex(uint.MaxValue);// 截断图元（四边形带、三角形带等）的索引值。

            //GL.DrawArrays(primitiveMode, 0, vertexCount);
            gl.DrawElements((uint)primitiveMode, vertexCount + (this.pipe.Count - 1) * 2, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

            gl.BindVertexArray(0);

            gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);

            // unbind shader program
            shaderProgram.Unbind(gl);
        }

        
        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        } // end sub

        /// <summary>
        /// Destruct instance of the class.
        /// </summary>
        ~WellPipe()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Backing field to track whether Dispose has been called.
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Dispose managed and unmanaged resources of this instance.
        /// </summary>
        /// <param name="disposing">If disposing equals true, managed and unmanaged resources can be disposed. If disposing equals false, only unmanaged resources can be disposed. </param>
        protected virtual void Dispose(bool disposing)
        {

            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    // TODO: Dispose managed resources.
                    CleanManagedRes();
                } // end if

                // TODO: Dispose unmanaged resources.
                CleanUnmanagedRes();
            } // end if

            this.disposedValue = true;
        } // end sub

        #endregion

        protected void CleanUnmanagedRes()
        {
            OpenGL gl = new OpenGL();// this is not cool.

            uint[] buffers = new uint[] { this.positionBufferObject, this.colorBufferObject, this.indexBufferObject };
            gl.DeleteBuffers(buffers.Length, buffers);
            gl.DeleteVertexArrays(this.vao.Length, this.vao);
        }

        protected void CleanManagedRes()
        {
        }
    }
}
