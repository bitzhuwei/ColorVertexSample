using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
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
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    public partial class UnStructuredGridderElement
    {
        uint[] textureName = new uint[1];
        void InitTexture(OpenGL gl)
        {
            int xSize = (int)(this.source.Max.X - this.source.Min.X);
            int ySize = (int)(this.source.Max.Y - this.source.Min.Y);
            int zSize = (int)(this.source.Max.Z - this.source.Min.Z);

            int minSize = Math.Min(xSize, Math.Min(ySize, zSize));

            using (var data = new UnmanagedArray<float>(xSize * ySize * zSize))
            {
                gl.PixelStore(OpenGL.GL_UNPACK_ALIGNMENT, 1);

                gl.GenTextures(1, this.textureName);

                gl.ActiveTexture(OpenGL.GL_TEXTURE0);

                gl.BindTexture(OpenGL.GL_TEXTURE_3D, this.textureName[0]);

                gl.TexParameter(OpenGL.GL_TEXTURE_3D, OpenGL.GL_TEXTURE_BASE_LEVEL, 0);
                gl.TexParameter(OpenGL.GL_TEXTURE_3D, OpenGL.GL_TEXTURE_MAX_LEVEL, (float)Math.Log(minSize, 2));
                gl.TexParameter(OpenGL.GL_TEXTURE_3D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR_MIPMAP_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_3D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_3D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_CLAMP_TO_EDGE);
                gl.TexParameter(OpenGL.GL_TEXTURE_3D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_CLAMP_TO_EDGE);
                gl.TexParameter(OpenGL.GL_TEXTURE_3D, OpenGL.GL_TEXTURE_WRAP_R, OpenGL.GL_CLAMP_TO_EDGE);

                gl.TexImage3D(OpenGL.GL_TEXTURE_3D,
                    0,
                    (int)OpenGL.GL_R32F,
                    xSize, ySize, zSize,
                    0,
                   OpenGL.GL_RED, OpenGL.GL_FLOAT,
                    data.Header);

                gl.GenerateMipmapEXT(OpenGL.GL_TEXTURE_3D);

                gl.PixelStore(OpenGL.GL_UNPACK_ALIGNMENT, 4);
            }
        }

    }
}
