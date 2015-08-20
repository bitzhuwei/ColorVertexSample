using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    public partial class UnStructuredGridderElement
    {

        ShaderProgram CreateShaderProgram(OpenGL gl)
        {
            String vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"UnStructuredGridderElement.vert");
            String fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"UnStructuredGridderElement.frag");
            ShaderProgram shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            {
                int location = shaderProgram.GetAttributeLocation(gl, "in_Position");
                if (location < 0) { throw new Exception(); }
                else { this.in_PositionLocation = (uint)location; }
            }
            {
                int location = shaderProgram.GetAttributeLocation(gl, "in_Color");
                if (location <= 0) { throw new Exception(); }
                else { this.in_ColorLocation = (uint)location; }
            }

            shaderProgram.AssertValid(gl);

            return shaderProgram;
        }

    }
}
