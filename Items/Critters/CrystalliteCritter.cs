using Microsoft.Xna.Framework;
using EventHorizons.Enemies;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EventHorizons.Enemies;
using EventHorizons.Items.Materials;
using Terraria.Audio;

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
			Item.bait = 35;
			Item.noMelee = true;
			Item.consumable = true;
			Item.autoReuse = true;

		}
        public override void OnConsumeItem(Player player)
        {
           
        }
    }
}