using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    public class AttributeBuffer : VBOInfoBase
    {

        public override uint Target
        {
            get
            {
                return OpenGL.GL_ARRAY_BUFFER;
            }
        }

        /// <summary>
        /// 此VBO代表的数组在shader中的对应名称（例如in vec3 positions里的positions）
        /// <para>index in VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)</para>
        /// </summary>
        public uint AttribLocation { get; set; }

        /// <summary>
        /// 在GLSL的shader中的in变量名（例如'in vec3 position;'里的'position'）
        /// </summary>
        public string VarNameInShader { get; set; }

        /// <summary>
        /// size in VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// type in VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        /// </summary>
        public uint Type { get; set; }

        public override void FetchInfoFromShaderProgram(OpenGL gl, SharpGL.Shaders.ShaderProgram shaderProgram)
        {
            int location = shaderProgram.GetAttributeLocation(gl, this.VarNameInShader);
            if (location < 0) { throw new Exception(string.Format("key[{0}] NOT exists in shader!")); }
            this.AttribLocation = (uint)location;
        }

        public override void LayoutForVAO(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.BufferID);
            gl.VertexAttribPointer(this.AttribLocation, this.Size, this.Type, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(this.AttribLocation);
        }

        public override string ToString()
        {
            return string.Format("{0}, AttribLocation: {1}, VarNameInShader: {2}, Size: {3}, Type: {4}",
                base.ToString(), AttribLocation, VarNameInShader, Size, Type);
            //return base.ToString();
        }
    }
}
