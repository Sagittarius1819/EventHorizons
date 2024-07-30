using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace EventHorizons.Commons
{
    public static partial class HorizonUtils
    {
        public static Vector2 NormalizeBetter(Vector2 vec)
        {
            if (vec == Vector2.Zero)
            {
                return new Vector2(0f, 0.001f);
            }

            return Vector2.Normalize(vec);
        }

        /// <summary>
        /// Gets a random number, which will stay above the value of <paramref name="absRange"/>
        /// </summary>
        /// <param name="randMin">The minimum value to pass to the seed.</param>
        /// <param name="randMax">The maximum value to pass to the seed.</param>
        /// <param name="absRange">The absolute value of the minumum value (ex you want smth outside (-40, 40), set this to 40).</param>
        /// <returns>The value, outside the range of <paramref name="absRange"/></returns>
        public static float RandomInRange(float randMin, float randMax, float absRange)
        {
            float val = Main.rand.NextFloat(randMin, randMax);

            if (Math.Abs(val) < absRange)
                val = absRange;


            return val;
        }
    }
}
