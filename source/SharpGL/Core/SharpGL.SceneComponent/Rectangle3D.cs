using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public struct Rectangle3D
    {


      private Vertex min;
      private Vertex max;

    

      public Rectangle3D(Vertex min, Vertex max){

         Vertex realMin, realMax;
         MakesureMinMax(min,max,out realMin, out realMax);
         this.min = realMin;
         this.max = realMax;

      }

      private static void MakesureMinMax(Vertex possibleMin, Vertex possibleMax, out Vertex realMin, out Vertex realMax){

          realMin = new Vertex();
          realMax = new Vertex();
          realMin = possibleMin;
          realMax = possibleMax;
          
          if(possibleMin.X > possibleMax.X){
            realMin.X = possibleMax.X;
            realMax.X = possibleMin.X;
          }
          if(possibleMin.Y > possibleMax.Y){
            realMin.Y = possibleMax.Y;
            realMax.Y = possibleMin.Y;
          }
          if(possibleMin.Z > possibleMax.Z){
            realMin.Z = possibleMax.Z;
            realMax.Z = possibleMin.Z;
          }
      }


      public Vertex Min{
        get{ return this.min;}
      }

      public Vertex Max{
        get{ return this.max;}
      }

      public Vertex Location{
          get{
            return this.min;
          }
      }

      public float SizeX{
         get{
           return this.GetSizeX();
         }
      }

      public float SizeY{
        get{
          return this.GetSizeY();
        }
      }

      public float SizeZ{
        get{
          return this.GetSizeZ();
        }
      }

      private float GetSizeX(){
         Vertex mx = this.min;
         mx.Y = this.max.Y;
         mx.Z = this.max.Z;
         return (float)((this.max - mx).Magnitude());
      }

      private float GetSizeY(){
         Vertex my = this.min;
         my.X = this.min.X;
         my.Z = this.min.Z;
         return (float)((this.max - my).Magnitude());
      }

      private float GetSizeZ(){
         Vertex mz = this.min;
         mz.X = this.min.X;
         mz.Y = this.min.Y;
         return (float)((this.max - mz).Magnitude());
      }


      /// <summary>
      /// Rect3D 的中心点
      /// </summary>
      public Vertex Center{
        get{
          return GetCenter();
        }
      }

      private  Vertex GetCenter(){
        
         Vertex distanceVector = this.max - this.min;
         double length = distanceVector.Magnitude();
         Vertex normVector = distanceVector;
         normVector.Normalize();
         float half = (float)(length/2.0);
         Vertex center =  this.min + normVector*half;
         return center;
      }

      public override string ToString()
      {
         return String.Format("({0},{1},{2}),({3},{4},{5})",
             this.min.X,this.min.Y,this.min.Z,
             this.max.X,this.max.Y,this.max.Z);
      }

      public void Union(Vertex point){

         if(this.min.X > point.X)
           this.min.X = point.X;
         if(this.min.Y > point.Y)
           this.min.Y = point.Y;
         if(this.min.Z > point.Z)
           this.min.Z = point.Z;
         if(this.max.X < point.X)
           this.max.X = point.X;
         if(this.max.Y < point.Y)
           this.max.Y = point.Y;
         if(this.max.Z <point.Z)
           this.max.Z = point.Z;
      }

      public void Union(Rectangle3D rect3D){
          this.Union(rect3D.min);
          this.Union(rect3D.max);
      }

    }
}
