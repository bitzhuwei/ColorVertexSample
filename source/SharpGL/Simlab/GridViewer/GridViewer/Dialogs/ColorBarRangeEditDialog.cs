using SimLab.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TracyEnergy.Simba.Data.Keywords.impl;

namespace GridViewer.Dialogs
{
    public partial class ColorBarRangeEditDialog : Form
    {

        
        private GridBlockProperty gbp;

        public ColorBarRangeEditDialog()
        {
            InitializeComponent();

            this.nudMinimum.Minimum = decimal.MinValue;
            this.nudMinimum.Maximum = decimal.MaxValue;
            this.nudMaximum.Minimum = decimal.MinValue;
            this.nudMaximum.Maximum = decimal.MaxValue;
            this.barChart.Series.Clear();
        }

        

        private void CheckBoxUseLogCheckedChanged(object sender, EventArgs e)
        {
           this.tbLogBase.Enabled = this.UseLogarithmic;
           this.ValidateRangeStepValue();
        }

        private void ResetClick(object sender, EventArgs e)
        {
           
        }

        

        private Series CreateSeries(List<RangeCounter> rangeCounters){

             Series s = new Series();
             s.Name ="Distributation";
             foreach(RangeCounter r in rangeCounters){
               s.Points.AddXY(r.Label,r.Counter);
             }
             s.ChartType = SeriesChartType.Column;
             return s;
        }

        private void CreateChartSeries(){
           
            barChart.Series.Clear();
            List<RangeCounter> rangeCounters = this.StatRangeCounters(this.gbp,Minimum,this.Maximum,this.Step,this.UseLogarithmic,this.Logbase);
            Series series = CreateSeries(rangeCounters);
            series.ChartArea = barChart.ChartAreas[0].Name;
            barChart.Series.Add(series);
        }
       
        

        private List<RangeCounter> StatRangeCounters(GridBlockProperty gbp,double minValue,double maxValue,double step,bool useLog, double logBase){
           
           List<RangeCounter> rangeCounters = CreateRangeCounters(minValue,maxValue,step,useLog, logBase);
           float[] values = gbp.Values;
           for(int i=0; i<values.Length; i++){
              double value = values[i];
              foreach(RangeCounter r in rangeCounters){
                 if(r.IsInRange(value)){
                   r.Counter +=1;
                   break;
                 }
              }
           }
           return rangeCounters;
        }

        private List<RangeCounter> CreateRangeCounters(double minValue, double maxValue, double step,bool useLog, double logBase){
           
            List<RangeCounter> rangeCounters = new List<RangeCounter>();
           
            if(useLog){
               minValue = Math.Log(minValue, logBase);
               maxValue =  Math.Log(maxValue,logBase);
               step =     Math.Log(step,logBase);
            }

            int count = (int)Math.Round(Math.Ceiling((maxValue - minValue)/step));
            for(int i=0; i<count; i++){
              RangeCounter r = new RangeCounter();
              double min = minValue+i*step;
              if(useLog)
                min = AxisMath.Trunc(Math.Pow(logBase,min));

              r.MinValue = min;
              if(i==0){
               r.IncludeMin = true;
              }else{
               r.IncludeMin = false;
              }

              double max = minValue+(i+1)*step;
              if(useLog)
                max = AxisMath.Trunc(Math.Pow(logBase,max));
              r.MaxValue = max;

              if(r.MaxValue > (useLog? Math.Pow(logBase,maxValue):maxValue))
                r.MaxValue = useLog? AxisMath.Trunc(Math.Pow(logBase,maxValue)):maxValue;

              r.IncludeMax = true;
              rangeCounters.Add(r);
            }
            return rangeCounters;
        }

        

