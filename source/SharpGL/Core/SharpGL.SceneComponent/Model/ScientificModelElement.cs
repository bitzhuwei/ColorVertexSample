using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Render the <see cref="IScientificModel"/>.
    /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        public virtual void Render(OpenGL gl, RenderMode renderMode)
        {
            //TODO: this 'virtual' maybe not necessary, consider to remove it.
            IScientificModel model = this.Model;
            if (model == null) { return; }

            model.Render(gl, renderMode);
            model.BoundingBox.Render(gl, renderMode);
        }

        #endregion

        #region IHasObjectSpace 成员

        /// <summary>
        /// rotate <see cref="IScientificModel"/> around its bounding box's center position.
        /// </summary>
        /// <param name="gl"></param>
        public virtual void PushObjectSpace(OpenGL gl)
        {
            gl.PushMatrix();
            IScientificModel model = this.Model;
            if (model == null) { return; }
            IBoundingBox boundingBox = model.BoundingBox;
            gl.LoadIdentity();
            float x = 0, y = 0, z = 0;
            if (boundingBox != null)
            {
                boundingBox.GetCenter(out x, out y, out z);
                gl.Translate(x, y, z);//lastly, move model back.
            }
            modelTransform.TransformMatrix(gl);// then, rotate and scale model.
            if (boundingBox != null)
            {
                gl.Translate(-x, -y, -z);// firstly, move model to (0, 0, 0);
            }
        }

        public virtual void PopObjectSpace(OpenGL gl)
        {
            gl.PopMatrix();
        }

        #endregion
    }
}
