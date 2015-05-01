using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw axis with arc ball rotation effect on viewport as an UI.
    /// </summary>
    public class OpenGLUIAxis : OpenGLUIRect
    {
        ArcBall2 arcBall = new ArcBall2();
        private LinearTransformationEffect axisTransform;


        public OpenGLUIAxis(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null)
            : base(anchor, margin, size, zNear, zFar)
        {
            SharpGL.SceneGraph.Primitives.Axies axis = new SharpGL.SceneGraph.Primitives.Axies();
            LinearTransformationEffect axisTransform = new LinearTransformationEffect();
            this.axisTransform = axisTransform;
            axis.AddEffect(axisTransform);
            base.AddChild(axis);
        }

        protected override void RenderModel(OpenGLUIRectArgs args, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            // Draw rectangle to show UI's scope.
            base.RenderModel(args, gl, renderMode);

            // ** / 2: half of width/height, 
            // ** / 3: SharpGL.SceneGraph.Primitives.Axies' vertices are (3, 0, 0) (0, 3, 0) (0, 0, 3)
            int min = Math.Min(args.UIWidth, args.UIHeight) / 2 / 3;
            this.axisTransform.LinearTransformation.ScaleX = min;
            this.axisTransform.LinearTransformation.ScaleY = min;
            this.axisTransform.LinearTransformation.ScaleZ = min;
        }

        public override void PushObjectSpace(OpenGL gl)
        {
            base.PushObjectSpace(gl);

            arcBall.TransformMatrix(gl);
        }

        public override SceneGraph.Cameras.LookAtCamera Camera
        {
            get
            {
                return base.Camera;
            }
            set
            {
                base.Camera = value;
                this.arcBall.Camera = value;
            }
        }

        public void MouseUp(int x, int y)
        {
            this.arcBall.MouseUp(x, y);
        }

        public void MouseMove(int x, int y)
        {
            this.arcBall.MouseMove(x, y);
        }

        public void SetBounds(int width, int height)
        {
            this.arcBall.SetBounds(width, height);
        }

        public void MouseDown(int x, int y)
        {
            this.arcBall.MouseDown(x, y);
        }
    }
}
