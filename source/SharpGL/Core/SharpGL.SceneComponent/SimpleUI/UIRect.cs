using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent.SimpleUI
{
    public class UIRect : SceneElement, IRenderable, IHasObjectSpace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="anchor">the edges of the viewport to which a SimpleUIRect is bound and determines how it is resized with its parent.
        /// <para>something like AnchorStyles.Left | AnchorStyles.Bottom.</para></param>
        /// <param name="margin">the space between viewport and SimpleRect.</param>
        /// <param name="size">Stores width when <see cref="OpenGLUIRect.Anchor"/>.Left & <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        /// <para> and height when <see cref="OpenGLUIRect.Anchor"/>.Top & <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        /// <param name="rectColor">default color is red.</param>
        public UIRect(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null)
        {
            this.Anchor = anchor;
            this.Margin = margin;
            this.Size = size;
            this.zNear = zNear;
            this.zFar = zFar;
            if (rectColor == null)
            { this.RectColor = new GLColor(1, 0, 0, 1); }
            else
            { this.RectColor = new GLColor(1, 0, 0, 1); }

            this.RenderBound = true;

            this.Transformation = new SceneGraph.Transformations.LinearTransformation();
        }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            //if (renderMode == RenderMode.HitTest) { return; }

            RenderModel(args, gl, renderMode);
        }

        /// <summary>
        /// render UI model at axis's center(0, 0, 0) in <paramref name="UIWidth"/> and <paramref name="UIHeight"/>.
        /// <para>The <see cref="SimpleUIRect.RenderMode()"/> only draws a rectangle to show the UI's scope.</para>
        /// </summary>
        /// <param name="UIWidth"></param>
        /// <param name="UIHeight"></param>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        protected virtual void RenderModel(UIRectArgs args, OpenGL gl, RenderMode renderMode)
        {
            if (this.RenderBound)
            {
                gl.Begin(Enumerations.BeginMode.LineLoop);
                gl.Color(RectColor);
                gl.Vertex(-args.UIWidth / 2, -args.UIHeight / 2, 0);
                gl.Vertex(args.UIWidth / 2, -args.UIHeight / 2, 0);
                gl.Vertex(args.UIWidth / 2, args.UIHeight / 2, 0);
                gl.Vertex(-args.UIWidth / 2, args.UIHeight / 2, 0);
                gl.End();
            }
        }

        #endregion

      

        /// <summary>
        /// leftRightAnchor = (AnchorStyles.Left | AnchorStyles.Right); 
        /// </summary>
        protected const AnchorStyles leftRightAnchor = (AnchorStyles.Left | AnchorStyles.Right);

        /// <summary>
        /// topBottomAnchor = (AnchorStyles.Top | AnchorStyles.Bottom);
        /// </summary>
        protected const AnchorStyles topBottomAnchor = (AnchorStyles.Top | AnchorStyles.Bottom);

        //protected int viewWidth;
        //protected int viewHeight;
        //protected int UIWidth;
        //protected int UIHeight;
        //protected int left;
        //protected int bottom;
        protected UIRectArgs args = new UIRectArgs();

        /// <summary>
        /// the edges of the OpenGLControl to which a SimpleUIRect is bound and determines how it is resized with its parent.
        /// <para>something like AnchorStyles.Left | AnchorStyles.Bottom.</para>
        /// </summary>
        public System.Windows.Forms.AnchorStyles Anchor { get; set; }
        
        /// <summary>
        /// Gets or sets the space between viewport and SimpleRect.
        /// </summary>
        public System.Windows.Forms.Padding Margin { get; set; }

        ///// <summary>
        ///// Left bottom point's location on view port.
        ///// <para>This works when <see cref="OpenGLUIRect.Anchor"/>.Left &amp; <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        ///// or <see cref="OpenGLUIRect.Anchor"/>.Top &amp; <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para>
        ///// </summary>
        //public System.Drawing.Point Location { get; set; }

        /// <summary>
        /// Stores width when <see cref="OpenGLUIRect.Anchor"/>.Left &amp; <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        /// <para> and height when <see cref="OpenGLUIRect.Anchor"/>.Top &amp; <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para>
        /// </summary>
        public System.Drawing.Size Size { get; set; }

        public int zNear { get; set; }

        public int zFar { get; set; }

        public GLColor RectColor { get; set; }

        public bool RenderBound { get; set; }

        #region IHasObjectSpace 成员

        /// <summary>
        /// Prepare projection matrix.
        /// </summary>
        /// <param name="gl"></param>
        public virtual void PushObjectSpace(OpenGL gl)
        {
           gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PushMatrix();

            UIContainer container = this.Parent as UIContainer;
            IOrthoInfo info = container;
            if (container == null) { return; }

            this.args = new UIRectArgs();
            //this.args.viewWidth = info.Width;
            //this.args.viewHeight = info.Height;

            CalculateCoords(args, info);
            
            this.Transformation.Transform(gl);
        }

        private void CalculateCoords(UIRectArgs args, IOrthoInfo info)
        {
            if ((Anchor & leftRightAnchor) == leftRightAnchor)
            {
                args.UIWidth = info.Width - Margin.Left - Margin.Right;
                if (args.UIWidth < 0) { args.UIWidth = 0; }
            }
            else
            {
                args.UIWidth = this.Size.Width;
            }

            if ((Anchor & topBottomAnchor) == topBottomAnchor)
            {
                args.UIHeight = info.Height - Margin.Top - Margin.Bottom;
                if (args.UIHeight < 0) { args.UIHeight = 0; }
            }
            else
            {
                args.UIHeight = this.Size.Height;
            }

            if ((Anchor & leftRightAnchor) == AnchorStyles.None)
            {
                Vertex translate = new Vertex();

                //args.left = -(args.UIWidth / 2
                //    + (viewWidth - args.UIWidth) * ((double)Margin.Left / (double)(Margin.Left + Margin.Right)));
            }
            else if ((Anchor & leftRightAnchor) == AnchorStyles.Left)
            {
                //args.left = -(args.UIWidth / 2 + Margin.Left);
            }
            else if ((Anchor & leftRightAnchor) == AnchorStyles.Right)
            {
                //args.left = -(viewWidth - args.UIWidth / 2 - Margin.Right);
            }
            else // if ((Anchor & leftRightAnchor) == leftRightAnchor)
            {
                //args.left = -(args.UIWidth / 2 + Margin.Left);
            }

            if ((Anchor & topBottomAnchor) == AnchorStyles.None)
            {
                //args.bottom = -viewHeight / 2;
                //args.bottom = -(args.UIHeight / 2
                //    + (viewHeight - args.UIHeight) * ((double)Margin.Bottom / (double)(Margin.Bottom + Margin.Top)));
            }
            else if ((Anchor & topBottomAnchor) == AnchorStyles.Bottom)
            {
                //args.bottom = -(args.UIHeight / 2 + Margin.Bottom);
            }
            else if ((Anchor & topBottomAnchor) == AnchorStyles.Top)
            {
                //args.bottom = -(viewHeight - args.UIHeight / 2 - Margin.Top);
            }
            else // if ((Anchor & topBottomAnchor) == topBottomAnchor)
            {
                //args.bottom = -(args.UIHeight / 2 + Margin.Bottom);
            }
        }

        public virtual void PopObjectSpace(OpenGL gl)
        {
            gl.PopMatrix();
        }

        /// <summary>
        /// This is not used.
        /// </summary>
        public virtual SceneGraph.Transformations.LinearTransformation Transformation
        { get; protected set; }

        #endregion

    }
}
