using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.Audio;
using EventHorizons.Content.Items.Materials;
using System.Linq;

namespace EventHorizons.Content.General
{
    internal class Class1 : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<SpaceDust>(), 30));

        }

    }
    internal class KingSlimeMusic : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            return Main.npc.Any(n => n.active && n.type == NPCID.KingSlime);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Boss/KStheme"); //make sure the music file is in a Music folder!
    }
}
    