using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;

namespace ColorVertexSample
{
    /// <summary>
    /// The ArcBall camera supports arcball projection, making it ideal for use with a mouse.
    /// </summary>
    class ArcBallEffect2 : Effect
    {
        public ArcBallEffect2(LookAtCamera camera)
        {
            this.arcBall.SetCamera(camera);
        }

        public ArcBallEffect2(Vertex position, Vertex target, Vertex up)
        {
            this.arcBall.SetCamera(position, target, up);
        }

        /// <summary>
        /// Pushes the effect onto the specified parent element.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="parentElement">The parent element.</param>
        public override void Push(OpenGL gl, SceneElement parentElement)
        {
            //  Push the stack.
            gl.PushMatrix();

            //  Perform the transformation.
            arcBall.TransformMatrix(gl);
        }

        /// <summary>
        /// Pops the specified parent element.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="parentElement">The parent element.</param>
        public override void Pop(OpenGL gl, SceneElement parentElement)
        {
            //  Pop the stack.
            gl.PopMatrix();
        }

        /// <summary>
        /// The arcball.
        /// </summary>
        private ArcBall2 arcBall = new ArcBall2();

        /// <summary>
        /// Gets or sets the linear transformation.
        /// </summary>
        /// <value>
        /// The linear transformation.
        /// </value>
        [Description("The ArcBall."), Category("Effect")]
        public ArcBall2 ArcBall
        {
            get { return arcBall; }
            set { arcBall = value; }
        }
    }
}
