using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw a rectangle on OpenGL control like a <see cref="Windows.Forms.Control"/> drawn on a <see cref="windows.Forms.Form"/>.
    /// Set its properties(Margin, Dock, etc) to adjust its behaviour.
    /// </summary>
    public class OpenGLUIRect : SceneElement, IRenderable, IHasObjectSpace
    {
        public OpenGLUIRect()
        {
            this.zNear = -1000;
            this.zFar = 1000;
            this.RectColor = new GLColor(1, 0, 0, 1);
        }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            //if (renderMode == RenderMode.HitTest) { return; }

            RenderModel(UIWidth, UIHeight, gl, renderMode);
        }

        private void CalculateViewport(OpenGL gl, out int viewWidth, out int viewHeight)
        {
            IRenderContextProvider rcp = gl.RenderContextProvider;
            Debug.Assert(rcp != null, "The gl.RenderContextProvider is null!");

            viewWidth = 0;
            viewHeight = 0;

            if (rcp != null)
            {
                viewWidth = rcp.Width;
                viewHeight = rcp.Height;
            }
            else
            {
                int[] viewport = new int[4];
                gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);
                viewWidth = viewport[2];
                viewHeight = viewport[3];
            }
        }

        private void CalculateCoords(int viewWidth, int viewHeight, out int UIWidth, out int UIHeight, out int left, out int bottom)
        {
            if ((Anchor & leftRightAnchor) == leftRightAnchor)
            {
                UIWidth = viewWidth - Margin.Left - Margin.Right;
            }
            else
            {
                UIWidth = this.Size.Width;
            }

            if ((Anchor & topBottomAnchor) == topBottomAnchor)
            {
                UIHeight = viewHeight - Margin.Top - Margin.Bottom;
            }
            else
            {
                UIHeight = this.Size.Height;
            }

            if ((Anchor & leftRightAnchor) == AnchorStyles.None)
            {
                left = -viewWidth / 2;
            }
            else if ((Anchor & leftRightAnchor) == AnchorStyles.Left)
            {
                left = -(UIWidth / 2 + Margin.Left);
            }
            else if ((Anchor & leftRightAnchor) == AnchorStyles.Right)
            {
                left = -(viewWidth - UIWidth / 2 - Margin.Right);
            }
            else // if ((Anchor & leftRightAnchor) == leftRightAnchor)
            {
                left = -(UIWidth / 2 + Margin.Left);
            }

            if ((Anchor & topBottomAnchor) == AnchorStyles.None)
            {
                bottom = -viewHeight / 2;
            }
            else if ((Anchor & topBottomAnchor) == AnchorStyles.Bottom)
            {
                bottom = -(UIHeight / 2 + Margin.Bottom);
            }
            else if ((Anchor & topBottomAnchor) == AnchorStyles.Top)
            {
                bottom = -(viewHeight - UIHeight / 2 - Margin.Top);
            }
            else // if ((Anchor & topBottomAnchor) == topBottomAnchor)
            {
                bottom = -(UIHeight / 2 + Margin.Bottom);
            }
        }


        #endregion

        /// <summary>
        /// render UI model at axis's center(0, 0, 0) in <paramref name="UIWidth"/> and <paramref name="UIHeight"/>.
        /// </summary>
        /// <param name="UIWidth"></param>
        /// <param name="UIHeight"></param>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        protected virtual void RenderModel(int UIWidth, int UIHeight, OpenGL gl, RenderMode renderMode)
        {
            gl.Begin(Enumerations.BeginMode.LineLoop);
            gl.Color(RectColor);
            gl.Vertex(-UIWidth / 2, -UIHeight / 2, 0);
            gl.Vertex(UIWidth / 2, -UIHeight / 2, 0);
            gl.Vertex(UIWidth / 2, UIHeight / 2, 0);
            gl.Vertex(-UIWidth / 2, UIHeight / 2, 0);
            gl.End();
        }

        /// <summary>
        /// leftRightAnchor = (AnchorStyles.Left | AnchorStyles.Right); 
        /// </summary>
        protected const AnchorStyles leftRightAnchor = (AnchorStyles.Left | AnchorStyles.Right);

        /// <summary>
        /// topBottomAnchor = (AnchorStyles.Top | AnchorStyles.Bottom);
        /// </summary>
        protected const AnchorStyles topBottomAnchor = (AnchorStyles.Top | AnchorStyles.Bottom);

        protected int viewWidth;
        protected int viewHeight;
        protected int UIWidth;
        protected int UIHeight;
        protected int left;
        protected int bottom;

        public virtual LookAtCamera Camera { get; set; }
        public System.Windows.Forms.AnchorStyles Anchor { get; set; }
        public System.Windows.Forms.Padding Margin { get; set; }

        ///// <summary>
        ///// Left bottom point's location on view port.
        ///// <para>This works when <see cref="OpenGLUIRect.Anchor"/>.Left & <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        ///// or <see cref="OpenGLUIRect.Anchor"/>.Top & <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para>
        ///// </summary>
        //public System.Drawing.Point Location { get; set; }

        /// <summary>
        /// Stores width when <see cref="OpenGLUIRect.Anchor"/>.Left & <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        /// <para> and height when <see cref="OpenGLUIRect.Anchor"/>.Top & <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para>
        /// </summary>
        public System.Drawing.Size Size { get; set; }

        public int zNear { get; set; }

        public int zFar { get; set; }

        public GLColor RectColor { get; set; }

        #region IHasObjectSpace 成员

        /// <summary>
        /// Prepare projection matrix.
        /// </summary>
        /// <param name="gl"></param>
        public virtual void PushObjectSpace(OpenGL gl)
        {
            //int viewWidth;
            //int viewHeight;
            CalculateViewport(gl, out viewWidth, out viewHeight);

            //int UIWidth, UIHeight, left, bottom;
            CalculateCoords(viewWidth, viewHeight, out UIWidth, out UIHeight, out left, out bottom);

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Ortho(left, left + viewWidth, bottom, bottom + viewHeight, zNear, zFar);

            LookAtCamera camera = this.Camera;
            if (camera == null)
            {
                gl.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);
                //throw new Exception("Camera not set!");
            }
            else
            {
                Vertex position = camera.Position - camera.Target;
                position.Normalize();
                gl.LookAt(position.X, position.Y, position.Z,
                    0, 0, 0,
                    camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            }

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PushMatrix();
        }

        public virtual void PopObjectSpace(OpenGL gl)
        {
            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.PopMatrix();

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PopMatrix();
        }

        public virtual SceneGraph.Transformations.LinearTransformation Transformation
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
