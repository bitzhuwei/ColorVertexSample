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
    public class UnstructureGeometryLoader
    {
        private string[] delimeters = { "\t", " " };
        private int lineCounter = 0;
        private int _readBufferSize = 16 * 1024*1024;

        public UnStructuredGridderSource LoadSource(string modelPathName, string fractionPathName,string borderPathName)
        {
            if (!File.Exists(modelPathName) || !File.Exists(fractionPathName))
            {
                return null;
            }

            UnStructuredGridderSource source = this.LoadSourceModel(modelPathName);
            int[] fractionTags = this.LoadTags(fractionPathName);

            Dictionary<int, int[]> tagTriangles = this.ProcessTagTriangles(source);
            source.Fractions = processTagsTriangles(tagTriangles, fractionTags);

            if (File.Exists(borderPathName))
            {
                int[] borderTags = this.LoadTags(borderPathName);
                source.Borders = this.processTagsTriangles(tagTriangles, borderTags);
            }
            return source;
           
        }


        private Dictionary<int, int[]> ProcessTagTriangles(UnStructuredGridderSource source)
        {

            int[][] triangles = source.Triangles;
            Dictionary<int, int[]> tagTriangles = new Dictionary<int, int[]>();
            for (int i = 0; i < triangles.Length; i++)
            {
                int[] t = triangles[i];
                if (t[3] != -1)
                {
                    tagTriangles.Add(t[3], t);
                }
            }
            return tagTriangles;

        }


        /// <summary>
        /// 按tags顺序,在tagTraingles中找到tag同tags对应的三角形;
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        private int[][] processTagsTriangles(Dictionary<int, int[]> tagTriangles, int[] tags)
        {
           
            int[][] fractions = new int[tags.Length][];
            for (int i = 0; i < tags.Length; i++)
            {
                int tag = tags[i];
                int[] t = tagTriangles[tag];
                fractions[i] = t;
            }
            return fractions;
        }


        /// <summary>
        /// 加载标记文件
        /// </summary>
        /// <param name="pathFileName"></param>
        /// <returns></returns>
        private int[] LoadTags(string pathFileName)
        {
            StreamReader reader = this.Open(pathFileName);
            try
            {
                return this.DoReadTags(reader);
            }
            finally
            {
                reader.Close();
            }
        }

        private int[] DoReadTags(StreamReader reader)
        {
            String data = this.ReadLine(reader);
            if (data == null)
                throw new Exception("unexpected end of fractions file");
            int count = Convert.ToInt32(data);
            int[] triangles = new int[count];
            for (int i = 0; i < count; i++)
            {
                String value = this.ReadLine(reader);
                if (value == null)
                    throw new Exception("unexected end of fraction index file");
                triangles[i] = Convert.ToInt32(value);
            }
            return triangles;
        }



        private Vertex[] DoReadVertexes(StreamReader reader, int pointCount,out Vertex min, out Vertex max)
        {
            Vertex[] points = new Vertex[pointCount];
            String data;
            min = new Vertex() ;
            max = new Vertex();
            bool initFlag = false;
            for (int i = 0; i < pointCount; i++)
            {

                data = this.ReadLine(reader);
                if (data == null)
                    throw new Exception(String.Format("read Points unexpected end"));
                String[] values = data.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length < 3)
                    throw new Exception(String.Format("Unexpected points format,line:#{0}", lineCounter));
                float x = Convert.ToSingle(values[0]);
                float y = Convert.ToSingle(values[1]);
                float z = Convert.ToSingle(values[2]);
                Vertex p = new Vertex(x, y, z);
                if (!initFlag)
                {
                    min = p;
                    max = p;
                    initFlag = true;
                }
                VertexHelper.MinMax(p, ref min, ref max);
                points[i] = p;
                
            }
            return points;
        }

        //读取三角形定义
        private int[][] ReadTriangles(StreamReader reader, int triangleCount)
        {
            int[][] triangles = new int[triangleCount][];
            String data;
            for (int i = 0; i < triangleCount; i++)
            {
                data = this.ReadLine(reader);
                if (data == null)
                {
                    throw new Exception("unexpected end, triangles error");
                }
                String[] values = data.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length < 5)
                {
                    throw new Exception(String.Format("unexpected triangles format,@#{0}", lineCounter));
                }
                int p1 = Convert.ToInt32(values[1]);
                int p2 = Convert.ToInt32(values[2]);
                int p3 = Convert.ToInt32(values[3]);
                int pos = Convert.ToInt32(values[4]);
                int[] t = new int[] { p1, p2, p3,pos};
                triangles[i] = t;
            }
            return triangles;
        }

        private int[][] ReadTetrahedrons(StreamReader reader, int tetraCount)
        {
            String data;
            int[][] tetList = new int[tetraCount][];
            for (int i = 0; i < tetraCount; i++)
            {
                data = this.ReadLine(reader);
                if (data == null)
                {
                    throw new Exception("unexpected end, triangles error");
                }
                String[] values = data.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length < 6)
                {
                    throw new Exception(String.Format("unexpected triangles format,@#{0}", lineCounter));
                }
                int p1 =  Convert.ToInt32(values[1]);
                int p2 =  Convert.ToInt32(values[2]);
                int p3 =  Convert.ToInt32(values[3]);
                int p4 =  Convert.ToInt32(values[4]);
                int pos = Convert.ToInt32(values[5]);

                int[] tet = new int[] { p1, p2, p3, p4,pos};
                tetList[i] = tet;
            }
            return tetList;
        }

        private UnStructuredGridderSource DoLoadSourceModel(StreamReader reader){

            String headerLine = ReadLine(reader);
            if (headerLine == null)
                throw new Exception(String.Format("read header unexpected end. line:#{0}", lineCounter));

            String[] headers = headerLine.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
            if (headers.Length < 4)
            {
                throw new Exception(String.Format("header format, unexpected params numbers. line:#{0}", lineCounter));
            }
            int pointsCount = Convert.ToInt32(headers[0]);
            int triangleCount = Convert.ToInt32(headers[1]);
            int tetraCount = Convert.ToInt32(headers[2]);
            Vertex min, max;
            Vertex[] coords =  DoReadVertexes(reader, pointsCount,out min,out max);
            int[][] triangles = this.ReadTriangles(reader, triangleCount);
            int[][] tetras  = this.ReadTetrahedrons(reader, tetraCount);

            UnStructuredGridderSource gridder = new UnStructuredGridderSource();
            gridder.Coords = coords;
            gridder.Triangles = triangles;
            gridder.Tetras = tetras;
            gridder.Min = min;
            gridder.Max = max;
            return gridder;
        }


        private UnStructuredGridderSource LoadSourceModel(string modelPathFileName)
        {
            StreamReader reader = this.Open(modelPathFileName);
            try
            {
                return this.DoLoadSourceModel(reader);
            }
            finally
            {
                reader.Close();
            }

        }


        private StreamReader Open(String fileName)
        {
            StreamReader reader = new StreamReader(new BufferedStream(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), this._readBufferSize));
            return reader;
        }

        private string ReadLine(StreamReader reader)
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
          

        

    }
}
