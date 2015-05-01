using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw color indicator on viewport as an UI.
    /// </summary>
    public class OpenGLUIColorIndicator : OpenGLUIRect
    {
        private SceneGraph.Transformations.LinearTransformation colorBarTransform;

        public OpenGLUIColorIndicator(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null)
            : base(anchor, margin, size, zNear, zFar)
        {
            var colorBar = new ColorIndicatorBar();
            var colorBarTransform = new SharpGL.SceneGraph.Effects.LinearTransformationEffect();
            colorBar.AddEffect(colorBarTransform);
            base.AddChild(colorBar);
            this.colorBarTransform = colorBarTransform.LinearTransformation;

            var colorNumber = new ColorIndicatorNumber();
            base.AddChild(colorNumber);
        }

        protected override void RenderModel(OpenGLUIRectArgs args, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            // Draw rectangle to show UI's scope.
            base.RenderModel(args, gl, renderMode);

            // ** / 2: half of width/height, 
            // ** / 3: SharpGL.SceneGraph.Primitives.Axies' vertices are (3, 0, 0) (0, 3, 0) (0, 0, 3)
            int min = Math.Min(args.UIWidth, args.UIHeight) / 2 / 3;
            this.colorBarTransform.ScaleX = min;
            this.colorBarTransform.ScaleY = min;
            this.colorBarTransform.ScaleZ = min;
        }
    }
}
