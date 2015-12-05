using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.SimGrid
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle
    {
        public int dot0;
        public int dot1;
        public int dot2;
    }
}
