using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public partial class HexahedronGrid 
    {

        protected override void DisposeUnmanagedResources()
        {
            base.DisposeUnmanagedResources();

            try
            {
                if (this.indexBuffer != null)
                {
                    gl.DeleteBuffers(this.indexBuffer.Length, this.indexBuffer);
                }

                if (this.vertexArrayObject != null)
                {
                    gl.DeleteVertexArrays(this.vertexArrayObject.Length, this.vertexArrayObject);
                }
                {
                    this.buildListsShaderProgram.Delete(gl);
                    this.resolveListsShaderProgram.Delete(gl);
                }
                {
                    gl.DeleteTextures(linked_list_texture.Length, linked_list_texture);
                    gl.DeleteBuffers(linked_list_buffer.Length, linked_list_buffer);
                    gl.DeleteBuffers(atomic_counter_buffer.Length, atomic_counter_buffer);
                    gl.DeleteTextures(head_pointer_texture.Length, head_pointer_texture);
                }
            }
            catch (Exception)
            {
            }

        }
    }
}
