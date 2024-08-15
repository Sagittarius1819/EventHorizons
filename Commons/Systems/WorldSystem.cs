using Terraria.WorldBuilding;
using System.Collections.Generic;
using EventHorizons.Commons.Systems.Genpasses;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria.ID;
using EventHorizons.Content.Tiles;
using Steamworks;
using FullSerializer.Internal;
using System;
using Microsoft.Build.Tasks;

namespace TutorialMod.Common.Systems
{
    internal class WorldSystem : ModSystem
    {
        public static bool JustPressed(Keys key) {
			return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
		}

		public override void PostUpdateWorld() {
			if (JustPressed(Keys.D1))
				TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
		}

		private void TestMethod(int x, int y) {
			Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, Color.YellowGreen, null);

            // Code to test placed here:
            //GenerateCrystallineCave(x, y);

            //Tunnel placed in middle
            //CreateEllipse(x, y, 45, 30, 2, ModContent.TileType<CrystallineStoneTile>());
            CreateSpikesAroundEllipse(x, y, 40, 50, 4f, 20, 30, ModContent.TileType<CrystallineStoneTile>());
            //GenerateSpike(x, y, 30, -.7f, -.3f, ModContent.TileType<CrystallineStoneTile>());

        }

        private void GenerateCrystallineCave(int x, int y)
        {

        }

        private void GenerateSpike(int x, int y, int length, float slope, float downSlope, int type)
        {
            //Always spawns on the left most block, spikes on the left side will need the difference added
            //left spikes look best with a small upslope and large down slope(When speaking about the absolute value of the slope)
            //Be very careful using a larger upslope, does not generate correctly(This applies to the absolute value)
            if (Math.Abs(slope) > Math.Abs(downSlope)) {
                throw new Exception("The absolute value of the downslope may not be greater than the upslope");
            }
            int helfLength = length / 2;
            if (slope > 0)
            {
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
            } else
            {
                for (int i = 0; i < helfLength; i++)
                {
                    int topY = (int)(i * -slope);
                    for (int j = 0; j < topY; j++)
                    {
                        WorldGen.KillTile(i + x, y + j - topY);
                        WorldGen.PlaceTile(i + x, y + j - topY, type);
                    }
                }

                for (int i = 0; i < helfLength; i++)
                {
                    int topY = (int)(i * downSlope) + (int)((length / 2) * -slope);
                    for (int j = 0; j < topY; j++)
                    {
                        WorldGen.KillTile(i + x + helfLength,y + j - topY);
                        WorldGen.PlaceTile(i + x + helfLength, y + j - topY, type);
                    }
                }
            }
            
        }

        private void CreateEllipse(int x, int y, int verticalRadius, int horizontalRadius, int thickness, int type)
        {
            for (int i = 0; i < horizontalRadius; i++)
            {
                int topY = (int)Math.Sqrt(Math.Pow(verticalRadius, 2) - ((Math.Pow(i, 2) * Math.Pow(verticalRadius, 2)) / Math.Pow(horizontalRadius, 2)));
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

        private void CreateSpikesAroundEllipse(int x, int y, int verticalRadius, int horizontalRadius, float estimatedAmount, int minLength, int MaxLength, int type)
        {
            //GenerateSpike(x, y + verticalRadius, WorldGen.genRand.Next(minLength, MaxLength), 2, 4, type);
            //GenerateSpike(x, y - verticalRadius, WorldGen.genRand.Next(minLength, MaxLength), -2, -4, type);

            //GenerateSpike(x + horizontalRadius / 2, y + verticalRadius, WorldGen.genRand.Next(minLength * 4, MaxLength * 4), .5f, 4, type);
            //GenerateSpike(x + horizontalRadius / 2, y - verticalRadius, WorldGen.genRand.Next(minLength * 4, MaxLength * 4), -.5f, -4, type);
            for (int i = 0; i < horizontalRadius; i++)
            {
                int topY = (int)Math.Sqrt(Math.Pow(verticalRadius, 2) - ((Math.Pow(i, 2) * Math.Pow(verticalRadius, 2)) / Math.Pow(horizontalRadius, 2)));
                WorldGen.KillTile(i + x, topY + y);
                WorldGen.PlaceTile(i + x, topY + y, type);

                WorldGen.KillTile(i + x, y - topY);
                WorldGen.PlaceTile(i + x, y - topY, type);

                WorldGen.KillTile(x - i, topY + y);
                WorldGen.PlaceTile(x - i, topY + y, type);

                WorldGen.KillTile(x - i, y - topY);
                WorldGen.PlaceTile(x - i, y - topY, type);

            }
            //float chance = estimatedAmount / (float) horizontalRadius;
            //for (int i = 0; i < horizontalRadius; i++)
            //{
            //    int topY = (int)Math.Sqrt(Math.Pow(verticalRadius, 2) - ((Math.Pow(i, 2) * Math.Pow(verticalRadius, 2)) / Math.Pow(horizontalRadius, 2)));
            //    for (int j = 0; j < (topY + y) - (y - topY); j++)
            //    {

            //        if (WorldGen.genRand.NextFloat() < chance)
            //        {

            //            GenerateSpike(x + i, y + topY, WorldGen.genRand.Next(minLength, MaxLength), WorldGen.genRand.Next(1, 5), WorldGen.genRand.Next(5, 10), type);
            //        }

            //    }

            //}


        }

        
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {

            
            int shiniesIndex = tasks.FindIndex(t => t.Name.Equals("Shinies"));
            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, new GalvaniteoreGenPass("Galvanite Ore Pass", 320f));
                tasks.Insert(shiniesIndex + 2, new CrystallineCavernsGenPass("Creating Crystalline Caverns", 100f));
            }
        }
    }
}