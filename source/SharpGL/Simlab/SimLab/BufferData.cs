using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{




    public abstract class BufferData
    {
         private IntPtr dataPointer;
        
        /// <summary>
        /// 数据指针指向的字节数
        /// </summary>
         private int size;

         //GL_FLOAT ect.
         private uint glDataType;
         private int   num;

         public IntPtr Data
         {
              get { return dataPointer; }
              private set { this.dataPointer = value; }
         }

         public int Size
         {
             get { return this.size; }
             private set { this.size = value; }
         }

        /// <summary>
        /// GL_FLOAT etc
        /// </summary>
         public uint GLDataType
         {
             get { return this.glDataType; }
             protected  set { this.glDataType = value; }
         }


         public  void AllocMem(int size){
              IntPtr psize = (IntPtr)size;
              this.Data  = Marshal.AllocHGlobal(psize);
              this.Size = size;
         }

         public void FreeMem()
         {
             if (this.Data != IntPtr.Zero)
             {
                 Marshal.FreeHGlobal(this.Data);
                 this.Data = IntPtr.Zero;
             }
         }
    }


    public class TextureCoordinatesBufferData:BufferData{

        public TextureCoordinatesBufferData(){
             this.GLDataType = OpenGL.GL_FLOAT;
        }

    }

    public class HexahedronTextureCoordinatesBufferData : TextureCoordinatesBufferData
    {
        public HexahedronTextureCoordinatesBufferData(){
            
        }
    }


    public class PositionsBufferData : BufferData
    {
          public PositionsBufferData(){
              this.GLDataType = OpenGL.GL_FLOAT;
          }
    }

    public class HexahedronPositionBufferData : PositionsBufferData
    {
        public HexahedronPositionBufferData()
        {
        }

    }


    





    public class TriangleIndicesBufferData : BufferData
    {
        public TriangleIndicesBufferData()
        {
             this.GLDataType = OpenGL.GL_INT;
        }
    }
}
