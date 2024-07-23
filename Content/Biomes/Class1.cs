using EventHorizons.Commons.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;


namespace EventHorizons.Content.Biomes
{
    public class Class1 : ModBiome
    {
        // Select all the scenery

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => GetInstance<CrystalBackgroundStyle>();

        // Select Music
        public override int Music => MusicLoader.GetMusicSlot("EventHorizons/Content/Assets/Music/Biome/Let_It_Be_The_Crystal");

        // Sets how the Scene Effect associated with this biome will be displayed with respect to vanilla Scene Effects. For more information see SceneEffectPriority & its values.
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow; // We have set the SceneEffectPriority to be BiomeLow for purpose of example, however default behavior is BiomeLow.


        // Calculate when the biome is active.

        // Calculate when the biome is active.
        public override bool IsBiomeActive(Player player)
        {
            // Limit the biome height to be underground in either rock layer or dirt layer
            return (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) &&
                // Check how many tiles of our biome are present, such that biome should be active
                GetInstance<CrystallineCavernsTileCount>().CrystallineStoneCount >= 125 &&
                // Limit our biome to be in only the horizontal center third of the world.
                Math.Abs(player.position.ToTileCoordinates().X - Main.maxTilesX / 2) < Main.maxTilesX / 6;
        }


        // In the event that both our biome AND one or more modded SceneEffect layers are active with the same SceneEffect Priority, this can decide which one.
        // It's uncommon that need to assign a weight - you'd have to specifically believe that you don't need higher SceneEffectPriority, but do need to be the active SceneEffect within the priority you designated
        // In this case, we don't need it, so this inclusion is purely to demonstrate this is available.
        // See the GetWeight documentation for more information.


    }


}