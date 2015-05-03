using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponent
{
    public class OrthoColorIndicatorBar : SceneElement, IRenderable
    {
        public PointerScientificModel rectModel { get; set; }

        public PointerScientificModel verticalLines { get; set; }

        public PointerScientificModel horizontalLines { get; set; }

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            PointerScientificModel rectModel = this.rectModel;
            PointerScientificModel verticalLines = this.verticalLines;
            PointerScientificModel horizontalLines = this.horizontalLines;

            if (rectModel != null)
            { rectModel.Render(gl, renderMode); }

            if (verticalLines != null)
            { verticalLines.Render(gl, renderMode); }

            if (horizontalLines != null)
            { horizontalLines.Render(gl, renderMode); }
        }

        public OrthoColorIndicatorBarEffect scaleEffect { get; set; }
    }
}
