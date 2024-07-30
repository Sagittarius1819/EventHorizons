using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EventHorizons.Commons.Systems.GlobalNPCs
{
    internal class KingSlimeMusic : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            return Main.npc.Any(n => n.active && n.type == NPCID.KingSlime);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/KingSlimeTheme"); 
    }
}
