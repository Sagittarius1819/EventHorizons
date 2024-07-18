using EventHorizons.Tiles;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace EventHorizons.Commons.Systems.Genpasses
{
    internal class GalvaniteoreGenPass : GenPass
    {
        public GalvaniteoreGenPass(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Charging up space";

            int maxToSpawn = (int)(Main.maxTilesX * Main.maxTilesY * 1E-02);
            for (int i = 0; i < maxToSpawn; i++)
            {
                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int y = WorldGen.genRand.Next((int)Main.worldSurface * 0, (int)((int)Main.worldSurface * 0.50f));

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(2, 3), ModContent.TileType<Galvaniteore>());
            }
        }
       
    }

}