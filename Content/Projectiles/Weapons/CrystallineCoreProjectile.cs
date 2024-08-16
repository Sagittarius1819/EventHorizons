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
    public class CrystallineCoreProjectile : ModProjectile //Formatter? Never heard of it.
    {
        private int bounceCount = 0;
        private const int maxBounces = 3;
        private float scaleFactor = 1f;
        private const float maxScale = 3f;
        private const float scaleSpeed = 0.1f;
        private float originalDamage;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            originalDamage = Projectile.damage;
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
            // Homing mechanism
            float homingRange = 400f; // Adjust as needed
            float homingSpeed = 4f; // Adjust as needed
            float maxDetectRadius = 400f;
            float desiredFlySpeedInPixelsPerFrame = 20f;
            float amountOfFramesToLerpBy = 50f;

            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC != null)
            {
                Vector2 desiredVelocity = Projectile.DirectionTo(closestNPC.Center) * desiredFlySpeedInPixelsPerFrame;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
            }

            if (Projectile.scale > 1f)
            {
                Projectile.scale -= scaleSpeed;
                if (Projectile.scale < 1f)
                {
                    Projectile.scale = 1f;
                }
            }

            if (Projectile.timeLeft < 240)
            {
                float fadeAmount = Projectile.timeLeft / 240f;
                Projectile.alpha = (int)((1 - fadeAmount) * 255);
            }
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
                private NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
            float closestDistanceSqr = float.MaxValue;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy(Projectile))
                {
                    float distanceSqr = Vector2.DistanceSquared(npc.Center, Projectile.Center);

                    if (distanceSqr < closestDistanceSqr && distanceSqr < sqrMaxDetectDistance)
                    {
                        closestDistanceSqr = distanceSqr;
                        closestNPC = npc;
                    }
                }
            }

            return closestNPC;
        }

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            SoundEngine.PlaySound(SoundID.Item4, Projectile.position);

            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Scale: 1.5f);
            }

            Projectile.damage = (int)(Projectile.damage * 0.5f);

            Projectile.scale = maxScale;

        }

        public void OnHitPlayer(Player target, int damage, bool crit)
        {
            for (int i = 0; i < 10; i++) 
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Scale: 1.5f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Increment bounce count
            bounceCount++;

            if (bounceCount >= maxBounces)
            {
                Projectile.Kill();
                return true;
            }

            // Bounce off walls
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Item150, Projectile.position); 

            Projectile.scale = maxScale;

            return false;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item4, Projectile.position);

            for (int i = 0; i < 10; i++) 
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ShimmerTorch, Scale: 1.5f);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                Color.White * (1f - Projectile.alpha / 255f), // Useless code doesnt do anything
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