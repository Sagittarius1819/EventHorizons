using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizons.Content.Items.Placeables.CrystallineCavernsbiome;
using EventHorizons.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace EventHorizons.Commons.Systems.Genpasses
{
    public class CrystallineCavernsGenPass : GenPass
    {
        public CrystallineCavernsGenPass(string name, double loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
			progress.Message = "Creating Crystalline Caverns";

			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++) {
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY);

				//WorldGen.digTunnel(x, y, 0, 0, 1, 75, false);
                //WorldGen.TileRunner(x, y, WorldGen.genRand.Next(80, 100), WorldGen.genRand.Next(2, 6), ModContent.TileType<CrystallineStoneTile>());

                //Code above is broken for now. The tunnel does not generate inside the new stone tiles. It also needs to be made way less common, but this is ok for testing
			}
        }
    }
}