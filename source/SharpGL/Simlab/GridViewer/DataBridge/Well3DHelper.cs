using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using Simlab.Well;
using SimLab.GridSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracyEnergy.Simba.Data;
using TracyEnergy.Simba.Data.Impl;
using TracyEnergy.Simba.Data.Keywords.impl;


namespace SimLabBridge
{
    public abstract class Well3DHelper
    {
        public List<Well> Convert(IList<WellSpecs> wells, WellCompatCollection wellCompSegments = null)
        {
            List<Well> result = new List<Well>();
            if (wells != null)
            {
                foreach (WellSpecs wellspec in wells)
                {
                    Well wellInfo = this.Convert(wellspec, wellCompSegments);
                    if (wellInfo != null)
                    {
                        result.Add(wellInfo);
                    }
                }
            }

            return result;
        }

        protected GLColor MapFluidToColor(Fluid fluid)
        {
            GLColor color = new GLColor();
            if (fluid == Fluid.OIL)
            {
                //green
                color.R = 0.0f;
                color.G = 1.0f;
                color.B = 0.0f;
                color.A = 1.0f;
            }
            else if (fluid == Fluid.WATER)
            {
                //blue
                color.R = 0.0f;
                color.G = 0.0f;
                color.B = 1.0f;
                color.A = 1.0f;
            }
            else if (fluid == Fluid.GAS)
            {
                //red
                color.R = 1.0f;
                color.G = 0.0f;
                color.B = 0.0f;
                color.A = 1.0f;
            }
            else
            {
                //默认按油井处理
                color.R = 1.0f;
                color.G = 0.5F;
                color.B = 0.0f;
            }
            return color;
        }

        protected abstract Well Convert(WellSpecs wellspec, WellCompatCollection wellCompSegments = null);
    }



    public class HexahedronGridderWell3DHelper : Well3DHelper
    {
        private HexahedronGridderSource gridder;
        private IScientificCamera camera;

        public HexahedronGridderWell3DHelper(HexahedronGridderSource hexahedronGridder, IScientificCamera camera)
        {
            this.gridder = hexahedronGridder;
            this.camera = camera;
        }

        /// <summary>
        /// 三维空间上a,b点的中点
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Vertex CenterOfLine(Vertex a, Vertex b)
        {
            Vertex vec = b - a;
            float Length = (float)vec.Magnitude();
            vec.Normalize();
            return (a + vec * (Length * 0.5f));
        }


