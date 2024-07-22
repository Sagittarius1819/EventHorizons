using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace EventHorizons.Content.Items.Placeables.Ores
{
    public class Galvanitebar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            ItemID.Sets.SortingPriorityMaterials[Type] = 59;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 2, copper: 75);
            Item.autoReuse = true;
            Item.rare = 4;
            Item.useAnimation = 10;
            Item.useStyle = 1;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.maxStack = 9999;
            Item.createTile = TileType<EventHorizons.Content.Tiles.Galvanitebar>();
            Item.placeStyle = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Galvaniteore>(), 4);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}



