using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace EventHorizons.Core.ModSystems
{
    class EVMusicLoader : ModSystem
    {
        public override void Load()
        {
            MusicLoader.AddMusic(Mod, "Assets/Music/KingSlimeTheme"); //add any more that you need to below
        }
    }
}
