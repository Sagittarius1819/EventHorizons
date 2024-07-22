using EventHorizons.Content.Assets.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria;
using Terraria.ID;

namespace EventHorizons.Commons
{
    public static partial class HorizonUtils
    {
        /// <summary>
        /// Handles drawing a proj. Accounts for frames, rotation, and origin. 
        /// </summary>
        /// <param name="Projectile">The projectile that should be drawn. Used to get the framecount.</param>
        /// <param name="texture">The texture to draw.</param>
        /// <param name="Color">The color to draw in. For regulat drawing, pass Predraw's lightColor.</param>
        /// <param name="IsGlow">Whether or not to draw purely in the color provided.</param>
        public static void SimpleDrawProjectile(this Projectile Projectile, Texture2D texture, Color Color, bool IsGlow, float scaleMod = 1f, float extraRot = 0f)
        {
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Vector2 drawPos = Projectile.Center - Main.screenPosition;

            Main.EntitySpriteDraw(texture, drawPos, new Microsoft.Xna.Framework.Rectangle?(sourceRectangle), IsGlow ? Color : Projectile.GetAlpha(Color), Projectile.rotation + extraRot, origin, Projectile.scale * scaleMod, spriteEffects, 0);
        }

        public static string TryGetTextureFromOther<T>() where T : class
        {
            if (typeof(T).Namespace == null)
            {
                GetInstance<EventHorizons>().Logger.Warn("No texture was found for the provided type, or it uses a different texture!");
                throw new System.Exception("LIGMA LIGMA THI ISNT VERY SIGMA (missing texture, make sure the tpye you are using this on has a texture matching its class name!)");
            }

            return typeof(T).Namespace.Replace(".", "/") + "/" + typeof(T).Name;
        }

