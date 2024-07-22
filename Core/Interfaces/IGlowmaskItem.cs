using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EventHorizons.Core.Interfaces
{
    /// <summary>
    /// Handles drawing an item's glomask while on the ground or in the inventory. Items that use this MUST have a _Glow texture.
    /// </summary>
    internal interface IGlowmaskItem
    {
        /// <summary>
        /// The color the item should emit while in the world.
        /// </summary>
        Color GlowEmitColor { get; }
    }

    class GlowItem : GlobalItem
    {
        public override bool AppliesToEntity(Item i, bool lateInstantiation) => i.ModItem is IGlowmaskItem;

        public override void PostUpdate(Item item)
        {
            Lighting.AddLight(item.Center, (item.ModItem as IGlowmaskItem).GlowEmitColor.ToVector3());

            base.PostUpdate(item);
        }

        public override bool PreDrawInWorld(Item i, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D tex = TextureAssets.Item[i.type].Value;
            Texture2D glow = Request<Texture2D>(i.ModItem.GetType().Namespace.Replace(".", "/") + "/" + i.ModItem.Name + "_Glow").Value;

            Main.spriteBatch.Draw(tex, i.Center - Main.screenPosition, null, lightColor, rotation, tex.Size() / 2f, scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(glow, i.Center - Main.screenPosition, null, Color.White, rotation, glow.Size() / 2f, scale, SpriteEffects.None, 0f);

            return false;
        }

        public override bool PreDrawInInventory(Item i, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = TextureAssets.Item[i.type].Value;
            Texture2D glow = Request<Texture2D>(i.ModItem.GetType().Namespace.Replace(".", "/") + "/" + i.ModItem.Name + "_Glow").Value;

            spriteBatch.Draw(tex, position, null, drawColor, 0f, origin, scale, 0, 0);
            spriteBatch.Draw(glow, position, null, Color.White, 0f, origin, scale, 0, 0);

            return false;
        }
    }
}
