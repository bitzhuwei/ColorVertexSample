﻿using SharpGL;
using SharpGL.SceneComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
{
    /// <summary>
    /// 描述一个顶点的半径。
    /// <para>一个顶点的半径信息由'1'个'float'描述。</para>
    /// </summary>
    public class PointRadiusBuffer : PropertyBuffer
    {
        public PointRadiusBuffer()
            : base(1, OpenGL.GL_FLOAT)//一个顶点的半径信息由'1'个'float'描述。
        {
        }

        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<float>(elementCount);
        }
    }
}