using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.IndexVAOElementBase
{
    public abstract class IndexedVAOElement : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable
    {

        #region IRenderable 成员

        public abstract void Render(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode);

        #endregion
    }
}
