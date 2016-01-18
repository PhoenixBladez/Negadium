using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons
{
    public class MharadiumArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Arrow";
            item.width = 10;
            item.height = 28;
            item.toolTip = "Only true heroes can handle its power!";
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 10;

            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType("MharadiumArrow");
            item.damage = 50;
            item.maxStack = 999;
            item.consumable = true;
            item.ammo = 1;
            item.knockBack = 2f;
            item.ranged = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 999);
            recipe.AddRecipe();
        }
    }
}
