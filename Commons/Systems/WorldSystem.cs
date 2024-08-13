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
            GenerateCrystallineCave(x, y);

            //Tunnel placed in middle


        }

        private void GenerateCrystallineCave(int x, int y)
        {
            CreateCrystallineStone(x, y, 100, 65, 2);
            CreateCrystallineStone(x + 20, y + 5, 100, 65, 2);
            WorldGen.digTunnel(x + 20 + WorldGen.genRand.Next(30, 35), y + 5 + WorldGen.genRand.Next(40, 60), 0, 0, 1, 30);
            WorldGen.digTunnel(x + WorldGen.genRand.Next(30, 35), y + WorldGen.genRand.Next(40, 60), 0, 0, 1, 30);
        }

        private void CreateCrystallineStone(int x, int y, int height, int width, int slope)
        {
            //There isn't a nice way to put this; this method is shit
            int center = x + width / 2;
            for (int i = 0; i < width; i += slope) {
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