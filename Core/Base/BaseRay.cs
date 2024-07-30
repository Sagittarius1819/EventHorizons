using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.GameContent.PlayerEyeHelper;
using System.Diagnostics;

namespace EventHorizons.Core.Base
{
    /// <summary>
    /// laza
    /// </summary>
    /// <param name="maxTime">The max amount of time, in ticks, that this projectile should last for.</param>
    /// <param name="maxLength">The maximum length of the ray.</param>
    /// <param name="laserGlowColor">The color that the ray emits along itself. Also the color used to draw the beam, if you don't override predraw.</param>
    /// <param name="maxLaserScale">The maximum scale of the laser.</param>
    /// <param name="laserExpandRate">How quickly it scales in and scales out.</param>
    /// <param name="SampleRate">Tile collision accuracy, if yo decide to use it. Values above 16 cause immense lag D:</param>
    /// <param name="maxRotateSpeed">Defaults to 0, but this is the rate at which the laser rotates, in radians.</param>
    /// <param name="drawBehindMode">for special drawing, make sure to set hide to true if you use this.</param>
    public abstract class BaseRay(int maxTime, float maxLength, Color laserGlowColor, float laserExpandRate, int SampleRate, float maxRotateSpeed = 0, int drawBehindMode = -1) : ModProjectile
    {
        #region Properties / vars

        /// <summary>
        /// The max amount of time the projectile can last for, in ticks.
        /// </summary>
        protected int MaxTime = maxTime;

        /// <summary>
        /// A ratio (0-1) of the laser's lifetime completion. 
        /// </summary>
        protected int TimeAlive;

        /// <summary>
        /// The current length of the laser, accounts for tiles. Use this for any effects related to the laser's length.
        /// </summary>
        protected float CurrentLength;

        /// <summary>
        /// The max length of the laser, unhindered by tiles
        /// </summary>
        protected float MaxLength = maxLength; // deafult non-tile collide laser range.

        /// <summary>
        /// The color the laser emits along itself. 
        /// </summary>
        protected Color LaserGlowColor = laserGlowColor;

        /// <summary>
        /// The rate at which the laser shrinks and expands. Used in <see cref="LifeTimeResize"/>.
        /// </summary>
        protected float LaserExpandRate = laserExpandRate;

        /// <summary>
        /// The speed (angle in radians per tick) at which the laser rotates. Unused by default.
        /// </summary>
        protected float MaxRotateSpeed = maxRotateSpeed;

        /// <summary>
        /// The number of points to check for tiles. Use values ranging from 5-10 for performance, and 11-20 for accuracy.
        /// </summary>
        protected int TileSamplingRate = SampleRate;

        /// <summary>
        /// Determines what the laser should draw over/behind. -1 by default
        /// <br>Modes:</br>
        /// <br>0: Behind npcs and tiles.</br>
        /// <br>1: Behind npcs.</br>
        /// <br>2: Behind projectiles.</br>
        /// <br>3: Over players.</br>
        /// <br>4: Over wires.</br>
        /// </summary>
        private readonly int DrawMode = drawBehindMode;

        /// <summary>
        /// The current amount of time this projectile has lasted. 
        /// </summary>
        protected ref float Time => ref Projectile.localAI[2];

        #endregion

        //textures!
        public abstract Texture2D StartTexture { get; }

        public abstract Texture2D MiddleTexture { get; }

        public abstract Texture2D EndTexture { get; }

        public virtual float MaxLaserScale { get; } = 1f;

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            switch (DrawMode)
            {
                case 0:
                    {
                        behindNPCsAndTiles.Add(index);
                    }
                    break;

                case 1:
                    {
                        behindNPCs.Add(index);
                    }
                    break;

                case 2:
                    {
                        behindProjectiles.Add(index);
                    }
                    break;

                case 3:
                    {
                        overPlayers.Add(index);
                    }
                    break;

                case 4:
                    {
                        overWiresUI.Add(index);
                    }
                    break;
            }
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 10000; 
        }

