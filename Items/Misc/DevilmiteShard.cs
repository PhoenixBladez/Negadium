using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Misc
{
    public class DevilmiteShard : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Devilmite Shard";
            item.width = 24;
            item.height = 24;
            item.toolTip = "A shard that came loose when you fought Satan.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 11;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.AddTile(null, "MharadiumAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
