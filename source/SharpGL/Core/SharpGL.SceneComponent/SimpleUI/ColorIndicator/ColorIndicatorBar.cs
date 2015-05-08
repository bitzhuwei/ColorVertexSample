﻿using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Render rectangles and lines of <see cref="SimpleUIColorIndicator"/>.
    /// </summary>
    internal class ColorIndicatorBar : SceneElement, IRenderable
    {
        private long lastModified = 0;

        private ColorIndicatorData data;

        public ColorIndicatorData Data
        {
            get { return data; }
            set
            {
                data = value;
                //TryUpdate(value);
                //if (value != null)
                //{ lastModified = value.LastModified; }
                //else
                //{ lastModified = DateTime.Now.Ticks; }
            }
        }

        private unsafe void TryUpdate(ColorIndicatorData data)
        {
            if (data == null)
            {
                this.rectModel = null;
                this.verticalLines = null;
                this.horizontalLines = null;
                return;
            }

            if (data.LastModified == this.lastModified) { return; }

            // initialize rectangles with gradient color.
            GenerateRectangles(data);

            // initialize two horizontal white lines.
            GenerateHorizontalLines();

            // initialize vertical lines.
            GenerateVerticalLines(data);

            this.lastModified = data.LastModified;
        }

        unsafe private void GenerateVerticalLines(ColorIndicatorData data)
        {
            //int blockCount = data.GetBlockCount();
            int blockCount = data.BlockCount;
            int length = blockCount + 1;
            PointerScientificModel verticalLines = new PointerScientificModel(length * 2, Enumerations.BeginMode.Lines);
            Vertex* positions = verticalLines.Positions;
            for (int i = 0; i < length; i++)
            {
                positions[i * 2].X = barWidth * i / (length - 1);
                positions[i * 2].Y = -9;
                positions[i * 2].Z = 0;
                positions[i * 2 + 1].X = barWidth * i / (length - 1);
                positions[i * 2 + 1].Y = barHeight;
                positions[i * 2 + 1].Z = 0;
            }
            // move the vertical lines' center to (0, 0, 0)
            for (int i = 0; i < length * 2; i++)
            {
                positions[i].X -= barWidth / 2;
                positions[i].Y -= barHeight / 2;
            }

            ByteColor* colors = verticalLines.Colors;
            for (int i = 0; i < length * 2; i++)
            {
                colors[i].red = byte.MaxValue / 2;
                colors[i].green = byte.MaxValue / 2;
                colors[i].blue = byte.MaxValue / 2;
            }

            this.verticalLines = verticalLines;
        }

        unsafe private void GenerateHorizontalLines()
        {
            int length = 4;
            PointerScientificModel horizontalLines = new PointerScientificModel(length, Enumerations.BeginMode.Lines);
            Vertex* positions = horizontalLines.Positions;
            positions[0].X = 0; positions[0].Y = 0; positions[0].Z = 0;
            positions[1].X = barWidth; positions[1].Y = 0; positions[1].Z = 0;
            positions[2].X = 0; positions[2].Y = barHeight; positions[2].Z = 0;
            positions[3].X = barWidth;
            positions[3].Y = barHeight;
            positions[3].Z = 0;
            // move the horizontal white lines' center to (0, 0, 0)
            for (int i = 0; i < length; i++)
            {
                positions[i].X -= barWidth / 2;
                positions[i].Y -= barHeight / 2;
            }
            ByteColor* colors = horizontalLines.Colors;
            for (int i = 0; i < length; i++)
            {
                colors[i].red = byte.MaxValue / 2;
                colors[i].green = byte.MaxValue / 2;
                colors[i].blue = byte.MaxValue / 2;
            }

            this.horizontalLines = horizontalLines;
        }

        unsafe private void GenerateRectangles(ColorIndicatorData data)
        {
            int length = data.ColorPalette.Colors.Length;
            PointerScientificModel rectModel = new PointerScientificModel(length * 2, Enumerations.BeginMode.QuadStrip);
            Vertex* positions = rectModel.Positions;
            for (int i = 0; i < length; i++)
            {
                positions[i * 2].X = barWidth * data.ColorPalette.Coords[i];// i / (length - 1);
                positions[i * 2].Y = 0;
                positions[i * 2].Z = 0;
                positions[i * 2 + 1].X = barWidth * data.ColorPalette.Coords[i];// i / (length - 1);
                positions[i * 2 + 1].Y = barHeight;
                positions[i * 2 + 1].Z = 0;
            }
            // move the rectangles' center to (0, 0, 0)
            for (int i = 0; i < length * 2; i++)
            {
                positions[i].X -= barWidth / 2;
                positions[i].Y -= barHeight / 2;
            }

            ByteColor* colors = rectModel.Colors;
            for (int i = 0; i < length; i++)
            {
                GLColor color = data.ColorPalette.Colors[i];
                colors[i * 2].red = (byte)(color.R * byte.MaxValue / 2);
                colors[i * 2].green = (byte)(color.G * byte.MaxValue / 2);
                colors[i * 2].blue = (byte)(color.B * byte.MaxValue / 2);
                colors[i * 2 + 1].red = (byte)(color.R * byte.MaxValue / 2);
                colors[i * 2 + 1].green = (byte)(color.G * byte.MaxValue / 2);
                colors[i * 2 + 1].blue = (byte)(color.B * byte.MaxValue / 2);
            }

            this.rectModel = rectModel;
        }

        public const int barWidth = 100;
        public const int barHeight = 30;

        private PointerScientificModel rectModel;

        private PointerScientificModel verticalLines;

        private PointerScientificModel horizontalLines;

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            TryUpdate(this.data);

            PointerScientificModel rectModel = this.rectModel;
            PointerScientificModel verticalLines = this.verticalLines;
            PointerScientificModel horizontalLines = this.horizontalLines;

            if (rectModel != null)
            { rectModel.Render(gl, renderMode); }

            if (verticalLines != null)
            { verticalLines.Render(gl, renderMode); }

            if (horizontalLines != null)
            { horizontalLines.Render(gl, renderMode); }
        }

    }
}