        protected override Well Convert(WellSpecs wellspec, WellCompatCollection wellCompSegments = null)
        {

            int locI = wellspec.Li;
            int locJ = wellspec.Lj;


            //if compat has position ,use compat
            List<WellCompat> segments = null;
            if (wellCompSegments != null)
                segments = wellCompSegments.GetWellSegments(wellspec.WellName);
            if (segments != null && segments.Count > 0)
            {
                locI = segments[0].PosI;
                locJ = segments[0].PosJ;
            }

            Vertex minCord = this.gridder.FlipTransform * this.gridder.SourceActiveBounds.Min;
            Vertex maxCord = this.gridder.FlipTransform * this.gridder.SourceActiveBounds.Max;
            Rectangle3D rectSrc = new Rectangle3D(minCord,maxCord);

            if (!this.gridder.IsSliceBlock(locI, locJ))
            {
                return null;
            }




            Vertex h1 = this.gridder.FlipTransform * this.gridder.PointFLT(locI, locJ, 1);
            Vertex h2 = this.gridder.FlipTransform * this.gridder.PointBRT(locI, locJ, 1);
            Vertex d0 = h2 - h1;
            float L = (float)d0.Magnitude();
            d0.Normalize();
            Vertex vec = d0 * (L * 0.5f);
            Vertex comp1 = CenterOfLine(h1, h2); ; //地层完井段的起始点


            Vertex modelTop = new Vertex(comp1.X, comp1.Y, rectSrc.Max.Z);


            float mdx = rectSrc.SizeX;
            float mdy = rectSrc.SizeY;
            float mdz = rectSrc.SizeZ;

            float xyextend = System.Math.Max(mdx, mdy); //XY平面的最大边长
            float extHeight; //延长线段
            if (mdz < 0.1f * xyextend) //z很小
            {
                extHeight = 0.1f * xyextend;
            }
            else if (mdz < 0.2f * xyextend)
            {
                extHeight = mdz * 0.5f;
            }
            else if (mdz < 0.3f * xyextend)
            {
                extHeight = mdz * 0.25f;
            }
            else if (mdz < 0.4f * xyextend)
            {
                extHeight = mdz * 0.2f;
            }
            else
            {
                extHeight = 0.2f * mdz;
            }

            //地表坐标


            Vertex direction = new Vertex(0, 0, 1.0f);
            Vertex head = modelTop + direction * extHeight;

            //确定井的半径
            float wellRadius;
            #region decide the well radius
            if (mdx < mdy)
            {
                if (mdy * 0.5f >= mdx) //长方形的模型
                {
                    int n = gridder.NX;
                    if (n >= 10)
                    {
                        wellRadius = (mdx / n) * 0.5f;
                    }
                    else
                    {
                        wellRadius = (mdx / n) * 0.35f;
                    }

                }
                else
                {
                    int n = gridder.NX;
                    if (n >= 10)
                    {
                        n = 10;
                        wellRadius = (mdx / n) * 0.5f;
                    }
                    else
                    {
                        wellRadius = (mdx / n) * 0.35f;
                    }
                }
            }
            else if (mdx == mdy)
            {
                int n = Math.Min(gridder.NX, gridder.NY);
                if (n >= 10)
                {
                    n = 10;
                    wellRadius = (mdx / n) * 0.85f;
                }
                else
                {
                    wellRadius = (mdx / n) * 0.5f;
                }
            }
            else
            {

                if (mdx * 0.5f >= mdy)
                {
                    int n = gridder.NY;
                    if (n > 10)
                    {
                        n = 10;
                        wellRadius = (mdy / n) * 0.5f;
                    }
                    else
                    {
                        wellRadius = (mdy / n) * 0.35f;
                    }

                }
                else
                {

                    int n = gridder.NY;
                    if (n > 10)
                    {
                        n = 10;
                        wellRadius = (mdx / n) * 0.5f;
                    }
                    else
                    {
                        wellRadius = (mdx / n) * 0.35f;
                    }
                }

            }
            #endregion

            Fluid fluid = FluidConverter.Convert(wellspec.Fluid);
            GLColor pipeColor = MapFluidToColor(fluid);
            GLColor textColor = new GLColor(1.0f, 1.0f, 1.0f, 1.0f);
            string wellName = wellspec.WellName;


            List<Vertex> wellPath = new List<Vertex>();
            wellPath.Add(head);
            wellPath.Add(modelTop);

            #region start decide the trajery of the well
            {
                int lastI = locI;
                int lastJ = locJ;
                int lastK = -1;
                Vertex lastVertex = comp1;
                int segCount = segments.Count;
                for (int i = 0; i < segCount; i++)
                {
                    WellCompat compseg = segments[i];
                    int compI = compseg.PosI;
                    int compJ = compseg.PosJ;
                    int compK1 = compseg.K1;
                    int compK2 = compseg.K2;
                    if (compK1 == compK2)//同一网格上
                    {
                        if ((lastI != compI) || (lastJ != compJ)||(lastK != compK1))
                        {
                            Vertex s1 = gridder.FlipTransform * gridder.PointFLT(compI, compJ, compK1);
                            Vertex s2 = gridder.FlipTransform * gridder.PointBRT(compI, compJ, compK1);
                            Vertex point = CenterOfLine(s1, s2);
                            wellPath.Add(point);
                            lastI = compI;
                            lastJ = compJ;
                            lastK = compK1;
                        }
                    }
                    else //compK1 != compK2
                    {
                        //k1 coord
                        if ((lastI != compI) || (lastJ != compJ) || (lastK != compK1))
                        {
                            Vertex s1 = gridder.FlipTransform * gridder.PointFLT(compI, compJ, compK1);
                            Vertex s2 = gridder.FlipTransform * gridder.PointBRT(compI, compJ, compK1);
                            Vertex point = CenterOfLine(s1, s2);
                            wellPath.Add(point);
                        }
                        lastI = compI;
                        lastJ = compJ;
                        lastK = compK1;

                        if ((lastI != compI) || (lastJ != compJ) || (lastK != compK2))
                        {
                            Vertex s1 = gridder.FlipTransform * gridder.PointFLT(compI, compJ, compK2);
                            Vertex s2 = gridder.FlipTransform * gridder.PointBRT(compI, compJ, compK2);
                            Vertex point = CenterOfLine(s1, s2);
                            wellPath.Add(point);
                        }
                        lastI = compI;
                        lastJ = compJ;
                        lastK = compK2;
                    }
                }//end for

                Well well3d = new Well(this.camera, wellPath, wellRadius, pipeColor, wellName, textColor, 12, wellName.Length * 10);
                well3d.ZAxisScale = 1;
                well3d.Transform = this.gridder.ScaleTranslateform;
                well3d.CreateVisualElements(this.camera);
                return well3d;
            }
            #endregion
        }

    }




