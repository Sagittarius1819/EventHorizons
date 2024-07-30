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

        public static Asset<Texture2D> HalfStar => Request<Texture2D>("EventHorizons/Assets/Textures/Misc/Extra_98", AssetRequestMode.ImmediateLoad);

        public static Asset<Texture2D> GlowLarge => Request<Texture2D>("EventHorizons/Assets/Textures/Misc/GlowBallLarge", AssetRequestMode.ImmediateLoad);

        public static Asset<Texture2D> BeamStart => Request<Texture2D>("EventHorizons/Assets/Textures/Misc/BeamStart", AssetRequestMode.ImmediateLoad);

        public static Asset<Texture2D> BeamMiddle => Request<Texture2D>("EventHorizons/Assets/Textures/Misc/BeamMiddle", AssetRequestMode.ImmediateLoad);

        public static Asset<Texture2D> BeamEnd => Request<Texture2D>("EventHorizons/Assets/Textures/Misc/BeamEnd", AssetRequestMode.ImmediateLoad);
    }
}
