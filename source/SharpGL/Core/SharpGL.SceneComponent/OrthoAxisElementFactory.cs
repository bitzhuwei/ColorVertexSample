using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Primitives;

namespace SharpGL.SceneComponent
{
    public class OrthoAxisElementFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="camera">if null, please set camera for result's orthoArcBallEffect.Camera property later.</param>
        /// <returns></returns>
        public static OrthoAxisElement Create(LookAtCamera camera = null)
        {
            OrthoAxisElement element = new OrthoAxisElement() { Name = "orthogonal axis element" };

            //  Create a set of scene attributes.
            OpenGLAttributesEffect sceneAttributes = new OpenGLAttributesEffect()
            {
                Name = "Scene Attributes"
            };

            //  Specify the scene attributes.
            sceneAttributes.EnableAttributes.EnableDepthTest = true;
            sceneAttributes.EnableAttributes.EnableNormalize = true;
            sceneAttributes.EnableAttributes.EnableLighting = false;
            //sceneAttributes.EnableAttributes.EnableTexture2D = true;
            //sceneAttributes.EnableAttributes.EnableBlend = true;
            //sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
            //sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
            //sceneAttributes.LightingAttributes.TwoSided = true;
            //sceneAttributes.TransformAttributes.MatrixMode = MatrixMode.Projection;
            element.AddEffect(sceneAttributes);

            OrthoArcBallEffect orthoAxisArcBallEffect = new OrthoArcBallEffect(camera);
            element.AddEffect(orthoAxisArcBallEffect);

            Axies axies = new Axies();
            element.AddChild(axies);
            LinearTransformationEffect transform = new LinearTransformationEffect();
            transform.LinearTransformation.ScaleX = 10;
            transform.LinearTransformation.ScaleY = 10;
            transform.LinearTransformation.ScaleZ = 10;
            axies.AddEffect(transform);

            //Grid grid = new Grid();
            //element.AddChild(grid);
            //LinearTransformationEffect transform = new LinearTransformationEffect();
            //transform.LinearTransformation.ScaleX = 0.1f;
            //transform.LinearTransformation.ScaleY = 0.1f;
            //transform.LinearTransformation.ScaleZ = 0.1f;
            //grid.AddEffect(transform);

            element.orthoArcBallEffect = orthoAxisArcBallEffect;

            return element;
        }
    }
}
