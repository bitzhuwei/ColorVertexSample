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
    internal class ScientificModelElement : SceneElement, IRenderable
    {
        /// <summary>
        /// The model shown in <see cref="ScientificVisual3DControl"/>.
        /// </summary>
        public IScientificModel Model { get; set; }

        public bool RenderBoundingBox { get; set; }

        public bool RenderModel { get; set; }

        public ScientificModelElement(IScientificModel model, bool renderBoundingBox = true, bool renderModel = true)
        {
            this.Model = model;
            this.RenderBoundingBox = renderBoundingBox;
            this.RenderModel = renderModel;
        }

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

            if (this.RenderModel)
            {
                model.Render(gl, renderMode);
            }

            if (this.RenderBoundingBox)
            {
                model.BoundingBox.Render(gl, renderMode);
            }
        }

        #endregion

    }
}
