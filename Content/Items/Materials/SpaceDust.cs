using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace EventHorizons.Content.Items.Materials //To be changed to fit actual path
{
    public class SpaceDust : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 50;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Purple;
            Item.value = 5;
            Item.maxStack = Item.CommonMaxStack;
        }
    }
}