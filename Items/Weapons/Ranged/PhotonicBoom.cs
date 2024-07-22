using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using EventHorizons.Items.Weapons.Ammo;
using EventHorizons.Items.Materials;

namespace EventHorizons.Items.Weapons.Ranged
{
    internal class PhotonicBoom : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = 5;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 35;
            Item.knockBack = 30;
            Item.UseSound = SoundID.Item96;
            Item.noMelee = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 5f;
            Item.useAmmo = AmmoID.Bullet;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlasmaBlast>(), 50);
            recipe.AddIngredient(ModContent.ItemType<SpaceDust>(), 5);
            recipe.AddIngredient(ItemID.PhoenixBlaster, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
