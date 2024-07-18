using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EventHorizons.Buffs
{
    public class ShockedPlayer : ModPlayer
    {
        public bool HasGalvaniteArmor;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int chance = Main.rand.Next(10);
            if (HasGalvaniteArmor = true)
            {
               if (chance == 0)
                    {
                        if (Player.HeldItem.DamageType == DamageClass.Magic)
                            {
                                target.AddBuff(ModContent.BuffType<Buffs.Shocked>(), 180);
                            }
                        else if (Player.HeldItem.DamageType == DamageClass.Ranged)
                            { 
                                 target.AddBuff(ModContent.BuffType<Buffs.Shocked>(), 180); 
                            }
                    }
            }
        }
    }
}
