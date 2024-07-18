using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using EventHorizons.Tiles;
using Microsoft.Xna.Framework;

using Terraria.DataStructures;

using EventHorizons.Items.Placeables;


namespace EventHorizons.Items.Placeables
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

            Item.createTile = ModContent.TileType<Tiles.Galvanitebar>();
            Item.placeStyle = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Galvaniteore>(), 4);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}
   


