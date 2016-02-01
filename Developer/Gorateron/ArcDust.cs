using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Negadium.Developer.Gorateron
{
    public class ArcDust : ModDust
    {
        public override void SetDefaults()
        {
            this.updateType = 226;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return lightColor;
        }
    }
}
