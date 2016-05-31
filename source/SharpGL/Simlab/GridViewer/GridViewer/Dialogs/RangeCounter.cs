using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewer.Dialogs
{
    internal class RangeCounter
    {
       double minValue;
       double maxValue;
       bool minIncluded;
       bool maxIncluded;

       double counter =0;

       public double MinValue{
          get{ return this.minValue;}
          set{ this.minValue = value;}
       }

       public double MaxValue{
          get{ return this.maxValue;}
          set{ this.maxValue = value;}
       }

       public bool IncludeMin{
          get{ return this.minIncluded;}
          set{ this.minIncluded = value;}
       }

       public bool IncludeMax{
          get{ return this.maxIncluded;}
          set{ this.maxIncluded = value;}
       }

       public double Counter{
          get{ return this.counter;}
          set{ this.counter = value;}
       }

       public String Label{

          get{
            StringBuilder sb = new StringBuilder();
            if(this.IncludeMin){
               sb.Append("[");
            }else{
               sb.Append("(");
            }  
            sb.Append(minValue);
            sb.Append(",");
            sb.Append(maxValue);
            if(this.IncludeMax)
               sb.Append("]");
            else
               sb.Append(")");
            return sb.ToString();        
          }
       }

       public override string ToString()
       {
           return this.Label;
       }

       public bool IsInRange(double value){

          bool result = false;
          if(IncludeMin){
            if(value < minValue)
              return result;
          }else{
            if(value <= minValue)
              return result;
          }
          if(IncludeMax){
             if(value > maxValue)
               return result;
          }else
            if(value >= maxValue)
              return result;
          return true;
          
       }
    }
}
