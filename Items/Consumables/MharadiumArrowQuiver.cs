using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Consumables
{
    public class MharadiumArrowQuiver : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Arrow Quiver";
            item.width = 20;
            item.height = 20;
            item.toolTip = "Right click for Mharadium Arrows";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("MharadiumArrow"), 999);
            item.stack = 2;
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
