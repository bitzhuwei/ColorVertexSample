using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{




    public abstract class BufferData
    {
         private IntPtr dataPointer;
         private int size;

         //GL_FLOAT ect.
         private uint glDataType;
         private int   num;

         public IntPtr Data
         {
              get { return dataPointer; }
              set { this.dataPointer = value; }
         }

         public int Size
         {
             get { return this.size; }
             set { this.size = value; }
         }

        /// <summary>
        /// GL_FLOAT etc
        /// </summary>
         public uint GLDataType
         {
             get { return this.glDataType; }
             protected  set { this.glDataType = value; }
         }
    }


    public class GridCoordsBufferData : BufferData
    {
          public GridCoordsBufferData(){
              this.GLDataType = OpenGL.GL_FLOAT;
          }
         

    }
}
