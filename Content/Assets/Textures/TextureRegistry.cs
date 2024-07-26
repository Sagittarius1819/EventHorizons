using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace EventHorizons.Content.Assets.Textures
{
    public static class TextureRegistry
    {
        public static Asset<Texture2D> MuzzleFlash => Request<Texture2D>("EventHorizons/Content/Assets/Textures/Misc/MuzzleFlash", AssetRequestMode.ImmediateLoad);

        public static Asset<Texture2D> HalfStar => Request<Texture2D>("EventHorizons/Content/Assets/Textures/Misc/Extra_98", AssetRequestMode.ImmediateLoad);

        public static Asset<Texture2D> GlowLarge => Request<Texture2D>("EventHorizons/Content/Assets/Textures/Misc/GlowBallLarge", AssetRequestMode.ImmediateLoad);
    }
}
