using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public class ColorCodedPickingShaderHelper
    {

        static string vertexShader = null;
        static string fragmentShader = null;
        public static string GetShaderSource(ShaderTypes shaderType)
        {
            string result = string.Empty;

            switch (shaderType)
            {
                case ShaderTypes.VertexShader:
                    if (vertexShader == null)
                    {
                        vertexShader = ManifestResourceLoader.LoadTextFile(@"ColorCodedPicking\PickingShader.vert");
                    }
                    result = vertexShader;
                    break;
                case ShaderTypes.FragmentShader:
                    if (fragmentShader == null)
                    {
                        fragmentShader = ManifestResourceLoader.LoadTextFile(@"ColorCodedPicking\PickingShader.frag");
                    }
                    result = fragmentShader;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }

        public enum ShaderTypes
        {
            VertexShader,
            FragmentShader,
        }
    }

}