        public static void Reload(this SpriteBatch spriteBatch, SpriteSortMode sortMode = SpriteSortMode.Deferred)
        {
            if ((bool)spriteBatch.GetType().GetField("beginCalled", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch))
                spriteBatch.End();

            BlendState blendState = (BlendState)spriteBatch.GetType().GetField("blendState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            SamplerState samplerState = (SamplerState)spriteBatch.GetType().GetField("samplerState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            DepthStencilState depthStencilState = (DepthStencilState)spriteBatch.GetType().GetField("depthStencilState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            RasterizerState rasterizerState = (RasterizerState)spriteBatch.GetType().GetField("rasterizerState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            Effect effect = (Effect)spriteBatch.GetType().GetField("customEffect", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            Matrix matrix = (Matrix)spriteBatch.GetType().GetField("transformMatrix", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }

        public static void Reload(this SpriteBatch spriteBatch, BlendState blendState = default)
        {
            if ((bool)spriteBatch.GetType().GetField("beginCalled", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch))
                spriteBatch.End();

            SpriteSortMode sortMode = SpriteSortMode.Deferred;
            SamplerState samplerState = (SamplerState)spriteBatch.GetType().GetField("samplerState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            DepthStencilState depthStencilState = (DepthStencilState)spriteBatch.GetType().GetField("depthStencilState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            RasterizerState rasterizerState = (RasterizerState)spriteBatch.GetType().GetField("rasterizerState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            Effect effect = (Effect)spriteBatch.GetType().GetField("customEffect", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            Matrix matrix = (Matrix)spriteBatch.GetType().GetField("transformMatrix", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }

        public static void Reload(this SpriteBatch spriteBatch, Effect effect = null)
        {
            if ((bool)spriteBatch.GetType().GetField("beginCalled", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch))
                spriteBatch.End();

            SpriteSortMode sortMode = SpriteSortMode.Deferred;
            BlendState blendState = (BlendState)spriteBatch.GetType().GetField("blendState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            SamplerState samplerState = (SamplerState)spriteBatch.GetType().GetField("samplerState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            DepthStencilState depthStencilState = (DepthStencilState)spriteBatch.GetType().GetField("depthStencilState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            RasterizerState rasterizerState = (RasterizerState)spriteBatch.GetType().GetField("rasterizerState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            Matrix matrix = (Matrix)spriteBatch.GetType().GetField("transformMatrix", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }

        //trails

        /// <summary>
        /// Draws a trail made out of extra_98's behind the given projectile.
        /// </summary>
        /// <param name="Projectile">the projectile to draw to</param>
        /// <param name="scaleOuter">outer strip scale</param>
        /// <param name="scaleMid">semi-inner strip scale</param>
        /// <param name="scaleInner">inner strip scale</param>
        /// <param name="colOuter">outer strip color</param>
        /// <param name="colMid">semi-inner strip color</param>
        /// <param name="colInner">inner strip color</param>
        /// <param name="extraRot">additional rotation, for projs that might have weird rotational offsets. -PiOver2 is the default value.</param>
        public static void DrawExtra_98Trail(this Projectile Projectile, Vector2 scaleOuter, Vector2 scaleMid, Vector2 scaleInner, Color colOuter, Color colMid, Color colInner, float extraRot = -PiOver2, bool appliedColorize = false)
        {
            Texture2D afterimageTexture = TextureRegistry.HalfStar.Value;

            for (float i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i += 0.5f)
            {
                Color color = colOuter * Projectile.Opacity * 0.75f;

                if (!appliedColorize)
                    color.A = 0;

                color *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];

                Color color2 = colMid * Projectile.Opacity * 0.75f;

                if (!appliedColorize)
                    color2.A = 0;

                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];

                Color color3 = colInner * Projectile.Opacity * 0.7f;

                if (!appliedColorize)
                    color3.A = 0;

                color3 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];

                int max0 = (int)i - 1;
                if (max0 < 0)
                    continue;

                float rot = Projectile.oldRot[max0] + extraRot;
                Vector2 center = Vector2.Lerp(Projectile.oldPos[(int)i], Projectile.oldPos[max0], 1 - i % 1);
                center += Projectile.Size / 2;

                Main.EntitySpriteDraw(afterimageTexture, center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), null, color, rot, afterimageTexture.Size() / 2f, scaleOuter * Projectile.scale, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(afterimageTexture, center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), null, color2, rot, afterimageTexture.Size() / 2f, scaleMid * Projectile.scale, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(afterimageTexture, center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), null, color3, rot, afterimageTexture.Size() / 2f, scaleInner * Projectile.scale, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// Draws a cool trail behind the provided projectle. 
        /// </summary>
        /// <param name="Projectile">The projectile to draw to.</param>
        /// <param name="scales">An array for the scales of the trail segments. [outer, middle, inner]</param>
        /// <param name="colors">An array for the colors of the trail segments. [outer, middle, inner]</param>
        public static void DrawTrail_WithSpheres(this Projectile Projectile, Vector2[] scales, Color[] colors)
        {
            Texture2D afterimageTexture = TextureRegistry.GlowLarge.Value;

            for (float i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i += 0.4f)
            {
                Color color = colors[0] * Projectile.Opacity * 0.75f;

                color *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];

                Color color2 = colors[1] * Projectile.Opacity * 0.75f;

                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];

                Color color3 = colors[2] * Projectile.Opacity * 0.7f;

                color3 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];

                int max0 = (int)i - 1;
                if (max0 < 0)
                    continue;

                float rot = Projectile.oldRot[max0];
                Vector2 center = Vector2.Lerp(Projectile.oldPos[(int)i], Projectile.oldPos[max0], 1 - i % 1);
                center += Projectile.Size / 2;

                float scaleMult = Lerp(1f, 0.2f, i / ProjectileID.Sets.TrailCacheLength[Projectile.type]);

                Main.EntitySpriteDraw(afterimageTexture, center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), null, color with { A = 0 }, rot, afterimageTexture.Size() / 2f, scales[0] * Projectile.scale * scaleMult, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(afterimageTexture, center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), null, color2 with { A = 0 }, rot, afterimageTexture.Size() / 2f, scales[1] * Projectile.scale * scaleMult, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(afterimageTexture, center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), null, color3 with { A = 0 }, rot, afterimageTexture.Size() / 2f, scales[2] * Projectile.scale * scaleMult, SpriteEffects.None, 0);
            }
        }
    }
}
