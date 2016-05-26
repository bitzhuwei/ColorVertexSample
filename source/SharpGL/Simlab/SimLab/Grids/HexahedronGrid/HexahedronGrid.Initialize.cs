using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public partial class HexahedronGrid
    {

        public void Init(HexahedronMeshGeometry3D geometry)
        {
            base.Init(geometry);

            indexBuffer = new uint[1];
            indexBuffer[0] = CreateVertexBufferObject(OpenGL.GL_ELEMENT_ARRAY_BUFFER, geometry.HalfHexahedronIndices, OpenGL.GL_STATIC_DRAW);

            int elementLength = sizeof(uint);// should be 4.
            this.indexBufferLength = geometry.HalfHexahedronIndices.SizeInBytes / (elementLength);

        }

        private void InitMisc(OpenGL gl)
        {
            // Create head pointer texture
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.ActiveTexture(OpenGL.GL_TEXTURE0);
            gl.GenTextures(1, head_pointer_texture);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, head_pointer_texture[0]);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, (int)OpenGL.GL_NEAREST);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, (int)OpenGL.GL_NEAREST);
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_R32UI, MAX_FRAMEBUFFER_WIDTH, MAX_FRAMEBUFFER_HEIGHT, 0, OpenGL.GL_RED_INTEGER, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            gl.GetDelegateFor<OpenGL.glBindImageTexture>()(0, head_pointer_texture[0], 0, true, 0, OpenGL.GL_READ_WRITE, OpenGL.GL_R32UI);

            // Create buffer for clearing the head pointer texture
            gl.GenBuffers(1, head_pointer_clear_buffer);
            gl.BindBuffer(OpenGL.GL_PIXEL_UNPACK_BUFFER, head_pointer_clear_buffer[0]);
            gl.BufferData(OpenGL.GL_PIXEL_UNPACK_BUFFER,
                MAX_FRAMEBUFFER_WIDTH * MAX_FRAMEBUFFER_HEIGHT * sizeof(uint), IntPtr.Zero,
                OpenGL.GL_STATIC_DRAW);
            IntPtr data = gl.MapBuffer(OpenGL.GL_PIXEL_UNPACK_BUFFER, OpenGL.GL_WRITE_ONLY);
            unsafe
            {
                var array = (uint*)data.ToPointer();
                for (int i = 0; i < MAX_FRAMEBUFFER_WIDTH * MAX_FRAMEBUFFER_HEIGHT; i++)
                {
                    array[i] = 0;
                }
            }
            gl.UnmapBuffer(OpenGL.GL_PIXEL_UNPACK_BUFFER);
            gl.BindBuffer(OpenGL.GL_PIXEL_UNPACK_BUFFER, 0);

            // Create the atomic counter buffer
            gl.GenBuffers(1, atomic_counter_buffer);
            gl.BindBuffer(OpenGL.GL_ATOMIC_COUNTER_BUFFER, atomic_counter_buffer[0]);
            gl.BufferData(OpenGL.GL_ATOMIC_COUNTER_BUFFER, sizeof(uint), IntPtr.Zero, OpenGL.GL_DYNAMIC_COPY);
            gl.BindBuffer(OpenGL.GL_ATOMIC_COUNTER_BUFFER, 0);

            // Create the linked list storage buffer
            gl.GenBuffers(1, linked_list_buffer);
            gl.BindBuffer(OpenGL.GL_TEXTURE_BUFFER, linked_list_buffer[0]);
            gl.BufferData(OpenGL.GL_TEXTURE_BUFFER,
                MAX_FRAMEBUFFER_WIDTH * MAX_FRAMEBUFFER_HEIGHT * 3 * Marshal.SizeOf(typeof(vec4)),
                IntPtr.Zero, OpenGL.GL_DYNAMIC_COPY);
            gl.BindBuffer(OpenGL.GL_TEXTURE_BUFFER, 0);

            // Bind it to a texture (for use as a TBO)
            gl.GenTextures(1, linked_list_texture);
            gl.BindTexture(OpenGL.GL_TEXTURE_BUFFER, linked_list_buffer[0]);
            gl.TexBuffer(OpenGL.GL_TEXTURE_BUFFER, OpenGL.GL_RGBA32UI, linked_list_buffer[0]);
            gl.BindTexture(OpenGL.GL_TEXTURE_BUFFER, 0);

            gl.GetDelegateFor<OpenGL.glBindImageTexture>()(1, linked_list_texture[0], 0, false, 0, OpenGL.GL_WRITE_ONLY, OpenGL.GL_RGBA32UI);

            gl.ClearDepth(1.0f);
        }

        private ShaderProgram InitBuildListsShaderProgram(OpenGL gl, RenderMode renderMode)
        {
            String vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"Grids.HexahedronGrid.HexahedronGridBuildLists.vert");
            String fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"Grids.HexahedronGrid.HexahedronGridBuildLists.frag");
            ShaderProgram shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            //shaderProgram.BindAttributeLocation(gl, ATTRIB_INDEX_POSITION, in_position);
            //shaderProgram.BindAttributeLocation(gl, ATTRIB_INDEX_COLOUR, in_uv);
            {
                int location = shaderProgram.GetAttributeLocation(gl, in_Position);
                if (location < 0) { throw new ArgumentException(); }
                this.buildListsPosition = (uint)location;
            }
            //{
            //    int location = shaderProgram.GetAttributeLocation(gl, in_uv);
            //    if (location < 0) { throw new ArgumentException(); }
            //    this.buildListsUV = (uint)location;
            //}
            shaderProgram.AssertValid(gl);
            return shaderProgram;
        }


        private void CreateResolveListsVertexArrayObject(OpenGL gl, RenderMode renderMode)
        {
            if (this.positionBuffer == null || this.colorBuffer == null) { return; }

            this.resolveListsVAO = new uint[1];
            gl.GenVertexArrays(1, this.resolveListsVAO);
            gl.BindVertexArray(this.resolveListsVAO[0]);

            // prepare positions
            {
                int location = resolveListsShaderProgram.GetAttributeLocation(gl, in_Position);
                this.resolveListsPosition = (uint)location;
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, positionBuffer[0]);
                gl.VertexAttribPointer(this.resolveListsPosition, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.resolveListsPosition);
            }
            //// prepare colors
            //{
            //    int location = resolveListsShaderProgram.GetAttributeLocation(gl, in_uv);
            //    buildListsUV = (uint)location;
            //    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, colorBuffer[0]);
            //    gl.VertexAttribPointer(buildListsUV, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            //    gl.EnableVertexAttribArray(buildListsUV);
            //}

            gl.BindVertexArray(0);
        }

        private void CreateBuildListsVertexArrayObject(OpenGL gl, RenderMode renderMode)
        {
            if (this.positionBuffer == null || this.colorBuffer == null) { return; }

            this.buildListsVAO = new uint[1];
            gl.GenVertexArrays(1, this.buildListsVAO);
            gl.BindVertexArray(this.buildListsVAO[0]);

            // prepare positions
            {
                int location = this.buildListsShaderProgram.GetAttributeLocation(gl, in_Position);
                buildListsPosition = (uint)location;
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, positionBuffer[0]);
                gl.VertexAttribPointer(buildListsPosition, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(buildListsPosition);
            }
            // prepare colors
            //{
            //    int location = this.buildListsShaderProgram.GetAttributeLocation(gl, in_uv);
            //    buildListsUV = (uint)location;
            //    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, colorBuffer[0]);
            //    gl.VertexAttribPointer(buildListsUV, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            //    gl.EnableVertexAttribArray(buildListsUV);
            //}

            gl.BindVertexArray(0);
        }

        private ShaderProgram InitResolveListsShaderProgram(OpenGL gl, RenderMode renderMode)
        {
            String vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"Grids.HexahedronGrid.HexahedronGridSolveLists.vert");
            String fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"Grids.HexahedronGrid.HexahedronGridSolveLists.frag");
            ShaderProgram shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            //shaderProgram.BindAttributeLocation(gl, ATTRIB_INDEX_POSITION, in_position);
            //shaderProgram.BindAttributeLocation(gl, ATTRIB_INDEX_COLOUR, in_uv);
            {
                int location = shaderProgram.GetAttributeLocation(gl, in_Position);
                if (location < 0) { throw new ArgumentException(); }
                this.resolveListsPosition = (uint)location;
            }
            //{
            //    int location = shaderProgram.GetAttributeLocation(gl, in_uv);
            //    if (location < 0) { throw new ArgumentException(); }
            //    this.resolveListsUV = (uint)location;
            //}
            shaderProgram.AssertValid(gl);
            return shaderProgram;
        }

    }
}
