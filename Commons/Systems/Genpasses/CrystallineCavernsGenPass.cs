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

			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 10E-07); k++) {
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow + 300, Main.maxTilesY);

                

            }
        }

        private void GenerateCrystallineCave(int x, int y)
        {
            CreateCrystallineStone(x, y, 100, 65, 2);
            CreateCrystallineStone(x + 20, y + 5, 100, 65, 2);
            WorldGen.digTunnel(x + 20 + WorldGen.genRand.Next(30, 35), y + 5 + WorldGen.genRand.Next(40, 60), 0, 0, 1, 30);
            WorldGen.digTunnel(x + WorldGen.genRand.Next(30, 35), y + WorldGen.genRand.Next(40, 60), 0, 0, 1, 30);
        }

        private void GenerateSpike(int x, int y, int length, int height, int type)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (j > i)
                    {
                        WorldGen.KillTile(j, i);
                        WorldGen.PlaceTile(j, i, type);
                    }
                }
            }
        }

        private void CreateCrystallineStone(int x, int y, int height, int width, int slope)
        {
            //There isn't a nice way to put this; this method is shit
            int center = x + width / 2;
            for (int i = 0; i < width; i += slope)
            {
                for (int j = 0; j < i; j++)
                {
                    for (int k = 0; k < slope; k++)
                    {
                        WorldGen.KillTile(center + j, y + i + k);
                        WorldGen.KillTile(center - j, y + i + k);
                        WorldGen.PlaceTile(center + j, y + i + k, ModContent.TileType<CrystallineStoneTile>());
                        WorldGen.PlaceTile(center - j, y + i + k, ModContent.TileType<CrystallineStoneTile>());
                    }

                }

            }

            for (int i = width; i < height - width + 2; i++)
            {
                for (int j = 1; j < width * 2; j++)
                {
                    WorldGen.KillTile(x - (width / 2) + j, y + i);
                    WorldGen.PlaceTile(x - (width / 2) + j, y + i, ModContent.TileType<CrystallineStoneTile>());
                }
            }

            for (int i = 0; i < width; i += slope)
            {
                for (int j = 0; j < i; j++)
                {
                    for (int k = 0; k < slope; k++)
                    {
                        WorldGen.KillTile(center + j, y + height - i + k);
                        WorldGen.KillTile(center - j, y + height - i + k);
                        WorldGen.PlaceTile(center + j, y + height - i + k, ModContent.TileType<CrystallineStoneTile>());
                        WorldGen.PlaceTile(center - j, y + height - i + k, ModContent.TileType<CrystallineStoneTile>());
                    }

                }

            }
        }
    }


}