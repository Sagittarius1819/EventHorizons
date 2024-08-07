using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ID;
using EventHorizons.Content.Items.Materials;
using EventHorizons.Content.Items.Placeables.Ores;
using EventHorizons.Content.Items.Weapons.Ranged.Crystalline;

namespace EventHorizons.Content.Tiles.EvolutionTable
{
    public class EvolutionTableUISystem : ModSystem
    {
        private UserInterface CustomInterface;
        public Vector2 TilePosition;
        public EvolutionTableS1UI CustomUI;
        public List<EvoTableRecipe> ValidRecipes = []; // O(1) recipe lookup with hashset of hashset of ingredients?

        public override void Load()
        {
            if (!Main.dedServ)
            {
                CustomUI = new EvolutionTableS1UI();
                CustomUI.Activate();
                CustomInterface = new UserInterface();
            }

            // Example recipe:
            RegisterRecipe(
                new Item(ItemID.Handgun),
                new Item(ModContent.ItemType<CrystallineCore>(), 3),
                new Item(ModContent.ItemType<CrystalliteBar>(), 3),
                new Item(ModContent.ItemType<CrystallinePistol>())
            );

        }

        public void Show()
        {
            CustomInterface.SetState(CustomUI);
        }

        public void Hide()
        {
            CustomInterface.SetState(null);
        }

        public void Toggle()
        {
            if (CustomInterface.CurrentState == null)
            {
                CustomInterface.SetState(CustomUI);
            }
            else
            {
                CustomInterface.SetState(null);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
            CustomInterface.Update(gameTime);

            // Close menu when far away from the evolution table
            if (Main.LocalPlayer.Distance(TilePosition * 16f) > 200) Hide();
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Evolution Table Crafting",
                    delegate {
                        if (CustomInterface?.CurrentState != null)
                        {
                            CustomInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public void RegisterRecipe(Item in1, Item in2, Item in3, Item res)
        {
            ValidRecipes.Add(new EvoTableRecipe(in1, in2, in3, res));

        }
    }

    public class EvoTableRecipe
    {
        public HashSet<(int, int)> Ingredients;
        public Item Result;

        public EvoTableRecipe(Item in1, Item in2, Item in3, Item res)
        {
            Ingredients = [(in1.type, in1.stack), (in2.type, in2.stack), (in3.type, in3.stack)];
            Result = res;
        }

        public bool CheckForRecipe(IEnumerable<Item> ingredients)
        {
            HashSet<(int, int)> ing = ingredients.Select((i) => (i.type, i.stack)).ToHashSet();
            return Ingredients.SetEquals(ing);
        }
    }
}
