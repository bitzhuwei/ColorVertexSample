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


        private void ResetMisc(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            // Reset atomic counter
            gl.BindBufferBase(OpenGL.GL_ATOMIC_COUNTER_BUFFER, 0, atomic_counter_buffer[0]);
            IntPtr data = gl.MapBuffer(OpenGL.GL_ATOMIC_COUNTER_BUFFER, OpenGL.GL_WRITE_ONLY);
            unsafe
            {
                var array = (uint*)data.ToPointer();
                array[0] = 0;
            }
            gl.UnmapBuffer(OpenGL.GL_ATOMIC_COUNTER_BUFFER);
            gl.BindBufferBase(OpenGL.GL_ATOMIC_COUNTER_BUFFER, 0, 0);

            // Clear head-pointer image
            gl.BindBuffer(OpenGL.GL_PIXEL_UNPACK_BUFFER, head_pointer_clear_buffer[0]);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, head_pointer_texture[0]);
            var viewport = new int[4];
            gl.GetInteger(SharpGL.Enumerations.GetTarget.Viewport, viewport);
            gl.TexSubImage2D(OpenGL.GL_TEXTURE_2D, 0, 0, 0, viewport[2], viewport[3],
                OpenGL.GL_RED_INTEGER, OpenGL.GL_UNSIGNED_BYTE, IntPtr.Zero);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            gl.BindBuffer(OpenGL.GL_PIXEL_UNPACK_BUFFER, 0);
            //

            // Bind head-pointer image for read-write
            gl.GetDelegateFor<OpenGL.glBindImageTexture>()(0, head_pointer_texture[0], 0, false, 0, OpenGL.GL_READ_WRITE, OpenGL.GL_R32UI);

            // Bind linked-list buffer for write
            gl.GetDelegateFor<OpenGL.glBindImageTexture>()(1, linked_list_texture[0], 0, false, 0, OpenGL.GL_WRITE_ONLY, OpenGL.GL_RGBA32UI);

        }

    }
}
