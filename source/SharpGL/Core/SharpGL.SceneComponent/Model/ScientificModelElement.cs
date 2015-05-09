using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    internal class ScientificModelElement : SceneElement, IRenderable, IMyHasObjectSpace
    {
        /// <summary>
        /// All <see cref="ScientificModelElement"/>s use one same instance so that arcball calculation only happens once when user moves mouse.
        /// </summary>
        internal IMouseLinearTransform modelTransform;

        /// <summary>
        /// The model shown in <see cref="ScientificVisual3DControl"/>.
        /// </summary>
        public IScientificModel Model { get; set; }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            IScientificModel model = this.Model;
            if (model == null) { return; }

            model.Render(gl, renderMode);
            model.BoundingBox.Render(gl, renderMode);
        }

        #endregion

        #region IHasObjectSpace 成员

        public virtual void PushObjectSpace(OpenGL gl)
        {
            gl.PushMatrix();
            if (this.Model == null) { return; }

            gl.LoadIdentity();
            Vertex center = this.Model.BoundingBox.GetCenter();
            gl.Translate(center.X, center.Y, center.Z);//lastly, move model back.
            modelTransform.TransformMatrix(gl);// then, rotate and scale model.
            gl.Translate(-center.X, -center.Y, -center.Z);// firstly, move model to (0, 0, 0);
        }

        public virtual void PopObjectSpace(OpenGL gl)
        {
            gl.PopMatrix();
        }

        #endregion
    }
}
