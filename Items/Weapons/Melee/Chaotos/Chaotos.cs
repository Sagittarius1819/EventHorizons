using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EventHorizons.Projectiles;
using EventHorizons.Items.Placeables;
using EventHorizons.Items.Materials;
using System;

namespace EventHorizons.Items.Weapons.Melee.Chaotos
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
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item169;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<Chaos>();
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
        public override void RightClick(Player player)
        {
            // Check if the item is off cooldown
            if (player.altFunctionUse == 2) // Right-click
            {
                // Cooldown logic
                if (player.itemTime == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        // Calculate spread angle
                        float angle = ToRadians(10 * (i - 1)); // Spread of 20 degrees

                        // Create a new projectile
                        Projectile.NewProjectile(
                            player.GetSource_ItemUse(Item), // Source of the projectile
                            player.Center, // Start position
                            new Vector2(10f, 0f).RotatedBy(angle), // Velocity
                            ProjectileID.Bullet, // Projectile type (replace with your projectile)
                            Item.damage, // Damage
                            Item.knockBack, // Knockback
                            player.whoAmI // Owner
                        );
                    }
                    player.itemTime = Cooldown; // Set cooldown
                    player.itemAnimation = Cooldown; // Sync animation with cooldown
                    return;
                }
            }
            return; // Base behavior
        }
        private const int Cooldown = 60;
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
            player.itemLocation.X = player.MountedCenter.X + player.width / 2 * player.direction;
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