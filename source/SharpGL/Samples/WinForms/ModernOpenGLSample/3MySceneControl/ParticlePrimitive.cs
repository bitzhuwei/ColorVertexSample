using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ModernOpenGLSample._3MySceneControl
{
    /// <summary>
    /// 以后把这个类扩展成和OVITO里的OpenGLParticlePrimitive类似的东西。
    /// </summary>
    class ParticlePrimitive
    {
        private ShaderProgram shaderProgram;
        private ShaderProgram pickingShaderProgram;
        private int particleCount;
        private RenderingTechnique _renderingTechnique;
        private bool _usingGeometryShader;
        private int maxVBOSize = 4 * 1024 * 1024;
        private int chunkSize;
        //private List<VertexBufferArray> positionVAOs = new List<VertexBufferArray>();
        //private List<VertexBuffer> positionVBOs = new List<VertexBuffer>();
        //private List<VertexBufferArray> radiusVAOs = new List<VertexBufferArray>();
        //private List<VertexBuffer> radiusVBOs = new List<VertexBuffer>();
        //private List<VertexBufferArray> colorVAOs = new List<VertexBufferArray>();
        //private List<VertexBuffer> colorVBOs = new List<VertexBuffer>();

        /// <summary>
        /// 以后把这个类扩展成和OVITO里的OpenGLParticlePrimitive类似的东西。
        /// </summary>
        public ParticlePrimitive()
        {
        }

        public void SetSize(int particleCount, SharpGL.OpenGL gl)
        {
            this.particleCount = particleCount;

            // Determine the required number of vertices that need to be sent to the graphics card per particle.
            int verticesPerParticle = GetVerticesPerParticle();

            int bytePerVertex = System.Runtime.InteropServices.Marshal.SizeOf(typeof(GlmNet.vec4));
            this.chunkSize = Math.Min(
                this.maxVBOSize / (bytePerVertex * verticesPerParticle), 
                particleCount);

            //TODO: 不知道这是在干什么，暂时不管
            //// Cannot use chunked VBOs when rendering semi-transparent particles,
            //// because they will be rendered in arbitrary order.
            //if (translucentParticles())
            //    _chunkSize = particleCount;

            int numChunks = particleCount > 0 ? (particleCount + this.chunkSize - 1) / this.chunkSize : 0;
            //this.positionVAOs.Clear(); this.positionVBOs.Clear();
            //this.radiusVAOs.Clear(); this.radiusVBOs.Clear();
            //this.colorVAOs.Clear(); this.colorVBOs.Clear();
            for (int i = 0; i < numChunks; i++)
            {
                int size = Math.Min(this.chunkSize, particleCount - i * this.chunkSize);
                {
                    ////  Create the vertex array object.
                    //VertexBufferArray vertexBufferArray = new VertexBufferArray();
                    //vertexBufferArray.Create(gl);
                    //vertexBufferArray.Bind(gl);

                    ////  Create a vertex buffer for the vertex data.
                    //VertexBuffer vertexDataBuffer = new VertexBuffer();
                    //vertexDataBuffer.Create(gl);
                    //vertexDataBuffer.Bind(gl);
                    //vertexDataBuffer.SetData(gl, 0, vertices, false, 3);

                    ////  Unbind the vertex array, we've finished specifying data for it.
                    //vertexBufferArray.Unbind(gl);


                }
            }
        }

        private int GetVerticesPerParticle()
        {
            int verticesPerParticle = 0;
            if (_renderingTechnique == RenderingTechnique.POINT_SPRITES)
            {
                verticesPerParticle = 1;
            }
            else if (_renderingTechnique == RenderingTechnique.IMPOSTER_QUADS)
            {
                if (_usingGeometryShader)
                    verticesPerParticle = 1;
                else
                    verticesPerParticle = 6;
            }
            else if (_renderingTechnique == RenderingTechnique.CUBE_GEOMETRY)
            {
                if (_usingGeometryShader)
                    verticesPerParticle = 1;
                else
                    verticesPerParticle = 14;
            }
            else
            { Debug.Assert(false); }

            return verticesPerParticle;
        }
    }
}
