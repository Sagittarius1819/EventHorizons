using EventHorizons.Content.Biomes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace EventHorizons.Content.Assets.Textures.Backgrounds
{
    public class Blackholesky : ModMenu
    {
        private int NearPlanetScroll;
        private List<Flyby> Flybys = new List<Flyby>();

        private static Texture2D AsteroidSmallTexture = ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/TitleAsteroid2", AssetRequestMode.ImmediateLoad).Value;
        private static Texture2D AsteroidMediumTexture = ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/TitleAsteroid3", AssetRequestMode.ImmediateLoad).Value;
        private static Texture2D AsteroidLargeTexture = ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/TitleAsteroid1", AssetRequestMode.ImmediateLoad).Value;
        private static Texture2D BackgroundTexture => ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/SpaceBackgroundFar", AssetRequestMode.ImmediateLoad).Value;
        private static Texture2D CometTexture => ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/TitleComet", AssetRequestMode.ImmediateLoad).Value;
        private static Texture2D NearPlanetTexture => ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/SpaceBackgroundClose", AssetRequestMode.ImmediateLoad).Value;

        private static Texture2D[] AsteroidTextures = [
            AsteroidSmallTexture,
            AsteroidMediumTexture,
            AsteroidLargeTexture
        ];

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("EventHorizons/Content/Assets/Textures/Backgrounds/ModLogo", AssetRequestMode.ImmediateLoad);

        public override int Music => MusicLoader.GetMusicSlot("EventHorizons/Content/Assets/Music/Discovery");

        public override string DisplayName => "Event Horizon";

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<HollowSpaceBackgroundStyle>();

        public override void Load()
        {
            NearPlanetScroll = -Main.screenWidth;
            Flybys.Add(new Flyby(FlybyType.Comet));
            for (int i = 0; i < 4; i++)
            {
                Flybys.Add(new Flyby(FlybyType.Asteroid));
            }
        }

        public override void Update(bool isOnTitleScreen)
        {
            NearPlanetScroll += 1;
            if (NearPlanetScroll > Main.screenWidth) NearPlanetScroll = -Main.screenWidth;

            foreach (Flyby f in Flybys) f.Update();

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

            // Draw large asteroids and comet in front of the scrolling planet
            foreach (Flyby f in Flybys.Where(a => a.Tex != AsteroidLargeTexture && a.Tex != CometTexture)) f.Draw(spriteBatch);
            spriteBatch.Draw(NearPlanetTexture, new Vector2(NearPlanetScroll, 0), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            foreach (Flyby f in Flybys.Where(a => a.Tex == AsteroidLargeTexture || a.Tex == CometTexture)) f.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, Main.Rasterizer, null, Main.UIScaleMatrix);
            return true;
        }

        private enum FlybyType
        {
            Asteroid,
            Comet
        }

        private class Flyby
        {
            public FlybyType Type;
            public Texture2D Tex;
            public Texture2D[] RandomTextures;
            public Vector2 Pos = new Vector2(0, -1000);
            public Vector2 MoveDir = Vector2.Zero;
            public float Rot;
            public float MoveSpeed;
            public float RotSpeed;
            public int SpawnSide;
            public float Scale = 2f;
            public int Cooldown;
            public bool DoRotate;

            public Flyby(FlybyType type)
            {
                Type = type;
                if (Type == FlybyType.Asteroid)
                {
                    Tex = AsteroidLargeTexture;
                    DoRotate = true;
                    RandomTextures = AsteroidTextures;
                }
                else if (Type == FlybyType.Comet)
                {
                    Tex = CometTexture;
                    DoRotate = false;
                    RandomTextures = [CometTexture];
                }
            }

            public void Update()
            {
                float biggestSide = Scale * Max(Tex.Width, Tex.Height);

                // Check if texture is fully off-screen - if so, "spawn" new object
                if (Pos.X + biggestSide / 2f < 0 && SpawnSide != 0 || Pos.X - biggestSide / 2f > Main.screenWidth && SpawnSide != 1 ||
                    Pos.Y + biggestSide / 2f < 0 || Pos.Y - biggestSide / 2f > Main.screenHeight)
                {
                    SpawnSide = Main.rand.Next(2);
                    float y = Main.screenHeight * Main.rand.NextFloat();
                    float x = SpawnSide == 0 ? -biggestSide / 2f : Main.screenWidth + biggestSide / 2f;
                    Pos = new Vector2(x, y);
                    MoveDir = SpawnSide == 0 ? Main.rand.NextVector2Unit(-Pi / 6f, 2 * Pi / 6f) : Main.rand.NextVector2Unit(5 * Pi / 6f, 2 * Pi / 6f);

                    Tex = RandomTextures[Main.rand.Next(RandomTextures.Length)];
                    Cooldown = Main.rand.Next(100, 600);

                    if (DoRotate) RotSpeed = Main.rand.NextFloat(-0.01f, 0.01f);
                    else Rot = Pos.AngleFrom(Pos + MoveDir);
                    MoveSpeed = Main.rand.NextFloat(0.5f, 2f);
                    return;
                }

                if (DoRotate) Rot += RotSpeed;

                if (Cooldown-- <= 0) Pos += MoveSpeed * MoveDir;
            }

            // Expected after SpriteBatch.Begin
            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(Tex, Pos, null, Color.White, Rot, Tex.Size() / 2f, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}