using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EventHorizons.Projectiles;
using EventHorizons.Items.Placeables;
using EventHorizons.Items.Materials;
using System;

namespace EventHorizons.Items.Weapons
{
    public class Chaotos : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.EventHorizons.hjson file.


        public override void SetDefaults()
        {
            Item.damage = 78;
            Item.DamageType = DamageClass.Melee;
            Item.width = 74;
            Item.height = 74;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Chaos>();
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
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Add custom effects when the sword is swung, for example, creating dust particles
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Skyware);
            }
        }
        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            // Customize the hold style here, for example, making the player glow while holding the sword
            player.itemLocation.X = player.MountedCenter.X + (float)((player.width / 2) * player.direction);
            player.itemLocation.Y = player.MountedCenter.Y;
            Lighting.AddLight(player.itemLocation, 0.5f, 0.5f, 0.5f); // Add light around the player
        }
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        // Custom use style logic for spinning the sword
        if (player.itemAnimation > 0)
        {
            // Calculate the rotation angle
            float rotationAngle = 2 * -(float)Math.PI * (player.itemAnimation / (float)player.itemAnimationMax);

            // Set the rotation to the calculated angle
            player.itemRotation = rotationAngle * player.direction;

                // Set the sword's position relative to the player's hand position
                player.itemLocation = player.MountedCenter;
        }
    }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 14);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.BreakerBlade, 1);
            recipe.AddIngredient(ModContent.ItemType<Galvanitebar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<SpaceDust>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.AdamantiteBar, 14);
            recipe2.AddIngredient(ItemID.FallenStar, 5);
            recipe2.AddIngredient(ItemID.BreakerBlade, 1);
            recipe2.AddIngredient(ModContent.ItemType<Galvanitebar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<SpaceDust>(), 5);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.Register();
        }
    }
}