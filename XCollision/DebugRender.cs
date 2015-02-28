using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XCollision
{
    public class DebugRender
    {
        GraphicsDevice GraphicsDevice;
        List<DebugLine> Lines;
        public DebugRender(GraphicsDevice gd) {
            this.GraphicsDevice = gd;
            this.Lines = new List<DebugLine>();
        }

        public void Clear()
        {
            this.Lines.Clear();
        }

        public void AddLine(Vector2 In, Vector2 Out, Color Color)
        {
            this.Lines.Add(new DebugLine(In, Out, Color));
        }

        public void Draw()
        {
            VertexPositionColor[] data = new VertexPositionColor[this.Lines.Count * 2];
            for (int i = 0; i < this.Lines.Count; i++ )
            {
                data[2 * i] = new VertexPositionColor(new Vector3(Lines[i].In.X, 0, Lines[i].In.Y),Lines[i].Color);
                data[(2 * i) + 1] = new VertexPositionColor(new Vector3(Lines[i].Out.X, 0, Lines[i].Out.Y), Lines[i].Color);
            }

            Matrix viewMatrix = Matrix.CreateLookAt(
                                                    new Vector3(0.0f, 0.0f, 1.0f),
                                                    Vector3.Zero,
                                                    Vector3.Up
                                                    );

            Matrix projectionMatrix = Matrix.CreateOrthographicOffCenter(
                                                                            0,
                                                                            (float)GraphicsDevice.Viewport.Width,
                                                                            (float)GraphicsDevice.Viewport.Height,
                                                                            0,
                                                                            1.0f, 1000.0f);
            BasicEffect e = new BasicEffect(GraphicsDevice);
            e.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.LineList,
                data,
                0,
                this.Lines.Count);


        }
    }

    public struct DebugLine
    {
        public DebugLine(Vector2 pIn, Vector2 pOut, Color pColor)
        {
            this.In = pIn;
            this.Out = pOut;
            this.Color = pColor;
        }
        public Vector2 In;
        public Vector2 Out;
        public Color Color;
    }
}
