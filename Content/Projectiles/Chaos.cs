using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using EventHorizons.Content.Projectiles.Weapons;
namespace EventHorizons.Content.Projectiles
{
    public class Chaos : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 180;



        }
        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 targetPosition = Main.MouseWorld;
            float speed = 10f; // Speed of the projectile
            Vector2 direction = targetPosition - Projectile.Center;
            direction.Normalize();
            direction *= speed;
            float rotateSpeed = 0.20f * Projectile.direction;
            Projectile.rotation += rotateSpeed;
            Projectile.velocity = (Projectile.velocity * 20f + direction) / 21f; // Smoothly homing in

            // Slowly shrink the projectile over time
            Projectile.scale -= 0.01f;
            if (Projectile.scale <= 0f)
            {
                Projectile.scale = 0f;
                Explode(); // Trigger the explosion of dust when the scale reaches 0
            }

            // Ensure the projectile explodes into dust when it is about to be removed
            if (Projectile.timeLeft == 1)
            {
                Explode();
            }

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[target.target];

            Vector2 direction = (player.Center - target.Center).SafeNormalize(Vector2.UnitX);
            direction = direction.RotatedByRandom(ToRadians(10));

            int projectile2 = Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, direction * 1, ProjectileID.MagicMissile, 5, 0, Main.myPlayer);
            Main.projectile[projectile2].timeLeft = 300;
            Main.projectile[projectile2].damage = 14;
            Main.projectile[projectile2].friendly = true;
            Main.projectile[projectile2].hostile = false;
            // Explode on tile impact and spawn another projectile
            Explode();
            return;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Print debug message to ensure method is called
            Explode();
            return true;
        }
        private void Explode()
        {
            // Create explosion effect

            // Play explosion sound
            SoundEngine.PlaySound(SoundID.Item4, Projectile.position);

            // Spawn another projectile at the point of impact
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.Zero, ProjectileType<Explosionvisual>(), Projectile.damage, Projectile.knockBack, Projectile.owner);

            // Destroy the projectile
            Projectile.Kill();
        }

    }

}
