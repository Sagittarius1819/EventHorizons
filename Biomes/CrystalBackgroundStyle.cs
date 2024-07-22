using Terraria;
using Terraria.ModLoader;

namespace EventHorizons.Biomes
{
	public class CrystalBackgroundStyle: ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
			textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Biomes/CrystalBiomeZero");
			textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Biomes/CrystalBiomeOne");
			textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Biomes/CrystalBiomeTwo");
			textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Biomes/CrystalBiomeThree");
		}
	}
}