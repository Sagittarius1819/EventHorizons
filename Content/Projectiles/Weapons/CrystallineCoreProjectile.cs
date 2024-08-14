using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria.GameContent;
namespace EventHorizons.Content.Projectiles.Weapons
{
    public class CrystallineCoreProjectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.damage = 30;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 1f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<OscillatingStar>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
                SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
            }
            Projectile.rotation += 0.1f;

            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].noGravity = true;
            Main.dust[dust].fadeIn = 0.75f;


            int numOrbitingStars = 3;
            float orbitRadius = 20f;
            float orbitSpeed = 0.1f;

            for (int i = 0; i < numOrbitingStars; i++)
            {
                float angle = (float)(Main.GameUpdateCount * orbitSpeed + i * MathHelper.TwoPi / numOrbitingStars);
                Vector2 orbitPosition = Projectile.Center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * orbitRadius;

  
                Dust orbitDust = Dust.NewDustPerfect(orbitPosition, DustID.PurpleCrystalShard);
                orbitDust.noGravity = true;
                orbitDust.fadeIn = 0.75f; 
            }
            Lighting.AddLight(Projectile.Center, 0.75f, 0f, 0.75f); 
        }

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            SoundEngine.PlaySound(SoundID.Item4, Projectile.position);
            
            for (int i = 0; i < 10; i++) 
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Scale: 1.5f);

            }

        }

        public void OnHitPlayer(Player target, int damage, bool crit)
        {
            for (int i = 0; i < 10; i++) 
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Scale: 1.5f);
            }
        }

        public void OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item4, Projectile.position);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Scale: 1.5f);
            }
            Projectile.Kill();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                Color.White,
                Projectile.rotation,
                origin,
                Projectile.scale,
                SpriteEffects.None,
                0
            );
            return false;
        }
    }
}