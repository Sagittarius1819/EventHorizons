using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace EventHorizons.Backgrounds
{
    public class Blackholesky : ModMenu
    {
        public override Asset<Texture2D> Logo => base.Logo; // logo, change to a string with your specified path
        public override void Update(bool isOnTitleScreen)
        {
            Main.dayTime = false;
            Main.time = 40001;

            // if you dont need, remove that
        }
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            Texture2D MenuBG = ModContent.Request<Texture2D>("EventHorizons/Backgrounds/Blackholesky").Value;
            float width = (float)Main.screenWidth / (float)MenuBG.Width;
            float height = (float)Main.screenHeight / (float)MenuBG.Height;
            Vector2 zero = Vector2.Zero;
            if (width != height)
            {
                if (height > width)
                {
                    width = height;
                    zero.X -= ((float)MenuBG.Width * width - (float)Main.screenWidth) * 0.5f;
                }
                else
                {
                    zero.Y -= ((float)MenuBG.Height * width - (float)Main.screenHeight) * 0.5f;
                }
            }
            spriteBatch.Draw(
                MenuBG, // texture
                new Vector2(zero.X, zero.Y), // position
                null, // rectangle
                Color.White, // color
                0f, // rotation
               zero, // origin
                width, // scale
                0, // spriteeffects
                0f); // layerdepth
            return true;
        }
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/TitleScreen");

        public override string DisplayName => "Event Horizon";



    }
}