using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Linq;
using EventHorizons.Content.NPCs.Town;

namespace EventHorizons
{
    public class EventHorizons : Mod
    {
        public static EventHorizons Instance => GetInstance<EventHorizons>();
    }
    public class EHGlobNPC : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            // Check if the NPC is the Guide (ID 22)
            if (npc.type == NPCID.Guide)
            {
                // Check if the modded NPC is alive
                bool isModdedNPCAlive = Main.npc.Any(n => n.active && n.type == ModContent.NPCType<DarwinNPC>());

                if (isModdedNPCAlive)
                {
                    //add custom dialogue options
                    if (Main.rand.NextFloat() < 0.25f) // 25% chance to play this dialogue
                    {
                        chat = "placeholder1";
                    }
                }
            }
            if (npc.type == NPCID.Guide)
            {
                bool isModdedNPCAlive = Main.npc.Any(n => n.active && n.type == ModContent.NPCType<DarwinNPC>());

                if (isModdedNPCAlive)
                {
                    if (Main.rand.NextFloat() < 0.25f)
                    {
                        chat = "placeholder2";
                    }
                }
            }
        }

    }
}
