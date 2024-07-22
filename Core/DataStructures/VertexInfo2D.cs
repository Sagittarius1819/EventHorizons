using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHorizons.Core.DataStructures
{
    public struct VertexInfo2D(Vector2 position, Color color, Vector2 texCoord) : IVertexType
    {
        private static VertexDeclaration _vertexDeclaration = new(
        [
            new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
            new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
        ]);

        public Vector2 position = position;
        public Color color = color;
        public Vector2 texCoord = texCoord;

        public readonly override string ToString() => $"[{position}, {color}, {texCoord}]";
        
        public readonly VertexDeclaration VertexDeclaration => _vertexDeclaration;
    }
}
