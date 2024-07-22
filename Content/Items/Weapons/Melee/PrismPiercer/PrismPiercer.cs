using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using EventHorizons.Content.Items.Materials;
using EventHorizons.Content.Items.Placeables.CrystallineCavernsbiome;

namespace EventHorizons.Content.Items.Weapons.Melee.PrismPiercer
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

            Item.shoot = ProjectileType<PrismPiercerProjectile>();
            Item.shootSpeed = 12f; //might need to be changed
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBroadsword, 14);
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.AddIngredient(ItemID.Bone, 15);
            recipe.AddIngredient(ItemType<Crystallite>(), 25);
            recipe.AddIngredient(ItemType<CrystallineStone>(), 35);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            //Alt recipe for platinum
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.PlatinumBroadsword, 14);
            recipe1.AddIngredient(ItemID.FallenStar, 3);
            recipe1.AddIngredient(ItemID.Bone, 15);
            recipe1.AddIngredient(ItemType<Crystallite>(), 25);
            recipe1.AddIngredient(ItemType<CrystallineStone>(), 35);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = PiOver4;

            position += Vector2.Normalize(velocity) * 45f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.4f;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
namespace EventHorizons.Content.Items.Weapons.Melee.PrismPiercer
{
    public class PrismPiercerProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.timeLeft = 45;
            Projectile.friendly = true;
            Projectile.width = 8;
            Projectile.height = 14;
            Projectile.damage = 10;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == -1 ? -PiOver2 : PiOver2);
            if (Projectile.ai[2] == 0)
            {
                Projectile.frame = Main.rand.Next(4);
                Projectile.ai[2] = 1;
            }
            {
                // Fade out over time
                Projectile.alpha += 3; // Increase alpha (transparency) by 3 each tick
                if (Projectile.alpha > 255)
                {
                    Projectile.alpha = 255; // Cap the alpha value at 255 (fully transparent)
                }
            }
            if (Projectile.localAI[0] == 0f) //set projectile to use Player velocity
            {
                Player player = Main.player[Projectile.owner];
                Projectile.velocity += player.velocity;
                Projectile.localAI[0] = 1f; // Set a flag to ensure this only happens once
            }
        }

        private Color TrailColor(float prog)
        {
            Color col = Color.MediumVioletRed;
            Color col2 = Color.Lerp(Color.Purple, Color.White, 0.9f);

            return Color.Lerp(col2, col, prog);
        }

        private float TrailWidth(float prog)
        {
            float w = 15f;
            w *= Lerp(1, 0, prog);
            return w;
        }

        public override bool PreDraw(ref Color lightColor)
            {
           //default(Trail).Draw(Projectile, TrailColor, TrailWidth);

         Projectile.SimpleDrawProjectile(TextureAssets.Projectile[Type].Value, Color.White, true, 1f);

            return false;
        }

    }
}