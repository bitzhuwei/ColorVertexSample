using SharpGL;
using SharpGL.SceneGraph;
using SimLab.SimGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{




    public abstract class BufferData : Disposable
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


        /// <summary>
        /// gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        /// 表示第2个参数
        /// </summary>
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


        /// <summary>
        /// Data指针指向的内存大小
        /// </summary>
         public int SizeInBytes
         {
             get { return this.sizeInBytes; }
             private set { this.sizeInBytes = value; }
         }

        /// <summary>
        /// GL_FLOAT etc
         /// gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
         /// 表示第3个参数
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

         protected void FreeMem()
         {
             if (this.Data != IntPtr.Zero)
             {
                 Marshal.FreeHGlobal(this.Data);
                 this.Data = IntPtr.Zero;
                 this.SizeInBytes = 0;
             }
         }

         protected override void DisposeUnmanagedResources()
         {
            //recuresive distroy
             if (this.Data != null)
             {
                   this.FreeMem();
             }
             base.DisposeUnmanagedResources();
         }
    }


    public class TextureCoordinatesBufferData:BufferData{

        public TextureCoordinatesBufferData(){
             this.GLDataType = OpenGL.GL_FLOAT;
        }


        private unsafe void DoDump()
        {

            if(this.GLDataType == OpenGL.GL_FLOAT){

                int texturesCount = this.SizeInBytes / sizeof(float);
                float* textures = (float *)this.Data;
                System.Console.WriteLine("textures:{0}", texturesCount);
                System.Console.WriteLine("==============textures BEGIN======================");
                for (int i = 0; i < texturesCount; i++)
                {
                     System.Console.WriteLine("{0}:{1}",i, textures[i]);
                }
                System.Console.WriteLine("==============textures END=======================");
            }
        }

        public void Dump()
        {
            this.DoDump();
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


          private unsafe void  DoDump(){
              if (this.GLDataType == OpenGL.GL_FLOAT)
              {
                  Vertex* positions = (Vertex *)this.Data;
                  int dimenSize = this.SizeInBytes / (sizeof(float)*this.GLSize);
                  Console.WriteLine(String.Format("Positions:{0}, Position Components:{1}",dimenSize, this.GLSize));
                  Console.WriteLine("=============Positions Start==================");
                  for (int i = 0; i < dimenSize; i++)
                  {
                      System.Console.WriteLine(String.Format("{0}: ({1},{2},{3})",i, positions[i].X,positions[i].Y,positions[i].Z));
                  }
                  Console.WriteLine("=============Positions End ==================");
              }
          }
            
          public void Dump()
          {
              this.DoDump();
          }
    }

    public class HexahedronPositionBufferData : PositionsBufferData
    {
        public HexahedronPositionBufferData()
        {
        }
    }


    
    public class WireFrameBufferData:BufferData{

        public WireFrameBufferData()
        {
            this.GLDataType = OpenGL.GL_INT;
        }

    }




    public class TriangleIndicesBufferData : BufferData
    {
        public TriangleIndicesBufferData()
        {
             this.GLDataType = OpenGL.GL_INT;
             this.GLSize = 1;
        }



        private unsafe void DoDump()
        {
            int trianglesCount = this.SizeInBytes / sizeof(TriangleIndex);
            TriangleIndex* triangles = (TriangleIndex*)this.Data;
            System.Console.WriteLine("Trinagles Count:{0}", trianglesCount);
            System.Console.WriteLine("====Triangles Indices==============");
            for (int i = 0; i < trianglesCount; i++)
            {
                System.Console.WriteLine(String.Format("{0}:({1},{2},{3})", i, triangles[i].dot0, triangles[i].dot1, triangles[i].dot2));
            }
            System.Console.WriteLine("====Triangles Indices  END==============");
        }

        public void Dump()
        {
            DoDump();
        }
    }
}
