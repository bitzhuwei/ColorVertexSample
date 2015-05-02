using ColorVertexSample.Model;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorVertexSample.Visual
{
    class PointModelElementFactory
    {
        internal static PointModelElement Create(int nx, int ny, int nz, float radius, float minValue, float maxValue)
        {
            PointModel model = PointModelFactory.Create(nx, ny, nz, radius, minValue, maxValue);

            PointModelElement element = new PointModelElement(model);

            //parent.AddChild(element);

            Axies axies = new Axies();
            LinearTransformationEffect effect = new LinearTransformationEffect();
            float length = maxValue - minValue;
            effect.LinearTransformation.ScaleX = length;
            effect.LinearTransformation.ScaleY = length;
            effect.LinearTransformation.ScaleZ = length;
            axies.AddEffect(effect);
            element.AddChild(axies);

            ArcBallEffect2 modelArcBallEffect = new ArcBallEffect2();
            modelArcBallEffect.ArcBall.Translate = element.Model.translateVector;
            element.AddEffect(modelArcBallEffect);

            element.modelArcBallEffect = modelArcBallEffect;

            return element;
        }
    }
}
