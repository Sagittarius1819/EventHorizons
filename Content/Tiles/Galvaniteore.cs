using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EventHorizons.Content.Tiles
{
    public class Galvaniteore : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 900;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 350;

            AddMapEntry(new Color(36, 48, 181), CreateMapEntryName());

            DustType = DustID.Cobalt;

            HitSound = SoundID.Tink;

            MineResist = 1.5f;
            MinPick = 60;



        }
    }
}
