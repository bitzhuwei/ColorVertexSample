using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponent
{
    public class OrthoColorIndicatorBar : SceneElement, IRenderable
    {
        public GenericModel rectModel { get; set; }

        public GenericModel verticalLines { get; set; }

        public GenericModel horizontalLines { get; set; }

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            var rectModel = this.rectModel;
            var verticalLines = this.verticalLines;
            var horizontalLines = this.horizontalLines;

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
