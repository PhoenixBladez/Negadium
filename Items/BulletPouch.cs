using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items
{
    public class BulletPouch : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Bullet Pouch";
            item.width = 20;
            item.height = 20;
            item.toolTip = "Right click for bullets.";
            item.rare = 10;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.RocketI, 999);
            player.QuickSpawnItem(ItemID.RocketII, 999);
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
