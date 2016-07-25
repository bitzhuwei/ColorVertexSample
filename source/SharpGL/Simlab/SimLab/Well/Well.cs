using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlab.Well
{
    /// <summary>
    /// 蛇形管道（井）+文字显示
    /// </summary>
    public class Well : SceneElement, IRenderable
    {
        private WellPipe wellPipeElement;
        private PointSpriteStringElement textElement;

        private string wellName;
        private List<Vertex> wellInitPath;
        private float wellInitRadius;
        private float wellPathInitMaxZ;
        private float wellPathInitMinZ;
        private float wellPathMaxDisplayZ;
        private int fontSize;
        private int maxRowWidth;
        private GLColor wellPathColor;
        private GLColor textColor;

        private float radiusScale = 1.0f;

        //井口名称显示同进口的距离
        private float wellNameAnnotationSpace = 0.0f;

        /// <summary>
        /// model transform
        /// </summary>
        private mat4 transform = mat4.identity();




        /// <summary>
        /// 蛇形管道（井）+文字显示
        /// </summary>
        /// <param name="srcWellPath"></param>
        /// <param name="pipeRadius"></param>
        /// <param name="pipeColor"></param>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="camera"></param>
        public Well(IScientificCamera camera, List<Vertex> srcWellPath, float pipeRadius, GLColor pipeColor, String name,
            GLColor textColor = null, int fontSize = 32, int maxRowWidth = 256)
        {
            if (srcWellPath == null || srcWellPath.Count < 2)
                throw new ArgumentException("well path error");

            this.wellInitPath = srcWellPath;
            this.wellInitRadius = pipeRadius;
            this.wellPathInitMaxZ = wellInitPath[0].Z;
            this.wellPathInitMinZ = wellInitPath[this.wellInitPath.Count - 1].Z;

            this.wellPathMaxDisplayZ = this.wellPathInitMaxZ;
            this.wellName = name;
            this.wellPathColor = pipeColor;
            this.textColor = textColor;
            this.fontSize = fontSize;
            this.maxRowWidth = maxRowWidth;

        }

        private void ClearVisualElements()
        {
            if (this.wellPipeElement != null)
            {
                //dispose后移除，还是移除后Dispose
                if (this.wellPipeElement is IDisposable)
                {
                    this.wellPipeElement.Dispose();
                }
                this.RemoveChild(this.wellPipeElement);
                this.wellPipeElement = null;
            }
            if (this.textElement != null)
            {
                if (textElement is IDisposable)
                {
                    this.textElement.Dispose();
                }
                this.RemoveChild(this.textElement);
                this.textElement = null;
            }
        }

        /// <summary>
        /// model  transform
        /// </summary>
        public mat4 Transform
        {

            get { return this.transform; }
            set { this.transform = value; }

        }

        /// <summary>
        /// support recreate the visual elements
        /// </summary>
        /// <param name="camera"></param>
        public void CreateVisualElements(IScientificCamera camera)
        {
            ClearVisualElements();

            Vertex srcTop = this.wellInitPath[0];
            srcTop.Z = this.wellPathMaxDisplayZ;
            Vertex destTop = this.Transform * srcTop;

            //create  well path dispay max z
            bool isValid = false;
            List<Vertex> destPath = new List<Vertex>();
            for (int i = 0; i < this.wellInitPath.Count - 1; i++)
            {
                Vertex p = this.Transform * this.wellInitPath[i];
                Vertex p2 = this.Transform * this.wellInitPath[i + 1];
                if (!isValid)
                {
                    if (p.Z >= destTop.Z && p2.Z <= destTop.Z)
                    {
                        isValid = true;
                        p.Z = destTop.Z;
                    }else if(p.Z <=destTop.Z){
                        isValid = true;
                        p.Z = destTop.Z;
                    }
                }
                if(isValid){
                  destPath.Add(p);
                  if((i+1)==(this.wellInitPath.Count-1)){
                     destPath.Add(p2);
                  }
                }
            }

        
            if(destPath.Count <2)
              return ;

            float wellRadius = this.wellInitRadius * this.radiusScale;
            Vertex wellNameAnnotationLocation = new Vertex(destPath[0].X, destPath[0].Y, destPath[0].Z + this.wellNameAnnotationSpace);
            this.wellPipeElement = new WellPipe(destPath, wellRadius, this.wellPathColor, camera);
            this.textElement = new PointSpriteStringElement(camera, this.wellName, wellNameAnnotationLocation, this.textColor, this.fontSize, this.maxRowWidth);
            this.wellPipeElement.ZAxisScale = this.ZAxisScale;
            this.textElement.ZAxisScale = this.ZAxisScale;
            this.AddChild(this.wellPipeElement);
            this.AddChild(this.textElement);

        }

        /// <summary>
        /// 修改半径通过,修改后需要重新
        /// </summary>
        public float WellInitRadius
        {
            get { return this.wellInitRadius; }
        }


        /// <summary>
        /// 有效值范围(0,无穷大)
        /// </summary>
        public float WellRadiusScale
        {
            get { return this.radiusScale; }
            set { this.radiusScale = value; }
        }

        public float WellNameAnnotationSpace
        {
            get { return this.wellNameAnnotationSpace; }
            set { this.wellNameAnnotationSpace = value; }
        }

        public float WellPathInitMaxZ
        {
            get { return this.wellPathInitMaxZ; }
        }

        public float WellPathInitMinZ
        {
            get { return this.wellPathInitMinZ; }
        }

        /// <summary>
        /// 值介于(WellPathInitMinZ,WellPathInitMaxZ)
        /// </summary>
        public float WellPathMaxDisplayZ
        {
            get { return this.wellPathMaxDisplayZ; }
            set { this.wellPathMaxDisplayZ = value; }
        }




        public override float ZAxisScale
        {
            get
            {
                return base.ZAxisScale;
            }
            set
            {
                base.ZAxisScale = value;
                if (this.wellPipeElement != null)
                    this.wellPipeElement.ZAxisScale = value;
                if (this.textElement != null)
                    this.textElement.ZAxisScale = value;
            }
        }


        public String WellName
        {
            get { return this.wellName; }
        }

        public void Initialize(OpenGL gl)
        {
            if(this.wellPipeElement != null)
              this.wellPipeElement.Initialize(gl);
            if(this.textElement!=null)
              this.textElement.Initialize(gl);
        }

        public void Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            if (this.wellPipeElement != null)
                this.wellPipeElement.Render(gl, renderMode);
            if (this.textElement != null)
                this.textElement.Render(gl, renderMode);
        }
    }
}
