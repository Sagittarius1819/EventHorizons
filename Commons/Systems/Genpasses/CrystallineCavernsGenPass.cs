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

                GenerateCrystallineCave(x, y);

            }
        }

        private void GenerateCrystallineCave(int x, int y)
        {
            CreateCrystallineStone(x, y, 100, 65, 2);
            CreateCrystallineStone(x + 20, y + 5, 100, 65, 2);
            WorldGen.digTunnel(x + +50, y + 55, 0, 0, 1, 30);
            WorldGen.digTunnel(x + 30, y + 50, 0, 0, 1, 30);
            int topSpikeSize = WorldGen.genRand.Next(-2, 0);
            if (topSpikeSize == -1)
            {
                GenerateSpike(x + 20, y + 17, WorldGen.genRand.Next(10, 15), topSpikeSize, ModContent.TileType<some_crystal_block_spritesheet>());
            }
            else
            {
                GenerateSpike(x + 20, y + 25, WorldGen.genRand.Next(10, 15), topSpikeSize, ModContent.TileType<some_crystal_block_spritesheet>());
            }
            GenerateSpike(x + 45, y + 50, WorldGen.genRand.Next(20, 25), (float)WorldGen.genRand.NextDouble(), ModContent.TileType<some_crystal_block_spritesheet>());
            if (WorldGen.genRand.NextBool())
            {
                GenerateSpike(x + 30, y + 65, 20, .25f, ModContent.TileType<some_crystal_block_spritesheet>());
            }
            GenerateSpike(x + 30, y, WorldGen.genRand.Next(15, 20), WorldGen.genRand.Next(-4, 0), ModContent.TileType<CrystallineStoneTile>());
            GenerateSpike(x + 20, y + 80, WorldGen.genRand.Next(15, 20), WorldGen.genRand.Next(2, 5), ModContent.TileType<CrystallineStoneTile>());
        }

        private void GenerateSpike(int x, int y, int length, float slope, int type)
        {
            if (length > 0 && slope > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < i * slope; j++)
                    {
                        WorldGen.KillTile(i + x, y + j + i);
                        WorldGen.PlaceTile(i + x, y + j + i, type);
                    }
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < i * -slope; j++)
                    {
                        WorldGen.KillTile(i + x, y - j + i);
                        WorldGen.PlaceTile(i + x, y - j + i, type);
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