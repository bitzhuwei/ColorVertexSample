using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 蛇形管道（井）+文字显示
    /// </summary>
    public class Well : SceneElement, IRenderable
    {
        private WellPipe wellPipeElement;
        private PointSpriteFontElement textElement;

        /// <summary>
        /// 蛇形管道（井）+文字显示
        /// </summary>
        /// <param name="pipe"></param>
        /// <param name="radius"></param>
        /// <param name="color"></param>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="camera"></param>
        public Well(List<Vertex> pipe, float radius, GLColor color, String name, Vertex position, IScientificCamera camera)
        {
            this.wellPipeElement = new WellPipe(pipe, radius, color, camera);

            this.textElement = new PointSpriteFontElement(camera, name, position);
        }

        public void Initialize(OpenGL gl)
        {
            this.wellPipeElement.Initialize(gl);

            this.textElement.Initialize(gl);
        }

        public void Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            this.wellPipeElement.Render(gl, renderMode);

            this.textElement.Render(gl, renderMode);
        }
    }
}
