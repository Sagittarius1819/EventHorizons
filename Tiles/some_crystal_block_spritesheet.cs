using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EventHorizons.Tiles
{
    public class some_crystal_block_spritesheet : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = false;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = false;
            Main.tileShine[Type] = 900;
            Main.tileLighted[Type] = true;
            Main.tileBrick[Type] = true;


            AddMapEntry(new Color(204, 0, 204), CreateMapEntryName());


            DustType = DustID.Corruption;

            HitSound = SoundID.DD2_WitherBeastCrystalImpact;

            MineResist = 1.5f;
            MinPick = 55;



        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.852f;
            g = 0.52f;
            b = 0.8f;
        }

    }
}
