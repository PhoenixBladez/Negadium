using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Accessories2.Mharadium
{
    public class MharadiumHook : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Hands of Satan";
            item.width = 62;
            item.height = 26;
            item.toolTip = "Only true heroes can handle its power!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;

            item.noUseGraphic = true;
            item.damage = 180;
            item.useStyle = 5;
            item.shootSpeed = 16F;
            item.shoot = mod.ProjectileType("MharadiumHook");
            item.useSound = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.noMelee = true;
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
