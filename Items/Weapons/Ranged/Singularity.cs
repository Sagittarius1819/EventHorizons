using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using EventHorizons.Items.Placeables;
using EventHorizons.Items.Materials;
using EventHorizons.Projectiles.Weapons;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.CameraModifiers;


namespace EventHorizons.Items.Weapons.Ranged
{
    internal class Singularity : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.useTime = 4;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 20;
            Item.knockBack = 30;
            Item.noMelee = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 6f;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item11 with { MaxInstances = 25 };
        }
        public override bool? UseItem(Player player)
        {


            PunchCameraModifier modifier = new(player.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 1f, 1f, 5, 100f, FullName);
            Main.instance.CameraModifiers.Add(modifier);
            return base.UseItem(player);


        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 2;

            for (int i = 0; i < NumProjectiles; i++)
            {
                player.velocity = player.DirectionTo(Main.MouseWorld) * -1f;
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SpaceDust>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Quazar>(), 1);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.VenusMagnum, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