    public class PointSetGridderWell3DHelper : Well3DHelper
    {
        private PointGridderSource gridderSource;
        private IScientificCamera camera;



        public PointSetGridderWell3DHelper(PointGridderSource source, IScientificCamera camera)
        {
            this.gridderSource = source;
            this.camera = camera;

        }

        private System.Windows.Point ArcPoint(double R, double degree)
        {
            double deg = Math.PI * degree / 180.0;
            double x = Math.Cos(deg) * R;
            double y = Math.Sin(deg) * R;
            System.Windows.Point p = new System.Windows.Point(x, y);
            return p;
        }

        private IList<System.Windows.Point> ArcPoints(double R, double[] degrees)
        {
            IList<System.Windows.Point> points = new List<System.Windows.Point>();
            for (int i = 0; i < degrees.Length; i++)
            {
                System.Windows.Point p = ArcPoint(R, degrees[i]);
                points.Add(p);
            }
            return points;
        }

        private IList<System.Windows.Point> ArcPath(double R)
        {
            double[] degrees = new double[] { 0, 10, 20, 30, 40, 50, 60, 70, 80 };
            return ArcPoints(R, degrees);
        }

        /// <summary>
        ///  半径，完井段
        /// </summary>
        /// <param name="R"></param>
        /// <param name="surface"></param>
        /// <param name="segments"></param>
        /// <returns></returns>
        private IList<Vertex> ArcWellPath(double R, Vertex surface, IList<Vertex> segments)
        {
            IList<Vertex> path = new List<Vertex>();
            if (segments.Count < 2)
            {
                return path;
            }
            //1. 延长segments线段
            Vertex p0 = segments[0];
            Vertex p1 = segments[1];
            Vertex extDirection = p0 - p1;
            extDirection.Normalize();


            Vertex ps = new Vertex(surface.X, surface.Y, surface.Z);

            IList<System.Windows.Point> arcs = this.ArcPath(R);
            for (int i = 0; i < arcs.Count; i++)
            {

                System.Windows.Point distance = arcs[i];
                float extDistance = (float)distance.X;
                float extDepth = (float)distance.Y;
                Vertex pext = p0 + (extDirection * extDistance);
                ps.X = pext.X;
                ps.Y = pext.Y;
                Vertex vertDirection = pext - ps;
                vertDirection.Normalize();
                Vertex pdest = ps + (vertDirection * extDepth);
                path.Add(pdest);
            }
            ///完井段增加
            foreach (Vertex seg in segments)
            {
                path.Add(seg);
            }
            return path;
        }




