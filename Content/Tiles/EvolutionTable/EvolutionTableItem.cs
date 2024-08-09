using EventHorizons.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace EventHorizons.Content.Tiles.EvolutionTable
{
    public class EvolutionTableItem : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.value = 100;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<EvolutionTableS1>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 25)
                .AddIngredient(ItemID.Diamond, 2)
                .AddIngredient(ItemID.BlueTorch, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 25)
                .AddIngredient(ItemID.Diamond, 2)
                .AddIngredient(ItemID.BlueTorch, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}