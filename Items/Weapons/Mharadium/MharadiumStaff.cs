using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons.Mharadium
{

    public class MharadiumStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Pyro Staff";
            item.damage = 375;
            item.magic = true;
            item.mana = 20;
            item.width = 40;
            item.height = 40;
            item.toolTip = "Shoots fiery pyro balls that have a chance to split";
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 10000;
            item.rare = 2;
            item.useSound = 20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("MharadiumInferno_Friendly");
            item.shootSpeed = 16f;
        }

        private Vector2 GetMouse(Player player)
        {
            Vector2 position = Main.screenPosition;
            position.X += Main.mouseX;
            position.Y += player.gravDir == 1 ? Main.mouseY : Main.screenHeight - Main.mouseY;
            return position;
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
