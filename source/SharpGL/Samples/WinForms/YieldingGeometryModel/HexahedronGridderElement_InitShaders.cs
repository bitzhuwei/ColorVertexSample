using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;

namespace YieldingGeometryModel
{
    public partial class HexahedronGridderElement
    {

        /// <summary>
        /// //  Create the picking shader program.
        /// </summary>
        /// <param name="gl"></param>
        private void InitPickingShader(OpenGL gl)
        {
            var vertexShaderSource = ColorCodedPickingShaderHelper.GetShaderSource(ColorCodedPickingShaderHelper.ShaderTypes.VertexShader);
            var fragmentShaderSource = ColorCodedPickingShaderHelper.GetShaderSource(ColorCodedPickingShaderHelper.ShaderTypes.FragmentShader);
            var shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
            shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
            shaderProgram.AssertValid(gl);
            this.pickingShaderProgram = shaderProgram;
        }

        /// <summary>
        /// Create the shader program.
        /// </summary>
        /// <param name="gl"></param>
        private void InitShader(OpenGL gl)
        {
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronGridder.vert");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronGridder.frag");
            var shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
            shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
            shaderProgram.AssertValid(gl);
            this.shaderProgram = shaderProgram;
        }


    }
}
