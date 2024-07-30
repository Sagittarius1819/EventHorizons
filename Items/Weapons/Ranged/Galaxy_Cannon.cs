using EventHorizons.Assets.Textures;
using EventHorizons.Core.Base;
using EventHorizons.Core.Interfaces;
using EventHorizons.Items.Placeables;
using EventHorizons.Projectiles.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.GameContent.Animations.IL_Actions.Sprites;

namespace EventHorizons.Items.Weapons.Ranged
{
    /*
    public class Galaxy_Cannon : ModItem
    {
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 44; // Hitbox width of the item.
            Item.height = 18; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 100; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 100; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
            Item.UseSound = SoundID.Item36; // The sound that this item plays when used.

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 86; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ModContent.ProjectileType<Superwaves>(); // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 10f; // The speed of the projectile (measured in pixels per frame.)
                                   // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }


        public override bool? UseItem(Player player)
        {
            SoundStyle usesound = new("EventHorizons/Assets/Sounds/GalacticBeep");



            PunchCameraModifier modifier = new(player.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
            Main.instance.CameraModifiers.Add(modifier);
            SoundEngine.PlaySound(usesound);
            return base.UseItem(player);

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.velocity = player.DirectionTo(Main.MouseWorld) * -10f;
            const int NumProjectiles = 8; // The humber of projectiles that this gun will shoot.

            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

            }

            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.


        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, -2f);
        }
    } */

    internal class Galaxy_Cannon : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.width = 60;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.useTurn = false;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<GalaxyCannonHoldout>();
            Item.shootSpeed = 0.1f;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.useAmmo = AmmoID.Bullet;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player) => false;

        public override bool CanUseItem(Player player) => !Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == ProjectileType<GalaxyCannonHoldout>());

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.MountedCenter, velocity, ProjectileType<GalaxyCannonHoldout>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes() => CreateRecipe().AddIngredient<CrystallineStone>(30).AddIngredient<Crystallite>(25).AddTile(TileID.Anvils).Register();
    }

    public class GalaxyCannonHoldout : ModProjectile
    {
        public override string Texture => TryGetTextureFromOther<Galaxy_Cannon>();

        public ref float Charge => ref Projectile.ai[0];

        public ref float KillTimer => ref Projectile.ai[2];

        public bool Firing = false;
        public bool HasFired = false;

        private Player Owner => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 42;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.netImportant = true;
        }

        public override void AI()
        {
            if (Projectile.owner != Main.myPlayer)
                return;

            if (Owner.dead || !Owner.active || Owner.noItems || Owner.CCed || !Owner.HasAmmo(Owner.HeldItem))
                Projectile.Kill();

            Projectile.damage = Owner.GetWeaponDamage(Owner.HeldItem);
            Projectile.CritChance = Owner.GetWeaponCrit(Owner.HeldItem);
            Projectile.knockBack = Owner.GetWeaponKnockback(Owner.HeldItem, Owner.HeldItem.knockBack);

            Vector2 armPos = Owner.RotatedRelativePoint(Owner.MountedCenter, false, true);
            Vector2 tipPos = armPos + Projectile.velocity * Projectile.width * 0.95f;
            Vector2 tipPos2 = armPos + Projectile.velocity * Projectile.width * 1.15f;

            Vector2 velocity = NormalizeBetter(Projectile.velocity) * 15f;

            ManageHeldProj(armPos);
            if (Owner.channel && !Firing)
            {
                Projectile.timeLeft++;

                if (++Charge < 181)
                    Charge++;

                switch (Charge)
                {
                    case 60:
                        {

                        }
                        break;
                    case 120:
                        {

                        }
                        break;
                    case 180:
                        {

                        }
                        break;
                }
            }

            else
            {
                Firing = true;

                if (!HasFired)
                {
                    int damage = (int)((Projectile.damage / 4) + (Charge * 0.75f));

                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), tipPos, velocity, ProjectileType<GalaxyLaser>(), damage, 3f, Projectile.owner, ai2: Charge);

                    HasFired = true;
                }

                else
                {
                    if (++KillTimer >= 20)
                        Projectile.Kill();
                }
            }
        }

        void ManageHeldProj(Vector2 armPos)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                float interpolant = Utils.GetLerpValue(5f, 55f, Projectile.Distance(Main.MouseWorld), true);
                Vector2 oldVelocity = Projectile.velocity;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.MouseWorld), interpolant);

                if (Projectile.velocity != oldVelocity)
                {
                    Projectile.netSpam = 0;
                    Projectile.netUpdate = true;
                }
            }

            Projectile.Center = armPos + Projectile.velocity * 14f;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == -1 ? Pi : 0f);
            Projectile.spriteDirection = Projectile.direction;

            Owner.ChangeDir(Projectile.direction);
            Owner.heldProj = Projectile.whoAmI;
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;
            Owner.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
        }

        #region drawing
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.SimpleDrawProjectile(TextureAssets.Projectile[Type].Value, Color.White, true, 1f);

            return false;
        }

        //public override void PostDraw(Color lightColor) => Projectile.SimpleDrawProjectile(Request<Texture2D>(Texture + "_Glow").Value, Color.White, true, 1f);

        #endregion
    }

    class GalaxyLaser : BaseRay
    {
        public GalaxyLaser() : base(30, 2000, Color.Lerp(Color.Violet, Color.Cyan, 0.1f), 4f, 10) { }

        public override Texture2D StartTexture => TextureRegistry.BeamStart.Value;
        public override Texture2D MiddleTexture => TextureRegistry.BeamMiddle.Value;
        public override Texture2D EndTexture => TextureRegistry.BeamEnd.Value;

        public override string Texture => TryGetTextureFromOther<Galaxy_Cannon>(); //this isnt drawn, dw

        public override float MaxLaserScale => 1f + (Projectile.ai[2] / 90);

        public override void ManageScale() => Projectile.scale = Projectile.timeLeft / MaxTime * MaxLaserScale;
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 10;
            Projectile.timeLeft = MaxTime;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void ExtraAI()
        {
            
        }
    }
}