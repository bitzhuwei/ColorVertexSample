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

            var bar = CreateBar(colorTemplate);

            element.AddChild(bar);
            element.bar = bar;

            var number = CreateNumber(colorTemplate);
            element.AddChild(number);
            element.number = number;

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

        private static OrthoColorIndicatorNumber CreateNumber(ColorTemplate colorTemplate)
        {
            var number = new OrthoColorIndicatorNumber() { Name = "color indicator's number" };

            number.colorTemplate = colorTemplate;

            return number;
        }

        private static unsafe OrthoColorIndicatorBar CreateBar(ColorTemplate colorTemplate)
        {
            var bar = new OrthoColorIndicatorBar() { Name = "color indicator's bar" };
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
                for (int i = 0; i < length; i++)
                {
                    var color = colorTemplate.Colors[i];
                    colors[i * 2].red = (byte)(color.R * byte.MaxValue / 2);
                    colors[i * 2].green = (byte)(color.G * byte.MaxValue / 2);
                    colors[i * 2].blue = (byte)(color.B * byte.MaxValue / 2);
                    colors[i * 2 + 1].red = (byte)(color.R * byte.MaxValue / 2);
                    colors[i * 2 + 1].green = (byte)(color.G * byte.MaxValue / 2);
                    colors[i * 2 + 1].blue = (byte)(color.B * byte.MaxValue / 2);
                }

                bar.rectModel = rectModel;
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
                }

                bar.horizontalLines = horizontalLines;
            }

            {
                var length = colorTemplate.Colors.Length;
                var verticalLines = new GenericModel(length * 2, Enumerations.BeginMode.Lines);
                var positions = verticalLines.Positions;
                for (int i = 0; i < length; i++)
                {
                    positions[i * 2].X = colorTemplate.Width * i / (length - 1);
                    positions[i * 2].Y = -9;
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
                }

                bar.verticalLines = verticalLines;
            }

            {
                var scaleEffect = new OrthoColorIndicatorBarEffect();
                scaleEffect.colorTemplate = colorTemplate;
                bar.AddEffect(scaleEffect);
                bar.scaleEffect = scaleEffect;
            }

            return bar;
        }
    }
}