        /// <summary>
        /// 最小值
        /// </summary>
        public double Minimum{

           get{
             return (double)nudMinimum.Value;
           }
           set{
             decimal d = (decimal)value;
             this.nudMinimum.DecimalPlaces = AxisMath.FracMinors(d);
             this.nudMinimum.Value = d;
             this.ValidateRangeStepValue();
           }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public double Maximum{
           get{
              return (double)nudMaximum.Value;
           }
           set{
             decimal d = (decimal)value;
             this.nudMinimum.DecimalPlaces = AxisMath.FracMinors(d);
             nudMaximum.Value = d;
             
             this.ValidateRangeStepValue();
           }
        }

        private bool LogBaseHasError{
           get{
              return !String.IsNullOrEmpty(this.errorProvider.GetError(this.tbLogBase));
           }
        }

        private bool MinimumHasError{
           get{
              return !String.IsNullOrEmpty(this.errorProvider.GetError(this.nudMinimum));
           }
        }

        private bool MaximumHasError{
           get{
             return !String.IsNullOrEmpty(this.errorProvider.GetError(this.nudMaximum));
           }
        }

        private bool StepHasError{

           get{
              return !String.IsNullOrEmpty(this.errorProvider.GetError(this.tbxStep));
           }
        }

        private void ValidateColorMapParams(){

           
           //validate base
           this.errorProvider.SetError(this.tbLogBase,String.Empty);
           if(this.UseLogarithmic){
              double logBase;
              try{
                logBase = System.Convert.ToDouble(this.tbLogBase.Text);
                if(logBase<=1.0d)
                  this.errorProvider.SetError(this.tbLogBase,"can not less or equal 1");
              }catch(Exception err){
                this.errorProvider.SetError(this.tbLogBase,err.Message);
              }
           }
          

           this.errorProvider.SetError(this.nudMaximum,String.Empty);
           this.errorProvider.SetError(this.nudMinimum,String.Empty);
           if (this.Minimum >= this.Maximum)
           {
               this.errorProvider.SetError(this.nudMaximum, String.Format("{0} value less than {1}", lblMaximum.Text, lblMinValue.Text));
           };

           if(!this.MinimumHasError){
              if(this.UseLogarithmic&&!this.LogBaseHasError){
                 if(this.Minimum <=0.0d){
                   this.errorProvider.SetError(this.nudMinimum,"logarithm can not <=0");
                 }
              }
           }
           if(!this.MaximumHasError){

              if(this.UseLogarithmic&&!this.LogBaseHasError){
                 if(this.Maximum <=0.0d){
                   this.errorProvider.SetError(this.nudMaximum,"logarithm can not <=0");
                 }
              }
           }

           this.errorProvider.SetError(this.tbxStep,String.Empty);
           double stepValue;
           try
           {
               stepValue = System.Convert.ToDouble(tbxStep.Text, CultureInfo.InvariantCulture);
           }
           catch (Exception err)
           {
               this.errorProvider.SetError(this.tbxStep, err.Message);
           }

           if(!this.StepHasError){
              stepValue = this.Step;
              if(stepValue <=0.0D)
                this.errorProvider.SetError(this.tbxStep,"can not be negative or zero");
           }
           
           if(!this.StepHasError){
             if(!this.MinimumHasError&&!this.MaximumHasError){
               
               if(!this.UseLogarithmic){

                   double d = this.Maximum - this.Minimum;
                   stepValue = this.Step;
                   int stepCount = (int)Math.Ceiling(d / stepValue);
                   if (stepCount >= 12)
                       this.errorProvider.SetError(this.tbxStep, "value is too small");
                   else if (stepCount <= 1)
                       this.errorProvider.SetError(this.tbxStep, "value is too big"); 
                        
               }else{
                  //log arithmic
                  if(!this.LogBaseHasError){
                     stepValue = this.Step;
                     double logMinimum = Math.Log(this.Minimum,this.Logbase);
                     double logMaximum = Math.Log(this.Maximum,this.Logbase);
                     double logStep = Math.Log(stepValue,this.Logbase);
                     if(logStep <=0.0d)
                       this.errorProvider.SetError(this.tbxStep, "too small for logArithmic");

                     if(!this.StepHasError){
                       double logD = logMaximum - logMinimum;
                       int stepCount = (int)Math.Ceiling(logD/logStep);
                       if(stepCount >=12)
                         this.errorProvider.SetError(this.tbxStep,"too small for logArithmic");
                       else if(stepCount <=1)
                         this.errorProvider.SetError(this.tbxStep,"too big for logArithmic");
                     }
                  }
               }
             }
           }




        }

        private void ValidateRangeStepValue(){

           this.ValidateColorMapParams();

        }

        

        public double Step{
           get{
             return System.Convert.ToDouble(tbxStep.Text,CultureInfo.InvariantCulture);
           }
           set{
            
             this.tbxStep.Text = value.ToString();
             this.ValidateRangeStepValue();
             this.nudMinimum.Increment =(decimal)value;
             this.nudMaximum.Increment =(decimal)value;
           
           }
        }

        public bool UseLogarithmic{
           get{
              return  this.cbxUseLog.Checked;
           }
           set{
              this.cbxUseLog.Checked = value;
              this.ValidateRangeStepValue();
           }
        }

        public float Logbase{
          get{
             return System.Convert.ToSingle(tbLogBase.Text,CultureInfo.InvariantCulture);
          }
          set{
             tbLogBase.Text = value.ToString();
             this.ValidateRangeStepValue();
          }
        }

        public bool UseAutoRange{
           get{
             return this.cbxUseAuto.Checked;
           }
           set{
             this.cbxUseAuto.Checked = value;
             this.ValidateRangeStepValue();
           }
        }

        public GridBlockProperty GridBlockPropery{
           get { return gbp;}
           set {
              this.gbp = value;
              if(this.gbp != null)
                this.InitAutoRange(this.gbp);

              if(!this.IsRangeStepHasError){
                 this.CreateChartSeries();
              }
           }
        }

       
       

        private void InitAutoRange(GridBlockProperty gbp){
           
           if(!this.UseAutoRange)
              return;
           double axisMin;
           double axisMax;
           double step;
           ColorMapAxisAutomation.Automate(gbp.MinValue,gbp.MaxValue,out axisMin, out axisMax,out step);
           this.Minimum = axisMin;
           this.Maximum = axisMax;
           this.Step = step;
        }


        private int FracPartMinorCount(decimal value){
            
            decimal intPart =   Decimal.Truncate(value);
            decimal fracPart =  value - intPart;
            if(fracPart == 0)
              return 0;
            double logValue = Math.Log10(Math.Abs((double)fracPart));
            int result =  (int)Math.Abs(Math.Floor(logValue))+1;
            return result;
        }

        private void UseAutoCheckedChanged(object sender, EventArgs e)
        {
           this.nudMinimum.Enabled = !UseAutoRange;
           this.nudMaximum.Enabled = !UseAutoRange;
           if(this.UseAutoRange){
              this.UseLogarithmic = !UseAutoRange;
           }
           this.cbxUseLog.Enabled = !UseAutoRange;
           this.tbxStep.Enabled = !UseAutoRange;

        

           if(this.UseAutoRange){
             if(this.GridBlockPropery!=null){
                this.InitAutoRange(this.GridBlockPropery);
             }
           }
        }

        private bool IsRangeStepHasError{

           get{

              string minError = this.errorProvider.GetError(this.nudMinimum);
              string maxError = this.errorProvider.GetError(this.nudMaximum);
              string stepError = this.errorProvider.GetError(this.tbxStep);
              if(String.IsNullOrEmpty(minError)&&
                 String.IsNullOrEmpty(maxError)&&
                 String.IsNullOrEmpty(stepError)){
                return false;
              }
              return true;
           }
        }

        private bool HasErrors{
           
           get{
              Control[] controls ={this.nudMaximum,this.nudMinimum,this.tbxStep,this.tbLogBase,this.cbxUseLog};
              foreach(Control c in controls){

               String error = this.errorProvider.GetError(c);
               if(!String.IsNullOrEmpty(error))
                  return true;
              }
              return false;
           }

        }

        private void OnMinimumValidating(object sender, CancelEventArgs e)
        {
            //System.Console.WriteLine(nudMinimum.Value);
        }

        private void OnMinimumValueChanged(object sender, EventArgs e)
        {
            this.nudMinimum.DecimalPlaces = AxisMath.FracMinors(this.nudMinimum.Value);
        }

        private void OnMaximumValueChanged(object sender, EventArgs e)
        {
           this.nudMaximum.DecimalPlaces = AxisMath.FracMinors(this.nudMaximum.Value);
        }

        private void OnStepValueTextChanged(object sender, EventArgs e)
        {
            this.ValidateRangeStepValue();
        }

        private void CaculateStatClick(object sender, EventArgs e)
        {
           if(!this.IsRangeStepHasError){
             this.CreateChartSeries();
           }
        }

        private void OnWindowClosing(object sender, FormClosingEventArgs e)
        {
              if(this.DialogResult == DialogResult.OK){
                 if(this.HasErrors)
                   e.Cancel = true;
              }
             
        }
       
    }
}
