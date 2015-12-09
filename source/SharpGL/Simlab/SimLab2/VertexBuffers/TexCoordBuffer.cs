using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
{
    //TODO: use 3 for positions, 1 for texture coordinates. use eum to do this.
    /// <summary>
    /// 用贴图坐标来描述一个顶点的颜色。
    /// <para>本项目中的Color Palette可用一维贴图描述，所以这里只保存横向坐标（UV中的U）</para>
    /// <para>一个顶点的颜色信息由'1'个'float'描述。</para>
    /// </summary>
    public abstract class TexCoordBuffer : PropertyBuffer
    {

        /// <summary>
        /// 用贴图坐标来描述一个顶点的颜色。
        /// <para>本项目中的Color Palette可用一维贴图描述，所以这里只保存横向坐标（UV中的U）</para> 
        /// </summary>
        public TexCoordBuffer()
            : base(1, OpenGL.GL_FLOAT)// 一个顶点的颜色信息由'1'个'float'描述。
        {
        }


        private unsafe void DoDump()
        {

            if (this.GLDataType == OpenGL.GL_FLOAT)
            {

                int texturesCount = this.SizeInBytes / sizeof(float);
                float* textures = (float*)this.Data;
                System.Console.WriteLine("textures:{0}", texturesCount);
                System.Console.WriteLine("==============textures BEGIN======================");
                for (int i = 0; i < texturesCount; i++)
                {
                    System.Console.WriteLine("{0}:{1}", i, textures[i]);
                }
                System.Console.WriteLine("==============textures END=======================");
            }
        }

        public void Dump()
        {
            this.DoDump();
        }

    }
}
