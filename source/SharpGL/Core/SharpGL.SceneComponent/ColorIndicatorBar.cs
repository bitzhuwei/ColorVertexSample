using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    class ColorIndicatorBar : SceneElement, IRenderable
    {
        public GenericModel rectModel { get; set; }

        public GenericModel verticalLines { get; set; }

        public GenericModel horizontalLines { get; set; }

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            GenericModel rectModel = this.rectModel;
            GenericModel verticalLines = this.verticalLines;
            GenericModel horizontalLines = this.horizontalLines;

            if (rectModel != null)
            { rectModel.Render(gl, renderMode); }

            if (verticalLines != null)
            { verticalLines.Render(gl, renderMode); }

            if (horizontalLines != null)
            { horizontalLines.Render(gl, renderMode); }
        }
    }
}
