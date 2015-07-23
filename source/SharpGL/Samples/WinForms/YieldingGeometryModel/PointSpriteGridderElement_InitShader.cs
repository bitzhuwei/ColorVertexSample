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
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    public partial class PointSpriteGridderElement
    {

        protected override void InitShader(SharpGL.OpenGL gl, out SharpGL.Shaders.ShaderProgram shader)
        {
            {
                //Bitmap bmp = ManifestResourceLoader.LoadBitmap("PointSprite.png");
                //this.texture = new Texture();
                //this.texture.Create(gl, bmp);
                //bmp.Dispose();

                //  Create the shader program.
                var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"PointSprite.vert");
                var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"PointSprite.frag");
                var shaderProgram = new ShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                int position = shaderProgram.GetAttributeLocation(gl, "in_Position");
                if (position >= 0) { attributeIndexPosition = (uint)position; }
                int color = shaderProgram.GetAttributeLocation(gl, "in_Color");
                if (color >= 0) { attributeIndexColour = (uint)color; }
                int radius = shaderProgram.GetAttributeLocation(gl, "in_radius");
                if (radius >= 0) { attributeIndexRadius = (uint)radius; }
                //shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                //shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                //shaderProgram.BindAttributeLocation(gl, attributeIndexVisible, "in_visible");//控制顶点可见性。
                //shaderProgram.BindAttributeLocation(gl, attributeIndexRadius, "in_radius");//圆的半径。
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
