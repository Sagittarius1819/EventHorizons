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
            GenerateSpike(x, y, 20, .5f, .4f, ModContent.TileType<CrystallineStoneTile>());

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