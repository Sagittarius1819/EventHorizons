using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;

namespace EventHorizons.Content.Tiles.EvolutionTable
{ //Evolution table is a work bench. Right click it to reveal 4 slots in a square on one side for inputs, and one button that crafts.
    internal class EvolutionTableS1 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileTable[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(200, 200, 200));
        }

        public override bool RightClick(int i, int j)
        {
            if (Main.myPlayer == Main.LocalPlayer.whoAmI)
            {
                // Debug message to verify if RightClick is being called
                Main.NewText("RightClick detected", 255, 255, 0);

                // Open the custom UI
                ModContent.GetInstance<EventHorizons>().OpenEvolutionTableS1UI();
            }
            return true; // Return true to indicate that the right-click action was handled
        }
    }
}
