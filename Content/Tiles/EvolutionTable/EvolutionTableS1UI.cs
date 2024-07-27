using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;

namespace EventHorizons.Content.Tiles.EvolutionTable
{
    public class EvolutuontableS1UI : UIState
    {
        private UIPanel panel;
        public override void OnInitialize()
        {
            panel = new UIPanel();
            panel.SetPadding(0);
            panel.Left.Set(400f, 0f);
            panel.Top.Set(100f, 0f);
            panel.Width.Set(200f, 0f);
            panel.Height.Set(300f, 0f);
            panel.BackgroundColor = new Color(73, 94, 171);

            UIText text = new UIText("Custom Menu");
            text.HAlign = 0.5f;
            text.Top.Set(10f, 0f);
            panel.Append(text);

            Append(panel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Update your UI elements here
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            // Draw your UI elements here
        }
    }
}