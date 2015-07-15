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
    public partial class HexahedronGridderElement
    {
        protected override void InitShader(SharpGL.OpenGL gl, out SharpGL.Shaders.ShaderProgram shader)
        {
            {
                var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronGridder.vert");
                var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronGridder.frag");
                var shaderProgram = new ShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram.AssertValid(gl);
                shader = shaderProgram;
            }
            //{
            //    var vertexShaderSource = ColorCodedPickingShaderHelper.GetShaderSource(ColorCodedPickingShaderHelper.ShaderTypes.VertexShader);
            //    var fragmentShaderSource = ColorCodedPickingShaderHelper.GetShaderSource(ColorCodedPickingShaderHelper.ShaderTypes.FragmentShader);
            //    var shaderProgram = new ShaderProgram();
            //    shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            //    shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
            //    shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
            //    shaderProgram.AssertValid(gl);
            //    this.pickingShader = shaderProgram;
            //}
        }

    }
}
