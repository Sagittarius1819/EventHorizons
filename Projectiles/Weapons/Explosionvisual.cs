using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace EventHorizons.Projectiles.Weapons
{
    public class Explosionvisual : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Total count animation frames
            Main.projFrames[Projectile.type] = 8;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
            Projectile.timeLeft--; // Maintain the projectile's existence for a short period
            if (Projectile.timeLeft <= 0)
            {
                // Destroy the projectile after playing the final animation frame
                Projectile.Kill();
            }
        }
        public override void SetDefaults()
        {
            Projectile.width = 32; // The width of the projectile
            Projectile.height = 32; // The height of the projectile
            Projectile.aiStyle = -1; // Custom AI style
            Projectile.friendly = true; // Determines if the projectile can damage enemies
            Projectile.hostile = false; // Determines if the projectile can damage the player
            Projectile.penetrate = -1; // The projectile never disappears from penetrating enemies
            Projectile.timeLeft = 80; // The time left before the projectile disappears
            Projectile.ignoreWater = true; // Whether the projectile ignores water
            Projectile.tileCollide = false; // Do not collide with tiles
            Projectile.extraUpdates = 1; // Number of extra updates per frame

        }



    }
}

