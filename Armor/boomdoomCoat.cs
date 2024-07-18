using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace EventHorizons.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class boomdoomCoat : ModItem
	{
		public override void Load() {
			if (Main.netMode == NetmodeID.Server) {
				return;
			}
		}

        public override void SetStaticDefaults() {

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults() {
			Item.width = 18;
			Item.height = 14;
			Item.rare = ItemRarityID.Master;
			Item.vanity = true;
            Item.defense = 3;
		}
        
	}
}
