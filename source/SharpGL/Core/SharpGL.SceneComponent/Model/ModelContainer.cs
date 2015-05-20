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

        BoundingBox expandedBoundingBox = new BoundingBox();

        private float expandFactor = 1.1f;

        /// <summary>
        /// Render expanded bounding box.
        /// <para>1 is for original size.</para>
        /// </summary>
        public float ExpandFactor
        {
            get { return expandFactor; }
            set
            {
                if (value != expandFactor)
                {
                    expandFactor = value;
                    UpdateExpandedBoudingBox();
                }
            }
        }

        /// <summary>
        /// Determins whether render the bounding box or not.
        /// </summary>
        public bool RenderBoundingBox { get; set; }

        /// <summary>
        /// Adds a child and extend bounding box.
        /// </summary>
        /// <param name="child"></param>
        internal void AddChild(ScientificModelElement child)
        {
            base.AddChild(child);
            BoundingBox boundingBox = this.boundingBox;
            IBoundingBox modelBoundingBox = child.Model.BoundingBox;
            if (base.Children.Count > 1)
            {
                boundingBox.Extend(modelBoundingBox.MinPosition);
                boundingBox.Extend(modelBoundingBox.MaxPosition);
            }
            else
            {
                boundingBox.Set(modelBoundingBox.MinPosition.X,
                    modelBoundingBox.MinPosition.Y,
                    modelBoundingBox.MinPosition.Z,
                    modelBoundingBox.MaxPosition.X,
                    modelBoundingBox.MaxPosition.Y,
                    modelBoundingBox.MaxPosition.Z);
            }
            UpdateExpandedBoudingBox();
        }
        
        /// <summary>
        /// Removes a chlid and updates expanded bounding box.
        /// </summary>
        /// <param name="child"></param>
        internal void RemoveChild(ScientificModelElement child)
        {
            base.RemoveChild(child);
            //TODO: improve efficiency with some algorithm.
            this.boundingBox.Set();
            foreach (var item in this.Children)
            {
                ScientificModelElement element = item as ScientificModelElement;
                if (element != null)
                {
                    IBoundingBox modelBoundingBox = child.Model.BoundingBox;
                    this.boundingBox.Extend(modelBoundingBox.MinPosition);
                    this.boundingBox.Extend(modelBoundingBox.MaxPosition);
                }
            }
            UpdateExpandedBoudingBox();
        }

        /// <summary>
        /// Clears children, resets bounding box and updates expanded bounding box.
        /// </summary>
        internal void ClearChild()
        {
            base.Children.Clear();
            this.boundingBox.Set();
            UpdateExpandedBoudingBox();
        }

        public ModelContainer()
        {
            this.RenderBoundingBox = true;
        }

        private void UpdateExpandedBoudingBox()
        {
            float x, y, z;
            this.boundingBox.GetCenter(out x, out y, out z);
            float xSize, ySize, zSize;
            this.boundingBox.GetBoundDimensions(out xSize, out ySize, out zSize);
            this.boundingBox.Set(
                ExpandFactor * (-xSize / 2) + x,
                ExpandFactor * (-ySize / 2) + y,
                ExpandFactor * (-zSize / 2) + z,
                ExpandFactor * (xSize / 2) + x,
                ExpandFactor * (ySize / 2) + y,
                ExpandFactor * (zSize / 2) + z);
        }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            if (renderMode == RenderMode.HitTest) { return; }
            if (!this.RenderBoundingBox) { return; }
            IBoundingBox boundingBox = this.expandedBoundingBox;
            //IBoundingBox boundingBox = this.boundingBox;
            if (boundingBox == null) { return; }

            boundingBox.Render(gl, renderMode);
        }

        #endregion
    }
}
