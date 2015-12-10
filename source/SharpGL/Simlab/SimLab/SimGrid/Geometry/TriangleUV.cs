using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.SimGrid.Geometry
{

    /// <summary>
    /// 三角形对应的纹理
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TriangleUV
    {
        public float P1;
        public float P2;
        public float P3;

        public void SetTextureCoord(float value)
        {
            this.P1 = value;
            this.P2 = value;
            this.P3 = value;
        }
    }
}
