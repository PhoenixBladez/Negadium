using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Placeable
{
    public class Incubator : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Incubator";
            item.width = 48;
            item.height = 48;
            item.toolTip = "Capable of developing something evil.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;

            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("Incubator");
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
