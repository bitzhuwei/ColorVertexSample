using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GlmNet;
using SharpGL;
using SharpGL.SceneGraph;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// The ArcBall camera supports arcball projection, making it ideal for use with a mouse.
    /// </summary>
    public class ArcBall2
    {
        protected bool isCameraSet = false;
        public bool mouseDownFlag;
        protected float _angle;
        protected float _length, _radiusRadius;
        protected float[] _lastRotation = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        protected Vertex _startPosition, _normalVector = new Vertex(0, 1, 0);
        protected int _width;
        protected int _height;
        protected Vertex _back;
        protected Vertex _up;
        protected Vertex _right;
        //protected mat4 currentRotation = mat4.identity();
        float _scale = 1.0f;
        SceneGraph.Cameras.LookAtCamera _camera;
             
        public void SetBounds(int width, int height)
        {
            this._width = width; this._height = height;
            _length = width > height ? width : height;
            var rx = (width / 2) / _length;
            var ry = (height / 2) / _length;
            _radiusRadius = (float)(rx * rx + ry * ry);
        }

        public void MouseDown(int x, int y)
        {
            this._startPosition = GetArcBallPosition(x, y);

            mouseDownFlag = true;
        }

        private Vertex GetArcBallPosition(int x, int y)
        {
            UpdateCameraAxis();
            var rx = (x - _width / 2) / _length;
            var ry = (_height / 2 - y) / _length;
            var zz = _radiusRadius - rx * rx - ry * ry;
            var rz = (zz > 0 ? Math.Sqrt(zz) : 0);
            /*                                 | rx |
             * result = [ _right _up _back ] * | ry |
             *                                 | rz |
             */
            var result = new Vertex(
                (float)(rx * _right.X + ry * _up.X + rz * _back.X),
                (float)(rx * _right.Y + ry * _up.Y + rz * _back.Y),
                (float)(rx * _right.Z + ry * _up.Z + rz * _back.Z)
                );
            return result;
        }

        private void UpdateCameraAxis()
        {
            var camera = this._camera;
            if (camera == null) { return; }

            _back = camera.Position - camera.Target;
            _back.Normalize();
            _right = camera.UpVector.VectorProduct(_back);
            _right.Normalize();
            _up = _back.VectorProduct(_right);
            _up.Normalize();
        }

        public void MouseMove(int x, int y)
        {
            if (mouseDownFlag)
            {
                var startPosition = this._startPosition;
                var endPosition = GetArcBallPosition(x, y);
                var cosAngle = startPosition.ScalarProduct(endPosition) / (startPosition.Magnitude() * endPosition.Magnitude());
                if (cosAngle > 1) { cosAngle = 1; }
                else if (cosAngle < -1) { cosAngle = -1; }
                var angle = 1 * (float)(Math.Acos(cosAngle) / Math.PI * 180);
                System.Threading.Interlocked.Exchange(ref _angle, angle);
                this._normalVector = startPosition.VectorProduct(endPosition);
                this._startPosition = endPosition;
            }
        }

        public void MouseUp(int x, int y)
        {
            mouseDownFlag = false;
        }

        //public mat4 GetTransformMat4()
        //{
        //    var rotation = GetRotation();
        //    var scale = glm.scale(mat4.identity(), new vec3(Scale));
        //    var translate = glm.translate(mat4.identity(), new vec3(Translate.X,
        //        Translate.Y, Translate.Z));
        //    //result = translate * rotation * scale;//rotate good
        //    //result = translate * scale * rotation;//rotate reversed
        //    //result = rotation * translate * scale;//rotate reversed
        //    //result = rotation * scale * translate;
        //    //result = scale * translate * rotation;
        //    var result = scale * rotation * translate;//rotate good
        //    return result;
        //}

        //public mat4 GetRotation()
        //{
        //    return currentRotation;
        //}

        //private void UpdateRotation()
        //{
        //    var angle = (float)(_angle * Math.PI / 180.0f);
        //    var rotation = glm.rotate(angle, new vec3(_normalVector.X, _normalVector.Y, _normalVector.Z));
        //    currentRotation = rotation * currentRotation;
        //}

        public void TransformMatrix(OpenGL gl)
        {
            if(!isCameraSet)
            { throw new Exception("Camera is not set by using SetCamera(..)"); }

            if (_angle != 0)
            {
                gl.PushMatrix();
                gl.LoadIdentity();
                gl.Rotate(2 * _angle, _normalVector.X, _normalVector.Y, _normalVector.Z);
                gl.MultMatrix(_lastRotation);
                gl.GetFloat(SharpGL.Enumerations.GetTarget.ModelviewMatix, _lastRotation);
                gl.PopMatrix();
                //UpdateRotation();
                System.Threading.Interlocked.Exchange(ref _angle, 0);
            }

            gl.Translate(Translate.X, Translate.Y, Translate.Z);
            gl.MultMatrix(_lastRotation);
            gl.Scale(Scale, Scale, Scale);
        }

        public void GoUp(float interval)
        {
            UpdateCameraAxis();
            this.Translate += this._up * interval;
        }
        public void GoDown(float interval)
        {
            UpdateCameraAxis();
            this.Translate -= this._up * interval;
        }
        public void GoLeft(float interval)
        {
            UpdateCameraAxis();
            this.Translate -= this._right * interval;
        }
        public void GoRight(float interval)
        {
            UpdateCameraAxis();
            this.Translate += this._right * interval;
        }

        public Vertex Translate { get; set; }

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public void GoFront(int interval)
        {
            UpdateCameraAxis();
            this.Translate -= this._back * interval;
        }

        public void GoBack(int interval)
        {
            UpdateCameraAxis();
            this.Translate += this._back * interval;
        }

        public void ResetRotation()
        {
            this._lastRotation = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            //this.currentRotation = mat4.identity();
        }

        public float[] GetCurrentRotation()
        {
            return this._lastRotation.ToArray();
        }

        public SceneGraph.Cameras.LookAtCamera Camera
        {
            get { return _camera; }
            set
            {
                _camera = value;
                isCameraSet = value != null;
            }
        }
    }
}
