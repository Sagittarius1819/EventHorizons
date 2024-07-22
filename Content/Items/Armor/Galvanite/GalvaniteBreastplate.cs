using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using EventHorizons.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EventHorizons.Content.Projectiles;
using EventHorizons.Content.Items.Placeables.Ores;

namespace EventHorizons.Content.Items.Armor.Galvanite
{
    [AutoloadEquip(EquipType.Body)]
    public class GalvaniteBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 8; // The amount of defense the item will give when equipped
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Galvanitebar>(), 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {

            Player.jumpHeight = 10;
            Player.jumpSpeed = 10;
            player.statManaMax2 += 20;
        }
    }
}
