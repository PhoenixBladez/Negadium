using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons.Mharadium
{
    public class MharadiumFlail : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "The Guardian";
            item.width = 30;
            item.height = 10;
            item.toolTip = "Only true heroes can handle its power!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;

            item.noMelee = true;
            item.useStyle = 5;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 7.5F;
            item.damage = 450;
            item.scale = 1.1F;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("MharadiumBall");
            item.shootSpeed = 15.9F;
            item.useSound = 1;
            item.melee = true;
            item.channel = true;
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
