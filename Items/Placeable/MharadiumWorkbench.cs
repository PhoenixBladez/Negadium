using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Placeable
{
    public class MharadiumWorkbench : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Workbench";
            item.width = 28;
            item.height = 14;
            item.maxStack = 99;
            AddTooltip("A Workbench made out of pure Mharadium.");
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150;
            item.createTile = mod.TileType("MharadiumWorkbench");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MharadiumBar", 6);
            recipe.AddTile(null, "MharadiumForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
