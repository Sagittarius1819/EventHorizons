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

        }

        private void GenerateSpike(int x, int y, int length, float slope, float downSlope, int type)
        {
            if (slope > 0)
            {
                int helfLength = length / 2;
                for (int i = 0; i < helfLength; i++)
                {
                    int topY = (int)(i * slope);
                    for (int j = 0; j < topY; j++)
                    {
                        WorldGen.KillTile(i + x, topY + y - j);
                        WorldGen.PlaceTile(i + x, topY + y - j, type);
                    }
                }

                for (int i = 0; i < helfLength; i++)
                {
                    int topY = (int)(i * -downSlope) + (int)((length / 2) * slope);
                    for (int j = 0; j < topY; j++)
                    {
                        WorldGen.KillTile(i + x + helfLength, topY + y - j);
                        WorldGen.PlaceTile(i + x + helfLength, topY + y - j, type);
                    }
                }
            }

        }

        private void CreateEllipse(int x, int y, int verticalRadius, int horizontalRadius, int thickness, int type)
        {
            for (int i = 0; i < horizontalRadius; i++)
            {
                int topY = (int)Math.Sqrt(Math.Pow(verticalRadius, 2) - ((Math.Pow(i, 2) * Math.Pow(verticalRadius, 2)) / Math.Pow(horizontalRadius, 2)));
                Main.NewText("x: " + i + ", y:" + topY);
                for (int j = 0; j < (topY + y) - (y - topY); j++)
                {
                    if (Main.tile[i + x, topY + y - j].TileType == TileID.Stone)
                    {
                        WorldGen.KillTile(i + x, topY + y - j);
                        WorldGen.PlaceTile(i + x, topY + y - j, type);
                    }
                    if (Main.tile[x - i, topY + y - j].TileType == TileID.Stone)
                    {
                        WorldGen.KillTile(x - i, topY + y - j);
                        WorldGen.PlaceTile(x - i, topY + y - j, type);
                    }

                }

                for (int j = 0; j < thickness; j++)
                {
                    WorldGen.KillTile(i + x, topY + y - j);
                    WorldGen.PlaceTile(i + x, topY + y - j, type);

                    WorldGen.KillTile(x - i, topY + y - j);
                    WorldGen.PlaceTile(x - i, topY + y - j, type);

                    WorldGen.KillTile(i + x, y - topY + j);
                    WorldGen.PlaceTile(i + x, y - topY + j, type);

                    WorldGen.KillTile(x - i, y - topY + j);
                    WorldGen.PlaceTile(x - i, y - topY + j, type);
                }




            }

        }
    }


}