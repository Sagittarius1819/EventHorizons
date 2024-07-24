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

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Boss/King_Slime"); //make sure the music file is in a Music folder!
    }
    internal class EOCMusic : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            return Main.npc.Any(n => n.active && n.type == NPCID.EyeofCthulhu);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Boss/Eye_Of_Cthulhu");
    }
    internal class QBMusic : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            return Main.npc.Any(n => n.active && n.type == NPCID.QueenBee);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Boss/A_Bee-g_Trouble");
    }
    internal class SkeleMusic : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            return Main.npc.Any(n => n.active && n.type == NPCID.SkeletronHead);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Boss/Skeletron");
    }
    internal class WOFMusic : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            return Main.npc.Any(n => n.active && n.type == NPCID.WallofFlesh);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Boss/Wall_Of_Flesh");
    }
}
