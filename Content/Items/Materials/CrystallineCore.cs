using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using EventHorizons.Content.Projectiles.Weapons;
using Microsoft.Xna.Framework;
using EventHorizons.Content.Items.Placeables.CrystallineCavernsbiome;
namespace EventHorizons.Content.Items.Materials
{
    public class CrystallineCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            ItemID.Sets.BossBag[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.value = 5;
            Item.maxStack = Item.CommonMaxStack;
            Item.UseSound = SoundID.NPCDeath7;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 23;
            Item.useTime = 23;
            Item.shoot = ModContent.ProjectileType<CrystallineCoreProjectile>();
            Item.ammo = ItemID.FallenStar;
            Item.consumable = true;
        }
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Prevent the custom ammo from creating a projectile when used by itself
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.FallenStar, 1);
            recipe.AddIngredient(ModContent.ItemType<Crystallite>(), 5);
            recipe.AddIngredient(ItemID.Meteorite, 5);
            //Crafted by hand
            recipe.Register();
        }
        public override bool ConsumeItem(Player player)
        {

            if (player.HeldItem.type != ItemID.StarCannon)
            {
                return false;
            }
            return base.ConsumeItem(player);
        }
    }
    public class StarCannonGlob : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            // Modify star cannon code wowie
            return entity.type == ItemID.StarCannon;
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            if (ammo.type == ItemID.FallenStar)
            {
                return true;
            }

            if (ammo.type == ModContent.ItemType<CrystallineCore>())
            {
                // 50% chance to consume the custom ammo
                return Main.rand.NextFloat() >= 0.5f;
            }

            return false;
        }
    }
}