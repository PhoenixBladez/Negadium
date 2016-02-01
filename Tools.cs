using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Negadium
{
    public static class Tools
    {
        public static bool Between(this float numberToCheck, float bottom, float top)
        {
            return (numberToCheck >= bottom && numberToCheck <= top);
        }

        public static bool Between(this int numberToCheck, int bottom, int top)
        {
            return (numberToCheck >= bottom && numberToCheck <= top);
        }

        public static bool Between(this double numberToCheck, double bottom, double top)
        {
            return (numberToCheck >= bottom && numberToCheck <= top);
        }
    }
}
