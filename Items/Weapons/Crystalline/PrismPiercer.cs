using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent;
using EventHorizons.Core.Primitives;
using System.Linq.Expressions;

namespace EventHorizons.Items.Weapons.Crystalline
{
    public class PrismPiercer : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(silver: 90);
            Item.rare = ItemRarityID.Purple; //might need to be changed
            Item.UseSound = SoundID.Item1; //might need to be changed
            Item.autoReuse = true;
            Item.crit = 8;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shoot = ModContent.ProjectileType<PrismPiercerProjectile>();
            Item.shootSpeed = 12f; //might need to be changed
        }

        public override void AddRecipes() => CreateRecipe().Register();
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = PiOver4 * 0.85f;

            position += Vector2.Normalize(velocity) * 20f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.4f;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }

    public class PrismPiercerProjectile : ModProjectile
    {
        public override string Texture => TryGetTextureFromOther<PrismPiercer>() + "_Projectile";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
        }

        public override void SetDefaults()
        {
            Projectile.timeLeft = 45;
            Projectile.friendly = true;
            Projectile.width = 8;
            Projectile.height = 14;
        }

        public override void AI()
        {
            if (Projectile.ai[2] == 0)
            {
                Projectile.frame = Main.rand.Next(4);
                Projectile.ai[2] = 1;
            }

            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == -1 ? -PiOver2 : PiOver2);
        }

        public override void OnKill(int timeLeft)
        {
            for(int i = 0; i < 12; i++)
            {
                var d = Dust.NewDustDirect(Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), 0, 0, DustID.GemAmethyst);
                d.velocity = Main.rand.NextVector2Circular(3f, 3f);
                d.noGravity = Main.rand.NextFloat(1.5f) > 1.1f;
            }
        }

        private Color TrailColor(float progressOnStrip)
        {
            Color col = Color.MediumVioletRed;
            Color col2 = Color.Lerp(Color.Purple, Color.White, 0.9f);
            Color colRet = Color.Lerp(col2, col, progressOnStrip);
            colRet.A = 0;

            return colRet;
        }

        private float TrailWidth(float progressOnStrip)
        {
            float num = 0.5f;
            float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
            num *= 1f - (1f - lerpValue) * (1f - lerpValue);

            return Lerp(0f, 35f, num);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            //default(Trail).Draw(Projectile, TrailColor, TrailWidth); PRIMS ARENT WORKING ATM, I'LL FIX IT SOON!

            Projectile.DrawTrail_WithSpheres([new Vector2(0.02f, 0.015f), new Vector2(0.012f, 0.007f), new Vector2(0.001f, 0.0007f)], [Color.MediumVioletRed, Color.Violet, Color.Lerp(Color.MediumVioletRed, Color.White, 0.95f)]);

            Projectile.SimpleDrawProjectile(TextureAssets.Projectile[Type].Value, Color.White, true, 1f);

            return false;
        }
    }
}
