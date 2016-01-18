using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons
{
    public class MharadiumRocketLauncher : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Cannon";
            item.width = 50;
            item.height = 20;
            item.toolTip = "Only some can handle its great powers.";
            item.value = Item.sellPrice(10, 0, 0, 0);
            item.rare = 10;

            item.damage = 400;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.ranged = true;
            item.knockBack = 4f;
            item.useSound = 11;
            item.useAmmo = ItemID.RocketI;
            item.shoot = mod.ProjectileType("MharadiumRocketI");
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
