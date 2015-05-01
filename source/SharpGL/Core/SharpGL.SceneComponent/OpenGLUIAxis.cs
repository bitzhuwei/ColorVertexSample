using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw axis with arc ball rotation effect on viewport as an UI.
    /// </summary>
    public class OpenGLUIAxis : OpenGLUIRect
    {
        ArcBall2 arcBall = new ArcBall2();
        private LinearTransformationEffect axisTransform;
        //private SceneGraph.Primitives.Axies axis;

        public OpenGLUIAxis()
        {
            SharpGL.SceneGraph.Primitives.Axies axis = new SharpGL.SceneGraph.Primitives.Axies();
            LinearTransformationEffect axisTransform = new LinearTransformationEffect();
            this.axisTransform = axisTransform;
            axis.AddEffect(axisTransform);
            base.AddChild(axis);
        }

        protected override void RenderModel(int UIWidth, int UIHeight, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            base.RenderModel(UIWidth, UIHeight, gl, renderMode);

            int min = Math.Min(UIWidth, UIHeight) / 2 / 3;
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
