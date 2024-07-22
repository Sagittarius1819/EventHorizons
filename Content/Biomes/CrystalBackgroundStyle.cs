using Terraria;
using Terraria.ModLoader;
using EventHorizons.Content.Assets.Textures;

namespace EventHorizons.Content.Biomes
{
    public class CrystalBackgroundStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Backgrounds/CrystalBiome");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Backgrounds/CrystalBiomeOne");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Backgrounds/CrystalBiomeTwo");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Backgrounds/CrystalBiomeThree");
        }
    }
}