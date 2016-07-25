using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimLab.Dialogs
{
    public partial class DynamicSourceLayerEditDialog : Form
    {
        private int matrixCount;
        private List<int> layersList = new List<int>();

        public DynamicSourceLayerEditDialog()
        {
            InitializeComponent();
        }

        private void InitLayers()
        {

            cbLayersList.BeginUpdate();
            try
            {
                cbLayersList.Items.Clear();
                for (int i = 0; i <= 100; i++)
                {

                    if (i == 0)
                    {
                        cbLayersList.Items.Add(i);
                        continue;
                    }
                    int modeResult = this.matrixCount % i;
                    if (modeResult == 0)
                      cbLayersList.Items.Add(i);
                }
            }
            finally
            {
                cbLayersList.EndUpdate();
            }
        }

        public int MatrixCount
        {
            get
            {
                return this.matrixCount;
            }
            set
            {
                this.matrixCount = value;
                lblMatrixCount.Text = String.Format("{0} matrix objects", value);
                this.PrepareLayersList();
            }
        }

        /// <summary>
        /// 初始化可选层的列表
        /// </summary>
        private void PrepareLayersList()
        {
            this.InitLayers();
            this.cbLayersList.SelectedItem = 0;
        }

        private void LayersListSelectedValueChanged(object sender, EventArgs e)
        {
            int selectedLayers = (int)this.cbLayersList.SelectedItem;
            this.cblVisibleLayers.Items.Clear();
            this.cblVisibleLayers.BeginUpdate();
            for (int layerNumber = 1; layerNumber <= selectedLayers; layerNumber++)
            {
                this.cblVisibleLayers.Items.Add(layerNumber);
                this.cblVisibleLayers.SetItemChecked(layerNumber-1,false);
            }
            this.cblVisibleLayers.EndUpdate();
        }

        

        private List<int> GetLayerVisibles(){

          System.Windows.Forms.CheckedListBox.CheckedItemCollection checkedItems =  this.cblVisibleLayers.CheckedItems;
          List<int> results = new List<int>();
          foreach(object item in checkedItems){
             results.Add((int)item);
          }
          return results;
        }



        private void SetLayerVisibles(List<int> layerNumberList){

           if(layerNumberList==null)
              return;

           for(int i=0; i<cblVisibleLayers.Items.Count; i++){
              int layerNumber = i+1;
              if(layerNumberList.IndexOf(layerNumber)>=0)
                cblVisibleLayers.SetItemChecked(i,true);
              else
                cblVisibleLayers.SetItemChecked(i,false);
           }
         
        }

        public List<int> VisibleLayers{

           get{
             return this.GetLayerVisibles();
           }
           set{
             this.SetLayerVisibles(value);
           }

        }

        


        public int MatrixLayers{
           get{
              return  (int)this.cbLayersList.SelectedItem;
           }
           set{
              this.cbLayersList.SelectedItem = value;
           }
        }

    }
}
