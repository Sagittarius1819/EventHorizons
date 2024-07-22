using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
public class PrismPiercer : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 32;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.knockBack = 4;
        Item.value = Item.sellPrice(silver: 90);
        Item.rare = ItemRarityID.Purple; //might need to be changed
        Item.UseSound = SoundID.Item1; //might need to be changed
        Item.autoReuse = true;
        Item.crit = 8;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<PrismPiercerProjectile>();
        Item.shootSpeed = 12f; //might need to be changed
    }

    public override void AddRecipes() => CreateRecipe().Register();
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float numberProjectiles = 3;
        float rotation = PiOver4;

        position += Vector2.Normalize(velocity) * 45f;

        for (int i = 0; i < numberProjectiles; i++)
        {
            Vector2 perturbedSpeed = velocity.RotatedBy(Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.4f;
            Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
        }

        return false;
    }
}

public class PrismPiercerProjectile : ModProjectile
{
    public override string Texture => TryGetTextureFromOther<PrismPiercer>() + "_Projectile";

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;

        ProjectileID.Sets.TrailingMode[Type] = 2;
        ProjectileID.Sets.TrailCacheLength[Type] = 5;
    }

    public override void SetDefaults()
    {
        Projectile.timeLeft = 45;
        Projectile.friendly = true;
        Projectile.width = 8;
        Projectile.height = 14;
    }

    public override void AI()
    {
        Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
        Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == -1 ? -PiOver2 : PiOver2);
        if (Projectile.ai[2] == 0)
        {
            Projectile.frame = Main.rand.Next(4);
            Projectile.ai[2] = 1;
        }
    }

    private Color TrailColor(float prog)
    {
        Color col = Color.MediumVioletRed;
        Color col2 = Color.Lerp(Color.Purple, Color.White, 0.9f);

        return Color.Lerp(col2, col, prog);
    }

    private float TrailWidth(float prog)
    {
        float w = 15f;
        w *= Lerp(1, 0, prog);
        return w;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        default(Trail).Draw(Projectile, TrailColor, TrailWidth);

        Projectile.SimpleDrawProjectile(TextureAssets.Projectile[Type].Value, Color.White, true, 1f);

        return false;
    }
}