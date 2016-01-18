using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons
{
    public class PiercingMharadiumBullet : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Piercing Mharadium Bullet";
            item.damage = 70;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.toolTip = "Only true heroes can handle its power!";
            item.toolTip = "This bullet pierces enemies.";
            item.consumable = true;
            item.knockBack = 4f;
            item.value = 10;
            item.rare = 10;
            item.shoot = mod.ProjectileType("PiercingMharadiumBullet");
            item.shootSpeed = 5F;
            item.ammo = ProjectileID.Bullet;
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
