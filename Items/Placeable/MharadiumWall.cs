using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Placeable
{
    public class MharadiumWall : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Wall";
            item.width = 12;
            item.height = 12;
            AddTooltip("A wall made out of pure Mharadium.");
            item.rare = 9;

            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 7;
            item.useStyle = 1;
            item.consumable = true;
            item.createWall = mod.WallType("MharadiumWall");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MharadiumBrick");
            recipe.AddTile(null, "MharadiumForge");
            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}
