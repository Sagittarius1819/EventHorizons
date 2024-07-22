using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace EventHorizons.Content.Items.Weapons.Melee.BoomDoomDevSet
{
    public class Elucidator : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.width = 60;
            Item.height = 10000;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 10;
            Item.value = Item.buyPrice(gold: 50);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 25;
            Item.useStyle = ItemUseStyleID.Swing;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 300);
        }

    }
}