        protected override Well Convert(WellSpecs wellspec, WellCompatCollection wellCompSegments = null)
        {

            if (wellspec == null)
                return null;

            String wellName = wellspec.WellName;

            int i = -1;
            int j = -1;
            int k = -1;

            List<WellCompat> wellSegments = null;
            if (wellCompSegments != null)
            {
                wellSegments = wellCompSegments.GetWellSegments(wellName);
            }

            if (wellSegments != null && wellSegments.Count > 0)
            {

                i = wellSegments[0].PosI;
                j = wellSegments[0].PosJ;
                k = wellSegments[0].K1;
            }
            else
            {

                i = wellspec.Li;
                j = wellspec.Lj;
                k = 1; //点集K始终为1.
            }

            Vertex  minCord = this.gridderSource.FlipTransform*this.gridderSource.SourceActiveBounds.Min;
            Vertex  maxCord = this.gridderSource.FlipTransform*this.gridderSource.SourceActiveBounds.Max;
            Rectangle3D srcRect = new Rectangle3D(minCord,maxCord);

            float dx = srcRect.SizeX;
            float dy = srcRect.SizeY;
            float dz = srcRect.SizeZ;

          
           

            //井在完井段的第一个点
            Vertex pb = this.gridderSource.FlipTransform*this.gridderSource.GetPosition(i, j, k); //well head or heal coordinates

          

            Vertex boundMin = srcRect.Min;
            Vertex boundMax = srcRect.Max;

           

            float surfaceZ = srcRect.Max.Z;
            float bottomZ =  srcRect.Min.Z;
           

            //井同模型边界的垂直相交点
            Vertex modelTop = new Vertex(pb.X, pb.Y, surfaceZ); //模型顶部坐标，垂直于井底坐标
            Vertex modelBottom = new Vertex(pb.X,pb.Y,bottomZ);

            //底到顶的方向 
            Vertex directVect = modelTop - modelBottom;
            //井的高度 z的正方向
            Vertex direct = directVect;
            direct.Normalize();

            float xyextend = System.Math.Max(dx, dy); //XY平面的最大边长
            float extHeight; //延长线段
            if (dz < 0.1f * xyextend) //z很小
            {
                extHeight = 0.1f * xyextend;
            }
            else if (dz < 0.2f * xyextend)
            {
                extHeight = dz * 0.5f;
            }
            else if (dz < 0.3f * xyextend)
            {
                extHeight = dz * 0.25f;
            }
            else if (dz < 0.4f * xyextend)
            {
                extHeight = dz * 0.2f;
            }
            else
            {
                extHeight = 0.2f * dz;
            }

            float maxValue = Math.Max(Math.Max(dx, dy), dz);
            float extraL = extHeight;
            direct.Normalize();
            Vertex head = modelTop + (direct * extraL);

            float wellRadius;
            #region decide the well radius
            if (dx != dy)
            {
                float min = Math.Min(dx,dy);
                float max = Math.Max(dx,dy);

                if(min > max*0.75f){
                    int n = 30;
                    wellRadius = (min / n) * 0.5f;
                }
                else if (min>=max * 0.5f) //长方形的模型
                {
                    int n = 20;
                    wellRadius = (min / n) * 0.5f;
                }
                else if (min>=(max * 0.25))
                {
                    int n = 15;
                    wellRadius = (min / n) * 0.5f;
                }
                else if (min>=max * 0.16)
                {
                    int n = 15;
                    wellRadius = (min / n) * 0.5f;
                }
                else
                {
                    int n = 10;
                    wellRadius = (min / n) * 0.5f;
                }
            }
            else
            {
                int n = 40;
                wellRadius = (dx / n) * 0.5f;
            }
            #endregion

            float diameter = Math.Min(dx, dy) * 0.01f;//井的直径

            List<Vertex> wellPath = new List<Vertex>();
            wellPath.Add(head);
            wellPath.Add(modelTop);

            //计算完井段的数据
            if (wellSegments != null && wellSegments.Count > 0)
            {

                foreach (WellCompat seg in wellSegments)
                {
                    int posI = seg.PosI;
                    int posJ = seg.PosJ;
                    int posK1 = seg.K1;
                    int posK2 = seg.K2;
                    if (posK1 == posK2)
                    {
                        Vertex pnt = this.gridderSource.FlipTransform*this.gridderSource.GetPosition(posI, posJ, posK1);
                        wellPath.Add(pnt);
                    }
                    else
                    {
                        Vertex pnt = this.gridderSource.FlipTransform*this.gridderSource.GetPosition(posI, posJ, posK1);
                        Vertex pn2 = this.gridderSource.FlipTransform*this.gridderSource.GetPosition(posI, posJ, posK2);
                        wellPath.Add(pnt);
                    }
                }
            }
            Fluid fluid = FluidConverter.Convert(wellspec.Fluid);
            GLColor pipeColor = this.MapFluidToColor(fluid);
            Well well3d = new Well(this.camera, wellPath, wellRadius, pipeColor, wellName, null, 16);
            well3d.ZAxisScale = 1;
            well3d.Transform = this.gridderSource.ScaleTranslateform;
            well3d.CreateVisualElements(this.camera);
            return well3d;
        }

    }




}
