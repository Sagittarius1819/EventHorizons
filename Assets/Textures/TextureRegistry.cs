using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace EventHorizons.Assets.Textures
{
    public static class TextureRegistry
    {
        public static Asset<Texture2D> MuzzleFlash => Request<Texture2D>("EventHorizons/Assets/Textures/Misc/MuzzleFlash", AssetRequestMode.ImmediateLoad);
    }
}
