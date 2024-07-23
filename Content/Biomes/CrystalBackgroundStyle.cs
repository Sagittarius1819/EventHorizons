using Terraria;
using Terraria.ModLoader;
using EventHorizons.Content.Assets.Textures;

namespace EventHorizons.Content.Biomes
{
    public class CrystalBackgroundStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Textures/Backgrounds/Crystal/Crystal_0");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Textures/Backgrounds/Crystal/Crystal_1");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Textures/Backgrounds/Crystal/Crystal_2");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Textures/Backgrounds/Crystal/Crystal_3");
        }
    }
}