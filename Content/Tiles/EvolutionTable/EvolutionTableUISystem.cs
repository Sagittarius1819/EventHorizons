using EventHorizons.Content.Items.Materials;
using EventHorizons.Content.Items.Placeables.Ores;
using EventHorizons.Content.Items.Weapons.Ranged.Crystalline;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace EventHorizons.Content.Tiles.EvolutionTable
{
    public class EvolutionTableUISystem : ModSystem
    {
        private UserInterface CustomInterface;
        public Vector2 TilePosition;
        public EvolutionTableS1UI CustomUI;
        public List<EvoTableRecipe> ValidRecipes = [];

        public override void PostSetupContent()
        {
            // Example recipe:
            RegisterRecipe(
                [
                    new Item(ModContent.ItemType<CrystallineCore>(), 3),
                    new Item(ModContent.ItemType<CrystalliteBar>(), 3),
                    new Item(ItemID.Handgun)
                ],
                new Item(ModContent.ItemType<CrystallinePistol>())
            );

            RegisterRecipe(
                [
                    new Item(ItemID.TinOre, 9)
                ],
                new Item(ItemID.TinBar, 9)
            );

            RegisterRecipe(
                 [
                     new Item(ItemID.TinOre, 2),
                    new Item(ItemID.TinOre, 3),
                    new Item(ItemID.TinOre, 5)
                 ],
                 new Item(ItemID.TinBar, 9)
             );
            RegisterRecipe(
        [
            new Item(ItemID.TinOre, 2),
                    new Item(ItemID.TinOre, 3),
                    new Item(ItemID.TinOre, 5)
        ],
        new Item(ItemID.TinBar, 9)
    );

            RegisterRecipe(
    [
        new Item(ItemID.TinOre, 2),
                    new Item(ItemID.TinOre, 3),
                    new Item(ItemID.TinOre, 5)
    ],
    new Item(ItemID.TinBar, 9)
);
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                CustomUI = new EvolutionTableS1UI();
                CustomUI.Activate();
                CustomInterface = new UserInterface();
            }
        }

        private void RegisterRecipe(Item item1, Item item2, Item item3, Item item4)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            SoundEngine.PlaySound(SoundID.MenuOpen);
            CustomInterface.SetState(CustomUI);
        }

        public void Hide()
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            CustomInterface.SetState(null);
        }

        public void Toggle()
        {
            if (CustomInterface.CurrentState == null)
                Show();
            else
                Hide();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
            CustomInterface.Update(gameTime);

            // Close menu when far away from the evolution table
            if (CustomInterface.CurrentState == CustomUI && Main.LocalPlayer.Distance(TilePosition * 16f) > 200) Hide();
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

        public void RegisterRecipe(Item[] ingredients, Item res)
        {
            if (ingredients.Length > 4) EventHorizons.Instance.Logger.Error("Error registering Evolution Table recipe: the number of ingredients must not exceed 4 item stacks.");
            ValidRecipes.Add(new EvoTableRecipe(ingredients, res));
        }
    }

    public class EvoTableRecipe
    {
        public List<(int, int)> Ingredients;
        public Item Result;

        public EvoTableRecipe(Item[] ingredients, Item res)
        {
            Ingredients = ingredients.Select((i) => (i.type, i.stack)).ToList();
            Result = res;
        }

        public bool CheckForRecipe(IEnumerable<Item> ingredients)
        {
            // Don't craft if more ingredients are given than the recipe needs
            if (ingredients.Count() > Ingredients.Count) return false;

            var temp = Ingredients.OrderBy((i) => i.Item2).ToList();
            foreach (Item ing in ingredients.OrderBy((i) => i.stack))
            {
                var match = temp.Where((i) => ing.type == i.Item1 && ing.stack >= i.Item2);
                if (match.Any()) temp.Remove(match.First());
            }
            return temp.Count == 0;
        }
    }
}
