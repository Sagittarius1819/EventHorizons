using Terraria;
using Terraria.ModLoader;
namespace EventHorizons.Content.Dusts
{
    public class CrystalDust : ModDust
    {
        public void OnSpawn(Dust dust)
        {
            dust.alpha = 170;
            dust.velocity *= 0.5f;
            dust.velocity.Y += 1f;
        }
    }
    public class CrystalStoneDust : ModDust
    {
        public void OnSpawn(Dust dust)
        {
            dust.alpha = 170;
            dust.velocity *= 0.5f;
            dust.velocity.Y += 1f;
        }
    }
}