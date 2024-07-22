using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using System.Numerics;

namespace EventHorizons.Content.Projectiles.Weapons
{
    internal class PlasmaBlastProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 5;

        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.aiStyle = 1;
            Projectile.hostile = false;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
            Projectile.light = 1f;

        }
        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
                return false;
            }

            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

            SoundEngine.PlaySound(SoundID.GlommerBounce, Projectile.position);

            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }

            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

        public override void AI()
        {
            // Get the default velocity of the projectile
            float defaultVelocity = Projectile.velocity.Length();

            // Calculate the range for the random value
            float minVelocity = defaultVelocity * 0.9f;
            float maxVelocity = defaultVelocity * 1.1f;

            // Generate a random value within the range
            float randomVelocity = Main.rand.NextFloat(minVelocity, maxVelocity);

            // Set the projectile's velocity to the random value
            Projectile.velocity.Normalize();
            Projectile.velocity *= randomVelocity;
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        public override void PostAI()
        {


            float rotateSpeed = 0.20f * Projectile.direction;
            Projectile.rotation += rotateSpeed;


        }

    }
}
