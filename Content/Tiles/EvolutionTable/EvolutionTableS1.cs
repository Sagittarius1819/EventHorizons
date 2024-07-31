using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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
                var system = ModContent.GetInstance<EvolutionTableUISystem>();
                system.TilePosition = new Vector2(i, j);
                system.Toggle();
            }
            return true;
        }
    }
}
