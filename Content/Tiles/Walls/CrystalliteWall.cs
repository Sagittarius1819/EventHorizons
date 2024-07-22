
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EventHorizons.Content.Tiles.Walls
{
    public class CrystalliteWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            DustType = DustID.FartInAJar;

            AddMapEntry(new Color(150, 150, 150));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}