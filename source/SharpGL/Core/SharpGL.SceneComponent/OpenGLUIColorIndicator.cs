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
        private ColorIndicatorData data;
        private ColorIndicatorBar colorBar;
        private ColorIndicatorNumber colorNumber;

        public ColorIndicatorData Data
        {
            get { return data; }
            set
            {
                data = value;
                this.colorBar.Data = value;
                this.colorNumber.Data = value;
            }
        }

        public OpenGLUIColorIndicator(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null)
            : base(anchor, margin, size, zNear, zFar)
        {
            {
                this.colorBar = new ColorIndicatorBar() { Name = "color indicator's bar" };
                var colorBarTransform = new SharpGL.SceneGraph.Effects.LinearTransformationEffect();
                colorBar.AddEffect(colorBarTransform);
                base.AddChild(colorBar);
                this.colorBarTransform = colorBarTransform.LinearTransformation;
            }

            {
                this.colorNumber = new ColorIndicatorNumber() { Name = "color indicator's number" };
                base.AddChild(colorNumber);
            }
        }

        protected override void RenderModel(OpenGLUIRectArgs args, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            // Draw rectangle to show UI's scope.
            base.RenderModel(args, gl, renderMode);

            this.colorBarTransform.ScaleX = (float)args.UIWidth / (float)ColorIndicatorBar.barWidth;
            this.colorBarTransform.ScaleY = (float)args.UIHeight / (float)ColorIndicatorBar.barHeight;
            //this.colorBarTransform.ScaleZ = 1;// This is not needed.

            this.colorNumber.CurrentArgs = args;
        }
    }
}