        public sealed override void AI()
        {
            Projectile.velocity = NormalizeBetter(Projectile.velocity);

            if (++Time >= MaxTime)
            {
                Projectile.Kill();
                return;
            }

            ManageScale();

            float updatedVelocityDirection = Projectile.velocity.ToRotation() + MaxRotateSpeed;
            Projectile.rotation = updatedVelocityDirection - PiOver2; // Pretty much all lasers have a vertical sheet (or, at least they should)
            Projectile.velocity = updatedVelocityDirection.ToRotationVector2();

            float idealLaserLength = GetLength(TileSamplingRate);
            CurrentLength = Lerp(CurrentLength, idealLaserLength, 0.85f); 

            if (LaserGlowColor != Color.Transparent)
            {
                for (int i = 0; i < CurrentLength; i++)
                {
                    Lighting.AddLight(Projectile.Center + (Projectile.velocity * i), LaserGlowColor.ToVector3() * (1f - ((MaxTime + 1) / (TimeAlive + 1))));
                }
            }

            ExtraAI();
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
                return true; // if target intersects with initial hitbox, return true

            float zero = 0;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * CurrentLength, Projectile.Size.Length(), ref zero))
                return true;

            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackMelee;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * CurrentLength, Projectile.Size.Length() * Projectile.scale, DelegateMethods.CutTiles);
        }

        public override bool ShouldUpdatePosition() => false; //required 

        public virtual void ManageScale()
        {
            Projectile.scale = (float)Math.Sin(TimeAlive / MaxTime * Pi) * LaserExpandRate * MaxLaserScale;
            if (Projectile.scale > MaxLaserScale)
                Projectile.scale = MaxLaserScale;
        }

        public virtual void ExtraAI()
        {

        }

        public float GetLength(int SampleCount)
        {
            if (Projectile.tileCollide)
            {
                float[] LaserSamples = new float[SampleCount];
                Collision.LaserScan(Projectile.Center, Projectile.velocity, Projectile.scale, MaxLength, LaserSamples);

                return LaserSamples.Average();
            }
            else return MaxLength;
        }

        public void DrawBeam(Color col, float scale)
        {
            Rectangle start = StartTexture.Frame(1, Main.projFrames[Projectile.type], 0, 0);
            Rectangle middle = MiddleTexture.Frame(1, Main.projFrames[Projectile.type], 0, 0);
            Rectangle end = EndTexture.Frame(1, Main.projFrames[Projectile.type], 0, 0);

            Main.EntitySpriteDraw(StartTexture, Projectile.Center - Main.screenPosition, start, col, Projectile.rotation, StartTexture.Size() / 2f, scale, SpriteEffects.None, 0);

            float length = CurrentLength;
            length -= (start.Height / 2 + end.Height) * scale;

            Vector2 center = Projectile.Center;
            center += Projectile.velocity * scale * start.Height / 2f;

            if (length > 0f)
            {
                float laserOffset = middle.Height * scale;
                float incrementalBodyLength = 0f;
                while (incrementalBodyLength + 1f < length)
                {
                    Main.EntitySpriteDraw(MiddleTexture, center - Main.screenPosition, middle, col, Projectile.rotation, MiddleTexture.Width * 0.5f * Vector2.UnitX, scale, SpriteEffects.None, 0);
                    incrementalBodyLength += laserOffset;
                    center += Projectile.velocity * laserOffset;
                }
            }

            if (Math.Abs(CurrentLength - GetLength(TileSamplingRate)) < 30f)
            {
                Vector2 laserEndCenter = center - Main.screenPosition;
                Main.EntitySpriteDraw(EndTexture, laserEndCenter, end, col, Projectile.rotation, EndTexture.Frame(1, 1, 0, 0).Top(), scale, SpriteEffects.None, 0);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawBeam(LaserGlowColor, 1f);
            DrawBeam(Color.Lerp(LaserGlowColor, Color.White, 0.8f), 0.5f);

            return false;
        }
    }
}
