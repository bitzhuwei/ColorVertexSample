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
        public const uint ATTRIB_INDEX_POSITION = 0;



         private IntPtr dataPointer;
        
        /// <summary>
        /// 数据指针指向的字节数
        /// </summary>
         private int sizeInBytes;


         /// <summary>
         /// 
         /// </summary>
         private uint attribIndex;
         
        /// <summary>
        /// 
        /// </summary>
         private uint glType;


        /// <summary>
         /// gl.VertexAttribPointer(
        /// </summary>
         private int   glSize;

         public int GLSize
         {
             get { return glSize; }
             protected set { this.glSize = value; }
         }

         public IntPtr Data
         {
              get { return dataPointer; }
              private set { this.dataPointer = value; }
         }

         public int SizeInBytes
         {
             get { return this.sizeInBytes; }
             private set { this.sizeInBytes = value; }
         }

        /// <summary>
        /// GL_FLOAT etc
        /// </summary>
         public uint GLDataType
         {
             get { return this.glType; }
             protected  set { this.glType = value; }
         }

        /// <summary>
         /// gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
         /// 指定第一个参数
        /// </summary>
         public uint GLAttribIndex
         {
             get { return this.attribIndex; }
             protected set { this.attribIndex = value; }
         }


         public  void AllocMem(int size){
              IntPtr psize = (IntPtr)size;
              this.Data  = Marshal.AllocHGlobal(psize);
              this.SizeInBytes = size;
         }

         public void FreeMem()
         {
             if (this.Data != IntPtr.Zero)
             {
                 Marshal.FreeHGlobal(this.Data);
                 this.Data = IntPtr.Zero;
                 this.SizeInBytes = 0;
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
              this.GLAttribIndex = BufferData.ATTRIB_INDEX_POSITION;
              this.GLSize = 3;
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
