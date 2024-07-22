using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;



namespace EventHorizons.Content.NPCs.Enemies.CCenemies
{

    internal class ProtectedCrystallineSlime : ModNPC
    {
        public override void SetStaticDefaults()

        {

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.ShimmerSlime;
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 24;
            NPC.damage = 30;
            NPC.defense = 25;
            NPC.lifeMax = 75;
            NPC.value = 300f;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.Item4;
            NPC.DeathSound = new SoundStyle("EventHorizons/Assets/Sounds/Short_Crystal_Soul_S") with
            {
                Volume = 0.5f,
                Pitch = 0.5f,
                PitchVariance = 0.1f,
                MaxInstances = 3,
                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
            };
            AIType = NPCID.GreenSlime;
            AnimationType = NPCID.GreenSlime;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 20)
            {
                NPC.frameCounter = 0;
            }
            NPC.frame.Y = (int)NPC.frameCounter / 10 * frameHeight;
        }

        public override void OnKill()
        {
            // Main.NewNPC(x, y, type);
            // Here, x and y are the coordinates, and type is the NPC type ID of the second enemy
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<ExposedCrystallineSlime>());
        }
    }
}
