using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw color indicator on viewport as an UI.
    /// </summary>
    class OpenGLUIColorIndicator : OpenGLUIRect
    {
        public OpenGLUIColorIndicator()
        {
        }

        protected override void RenderModel(OpenGLUIRectArgs args, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            // Draw rectangle to show UI's scope.
            base.RenderModel(args, gl, renderMode);

        }
    }
}
