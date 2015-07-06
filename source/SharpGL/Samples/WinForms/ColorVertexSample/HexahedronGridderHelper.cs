using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVertexSample
{
    static class HexahedronGridderHelper
    {
        /// <summary>
        /// 缩小目标顶点的位置。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        internal static void ZoomOut(ref Vertex target,  Vertex source)
        {
            if (target.X > source.X) { target.X = source.X; }
            if (target.Y > source.Y) { target.Y = source.Y; }
            if (target.Z > source.Z) { target.Z = source.Z; }
        }

        /// <summary>
        /// 放大目标顶点位置。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        internal static void ZoomIn(ref Vertex target,  Vertex source)
        {
            if (target.X < source.X) { target.X = source.X; }
            if (target.Y < source.Y) { target.Y = source.Y; }
            if (target.Z < source.Z) { target.Z = source.Z; }
        }


        internal static void GetMaxMinPosition(this GeometryModel.Gridder.HexahedronGridder gridder, out Vertex min, out Vertex max)
        {
            min = gridder.Cells[0].blb;
            max = gridder.Cells[0].blb;
            foreach (var cell in gridder.Cells)
            {
                ZoomOut(ref min, cell.blb);
                ZoomOut(ref min, cell.blt);
                ZoomOut(ref min, cell.brb);
                ZoomOut(ref min, cell.brt);
                ZoomOut(ref min, cell.flb);
                ZoomOut(ref min, cell.flt);
                ZoomOut(ref min, cell.frb);
                ZoomOut(ref min, cell.frt);

                ZoomIn(ref max, cell.blb);
                ZoomIn(ref max, cell.blt);
                ZoomIn(ref max, cell.brb);
                ZoomIn(ref max, cell.brt);
                ZoomIn(ref max, cell.flb);
                ZoomIn(ref max, cell.flt);
                ZoomIn(ref max, cell.frb);
                ZoomIn(ref max, cell.frt);
            }
        }
        internal static ScientificModel GetModel(this GeometryModel.Gridder.HexahedronGridder gridder)
        {
            if (gridder == null) { return null; }

            Random random = new Random();

            const int factor = 14;
            ScientificModel model = new ScientificModel(gridder.Cells.Length * factor, SharpGL.Enumerations.BeginMode.TriangleString);
            
            // se binding box.
            Vertex min , max;
            GetMaxMinPosition(gridder, out min, out max);
            model.BoundingBox.Set(min.X, min.Y, min.Z, max.X, max.Y, max.Z);

            // set positions and colors.
            int modelIndex =0;
            for (int index = 0; index < gridder.Cells.Length; index++)
            {
                GeometryModel.GLPrimitive.Hexahedron cell = gridder.Cells[index];
                Vertex[] vertexes = new Vertex[factor] { cell.frt, cell.frb, cell.brt, cell.brb, cell.blb, cell.frb, cell.flb, cell.frt, cell.flt, cell.brt, cell.blt, cell.blb, cell.flt, cell.flb, };

                foreach (Vertex vertex in vertexes)
                {
                    model.Positions[modelIndex + 0] = vertex.X;
                    model.Positions[modelIndex + 1] = vertex.Y;
                    model.Positions[modelIndex + 2] = vertex.Z;

                    model.Colors[modelIndex + 0] = cell.color.R;
                    model.Colors[modelIndex + 1] = cell.color.G;
                    model.Colors[modelIndex + 2] = cell.color.B;
                    modelIndex += 3;
                }
            }

            // set first, count and primitive count for MultiDrawArrays()
            int[] first = new int[gridder.Cells.Length];
            for (int i = 0; i < first.Length; i++)
            {
                first[i] = i * factor;
            }
            model.First = first;
            int[] count = new int[gridder.Cells.Length];
            for (int i = 0; i < count.Length; i++)
            {
                count[i] = factor;
            }
            model.Count = count;
            model.PrimitiveCount = gridder.Cells.Length;

            //// set random color for now
            //for (int i = 0; i < model.Colors.Length; i++)
            //{
            //    model.Colors[i] = (float)random.NextDouble();
            //}
            //for (int i = 0; i < gridder.Cells.Length; i++)
            //{
            //    model.Colors[i * 3 * factor] = gridder.Cells[i].color.R;
            //    model.Colors[i * 3 * factor + 1] = gridder.Cells[i].color.G;
            //    model.Colors[i * 3 * factor + 2] = gridder.Cells[i].color.B;
            //}

            return model;

            //Random random = new Random();
            //Vertex min = new Vertex(), max = new Vertex();
            //bool isInit = false;

            //for (int i = 0; i < model.VertexCount; i++)
            //{
            //    var x = (float)((maxPosition.X - minPosition.X) * random.NextDouble() + minPosition.X);
            //    var y = (float)((maxPosition.Y - minPosition.Y) * random.NextDouble() + minPosition.Y);
            //    var z = (float)((maxPosition.Z - minPosition.Z) * random.NextDouble() + minPosition.Z);
            //    if (!isInit)
            //    {
            //        min = new Vertex(x, y, z);
            //        max = new Vertex(x, y, z);
            //        isInit = true;
            //    }
            //    if (x < min.X) min.X = x;
            //    if (x > max.X) max.X = x;
            //    if (y < min.Y) min.Y = y;
            //    if (y > max.Y) max.Y = y;
            //    if (z < min.Z) min.Z = z;
            //    if (z > max.Z) max.Z = z;

            //    model.Positions[i * 3 + 0] = x;
            //    model.Positions[i * 3 + 1] = y;
            //    model.Positions[i * 3 + 2] = z;

            //    model.Colors[i * 3 + 0] = (float)random.NextDouble();
            //    model.Colors[i * 3 + 1] = (float)random.NextDouble();
            //    model.Colors[i * 3 + 2] = (float)random.NextDouble();
            //}

            //model.BoundingBox.Set(min.X, min.Y, min.Z, max.X, max.Y, max.Z);
            //throw new NotImplementedException();
        }
    }
}
