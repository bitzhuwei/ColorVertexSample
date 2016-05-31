using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewer.Dialogs
{
    public class AxisMath
    {

    /// <summary>
    /// 返回小数点次要精度的位数
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
        public static int FracMinors(double value)
        {
            double intPart =  Math.Truncate(value);
            double fracPart = value - intPart;
            if (fracPart == 0)
                return 0;
            double logValue = Math.Log10(Math.Abs((double)fracPart));
            int result = (int)Math.Abs(Math.Floor(logValue)) + 1;
            return result;
        }

        public static int FracMinors(decimal value)
        {

            decimal intPart = Decimal.Truncate(value);
            decimal fracPart = value - intPart;
            if (fracPart == 0)
                return 0;
            double logValue = Math.Log10(Math.Abs((double)fracPart));
            int result = (int)Math.Abs(Math.Floor(logValue)) + 1;
            return result;
        }

        public static int FracMajors(double value){
            double intPart = Math.Truncate(value);
            double fracPart = value - intPart;
            if (fracPart == 0)
                return 0;
            double logValue = Math.Log10(Math.Abs((double)fracPart));
            int result = (int)Math.Abs(Math.Floor(logValue));
            return result;
        }


        /// <summary>
        /// 将主要精度的值后的小数位使用四舍五入舍弃，
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Trunc(double value){
           
           int major = FracMajors(value);
           double expand = Math.Pow(10.0d,major);
           double r = Math.Round(value*expand);
           double rvalue = r/expand;
           return rvalue;
    
        }

       



    }
}
