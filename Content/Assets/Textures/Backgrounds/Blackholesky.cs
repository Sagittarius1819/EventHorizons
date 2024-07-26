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
        private int CometScroll;
        private int NearPlanetScroll;

        public static Texture2D BackgroundTexture => ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/SpaceBackgroundFar", AssetRequestMode.ImmediateLoad).Value;
        //public static Texture2D CometTexture => ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/TitleScreenComet", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D NearPlanetTexture => ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/SpaceBackgroundClose", AssetRequestMode.ImmediateLoad).Value;

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/ModLogo", AssetRequestMode.ImmediateLoad);

        public override int Music => MusicLoader.GetMusicSlot("EventHorizons/Content/Assets/Music/Discovery");

        public override string DisplayName => "Event Horizon";

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<HollowSpaceBackgroundStyle>();

        public override void Load()
        {
            CometScroll = Main.screenWidth;
            NearPlanetScroll = -Main.screenWidth;
        }

        public override void Update(bool isOnTitleScreen)
        {
            // to-do: a better system than hardcoding this
            NearPlanetScroll += 1;
            if (NearPlanetScroll > Main.screenWidth) NearPlanetScroll = -Main.screenWidth;

            CometScroll -= 3;
            if (CometScroll < 0) CometScroll = Main.screenWidth;

            // Keeps the "sun" up in the menu
            Main.time = 12000;
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            spriteBatch.End();

            float scaleX = Main.screenWidth / (float)BackgroundTexture.Width;
            float scaleY = Main.screenHeight / (float)BackgroundTexture.Height;
            float scale = Max(scaleX, scaleY);
            // tML doesn't let you change the background image, so we have to draw it ourselves
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, null, Main.UIScaleMatrix);
            spriteBatch.Draw(BackgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();

            // ... since the other background elements are drawn before this, that means we have to manually draw them, too
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, null, Main.UIScaleMatrix);
            spriteBatch.Draw(NearPlanetTexture, new Vector2(NearPlanetScroll, 0), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, Main.Rasterizer, null, Main.UIScaleMatrix);
            return true;
        }
    }
}