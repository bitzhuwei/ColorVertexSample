using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public partial class HexahedronGrid : SimLabGrid, IRenderable
    {
        private const string in_Position = "in_Position";
        private const string in_uv = "in_uv";
        //uint buildListsPosition;
        //uint buildListsUV;
        //uint resolveListsPosition;
        //uint resolveListsUV;

        protected uint[] indexBuffer;
        protected int indexBufferLength;

        uint[] buildListsVAO;
        uint[] resolveListsVAO;

        private GlmNet.mat4 projectionMatrix;
        private GlmNet.mat4 viewMatrix;
        private mat4 modelMatrix;
        private ShaderProgram buildListsShaderProgram;
        private ShaderProgram resolveListsShaderProgram;

        private uint[] head_pointer_texture = new uint[1];
        private const int MAX_FRAMEBUFFER_WIDTH = 2048;
        private const int MAX_FRAMEBUFFER_HEIGHT = 2048;
        private uint[] head_pointer_clear_buffer = new uint[1];
        private uint[] atomic_counter_buffer = new uint[1];
        private uint[] linked_list_buffer = new uint[1];
        private uint[] linked_list_texture = new uint[1];
        private int width;
        private int height;
        private int backup;

        public HexahedronGrid(OpenGL gl, IScientificCamera camera)
            : base(gl, camera)
        {
            var viewport = new int[4];
            gl.GetInteger(SharpGL.Enumerations.GetTarget.Viewport, viewport);
            this.width = viewport[2];
            this.height = viewport[3];
            this.backup = 3;
        }

    }
}
