using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace Negadium.Items.Accessories.Mharadium
{
    public class MharadiumWings : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Wings);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Mharadium Wings";
            item.width = 24;
            item.height = 30;
            item.toolTip = "Only true heroes can handle its power!";
            item.toolTip2 = "Allows 5 minutes of flight.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player)
        {
            player.wingTimeMax = 18000; // 5 minutes flight.
        }

        public override void VerticalWingSpeeds(ref float ascentWhenFalling, ref float ascentWhenRising,
           ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(ref float speed, ref float acceleration)
        {
            speed = 10f;
            acceleration *= 3f;
        }

        public override void WingUpdate(Player player, bool inUse)
        {
            if (inUse)
                Dust.NewDust(player.position, player.width, player.height, mod.DustType("Flame"), 0, 0, 0, Color.Red);
            base.WingUpdate(player, inUse);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
