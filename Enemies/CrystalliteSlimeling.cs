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
using EventHorizons.Items.Materials;
using Terraria.Audio;
using EventHorizons.Items.Critters;

namespace EventHorizons.Enemies
{
    internal class CrystalliteSlimeling : ModNPC
    {
        public override void SetStaticDefaults()

        {

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];
        }

        public override void SetDefaults()
        {
            NPC.width = 16;
            NPCID.Sets.CountsAsCritter[Type] = true;
            NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
            NPC.height = 12;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 15;
            NPC.value = 0;
            NPC.aiStyle = 66;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.Item4;
            AIType = NPCID.Maggot;
            AnimationType = NPCID.GreenSlime;
            NPC.catchItem = (short)ModContent.ItemType<CrystalliteCritter>();

        }
        public override void FindFrame(int frameHeight)
        {
            // Timer for frame switching
            NPC.frameCounter++;

            // Switch frames every 10 ticks (adjust the value for speed)
            if (NPC.frameCounter >= 20)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                // If we have gone past the last frame, loop back to the first frame
                if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
                {
                    NPC.frame.Y = 0;
                }
            }
        }
        public override void OnCaughtBy(Player player, Item item, bool failed)
        {
            if (failed)
            {
                return;
            }


        }
    }
}
