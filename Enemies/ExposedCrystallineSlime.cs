using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EventHorizons.Enemies;

namespace EventHorizons.Enemies
{
    internal class ExposedCrystallineSlime : ModNPC
    {
        public override void SetStaticDefaults()

        {
            
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.ShimmerSlime;
        }
        public override void OnKill()
        {
            // Randomly decide to spawn 2 or 3 enemies
            int numberOfSpawns = Main.rand.Next(3, 6); // 2 to 3

            for (int i = 0; i < numberOfSpawns; i++)
            {
                // Offset the spawn position slightly to avoid stacking
                int spawnX = (int)NPC.position.X + Main.rand.Next(-20, 21);
                int spawnY = (int)NPC.position.Y + Main.rand.Next(-20, 21);
                NPC.NewNPC(NPC.GetSource_FromAI(), spawnX, spawnY, ModContent.NPCType<CrystalliteSlimeling>());
            }
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 24;
            NPC.damage = 35;
            NPC.defense = 12;
            NPC.lifeMax = 90;
            NPC.value = 300f;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.Item4;
            NPC.DeathSound = SoundID.Item29;
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

    }
}
