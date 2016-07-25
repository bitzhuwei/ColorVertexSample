using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using Simlab.Well;
using SimLab.GridSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracyEnergy.Simba.Data.Well;

namespace SimLabBridge
{
    public class Well3DTrajectoryHelper
    {
        private GridderSource  gridder;
        private IScientificCamera camera;
        private List<WellTrajectory> wellTrajectoryList;

        public Well3DTrajectoryHelper(GridderSource source, IScientificCamera camera,List<WellTrajectory> wells){
           this.gridder = source;
           this.camera = camera;
           this.wellTrajectoryList = wells;
        }


        private float GetRadius(GridderSource source){

           Rectangle3D rect = source.TransformedActiveBounds;
           float dx = rect.SizeX;
           float dy = rect.SizeY;

           float wellRadius;
           #region decide the well radius
           if (dx != dy)
           {
               float min = Math.Min(dx, dy);
               float max = Math.Max(dx, dy);

               if (min > max * 0.75f)
               {
                   int n = 30;
                   wellRadius = (min / n) * 0.5f;
               }
               else if (min >= max * 0.5f) //长方形的模型
               {
                   int n = 20;
                   wellRadius = (min / n) * 0.5f;
               }
               else if (min >= (max * 0.25))
               {
                   int n = 15;
                   wellRadius = (min / n) * 0.5f;
               }
               else if (min >= max * 0.16)
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
           return wellRadius;
        }

        protected Well Convert(WellTrajectory well){

            List<Vertex> wellPath = new List<Vertex>();
            String wellName = well.WellName;
            float wellRadius = GetRadius(this.gridder);
            GLColor wellPathColor = new GLColor(0.0f,1.0f,0.0f,1.0f);//green
            GLColor textColor = new GLColor(1.0F,1.0F,1.0F,1.0F);
            foreach(WellTrajectoryItem item in well.Path){
              Vertex v = new Vertex(item.XCoord,item.YCoord,item.TVDSS);
              wellPath.Add(v);
            }
            Well well3D = new Well(camera,wellPath,wellRadius,wellPathColor,wellName,textColor,18);
            well3D.ZAxisScale = 1.0f;
            well3D.Transform = this.gridder.ScaleTranslateform;
            well3D.CreateVisualElements(this.camera);
            return well3D;
        }

        public List<Well> Convert(){

            List<Well> wells = new List<Well>();
            foreach(WellTrajectory wt in this.wellTrajectoryList){
              Well well =   Convert(wt);
              if(well!=null)
                 wells.Add(well);
            }
            return wells;

        }



    }
}
