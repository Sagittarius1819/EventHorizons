using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EventHorizons.Projectiles;

namespace EventHorizons.Projectiles.Weapons
{
    public class Accretion_disc : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Magic;
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;


        }
        public override void PostAI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] < 60f)
            {
                Projectile.velocity *= 0.99f;
            }
            else
            {
                Projectile.velocity *= 0.99f;
                if (Projectile.ai[0] >= 180)
                {
                    Projectile.Kill();
                }
            }

            float rotateSpeed = 0.10f * (float)Projectile.direction;
            Projectile.rotation += rotateSpeed;


        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.Center, 1, 1, 16, 0f, 0f, 0);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.5f;
            Main.dust[dust].scale = (float)Main.rand.Next(20, 30) * 0.05f;


        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[target.target];

            Vector2 direction = (player.Center - target.Center).SafeNormalize(Vector2.UnitX);
            direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

            int projectile2 = Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, direction * 1, ProjectileID.CultistBossIceMist, 5, 0, Main.myPlayer);
            Main.projectile[projectile2].timeLeft = 300;
            Main.projectile[projectile2].damage = 14;
            Main.projectile[projectile2].friendly = true;
            Main.projectile[projectile2].hostile = false;

        }

    }

}
