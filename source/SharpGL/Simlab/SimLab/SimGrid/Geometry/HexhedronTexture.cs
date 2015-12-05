using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.SimGrid.Geometry
{

    [StructLayout(LayoutKind.Sequential)]
    public struct HexhedronTextureCoordinates
    {
       public  float FLT;
       public  float FRT;
       public  float BRT;
       public  float BLT;

       public float FLB;
       public float FRB;
       public float BRB;
       public float BLB;

       public void SetCoord(float value)
       {
            FLT = value;
            FRT = value;
            BRT = value;
            BLT = value;
            FLB = value;
            FRB = value;
            BRB = value;
            BLB = value;
       }
    }
}
