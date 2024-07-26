using EventHorizons.Content.Biomes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace EventHorizons.Content.Assets.Textures.Backgrounds
{
    public class Blackholesky : ModMenu
    {
        public static Texture2D BackgroundTexture => ModContent.Request<Texture2D>("EventHorizons/Assets/Textures/Backgrounds/EHtitlebackdrop", AssetRequestMode.ImmediateLoad).Value;
        private Asset<Texture2D> modLogo;
        public override void Load()
        {
            modLogo  = ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/ModLogo");
        }

        public override Asset<Texture2D> Logo => modLogo;
        public override int Music => MusicLoader.GetMusicSlot("EventHorizons/Content/Assets/Music/Discovery");

        public override string DisplayName => "Event Horizon";

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<HollowSpaceBackgroundStyle>();


    }
}