using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
namespace EventHorizons.Content.Items.Materials
{
    public class CrystallineCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.value = 5;
            Item.maxStack = Item.CommonMaxStack;
            Item.UseSound = SoundID.NPCDeath7;
            Item.useStyle = 4;
        }
    }
}