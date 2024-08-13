using EventHorizons.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace EventHorizons.Content.Tiles.Rubbles
{

    public abstract class OBOCrystal : ModTile
    {
        // We want both tiles to use the same texture
        public override string Texture => "EventHorizons/Content/Tiles/Rubbles/Crystal1x1";

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileObsidianKill[Type] = true;

            DustType = ModContent.DustType<CrystalStoneDust>();

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(15, 0, 55));
        }
    }
    // this is placed during world generation in the RubbleWorldGen class.
    public class Crystal1x1 : OBOCrystal
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            TileObjectData.GetTileData(Type, 0).LavaDeath = false;
        }

       /* public override void DropCritterChance(int i, int j, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
        {
            wormChance = 8;a
        } */
    }
}