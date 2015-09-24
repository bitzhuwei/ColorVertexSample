using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.DataSource;

namespace YieldingGeometryModel.loader
{
    public class DynamicUnstructureGeometryLoader
    {
        private static readonly string[] delimeters = { "\t", " " };

        public const int ElEMENT_FORMAT3_TRIANGLE = 3;
        public const int ELEMENT_FORMAT4_TETRAHEDRON = 4;

        public const int FRACTURE_FORMAT2_LINE = 2;
        public const int FRACTURE_FORMAT3_TRIANGLE = 3;

        public const int MARKER_FRACTURE = 1;
        public const int MARKER_FAULT = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathFileName"></param>
        /// <param name="nx"></param>
        /// <param name="ny"></param>
        /// <param name="nz"></param>
        /// <returns></returns>
        public DynamicUnstructuredGridderSource LoadSource(string pathFileName, int nx, int ny, int nz)
        {
            DynamicUnstructuredGridderSource src = new DynamicUnstructuredGridderSource();
            src.NX = nx;
            src.NY = ny;
            src.NZ = nz;
            StreamReader reader = Open(pathFileName);
            try
            {
                int lineCounter = 0;
                String headerDescriptor = ReadLine(reader, ref lineCounter);
                if (headerDescriptor == null)
                    throw new FormatException("unexpected end of file");
                if (!headerDescriptor.StartsWith("node"))
                    throw new FormatException("bad format,header descriptor missing");
                String head = ReadLine(reader, ref lineCounter);
                if (String.IsNullOrEmpty(head))
                    throw new FormatException("bad format,header mising");

                string[] heads = head.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                if (heads.Length < 6)
                    throw new FormatException("bad format, head not match");

                int total = src.DimenSize;
                //src.NX = total;//TODO：是否应由此处指定NX？
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

                bool gotFirstMin = false; bool gotFirstMax = false;
                Vertex min = new Vertex(), max = new Vertex();

                #region read nodes
                Vertex[] nodes = new Vertex[nodeNum];
                for (int i = 0; i < nodeNum; i++)
                {
                    String nodeLine = ReadLine(reader, ref lineCounter);
                    if (nodeLine == null)
                        throw new FormatException("unexpected end of node");

                    String[] fields = nodeLine.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length != 4)
                        throw new FormatException(String.Format("node format error,line:{0}", lineCounter));
                    float x = System.Convert.ToSingle(fields[0]);
                    float y = System.Convert.ToSingle(fields[1]);
                    float z = System.Convert.ToSingle(fields[2]);
                    nodes[i] = new Vertex(x, y, z);
                    if (!gotFirstMax)
                    {
                        max = nodes[i];
                        gotFirstMax = true;
                    }
                    else
                    {
                        max = VertexHelper.Max(max, nodes[i]);
                    }
                    if (!gotFirstMin)
                    {
                        min = nodes[i];
                        gotFirstMin = true;
                    }
                    else
                    {
                        min = VertexHelper.Min(min, nodes[i]);
                    }
                }
                #endregion

                src.Min = min;
                src.Max = max;

                #region read elements
                int[][] elements = new int[elemNum][];
                for (int i = 0; i < elemNum; i++)
                {
                    String elemLine = ReadLine(reader, ref lineCounter);
                    if (elemLine == null)
                        throw new FormatException("unexpected end of element");
                    String[] fields = elemLine.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length != elemFormat + 1)
                        throw new FormatException(String.Format("element format error, line:{0}", lineCounter));

                    int[] elemnt = new int[elemFormat + 1];
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

                src.NodeNum = nodeNum;
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

        private StreamReader Open(String fileName)
        {
            StreamReader reader = new StreamReader(new BufferedStream(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), 128 * 1024));
            return reader;
        }

        private string ReadLine(StreamReader reader, ref int lineCounter)
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

        class VertexHelper
        {
            public static Vertex Min(Vertex current, Vertex other)
            {
                var x = Math.Min(current.X, other.X);
                var y = Math.Min(current.Y, other.Y);
                var z = Math.Min(current.Z, other.Z);

                return new Vertex(x, y, z);
            }

            public static Vertex Max(Vertex current, Vertex other)
            {
                var x = Math.Max(current.X, other.X);
                var y = Math.Max(current.Y, other.Y);
                var z = Math.Max(current.Z, other.Z);

                return new Vertex(x, y, z);
            }

        }
    }
}
