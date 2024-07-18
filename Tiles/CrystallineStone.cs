using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EventHorizons.Tiles
{
    public class CrystallineStone : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = false;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileShine2[Type] = false;
            Main.tileSpelunker[Type] = false;
            Main.tileBrick[Type] = true;

            AddMapEntry(new Color(27, 27, 55), CreateMapEntryName());

            DustType = DustID.Corruption;

            HitSound = SoundID.DD2_WitherBeastCrystalImpact;

            MineResist = 1.5f;
            MinPick = 55;



        }
    }
}
