using System;
using EventHorizons.Content.Tiles;
using Terraria.ModLoader;

namespace EventHorizons.Content.Biomes
{
    public class CrystallineCavernsTileCount : ModSystem
    {
        public int CrystallineStoneCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            CrystallineStoneCount = tileCounts[TileType<CrystallineStoneTile>()];
        }
    }
}