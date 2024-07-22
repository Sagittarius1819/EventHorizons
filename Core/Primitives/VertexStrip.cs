﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using EventHorizons.Core.DataStructures;

namespace EventHorizons.Core.Primitives
{
    public class VertexStrip
    {
        public delegate Color StripColorFunction(float progressOnStrip);

        public delegate float StripHalfWidthFunction(float progressOnStrip);

        private VertexInfo2D[] _vertices = new VertexInfo2D[1];

        private int _vertexAmountCurrentlyMaintained;

        private short[] _indices = new short[1];

        private int _indicesAmountCurrentlyMaintained;

        private readonly List<Vector2> _temporaryPositionsCache = [];

        private readonly List<float> _temporaryRotationsCache = [];

        public void PrepareStrip(Vector2[] positions, float[] rotations, StripColorFunction colorFunction, StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), int? expectedVertexPairsAmount = null, bool includeBacksides = false)
        {
            int num = positions.Length;
            int num2 = (_vertexAmountCurrentlyMaintained = num * 2);

            if (_vertices.Length < num2)
            {
                Array.Resize(ref _vertices, num2);
            }

            int num3 = num;
            if (expectedVertexPairsAmount.HasValue)
            {
                num3 = expectedVertexPairsAmount.Value;
            }

            for (int i = 0; i < num; i++)
            {
                if (positions[i] == Vector2.Zero)
                {
                    num = i - 1;
                    _vertexAmountCurrentlyMaintained = num * 2;
                    break;
                }

                Vector2 pos = positions[i] + offsetForAllPositions;
                float rot = WrapAngle(rotations[i]);
                int indexOnVertexArray = i * 2;
                float progressOnStrip = (float)i / (float)(num3 - 1);
                AddVertex(colorFunction, widthFunction, pos, rot, indexOnVertexArray, progressOnStrip);
            }

            PrepareIndices(num, includeBacksides);
        }

        public void PrepareStripWithProceduralPadding(Vector2[] positions, float[] rotations, StripColorFunction colorFunction, StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), bool includeBacksides = false, bool tryStoppingOddBug = true)
        {
            int num = positions.Length;
            _temporaryPositionsCache.Clear();
            _temporaryRotationsCache.Clear();

            for (int i = 0; i < num && !(positions[i] == Vector2.Zero); i++)
            {
                Vector2 vector = positions[i];
                float num2 = WrapAngle(rotations[i]);
                _temporaryPositionsCache.Add(vector);
                _temporaryRotationsCache.Add(num2);

                if (i + 1 >= num || !(positions[i + 1] != Vector2.Zero))
                {
                    continue;
                }

                Vector2 vector2 = positions[i + 1];
                float num3 = WrapAngle(rotations[i + 1]);
                int num4 = (int)(Math.Abs(WrapAngle(num3 - num2)) / ((float)Math.PI / 12f));

                if (num4 != 0)
                {
                    float num5 = vector.Distance(vector2);
                    Vector2 value = vector + num2.ToRotationVector2() * num5;
                    Vector2 value2 = vector2 + num3.ToRotationVector2() * (0f - num5);
                    int num6 = num4 + 2;
                    float num7 = 1f / (float)num6;
                    Vector2 target = vector;

                    for (float num8 = num7; num8 < 1f; num8 += num7)
                    {
                        Vector2 vector3 = Vector2.CatmullRom(value, vector, vector2, value2, num8);
                        float item = WrapAngle(vector3.DirectionTo(target).ToRotation());
                        _temporaryPositionsCache.Add(vector3);
                        _temporaryRotationsCache.Add(item);
                        target = vector3;
                    }
                }
            }

            int count = _temporaryPositionsCache.Count;
            Vector2 zero = Vector2.Zero;

            for (int j = 0; j < count && (!tryStoppingOddBug || !(_temporaryPositionsCache[j] == zero)); j++)
            {
                Vector2 pos = _temporaryPositionsCache[j] + offsetForAllPositions;
                float rot = _temporaryRotationsCache[j];
                int indexOnVertexArray = j * 2;
                float progressOnStrip = (float)j / (float)(count - 1);
                AddVertex(colorFunction, widthFunction, pos, rot, indexOnVertexArray, progressOnStrip);
            }

            _vertexAmountCurrentlyMaintained = count * 2;
            PrepareIndices(count, includeBacksides);
        }

        private void PrepareIndices(int vertexPadsAdded, bool includeBacksides)
        {
            int num = vertexPadsAdded - 1;
            int num2 = 6 + includeBacksides.ToInt() * 6;
            int num3 = (_indicesAmountCurrentlyMaintained = num * num2);

            if (_indices.Length < num3)
            {
                Array.Resize(ref _indices, num3);
            }

            for (short num4 = 0; num4 < num; num4 = (short)(num4 + 1))
            {
                short num5 = (short)(num4 * num2);
                int num6 = num4 * 2;
                _indices[num5] = (short)num6;
                _indices[num5 + 1] = (short)(num6 + 1);
                _indices[num5 + 2] = (short)(num6 + 2);
                _indices[num5 + 3] = (short)(num6 + 2);
                _indices[num5 + 4] = (short)(num6 + 1);
                _indices[num5 + 5] = (short)(num6 + 3);

                if (includeBacksides)
                {
                    _indices[num5 + 6] = (short)(num6 + 2);
                    _indices[num5 + 7] = (short)(num6 + 1);
                    _indices[num5 + 8] = (short)num6;
                    _indices[num5 + 9] = (short)(num6 + 2);
                    _indices[num5 + 10] = (short)(num6 + 3);
                    _indices[num5 + 11] = (short)(num6 + 1);
                }
            }
        }

        private void AddVertex(StripColorFunction colorFunction, StripHalfWidthFunction widthFunction, Vector2 pos, float rot, int indexOnVertexArray, float progressOnStrip)
        {
            while (indexOnVertexArray + 1 >= _vertices.Length)
            {
                Array.Resize(ref _vertices, _vertices.Length * 2);
            }

            Color color = colorFunction(progressOnStrip);
            float scaleFactor = widthFunction(progressOnStrip);
            Vector2 value = WrapAngle(rot - PiOver2).ToRotationVector2() * scaleFactor;
            _vertices[indexOnVertexArray].position = pos + value;
            _vertices[indexOnVertexArray + 1].position = pos - value;
            _vertices[indexOnVertexArray].texCoord = new Vector3(progressOnStrip, 1f, 0f);
            _vertices[indexOnVertexArray + 1].texCoord = new Vector3(progressOnStrip, 0f, 0f);
            _vertices[indexOnVertexArray].color = color;
            _vertices[indexOnVertexArray + 1].color = color;
        }

        public void DrawTrail()
        {
            if (_vertexAmountCurrentlyMaintained >= 3)
            {
                Main.instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertexAmountCurrentlyMaintained, _indices, 0, _indicesAmountCurrentlyMaintained / 3);
            }
        }
    }
}
