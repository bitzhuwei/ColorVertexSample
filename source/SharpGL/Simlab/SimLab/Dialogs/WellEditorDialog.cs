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

namespace SimLab.Dialogs
{
    public partial class WellEditorDialog : Form
    {
        public WellEditorDialog()
        {
            InitializeComponent();
        }

        private float minZValue;
        private float maxZValue;
        private float maxDisplayZValue;
        private float radiusScale;
        Dictionary<Control, String> errors = new Dictionary<Control, string>();

        private void SetError(Control control, String error)
        {
            if (String.IsNullOrEmpty(error))
            {
                if (errors.ContainsKey(control))
                    errors.Remove(control);
            }
            else
            {
                if (!this.errors.ContainsKey(control))
                    this.errors.Add(control, error);
                else
                    this.errors[control] = error;
            }
            this.errorProvider1.SetError(control, error);
            this.UpdateControlEnables();
        }


        private void FormatZValue(Label lbl, float value)
        {
            lbl.Text = String.Format("{0}", value);
        }

        private void FormatTextBox(TextBox box, float value)
        {
            box.Text = String.Format("{0}", value);
        }

        private void UpdateControlEnables()
        {
             this.btnOK.Enabled = !this.HasError;
        }


        public float MinZValue
        {
            get
            {
                return this.minZValue;
            }
            set
            {
                this.minZValue = value;
                this.FormatZValue(this.lblMinZValue, value);
            }
        }

        public float MaxZValue
        {
            get { return this.maxZValue; }
            set
            {
                this.maxZValue = value;
                this.FormatZValue(this.lblMaxZValue, value);
            }
        }

        public float MaxDisplayZValue
        {
            get { return this.maxDisplayZValue; }
            set
            {
                this.maxDisplayZValue = value;
                this.FormatTextBox(this.tbxMaximumDisplayZ, value);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            float minValue = this.trackBar1.Minimum;
            float maxValue = this.trackBar1.Maximum;
            float value = this.trackBar1.Value;
            float d1 = value - minValue;
            float d = maxValue - minValue;
            float ratio = d1 / d;
            this.MaxDisplayZValue = this.MinZValue +  (this.MaxZValue - this.MinZValue) * ratio;
        }

        public float RadiusScale
        {
            get
            {
                return this.radiusScale;
            }
            set
            {
                this.radiusScale = value;
                this.FormatTextBox(this.tbxRadiusScale, value);
            }
        }

        private bool HasError
        {
            get
            {
                return this.errors.Keys.Count > 0;
            }
        }

        private void tbxRadiusScale_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.radiusScale = float.Parse(tbxRadiusScale.Text, CultureInfo.InvariantCulture);
                this.SetError(tbxRadiusScale, String.Empty);
            }
            catch (Exception err)
            {
                this.SetError(this.tbxRadiusScale, String.Format("invalid input:{0}", err.Message));
            }
        }

        private void tbxMaximumDisplayZ_TextChanged(object sender, EventArgs e)
        {

            try
            {
                this.maxDisplayZValue = float.Parse(this.tbxMaximumDisplayZ.Text, CultureInfo.InvariantCulture);
                this.SetError(this.tbxMaximumDisplayZ, String.Empty);
            }
            catch (Exception err)
            {
                this.SetError(this.tbxMaximumDisplayZ, String.Format("invalid input:{0}", err.Message));
            }
        }

    }
}
