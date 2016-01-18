using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons
{
    public class MharadiumSword : ModItem
    {
        int shootCooldown;

        public override void SetDefaults()
        {
            item.name = "Evangelion";
            item.width = 56;
            item.height = 56;
            item.toolTip = "Only true heroes can handle its power!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;

            item.damage = 700;
            item.crit += 25;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.autoReuse = true;
            item.melee = true;
            item.useTurn = true;
            item.knockBack = 6.5F;
            item.scale = 1.1F;

            item.shoot = mod.ProjectileType("DevilsBeam");
            item.shootSpeed = 12F; // The speed at which the projectile will travel.

            item.useSound = 1;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 direction = new Vector2(speedX, speedY);
            direction.Normalize();
            position += direction * item.width;

            if (shootCooldown >= 60)
            {
                shootCooldown = 0;
                return true;
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 30);
            target.AddBuff(BuffID.Cursed, 30);            

            base.OnHitNPC(player, target, damage, knockBack, crit);
        }

        public override void UpdateInventory(Player player)
        {
            shootCooldown++;
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
