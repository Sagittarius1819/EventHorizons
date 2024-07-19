using EventHorizons.Enemies;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EventHorizons.Items.Critters
{
    public class CrystalliteCritter : ModItem
	{


		public override void SetDefaults()
		{
			Item.width = Item.height = 32;
			Item.rare = ItemRarityID.White;
			Item.maxStack = 99;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.useTime = Item.useAnimation = 20;
			Item.useAnimation = 25;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.bait = 35;
			Item.noMelee = true;
			Item.consumable = true;
            Item.makeNPC = ModContent.NPCType<CrystalliteSlimeling>();

        }
	}
}