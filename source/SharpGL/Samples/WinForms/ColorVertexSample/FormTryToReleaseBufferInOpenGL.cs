using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneComponent;
using YieldingGeometryModel;
using SharpGL.SceneComponent.Model;

namespace ColorVertexSample
{
    public partial class FormTryToReleaseBufferInOpenGL : Form
    {
        public FormTryToReleaseBufferInOpenGL()
        {
            InitializeComponent();

            InitilizeViewTypeControl();

        }

        private void InitilizeViewTypeControl()
        {
            foreach (string item in Enum.GetNames(typeof(ViewTypes)))
            {
                this.cmbViewType.Items.Add(item);
            }

            foreach (string item in Enum.GetNames(typeof(CameraTypes)))
            {
                this.cmbCameraType.Items.Add(item);
            }
        }

        private float[] initArray(int size, float value)
        {
            float[] result = new float[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = value;
            }
            return result;
        }

        private void Create3DObject(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                this.timer1.Enabled = true;
                btnStart.Text = "Stop";
            }
            else
            {
                this.timer1.Enabled = false;
                this.btnStart.Text = "Start";
                this.startedCycle = 0;
                this.stoppedCycle = 0;
            }

        }

        private List<string> rangeMin = new List<string>() { "-1000", "1100", "3200" };
        private List<string> rangeMax = new List<string>() { "1000", "3100", "5200" };
        private List<string> stepList = new List<string>() { "110", "110", "100" };

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            GC.Collect();
        }

        private void btnClearModels_Click(object sender, EventArgs e)
        {
            this.scientificVisual3DControl.ClearScientificModels();

            GC.Collect();
        }

        private void lblDebugInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bool depthTest = this.scientificVisual3DControl.OpenGL.IsEnabled(OpenGL.GL_DEPTH_TEST);
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("depth test: {0}", depthTest ? "enabled" : "disabled"));
                MessageBox.Show(builder.ToString());
            }
        }

        private void chkRenderContainerBox_CheckedChanged(object sender, EventArgs e)
        {
            this.scientificVisual3DControl.RenderBoundingBox = this.chkRenderContainerBox.Checked;
        }

        private void cmbViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cmbViewType.SelectedItem.ToString();
            ViewTypes viewType = (ViewTypes)Enum.Parse(typeof(ViewTypes), selected);
            this.scientificVisual3DControl.ViewType = viewType;
        }

        private void cmbCameraType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cmbCameraType.SelectedItem.ToString();
            CameraTypes cameraType = (CameraTypes)Enum.Parse(typeof(CameraTypes), selected);
            this.scientificVisual3DControl.CameraType = cameraType;
        }


        int startedCycle = 0;
        int stoppedCycle = 0;

        const int length = 10000000;
        UnmanagedArray<Vertex> vertexes = new UnmanagedArray<Vertex>(length);
        UnmanagedArray<ColorF> colors = new UnmanagedArray<ColorF>(length);

        //float[] vs = new float[length*3];
        //float[] cs = new float[length*4];
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.vertexes.Dispose();
            this.colors.Dispose();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //vs[length - 1] = 4.0f;
            //cs[length - 1] = 5.0f;

            startedCycle++;
            InitVertexes(this.scientificVisual3DControl.OpenGL, vertexes, colors);
            //InitVertexes(this.scientificVisual3DControl.OpenGL, vs, cs);
            stoppedCycle++;
            this.lblState.Text = string.Format("{0}/{1} times", startedCycle, stoppedCycle);
        }
        //private void InitVertexes(OpenGL gl, float  [] vertexes, float[] colorArray)
        //{
        //    uint ATTRIB_INDEX_POSITION = 0;
        //    uint ATTRIB_INDEX_COLOUR = 1;

        //    uint[] vao = new uint[1];
        //    gl.GenVertexArrays(vao.Length, vao);
        //    gl.BindVertexArray(vao[0]);

        //    uint[] vboVertex = new uint[1];
        //    gl.GenBuffers(vboVertex.Length, vboVertex);
        //    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboVertex[0]);
        //    gl.BufferData(OpenGL.GL_ARRAY_BUFFER, vertexes, OpenGL.GL_STATIC_DRAW);
        //    gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //    gl.EnableVertexAttribArray(ATTRIB_INDEX_POSITION);

        //    uint[] vboColor = new uint[1];
        //    gl.GenBuffers(vboColor.Length, vboColor);
        //    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboColor[0]);
        //    gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray, OpenGL.GL_DYNAMIC_DRAW);
        //    gl.VertexAttribPointer(ATTRIB_INDEX_COLOUR, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //    gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);

        //    gl.BindVertexArray(0);

        //    gl.InvalidateBufferData(vboVertex[0]);
        //    gl.InvalidateBufferData(vboColor[0]);
        //    gl.DeleteBuffers(1, vboVertex);
        //    gl.DeleteBuffers(1, vboColor);
        //    gl.DeleteVertexArrays(1, vao);
        //}
        private void InitVertexes(OpenGL gl, UnmanagedArray<Vertex> vertexes, UnmanagedArray<ColorF> colorArray)
        {
            uint ATTRIB_INDEX_POSITION = 0;
            uint ATTRIB_INDEX_COLOUR = 1;

            uint[] vao = new uint[1];
            gl.GenVertexArrays(vao.Length, vao);
            gl.BindVertexArray(vao[0]);

            uint[] vboVertex = new uint[1];
            gl.GenBuffers(vboVertex.Length, vboVertex);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboVertex[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, vertexes.ByteLength, vertexes.Header, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(ATTRIB_INDEX_POSITION);

            uint[] vboColor = new uint[1];
            gl.GenBuffers(vboColor.Length, vboColor);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboColor[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_DYNAMIC_DRAW);
            gl.VertexAttribPointer(ATTRIB_INDEX_COLOUR, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);

            gl.BindVertexArray(0);

            //gl.InvalidateBufferData(vboVertex[0]);
            //gl.InvalidateBufferData(vboColor[0]);
            gl.DeleteBuffers(1, vboVertex);
            gl.DeleteBuffers(1, vboColor);
            gl.DeleteVertexArrays(1, vao);
        }

    }
}
