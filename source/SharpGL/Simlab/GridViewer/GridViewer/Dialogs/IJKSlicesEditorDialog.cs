using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridViewer.Dialogs
{
    public partial class IJKSlicesEditorDialog : Form
    {
        /// <summary>
        /// I,J,K 相关控件例子
        /// </summary>
        internal class DirectionControls{

           public CheckedListBox CoordList{
              get;set;
           }
           public NumericUpDown  Every{
              get;set;
           }
           public CheckBox All{
              get;set;
           }

           public Label InfoLabel{
              get;set;
           }
           public int ID{
              get;set;
           }

           public void Init(int n){
               this.CoordList.BeginUpdate();
               CoordList.Items.Clear();
               for (int i = 1; i <= n; i++)
               {
                  CoordList.Items.Add(i);
               }
               this.CoordList.EndUpdate();
               Every.Minimum = 1;
               Every.Maximum = n;
               Every.Increment = 1;
           }


           public List<int> GetCheckedItemList(){

              CheckedListBox.CheckedItemCollection checkedItems =CoordList.CheckedItems;
              List<int> list = new List<int>();
              for(int i=0; i<checkedItems.Count; i++){
                list.Add((int)checkedItems[i]);
              }
              return list;
           }

           public void SetCheckedItemList(List<int> checkValues){
              
              this.CoordList.BeginUpdate();
              for(int i=0;i<CoordList.Items.Count; i++){
                this.CoordList.SetItemChecked(i,false);
              }
              for(int i=0; i<checkValues.Count; i++){
                 int index = checkValues[i]-1;
                 this.CoordList.SetItemChecked(index,true);
              }
              this.CoordList.EndUpdate();
              this.UpdateDirectionInfo();
           }

           public void SetEveryChecked(){
              
              int every = (int)this.Every.Value;
              this.CoordList.BeginUpdate();
              for (int i = 0; i < CoordList.Items.Count; i++)
              {
                 this.CoordList.SetItemChecked(i, false);
              }
              for(int chth = 1; chth <= this.CoordList.Items.Count;chth+=every){

                 this.CoordList.SetItemChecked(chth-1,true);

              }
              this.CoordList.EndUpdate();
              this.UpdateDirectionInfo();
           }

           public void SetAllChecked(){
              this.SetAllChecked(this.All.Checked);
           }

           public void SetAllChecked(bool isChecked){

               this.CoordList.BeginUpdate();
               for (int i = 0; i < CoordList.Items.Count; i++)
               {
                   this.CoordList.SetItemChecked(i, isChecked);
               }
               this.CoordList.EndUpdate();
               this.UpdateDirectionInfo();
           }

           private void UpdateDirectionInfo()
           {
               this.InfoLabel.Text = String.Format("Selected:{0},Total:{1}", CoordList.CheckedItems.Count, CoordList.Items.Count);
           }

           public bool IsBelong(object control){
              if(Object.Equals(control,this.CoordList))
                 return true;
              if(Object.Equals(control,this.All))
                return true;
              if(Object.Equals(control,this.Every))
                return true;
              if(Object.Equals(control,this.InfoLabel))
                return true;
              return false;
           }
        }

        private List<DirectionControls> directionsControl;

        public event EventHandler Apply;

        private int ni;
        private int nj;
        private int nk;

        public List<int> ISlices{
           get{
             return GetCheckedDirections(this.directionsControl[0]);
           }
           set{
              this.directionsControl[0].SetCheckedItemList(value);
              
           }
        }

        public List<int> JSlices{
            get
            {
                return GetCheckedDirections(this.directionsControl[1]);
            }
            set
            {
                this.directionsControl[1].SetCheckedItemList(value);
            }
        }

        public List<int> KSlices{

            get
            {
                return GetCheckedDirections(this.directionsControl[2]);
            }
            set
            {
                this.directionsControl[2].SetCheckedItemList(value);
            }

        }



        private List<int> GetCheckedDirections(DirectionControls direction){
            return direction.GetCheckedItemList();
        }
        private void SetCheckedDirections(DirectionControls direction,List<int> selectedValues){
            direction.SetCheckedItemList(selectedValues);
        }


        private void RaiseApply(){
          if(this.Apply!=null){
            this.Apply(this,EventArgs.Empty);
            this.IsApplyed = true;
          }
        }
        

        public IJKSlicesEditorDialog()
        {
            InitializeComponent();

            this.directionsControl = this.BuildDirectionControls();
        }

        private List<DirectionControls>  BuildDirectionControls(){

           List<DirectionControls> directions = new List<DirectionControls>();
           directions.Add(new DirectionControls(){CoordList= this.clbIList,Every = this.nudEveryI,All = this.cbIAll,InfoLabel=this.lblInfoI,ID=0});
           directions.Add(new DirectionControls(){CoordList = this.clbJList,Every = this.nudEveryJ,All = this.cbJAll,InfoLabel=this.lblInfoJ,ID=1});
           directions.Add(new DirectionControls(){CoordList = this.clbKList,Every = this.nudEveryK,All = this.cbKAll,InfoLabel=this.lblInfoK,ID=2});
           return directions;
        }

        
        public int NI{

            get{ return this.ni;}
            set{
               this.ni = value;
               this.directionsControl[0].Init(value);
            }
        }


        public int NJ{
           get{ return this.nj;}
           set{ 
             this.nj = value;
             this.directionsControl[1].Init(value);
           }
        }



        public int NK{
            get{
              return this.nk;
            }
            set{
              this.nk = value;
              this.directionsControl[2].Init(value);
            }
        }

        public bool IsApplyed{
           get;set;
        }

      

        private void UpdateDirectionInfo(DirectionControls d, int n)
        {
           d.InfoLabel.Text = String.Format("Selected:{0},Total:{1}",d.CoordList.CheckedItems.Count,d.CoordList.Items.Count);
        }

        private DirectionControls GetDirectionControls(object control){

           for(int i=0; i<this.directionsControl.Count; i++){

              if(this.directionsControl[i].IsBelong(control))
                 return this.directionsControl[i];
           }
           return null;
        }

        private void OnEveryValueChanged(object sender, EventArgs e)
        {

            DirectionControls direction = GetDirectionControls(sender);
            if(direction!=null)
              direction.SetEveryChecked();
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            DirectionControls direction = GetDirectionControls(sender);
            if(direction!=null)
              direction.SetAllChecked();
        }

        private void ApplyClick(object sender, EventArgs e)
        {
           this.RaiseApply();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
           this.Apply = null;

        }

        private void CloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
