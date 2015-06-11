using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// maintains bounding box that contains all models.
    /// </summary>
    public class ModelContainer : SceneElement, IRenderable
    {
        BoundingBox boundingBox = new BoundingBox();

        /// <summary>
        /// Gets bounding box that contains all models.
        /// </summary>
        public BoundingBox BoundingBox
        {
            get { return boundingBox; }
        }

        /// <summary>
        /// Determins whether render the bounding box or not.
        /// </summary>
        public bool RenderBoundingBox { get; set; }

        public ModelContainer(bool renderBoundingBox = true)
        {
            this.RenderBoundingBox = renderBoundingBox;
        }

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (renderMode == RenderMode.HitTest) { return; }
            if (!this.RenderBoundingBox) { return; }
            //IBoundingBox boundingBox = this.expandedBoundingBox;
            IBoundingBox boundingBox = this.boundingBox;
            if (boundingBox == null) { return; }

            //gl.Enable(OpenGL.GL_POLYGON_OFFSET_LINE);
            //gl.PolygonOffset(-1.0f, -1.0f);
            //int [] results = new int[2];
            //gl.GetInteger(Enumerations.GetTarget.PolygonMode, results);
            //gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);

            boundingBox.Render(gl, renderMode);

            //gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, (uint)results[0]);
            //gl.Disable(OpenGL.GL_POLYGON_OFFSET_LINE);
        }

        #endregion
    }
}
