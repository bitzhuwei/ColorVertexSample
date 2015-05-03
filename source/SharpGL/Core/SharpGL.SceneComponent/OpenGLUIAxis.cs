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
    public class OpenGLUIAxis : OpenGLUIRect, IMouseRotation
    {
        internal IMouseTransform mouseTransform = new ArcBall2();
        /// <summary>
        /// keeps axis' scale.
        /// </summary>
        private SceneGraph.Transformations.LinearTransformation axisTransform;


        public OpenGLUIAxis(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null)
            : base(anchor, margin, size, zNear, zFar)
        {
            CylinderAxis axis = new CylinderAxis();
            LinearTransformationEffect axisTransform = new SharpGL.SceneGraph.Effects.LinearTransformationEffect();
            this.axisTransform = axisTransform.LinearTransformation;
            axis.AddEffect(axisTransform);
            base.AddChild(axis);
            this.RectColor = new GLColor(1, 1, 0, 1);// red(x axis) + green(y axis)
        }

        protected override void RenderModel(OpenGLUIRectArgs args, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            // Draw rectangle to show UI's scope.
            base.RenderModel(args, gl, renderMode);

            // ** / 2: half of width/height, 
            // ** / 3: CylinderAxis' length is 3.
            this.axisTransform.ScaleX = args.UIWidth / 2 / 3;
            this.axisTransform.ScaleY = args.UIHeight / 2 / 3;
            int max = Math.Max(args.UIWidth,args.UIHeight);
            this.axisTransform.ScaleZ = max / 2 / 3;
        }

        public override void PushObjectSpace(OpenGL gl)
        {
            base.PushObjectSpace(gl);

            if (mouseTransform.Camera != null)
            {
                mouseTransform.TransformMatrix(gl);
            }
        }



        #region IRotation 成员

        /// <summary>
        /// if Camera is null, this UI rectangle area will be drawn with an invoking
        /// <para>gl.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);</para>
        /// <para>otherwise, it uses gl.LookAt(Camera's (Position - Target), Target, UpVector);</para>
        /// </summary>
        public override SceneGraph.Cameras.LookAtCamera Camera
        {
            get
            {
                return base.Camera;
            }
            set
            {
                base.Camera = value;
                this.mouseTransform.Camera = value;
            }
        }

        public void MouseUp(int x, int y)
        {
            this.mouseTransform.MouseUp(x, y);
        }

        public void MouseMove(int x, int y)
        {
            this.mouseTransform.MouseMove(x, y);
        }

        public void SetBounds(int width, int height)
        {
            this.mouseTransform.SetBounds(width, height);
        }

        public void MouseDown(int x, int y)
        {
            this.mouseTransform.MouseDown(x, y);
        }

        public void ResetRotation()
        {
            this.mouseTransform.ResetRotation();
        }

        #endregion
    }
}
