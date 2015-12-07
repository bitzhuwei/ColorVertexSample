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
        private PointSpriteStringElement textElement;

        /// <summary>
        /// 蛇形管道（井）+文字显示
        /// </summary>
        /// <param name="pipe"></param>
        /// <param name="pipeRadius"></param>
        /// <param name="pipeColor"></param>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="camera"></param>
        public Well(IScientificCamera camera, List<Vertex> pipe, float pipeRadius, GLColor pipeColor, String name, Vertex position,
            GLColor textColor = null, int fontSize = 32, int maxRowWidth = 256)
        {
            this.wellPipeElement = new WellPipe(pipe, pipeRadius, pipeColor, camera);

            this.textElement = new PointSpriteStringElement(camera, name, position, textColor, fontSize, maxRowWidth);
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
