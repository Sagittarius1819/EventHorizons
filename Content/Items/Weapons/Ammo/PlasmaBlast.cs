using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using EventHorizons.Content.Items.Placeables;
using EventHorizons.Content.Items.Materials;
using EventHorizons.Content.Projectiles.Weapons;

namespace EventHorizons.Content.Items.Weapons.Ammo
{
    internal class PlasmaBlast : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 1.2f;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.ammo = AmmoID.Bullet;
            Item.shoot = ProjectileType<PlasmaBlastProjectile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(ItemType<SpaceDust>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            Recipe recipe2 = CreateRecipe(100);
            recipe2.AddIngredient(ItemID.MeteorShot, 75);
            recipe2.AddIngredient(ItemType<SpaceDust>(), 3);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}
