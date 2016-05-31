using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridViewer.Commands
{
    public class Command
    {

        private ToolStripItem[] toolStripItems;
        private Func<object, bool> canEnabled;
        
        public Command(ToolStripItem[] stripItems,Func<object,bool> canEnabled)
        {
            this.toolStripItems = stripItems;
            this.canEnabled  = canEnabled;
        }

        
        public void UpdateUI(object param){

           bool isEnabled =  this.canEnabled(param);
           foreach(ToolStripItem item in toolStripItems){
             item.Enabled = isEnabled;
           }
        }
    }
}
