using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Shaders;
using Terraria;
using System.Runtime.InteropServices;

namespace EventHorizons.Core.Primitives
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    //to draw these, assign a value to TrailCacheLength, AND ALWAYS USE 2 AS THE MODE (they need it for properly rotating)
    //call default(TrailName).Draw(Projectile) in PreDraw to apply them!
    public struct Trail
    {
        private static VertexStrip _vertexStrip = new();

        /// <summary>
        /// Draws a trail behind the provided projctile.
        /// </summary>
        /// <param name="proj">The proj to draw to.</param>
        /// <param name="col">The color to draw with.</param>
        /// <param name="width">The width of the trail.</param>
        public static void Draw(Projectile proj, VertexStrip.StripColorFunction col, VertexStrip.StripHalfWidthFunction width, bool UseBacksides = true, bool TryFixingMissalignment = false)
        {
            MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];

            miscShaderData.UseSaturation(-2.8f);
            miscShaderData.UseOpacity(4f);
            miscShaderData.Apply();

            _vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, col, width, -Main.screenPosition + proj.Size / 2f, UseBacksides, TryFixingMissalignment);
            _vertexStrip.DrawTrail();

            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        }
    }

    public struct Trail_Flamelash
    {
        private static VertexStrip _vertexStrip = new();

        /// <summary>
        /// Draws a trail behind the provided projctile.
        /// </summary>
        /// <param name="proj">The proj to draw to.</param>
        /// <param name="col">The color to draw with.</param>
        /// <param name="width">The width of the trail.</param>
        public static void Draw(Projectile proj, VertexStrip.StripColorFunction col, VertexStrip.StripHalfWidthFunction width)
        {
            MiscShaderData miscShaderData = GameShaders.Misc["FlameLash"];

            miscShaderData.UseSaturation(-2.8f);
            miscShaderData.UseOpacity(4f);
            miscShaderData.Apply();

            _vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, col, width, -Main.screenPosition + proj.Size / 2f, includeBacksides: true);
            _vertexStrip.DrawTrail();

            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        }
    }
}
