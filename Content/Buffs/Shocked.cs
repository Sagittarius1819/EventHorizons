using Terraria;
using Terraria.ModLoader;

namespace EventHorizons.Content.Buffs
{
    public class Shocked : ModBuff
    {
        private int timer;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
                npc.lifeRegen = 0;
            npc.lifeRegen -= 10;

        }

    }
}
