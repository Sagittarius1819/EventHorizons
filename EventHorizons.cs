using EventHorizons.Content.Tiles.EvolutionTable;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria;
using Terraria.ObjectData;

namespace EventHorizons
{
    public class EventHorizons : Mod
    {
        public static EventHorizons Instance => GetInstance<EventHorizons>();
       
        private UserInterface customInterface;
            public EvolutuontableS1UI customUI;

            public override void Load()
            {
                if (!Main.dedServ)
                {
                    customUI = new EvolutuontableS1UI();
                    customUI.Activate();
                    customInterface = new UserInterface();
                    customInterface.SetState(customUI);
                }
            }

            public bool UpdateUI(GameTime gameTime)
            {
                if (customInterface?.CurrentState != null)
                {
                    customInterface.Update(gameTime);
                }
                return true;

            }

            public void OpenEvolutionTableS1UI()
            {
                customInterface?.SetState(customUI);
            }

            public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
            {
                int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
                if (mouseTextIndex != -1)
                {
                    layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                        "YourMod: Custom UI",
                        delegate
                        {
                            customInterface.Draw(Main.spriteBatch, new GameTime());
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }
}
