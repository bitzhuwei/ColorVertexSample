using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    public class HexadronGridderElement : SceneElement, IRenderable
    {
        #region IRenderable 成员

        void IRenderable.Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
