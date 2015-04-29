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
            var element = new OrthoAxisElement() { Name = "orthogonal element" };

            //  Create a set of scene attributes.
            var sceneAttributes = new OpenGLAttributesEffect()
            {
                Name = "Scene Attributes"
            };

            //  Specify the scene attributes.
            sceneAttributes.EnableAttributes.EnableDepthTest = true;
            sceneAttributes.EnableAttributes.EnableNormalize = true;
            sceneAttributes.EnableAttributes.EnableLighting = false;
            sceneAttributes.EnableAttributes.EnableTexture2D = true;
            sceneAttributes.EnableAttributes.EnableBlend = true;
            sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
            sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
            sceneAttributes.LightingAttributes.TwoSided = true;
            sceneAttributes.TransformAttributes.MatrixMode = MatrixMode.Projection;
            element.AddEffect(sceneAttributes);

            var orthoAxisArcBallEffect = new OrthoArcBallEffect();
            //orthoAxisArcBallEffect.arcBall.Translate = new SharpGL.SceneGraph.Vertex(000, 100, 0);
            orthoAxisArcBallEffect.arcBall.Scale = 100;
            element.AddEffect(orthoAxisArcBallEffect);

            var axies = new Axies();
            element.AddChild(axies);

            var grid = new Grid();
            element.AddChild(grid);
            var transform = new LinearTransformationEffect();
            transform.LinearTransformation.ScaleX = 0.1f;
            transform.LinearTransformation.ScaleY = 0.1f;
            transform.LinearTransformation.ScaleZ = 0.1f;
            grid.AddEffect(transform);

            element.orthoArcBallEffect = orthoAxisArcBallEffect;

            return element;
        }
    }
}
