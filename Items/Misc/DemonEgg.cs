using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Misc
{
    public class DemonEgg : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Demon Egg";
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.toolTip = "Hmm, what could possibly be growing in this egg?";
            item.value = 0;
            item.rare = 10;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            Main.NewText("An evil spirit has been released in your world. He will haunt you until you defeat him!", 0, 255, 0, true);
            player.QuickSpawnItem(mod.ItemType("DemonFetus"));
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
