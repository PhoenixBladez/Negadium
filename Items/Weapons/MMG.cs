using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Negadium.Items.Weapons
{
    public class MMG : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "M.M.G.";
            item.width = 62;
            item.height = 26;
            item.toolTip = "Only true heroes can handle its power!";
            item.toolTip2 = "50% chance not to consume ammo.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;

            item.damage = 130;
            item.crit += 20;

            item.ranged = true;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = 5;
            item.autoReuse = true;
            item.noMelee = true;
            item.knockBack = 2;
            item.useSound = 40;
            item.shoot = 10;
            item.shootSpeed = 12F;
            item.useAmmo = ProjectileID.Bullet;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.Y -= 10 * player.gravDir;
            speedX = speedX + (float)Main.rand.Next(-40, 41) * 0.01f;
            speedY = speedY + (float)Main.rand.Next(-40, 41) * 0.01f;
            return true; // Spawn the bullet with the changed position and rotation.
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 2) == 0) // 50% chance not to consume ammo.
                return false;
            return true;
        }

        public override bool UseItem(Player player)
        {
            return true;// return base.UseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 30);
            target.AddBuff(BuffID.Ichor, 30);
            base.OnHitNPC(player, target, damage, knockBack, crit);
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
