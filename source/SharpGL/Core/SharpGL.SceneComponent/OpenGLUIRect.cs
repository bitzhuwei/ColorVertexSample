using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw a rectangle on OpenGL control like a <see cref="Windows.Forms.Control"/> drawn on a <see cref="windows.Forms.Form"/>.
    /// Set its properties(Margin, Dock, etc) to adjust its behaviour.
    /// </summary>
    class OpenGLUIRect : SceneElement, IRenderable
    {
        #region IRenderable 成员

        public virtual void Render(OpenGL gl, RenderMode renderMode)
        {
            //if (renderMode == RenderMode.HitTest) { return; }

            throw new NotImplementedException();
        }

        #endregion
    }
}
