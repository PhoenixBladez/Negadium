using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons
{
    public class MharadiumRocketII : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Rocket II";
            item.shoot = 1;
            item.damage = 40;
            item.width = 20;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.ammo = ItemID.RocketI;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.ranged = true;
            item.toolTip = "Small blast radius. Will destroy tiles";
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
