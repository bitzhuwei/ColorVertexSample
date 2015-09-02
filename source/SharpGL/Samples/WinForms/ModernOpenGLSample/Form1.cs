using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModernOpenGLSample
{
    public partial class Form1 : Form
    {
        private uint[] m_vertexVBOID;
        private float[] m_vertices;
        private uint[] m_indexVBOID;
        private ushort[] m_indices;
        private int m_numIndices;
        public Form1()
        {
            InitializeComponent();
        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            m_vertices = new float[9] { 0.0f, 1.0f, 0.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f };
            m_indices = new ushort[3] { 0, 1, 2 };
            m_numIndices = m_indices.Length;

            var gl = openGLControl1.OpenGL;
            m_vertexVBOID = new uint[1];
            gl.GenBuffers(1, m_vertexVBOID);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, m_vertexVBOID[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, m_vertices, OpenGL.GL_STATIC_DRAW);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            m_indexVBOID = new uint[1];
            gl.GenBuffers(1, m_indexVBOID);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, m_indexVBOID[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, m_indices, OpenGL.GL_STATIC_DRAW);
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            var gl = openGLControl1.OpenGL;
            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.5f);

            //  Clear the scene.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            //gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, m_vertexVBOID[0]);
            //gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, m_indexVBOID[0]);
            //gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            //gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, m_vertexVBOID[0]);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, m_indexVBOID[0]);
            gl.DrawElements(OpenGL.GL_TRIANGLES, m_numIndices, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);

            //DrawPyramid(gl);

        }
        private static void DrawPyramid(OpenGL gl)
        {
            //  Draw a coloured pyramid.
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.End();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl1.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            //gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);
            gl.LookAt(-2, 2, -2, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
    }
}
