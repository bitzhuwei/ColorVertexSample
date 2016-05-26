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

        #region IRenderable

        protected void AfterRendering(OpenGL gl, RenderMode renderMode)
        {
            gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

            resolveListsShaderProgram.Unbind(gl);

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            //gl.Disable(OpenGL.GL_TEXTURE_2D);
        }

        #endregion IRenderable

    }
}
