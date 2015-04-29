using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Effects;

namespace SharpGL.SceneComponent
{
    public class OrthoColorIndicatorElementFactory
    {
        public static unsafe OrthoColorIndicatorElement Create(ColorTemplate colorTemplate)
        {
            var element = new OrthoColorIndicatorElement() { Name = "orthogonal color indicator element" };

            {
                var length = colorTemplate.Colors.Length;
                var rectModel = new GenericModel(length * 2, Enumerations.BeginMode.QuadStrip);
                var positions = rectModel.Positions;
                for (int i = 0; i < length; i++)
                {
                    positions[i * 2].X = colorTemplate.Width * i / (length - 1);
                    positions[i * 2].Y = 0;
                    positions[i * 2].Z = 0;
                    positions[i * 2 + 1].X = colorTemplate.Width * i / (length - 1);
                    positions[i * 2 + 1].Y = colorTemplate.Height;
                    positions[i * 2 + 1].Z = 0;
                }
                var colors = rectModel.Colors;
                //for (int i = 0; i < length*2; i++)
                //{
                //    colors[i].red = byte.MaxValue/ 2;
                //    colors[i].green = byte.MaxValue;
                //    colors[i].blue = byte.MaxValue;
                //}
                for (int i = 0; i < length; i++)
                {
                    var color = colorTemplate.Colors[i];
                    colors[i * 2].red = (byte)(color.R * byte.MaxValue / 2);
                    colors[i * 2].green = (byte)(color.G * byte.MaxValue / 2);
                    colors[i * 2].blue = (byte)(color.B * byte.MaxValue / 2);
                    //colors[i * 2].alpha = (byte)(color.A * byte.MaxValue / 2);
                    colors[i * 2 + 1].red = (byte)(color.R * byte.MaxValue / 2);
                    colors[i * 2 + 1].green = (byte)(color.G * byte.MaxValue / 2);
                    colors[i * 2 + 1].blue = (byte)(color.B * byte.MaxValue / 2);
                    //colors[i * 2 + 1].alpha = (byte)(color.A * byte.MaxValue / 2);
                }

                element.rectModel = rectModel;
            }

            {
                var length = 4;
                var horizontalLines = new GenericModel(length, Enumerations.BeginMode.Lines);
                var positions = horizontalLines.Positions;
                positions[0].X = 0; positions[0].Y = 0; positions[0].Z = 0;
                positions[1].X = colorTemplate.Width; positions[1].Y = 0; positions[1].Z = 0;
                positions[2].X = 0; positions[2].Y = colorTemplate.Height; positions[2].Z = 0;
                positions[3].X = colorTemplate.Width;
                positions[3].Y = colorTemplate.Height;
                positions[3].Z = 0;
                var colors = horizontalLines.Colors;
                for (int i = 0; i < length; i++)
                {
                    colors[i].red = byte.MaxValue / 2;
                    colors[i].green = byte.MaxValue / 2;
                    colors[i].blue = byte.MaxValue / 2;
                    //colors[i].alpha = byte.MaxValue;
                }

                element.horizontalLines = horizontalLines;
            }

            {
                var length = colorTemplate.Colors.Length;
                var verticalLines = new GenericModel(length * 2, Enumerations.BeginMode.Lines);
                var positions = verticalLines.Positions;
                for (int i = 0; i < length; i++)
                {
                    positions[i * 2].X = colorTemplate.Width * i / (length - 1);
                    positions[i * 2].Y = 0;
                    positions[i * 2].Z = 0;
                    positions[i * 2 + 1].X = colorTemplate.Width * i / (length - 1);
                    positions[i * 2 + 1].Y = colorTemplate.Height;
                    positions[i * 2 + 1].Z = 0;
                }
                var colors = verticalLines.Colors;
                for (int i = 0; i < length * 2; i++)
                {
                    colors[i].red = byte.MaxValue / 2;
                    colors[i].green = byte.MaxValue / 2;
                    colors[i].blue = byte.MaxValue / 2;
                    //colors[i].alpha = byte.MaxValue;
                }

                element.verticalLines = verticalLines;
            }

            {
                var scaleEffect = new OrthoColorIndicatorTransformEffect();
                scaleEffect.colorTemplate = colorTemplate;
                element.AddEffect(scaleEffect);
                element.scaleEffect = scaleEffect;
            }

            //  Create a set of scene attributes.
            var sceneAttributes = new OpenGLAttributesEffect()
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

            return element;
        }
    }
}
