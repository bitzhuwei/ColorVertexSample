using SharpGL.SceneGraph;
using SimLab.GridSource;
using SimLab.SimGrid.helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.SimGrid.Loader
{
    public class PointSetLoader
    {
        public static PointGridderSource LoadFromFile(string pathFileName, int nx, int ny, int nz)
        {
            StreamReader reader = new StreamReader(new FileStream(pathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            try
            {
                return DoLoadPointSet(reader, nx,ny,nz);
            }
            finally
            {
                reader.Close();
            }
        }

        public static PointGridderSource DoLoadPointSet(StreamReader reader, int nx, int ny, int nz)
        {

            int dimenSize = nx * ny * nz;
            PointGridderSource ps = new PointGridderSource();
            ps.NX = nx;
            ps.NY = ny;
            ps.NZ = nz;
            Vertex minValue = new Vertex();
            Vertex maxValue = new Vertex();
            char[] delimeters = new char[] { ' ', '\t' };
            string line;
            Vertex[] positions = new Vertex[dimenSize];
            int positionCount = 0;
            bool isSet = false;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (String.IsNullOrEmpty(line))
                    continue;
                string[] fields = line.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                if (fields.Length >= 3)
                {

                    float x = System.Convert.ToSingle(fields[0]);
                    float y = System.Convert.ToSingle(fields[1]);
                    float z = Math.Abs(System.Convert.ToSingle(fields[2])); //全部Z按深度来处理，

                    Vertex pt = new Vertex(x, y, z);
                    if (!isSet)
                    {
                        minValue = pt;
                        maxValue = pt;
                        isSet = true;
                    }
                    minValue = VertexHelper.MinVertex(minValue, pt);
                    maxValue = VertexHelper.MaxVertex(maxValue, pt);

                    positions[positionCount] = pt;
                    positionCount++;
                    if(positionCount == dimenSize)
                        break;
                }
            }
            if (positionCount!= dimenSize)
                throw new ArgumentException(String.Format("file format error,points number:{0} not equals DIMENS",positionCount,dimenSize));
            ps.Max = maxValue;
            ps.Min = minValue;
            return ps;
        }
    }
}
