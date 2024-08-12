using EventHorizons.Content.Items.Materials;
using EventHorizons.Content.Items.Placeables.Ores;
using EventHorizons.Content.Items.Weapons.Ammo;
using EventHorizons.Content.Items.Weapons.Ranged.Crystalline;
using EventHorizons.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace EventHorizons.Content.NPCs.Town
{
    public class DarwinNPC : ModNPC
    {
        public const string ShopName = "Evolution Store";
        public int NumberOfTimesTalkedTo = 0;

        private static Profiles.StackedNPCProfile NPCProfile;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has

            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
            NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 2; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 30; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.


            // Connects this NPC with a custom emote.
            // This makes it when the NPC is in the world, other NPCs will "talk about him".
            // By setting this you don't have to override the PickEmote method for the emote to appear.
            NPCID.Sets.FaceEmote[Type] = EmoteID.EmoteLaugh;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers() {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                              // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                              // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            // Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
            // NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.
            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Love)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Love)
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like) // Likes living near the guide.
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike) // Dislikes living near the merchant.
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate) // Hates living near the demolitionist.
            ; // < Mind the semicolon!

            // This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is 
            /*NPCProfile = new Profiles.StackedNPCProfile(
                new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture + "_Party")
            );*/
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("The great mind of the World we live in, he believes everything evolved from something...but his studies also found out...that non living things evolve"),
            });
        }

        // The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
        // Returning false will allow you to manually draw your NPC
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            // This code slowly rotates the NPC in the bestiary
            // (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
            if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
            {
                drawModifiers.Rotation += 0.001f;

                // Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
                NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            }

            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;

            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
            }

            // Create gore when the NPC is killed.
            /*if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                string variant = "";
                if (NPC.altTexture == 1) variant += "_Party";
                int hatGore = NPC.GetPartyHatGore();
                int headGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Head").Type;
                int armGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Arm").Type;
                int legGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Leg").Type;

                if (hatGore > 0)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, hatGore);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, headGore, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
            }*/
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        { // Requirements for the town NPC to spawn.
            foreach (var player in Main.ActivePlayers)
            {
                // Player has to have either an ExampleItem or an ExampleBlock in order for the NPC to spawn
                if (player.inventory.Any(item => item.type == ModContent.ItemType<CrystallineCore>()))
                {
                    return true;
                }
            }

            return false;
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return NPCProfile;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Doctor Darwin",
                "Doc Darwin",
                "Mister Darwin",
                "Mr. Darwin",
                "The Great Darwin",
                "Darwin The Great",
                "Charles",
                "Charlie"
            };
        }


        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int guide = NPC.FindFirstNPC(NPCID.Guide);
            if (guide >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("The guide.. He's amazing...A mind greater than mine... And somehow, still alive!", Main.npc[guide].GivenName));
            }
            int Merchant = NPC.FindFirstNPC(NPCID.Merchant);
            if (Merchant >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("The merchant. That's what we would call in my days a scam...but i need myself some stuff so it's the only reliable man here.", Main.npc[Merchant].GivenName));
            }
            int Demo = NPC.FindFirstNPC(NPCID.Demolitionist);
            if (Demo >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("Can you ask my neighbor to stop blowing firecrackers at night? It disturbs my sleep", Main.npc[Demo].GivenName));
            }
            int Nurse = NPC.FindFirstNPC(NPCID.Nurse);
            if (Nurse >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("Finally! A way to treat arthritis! What? What do you mean she can't heal it?-", Main.npc[Nurse].GivenName));
            }
            int Golf = NPC.FindFirstNPC(NPCID.Golfer);
            if (Golf >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("The Golfer sure knows how to enjoy life...I wish I wasn't so fixated on my work, so I could play with him.", Main.npc[Golf].GivenName));
            }
            int Dye = NPC.FindFirstNPC(NPCID.DyeTrader);
            if (Dye >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("That's an...original man right there, not everyone can extract the natural colors of the plant and sell them in bottles like this! I really hope i get orange...", Main.npc[Dye].GivenName));
            }
            int Chippy = NPC.FindFirstNPC(NPCID.Clothier);
            if (Chippy >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("I knew I recognized that face! He seems old still but he's just as fixated to clothes as i am for evolution and papers!", Main.npc[Chippy].GivenName));
            }
            int fish = NPC.FindFirstNPC(NPCID.Angler);
            if (fish >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("Such a short Fisherman.. Rude, annoying and pretentious...this new generation is doomed!", Main.npc[fish].GivenName));
            }
            int EOC = NPC.FindFirstNPC(NPCID.EyeofCthulhu);
            if (EOC >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("I'M FEELING WATCHED UNRESPECTFULLY", Main.npc[EOC].GivenName));
            }
            chat.Add(Language.GetTextValue("Have you read the pages of my work? I'm actually surprised! Kids these days just look everything online...in a wiki"));
            chat.Add(Language.GetTextValue("The scraps of my work are what i define as...extensive...do you have a degree to read them?"));
            chat.Add(Language.GetTextValue("Back in my days the only weapons you needed were muskets and cannons...maybe it's a good thing we got weapons that don't risk to kill you on the spot"));
            chat.Add(Language.GetTextValue("Don't you like the fresh wind? I know I don't like it at all, my old bones trembles in winter"));
            chat.Add(Language.GetTextValue("I've heard once that penguins hate me... it's their fault if they can't hold screwdrivers"));
            chat.Add(Language.GetTextValue("Trust me, don't flash a giraffe with a phone..."));
            chat.Add(Language.GetTextValue("Snakes are either stupid for losing their legs or knew what they were doing thinking about the tattoos they'll be on"));
            chat.Add(Language.GetTextValue("A scientific man ought to have no wishes, no affections, a mere heart of stone."), 4.0);
            chat.Add(Language.GetTextValue("I got a glock in my rari"), 0.01); //<-- really frickin rare

            NumberOfTimesTalkedTo++;
            if (NumberOfTimesTalkedTo >= 10)
            {
                //This counter is linked to a single instance of the NPC, so if ExamplePerson is killed, the counter will reset.
                chat.Add(Language.GetTextValue("Listen, Thanks for the chat, Ive got to go now."));
            }

            string chosenChat = chat; // chat is implicitly cast to a string. This is where the random choice is made.
            return chosenChat;
        }


        // Not completely finished, but below is what the NPC will sell
        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, ShopName)
                .Add<CrystallineCore>()
                .Add(new Item(ModContent.ItemType<CrystallinePistol>()) { shopCustomPrice = Item.buyPrice(gold: 2) })
                .Add<CrystalliteBar>()
            //Conditions
                .Add<PlasmaBlast>(Condition.MoonPhasesQuarter1)
                .Add<Galvanitebar>(Condition.DownedMoonLord);
        }

        public override void ModifyActiveShop(string shopName, Item[] items)
        {
            foreach (Item item in items)
            {
                // Skip 'air' items and null items.
                if (item == null || item.type == ItemID.None)
                {
                    continue;
                }

            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Book));
        }




        public override void OnGoToStatue(bool toKingStatue)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)NPC.whoAmI);
                packet.Send();
            }
            else
            {
                StatueTeleport();
            }
        }

        // Create a square of pixels around the NPC on teleport.
        public void StatueTeleport()
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Main.rand.NextVector2Square(-20, 21);
                if (Math.Abs(position.X) > Math.Abs(position.Y))
                {
                    position.X = Math.Sign(position.X) * 20;
                }
                else
                {
                    position.Y = Math.Sign(position.Y) * 20;
                }

                Dust.NewDustPerfect(NPC.Center + position, DustID.DarkCelestial, Vector2.Zero).noGravity = true;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<Chaos>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
            // SparklingBall is not affected by gravity, so gravityCorrection is left alone.
        }

        public override void LoadData(TagCompound tag)
        {
            NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
        }

        public override void SaveData(TagCompound tag)
        {
            tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
        }
    }
}