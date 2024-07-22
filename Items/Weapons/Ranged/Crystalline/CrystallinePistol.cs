using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using EventHorizons.Core.Interfaces;
using EventHorizons.Assets.Textures;
using EventHorizons.Items.Materials;
using EventHorizons.Items.Placeables;

namespace EventHorizons.Items.Weapons.Ranged.Crystalline
{
    internal class CrystallinePistol : ModItem, IGlowmaskItem
    {
        public Color GlowEmitColor => Color.Violet;

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.width = 40;
            Item.height = 33;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.useTurn = false;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<CrystallinePistolHoldout>();
            Item.shootSpeed = 0.1f;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.useAmmo = AmmoID.Bullet;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player) => false;

        public override bool CanUseItem(Player player) => !Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == ProjectileType<CrystallinePistolHoldout>());

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.MountedCenter, velocity, ProjectileType<CrystallinePistolHoldout>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes() => CreateRecipe().AddIngredient<CrystallineStone>(30).AddIngredient<Crystallite>(25).AddTile(TileID.Anvils).Register();
    }

    public class CrystallinePistolHoldout : ModProjectile
    {
        public override string Texture => TryGetTextureFromOther<CrystallinePistol>();

        public ref float Charge => ref Projectile.ai[0];

        public ref float KillTimer => ref Projectile.ai[2];

        public bool Dying = false;

        public ref float RotRand => ref Projectile.ai[1];

        private Player Owner => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 16;
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
            Vector2 tipPos = armPos + Projectile.velocity * Projectile.width * 0.95f + new Vector2(0f, -7f);
            Vector2 tipPos2 = armPos + Projectile.velocity * Projectile.width * 1.15f + new Vector2(0f, -7f);

            Vector2 velocity = NormalizeBetter(Projectile.velocity) * 15f;

            ManageHeldProj(armPos);
            if (Owner.channel && !Dying)
            {
                Projectile.timeLeft++;

                if (++Charge >= 13f && Charge < 14f) //fire, also set the random rotation
                {
                    CheckAmmo();

                    int type = ProjectileID.Bullet;
                    Owner.PickAmmo(Owner.HeldItem, out type, out float spd, out int dmg, out float kb, out int _);

                    if (type == ProjectileID.Bullet) //musket balls => crystal shards
                    {
                        type = ProjectileID.CrystalBullet; //for now
                    }

                    RotRand = RandomInRange(Pi / 8, Pi / 10, Pi / 8.2f) * (Projectile.direction == 1 ? -1f : 1f);

                    //fire after setting rotation so we fire in the right direction
                    SoundEngine.PlaySound(SoundID.Item40 with { Pitch = 0.1f, Volume = 0.7f });

                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 vel = velocity.RotatedByRandom(Pi / 7f);

                        var d = Dust.NewDustDirect(tipPos, 0, 0, DustID.GemAmethyst);
                        d.velocity = vel * Main.rand.NextFloat(0.25f, 0.32f);
                        d.noGravity = true;
                        d.scale = Main.rand.NextFloat(1.4f, 1.9f);
                    }

                    Lighting.AddLight(tipPos2, Color.Lerp(Color.Purple, Color.MediumVioletRed, 0.2f).ToVector3() * 0.5f);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), tipPos, velocity, type, Projectile.damage, 3f, Projectile.owner);
                }

                if (Charge >= 14f)
                    RotRand = Lerp(RotRand, 0f, (Charge - 14) / 6);

                if (Charge >= 20f)
                {
                    Charge = 0;
                }
            }

            else
            {
                Dying = true;
                RotRand = 0f;
                Charge = 0f;

                if (++KillTimer >= 20)
                    Projectile.Kill();
            }

        }

        void CheckAmmo()
        {
            bool success = false;

            for (int j = 0; j < 58; j++)
            {
                var item = Owner.inventory[j];

                if (item.ammo == Owner.HeldItem.useAmmo && item.stack > 0)
                {
                    if (item.maxStack > 1) //dont consume things like pouches
                        item.stack--;

                    success = true;
                    break;
                }
            }

            if (!success)
            {
                Projectile.Kill();
                return;
            }
        }

        void ManageHeldProj(Vector2 armPos)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                float interpolant = Utils.GetLerpValue(5f, 55f, Projectile.Distance(Main.MouseWorld), true);
                Vector2 oldVelocity = Projectile.velocity;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.MouseWorld), interpolant).RotatedBy(RotRand);

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
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D flash = TextureRegistry.MuzzleFlash.Value;

            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            int frameHeightM = flash.Height / Main.projFrames[Projectile.type];
            int startYM = frameHeightM * Projectile.frame;

            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Rectangle sourceRectangleM = new(0, startYM, flash.Width, frameHeightM);

            Vector2 origin = sourceRectangle.Size() / 2f;
            Vector2 originM = sourceRectangleM.Size() / 2f;

            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            Vector2 muzzleFlashPos = drawPos + (Projectile.velocity * Projectile.width * 0.84f + new Vector2(0f, -7f));
            Vector2 muzzleFlashPos2 = drawPos + (Projectile.velocity * Projectile.width * 0.7f + new Vector2(0f, -7f));

            Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            if (Charge >= 12 && Charge <= 13)
            {
                Main.EntitySpriteDraw(flash, muzzleFlashPos, sourceRectangleM, Color.Lerp(Color.MediumPurple, Color.MediumVioletRed, 0.4f) with { A = 0 }, Projectile.rotation, originM, Projectile.scale * 0.16f * new Vector2(0.9f, 0.5f) * Lerp(1f, 0f, (Charge - 12) / 2), spriteEffects, 0);

                Main.spriteBatch.Reload(BlendState.Additive);

                Main.EntitySpriteDraw(flash, muzzleFlashPos2, sourceRectangleM, Color.Lerp(Color.MediumVioletRed, Color.White, 0.8f), Projectile.rotation, originM, Projectile.scale * 0.12f * new Vector2(0.87f, 0.5f) * Lerp(1f, 0f, (Charge - 12) / 2), spriteEffects, 0);

                Main.spriteBatch.Reload(BlendState.AlphaBlend);
            }

            return false;
        }

        public override void PostDraw(Color lightColor) => Projectile.SimpleDrawProjectile(Request<Texture2D>(Texture + "_Glow").Value, Color.White, true, 1f);

        #endregion
    }
}
