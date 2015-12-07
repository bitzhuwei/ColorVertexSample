using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simlab.Well
{
    public partial class PointSpriteStringElement
    {

        protected void InitShaderProgram(SharpGL.OpenGL gl, out SharpGL.Shaders.ShaderProgram shaderProgram)
        {
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"PointSpriteStringElement.vert");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"PointSpriteStringElement.frag");
            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            int position = shaderProgram.GetAttributeLocation(gl, "in_Position");
            if (position >= 0) { attributeIndexPosition = (uint)position; }
            //int color = shaderProgram.GetAttributeLocation(gl, "in_Color");
            //if (color >= 0) { attributeIndexColour = (uint)color; }
            //int radius = shaderProgram.GetAttributeLocation(gl, "in_radius");
            //if (radius >= 0) { attributeIndexRadius = (uint)radius; }
            //int visible = shaderProgram.GetAttributeLocation(gl, "in_visible");
            //if (visible >= 0) { attributeIndexVisible = (uint)visible; }
            shaderProgram.AssertValid(gl);
        }

    }
}
