using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Transformations;
using SharpGL.SceneGraph;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Show axis at the left bottom of screen wherever the camera is.
    /// Don't use this class as it shows black background under axis.
    /// </summary>
    public class AxisTransformEffect : Effect
    {
        /// <summary>
        /// Pushes the effect onto the specified parent element.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="parentElement">The parent element.</param>
        public override void Push(OpenGL gl, SceneElement parentElement)
        {
            //  Push the stack.
            gl.PushMatrix();

            LookAtCamera camera = parentElement.TraverseToRootElement().ParentScene.CurrentCamera as LookAtCamera;
            if (camera != null)
            {
                SetTranslate(gl, camera);
                const int width = 200;
                gl.GetInteger(OpenGL.GL_VIEWPORT, result);
                gl.Viewport(0, 0, width, width * result[3] / result[2]);
            }

            //  Perform the transformation.
            linearTransformation.Transform(gl);
        }

        private void SetTranslate(OpenGL gl, LookAtCamera camera)
        {
            gl.Flush();

            Vertex towards = camera.Target - camera.Position;
            towards.Normalize();
            Vertex position = camera.Position + towards * 5f;

            this.linearTransformation.TranslateX = position.X;
            this.linearTransformation.TranslateY = position.Y;
            this.linearTransformation.TranslateZ = position.Z;
        }

        /// <summary>
        /// Pops the specified parent element.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="parentElement">The parent element.</param>
        public override void Pop(OpenGL gl, SceneElement parentElement)
        {
            LookAtCamera camera = parentElement.TraverseToRootElement().ParentScene.CurrentCamera as LookAtCamera;
            if (camera != null)
            {
                gl.Viewport(0, 0, result[2], result[3]);
            }

            //  Pop the stack.
            gl.PopMatrix();
        }

        /// <summary>
        /// The linear transformation.
        /// </summary>
        private LinearTransformation linearTransformation = new LinearTransformation();
        private int[] result = new int[4];

        /// <summary>
        /// Gets or sets the linear transformation.
        /// </summary>
        /// <value>
        /// The linear transformation.
        /// </value>
        [Description("The linear transformation."), Category("Effect")]
        public LinearTransformation LinearTransformation
        {
            get { return linearTransformation; }
            set { linearTransformation = value; }
        }
    }
}
