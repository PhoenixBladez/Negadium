using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Misc.Mharadium
{
    public class MharadiumBar : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Bar";
            item.width = 30;
            item.height = 24;
            item.toolTip = "A bar of pure Mharadium.";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.maxStack = 999;
            item.rare = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MharadiumOre", 6);
            recipe.AddTile(null, "MharadiumForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
