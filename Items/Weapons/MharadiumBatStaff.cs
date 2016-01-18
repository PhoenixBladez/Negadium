using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons
{
    public class MharadiumBatStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Bat Staff";
            item.width = 26;
            item.height = 28;
            item.toolTip = "Summons a Mharadium Bat to fight for you";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 180;
            item.knockBack = 2f;
            item.useStyle = 1;
            item.useTime = 28;
            item.useAnimation = 28;

            item.noMelee = true;
            item.mana = 30;
            item.shoot = mod.ProjectileType("MharadiumBatMinion");
            item.buffType = mod.BuffType("MharadiumBatMinionBuff");
            item.buffTime = 3600;

            item.shootSpeed = 10f;

            item.useSound = 44;

            item.summon = true;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.X = (float)Main.mouseX + Main.screenPosition.X;
            position.Y = (float)Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(position.X, position.Y, 0, 0, type, 10, 0.5F, player.whoAmI, Main.rand.Next(1, 4), 0f);
            return false;
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
