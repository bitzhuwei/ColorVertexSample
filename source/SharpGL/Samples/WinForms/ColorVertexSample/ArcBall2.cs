using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GlmNet;
using SharpGL;
using SharpGL.SceneGraph;

namespace ColorVertexSample
{
    class ArcBall2
    {
        private bool isCameraSet = false;
        private bool mouseDownFlag;
        private float _angle;
        private float _length, _radiusRadius;
        private float[] _lastRotation = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        private Vertex _startPosition, _endPosition, _normalVector = new Vertex(0, 1, 0);
        int _width;
        int _height;
        Vertex _back;
        Vertex _up;
        Vertex _right;
        private mat4 originalRotation = mat4.identity();

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
            var rx = (x - _width / 2) / _length;
            var ry = (_height / 2 - y) / _length;
            var zz = _radiusRadius - rx * rx - ry * ry;
            var rz = (zz > 0 ? Math.Sqrt(zz) : 0);
            var result = new Vertex(
                (float)(rx * _right.X + ry * _up.X + rz * _back.X),
                (float)(rx * _right.Y + ry * _up.Y + rz * _back.Y),
                (float)(rx * _right.Z + ry * _up.Z + rz * _back.Z)
                );
            return result;
        }


        public void MouseMove(int x, int y)
        {
            if (mouseDownFlag)
            {
                this._endPosition = GetArcBallPosition(x, y);
                var cosAngle = _startPosition.ScalarProduct(_endPosition) / (_startPosition.Magnitude() * _endPosition.Magnitude());
                if (cosAngle > 1) { cosAngle = 1; }
                else if (cosAngle < -1) { cosAngle = -1; }
                var angle = 10 * (float)(Math.Acos(cosAngle) / Math.PI * 180);
                System.Threading.Interlocked.Exchange(ref _angle, angle);
                _normalVector = _startPosition.VectorProduct(_endPosition);
                _startPosition = _endPosition;
            }
        }

        public void MouseUp(int x, int y)
        {
            mouseDownFlag = false;
        }

        public mat4 GetTransformMat4()
        {
            if (_angle != 0)
            {
                var angle = (float)(_angle * Math.PI / 180.0f);
                var rotation = glm.rotate(angle, new vec3(_normalVector.X, _normalVector.Y, _normalVector.Z));
                rotation = rotation * originalRotation;
                originalRotation = rotation;
                System.Threading.Interlocked.Exchange(ref _angle, 0);
            }
            var scale = glm.scale(mat4.identity(), new vec3(Scale));
            var translate = glm.translate(mat4.identity(), new vec3(TranslateX,
                TranslateY, TranslateZ));
            //result = translate * originalRotation * scale;//rotate good
            //result = translate * scale * originalRotation;//rotate reversed
            //result = originalRotation * translate * scale;//rotate reversed
            //result = originalRotation * scale * translate;
            //result = scale * translate * originalRotation;
            var result = scale * originalRotation * translate;//rotate good
            return result;
        }
        public mat4 GetRotation()
        {
            var angle = (float)(_angle * Math.PI / 180.0f);
            var rotation = glm.rotate(angle, new vec3(_normalVector.X, _normalVector.Y, _normalVector.Z));
            rotation = rotation * originalRotation;
            originalRotation = rotation;
            System.Threading.Interlocked.Exchange(ref _angle, 0);
            return rotation;
        }
        public void TransformMatrix(OpenGL gl)
        {
            if(!isCameraSet)
            { throw new Exception("Camera is not set by using SetCamera(..)"); }

            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Rotate(2 * _angle, _normalVector.X, _normalVector.Y, _normalVector.Z);
            System.Threading.Interlocked.Exchange(ref _angle, 0);
            gl.MultMatrix(_lastRotation);
            gl.GetFloat(SharpGL.Enumerations.GetTarget.ModelviewMatix, _lastRotation);
            gl.PopMatrix();
            gl.Translate(_translateX, _translateY, _translateZ);
            gl.MultMatrix(_lastRotation);
            gl.Scale(Scale, Scale, Scale);
        }

        /// <summary>
        /// Default camera is at positive Z axis to look at negtive Z axis with up vector to positive Y axis.
        /// </summary>
        /// <param name="eyex"></param>
        /// <param name="eyey"></param>
        /// <param name="eyez"></param>
        /// <param name="centerx"></param>
        /// <param name="centery"></param>
        /// <param name="centerz"></param>
        /// <param name="upx"></param>
        /// <param name="upy"></param>
        /// <param name="upz"></param>
        public void SetCamera(float eyex, float eyey, float eyez,
            float centerx, float centery, float centerz,
            float upx, float upy, float upz)
        {
            _back = new Vertex(eyex - centerx, eyey - centery, eyez - centerz);
            _back.Normalize();
            _up = new Vertex(upx, upy, upz);
            _right = _up.VectorProduct(_back);
            _right.Normalize();
            _up = _back.VectorProduct(_right);
            _up.Normalize();
            isCameraSet = true;
        }

        /// <summary>
        /// Default camera is at positive Z axis to look at negtive Z axis with up vector to positive Y axis. 
        /// </summary>
        /// <param name="lookAtCamera"></param>
        public void SetCamera(SharpGL.SceneGraph.Cameras.LookAtCamera lookAtCamera)
        {
            SetCamera(lookAtCamera.Position.X, lookAtCamera.Position.Y, lookAtCamera.Position.Z,
                lookAtCamera.Target.X, lookAtCamera.Target.Y, lookAtCamera.Target.Z,
                lookAtCamera.UpVector.X, lookAtCamera.UpVector.Y, lookAtCamera.UpVector.Z);
        }

        internal void SetCamera(Vertex position, Vertex target, Vertex up)
        {
            SetCamera(position.X, position.Y, position.Z,
                target.X, target.Y, target.Z, up.X, up.Y, up.Z);
        }

        public void GoUp(float interval)
        {
            this._translateX += this._up.X * interval;
            this._translateY += this._up.Y * interval;
            this._translateZ += this._up.Z * interval;
        }
        public void GoDown(float interval)
        {
            this._translateX -= this._up.X * interval;
            this._translateY -= this._up.Y * interval;
            this._translateZ -= this._up.Z * interval;
        }
        public void GoLeft(float interval)
        {
            this._translateX -= this._right.X * interval;
            this._translateY -= this._right.Y * interval;
            this._translateZ -= this._right.Z * interval;
        }
        public void GoRight(float interval)
        {
            this._translateX += this._right.X * interval;
            this._translateY += this._right.Y * interval;
            this._translateZ += this._right.Z * interval;
        }

        float _translateZ;

        public float TranslateZ
        {
            get { return _translateZ; }
            set { _translateZ = value; }
        }
        float _translateY;

        public float TranslateY
        {
            get { return _translateY; }
            set { _translateY = value; }
        }
        float _translateX;

        public float TranslateX
        {
            get { return _translateX; }
            set { _translateX = value; }
        }

        float _scale = 1.0f;
     
        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }


        public void SetTranslate(double x, double y, double z)
        {
            this._translateX = (float)x;
            this._translateY = (float)y;
            this._translateZ = (float)z;
        }

        public void GoFront(int interval)
        {
            this._translateX -= this._back.X * interval;
            this._translateY -= this._back.Y * interval;
            this._translateZ -= this._back.Z * interval;
        }
        public void GoBack(int interval)
        {
            this._translateX += this._back.X * interval;
            this._translateY += this._back.Y * interval;
            this._translateZ += this._back.Z * interval;
        }

        public void ResetRotation()
        {
            this._lastRotation = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            this.originalRotation = mat4.identity();
        }


    }
}
