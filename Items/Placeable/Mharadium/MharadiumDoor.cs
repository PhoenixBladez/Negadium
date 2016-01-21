using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Placeable.Mharadium
{
    public class MharadiumDoor : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Door";
            item.width = 14;
            item.height = 28;
            item.toolTip = "A door made out of pure Mharadium.";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 10;

            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("MharadiumDoorClosed");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MharadiumBar", 4);
            recipe.AddTile(null, "MharadiumAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
