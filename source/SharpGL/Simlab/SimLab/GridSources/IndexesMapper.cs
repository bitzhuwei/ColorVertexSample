using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridSources
{
    internal class GridIndexesMapper
    {
         private List<int>[] mapper;

         public GridIndexesMapper(int[] imaps){

           if(imaps == null||imaps.Length <=0){
             this.mapper = null;
             return;
           }
     
            mapper = new List<int>[imaps.Length];
            for(int i=0; i<imaps.Length; i++){
              int j = imaps[i]-1;
              if(mapper[j]==null){
                 mapper[j] = new List<int>();
              }   
              mapper[j].Add(i);
            }
         }

         public int[] MapIndexes(int blockIndex){
            
            if(this.mapper == null)
               return new int[]{blockIndex};
            else{
               if(this.mapper[blockIndex] == null)
                  return new int[]{blockIndex};
               return mapper[blockIndex].ToArray();
            }
         }
    }
}
