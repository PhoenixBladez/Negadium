using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons.Mharadium
{
    public class MharadiumYoyo : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Zealot";
            item.width = 18;
            item.height = 18;
            item.toolTip = "Only true heroes can handle its power!";
            item.rare = 10;

            item.damage = 300;
            item.knockBack = 3.25f;
            item.crit += 20;

            item.useStyle = 5;
            item.noUseGraphic = true;
            item.useSound = 1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("MharadiumYoyo");
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 32f;
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
