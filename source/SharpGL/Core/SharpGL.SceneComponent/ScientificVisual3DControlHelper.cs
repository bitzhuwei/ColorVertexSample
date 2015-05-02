using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Initialize <see cref="ScientificVisual3DControl"/>'s UIScene.
    /// </summary>
    public class ScientificVisual3DControlHelper
    {
        internal static void InitializeUIScene(ScientificVisual3DControl scientificVisual3DControl)
        {
            if (scientificVisual3DControl == null) { return; }

            MyScene UIScene = scientificVisual3DControl.UIScene;
            UIScene.SceneContainer.Name = "UI Scene's container";
            UIScene.SceneContainer.Children.Clear();
            UIScene.SceneContainer.Effects.Clear();

            Initialize2DUI(scientificVisual3DControl);

            InitializeAttributes(UIScene.SceneContainer);

            LookAtCamera camera = scientificVisual3DControl.Scene.CurrentCamera as LookAtCamera;
            if (camera != null)
            {
                UIScene.CurrentCamera = camera;
            }

            UIScene.RenderBoundingVolumes = false;
        }

        private static OpenGLAttributesEffect InitializeAttributes(SceneElement parent)
        {
            //  Create a set of scene attributes.
            OpenGLAttributesEffect sceneAttributes = new OpenGLAttributesEffect()
            {
                Name = "UI Scene Attributes"
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
            sceneAttributes.LightingAttributes.AmbientLight = new GLColor(1, 1, 1, 1);
            parent.AddEffect(sceneAttributes);

            return sceneAttributes;
        }

        private static void Initialize2DUI(ScientificVisual3DControl scientificVisual3DControl)
        {
            SceneElement parent = scientificVisual3DControl.UIScene.SceneContainer;
            OpenGLUIAxis uiAxis = new OpenGLUIAxis(
                AnchorStyles.Left | AnchorStyles.Bottom,
                new Padding(10, 0, 0, 20), new Size(40, 40)) { Name = "UI: Axis", };
            parent.AddChild(uiAxis);
            //scientificVisual3DControl.RotationObjects.Add(uiAxis);
            scientificVisual3DControl.uiAxis = uiAxis;

            OpenGLUIColorIndicator uiColorIndicator = new OpenGLUIColorIndicator(
                AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                new Padding(10 + 40 + 10, 0, 40, 40), new Size(100, 15)) { Name = "UI: Color Indicator", };
            ColorIndicatorData rainbow = ColorIndicatorDataFactory.CreateRainbow();
            uiColorIndicator.Data = rainbow;
            parent.AddChild(uiColorIndicator);
            scientificVisual3DControl.uiColorIndicator = uiColorIndicator;
        }
    }
}
