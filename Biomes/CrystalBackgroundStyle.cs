using Terraria.ModLoader;

namespace EventHorizons.Biomes
{
	public class CrystalBackgroundStyle: ModUndergroundBackgroundStyle
	{
		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "EventHorizons/Backgrounds/crystal0");
			textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "EventHorizons/Backgrounds/crystal1");
			textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "EventHorizons/Backgrounds/crystal2");
			textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "EventHorizons/Backgrounds/crystal3");
		}
	}
}