﻿using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using EventHorizons.Content.Items.Materials;
using EventHorizons.Content.Items.Placeables.Ores;

namespace EventHorizons.Content.Items.Weapons.Ranged
{
    internal class Quazar : ModItem
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
            Item.damage = 48;
            Item.knockBack = 30;
            Item.UseSound = SoundID.Item96;
            Item.noMelee = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 6f;
            Item.useAmmo = AmmoID.Bullet;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<SpaceDust>(), 5);
            recipe.AddIngredient(ItemType<PhotonicBoom>(), 1);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemType<Galvanitebar>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
