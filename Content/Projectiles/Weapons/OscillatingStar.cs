using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
namespace EventHorizons.Content.Projectiles.Weapons
{
    public class OscillatingStar : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile mainProjectile = Main.projectile[(int)Projectile.ai[0]];

            if (!mainProjectile.active || mainProjectile.type != ModContent.ProjectileType<CrystallineCoreProjectile>())
            {
                Projectile.Kill();
                return;
            }
            Projectile.Center = mainProjectile.Center;

            float scale = 0.5f + 0.5f * (float)Math.Sin(Main.GameUpdateCount * 0.1f);
            Projectile.scale = scale;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Draw the projectile with its current scale
            Main.EntitySpriteDraw(
                TextureAssets.Projectile[Projectile.type].Value,
                Projectile.Center - Main.screenPosition,
                null,
                Color.White,
                Projectile.rotation,
                TextureAssets.Projectile[Projectile.type].Size() / 2,
                Projectile.scale,
                SpriteEffects.None,
                0
            );
            return false;
        }
    }
}