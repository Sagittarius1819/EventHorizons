using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.Audio;
using EventHorizons.Content.Items.Materials;

namespace EventHorizons.Content.General
{
    internal class Class1 : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SpaceDust>(), 30));

        }

    }
}
