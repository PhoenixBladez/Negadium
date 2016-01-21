using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons.Mharadium
{
    public class FollowingMharadiumBullet : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Bullet";
            item.width = 8;
            item.height = 8;
            item.toolTip = "Only true heroes can handle its power!";
            item.toolTip2 = "This bullet follows it's enemies.";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.maxStack = 999;
            item.rare = 10;

            item.ranged = true;
            item.consumable = true;
            item.damage = 80;
            item.knockBack = 4.5F;
            item.shootSpeed = 5F;
            item.shoot = mod.ProjectileType("FollowingMharadiumBullet");
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
