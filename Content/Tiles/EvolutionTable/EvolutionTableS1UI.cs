using Microsoft.Xna.Framework;
using SerousCommonLib.UI;
using System.Linq;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace EventHorizons.Content.Tiles.EvolutionTable
{
    public class EvolutionTableS1UI : UIState
    {
        private UIPanel Panel;
        private EnhancedItemSlotV2[] Slots = [];

        public override void OnInitialize()
        {
            Panel = new UIPanel();
            Panel.SetPadding(0);
            Panel.HAlign = 0.5f;
            Panel.VAlign = 0.75f;
            Panel.Width.Set(400, 0f);
            Panel.Height.Set(240, 0f);
            Panel.BackgroundColor = new Color(73, 94, 171);

            UIText text = new UIText("Evolution Table Crafting");
            text.HAlign = 0.5f;
            text.Top.Set(10f, 0f);
            Panel.Append(text);

            for (int i = 0; i < 4; i++)
            {
                EnhancedItemSlotV2 craftSlot = new EnhancedItemSlotV2(i, ItemSlot.Context.ChestItem);
                craftSlot.HAlign = 0.7f;
                craftSlot.VAlign = 0f;
                craftSlot.Left.Set((i % 2) * 64, 0f);
                craftSlot.Top.Set(60 + (i / 2) * 64, 0f);
                Slots = Slots.Append(craftSlot).ToArray();
                Panel.Append(craftSlot);
            }

            EnhancedItemSlotV2 resultSlot = new EnhancedItemSlotV2(4, scale: 2);
            resultSlot.IsItemAllowed = (slot, item) => item.IsAir;
            resultSlot.HAlign = 0.2f;
            resultSlot.VAlign = 0.5f;
            Slots = Slots.Append(resultSlot).ToArray();
            Panel.Append(resultSlot);

            UIButton<string> craftButton = new UIButton<string>("Generate");
            craftButton.Width.Set(100, 0f);
            craftButton.Height.Set(35, 0f);
            craftButton.HAlign = 0.5f;
            craftButton.VAlign = 0.95f;
            craftButton.OnLeftClick += TryCraft;
            Panel.Append(craftButton);

            UIButton<string> closeButton = new UIButton<string>("X");
            closeButton.Width.Set(25, 0f);
            closeButton.Height.Set(25, 0f);
            closeButton.HAlign = 0.95f;
            closeButton.VAlign = 0.05f;
            closeButton.OnLeftClick += (evt, listeningElement) => { ModContent.GetInstance<EvolutionTableUISystem>().Hide(); };
            Panel.Append(closeButton);

            Append(Panel);
        }

        private void TryCraft(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!Slots[4].StoredItem.IsAir) return;

            var ingredients = Slots[0..4].Select((s) => s.StoredItem);
            foreach (EvoTableRecipe recipe in ModContent.GetInstance<EvolutionTableUISystem>().ValidRecipes)
            {
                if (recipe.CheckForRecipe(ingredients))
                {
                    Craft(recipe);
                    return;
                }
            }
            return;
        }

        private void Craft(EvoTableRecipe recipe)
        {
            SoundEngine.PlaySound(SoundID.Item176 with { Pitch = -0.2f });
            foreach (EnhancedItemSlotV2 slot in Slots) slot.StoredItem.TurnToAir();
            Slots[4].SetBoundItem(recipe.Result.Clone());
        }
    }
}