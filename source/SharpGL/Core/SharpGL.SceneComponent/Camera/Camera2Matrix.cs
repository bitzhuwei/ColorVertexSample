using GlmNet;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent.Camera
{
    public static class Camera2Matrix
    {

        /// <summary>
        /// 根据摄像机的类型获取其投影矩阵
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static mat4 GetProjectionMat4(this IScientificCamera camera)
        {
            mat4 result;

            switch (camera.CameraType)
            {
                case CameraTypes.Perspecitive:
                    result = ((IPerspectiveCamera)camera).GetProjectionMat4();
                    break;
                case CameraTypes.Ortho:
                    result = ((IOrthoCamera)camera).GetProjectionMat4();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }

        /// <summary>
        /// Extension method for <see cref="IPerspectiveCamera"/> to get projection matrix.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static mat4 GetProjectionMat4(this IPerspectiveCamera camera)
        {
            mat4 perspective = glm.perspective(
                (float)(camera.FieldOfView / 360.0 * Math.PI * 2),
                (float)camera.AspectRatio, (float)camera.Near, (float)camera.Far);
            return perspective;
        }

        /// <summary>
        /// Extension method for <see cref="IOrthoCamera"/> to get projection matrix.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static mat4 GetProjectionMat4(this IOrthoCamera camera)
        {
            mat4 ortho = glm.ortho((float)camera.Left, (float)camera.Right,
                (float)camera.Bottom, (float)camera.Top,
                (float)camera.Near, (float)camera.Far);
            return ortho;
        }

        /// <summary>
        /// Extension method for <see cref="IViewCamera"/> to get view matrix.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static mat4 GetViewMat4(this IViewCamera camera)
        {
            mat4 lookAt = glm.lookAt(camera.Position.ToVec3(), camera.Target.ToVec3(), camera.UpVector.ToVec3());
            return lookAt;
        }

        public static vec3 ToVec3(this Vertex vertex)
        {
            vec3 result = new vec3(vertex.X, vertex.Y, vertex.Z);

            return result;
        }
    }
}
