﻿using System;
using EventHorizons.Tiles;
using Terraria.ModLoader;

namespace EventHorizons.Biomes
{
	public class CrystallineCavernsTileCount : ModSystem
	{
		public int CrystallineStoneCount;

		public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
		{
			CrystallineStoneCount = tileCounts[ModContent.TileType<CrystallineStone>()];
		}
	}
}