using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.DataSource
{
    /// <summary>
    /// 无结构的四面体网格，包含二维无结构和三维四面体网格的格式,
    /// 文件内容分为三个段,依次为nodes,elements,factures.
    /// nodes为（x,y,z,0)的数组
    /// elements元素为nodes数组的索引,element[ELEMENT_FORMAT3+1](三角形) 或element[ELEMENT_FORMAT4+1](4面体)
    /// fratures元素为node数组的索引, fracture[FRACTURE_FORMAT2+1] (线段） 或fracture[FRACTURE_FORMAT3+1](三角形)]  
    /// elements.Length+fractures.Length = NX*NY*NZ ,通常NY,NZ =1， 所以NX = (elements.length+fratures.length)
    /// </summary>
    public class DynamicUnstructureGridderSource:GridderSource
    {
        private static readonly string[] delimeters = { "\t", " " };

        public const int  ElEMENT_FORMAT3_TRIANGLE =3;
        public const int  ELEMENT_FORMAT4_TETRAHEDRON =4;

        public const int  FRACTURE_FORMAT2_LINE = 2;
        public const int  FRACTURE_FORMAT3_TRIANGLE = 3;

        public const int  MARKER_FRACTURE = 1;
        public const int  MARKER_FAULT = 2;

        //文件头定义
        /// <summary>
        /// 点的个数
        /// </summary>
        private int nodeNum;


        public int NodeNum
        {
            get { return nodeNum; }
            protected set { nodeNum = value; }
        }

        private Vertex[] nodes;

        public Vertex[] Nodes
        {
            get
            {
                return nodes;
            }
            private set
            {
                this.nodes = value;
            }
        }

        /// <summary>
        /// 如果nodeInElem 为NODE_FORMAT3 时，element部分表示三角形，elem
        /// </summary>
        private int elementNum;

        public int ElementNum
        {
            get
            {
                return this.elementNum;
            }
            private set
            {
                elementNum = value;
            }
        }


        /// <summary>
        /// 基质几何结构描述
        /// </summary>
        private int[][] elements;

        public int[][] Elements
        {
            get
            {
                return this.elements;
            }
            private set
            {
                this.elements = value;
            }
        }


        /// <summary>
        /// 基质格式定义
        /// 当值为ElEMENT_FORMAT3,表示elements段为三角型，此时任意element为elements[i][ELEMENT_FORMAT3+1]，
        /// ELEMENT_FORMAT4时表示为四面体,此时elements[i][ELEMENT_FORMAT4+1]四面体
        /// 每个element数组最后一个描述保留，值为0
        /// </summary>
        private int elementFormat;


        public int ElementFormat
        {
            get { return this.ElementFormat; }
            private set { this.elementFormat = value; }
        }



        /// <summary>
        /// 断层和裂缝数
        /// </summary>
        private int fractureNum;


        /// <summary>
        /// 断层和裂缝数
        /// </summary>
        public int FractureNum
        {
            get { return this.fractureNum; }
            private set { this.fractureNum = value; }
        }


        private int[][] fractures;

        public int[][] Fractures
        {
            get { return this.fractures; }
            private set { this.fractures = value; }
        }

        /// <summary>
        /// FRACTURE_FORMAT2是 fractures[i][FRACTURE_FORMAT2+1]
        /// FRACTURE_FORMAT2是 fractures[i][FRACTURE_FORMAT3+1]
        /// fracure[]中最后一个数组元素表示MARKER
        /// </summary>
        private int fractureFormat;

        public int FractureFormat
        {
            get
            {
                return this.fractureFormat;
            }
            private set
            {
                this.fractureFormat = value;
            }
        }



        private static StreamReader Open(String fileName)
        {
            StreamReader reader = new StreamReader(new BufferedStream(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), 128*1024));
            return reader;
        }

        private static string ReadLine(StreamReader reader,ref int lineCounter)
        {
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                lineCounter++;
                line = line.Trim();
                if (String.IsNullOrEmpty(line))
                    continue;
                else
                    break;
            }
            return line;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathFileName"></param>
        /// <param name="nx"></param>
        /// <param name="ny"></param>
        /// <param name="nz"></param>
        /// <returns></returns>
        public static DynamicUnstructureGridderSource Load(string pathFileName, int nx, int ny, int nz)
        {
            DynamicUnstructureGridderSource src = new DynamicUnstructureGridderSource();
            src.NX = nx;
            src.NY = ny;
            src.NZ = nz;
            StreamReader reader = Open(pathFileName);
            try
            {
                int lineCounter=0;
                String headerDescriptor = ReadLine(reader, ref lineCounter);
                if (headerDescriptor == null)
                    throw new FormatException("unexpected end of file");
                if (!headerDescriptor.StartsWith("node"))
                    throw new FormatException("bad format,header descriptor missing");
                String head = ReadLine(reader, ref lineCounter);
                if (String.IsNullOrEmpty(head))
                    throw new FormatException("bad format,header mising");

                string[] heads =  head.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                if (heads.Length < 6)
                    throw new FormatException("bad format, head not match");

                int total = src.DimenSize;
                int nodeNum, elemNum, elemFormat, fracNum, fracFormat;

                #region read header
                nodeNum = System.Convert.ToInt32(heads[0]);
                 elemNum = System.Convert.ToInt32(heads[1]);
                 elemFormat = System.Convert.ToInt32(heads[2]);
                 fracNum = System.Convert.ToInt32(heads[3]);
                 fracFormat = System.Convert.ToInt32(heads[4]);
                 if (total != (elemNum + fracNum))
                     throw new FormatException("bad format, not match grid dimens");

                 if (elemFormat != ElEMENT_FORMAT3_TRIANGLE && elemFormat != ELEMENT_FORMAT4_TETRAHEDRON)
                     throw new FormatException("bad format, unknown element format");
                 if (fracFormat != FRACTURE_FORMAT2_LINE && fracFormat != FRACTURE_FORMAT3_TRIANGLE)
                     throw new FormatException("bad format, unknown frac format");

                #endregion 

                #region read nodes
                 Vertex[] nodes = new Vertex[nodeNum];
                 for (int i = 0; i < nodeNum; i++)
                 {
                     String nodeLine = ReadLine(reader, ref lineCounter);
                     if (nodeLine == null)
                         throw new FormatException("unexpected end of node");

                     String[] fields = nodeLine.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                     if(fields.Length !=4)
                         throw new FormatException(String.Format("node format error,line:{0}",lineCounter));
                     float x = System.Convert.ToSingle(fields[0]);
                     float y = System.Convert.ToSingle(fields[1]);
                     float z = System.Convert.ToSingle(fields[2]);
                     nodes[i] = new Vertex(x,y,z);
                 }
                #endregion 

                #region read elements
                int[][] elements = new int[elemNum][];
                for(int i=0; i<elemNum; i++){
                    String elemLine = ReadLine(reader, ref lineCounter);
                    if (elemLine == null)
                       throw new FormatException("unexpected end of element");
                    String[] fields = elemLine.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length != elemFormat + 1)
                      throw new FormatException(String.Format("element format error, line:{0}", lineCounter));

                    int[] elemnt = new int[elemFormat+1];
                    for (int j = 0; j < elemnt.Length; j++)
                    {
                        elemnt[j] = System.Convert.ToInt32(fields[j]);
                    }
                    elements[i] = elemnt;
                }
                #endregion 
                #region read fracture
                int[][] fractures = new int[fracNum][];
                for (int i = 0; i < fracNum; i++)
                {

                    String fracLine = ReadLine(reader, ref lineCounter);
                    if (fracLine == null)
                        throw new FormatException("unexpected end of element");
                    String[] fields = fracLine.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length != fracFormat + 1)
                        throw new FormatException(String.Format("element format error, line:{0}", lineCounter));

                    int[] frac = new int[fracFormat + 1];
                    for (int j = 0; j < frac.Length; j++)
                    {
                        frac[j] = System.Convert.ToInt32(fields[j]);
                    }
                    fractures[i] = frac;
                }
                #endregion 

                src.nodeNum=nodeNum;
                src.Nodes = nodes;
                src.ElementFormat = elemFormat;
                src.ElementNum = elemNum;
                src.Elements = elements;
                src.FractureFormat = fracFormat;
                src.FractureNum = fracNum;
                src.Fractures = fractures;
                return src;
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
