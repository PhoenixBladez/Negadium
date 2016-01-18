using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Negadium.Items.Misc
{
    public class DemonFetus : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Demon Fetus";
            item.width = 48;
            item.height = 48;
            item.toolTip = "A Demon Fetus... Yuck";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.maxStack = 999;
            item.rare = 10;
        }

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 4);
        }
    }
}
