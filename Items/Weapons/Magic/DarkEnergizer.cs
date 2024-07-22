using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EventHorizons.Projectiles.Weapons;
using EventHorizons.Items.Placeables;
using EventHorizons.Items.Materials;

namespace EventHorizons.Items.Weapons.Magic
{
    public class DarkEnergizer : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.EventHorizons.hjson file.


        public override void SetDefaults()
        {
            Item.damage = 78;
            Item.DamageType = DamageClass.Magic;
            Item.width = 74;
            Item.height = 74;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 1;
            Item.knockBack = 22;
            Item.value = 10000;
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<Accretion_disc>();
            Item.shootsEveryUse = true;
            Item.shootSpeed = 10.5f;


        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 14);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.BreakerBlade, 1);
            recipe.AddIngredient(ItemType<Galvanitebar>(), 5);
            recipe.AddIngredient(ItemType<SpaceDust>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.AdamantiteBar, 14);
            recipe2.AddIngredient(ItemID.FallenStar, 5);
            recipe2.AddIngredient(ItemID.BreakerBlade, 1);
            recipe2.AddIngredient(ItemType<Galvanitebar>(), 5);
            recipe.AddIngredient(ItemType<SpaceDust>(), 5);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.Register();
        }
    }
}