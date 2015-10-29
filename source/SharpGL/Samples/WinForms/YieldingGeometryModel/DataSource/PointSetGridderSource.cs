using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.DataSource
{
    public class PointSetGridderSource : PointSpriteGridderSource
    {
       private List<Vertex> points;

       private float radius = 0.1f;

       private float originalRadius = 1.0f;

       /// <summary>
       /// 模型中每个点的半径大小
       /// </summary>
       private float[] modelPointRadius;

       private float[] radiusArray;

       public float Radius
       {
           get
           {
               return this.radius;
           }
           set
           {
               if(value >0.0d)
                 this.radius = value;
           }
       }

       public float OriginalRadius
       {
           get
           {
               return this.originalRadius;
           }
           set
           {
               this.originalRadius = value;
           }
       }

       public List<Vertex> Points
       {
           get
           {
               return this.points;
           }
           set
           {
               this.points = value;
           }
       }

       public float[] RadiusArray
       {
           get
           {
               return this.radiusArray;
           }
           set
           {
               this.radiusArray = value;
           }
       }

       public float[] ModelPointRadius
       {
           get
           {
               return this.modelPointRadius;
           }

           set
           {
               this.modelPointRadius = value;
           }

       }

      



       public override Vertex GetPosition(int i, int j, int k)
       {
           int gridIndex;
           IJK2Index(i, j, k,out gridIndex);
           return points[gridIndex];

       }

       public override float GetRadius(int i, int j, int k)
       {
           if(this.radiusArray == null||this.radiusArray.Length<=0)
               return this.radius;
           int gridIndex;
           IJK2Index(i, j, k, out gridIndex);
           return this.radiusArray[gridIndex];
       }

    }
}
