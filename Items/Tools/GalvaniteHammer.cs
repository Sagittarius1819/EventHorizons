using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria;
using EventHorizons.Items.Placeables;


namespace EventHorizons.Items.Tools
{
    internal class GalvaniteHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.autoReuse = true;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 38;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(silver: 35);
            Item.rare = ItemRarityID.Orange;
            Item.hammer = 70;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 10);
            recipe.AddIngredient(ModContent.ItemType<Galvanitebar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
