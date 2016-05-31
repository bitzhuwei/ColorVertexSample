using SimLab.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewer.Dialogs
{
    public static class RangeEditExtension
    {

       public static ColorMapParams GetColorMapParams(this ColorBarRangeEditDialog rangeEditDialog){
          
          ColorMapParams result = new ColorMapParams();
          result.MinValue = rangeEditDialog.Minimum;
          result.MaxValue = rangeEditDialog.Maximum;
          result.Step = rangeEditDialog.Step;
          result.UseLogarithmic = rangeEditDialog.UseLogarithmic;
          result.IsAutomatic = rangeEditDialog.UseAutoRange;
          return result;
       }
    }
}
