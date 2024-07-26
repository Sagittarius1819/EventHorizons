using Terraria.ModLoader;

namespace EventHorizons.Content.Biomes
{
    public class HollowSpaceBackgroundStyle : ModSurfaceBackgroundStyle
    {
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
               
            }
        }
        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Textures/Backgrounds/SpaceBackgroundFar");
        }

        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Textures/Backgrounds/SpaceBackgroundMid");
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot("EventHorizons/Content/Assets/Textures/Backgrounds/SpaceBackgroundClose");
        }

    }
    }
